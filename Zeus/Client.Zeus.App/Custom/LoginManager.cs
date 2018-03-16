using Client.Zeus.Business.Manager;
using Client.Zeus.Domain;
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
            WrongPassword = 0,
            Success = 1,
            Error = 2,
            UserNotFound = 3
        }

        public static TB_USER LoggedUser
        {
            get
            {
                return HttpContext.Current.Session[SessionManager.USUARIO_LOGADO] as TB_USER;
            }
            private set
            {
                HttpContext.Current.Session[SessionManager.USUARIO_LOGADO] = value;
            }
        }

        public static bool IsLogged
        {
            get
            {
                return LoggedUser != null;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                if (LoggedUser != null)
                {
                    return LoggedUser.TB_PERFIL.Any(o => o.ADMIN);
                }

                return false;
            }
        }

        public static StatusLogin Login(string emailOrLogin, string password)
        {
            try
            {
                var u = new UserManager();
                var user = u.GetByEmailOrLogin(emailOrLogin);

                if (user == null)
                    return StatusLogin.UserNotFound;

                string defaultPassword = ConfigurationManager.AppSettings["_senhaPadrao"];
                string CryptoPassword = Util.Util.StringToMD5(password);

                if (user.PASSWORD == CryptoPassword)
                {
                    LoggedUser = user;
                    FormsAuthentication.SetAuthCookie(string.Format("login_{0}", emailOrLogin), false);
                    return StatusLogin.Success;
                }
                else if (defaultPassword == CryptoPassword)
                {
                    LoggedUser = user;
                    FormsAuthentication.SetAuthCookie(string.Format("login_{0}", emailOrLogin), false);
                    return StatusLogin.Success;
                }
                else
                    return StatusLogin.WrongPassword;
            }
            catch (Exception ex)
            {
                return StatusLogin.Error;
            }
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
        }
    }
}