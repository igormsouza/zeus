using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace Client.Zeus.Util
{
    public class Mail
    {
        private string from;
        private string to;
        private string subject;
        private string doby;
        private string displayName;
        private bool bCopia;

        public Mail(string from, string to, string subject, bool copy)
        {
            this.from = from;
            this.to = to;
            this.displayName = ConfigurationManager.AppSettings["DisplayNameEmail"];
            this.subject = subject;
            this.bCopia = copy;
        }

        public void TextMail(string mensage)
        {
            this.doby = mensage;
            SendMail(false);
        }

        
        public void HtmlMail(string name, string call, string title, string mensage, int ID)
        {
            this.doby = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/content/Email/template.html"));
            this.doby = this.doby.Replace("[URL]", ConfigurationManager.AppSettings["siteURL"]);
            this.doby = this.doby.Replace("[URLTOP]", ConfigurationManager.AppSettings["siteURL"] + "/Images/logo_60.png");
            this.doby = this.doby.Replace("[SUBJECT]", this.subject);
            this.doby = this.doby.Replace("[NAME]", name);
            this.doby = this.doby.Replace("[TITLE]", title);
            this.doby = this.doby.Replace("[MENSAGE]", mensage);
            
            SendMail(true);
        }
     
        public void SendMail(bool isHtml)
        {
            MailMessage mm = null;
            SmtpClient smtp = null;

            smtp = new SmtpClient(
                ConfigurationManager.AppSettings["SMTPAddress"],
                Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"])
                );
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["SMTPUser"],
                ConfigurationManager.AppSettings["SMTPPassword"]);
            mm = new MailMessage();
            mm.IsBodyHtml = isHtml;
            mm.From = new MailAddress(this.from, this.displayName);
            mm.Sender = new MailAddress(this.from, this.displayName);
            mm.To.Add(new MailAddress(this.to)); // 1719: Email com formato inválido (vazio)
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SMTPCopy"]) && this.bCopia == true)
            {
                mm.CC.Add(new MailAddress(ConfigurationManager.AppSettings["SMTPCopy"]));
            }
            mm.Subject = this.subject;
            mm.Body = this.doby;
            mm.BodyEncoding = System.Text.Encoding.UTF8;

            smtp.Send(mm);
        }
    }
}
