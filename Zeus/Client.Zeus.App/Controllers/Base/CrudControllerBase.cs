using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Client.Zeus.Domain.Base;
using Client.Zeus.Business.Base;
using Client.Zeus.Util;
using Client.Zeus.App.Models;
using Client.Zeus.App.Models.Base;
using System.Reflection;

namespace Client.Zeus.App.Controllers
{
    public abstract class CrudControllerBase<T, U> : BaseController
        where T : BaseDomain, new()
        where U : BaseSearchCodeDescription<T>
    {
        public CrudControllerBase()
        {
            ViewBag.Modelo = AutomaticModel;
        }

        public bool CarregaEmpresa { get; set; }

        private AutomaticModel automaticModel;
        public AutomaticModel AutomaticModel
        {
            get
            {
                if (automaticModel == null)
                    automaticModel = new AutomaticModel();
                return automaticModel;
            }
            set { automaticModel = value; }
        }

        private BaseManager<T> baseManager;
        protected BaseManager<T> BaseManager
        {
            get
            {
                if (baseManager == null)
                {
                    var properties = typeof(BaseController).GetProperties();
                    var Tproperties = new List<BaseManager<T>>();
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                    foreach (PropertyInfo i in properties)
                    {
                        var item = i.GetValue(this);
                        if (item is BaseManager<T>)
                        {
                            Tproperties.Add((BaseManager<T>)item);
                        }
                    }

                    foreach (BaseManager<T> Tprop in Tproperties)
                    {
                        var propName = Tprop.GetType().Name;
                        if (propName.Substring(0, propName.IndexOf("Manager")).Equals(controllerName))
                        {
                            baseManager = Tprop;
                            break;
                        }
                    }
                }

                return baseManager;
            }
        }

        public string ContextSerach
        {
            get
            {
                return typeof(U).Name;
            }
        }

        #region Search

        protected virtual IList<T> Search(out int countItems, U maintenFilters)
        {
            return BaseManager.Search(out countItems, maintenFilters);
        }

        public virtual void PrePesquisar()
        {
            LoadHidden(null);
        }

        [Authorize]
        public virtual ActionResult Index(U maintenFilters = null)
        {
            try
            {
                CleanOldRedirect();
                if (!CheckSearchPermission())
                    return RedirectToAction("Autorizacao", "Erro");

                PrePesquisar();

                if (FlagPesquisaAutomatica || base.IsPostBack)
                {
                    int quantidadeItens;

                    if (FlagPreservaValoresAcaoVoltar && TempData["AcaoVoltar"] != null && Convert.ToBoolean(TempData["AcaoVoltar"]) && Session[ContextSerach] != null && Session[ContextSerach] is U)
                    {
                        maintenFilters = Session[ContextSerach] as U;
                    }
                    else if (maintenFilters == null)
                    {
                        maintenFilters = Activator.CreateInstance<U>();
                    }

                    // Para casos de paginação
                    if (Request.HttpMethod == "GET" && Session[ContextSerach] != null && Session[ContextSerach] is U)
                    {
                        // Guarda novas condições de paginação / ordenação
                        var sort = maintenFilters.Sort;
                        var sortDir = maintenFilters.SortDir;
                        var page = maintenFilters.Page;

                        // Obtém filtro mantido na sessão
                        maintenFilters = Session[ContextSerach] as U;

                        // Restaura paginação ordenação
                        maintenFilters.Sort = sort;
                        maintenFilters.SortDir = sortDir;
                        maintenFilters.Page = page;
                    }

                    var itens = Search(out quantidadeItens, maintenFilters);
                    maintenFilters.Itens = itens;
                    maintenFilters.TotalItens = quantidadeItens;
                    maintenFilters.OpenGrid = true;
                    Session[ContextSerach] = maintenFilters;

                    PostSearch();
                }
                else
                {
                    maintenFilters = Activator.CreateInstance<U>();
                }

                return View("Index", maintenFilters);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errosValidacao = new Dictionary<string, string>();
                return DefaultErrosTreatment(errosValidacao, ex);
            }
        }

        private void CleanOldRedirect()
        {
            if (base.Redirect.Count > 0)
            {
                base.AddRedirect(null, true, Request.Url.Segments[1]);
            }
        }

        public virtual void PostSearch()
        {

        }

        [Authorize]
        public virtual ActionResult CleanSearch()
        {
            if (!CheckSearchPermission())
                return RedirectToAction("Autorizacao", "Erro");

            Session[ContextSerach] = null;
            return RedirectToAction("Index");
        }

        #endregion

        #region Inserir

        public virtual void PreInsertGet(ref T entity)
        {
            LoadHidden(entity);
        }

        [Authorize]
        public virtual ActionResult Insert()
        {
            try
            {
                if (!CheckCreatePermission())
                    return RedirectToAction("Autorizacao", "Erro");

                CleanOldRedirect();
                Session[ContextSerach] = null;

                if (FlagPreservaValoresAcaoVoltar)
                    TempData["AcaoVoltar"] = null;

                T entity = new T();
                PreInsertGet(ref entity);
                return View(entity);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> erros = new Dictionary<string, string>();
                return DefaultErrosTreatment(erros, ex);
            }
        }

        public virtual void PreInsertPost(ref T id)
        {

        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult Insert(T entity)
        {
            Dictionary<string, string> erros = new Dictionary<string, string>();

            try
            {
                if (!CheckCreatePermission())
                    return RedirectToAction("Autorizacao", "Erro");

                PreInsertPost(ref entity);
                if (ValidaModelStateInserir(entity))
                {
                    bool retorno = AcaoInserir(entity, out erros);
                    PosInsertPost(entity, retorno);
                }
                else
                {
                    erros = new Dictionary<string, string>();
                    erros.Add("modelStatusInvalido", Messages.ModelStateInvalido);
                }

                ShowMessageAfterInsert(erros);

                return ReturnCreate(entity, erros);

            }
            catch (Exception ex)
            {
                return DefaultErrosTreatment(erros, ex);
            }
        }

        private bool ValidaModelStateInserir(T entity)
        {
            return ModelState.IsValid;
        }

        private bool AcaoInserir(T entity, out Dictionary<string, string> erros)
        {
            bool result = BaseManager.Create(entity, out erros);
            return result;
        }

        public virtual void ShowMessageAfterInsert(Dictionary<string, string> erros)
        {
            ModelState.ShowErros(erros);
            ShowMessage.Show((Controller)this, erros);
        }

        public virtual ActionResult ReturnCreate(T entity, Dictionary<string, string> erros)
        {
            if (ModelState.IsValid && erros.Count == 0)
                return RedirectToAction("Index");
            else
                return View(entity);
        }

        public virtual void PosInsertPost(T id, bool result)
        {

        }

        #endregion

        #region Edit

        [Authorize]
        public ActionResult Edit(int id)
        {
            try
            {
                if (!CheckEditPermission())
                    return RedirectToAction("Autorizacao", "Erro");

                CleanOldRedirect();

                T entity = BaseManager.GetById(id);
                if (entity == null)
                    return RedirectToAction("Error", "Error");
                else
                    PosEditGet(ref entity);

                return View(entity);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> erros = new Dictionary<string, string>();
                return DefaultErrosTreatment(erros, ex);
            }
        }

        public virtual void PosEditGet(ref T entity)
        {
            LoadHidden(entity);
        }

        public virtual void PreEditPost(ref T id)
        {

        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult Edit(T entity)
        {
            Dictionary<string, string> erros = new Dictionary<string, string>();

            try
            {
                if (!CheckEditPermission())
                    return RedirectToAction("Autorizacao", "Erro");

                PreEditPost(ref entity);

                if (GetValidModelState(entity))
                {
                    bool result = ActionEdit(entity, out erros);
                    PosEditarPost(entity, result);
                }
                else
                {
                    erros = new Dictionary<string, string>();
                    erros.Add("modelStatusInvalido", Messages.ModelStateInvalido);
                }

                ShowMessageAfterEdit(erros);

                return ReturnEditar(entity, erros);
            }
            catch (Exception ex)
            {
                return DefaultErrosTreatment(erros, ex);
            }
        }

        private bool GetValidModelState(T entity)
        {
            return ModelState.IsValid;
        }

        private bool ActionEdit(T entity, out Dictionary<string, string> erros)
        {
            bool retorno = BaseManager.Edit(entity, out erros);
            return retorno;
        }

        public virtual void ShowMessageAfterEdit(Dictionary<string, string> erros)
        {
            ModelState.ShowErros(erros);
            ShowMessage.Show((Controller)this, erros);
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

        #region Delete

        public virtual void PreDelete(int id)
        {

        }

        [Authorize]
        public virtual ActionResult Delete(int id)
        {
            Dictionary<string, string> erros = new Dictionary<string, string>();

            try
            {
                if (!CheckDeletePermission())
                    return RedirectToAction("Autorizacao", "Erro");

                PreDelete(id);
                bool result = ActionDelete(id, out erros);
                PostDelete(id, result);
                ShowMessageAfterDelete(erros);

                if (FlagPreservaValoresAcaoVoltar)
                    TempData["AcaoVoltar"] = true;

                return ReturnDelete(id);
            }
            catch (Exception ex)
            {
                return DefaultErrosTreatment(erros, ex);
            }
        }

        public virtual bool ActionDelete(int id, out Dictionary<string, string> erros)
        {
            return BaseManager.Delete(id, out erros);
        }

        public virtual void ShowMessageAfterDelete(Dictionary<string, string> erros)
        {
            ModelState.ShowErros(erros);
            ShowMessage.Show((Controller)this, erros);
        }

        public virtual ActionResult ReturnDelete(int id)
        {
            return RedirectToAction("Index");
        }

        public virtual void PostDelete(int id, bool result)
        {

        }

        #endregion

        public virtual void LoadHidden(T entity)
        {
            AddControl(entity);
        }

        public virtual void AddControl(T entity)
        {

        }

        public SelectList SelectList<TModel>(
            Func<TModel, object> fieldDescription,
            Func<TModel, object> fieldId,
            string selected = null,
            Func<TModel, bool> condiction = null)
            where TModel : BaseDomain, new()
        {
            BaseManager<TModel> listManager = null;
            var properties = typeof(BaseController).GetProperties();
            foreach (PropertyInfo i in properties)
            {
                var propriedade = i.GetValue(this);
                if (propriedade is BaseManager<TModel>)
                {
                    listManager = propriedade as BaseManager<TModel>;
                    break;
                }
            }

            if (listManager != null)
            {
                var items = (condiction == null) ? listManager.List() : listManager.List(condiction).ToList();

                var aux = items.Select(a => new SelectListItem
                {
                    Value = fieldId(a).ToString(),
                    Text = fieldDescription(a).ToString(),
                    Selected = fieldId(a).ToString() == selected
                }).ToList();

                return new SelectList(aux, "Value", "Text", selected);
            }
            else
            {
                var aux = new List<SelectListItem>();
                return new SelectList(aux, "Value", "Text");
            }
        }


        public SelectList SelectList<TModel>(
            Func<TModel, object> fieldDescription,
            Func<TModel, object> fieldId,
            string selected = null,
            IList<TModel> list = null)
            where TModel : BaseDomain, new()
        {
            if (list != null)
            {
                var aux = list.Select(a => new SelectListItem
                {
                    Value = fieldId(a).ToString(),
                    Text = fieldDescription(a).ToString(),
                    Selected = fieldId(a).ToString() == selected
                }).ToList();

                return new SelectList(aux, "Value", "Text", selected);
            }
            else
            {
                var aux = new List<SelectListItem>();
                return new SelectList(aux, "Value", "Text");
            }
        }

        private ActionResult DefaultErrosTreatment(Dictionary<string, string> errosValidacao, Exception ex)
        {
            base.ErroCatchPadrao(errosValidacao, ex);
            return View("Erro");
        }

    }
}

