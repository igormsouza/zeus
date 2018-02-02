using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Domain.Base
{
    public class GridField
    {
        public GridField(string name)
            : this(name, name)
        {

        }

        public GridField(string name, string columnTitle, bool active = true)
        {
            Name = name;
            ColumnTitle = columnTitle;
            Active = active;
        }

        public string Name { get; set; }

        public string ColumnTitle { get; set; }

        public bool Active { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, ColumnTitle);
        }
    }
}
