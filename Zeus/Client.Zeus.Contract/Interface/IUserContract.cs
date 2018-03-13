using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Client.Zeus.Contract.Interface.Base;
using Client.Zeus.Domain;

namespace Client.Zeus.Contract.Interface
{
    [ServiceContract]
    public interface IUserContract : IBaseContract<TB_USER>
    {
        
    }
}
