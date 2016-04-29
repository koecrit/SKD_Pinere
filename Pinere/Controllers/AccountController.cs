using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Pinere.Models;
using Pinere.Helper;

namespace Pinere.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(User model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (var dc = new PinereDataContext(PinereConstant.PinereConnectionString))
                {
                    string username = model.Username;
                    string password = model.Password;

                    bool userVaid = dc.tbl_users.Any(user => user.username == username && user.password == password);
                    if (userVaid)
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        //{
                        //    return Redirect(returnUrl);
                        //}
                        //else
                        //{
                        var role = (from a
                               in dc.tbl_users
                                    where a.username == username && a.password == password
                                    select a.role).SingleOrDefault();

                        if (role == "rs")
                        {
                            return RedirectToAction("Index", "RS");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        //}
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username dan password tidak cocok, silahkan ulangi lagi.");
                    }
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
