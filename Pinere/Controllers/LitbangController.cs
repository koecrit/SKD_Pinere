using Pinere.Models;
using Pinere.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using System.Globalization;
using MICA.Web.Mvc;
using MICA.Web.Mvc.Extensions;
using MICA.Web.Mvc.Helper;

namespace Pinere.Controllers
{
    public class LitbangController : Controller
    {
        public ActionResult Task()
        {
            ViewBag.Title = "Data Pasien Litbang";
            SearchParameter model = new SearchParameter();
            ViewBag.LitbangData = DataRepository.GetTotalDataForLitbang();

            return View("Task", model);
        }
        public ActionResult GetResultData()
        {
            var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
            List<SearchResult> ResultList = new List<SearchResult>();
            try
            {
                var Result = (from a in dc.GetPasienListForLitbang() select a);
                Result = Result.OrderByDescending(o => o.KKPId);
                ResultList = (from a in Result
                              select new SearchResult
                              {
                                  NamaAirline = a.NamaAirline,
                                  NomorPenerbangan = a.NomorPenerbangan,
                                  NamaPasien = a.NamaPasien,
                                  TanggalLahir = DataRepository.GetDateStringFromDate(a.TanggalLahir),
                                  HasilDiagnosa = a.Diagnosa,
                                  RujukRS = a.Rujuk_RS_Dis,
                                  LitbangFlag = a.Litbang_Flag.ToString(),
                                  ActionEdit = Url.Action("Input", new { @KKPId = a.KKPId })
                              }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(new
            {
                iTotalRecords = ResultList.Count(),
                iTotalDisplayRecords = ResultList.Count(),
                aaData = ResultList
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Input(int KKPId)
        {
            @ViewBag.Title = "Input Hasil Lab";
            PinereDataModel model = this.GetPinnedModel<PinereDataModel>(true);
            model = new PinereDataModel();

            try
            {
                ViewBag.LitbangData = DataRepository.GetTotalDataForLitbang();
                model.KKP = DataRepository.GetDataKKP(KKPId);
                model.Pasien = DataRepository.GetPasien(int.Parse(model.KKP.PasienId));
                model.DataAirline = DataRepository.GetDataAirline(int.Parse(model.Pasien.DataAirlineId));
                model.Sampel = DataRepository.GetSampel(int.Parse(model.KKP.PasienId));
                model.Litbang = DataRepository.GetLitbang(int.Parse(model.KKP.PasienId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return View("Input", model);
        }
        public ActionResult GetKriteraDanTindakan(string Cat, string Resiko)
        {
            Boolean IsSuccess = false;
            string ErrorMessage = string.Empty;
            string Kriteria = string.Empty;
            string Tindakan = string.Empty;
            try
            {
                Kriteria = DataRepository.GetKriteriaBasedOnResiko(Cat, Resiko);
                Tindakan = DataRepository.GetTindakanBasedOnResiko(Cat, Resiko);
                IsSuccess = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(new
            {
                status = IsSuccess,
                kriteria = Kriteria,
                tindakan = Tindakan,
                message = ErrorMessage
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Submit(PinereDataModel Model)
        {
            string errorMessage = string.Empty;
            string UserName = User.Identity.Name;
            string AttachmentPath = string.Empty;
            string AttachmentDir = string.Empty;
            string FileName = string.Empty;

            try
            {
                var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
                try
                {
                    dc.Connection.Open();
                    dc.Transaction = dc.Connection.BeginTransaction();
                    dc.ObjectTrackingEnabled = true;

                    DataRepository.SaveDataLitbang(dc, UserName, Model.Litbang, Model.Pasien.Id);
                    DataRepository.UpdateLitbangFlag(dc, Model.Sampel.Id);
                    dc.Transaction.Commit();

                    AttachmentPath = string.Format("{0}/{1}", PinereConstant.AttachmentUrl, Model.Pasien.Id.ToString());
                    AttachmentDir = Server.MapPath(AttachmentPath);
                    if (!Directory.Exists(AttachmentDir))
                    {
                        Directory.CreateDirectory(AttachmentDir);
                    }
                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf.ContentLength == 0)
                        {
                            continue;
                        }
                        FileName = Path.GetFileName(hpf.FileName);
                        AttachmentDir = AttachmentDir + "\\" + FileName;
                        hpf.SaveAs(AttachmentDir);
                    }
                }
                catch (Exception e)
                {
                    dc.Transaction.Rollback();
                    throw new Exception(e.Message);
                }
                SendEmail.SendMail("ptgs.p2pl@gmail.com", "[Pinere] Data Pasien Baru", "Ini adalah system notifikasi dari PINERE System.<br> Silahkan login dan buka di PINERE System.<br><br> Terimakasih<br>PinereSystem.");
            }
            catch (Exception e)
            {
                return (ActionResult)this.RedirectToAction("Error", "Information", new { @Message = e.Message });
            }
            return (ActionResult)this.RedirectToAction("Submitted", "Information", new { @MainId = "" });
        }

    }
}
