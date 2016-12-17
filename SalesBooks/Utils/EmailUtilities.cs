using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using DAL.Entities;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Services;

namespace Utils
{
    public class EmailUtilities
    {
        public static bool SendActivationEmail(User u, string urlActivation)
        {
            string shortUrlActivation = shortenIt(urlActivation);

            string subject = "Activación de cuenta";
            string body = "Hola " + u.UserName + ",";
            body += "<br /><br />Por favor haga click en el siguiente link para activar su cuenta:";
            body += "<br /><a href = '" + shortUrlActivation + "'>Activar cuenta.</a>";
            body += "<br /><br />Gracias.";

            string AccUserName = ConfigurationManager.AppSettings["accountUsername"].ToString();
            string accPassword = ConfigurationManager.AppSettings["accountPassword"].ToString();

            SmtpClient MyMail = new SmtpClient();

            MailMessage MyMsg = new MailMessage();
            MyMail.Host = "smtp.gmail.com";
            MyMail.Port = 587;
            MyMail.EnableSsl = true;

            MyMsg.Priority = MailPriority.High;
            MyMsg.To.Add(new MailAddress(u.Email));
            MyMsg.Subject = subject;
            MyMsg.SubjectEncoding = Encoding.UTF8;
            MyMsg.IsBodyHtml = true;
            MyMsg.From = new MailAddress(AccUserName, "Leonel Bonifazi");
            MyMsg.BodyEncoding = Encoding.UTF8;
            MyMsg.Body = body;
            MyMail.UseDefaultCredentials = false;
            NetworkCredential MyCredentials = new NetworkCredential(AccUserName, accPassword);
            MyMail.Credentials = MyCredentials;

            try
            {
                MyMail.Send(MyMsg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string shortenIt(string url)
        {
            UrlshortenerService service = new UrlshortenerService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAFVnxJQOZD3kbuUojMnqgLREQ5Ogh8HF0",
                ApplicationName = "Leonel Bonifazi",
            });

            var m = new Google.Apis.Urlshortener.v1.Data.Url();
            m.LongUrl = url;
            return service.Url.Insert(m).Execute().Id;
        }
    }
}
