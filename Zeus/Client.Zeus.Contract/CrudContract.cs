using Client.Zeus.Contract.Entity;
using Client.Zeus.Contract.Interface.Base;
using Client.Zeus.Domain.Base;
using Client.Zeus.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Contract
{
    public class CrudContract<T> : BaseContract, IBaseContract<T> where T : BaseDomain, new()
    {
        private BaseManager<T> manager;
        protected BaseManager<T> BaseManager
        {
            get
            {
                if (manager == null)
                {
                    var propriedades = typeof(BaseContract).GetProperties();
                    foreach (PropertyInfo i in propriedades)
                    {
                        var propriedade = i.GetValue(this);
                        if (propriedade is BaseManager<T>)
                        {
                            manager = propriedade as BaseManager<T>;
                            break;
                        }
                    }
                }

                return manager;
            }
        }

        public IList<T> List()
        {
            var result = BaseManager.List();
            return result;
        }

        public ReturnTotalList<T> PagingList(int paginaAtual, string ordenacao, string direcaoOrdenacao, int quantidadePorPagina)
        {
            var result = new ReturnTotalList<T>();
            int totalItems;
            result.List = BaseManager.PagingList(out totalItems, paginaAtual, ordenacao, direcaoOrdenacao, quantidadePorPagina);
            result.TotalItems = totalItems;
            return result;
        }

        public T GetById(int id)
        {
            T result = BaseManager.GetById(id);
            return result;
        }

        public ReturnMessage Create(T objetoNovo)
        {
            var result = new ReturnMessage();
            Dictionary<string, string> erros;
            result.Result = BaseManager.Create(objetoNovo, out erros);
            result.Erros = erros;
            return result;
        }

        public ReturnMessage Edit(T objetoEditado)
        {
            var retorno = new ReturnMessage();
            Dictionary<string, string> erros;
            retorno.Result = BaseManager.Edit(objetoEditado, out erros);
            retorno.Erros = erros;
            return retorno;
        }

        public ReturnMessage Delete(int id)
        {
            var result = new ReturnMessage();
            Dictionary<string, string> errosValidacao;
            result.Result = BaseManager.Delete(id, out errosValidacao);
            result.Erros = errosValidacao;
            return result;
        }

        public ReturnMessage DeleteList(IList<int> id)
        {
            var result = new ReturnMessage();
            Dictionary<string, string> erros;
            result.Result = BaseManager.Delete(id, out erros);
            result.Erros = erros;
            return result;
        }
    }
}
