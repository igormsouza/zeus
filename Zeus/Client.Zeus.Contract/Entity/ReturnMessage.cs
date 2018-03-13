using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Contract.Entity
{
    [DataContract]
    public class ReturnMessage
    {
        [DataMember]
        public bool Result { get; set; }
        
        [DataMember]
        public Dictionary<string, string> Erros { get; set; }
    }
}
