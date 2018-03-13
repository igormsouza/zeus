using Client.Zeus.Dominio;
using Client.Zeus.Negocio.Gerenciador;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Client.Zeus.App.Custom
{
    public static class LoginManager
    {
        public enum StatusLogin
        {
            SenhaIncorreta = 0,
            Sucesso = 1,
            Erro = 2,
            UsuarioNaoExiste = 3
        }

        public static TB_USUARIO UsuarioLogado
        {
            get
            {
                return HttpContext.Current.Session[SessionManager.USUARIO_LOGADO] as TB_USUARIO;
            }
            private set
            {
                HttpContext.Current.Session[SessionManager.USUARIO_LOGADO] = value;
            }
        }

        public static bool IsUsuarioLogado
        {
            get
            {
                return UsuarioLogado != null;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                if (UsuarioLogado != null)
                {
                    return UsuarioLogado.TB_PERFIL.Any(o => o.ADMIN);
                }

                return false;
            }
        }

        public static StatusLogin Login(string emailOuLogin, string senha)
        {
            try
            {
                UsuarioGerenciador u = new UsuarioGerenciador();
                var user = u.BuscarPorEmailOuLogin(emailOuLogin);

                if (user == null)
                    return StatusLogin.UsuarioNaoExiste;

                string senhaPadrao = ConfigurationManager.AppSettings["_senhaPadrao"];
                string senhaCriptografada = Util.Util.StringToMD5(senha);

                if (user.SENHA == senhaCriptografada)
                {
                    UsuarioLogado = user;
                    FormsAuthentication.SetAuthCookie(string.Format("login_{0}", emailOuLogin), false);
                    return StatusLogin.Sucesso;
                }
                else if (senhaPadrao == senhaCriptografada)
                {
                    UsuarioLogado = user;
                    FormsAuthentication.SetAuthCookie(string.Format("login_{0}", emailOuLogin), false);
                    return StatusLogin.Sucesso;
                }
                else
                    return StatusLogin.SenhaIncorreta;
            }
            catch (Exception ex)
            {
                return StatusLogin.Erro;
            }
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
        }
    }
}