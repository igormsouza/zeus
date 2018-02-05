using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Linq.Expressions;
using Client.Zeus.Domain;
using Client.Zeus.Data;
using Client.Zeus.Business.Base;
using Client.Zeus.Data.Repository;

namespace Client.Zeus.Business.Gerenciador
{
    public partial class MenuGerenciador : BaseManager<TB_MENU>
    {
        public IList<TB_MENU> SearchByPerfilId(int idPerfil)
        {
            var query = (from a in base.Query
                         from b in a.TB_PERFIL
                         where b.ID == idPerfil
                         select a);

            return query.ToList();
        }
    }
}
