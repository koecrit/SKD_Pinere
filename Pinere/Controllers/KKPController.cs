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
    public class KKPController : Controller
    {
        //
        // GET: /KKP/
        public ActionResult Task()
        {
            ViewBag.Title = "Data Airline";
            SearchParameter model = new SearchParameter();
            ViewBag.KKPData = DataRepository.GetTotalDataForKKP();

            return View("Task", model);
        }
        public ActionResult GetResultData()
        {
            var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
            List<SearchResult> ResultList = new List<SearchResult>();
            try
            {
                var Result = (from a in dc.GetDataAirlineForKKP() select a);
                Result = Result.OrderByDescending(o => o.CreatedDate);
                ResultList = (from a in Result
                              select new SearchResult
                              {
                                  NamaAirline = a.Nama_Airline_Dis,
                                  NomorPenerbangan = a.Nomor_Penerbangan,
                                  WaktuBerangkat = DataRepository.GetDateStringFromDate(a.Waktu_Berangkat),
                                  KotaAsal = a.Kota_Asal_Dis,
                                  KotaTujuan = a.Kota_Tujuan_Dis,
                                  PenumpangSakit = a.Penumpang_Sakit_Dis,
                                  KKPFlag = a.KKP_Flag.Value.ToString(),
                                  ActionEdit = Url.Action("Input", new { @DataAirlineId = a.ID })
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
        public ActionResult Input(int DataAirlineId)
        {
            @ViewBag.Title = "View Data Airline";
            PinereDataModel model = this.GetPinnedModel<PinereDataModel>(true);
            model = new PinereDataModel();
            
            try
            {
                ViewBag.KKPData = DataRepository.GetTotalDataForKKP();
                model.DataAirline = DataRepository.GetDataAirline(DataAirlineId);
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return View("Input", model);
        }
        public ActionResult NewEntry()
        {
            @ViewBag.Title = "Input Data KKP";
            PinereDataModel model = this.GetPinnedModel<PinereDataModel>(true);
            model = new PinereDataModel();

            try
            {
                ViewBag.KKPData = DataRepository.GetTotalDataForKKP();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return View("NewEntry", model);
        }
        public ActionResult Tracking()
        {
            ViewBag.Title = "Pasien History";
            SearchParameter model = new SearchParameter();
            ViewBag.KKPData = DataRepository.GetTotalDataForKKP();

            return View("Tracking", model);
        }
        public ActionResult Edit(int KKPId)
        {
            @ViewBag.Title = "View Pasien";
            PinereDataModel model = this.GetPinnedModel<PinereDataModel>(true);
            model = new PinereDataModel();
            try
            {
                ViewBag.KKPData = DataRepository.GetTotalDataForKKP();
                model.KKP = DataRepository.GetDataKKP(KKPId);
                model.Pasien = DataRepository.GetPasien(int.Parse(model.KKP.PasienId));
                model.DataAirline = DataRepository.GetDataAirline(int.Parse(model.Pasien.DataAirlineId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return View("Edit", model);
        }
        public ActionResult GetResultDataPasien(string search, string airline, string nomorPenerbangan, string namaPasien, string tanggalLahir, string rujukRS)
        {
            var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
            List<SearchResult> ResultList = new List<SearchResult>();
            try
            {
                if (search == "True")
                {
                    var Result = (from a in dc.GetPasienList() select a);
                    if (!airline.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.AirlineId.Value.ToString() == airline);
                    }
                    if (!nomorPenerbangan.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.NomorPenerbangan.Contains(nomorPenerbangan));
                    }
                    if (!namaPasien.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.NamaPasien.Contains(namaPasien));
                    }
                    if (!tanggalLahir.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => DataRepository.GetDateStringFromDate(o.TanggalLahir) == tanggalLahir);
                    }
                    if (!rujukRS.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Rujuk_RS.ToString() == rujukRS);
                    }

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
                                      ActionEdit = Url.Action("Edit", new { @KKPId = a.KKPId })
                                  }).ToList();
                }
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
        public ActionResult GetMaskapai()
        {
            List<Maskapai> MaskapaiList = new List<Maskapai>();
            string Filter = Request.QueryString["sSearch"].SafeTrim();
            int TotalRecords = 0;
            int TotalDisplayRecords = 0;
            int DisplayStart = 0;
            int output = 0;
            if (int.TryParse(Request.QueryString["iDisplayStart"].SafeTrim(), out output))
            {
                DisplayStart = output;
            }

            int DisplayLength = 10;
            if (int.TryParse(Request.QueryString["iDisplayLength"].SafeTrim(), out output))
            {
                DisplayLength = output;
            }
            try
            {
                var Maskapai = DataRepository.GetMaskapai().AsQueryable();
                TotalRecords = Maskapai.Count();
                Maskapai = Maskapai.Where(p => p.Id.ToUpper().Contains(Filter) || p.Name.ToUpper().Contains(Filter.ToUpper()) || p.Country.ToUpper().Contains(Filter.ToUpper()));
                TotalDisplayRecords = Maskapai.Count();
                Maskapai = Maskapai.Skip(DisplayStart).Take(DisplayLength);
                MaskapaiList = Maskapai.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(new
            {
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalDisplayRecords,
                aaData = MaskapaiList
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKota()
        {
            List<City> CityList = new List<City>();
            string Filter = Request.QueryString["sSearch"].SafeTrim();
            int TotalRecords = 0;
            int TotalDisplayRecords = 0;
            int DisplayStart = 0;
            int output = 0;
            if (int.TryParse(Request.QueryString["iDisplayStart"].SafeTrim(), out output))
            {
                DisplayStart = output;
            }

            int DisplayLength = 10;
            if (int.TryParse(Request.QueryString["iDisplayLength"].SafeTrim(), out output))
            {
                DisplayLength = output;
            }
            try
            {
                var City = DataRepository.GetKota().AsQueryable();
                TotalRecords = City.Count();
                City = City.Where(p => p.Code.ToUpper().Contains(Filter) || p.Name.ToUpper().Contains(Filter.ToUpper()) || p.Country.ToUpper().Contains(Filter.ToUpper()));
                TotalDisplayRecords = City.Count();
                City = City.Skip(DisplayStart).Take(DisplayLength);
                CityList = City.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(new
            {
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalDisplayRecords,
                aaData = CityList
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRS()
        {
            List<RS> RSList = new List<RS>();
            string Filter = Request.QueryString["sSearch"].SafeTrim();
            int TotalRecords = 0;
            int TotalDisplayRecords = 0;
            int DisplayStart = 0;
            int output = 0;
            if (int.TryParse(Request.QueryString["iDisplayStart"].SafeTrim(), out output))
            {
                DisplayStart = output;
            }

            int DisplayLength = 10;
            if (int.TryParse(Request.QueryString["iDisplayLength"].SafeTrim(), out output))
            {
                DisplayLength = output;
            }
            try
            {
                var RS = DataRepository.GetRS().AsQueryable();
                TotalRecords = RS.Count();
                RS = RS.Where(p => p.Nama.ToUpper().Contains(Filter) || p.Alamat.ToUpper().Contains(Filter.ToUpper()));
                TotalDisplayRecords = RS.Count();
                RS = RS.Skip(DisplayStart).Take(DisplayLength);
                RSList = RS.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(new
            {
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalDisplayRecords,
                aaData = RSList
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKriteraDanTindakan(string Cat, string Resiko)
        {
            Boolean IsSuccess = false;
            string ErrorMessage = string.Empty;
            string Kriteria = string.Empty;
            string Tindakan = string.Empty;
            try
            {
                Kriteria = DataRepository.GetKriteriaBasedOnResiko(Cat,Resiko);
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
            string PasienId = string.Empty;
            string DataAirlineId = Model.Pasien.DataAirlineId;

            try
            {
                var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
                try
                {
                    dc.Connection.Open();
                    dc.Transaction = dc.Connection.BeginTransaction();
                    dc.ObjectTrackingEnabled = true;

                    DataRepository.SavePasien(dc, UserName,int.Parse(Model.Pasien.DataAirlineId), Model.Pasien, out PasienId);
                    DataRepository.SaveDataKKP(dc, UserName, Model.KKP, int.Parse(PasienId));
                    DataRepository.UpdateKKPFlag(dc, int.Parse(DataAirlineId));
                    dc.Transaction.Commit();
                }
                catch (Exception e)

                {
                    dc.Transaction.Rollback();
                    throw new Exception(e.Message);
                }
                if (Model.KKP.RujukRS == "1" && (Model.KKP.HasilDiagnosa == "01EBL" || Model.KKP.HasilDiagnosa == "02MRS") )
                {
                    string email = string.Empty;
                    using (var dca = new PinereDataContext(PinereConstant.PinereConnectionString))
                    {
                        var resp = dca.GetEmailByNameAndResp(User.Identity.Name,Model.KKP.IdRS, "rs").SingleOrDefault();
                        if (resp != null)
                        {
                            email = resp.Email;
                        }
                    }
                    if (!email.IsNullOrEmpty())
                    {
                        SendEmail.SendMail(email, "[Pinere] Data Pasien Baru", "Ini adalah system notifikasi dari PINERE System.<br> Silahkan login dan buka di PINERE System.<br><br> Terimakasih<br>PinereSystem.");   
                    }
                }
            }
            catch (Exception e)
            {
                return (ActionResult)this.RedirectToAction("Error", "Information", new { @Message = e.Message });
            }
            return (ActionResult)this.RedirectToAction("SubmitKKP", "Information", new { @MainId = DataAirlineId });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitNew(PinereDataModel Model)
        {
            string errorMessage = string.Empty;
            string UserName = User.Identity.Name;
            string PasienId = string.Empty;
            string DataAirlineId = string.Empty;
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

                    DataRepository.SaveDataAirline(dc, UserName, Model.DataAirline, out DataAirlineId);
                    DataRepository.SavePasien(dc, UserName,int.Parse(DataAirlineId), Model.Pasien, out PasienId);
                    DataRepository.SaveDataKKP(dc, UserName, Model.KKP, int.Parse(PasienId));
                    DataRepository.UpdateKKPFlag(dc, int.Parse(DataAirlineId));

                    dc.Transaction.Commit();

                    AttachmentPath = string.Format("{0}/{1}", PinereConstant.AttachmentUrl, DataAirlineId);
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
                if (Model.KKP.RujukRS == "1" && (Model.KKP.HasilDiagnosa == "01EBL" || Model.KKP.HasilDiagnosa == "02MRS"))
                {
                    string email = string.Empty;
                    using (var dca = new PinereDataContext(PinereConstant.PinereConnectionString))
                    {
                        var resp = dca.GetEmailByNameAndResp(User.Identity.Name, Model.KKP.IdRS, "rs").SingleOrDefault();
                        if (resp != null)
                        {
                            email = resp.Email;
                        }
                    }
                    if (!email.IsNullOrEmpty())
                    {
                        SendEmail.SendMail(email, "[Pinere] Data Pasien Baru", "Ini adalah system notifikasi dari PINERE System.<br> Silahkan login dan buka di PINERE System.<br><br> Terimakasih<br>PinereSystem.");
                    }
                }
            }
            catch (Exception e)
            {
                return (ActionResult)this.RedirectToAction("Error", "Information", new { @Message = e.Message });
            }
            return (ActionResult)this.RedirectToAction("SubmitKKP", "Information", new { @MainId = DataAirlineId });
        }
    }
}
