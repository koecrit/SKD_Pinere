using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Pinere.Models;
using Pinere.Helper;
using MICA.Web.Mvc.ModelBinders;

namespace Pinere
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(PetugasAirline[]), new JsonModelBinder<PetugasAirline[]>());
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        using (PinereDataContext entities = new PinereDataContext(PinereConstant.PinereConnectionString))
                        {
                            //User user = entities.tbl_users.SingleOrDefault(u => u.username == username);
                            tbl_user tbl_user = entities.tbl_users.SingleOrDefault(u => u.username == username);
                            User user = null;
                            if (tbl_user != null)
                            {
                                user = new Models.User
                                {
                                    UserId = tbl_user.user_id,
                                    Username = tbl_user.username,
                                    Role = tbl_user.role,
                                    Password = tbl_user.password,
                                    LongName = tbl_user.long_name,
                                    Email = tbl_user.email
                                };
                            }
                            else
                            {
                                user = new User();
                            }
                            roles = user.Role;
                        }
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }
    }
}