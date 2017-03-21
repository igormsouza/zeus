using BHS.ProjetoBaseMvc.Dominio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.Base
{
    public class PesquisaBaseCodigoDescricao<T> : PesquisaBase where T : DominioBase, new()
    {
        public PesquisaBaseCodigoDescricao()
            : base()
        {

        }

        public PesquisaBaseCodigoDescricao(int quantidadePorpagina, int page, string sort, string sortDir)
            : base(quantidadePorpagina, page, sort, sortDir)
        {

        }

        public PesquisaBaseCodigoDescricao(IList<T> itens, int totalItens)
            : base()
        {
            this.Itens = itens;
            this.TotalItens = totalItens;
        }

        [DisplayName("Código")]
        public int Id { get; set; }

        public string Descricao { get; set; }

        private T query;
        public T Query
        {
            get
            {
                if (query == null)
                    query = Activator.CreateInstance<T>();
                return query;
            }
            set { query = value; }
        }

        private IList<T> itens;
        public IList<T> Itens
        {
            get
            {
                if (itens == null)
                    itens = new List<T>();
                return itens;
            }

            set { itens = value; }
        }

        public int TotalItens { get; set; }
    }
}
