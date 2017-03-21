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
        private const string linkMenu = "<li><a href=\"{0}\" target=\"{2}\">{1}</a></li>";



        private const string itemMenuFirstNivel = "<li class=\"dropdown\">" +
                                                         "<a href =\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\">{0}<span class=\"caret\"></span></a>" +
                                                              "<ul class=\"dropdown-menu\">";

        private const string itemMenuSecondNivel = " <li class=\"dropdown-submenu\">" +
                                                          "<a href = \"#\" >{0}</a>" +
                                                              "<ul class=\"dropdown-menu\">";

        private const string itemMenuFirstNivelEnd = "</ul></a></li>";

        private const string itemMenuSecondNivelEnd = "</ul></a></li>";

        public static MvcHtmlString CustomMenu(List<MenuModel> itens)
        {

            if (itens == null)
                return MvcHtmlString.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (var item in itens.FindAll(x => x.IdFather == null))
            {
                BuildMenu(itens, item, sb, 1);
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        private static void BuildMenu(List<MenuModel> list, MenuModel itemMenu, StringBuilder sb, int indice)
        {
            if (!string.IsNullOrEmpty(itemMenu.Url))
            {
                sb.AppendFormat(linkMenu, itemMenu.Url, itemMenu.Title, (itemMenu.NewPage.Value ? "_blank" : "_self"));
            }
            else if (!string.IsNullOrEmpty(itemMenu.Controller))
            {
                if (string.IsNullOrEmpty(itemMenu.Action) || itemMenu.Action.ToLower() == "index")
                    sb.AppendFormat(linkMenu, string.Format("../{0}", itemMenu.Controller), itemMenu.Title, (itemMenu.NewPage.Value ? "_blank" : "_self"));
                else
                    sb.AppendFormat(linkMenu, string.Format("../{0}/{1}", itemMenu.Controller, itemMenu.Action), itemMenu.Title, (itemMenu.NewPage.Value ? "_blank" : "_self"));
            }
            else
            {
                sb.AppendFormat((indice == 1 ? itemMenuFirstNivel : itemMenuSecondNivel), itemMenu.Title);
                var sons = list.FindAll(x => x.IdFather == itemMenu.ID);

                foreach (var item in sons)
                {
                    BuildMenu(list, item, sb, indice + 1);
                }
                sb.AppendFormat((indice == 1 ? itemMenuFirstNivelEnd : itemMenuSecondNivelEnd), itemMenu.Title);
            }
        }
    }
}
