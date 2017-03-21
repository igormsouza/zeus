using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Contrato.Entidade
{
    [DataContract]
    public class RetornoComMensagem
    {
        [DataMember]
        public bool Retorno { get; set; }
        
        [DataMember]
        public Dictionary<string, string> ErrosValidacao { get; set; }
    }
}
