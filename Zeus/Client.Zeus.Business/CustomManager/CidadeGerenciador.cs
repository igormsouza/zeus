//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using Client.Zeus.Data;
//using Client.Zeus.Data.Repository;
//using Client.Zeus.Domain;
//using Client.Zeus.Business.Base;

//namespace Client.Zeus.Business.Gerenciador
//{
//   public partial class CidadeGerenciador : BaseManager<TB_CIDADE>
//    {
//        public IList<TB_CIDADE> Pesquisar(out int countItens, string nome, int idUF, int id, int page, string sort, string sortDir, int quantidadePorPagina = 10)
//        {
//            IQueryable<TB_CIDADE> consulta = base.Query;

//            if (!string.IsNullOrEmpty(nome))
//            {
//                consulta = consulta.Where(o => o.NOME.ToUpper().Contains(nome.ToUpper()));
//            }

//            if (idUF > 0)
//            {
//                consulta = consulta.Where(o => o.ID_UF == idUF);
//            }

//            if (id > 0)
//            {
//                consulta = consulta.Where(o => o.ID > 0);
//            }

//            return BaseRepository.PagingList(out countItens, consulta, null, page, quantidadePorPagina, sort, sortDir);
//        }
//    }
//}
