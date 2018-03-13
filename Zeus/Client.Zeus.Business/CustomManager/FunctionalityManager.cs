using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Client.Zeus.Data;
using Client.Zeus.Data.Repository;
using Client.Zeus.Domain;
using Client.Zeus.Business.Base;

namespace Client.Zeus.Business.Gerenciador
{

    public partial class FunctionalityManager : BaseManager<TB_FUNCTIONALITY>
    {
        public TB_FUNCTIONALITY ConsultarInformacoesFuncionalidade(string action, string controller)
        {
            var query = base.Query;

            query = query.Where(x => x.ACTION == action && x.CONTROLLER == controller);

            return adapter.FunctionalityRepository.Search(query, null, "TB_PERFIL_FUNCTIONALITY").FirstOrDefault();
        }
    }
}
