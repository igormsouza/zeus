using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Negocio;
using BHS.ProjetoBaseMvc.Util;
using System.Configuration;
using BHS.ProjetoBaseMvc.App.CrudService;
using BHS.ProjetoBaseMvc.Dominio.Base;
using System.Linq.Expressions;
using BHS.ProjetoBaseMvc.Negocio.Base;
using System.Reflection;
using BHS.ProjetoBaseMvc.Negocio.Gerenciador;
using BHS.ProjetoBaseMvc.Dominio.DTO;
using BHS.ProjetoBaseMvc.Util.Map;
using BHS.ProjetoBaseMvc.Util.Menu;
using BHS.ProjetoBaseMvc.App.Custom;
using Microsoft.AspNet.Identity;

namespace BHS.ProjetoBaseMvc.App.Controllers
{
    public partial class BaseController : Controller
    {
        public BaseController()
        {
            //Verificar se usuario estao logado, se sim buscar os menus e por no cache.
            ViewBag.Menus = (LoginManager.IsUsuarioLogado) ? TransformHelper.TransformList<MenuModel>(new MenuGerenciador().ConsultarMenuPorPerfil(1)).ToList() : null;
        }

        private CrudContratoClient servicoCrudContrato;
        public CrudContratoClient ServicoCrudContrato
        {
            get
            {
                if (servicoCrudContrato == null)
                    servicoCrudContrato = new CrudContratoClient();
                return servicoCrudContrato;
            }
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

        public TB_USUARIO UsuarioSessao
        {
            get
            {
                return LoginManager.UsuarioLogado;
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
            ModelState.ExibeErros(errosValidacao, ex);
            ExibeMensagem.Show((Controller)this, errosValidacao);
        }

        protected virtual bool VerificarPermissaoPesquisar()
        {
            return true;
        }

        protected virtual bool VerificarPermissaoInserir()
        {
            return true;
        }

        protected virtual bool VerificarPermissaoEditar()
        {
            return true;
        }

        protected virtual bool VerificarPermissaoExcluir()
        {
            return true;
        }

        protected ActionResult RedirectHome()
        {
            return RedirectToAction("Index", "Home");
        }

        private IList<Redirecionar> redirecionar;
        public IList<Redirecionar> Redirecionar
        {
            get
            {
                redirecionar = redirecionar ?? new List<Redirecionar>();

                if (Session["Redirecionar"] != null && Session["Redirecionar"] is IList<Redirecionar>)
                {
                    redirecionar = Session["Redirecionar"] as IList<Redirecionar>;
                }

                return redirecionar;
            }
            set
            {
                Session["Redirecionar"] = value;
            }
        }

        public void AdicionaRedirecionar(Redirecionar redirecionarItem, bool removeItensMesmoController, string limparController = "")
        {
            var lista = new List<Redirecionar>();

            if (redirecionarItem != null)
            {
                if (removeItensMesmoController)
                    lista.AddRange(Redirecionar.Where(o => o.ControllerOrigem != redirecionarItem.ControllerOrigem).ToList());
                else
                    lista.AddRange(Redirecionar);

                lista.Add(redirecionarItem);
            }
            else if (!string.IsNullOrWhiteSpace(limparController))
            {
                limparController = limparController.Replace('/', ' ').Trim();
                if (removeItensMesmoController)
                    lista.AddRange(Redirecionar.Where(o => o.ControllerOrigem != limparController).ToList());
                else
                    lista.AddRange(Redirecionar);
            }
            else
            {
                lista.AddRange(Redirecionar);
            }

            Redirecionar = lista;
        }

        public Redirecionar BuscaRedirecionarAtual(string controller = "")
        {
            Redirecionar retorno = null;

            if (Redirecionar != null)
            {
                if (string.IsNullOrWhiteSpace(controller))
                {
                    var controllerAtual = Request.Url.Segments[1].Replace('/', ' ').Trim();
                    retorno = Redirecionar.FirstOrDefault(o => o.ControllerAcionado == controllerAtual);
                }
                else
                {
                    retorno = Redirecionar.FirstOrDefault(o => o.ControllerAcionado == controller);
                }
            }

            return retorno;
        }

        #region Utilitarios

        public string ObterNomePropriedade<T>(Expression<Func<T, object>> expressao) where T : DominioBase, new()
        {
            if (expressao != null && expressao.Body is MemberExpression)
                return (expressao.Body as MemberExpression).Member.Name;
            else if (expressao != null && expressao.Body is UnaryExpression)
                return ((expressao.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
            else
                return null;
        }

        public SelectList SelectList<T>(
            Expression<Func<T, object>> CampoDescricao = null,
            Expression<Func<T, object>> CampoID = null)
            where T : DominioBase, new()
        {
            var NomeID = ObterNomePropriedade<T>(CampoID) ?? "ID";
            var NomeDescricao = ObterNomePropriedade<T>(CampoDescricao) ?? "DESCRICAO";

            var propriedades = typeof(BaseController).GetProperties();
            BaseGerenciador<T> gerenciador = null;
            foreach (PropertyInfo i in propriedades)
            {
                var propriedade = i.GetValue(this);
                if (propriedade is BaseGerenciador<T>)
                {
                    gerenciador = propriedade as BaseGerenciador<T>;
                    break;
                }
            }

            if (gerenciador == null)
                gerenciador = Activator.CreateInstance<BaseGerenciador<T>>();

            return new SelectList(gerenciador.Listar(), NomeID, NomeDescricao);
        }

        #endregion
    }
}
