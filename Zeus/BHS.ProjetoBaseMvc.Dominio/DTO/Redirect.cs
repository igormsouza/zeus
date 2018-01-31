using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.DTO
{
    public class Redirect
    {
        public string OrginController { get; set; }
        public string OrignAction { get; set; }
        public string DestinyController { get; set; }
        public string DestinyAction { get; set; }

        private IDictionary<string, object> parameters;
        public IDictionary<string, object> Parameters
        {
            get 
            {
                if (parameters == null)
                    parameters = new Dictionary<string, object>();
                return parameters;
            }
            set { parameters = value; }
        }
    }
}
