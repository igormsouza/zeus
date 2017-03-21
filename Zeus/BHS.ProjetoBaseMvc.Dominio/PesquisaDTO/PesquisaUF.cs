using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.PesquisaDTO
{
    public class PesquisaUF : PesquisaBaseCodigoDescricao<TB_UF>
    {
        public PesquisaUF() { }

        public PesquisaUF(IList<TB_UF> itens, int totalItens)
            : base(itens, totalItens)
        {

        }

        public string Nome { get; set; }

        public string Sigla { get; set; }
    }
}
