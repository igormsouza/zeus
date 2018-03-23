using Client.Zeus.App.Custom;
using Client.Zeus.Domain;
using Client.Zeus.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Zeus.App.Controllers
{

    public class PerfilController : CrudControllerBase<TB_PERFIL, BaseSearchCodeDescription<TB_PERFIL>>
    {
        public PerfilController()
        {
            ViewBag.Titulo = "Perfil";
            ViewBag.CamposPesquisa = new[] { "NAME" };
            ViewBag.CamposGrid = new[] { "ID", "NAME" };
            ViewBag.Acoes = new[] { "Edit", "Delete" };
            ViewBag.ExibirNovo = true;
        }
    }
}