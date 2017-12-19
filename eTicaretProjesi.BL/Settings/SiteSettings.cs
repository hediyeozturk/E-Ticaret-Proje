using eTicaretProjesi.ENT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.BL.Settings
{
    public class SiteSettings
    {
        public static string SiteMail = "platoakademi123@gmail.com";
        public static string SiteMailPassword = "1a2s3d4f..";
        public static string SiteMailSmtpHost = "smtp.gmail.com";
        public static int SiteMailSmtpPort = 587;
        public static bool SiteMailEnableSsl = true;


        public async static Task SendMail(MailViewModel model)
        {
            using (var smtp = new SmtpClient())
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.Kime));
                message.From = new MailAddress(SiteMail);
                if (!string.IsNullOrEmpty(model.Bcc))
                    message.Bcc.Add(new MailAddress(model.Bcc));
                if (!string.IsNullOrEmpty(model.Cc))
                    message.CC.Add(new MailAddress(model.Cc));
                message.Subject = model.Konu;
                message.IsBodyHtml = true;
                message.Body = model.Mesaj;
                var credential = new NetworkCredential
                {
                    UserName = SiteMail,
                    Password = SiteMailPassword
                };
                smtp.Credentials = credential;
                smtp.Host = SiteMailSmtpHost;
                smtp.Port = SiteMailSmtpPort;
                smtp.EnableSsl = SiteMailEnableSsl;
                await smtp.SendMailAsync(message);
            }

        }
    }
}
