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
    public class InformationController : Controller
    {
        //
        // GET: /Info/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Submitted(string MainId)
        {
            ViewBag.LitbangData = DataRepository.GetTotalDataForLitbang();
            ViewBag.MainId = MainId;

            return View();
        }
        public ActionResult Error(string Message)
        {
            ViewBag.Message = Message;

            return View();
        }
        public ActionResult SubmitKKP(string MainId)
        {
            ViewBag.KKPData = DataRepository.GetTotalDataForKKP();
            ViewBag.MainId = MainId;

            return View();
        }
    }
}
