using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.Base
{
    [DataContract]
    public class BaseDomain
    {
        [DataMember]
        [DisplayName("Id")]
        public int ID { get; set; }
    }
}
