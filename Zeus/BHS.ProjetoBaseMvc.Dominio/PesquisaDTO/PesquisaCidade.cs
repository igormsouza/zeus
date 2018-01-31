using Client.Zeus.Domain;
using Client.Zeus.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.PesquisaDTO
{
    public class PesquisaCidade : BaseSearchCodeDescription<TB_CIDADE>
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
