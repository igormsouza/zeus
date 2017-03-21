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
        private string De;
        private string Para;
        private string Assunto;
        private string Body;
        private string Mascara;
        private bool bCopia;
        private string EmailControladoria = string.Empty;

        public Mail(string de, string para, string assunto, bool copia)
        {
            this.De = de;
            this.Para = para;
            this.Mascara = ConfigurationManager.AppSettings["MascaraCarteiro"];
            this.Assunto = assunto;
            this.bCopia = copia;
        }

        public void TextMail(string mensagem)
        {
            this.Body = mensagem;
            SendMail(false);
        }

        
        public void HtmlMail(string nome, string chamada, string titulo, string mensagem, int ID)
        {
            this.Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/content/Email/template.html"));
            this.Body = this.Body.Replace("[URL]", ConfigurationManager.AppSettings["siteURL"]);
            this.Body = this.Body.Replace("[URLTOPO]", ConfigurationManager.AppSettings["siteURL"] + "/Images/logo_60.png");
            this.Body = this.Body.Replace("[ASSUNTO]", this.Assunto);
            this.Body = this.Body.Replace("[NOME]", nome);
            this.Body = this.Body.Replace("[TITULO]", titulo);
            this.Body = this.Body.Replace("[MENSAGEM]", mensagem);
            
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
            mm.From = new MailAddress(this.De, this.Mascara);
            mm.Sender = new MailAddress(this.De, this.Mascara);
            mm.To.Add(new MailAddress(this.Para)); // 1719: Email com formato inválido (vazio)
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CopiaSMTP"]) && this.bCopia == true)
            {
                mm.CC.Add(new MailAddress(ConfigurationManager.AppSettings["CopiaSMTP"]));
            }
            if (!string.IsNullOrEmpty(EmailControladoria))
            {
                mm.CC.Add(new MailAddress(EmailControladoria));
            }
            mm.Subject = this.Assunto;
            mm.Body = this.Body;
            mm.BodyEncoding = System.Text.Encoding.UTF8;

            smtp.Send(mm);
        }
    }
}
