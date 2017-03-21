using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BHS.ProjetoBaseMvc.Contrato.Interface.Base;
using BHS.ProjetoBaseMvc.Dominio;

namespace BHS.ProjetoBaseMvc.Contrato.Interface
{
    [ServiceContract]
    public interface IUFContrato : IContratoBase<TB_UF>
    {
        
    }
}
