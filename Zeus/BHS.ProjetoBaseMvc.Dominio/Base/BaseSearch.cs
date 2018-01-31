using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.Base
{
    public class BaseSearch
    {
        public BaseSearch()
        {
            CountPerPage = 5;
            Page = 1;
            Sort = "ID";
            SortDir = "ASC";
        }

        public BaseSearch(int countPerPage, int page, string sort, string sortDir)
            : this()
        {
            if (countPerPage > 0)
                this.CountPerPage = countPerPage;

            if (page > 0)
                this.Page = page;

            if (!string.IsNullOrWhiteSpace(sort))
                this.Sort = sort;

            if (!string.IsNullOrWhiteSpace(sortDir))
                this.SortDir = sortDir;
        }

        public int CountPerPage { get; set; }

        public int Page { get; set; }

        public string Sort { get; set; }

        public string SortDir { get; set; }

        public bool OpenGrid { get; set; }
    }
}
