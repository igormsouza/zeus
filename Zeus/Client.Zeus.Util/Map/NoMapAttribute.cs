using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Util.Map
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class NoMapAttribute : Attribute
    {
        // Methods
        public NoMapAttribute()
        {
        }

        public NoMapAttribute(string noMapValue)
        {
            this.NoMapValue = noMapValue;
        }

        // Properties
        public string NoMapValue { get; set; }
    }
}
