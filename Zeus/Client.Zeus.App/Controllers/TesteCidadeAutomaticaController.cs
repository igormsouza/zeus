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

namespace Client.Zeus.App.Controllers
{
    [AllowAnonymous]
    public class TesteCidadeAutomaticaController : CrudControllerBase<TB_CIDADE, BaseSearchCodeDescription<TB_CIDADE>>
    {
        public TesteCidadeAutomaticaController()
        {
            var campos = new[] { 
                new Field("NOME") 
            }.ToList<Field>();

            var camposPesquisa = new[] { 
                "NOME"
            }.Select(o => new Field(o)).ToList();

            var camposGrid = new[] { 
                "NOME"
            };

            ModelAutomaticoTela.Title = "Cidade";
            ModelAutomaticoTela.SearchFields = camposPesquisa;
            ModelAutomaticoTela.GridField = camposGrid;
            ModelAutomaticoTela.Fields = campos;
        }
    }
}
