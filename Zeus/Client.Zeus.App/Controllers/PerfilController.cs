using BHS.ProjetoBaseMvc.App.Custom;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHS.ProjetoBaseMvc.App.Controllers
{

    public class PerfilController : CrudControllerBase<TB_PERFIL, BaseSearchCodeDescription<TB_PERFIL>>
    {
        public PerfilController()
        {
            ViewBag.Titulo = "Perfil";
            ViewBag.CamposPesquisa = new[] { "DESCRICAO" };
            ViewBag.CamposGrid = new[] { "ID", "DESCRICAO" };
            //ViewBag.Detalhe = new[] { "Assinatura_Pagseguro_Cobranca", "Assinatura_Pagseguro_Notificacao" };
            //ViewBag.DetalheTitulo = new[] { "Cobranças", "Histórico" };
            ViewBag.Acoes = new[] { "Editar", "Deletar" };
            ViewBag.ExibirNovo = true;

            //IncluirTabelas = "TB_ASSINATURA.TB_CIDADE";
        }
    }
}