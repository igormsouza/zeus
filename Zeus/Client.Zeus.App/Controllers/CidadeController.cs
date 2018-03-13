using Client.Zeus.App.CrudService;
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
    [AllowAnonymous]
    public class CidadeController : CrudServicoControllerBase<TB_CIDADE, PesquisaCidade>
    {
        public override void PrePesquisar()
        {
            CarregarUF();
        }

        public override void PreInserirGet(ref TB_CIDADE dominio)
        {
            CarregarUF(dominio.ID_UF);
        }

        public override void PosEditarGet(ref TB_CIDADE dominio)
        {
            CarregarUF(dominio.ID_UF);
        }

        public override void PosEditarPost(TB_CIDADE dominio, bool retorno)
        {
            CarregarUF(dominio.ID_UF);
        }

        protected override IList<TB_CIDADE> Pesquisar(out int quantidadeItens, PesquisaCidade manterFiltros)
        {
            RetornoListaTotalOfTB_CIDADE_Suar5u8q retorno = ServicoCrudContrato.ListarPaginadoCidade(manterFiltros.Page, manterFiltros.Sort, manterFiltros.SortDir, 0);
            quantidadeItens = retorno.TotalItens;
            return retorno.Lista;
            //return CidadeGerenciador.Pesquisar(out quantidadeItens, manterFiltros.Nome, manterFiltros.UF, manterFiltros.Id, manterFiltros.Page, manterFiltros.Sort, manterFiltros.SortDir);
        }

        private void CarregarUF(int idUF = 0)
        {
            IList<TB_UF> itens = UFGerenciador.Listar();
            
            if (idUF == 0)
                ViewBag.DdlUF = new SelectList(itens, "ID", "NOME");
            else
                ViewBag.DdlUF = new SelectList(itens, "ID", "NOME", idUF);
        }
    }
}