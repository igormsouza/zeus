using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Util.Map
{
    public class Property
    {
        public PropertyInfo PropertyInfo { get; set; }

        public object Source { get; set; }

        public object Value { get; set; }
    }
}
