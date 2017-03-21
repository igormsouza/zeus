using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Util.Map
{
    public class PropertyHelper
    {
        // Methods
        public static Property GetProperty(object instance, string sufix)
        {
            Property property = null;
            
            if (string.IsNullOrEmpty(sufix))
            {
                return property;
            }
            
            string[] strArray = sufix.Split(new char[] { '.' });
            
            if (strArray.Length <= 0)
            {
                return property;
            }

            string name = strArray[0];
            string str2 = string.Empty;
            StringBuilder builder = new StringBuilder();
            
            for (int i = 0; i < strArray.Length; i++)
            {
                if (i != 0)
                {
                    builder.Append(str2 + strArray[i]);
                    if (str2.Length == 0)
                    {
                        str2 = ".";
                    }
                }
            }
            
            IList list = instance as IList;
            
            if ((list != null) && (list.Count > 0))
            {
                instance = list[0];
            }
            
            Type type = instance.GetType();
            PropertyInfo info = null;

            try
            {
                info = type.GetProperty(name) ?? type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Instance);
            }
            catch
            {
                try
                {
                    if (type.BaseType != null)
                    {
                        info = type.BaseType.GetProperty(name);
                    }
                }
                catch
                {
                    info = null;
                }
            }

            if (info == null)
            {
                return property;
            }

            object obj2 = info.GetValue(instance, null);
            
            if ((builder.Length > 0) && (obj2 != null))
            {
                return GetProperty(obj2, builder.ToString());
            }

            return new Property { PropertyInfo = info, Source = instance, Value = obj2 };
        }

        public static object GetPropertyValue(object instance, string sufix)
        {
            Property property = GetProperty(instance, sufix);
            return ((property != null) ? property.Value : null);
        }
    }
}
