using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Linq.Expressions;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Dados;
using BHS.ProjetoBaseMvc.Negocio.Base;
using BHS.ProjetoBaseMvc.Dados.Repositorio;

namespace BHS.ProjetoBaseMvc.Negocio.Gerenciador
{
    public partial class UFGerenciador : BaseGerenciador<TB_UF>
    {
        public IList<TB_UF> Pesquisar(out int countItens, string nome, string sigla, int id, int page, string sort, string sortDir, int quantidadePorPagina = 10)
        {
            IQueryable<TB_UF> consulta = base.Query;

            if (!string.IsNullOrEmpty(nome))
            {
                consulta = consulta.Where(o => o.NOME.ToUpper().Contains(nome.ToUpper()));
            }

            if (!string.IsNullOrEmpty(sigla))
            {
                consulta = consulta.Where(o => o.SIGLA.ToUpper().Contains(sigla.ToUpper()));
            }

            if (id > 0)
            {
                consulta = consulta.Where(o => o.ID> 0);
            }

            return RepositorioBase.ListarPaginado(out countItens, consulta, null, page, quantidadePorPagina, sort, sortDir);
        }
    }
}