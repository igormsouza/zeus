using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.Base
{
    public class CampoGrid
    {
        public CampoGrid(string nome)
            : this(nome, nome)
        {

        }

        public CampoGrid(string nome, string tituloColuna, bool ativo = true)
        {
            Nome = nome;
            TituloColuna = tituloColuna;
            Ativo = ativo;
        }

        public string Nome { get; set; }

        public string TituloColuna { get; set; }

        public bool Ativo { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Nome, TituloColuna);
        }
    }
}
