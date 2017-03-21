using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Dominio.DTO
{
    public class Redirecionar
    {
        public string ControllerOrigem { get; set; }
        public string ActionOrigem { get; set; }
        public string ControllerAcionado { get; set; }
        public string ActionAcionado { get; set; }

        private IDictionary<string, object> parametros;
        public IDictionary<string, object> Parametros
        {
            get 
            {
                if (parametros == null)
                    parametros = new Dictionary<string, object>();
                return parametros;
            }
            set { parametros = value; }
        }
    }
}
