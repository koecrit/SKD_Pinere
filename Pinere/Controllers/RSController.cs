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
    public class RSController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            string responsibleId = string.Empty;
            string responsibleName = string.Empty;

            using (var dca = new PinereDataContext(PinereConstant.PinereConnectionString))
            {
                var resp = dca.GetResponsibleByUserName(User.Identity.Name, "rs").SingleOrDefault();
                if (!User.IsInRole("admin"))
                {
                    if (resp != null)
                    {
                        responsibleId = resp.Id.Value.ToString();
                        responsibleName = resp.Name;
                    }
                }
            }

            ViewBag.RSData = DataRepository.GetTotalDataForRS(int.Parse(responsibleId));
         
            return View();
        }
        public ActionResult Task()
        {
            ViewBag.Title = "Data Pasien RS";
            SearchParameter model = new SearchParameter();
            string responsibleId = string.Empty;
            string responsibleName = string.Empty;

            using (var dca = new PinereDataContext(PinereConstant.PinereConnectionString))
            {
                var resp = dca.GetResponsibleByUserName(User.Identity.Name, "rs").SingleOrDefault();
                if (!User.IsInRole("admin"))
                {
                    if (resp != null)
                    {
                        responsibleId = resp.Id.Value.ToString();
                        responsibleName = resp.Name;
                    }
                }
            }

            ViewBag.RSData = DataRepository.GetTotalDataForRS(int.Parse(responsibleId));

            return View("Task", model);
        }
        public ActionResult GetResultData()
        {
            string responsibleId = string.Empty;
            string responsibleName = string.Empty;

            using (var dca = new PinereDataContext(PinereConstant.PinereConnectionString))
            {
                var resp = dca.GetResponsibleByUserName(User.Identity.Name, "rs").SingleOrDefault();
                if (!User.IsInRole("admin"))
                {
                    if (resp != null)
                    {
                        responsibleId = resp.Id.Value.ToString();
                        responsibleName = resp.Name;
                    }
                }
            }
            
            var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
            List<SearchResult> ResultList = new List<SearchResult>();
            try
            {
                var Result = (from a in dc.GetPasienListForRS(int.Parse(responsibleId)) select a);
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
                                  RSFlag = a.RS_Flag.ToString(),
                                  NamaRS = a.NamaRS,
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
            @ViewBag.Title = "Input Data Pasien RS";
            PinereDataModel model = this.GetPinnedModel<PinereDataModel>(true);
            model = new PinereDataModel();

            try
            {
                string responsibleId = string.Empty;
                string responsibleName = string.Empty;

                using (var dca = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    var resp = dca.GetResponsibleByUserName(User.Identity.Name, "rs").SingleOrDefault();
                    if (!User.IsInRole("admin"))
                    {
                        if (resp != null)
                        {
                            responsibleId = resp.Id.Value.ToString();
                            responsibleName = resp.Name;
                        }
                    }
                }

                ViewBag.RSData = DataRepository.GetTotalDataForRS(int.Parse(responsibleId));
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
            
            try
            {
                var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
                try
                {
                    dc.Connection.Open();
                    dc.Transaction = dc.Connection.BeginTransaction();
                    dc.ObjectTrackingEnabled = true;

                    DataRepository.SaveDataSampel(dc, UserName, Model.Sampel, Model.Pasien.Id);
                    DataRepository.UpdateRSFlag(dc, Model.KKP.Id);
                    dc.Transaction.Commit();
                }
                catch (Exception e)
                {
                    dc.Transaction.Rollback();
                    throw new Exception(e.Message);
                }
                SendEmail.SendMail("ptgs.balitbangkes@gmail.com", "[Pinere] Data Pasien Baru", "Ini adalah system notifikasi dari PINERE System.<br> Silahkan login dan buka di PINERE System.<br><br> Terimakasih<br>PinereSystem.");
               
            }
            catch (Exception e)
            {
                return (ActionResult)this.RedirectToAction("Error", "Information", new { @Message = e.Message });
            }
            return (ActionResult)this.RedirectToAction("Submitted", "Information", new { @MainId = "" });
        }
    }
}
