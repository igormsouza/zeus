using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BHS.ProjetoBaseMvc.Dados;
using BHS.ProjetoBaseMvc.Dados.Repositorio;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Dominio.PesquisaDTO;
using BHS.ProjetoBaseMvc.Negocio.Base;

namespace BHS.ProjetoBaseMvc.Negocio.Gerenciador
{

    public partial class FuncionalidadeGerenciador : BaseGerenciador<TB_FUNCIONALIDADE>
    {
        public TB_FUNCIONALIDADE ConsultarInformacoesFuncionalidade(string action, string controller)
        {
            var consulta = base.Query;

            consulta = consulta.Where(x => x.ACTION == action && x.CONTROLLER == controller);

            return adaptador.FuncionalidadeRepositorio.Consultar(consulta, null, "TB_PERFIL_FUNCIONALIDADE").FirstOrDefault();
        }
    }
}
