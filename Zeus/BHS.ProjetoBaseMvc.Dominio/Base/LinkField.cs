using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.Base
{
    public class LinkField
    {
        /// <summary>
        /// Criação dos controles e ações do grid
        /// </summary>
        /// <param name="nome">Nome que exibe</param>
        /// <param name="linkControllerGrid">Controller</param>
        /// <param name="linkActionGrid">ACtion</param>
        /// <param name="campoReferencia">Campo que vai buscar o valor</param>
        /// <param name="parametroUrl">Campo que vai exibir na tela ex: ?ID_PRODUTO=</param>
        public LinkField(string nome, string linkControllerGrid, string linkActionGrid, string campoReferencia = "", string parametroUrl = "id")
        {
            Nome = nome;
            LinkControllerGrid = linkControllerGrid;
            LinkActionGrid = linkActionGrid;
            CampoReferencia = campoReferencia;
            ParametroUrl = parametroUrl;
        }

        public string Nome { get; set; }

        public string LinkActionGrid { get; set; }

        public string LinkControllerGrid { get; set; }

        public string PropriedadeReferencia { get; set; }

        public string CampoReferencia { get; set; }

        public string ParametroUrl { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
