using BHS.ProjetoBaseMvc.Util.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Util.Menu
{
    public class MenuModel
    {
        public int ID { get; set; }

        [Map("TITULO")]
        public string Title { get; set; }

        [Map("CONTROLLER")]
        public string Controller { get; set; }

        [Map("ACTION")]
        public string Action { get; set; }

        [Map("IMAGEM")]
        public string Image { get; set; }

        [Map("ID_PAI")]
        public Nullable<int> IdFather { get; set; }

        [Map("URL")]
        public string Url { get; set; }

        [Map("IND_ABRIR_NOVA_PAGINA")]
        public Nullable<bool> NewPage { get; set; }

        public List<MenuModel> Childreen { get; set; }
    }
}
