using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Negocio.Gerenciador
{
    public partial class UsuarioGerenciador : BaseGerenciador<TB_USUARIO>
    {
        public TB_USUARIO BuscarPorEmailOuLogin(string emailOuLogin)
        {
            var includesConsulta = "TB_PERFIL, TB_PERFIL.TB_MENU, TB_PERFIL.TB_FUNCIONALIDADE";

            var consulta = base.Query.Where(o => o.EMAIL == emailOuLogin || o.LOGIN == emailOuLogin);

            var usuario = adaptador.UsuarioRepositorio.Consultar(consulta, null, includesConsulta).FirstOrDefault();

            return usuario;
        }

        public override IList<TB_USUARIO> ListarPaginado(out int totalItens, IQueryable<TB_USUARIO> filtro = null, string propriedadesAIncluir = "", int paginaAtual = 1, int quantidadePorPagina = 10, string ordenacao = "", string direcaoOrdenacao = "")
        {
            filtro = filtro.Where(o => o.NOME != "admin");
            return base.ListarPaginado(out totalItens, filtro, propriedadesAIncluir, paginaAtual, quantidadePorPagina, ordenacao, direcaoOrdenacao);
        }
    }
}
