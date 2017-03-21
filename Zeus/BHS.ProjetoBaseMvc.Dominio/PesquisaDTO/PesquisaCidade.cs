using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.PesquisaDTO
{
    public class PesquisaCidade : PesquisaBaseCodigoDescricao<TB_CIDADE>
    {
        public PesquisaCidade() { }

        public PesquisaCidade(IList<TB_CIDADE> itens, int totalItens)
            : base(itens, totalItens)
        {

        }

        public string Nome { get; set; }

        public int UF { get; set; }
    }
}
