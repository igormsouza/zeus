using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.Base
{
    public class Field
    {
        public Field(string name)
            : this(name, "", enumFieldType.Default)
        {

        }

        public Field(string name, string linkControllerGrid, string linkActionGrid)
            : this(name, "", enumFieldType.Default, linkControllerGrid: linkControllerGrid, linkActionGrid: linkActionGrid)
        {

        }

        public Field(string name = "", string cssClass = "", enumFieldType type = enumFieldType.Default, 
            string fixedValue = "", string linkActionGrid = "", string linkControllerGrid = "", string width = "6", bool ativo = true)
        {
            Name = name;
            CssClass = cssClass;
            Type = type;
            FixedValue = fixedValue;
            LinkActionGrid = linkActionGrid;
            LinkControllerGrid = linkControllerGrid;
            Active = ativo;
            Width = width;
        }

        public string Name { get; set; }

        public string LinkActionGrid { get; set; }

        public string LinkControllerGrid { get; set; }

        public string FixedValue { get; set; }

        public string CssClass { get; set; }

        public enumFieldType Type { get; set; }

        public string LoadControl { get; set; }

        public string Width { get; set; }

        public bool Active { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
