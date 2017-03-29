using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.Base
{
    [DataContract]
    public class BaseDomain
    {
        [DataMember]
        [DisplayName("Código")]
        public int ID { get; set; }
    }
}
