using Client.Zeus.Contract.Entity;
using Client.Zeus.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Contract.Interface.Base
{
    [ServiceContract]
    public interface IBaseContract<T> where T : BaseDomain, new()
    {
        [OperationContract]
        IList<T> List();

        [OperationContract]
        ReturnTotalList<T> PagingList(int currentPage, string order, string orderDirection, int countPerPage);

        [OperationContract]
        T GetById(int id);

        [OperationContract]
        ReturnMessage Create(T entity);

        [OperationContract]
        ReturnMessage Edit(T entity);

        [OperationContract]
        ReturnMessage Delete(int id);

        [OperationContract]
        ReturnMessage DeleteList(IList<int> id);
    }
}
