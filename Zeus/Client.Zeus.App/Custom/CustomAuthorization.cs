using Client.Zeus.Negocio.Gerenciador;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Zeus.App.Custom
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        public CustomAuthorization()
        {
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                base.OnAuthorization(filterContext);

                if (filterContext.Result is HttpUnauthorizedResult)
                    filterContext.Result = new RedirectResult("~/Erro/Autorizacao");

            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Erro/Autorizacao");
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!LoginManager.IsLogged)
                return false;

            var action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            var controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            if (LoginManager.IsAdmin)
            {
                return true;
            }
            else
            {
                var temPermissao = (from a in LoginManager.LoggedUser.TB_PERFIL
                                   from b in a.TB_FUNCIONALIDADE
                                   where b.ACTION == action && b.CONTROLLER == controller
                                   select b).Count();
                return temPermissao > 0;
            }
        }
    }
}