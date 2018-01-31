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

        public Field(string name = "", string cssClass = "", enumFieldType tipo = enumFieldType.Default, 
            string valorFixo = "", string linkActionGrid = "", string linkControllerGrid = "", string largura = "6", bool ativo = true)
        {
            Name = name;
            CssClass = cssClass;
            Tipo = tipo;
            ValorFixo = valorFixo;
            LinkActionGrid = linkActionGrid;
            LinkControllerGrid = linkControllerGrid;
            Active = ativo;
            Largura = largura;
        }

        public string Name { get; set; }

        public string LinkActionGrid { get; set; }

        public string LinkControllerGrid { get; set; }

        public string ValorFixo { get; set; }

        public string CssClass { get; set; }

        public enumFieldType Tipo { get; set; }

        public string CarregarControle { get; set; }

        public string Largura { get; set; }

        public bool Active { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
