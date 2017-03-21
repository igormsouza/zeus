using BHS.ProjetoBaseMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BHS.ProjetoBaseMvc.Util.Menu
{
    public static class MenuExtensions
    {
        private const string LinkMenu = "<li><a href=\"{0}\" target=\"{2}\">{1}</a></li>";



        private const string ItemMenuNivel1Inicio = "<li class=\"dropdown\">" +
                                                         "<a href =\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\">{0}<span class=\"caret\"></span></a>" +
                                                              "<ul class=\"dropdown-menu\">";

        private const string ItemMenuNivel2Inicio = " <li class=\"dropdown-submenu\">" +
                                                          "<a href = \"#\" >{0}</a>" +
                                                              "<ul class=\"dropdown-menu\">";

        private const string ItemMenuNivel1Fim = "</ul></a></li>";

        private const string ItemMenuNivel2Fim = "</ul></a></li>";

        public static MvcHtmlString BhsMenu(List<MenuModel> itens)
        {

            if (itens == null)
                return MvcHtmlString.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (var item in itens.FindAll(x => x.IDPai == null))
            {
                ConstroiMenu(itens, item, sb, 1);
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        private static void ConstroiMenu(List<MenuModel> lista, MenuModel item, StringBuilder sb, int indice)
        {
            if (!string.IsNullOrEmpty(item.Url))
            {
                sb.AppendFormat(LinkMenu, item.Url, item.Titulo, (item.IndAbrirNovaPagina.Value ? "_blank" : "_self"));
            }
            else if (!string.IsNullOrEmpty(item.Controller))
            {
                if (string.IsNullOrEmpty(item.Action) || item.Action.ToLower() == "index")
                    sb.AppendFormat(LinkMenu, string.Format("../{0}", item.Controller), item.Titulo, (item.IndAbrirNovaPagina.Value ? "_blank" : "_self"));
                else
                    sb.AppendFormat(LinkMenu, string.Format("../{0}/{1}", item.Controller, item.Action), item.Titulo, (item.IndAbrirNovaPagina.Value ? "_blank" : "_self"));
            }
            else
            {
                sb.AppendFormat((indice == 1 ? ItemMenuNivel1Inicio : ItemMenuNivel2Inicio), item.Titulo);
                var filhos = lista.FindAll(x => x.IDPai == item.ID);

                foreach (var filho in lista.FindAll(x => x.IDPai == item.ID))
                {
                    ConstroiMenu(lista, filho, sb, indice + 1);
                }
                sb.AppendFormat((indice == 1 ? ItemMenuNivel1Fim : ItemMenuNivel2Fim), item.Titulo);
            }
        }
    }
}
