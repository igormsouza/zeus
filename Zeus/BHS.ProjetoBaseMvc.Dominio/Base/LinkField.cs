using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.Base
{
    public class LinkField
    {
        public LinkField(string name, string linkControllerGrid, string linkActionGrid, string referenceField = "", string parameterUrl = "id")
        {
            Name = name;
            LinkControllerGrid = linkControllerGrid;
            LinkActionGrid = linkActionGrid;
            ReferenceField = referenceField;
            ParameterUrl = parameterUrl;
        }

        public string Name { get; set; }

        public string LinkActionGrid { get; set; }

        public string LinkControllerGrid { get; set; }

        public string ReferenceProporty { get; set; }

        public string ReferenceField { get; set; }

        public string ParameterUrl { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
