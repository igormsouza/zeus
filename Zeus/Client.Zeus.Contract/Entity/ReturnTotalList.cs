using Client.Zeus.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Contract.Entity
{
    [DataContract]
    public class ReturnTotalList<T> where T : BaseDomain
    {
        [DataMember]
        public int TotalItems { get; set; }

        [DataMember]
        public IList<T> List { get; set; }
    }
}
