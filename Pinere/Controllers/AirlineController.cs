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
    public class AirlineController : Controller
    {
        //
        // GET: /Airline/

        public ActionResult Input()
        {
            ViewBag.Title = "Input Data Airline";
            string responsibleId = string.Empty;
            string responsibleName = string.Empty;

            using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
            {
                var resp = dc.GetResponsibleByUserName(User.Identity.Name, "airline").SingleOrDefault();
                if (!User.IsInRole("admin"))
                {
                    if (resp != null)
                    {
                        responsibleId = resp.Id.Value.ToString();
                        responsibleName = resp.Name;
                    }
                }
            }

            ViewBag.ResponsibleId = responsibleId;
            ViewBag.ResponsibleName = responsibleName;
            PinereDataModel model = new PinereDataModel();
            
            return View("Input", model);
        }
        public ActionResult Tracking()
        {
            ViewBag.Title = "Data Airline History";
            string responsibleId = string.Empty;
            string responsibleName = string.Empty;

            using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
            {
                var resp = dc.GetResponsibleByUserName(User.Identity.Name, "airline").SingleOrDefault();
                if (!User.IsInRole("admin"))
                {
                    if (resp != null)
                    {
                        responsibleId = resp.Id.Value.ToString();
                        responsibleName = resp.Name;
                    }
                }
            }

            ViewBag.ResponsibleId = responsibleId;
            ViewBag.ResponsibleName = responsibleName;

            SearchParameter model = new SearchParameter();

            return View("Tracking", model);
        }
        public ActionResult Edit(int DataAirlineId)
        {
            @ViewBag.Title = "View Data Airline";

            PinereDataModel model = this.GetPinnedModel<PinereDataModel>(true);
            model = new PinereDataModel();
            try
            {
                model.DataAirline = DataRepository.GetDataAirline(DataAirlineId);
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return View("Edit", model);
        }
        public ActionResult GetResultData(string search, string airline, string nomorPenerbangan, string kotaAsal, string kotaTujuan, string waktuBerangkat, string penumpangSakit)
        {
            var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
            List<SearchResult> ResultList = new List<SearchResult>();
            try
            {
                if (search == "True")
                {
                    var Result = (from a in dc.GetDataAirlineList() select a);
                    if (!airline.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Nama_Airline.Value.ToString() == airline);
                    }
                    if (!nomorPenerbangan.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Nomor_Penerbangan.Contains(nomorPenerbangan));
                    }
                    if (!kotaAsal.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Kota_Asal == kotaAsal);
                    }
                    if (!kotaTujuan.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Kota_Tujuan == kotaTujuan);
                    }
                    if (!waktuBerangkat.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => DataRepository.GetDateStringFromDate(o.Waktu_Berangkat) == waktuBerangkat);
                    }
                    if (!penumpangSakit.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Penumpang_Sakit.Value.ToString() == penumpangSakit);
                    }

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
                                      ActionEdit = Url.Action("Edit", new { @DataAirlineId = a.ID })
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Submit(PinereDataModel Model)
        {
            string errorMessage = string.Empty;
            string UserName = User.Identity.Name;
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
                    DataRepository.SavePetugasAirline(dc, int.Parse(DataAirlineId), UserName, Model.DataAirline.PetugasAirlineCollection.PetugasAirline);
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
                if (Model.DataAirline.PenumpangSakit == "1")
                {
                    SendEmail.SendMail("ptgs.kkp@gmail.com", "[Pinere] Data Airline Baru", "Ini adalah system notifikasi dari PINERE System.<br> Silahkan login dan buka di PINERE System.<br><br> Terimakasih<br>PinereSystem.");   
                }
            }
            catch (Exception e)
            {
                return (ActionResult)this.RedirectToAction("Error", "Information", new { @Message = e.Message });
            }
            return (ActionResult)this.RedirectToAction("Submitted", "Information", new { @MainId = DataAirlineId });
        }

    }
}
