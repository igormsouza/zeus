using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace BHS.ProjetoBaseMvc.Util
{
    public class Mail
    {
        private string from;
        private string to;
        private string subject;
        private string doby;
        private string mask;
        private bool bCopia;
        private string EmailControladoria = string.Empty;

        public Mail(string de, string para, string assunto, bool copia)
        {
            this.from = de;
            this.to = para;
            this.mask = ConfigurationManager.AppSettings["MascaraCarteiro"];
            this.subject = assunto;
            this.bCopia = copia;
        }

        public void TextMail(string mensagem)
        {
            this.doby = mensagem;
            SendMail(false);
        }

        
        public void HtmlMail(string nome, string chamada, string titulo, string mensagem, int ID)
        {
            this.doby = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/content/Email/template.html"));
            this.doby = this.doby.Replace("[URL]", ConfigurationManager.AppSettings["siteURL"]);
            this.doby = this.doby.Replace("[URLTOPO]", ConfigurationManager.AppSettings["siteURL"] + "/Images/logo_60.png");
            this.doby = this.doby.Replace("[ASSUNTO]", this.subject);
            this.doby = this.doby.Replace("[NOME]", nome);
            this.doby = this.doby.Replace("[TITULO]", titulo);
            this.doby = this.doby.Replace("[MENSAGEM]", mensagem);
            
            SendMail(true);
        }
     
        public void SendMail(bool isHtml)
        {
            MailMessage mm = null;
            SmtpClient smtp = null;

            smtp = new SmtpClient(
                ConfigurationManager.AppSettings["EnderecoSMTP"],
                Convert.ToInt32(ConfigurationManager.AppSettings["PortaSMTP"])
                );
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["UsuarioSMTP"],
                ConfigurationManager.AppSettings["SenhaSMTP"]);
            mm = new MailMessage();
            mm.IsBodyHtml = isHtml;
            mm.From = new MailAddress(this.from, this.mask);
            mm.Sender = new MailAddress(this.from, this.mask);
            mm.To.Add(new MailAddress(this.to)); // 1719: Email com formato inválido (vazio)
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CopiaSMTP"]) && this.bCopia == true)
            {
                mm.CC.Add(new MailAddress(ConfigurationManager.AppSettings["CopiaSMTP"]));
            }
            if (!string.IsNullOrEmpty(EmailControladoria))
            {
                mm.CC.Add(new MailAddress(EmailControladoria));
            }
            mm.Subject = this.subject;
            mm.Body = this.doby;
            mm.BodyEncoding = System.Text.Encoding.UTF8;

            smtp.Send(mm);
        }
    }
}
