using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.Base
{
    public class BaseSearch
    {
        public BaseSearch()
        {
            QuantidadePorPagina = 5;
            Page = 1;
            Sort = "ID";
            SortDir = "ASC";
        }

        public BaseSearch(int quantidadePorPagina, int page, string sort, string sortDir)
            : this()
        {
            if (quantidadePorPagina > 0)
                this.QuantidadePorPagina = quantidadePorPagina;

            if (page > 0)
                this.Page = page;

            if (!string.IsNullOrWhiteSpace(sort))
                this.Sort = sort;

            if (!string.IsNullOrWhiteSpace(sortDir))
                this.SortDir = sortDir;
        }

        public int QuantidadePorPagina { get; set; }

        public int Page { get; set; }

        public string Sort { get; set; }

        public string SortDir { get; set; }

        public bool AbrirGrid { get; set; }
    }
}
