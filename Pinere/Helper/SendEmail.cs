using Pinere.Models;
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
using System.Net;
using System.Net.Mail;

namespace Pinere.Helper
{
    public class SendEmail
    {
        public static void SendMail(string mailTo, string mailSubject, string mailBody)
        {
            string fromEmail = "p1n3r3@gmail.com";
            MailMessage mailMessage = new MailMessage(fromEmail, mailTo, mailSubject, mailBody);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromEmail, "ike@pinere");
            mailMessage.IsBodyHtml = true;
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
    }
}