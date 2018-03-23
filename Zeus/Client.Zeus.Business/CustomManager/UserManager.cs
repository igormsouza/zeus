using Client.Zeus.Domain;
using Client.Zeus.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Business.Manager
{
    public partial class UserManager : BaseManager<TB_USER>
    {
        public TB_USER GetByEmailOrLogin(string emailOrLogin)
        {
            var includedProperty = "TB_PERFIL, TB_PERFIL.TB_MENU, TB_PERFIL.TB_FUNCTIONALITY";

            var query = base.Query.Where(o => o.EMAIL == emailOrLogin || o.LOGIN == emailOrLogin);

            var result = adapter.UserRepository.Search(query, null, includedProperty).FirstOrDefault();

            return result;
        }

        public override IList<TB_USER> PagingList(out int totalItems, IQueryable<TB_USER> filter = null, string includedProperty = "", int currentPage = 1, int countPerPage = 10, string order = "", string orderDirection = "")
        {
            filter = filter.Where(o => o.NAME != "admin");
            return base.PagingList(out totalItems, filter, includedProperty, currentPage, countPerPage, order, orderDirection);
        }
    }
}
