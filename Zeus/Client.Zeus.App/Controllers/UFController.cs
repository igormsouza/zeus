using Client.Zeus.App.Custom;
using Client.Zeus.App.Models;
using Client.Zeus.Dominio;
using Client.Zeus.Dominio.PesquisaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Zeus.App.Controllers
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