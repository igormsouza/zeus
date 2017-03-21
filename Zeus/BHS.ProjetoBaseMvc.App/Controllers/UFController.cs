using BHS.ProjetoBaseMvc.App.Custom;
using BHS.ProjetoBaseMvc.App.Models;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Dominio.PesquisaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHS.ProjetoBaseMvc.App.Controllers
{
    [CustomAuthorization]
    public class UFController : CrudControllerBase<TB_UF, PesquisaUF>
    {
        protected override IList<TB_UF> Pesquisar(out int quantidadeItens, PesquisaUF manterFiltros)
        {
            return UFGerenciador.Pesquisar(out quantidadeItens, manterFiltros.Nome, manterFiltros.Sigla, manterFiltros.Id, manterFiltros.Page, manterFiltros.Sort, manterFiltros.SortDir);
        }
    }
}