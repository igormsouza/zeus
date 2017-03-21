using System;
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
using BHS.ProjetoBaseMvc.Util.Map;
using System.Reflection;

namespace BHS.ProjetoBaseMvc.App.Controllers
{
    public abstract class CrudDTOControllerBase<T, U, D> : BaseController
        where T : DominioBase, new()
        where U : PesquisaBaseCodigoDescricao<T>
        where D : DominioBaseDTO, new()
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

        public abstract T ConvertDTOEmDominioExternoInserir(D dto);

        public abstract void ConvertDTOEmDominioExternoEditar(ref T dominio, D dto);

        public virtual T ConvertDTOEmDominio(D dto)
        {
            T retorno = dto.Transform<T>();
            return retorno;
        }

        public virtual D ConvertDominioEmDTOExterno(T dominio)
        {
            D retorno = null;

            if (dominio == null)
            {
                retorno = Activator.CreateInstance<D>();
            }
            else
            {
                retorno = ConvertDominioEmDTO(dominio);
            }

            return retorno;
        }

        public virtual D ConvertDominioEmDTO(T dominio)
        {
            D retorno = dominio.Transform<D>();
            return retorno;
        }

        public virtual void PosConvertDominioEmDTOExterno(ref D dto, T dominio)
        {

        }

        #region Pesquisar

        protected virtual IList<T> Pesquisar(out int quantidadeItens, U manterFiltros)
        {
            return GerenciadorBase.Pesquisar(out quantidadeItens, manterFiltros);
        }

        public virtual void PrePesquisar()
        {
            CarregaOcultos(null);
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


                    if (FlagPreservaValoresAcaoVoltar && ViewData["AcaoVoltar"] != null && Convert.ToBoolean(ViewData["AcaoVoltar"]) && Session[PesquisaContexto] != null && Session[PesquisaContexto] is U)
                    {
                        manterFiltros = Session[PesquisaContexto] as U;
                    }
                    else if (manterFiltros == null)
                    {
                        manterFiltros = Activator.CreateInstance<U>();
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

        public virtual void PreInserirGet(ref D dto)
        {
            CarregaOcultos(dto);
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
                    ViewData["AcaoVoltar"] = null;

                D dominioDTO = new D();
                PreInserirGet(ref dominioDTO);
                return View(dominioDTO);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errosValidacao = new Dictionary<string, string>();
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        public virtual void PreInserirPost(D dto, ref T id)
        {

        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult Inserir(D dto)
        {
            Dictionary<string, string> errosValidacao = new Dictionary<string, string>();

            try
            {
                if (!VerificarPermissaoInserir())
                    return RedirectToAction("Autorizacao", "Erro");

                T dominio = ConvertDTOEmDominioExternoInserir(dto);

                PreInserirPost(dto, ref dominio);
                if (ValidaModelStateInserir(dto))
                {
                    bool retorno = AcaoInserir(dominio, out errosValidacao);
                    PosInserirPost(dto, dominio, retorno);
                }
                else
                {
                    errosValidacao = new Dictionary<string, string>();
                    errosValidacao.Add("modelStatusInvalido", Mensagens.ModelStateInvalido);
                }

                ExibeMensagemAposInserir(errosValidacao);

                return ReturnCreate(dto, dominio, errosValidacao);

            }
            catch (Exception ex)
            {
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        private bool ValidaModelStateInserir(D dto)
        {
            return ModelState.IsValid;
        }

        private bool AcaoInserir(T dominio, out Dictionary<string, string> errosValidacao)
        {
            bool retorno = GerenciadorBase.Criar(dominio, out errosValidacao);
            return retorno;
        }

        public virtual void ExibeMensagemAposInserir(Dictionary<string, string> errosValidacao)
        {
            ModelState.ExibeErros(errosValidacao);
            ExibeMensagem.Show((Controller)this, errosValidacao);
        }

        public virtual ActionResult ReturnCreate(D dto, T dominio, Dictionary<string, string> erros)
        {
            if (ModelState.IsValid && erros.Count == 0)
                return RedirectToAction("Index");
            else
                return View(dominio);
        }

        public virtual void PosInserirPost(D dto, T idDominio, bool retorno)
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
                D dto = ConvertDominioEmDTOExterno(dominio);

                if (dominio == null)
                    return RedirectToAction("Error", "Error");
                else
                    PosEditarGet(dto);

                return View(dto);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errosValidacao = new Dictionary<string, string>();
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        public virtual void PosEditarGet(D dto)
        {
            CarregaOcultos(dto);
        }

        public virtual void PreEditarPost(ref D dto)
        {

        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult Editar(D dto)
        {
            Dictionary<string, string> errosValidacao = new Dictionary<string, string>();

            try
            {
                if (!VerificarPermissaoEditar())
                    return RedirectToAction("Autorizacao", "Erro");

                PreEditarPost(ref dto);

                if (ValidaModelStateEditar(dto))
                {
                    T dominio = GerenciadorBase.BuscarPorId(dto.ID);
                    ConvertDTOEmDominioExternoEditar(ref dominio, dto);

                    bool retorno = AcaoEditar(dominio, out errosValidacao);
                    PosEditarPost(dto, dominio, retorno);
                }
                else
                {
                    errosValidacao = new Dictionary<string, string>();
                    errosValidacao.Add("modelStatusInvalido", Mensagens.ModelStateInvalido);
                }

                ExibeMensagemAposEditar(errosValidacao);

                return ReturnEditar(dto, errosValidacao);
            }
            catch (Exception ex)
            {
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        private bool ValidaModelStateEditar(D dto)
        {
            return ModelState.IsValid;
        }

        private bool AcaoEditar(T dominio, out Dictionary<string, string> errosValidacao)
        {
            bool retorno = GerenciadorBase.Editar(dominio, out errosValidacao);
            return retorno;
        }

        public virtual void ExibeMensagemAposEditar(Dictionary<string, string> errosValidacao)
        {
            ModelState.ExibeErros(errosValidacao);
            ExibeMensagem.Show((Controller)this, errosValidacao);
        }

        public virtual ActionResult ReturnEditar(D dto, Dictionary<string, string> erros)
        {
            if (ModelState.IsValid && erros.Count == 0)
            {
                if (FlagPreservaValoresAcaoVoltar)
                    ViewData["AcaoVoltar"] = true;

                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public virtual void PosEditarPost(D dto, T dominio, bool retorno)
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
                    ViewData["AcaoVoltar"] = true;

                return ReturnDeletar(id);
            }
            catch (Exception ex)
            {
                return ErroCatchPadraoRedirecionando(errosValidacao, ex);
            }
        }

        public virtual bool AcaoDeletar(int id, out Dictionary<string, string> errosValidacao)
        {
            return GerenciadorBase.Excluir(id, out errosValidacao);
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

        public virtual void CarregaOcultos(D dto)
        {
            AdicionaControles(dto);
        }

        public virtual void AdicionaControles(D dto)
        {

        }

        public SelectList SelectList<TModel>(
            Func<TModel, object> CampoDescricao,
            Func<TModel, object> CampoID,
            string selecionado = null,
            Func<TModel, bool> Condicoes = null)
            where TModel : DominioBase, new()
        {
            BaseGerenciador<TModel> gerenciadorLista = null;
            var propriedades = typeof(BaseController).GetProperties();
            foreach (PropertyInfo i in propriedades)
            {
                var propriedade = i.GetValue(this);
                if (propriedade is BaseGerenciador<TModel>)
                {
                    gerenciadorLista = propriedade as BaseGerenciador<TModel>;
                    break;
                }
            }

            if (gerenciadorLista != null)
            {
                var itens = (Condicoes == null) ? gerenciadorLista.Listar() : gerenciadorLista.Listar(Condicoes).ToList();

                var aux = itens.Select(a => new SelectListItem
                {
                    Value = CampoID(a).ToString(),
                    Text = CampoDescricao(a).ToString(),
                    Selected = CampoID(a).ToString() == selecionado
                }).ToList();

                return new SelectList(aux, "Value", "Text", selecionado);
            }
            else
            {
                var aux = new List<SelectListItem>();
                return new SelectList(aux, "Value", "Text");
            }
        }

        public SelectList SelectList<TModel>(
            Func<TModel, object> CampoDescricao,
            Func<TModel, object> CampoID,
            string selecionado = null,
            IList<TModel> listaPronta = null)
            where TModel : DominioBase, new()
        {
            if (listaPronta != null)
            {
                var aux = listaPronta.Select(a => new SelectListItem
                {
                    Value = CampoID(a).ToString(),
                    Text = CampoDescricao(a).ToString(),
                    Selected = CampoID(a).ToString() == selecionado
                }).ToList();

                return new SelectList(aux, "Value", "Text", selecionado);
            }
            else
            {
                var aux = new List<SelectListItem>();
                return new SelectList(aux, "Value", "Text");
            }
        }

        private ActionResult ErroCatchPadraoRedirecionando(Dictionary<string, string> errosValidacao, Exception ex)
        {
            base.ErroCatchPadrao(errosValidacao, ex);
            return Index();
        }
    }
}
