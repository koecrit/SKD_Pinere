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
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Report()
        {
            ViewBag.Title = "Report";
            SearchParameter model = new SearchParameter();

            return View("Report", model);
        }
        public ActionResult GetResultData(string search, string airline, string nomorPenerbangan, string startDate, string endDate, string suspect, string rujukRS, string hasilLab, string kondisi)
        {
            var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
            List<SearchResult> ResultList = new List<SearchResult>();
            try
            {
                if (search == "True")
                {
                    var Result = (from a in dc.GetPasienListReport() select a);
                    if (!airline.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.AirlineId.Value.ToString() == airline);
                    }
                    if (!nomorPenerbangan.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.NomorPenerbangan.Contains(nomorPenerbangan));
                    }
                    if (!startDate.IsNullOrEmpty() && !endDate.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => (o.TanggalDatang >= DateTime.Parse(startDate) && o.TanggalDatang <= DateTime.Parse(endDate)));
                    }
                    if (!suspect.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Hasil_Diagnosa == suspect);
                    }
                    if (!rujukRS.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Rujuk_RS == rujukRS);
                    }
                    if (!hasilLab.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Hasil_Lab == hasilLab);
                    }
                    if (!kondisi.IsNullOrEmpty())
                    {
                        Result = Result.Where(o => o.Kondisi == kondisi);
                    }

                    Result = Result.OrderByDescending(o => o.TanggalDatang);
                    ResultList = (from a in Result
                                  select new SearchResult
                                  {
                                      NamaPasien = a.NamaPasien,
                                      TanggalLahir = DataRepository.GetDateStringFromDate(a.TanggalLahir),
                                      NamaAirline = a.NamaAirline,
                                      NomorPenerbangan = a.NomorPenerbangan,
                                      WaktuBerangkat = DataRepository.GetDateStringFromDate(a.TanggalDatang),
                                      Suspect = a.DiagnosaDis,
                                      RujukRS = a.Rujuk_RS_Dis,
                                      Resiko = a.tingkat_risiko_dis,
                                      HasilLab = a.Hasil_Lab_Dis,
                                      Kondisi = a.Kondisi_Dis, 
                                      ActionEdit = Url.Action("View", new { @KKPId = a.KKPId })
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
        public ActionResult View(int KKPId)
        {
            @ViewBag.Title = "View Data";
            PinereDataModel model = this.GetPinnedModel<PinereDataModel>(true);
            model = new PinereDataModel();

            try
            {
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
            return View("View", model);
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

    }
}
