using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Client.Zeus.Domain;
using Client.Zeus.Business;
using Client.Zeus.Util;
using System.Configuration;
using Client.Zeus.Domain.Base;
using System.Linq.Expressions;
using Client.Zeus.Business.Base;
using System.Reflection;
using Client.Zeus.Business.Manager;
using Client.Zeus.Domain.DTO;
using Client.Zeus.Util.Map;
using Client.Zeus.Util.Menu;
using Client.Zeus.App.Custom;
using Microsoft.AspNet.Identity;

namespace Client.Zeus.App.Controllers
{
    public partial class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.Menus = (LoginManager.IsLogged) ? TransformHelper.TransformList<MenuModel>(new MenuManager().SearchByPerfilId(1)).ToList() : null;
        }

        private bool? flagPreservaValoresAcaoVoltar;
        public bool FlagPreservaValoresAcaoVoltar
        {
            get
            {
                if (!flagPreservaValoresAcaoVoltar.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["_preservaValoresAcaoVoltar"]))
                    {
                        bool valor;
                        bool.TryParse(ConfigurationManager.AppSettings["_preservaValoresAcaoVoltar"], out valor);
                        flagPreservaValoresAcaoVoltar = valor;
                    }
                    else
                        flagPreservaValoresAcaoVoltar = false;
                }

                return flagPreservaValoresAcaoVoltar.Value;
            }
        }

        public TB_USER UsuarioSessao
        {
            get
            {
                return LoginManager.LoggedUser;
            }
        }

        private bool? flagPesquisaAutomatica;
        public bool FlagPesquisaAutomatica
        {
            get
            {
                if (!flagPreservaValoresAcaoVoltar.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["_pesquisaAutomatica"]))
                    {
                        bool valor;
                        bool.TryParse(ConfigurationManager.AppSettings["_pesquisaAutomatica"], out valor);
                        flagPesquisaAutomatica = valor;
                    }
                    else
                        flagPesquisaAutomatica = false;
                }

                if (flagPesquisaAutomatica.HasValue)
                    return flagPesquisaAutomatica.Value;
                else
                    return false;
            }
        }

        public bool IsPostBack
        {
            get
            {

                return Request.Form.Count > 0;
            }
        }

        public virtual void ErroCatchPadrao(Dictionary<string, string> errosValidacao, Exception ex)
        {
            ModelState.ShowErros(errosValidacao, ex);
            ShowMessage.Show((Controller)this, errosValidacao);
        }

        protected virtual bool CheckSearchPermission()
        {
            return true;
        }

        protected virtual bool CheckCreatePermission()
        {
            return true;
        }

        protected virtual bool CheckEditPermission()
        {
            return true;
        }

        protected virtual bool CheckDeletePermission()
        {
            return true;
        }

        protected ActionResult RedirectHome()
        {
            return RedirectToAction("Index", "Home");
        }

        private IList<Redirect> redirect;
        public IList<Redirect> Redirect
        {
            get
            {
                redirect = redirect ?? new List<Redirect>();

                if (Session["Redirecionar"] != null && Session["Redirecionar"] is IList<Redirect>)
                {
                    redirect = Session["Redirecionar"] as IList<Redirect>;
                }

                return redirect;
            }
            set
            {
                Session["Redirecionar"] = value;
            }
        }

        public void AddRedirect(Redirect redirect, bool removeItemsSameController, string cleanController = "")
        {
            var result = new List<Redirect>();

            if (redirect != null)
            {
                if (removeItemsSameController)
                    result.AddRange(Redirect.Where(o => o.OrginController != redirect.OrginController).ToList());
                else
                    result.AddRange(Redirect);

                result.Add(redirect);
            }
            else if (!string.IsNullOrWhiteSpace(cleanController))
            {
                cleanController = cleanController.Replace('/', ' ').Trim();
                if (removeItemsSameController)
                    result.AddRange(Redirect.Where(o => o.OrginController != cleanController).ToList());
                else
                    result.AddRange(Redirect);
            }
            else
            {
                result.AddRange(Redirect);
            }

            Redirect = result;
        }

        public Redirect GetCurrentRedirect(string controller = "")
        {
            Redirect result = null;

            if (Redirect != null)
            {
                if (string.IsNullOrWhiteSpace(controller))
                {
                    var controllerAtual = Request.Url.Segments[1].Replace('/', ' ').Trim();
                    result = Redirect.FirstOrDefault(o => o.DestinyController == controllerAtual);
                }
                else
                {
                    result = Redirect.FirstOrDefault(o => o.DestinyController == controller);
                }
            }

            return result;
        }

        #region Utilitarios

        public string GetPropertyName<T>(Expression<Func<T, object>> expression) where T : BaseDomain, new()
        {
            if (expression != null && expression.Body is MemberExpression)
                return (expression.Body as MemberExpression).Member.Name;
            else if (expression != null && expression.Body is UnaryExpression)
                return ((expression.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
            else
                return null;
        }

        public SelectList SelectList<T>(
            Expression<Func<T, object>> fieldDescription = null,
            Expression<Func<T, object>> fieldId = null)
            where T : BaseDomain, new()
        {
            var nameID = GetPropertyName<T>(fieldId) ?? "ID";
            var nameDescricao = GetPropertyName<T>(fieldDescription) ?? "DESCRIPTION";

            var properties = typeof(BaseController).GetProperties();
            BaseManager<T> manager = null;
            foreach (PropertyInfo i in properties)
            {
                var propriedade = i.GetValue(this);
                if (propriedade is BaseManager<T>)
                {
                    manager = propriedade as BaseManager<T>;
                    break;
                }
            }

            if (manager == null)
                manager = Activator.CreateInstance<BaseManager<T>>();

            return new SelectList(manager.List(), nameID, nameDescricao);
        }

        #endregion
    }
}
