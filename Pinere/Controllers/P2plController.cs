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
    public class P2plController : Controller
    {
        //
        // GET: /P2pl/

        public ActionResult Index()
        {
            ViewBag.Title = "Data";
            SearchParameter model = new SearchParameter();

            return View("Index", model);
        }
        public ActionResult GetResultData()
        {
            var dc = new PinereDataContext(PinereConstant.PinereConnectionString);
            List<SearchResult> ResultList = new List<SearchResult>();
            try
            {
                var Result = (from a in dc.GetPasienListForRS(0) select a);
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
                                  HasilLab = a.Hasil_Lab,
                                  ActionEdit = Url.Action("View", new { @KKPId = a.KKPId })
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

    }
}
