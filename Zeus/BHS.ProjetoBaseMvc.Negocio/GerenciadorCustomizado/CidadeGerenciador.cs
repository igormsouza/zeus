using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BHS.ProjetoBaseMvc.Dados;
using BHS.ProjetoBaseMvc.Dados.Repositorio;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Negocio.Base;

namespace BHS.ProjetoBaseMvc.Negocio.Gerenciador
{
   public partial class CidadeGerenciador : BaseGerenciador<TB_CIDADE>
    {
        public IList<TB_CIDADE> Pesquisar(out int countItens, string nome, int idUF, int id, int page, string sort, string sortDir, int quantidadePorPagina = 10)
        {
            IQueryable<TB_CIDADE> consulta = base.Query;

            if (!string.IsNullOrEmpty(nome))
            {
                consulta = consulta.Where(o => o.NOME.ToUpper().Contains(nome.ToUpper()));
            }

            if (idUF > 0)
            {
                consulta = consulta.Where(o => o.ID_UF == idUF);
            }

            if (id > 0)
            {
                consulta = consulta.Where(o => o.ID > 0);
            }

            return RepositorioBase.ListarPaginado(out countItens, consulta, null, page, quantidadePorPagina, sort, sortDir);
        }
    }
}
