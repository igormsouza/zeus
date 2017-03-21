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
using BHS.ProjetoBaseMvc.App.Models;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.App.Models.Base;
using BHS.ProjetoBaseMvc.Dominio.Base;

namespace BHS.ProjetoBaseMvc.App.Controllers
{
    [AllowAnonymous]
    public class TesteCidadeAutomaticaController : CrudControllerBase<TB_CIDADE, PesquisaBaseCodigoDescricao<TB_CIDADE>>
    {
        public TesteCidadeAutomaticaController()
        {
            var campos = new[] { 
                new Campo("NOME") 
            }.ToList<Campo>();

            var camposPesquisa = new[] { 
                "NOME"
            }.Select(o => new Campo(o)).ToList();

            var camposGrid = new[] { 
                "NOME"
            };

            ModelAutomaticoTela.Titulo = "Cidade";
            ModelAutomaticoTela.CamposPesquisa = camposPesquisa;
            ModelAutomaticoTela.CamposGrid = camposGrid;
            ModelAutomaticoTela.Campos = campos;
        }
    }
}
