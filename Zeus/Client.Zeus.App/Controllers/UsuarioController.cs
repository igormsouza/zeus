#region Info
//===============================================================================
// Microsoft VS Integrated Guidance Package
//
// This file was generated automatically by a tool.
//
//===============================================================================
// BHS v1.0
//==============================================================================
#endregion Info

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.Zeus.App.Models;
using Client.Zeus.Dominio;
using Client.Zeus.App.Models.Base;
using Client.Zeus.Dominio.Base;
using Client.Zeus.App.Custom;
using Client.Zeus.Negocio.Gerenciador;

namespace Client.Zeus.App.Controllers
{
    [CustomAuthorization]
    public class UsuarioController : CrudControllerBase<TB_USUARIO, BaseSearchCodeDescription<TB_USUARIO>>
    {
        public UsuarioController()
        {
            var campos = new[] {
                new Field("NOME"),
                new Field("LOGIN"),
                new Field("EMAIL"),
                new Field("CPF")
            }.ToList<Field>();

            var camposPesquisa = new[] {
                "NOME", "LOGIN", "EMAIL"
            }.Select(o => new Field(o)).ToList();

            var camposGrid = new[] {
                "ID", "NOME", "LOGIN", "EMAIL"
            };

            ModelAutomaticoTela.Title = "Usu�rio";
            ModelAutomaticoTela.SearchFields = camposPesquisa;
            ModelAutomaticoTela.GridField = camposGrid;
            ModelAutomaticoTela.Fields = campos;
        }

        public ActionResult Permissao()
        {
            ViewBag.Funcionalidades = new FuncionalidadeGerenciador().Listar();
            return View();
        }
    }
}
