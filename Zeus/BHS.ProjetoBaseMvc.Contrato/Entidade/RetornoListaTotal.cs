using BHS.ProjetoBaseMvc.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Contrato.Entidade
{
    [DataContract]
    public class RetornoListaTotal<T> where T : DominioBase
    {
        [DataMember]
        public int TotalItens { get; set; }

        [DataMember]
        public IList<T> Lista { get; set; }
    }
}
