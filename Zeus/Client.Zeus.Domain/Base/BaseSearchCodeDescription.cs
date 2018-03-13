using Client.Zeus.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.Base
{
    public class BaseSearchCodeDescription<T> : BaseSearch where T : BaseDomain, new()
    {
        public BaseSearchCodeDescription()
            : base()
        {

        }

        public BaseSearchCodeDescription(int quantidadePorpagina, int page, string sort, string sortDir)
            : base(quantidadePorpagina, page, sort, sortDir)
        {

        }

        public BaseSearchCodeDescription(IList<T> items, int totalItems)
            : base()
        {
            this.Itens = items;
            this.TotalItens = totalItems;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        public string Description { get; set; }

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
