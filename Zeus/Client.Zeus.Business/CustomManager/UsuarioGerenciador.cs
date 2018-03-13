using Client.Zeus.Domain;
using Client.Zeus.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Business.Gerenciador
{
    public partial class UsuarioGerenciador : BaseManager<TB_USER>
    {
        public TB_USER BuscarPorEmailOuLogin(string emailOuLogin)
        {
            var includesConsulta = "TB_PERFIL, TB_PERFIL.TB_MENU, TB_PERFIL.TB_FUNCIONALIDADE";

            var query = base.Query.Where(o => o.EMAIL == emailOuLogin || o.LOGIN == emailOuLogin);

            var result = adapter.UserRepository.Search(query, null, includesConsulta).FirstOrDefault();

            return result;
        }

        public override IList<TB_USER> PagingList(out int totalItems, IQueryable<TB_USER> filter = null, string includedProperty = "", int currentPage = 1, int countPerPage = 10, string order = "", string orderDirection = "")
        {
            filter = filter.Where(o => o.NAME != "admin");
            return base.PagingList(out totalItems, filter, includedProperty, currentPage, countPerPage, order, orderDirection);
        }
    }
}
