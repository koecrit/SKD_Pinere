using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Pinere.Helper
{
    public class PinereConstant
    {
        public static string ProcessName = ConfigurationManager.AppSettings["ProcessName"];
        public static string PinereConnectionString = ConfigurationManager.ConnectionStrings["PinereConnectionString"].ConnectionString;

        public static string ErrorMessageAjax = ConfigurationManager.AppSettings["ErrorMessageAjax"];
        public static string ErrorMessageSaveAjax = ConfigurationManager.AppSettings["ErrorMessageSaveAjax"];
        public static string ErrorMessageSubmit = ConfigurationManager.AppSettings["ErrorMessageSubmit"];

        public static string AttachmentUrl = ConfigurationManager.AppSettings["AttachmentUrl"];
    }
}