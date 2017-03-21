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
        public string Titulo { get; set; }

        [Map("CONTROLLER")]
        public string Controller { get; set; }

        [Map("ACTION")]
        public string Action { get; set; }

        [Map("IMAGEM")]
        public string Imagem { get; set; }

        [Map("ID_PAI")]
        public Nullable<int> IDPai { get; set; }

        [Map("URL")]
        public string Url { get; set; }

        [Map("IND_ABRIR_NOVA_PAGINA")]
        public Nullable<bool> IndAbrirNovaPagina { get; set; }

        public List<MenuModel> Filhos { get; set; }
    }
}
