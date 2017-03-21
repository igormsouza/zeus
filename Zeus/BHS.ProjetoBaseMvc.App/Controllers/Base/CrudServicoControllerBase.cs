﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BHS.ProjetoBaseMvc.Dominio.Base;
using BHS.ProjetoBaseMvc.Negocio.Base;
using BHS.ProjetoBaseMvc.Util;
using BHS.ProjetoBaseMvc.App.Models;
using BHS.ProjetoBaseMvc.App.Models.Base;
using System.Reflection;
using BHS.ProjetoBaseMvc.App.CrudService;
using System.Linq.Expressions;

namespace BHS.ProjetoBaseMvc.App.Controllers
{
    public abstract class CrudServicoControllerBase<T, U> : BaseController
        where T : DominioBase, new()
        where U : PesquisaBaseCodigoDescricao<T>
    {
        private BaseGerenciador<T> retorno;
        protected BaseGerenciador<T> GerenciadorBase
        {
            get
            {
                if (retorno == null)
                {
                    var propriedades = typeof(BaseController).GetProperties();
                    //Gerenciadores que utilizam o domínio do tipo T
                    var Tproperties = new List<BaseGerenciador<T>>();
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                    foreach (PropertyInfo i in propriedades)
                    {
                        var propriedade = i.GetValue(this);
                        if (propriedade is BaseGerenciador<T>)
                        {
                            Tproperties.Add((BaseGerenciador<T>)propriedade);
                        }
                    }

                    foreach (BaseGerenciador<T> Tprop in Tproperties)
                    {
                        var propName = Tprop.GetType().Name;
                        if (propName.Substring(0, propName.IndexOf("Gerenciador")).Equals(controllerName))
                        {
                            retorno = Tprop;
                            break;
                        }
                    }
                }

                return retorno;
            }
        }

        public string PesquisaContexto
        {
            get
            {
                return typeof(U).Name;
            }
        }

        #region Pesquisar

        protected virtual IList<T> Pesquisar(out int quantidadeItens, U manterFiltros)
        {
            return GerenciadorBase.Pesquisar(out quantidadeItens, manterFiltros);
        }

        public virtual void PrePesquisar()
        {

        }

        [Authorize]
        public virtual ActionResult Index(U manterFiltros = null)
        {
            try
            {
                if (!VerificarPermissaoPesquisar())
                    return RedirectToAction("Autorizacao", "Erro");

                PrePesquisar();

                if (FlagPesquisaAutomatica || base.IsPostBack)
                {
                    int quantidadeItens;

                    if (FlagPreservaValoresAcaoVoltar && TempData["AcaoVoltar"] != null && Convert.ToBoolean(TempData["AcaoVoltar"]) && Session[PesquisaContexto] != null && Session[PesquisaContexto] is U)
                    {
                        manterFiltros = Session[PesquisaContexto] as U;
                    }
                    else if (manterFiltros == null)
                    {
                        manterFiltros = Activator.CreateInstance<U>();
                    }

                    // Para casos de paginação
                    if (Request.HttpMethod == "GET" && Session[PesquisaContexto] != null && Session[PesquisaContexto] is U)
                    {
                        // Guarda novas condições de paginação / ordenação
                        var sort = manterFiltros.Sort;
                        var sortDir = manterFiltros.SortDir;
                        var page = manterFiltros.Page;

                        // Obtém filtro mantido na sessão
                        manterFiltros = Session[PesquisaContexto] as U;

                        // Restaura paginação ordenação
                        manterFiltros.Sort = sort;
                        manterFiltros.SortDir = sortDir;
                        manterFiltros.Page = page;
                    }

                    var itens = Pesquisar(out quantidadeItens, manterFiltros);
                    manterFiltros.Itens = itens;
                    manterFiltros.TotalItens = quantidadeItens;
                    manterFiltros.AbrirGrid = true;
                    Session[PesquisaContexto] = manterFiltros;

                    PosPesquisar();
                }
                else
                {
                    manterFiltros = Activator.CreateInstance<U>();
                }

                return View("Index", manterFiltros);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errosValidacao = new Dictionary<string, string>();
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        public virtual void PosPesquisar()
        {

        }

        [Authorize]
        public virtual ActionResult LimparPesquisa()
        {
            if (!VerificarPermissaoPesquisar())
                return RedirectToAction("Autorizacao", "Erro");

            PrePesquisar();

            U manterFiltros = Activator.CreateInstance<U>();
            Session[PesquisaContexto] = null;
            return View("Index", manterFiltros);
        }

        #endregion

        #region Inserir

        public virtual void PreInserirGet(ref T dominio)
        {

        }

        [Authorize]
        public virtual ActionResult Inserir()
        {
            try
            {
                if (!VerificarPermissaoInserir())
                    return RedirectToAction("Autorizacao", "Erro");

                Session[PesquisaContexto] = null;

                if (FlagPreservaValoresAcaoVoltar)
                    TempData["AcaoVoltar"] = null;

                T dominio = new T();
                PreInserirGet(ref dominio);
                return View(dominio);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errosValidacao = new Dictionary<string, string>();
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        public virtual void PreInserirPost(ref T id)
        {

        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult Inserir(T dominio)
        {
            Dictionary<string, string> errosValidacao = new Dictionary<string, string>();

            try
            {
                if (!VerificarPermissaoInserir())
                    return RedirectToAction("Autorizacao", "Erro");

                PreInserirPost(ref dominio);
                if (ValidaModelStateInserir(dominio))
                {
                    bool retorno = AcaoInserir(dominio, out errosValidacao);
                    PosInserirPost(dominio, retorno);
                }
                else
                {
                    errosValidacao = new Dictionary<string, string>();
                    errosValidacao.Add("modelStatusInvalido", Mensagens.ModelStateInvalido);
                }

                ExibeMensagemAposInserir(errosValidacao);

                return ReturnCreate(dominio, errosValidacao);

            }
            catch (Exception ex)
            {
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        private bool ValidaModelStateInserir(T dominio)
        {
            return ModelState.IsValid;
        }

        private bool AcaoInserir(T dominio, out Dictionary<string, string> errosValidacao)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            errosValidacao = new Dictionary<string, string>();

            Type type = typeof(CrudContratoClient);
            MethodInfo info = type.GetMethod("Criar" + Util.Util.GeraNomeClasse(typeof(T).Name));

            object[] parametro = { dominio };
            object retornoMetodo = info.Invoke(ServicoCrudContrato, parametro);

            if (retornoMetodo is RetornoComMensagem)
            {
                retorno = retornoMetodo as RetornoComMensagem;
                if (!retorno.Retorno)
                    errosValidacao = retorno.ErrosValidacao;
            }

            return retorno.Retorno;
        }

        public virtual void ExibeMensagemAposInserir(Dictionary<string, string> errosValidacao)
        {
            ModelState.ExibeErros(errosValidacao);
            ExibeMensagem.Show((Controller)this, errosValidacao);
        }

        public virtual ActionResult ReturnCreate(T dominio, Dictionary<string, string> erros)
        {
            if (ModelState.IsValid && erros.Count == 0)
                return RedirectToAction("Index");
            else
                return View(dominio);
        }

        public virtual void PosInserirPost(T idDominio, bool retorno)
        {

        }

        #endregion

        #region Editar

        [Authorize]
        public ActionResult Editar(int id)
        {
            try
            {
                if (!VerificarPermissaoEditar())
                    return RedirectToAction("Autorizacao", "Erro");

                T dominio = GerenciadorBase.BuscarPorId(id);
                if (dominio == null)
                    return RedirectToAction("Error", "Error");
                else
                    PosEditarGet(ref dominio);

                return View(dominio);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errosValidacao = new Dictionary<string, string>();
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        public virtual void PosEditarGet(ref T dominio)
        {

        }

        public virtual void PreEditarPost(ref T id)
        {

        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult Editar(T dominio)
        {
            Dictionary<string, string> errosValidacao = new Dictionary<string, string>();

            try
            {
                if (!VerificarPermissaoEditar())
                    return RedirectToAction("Autorizacao", "Erro");

                PreEditarPost(ref dominio);

                if (ValidaModelStateEditar(dominio))
                {
                    bool retorno = AcaoEditar(dominio, out errosValidacao);
                    PosEditarPost(dominio, retorno);
                }
                else
                {
                    errosValidacao = new Dictionary<string, string>();
                    errosValidacao.Add("modelStatusInvalido", Mensagens.ModelStateInvalido);
                }

                ExibeMensagemAposEditar(errosValidacao);

                return ReturnEditar(dominio, errosValidacao);
            }
            catch (Exception ex)
            {
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        private bool ValidaModelStateEditar(T dominio)
        {
            return ModelState.IsValid;
        }

        private bool AcaoEditar(T dominio, out Dictionary<string, string> errosValidacao)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            errosValidacao = new Dictionary<string, string>();

            Type type = typeof(CrudContratoClient);
            MethodInfo info = type.GetMethod("Editar" + Util.Util.GeraNomeClasse(typeof(T).Name));

            object[] parametro = { dominio };
            object retornoMetodo = info.Invoke(ServicoCrudContrato, parametro);

            if (retornoMetodo is RetornoComMensagem)
            {
                retorno = retornoMetodo as RetornoComMensagem;
                if (!retorno.Retorno)
                    errosValidacao = retorno.ErrosValidacao;
            }

            return retorno.Retorno;
        }

        public virtual void ExibeMensagemAposEditar(Dictionary<string, string> errosValidacao)
        {
            ModelState.ExibeErros(errosValidacao);
            ExibeMensagem.Show((Controller)this, errosValidacao);
        }

        public virtual ActionResult ReturnEditar(T dominio, Dictionary<string, string> erros)
        {
            if (ModelState.IsValid && erros.Count == 0)
            {
                if (FlagPreservaValoresAcaoVoltar)
                    TempData["AcaoVoltar"] = true;

                return RedirectToAction("Index");
            }
            else
                return View(dominio);
        }

        public virtual void PosEditarPost(T dominio, bool retorno)
        {

        }

        #endregion

        #region Deletar

        public virtual void PreDeletar(int id)
        {

        }

        [Authorize]
        public virtual ActionResult Deletar(int id)
        {
            Dictionary<string, string> errosValidacao = new Dictionary<string, string>();

            try
            {
                if (!VerificarPermissaoExcluir())
                    return RedirectToAction("Autorizacao", "Erro");

                PreDeletar(id);
                bool retorno = AcaoDeletar(id, out errosValidacao);
                PosDeletar(id, retorno);
                ExibeMensagemAposDeletar(errosValidacao);

                if (FlagPreservaValoresAcaoVoltar)
                    TempData["AcaoVoltar"] = true;

                return ReturnDeletar(id);
            }
            catch (Exception ex)
            {
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        public virtual bool AcaoDeletar(int id, out Dictionary<string, string> errosValidacao)
        {
            RetornoComMensagem retorno = new RetornoComMensagem();
            errosValidacao = new Dictionary<string, string>();

            Type type = typeof(CrudContratoClient);
            MethodInfo info = type.GetMethod("Excluir" + Util.Util.GeraNomeClasse(typeof(T).Name));

            object[] parametro = { id };
            object retornoMetodo = info.Invoke(ServicoCrudContrato, parametro);

            if (retornoMetodo is RetornoComMensagem)
            {
                retorno = retornoMetodo as RetornoComMensagem;
                if (!retorno.Retorno)
                    errosValidacao = retorno.ErrosValidacao;
            }

            return retorno.Retorno;
        }

        public virtual void ExibeMensagemAposDeletar(Dictionary<string, string> errosValidacao)
        {
            ModelState.ExibeErros(errosValidacao);
            ExibeMensagem.Show((Controller)this, errosValidacao);
        }

        public virtual ActionResult ReturnDeletar(int id)
        {
            return RedirectToAction("Index");
        }

        public virtual void PosDeletar(int idDominio, bool retorno)
        {

        }

        #endregion

        private ActionResult ErroCatchPadraoRedirecionando(Dictionary<string, string> errosValidacao, Exception ex)
        {
            base.ErroCatchPadrao(errosValidacao, ex);
            return Index();
        }
    }
}

