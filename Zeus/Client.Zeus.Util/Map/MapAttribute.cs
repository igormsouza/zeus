using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Util.Map
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Property)]
    public class MapAttribute : Attribute
    {
        // Fields
        private string propertyName;

        // Methods
        public MapAttribute()
        {
        }

        public MapAttribute(string property)
        {
            this.propertyName = property;
        }

        // Properties
        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
            set
            {
                this.propertyName = value;
            }
        }

        public string Type { get; set; }
    }
}
