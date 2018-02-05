using Client.Zeus.Domain;
using Client.Zeus.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Business.Gerenciador
{
    public partial class UsuarioGerenciador : BaseManager<TB_USUARIO>
    {
        public TB_USUARIO BuscarPorEmailOuLogin(string emailOuLogin)
        {
            var includesConsulta = "TB_PERFIL, TB_PERFIL.TB_MENU, TB_PERFIL.TB_FUNCIONALIDADE";

            var consulta = base.Query.Where(o => o.EMAIL == emailOuLogin || o.LOGIN == emailOuLogin);

            var usuario = adapter.UsuarioRepositorio.Consultar(consulta, null, includesConsulta).FirstOrDefault();

            return usuario;
        }

        public override IList<TB_USUARIO> PagingList(out int totalItems, IQueryable<TB_USUARIO> filter = null, string includedProperty = "", int currentPage = 1, int countPerPage = 10, string order = "", string orderDirection = "")
        {
            filter = filter.Where(o => o.NOME != "admin");
            return base.PagingList(out totalItems, filter, includedProperty, currentPage, countPerPage, order, orderDirection);
        }
    }
}
