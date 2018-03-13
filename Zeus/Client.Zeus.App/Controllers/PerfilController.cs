using Client.Zeus.App.Custom;
using Client.Zeus.Dominio;
using Client.Zeus.Dominio.Base;
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