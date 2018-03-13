//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Threading;
//using System.Linq.Expressions;
//using Client.Zeus.Domain;
//using Client.Zeus.Data;
//using Client.Zeus.Business.Base;
//using Client.Zeus.Data.Repository;

//namespace Client.Zeus.Business.Gerenciador
//{
//    public partial class UFGerenciador : BaseManager<TB_UF>
//    {
//        public IList<TB_UF> Pesquisar(out int countItens, string nome, string sigla, int id, int page, string sort, string sortDir, int quantidadePorPagina = 10)
//        {
//            IQueryable<TB_UF> consulta = base.Query;

//            if (!string.IsNullOrEmpty(nome))
//            {
//                consulta = consulta.Where(o => o.NOME.ToUpper().Contains(nome.ToUpper()));
//            }

//            if (!string.IsNullOrEmpty(sigla))
//            {
//                consulta = consulta.Where(o => o.SIGLA.ToUpper().Contains(sigla.ToUpper()));
//            }

//            if (id > 0)
//            {
//                consulta = consulta.Where(o => o.ID> 0);
//            }

//            return BaseRepository.PagingList(out countItens, consulta, null, page, quantidadePorPagina, sort, sortDir);
//        }
//    }
//}