using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Client.Zeus.Data;
using Client.Zeus.Data.Repository;
using Client.Zeus.Domain;
using Client.Zeus.Domain.PesquisaDTO;
using Client.Zeus.Business.Base;

namespace Client.Zeus.Business.Gerenciador
{

    public partial class FuncionalidadeGerenciador : BaseManager<TB_FUNCIONALIDADE>
    {
        public TB_FUNCIONALIDADE ConsultarInformacoesFuncionalidade(string action, string controller)
        {
            var consulta = base.Query;

            consulta = consulta.Where(x => x.ACTION == action && x.CONTROLLER == controller);

            return adapter.FuncionalidadeRepositorio.Consultar(consulta, null, "TB_PERFIL_FUNCIONALIDADE").FirstOrDefault();
        }
    }
}
