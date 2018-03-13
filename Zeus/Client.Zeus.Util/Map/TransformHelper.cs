using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Util.Map
{
    public static class TransformHelper
    {
        // Methods
        private static void AddObjectToCache(object source, Type destiny, object createdObject, Dictionary<object, Dictionary<Type, object>> cache)
        {
            Dictionary<Type, object> dictionary;
            if (!cache.ContainsKey(source))
            {
                dictionary = new Dictionary<Type, object>();
                cache[source] = dictionary;
            }
            else
            {
                dictionary = cache[source];
            }
            dictionary[destiny] = createdObject;
        }

        private static object GetCachedObject(object source, Type destiny, Dictionary<object, Dictionary<Type, object>> cache)
        {
            if (cache.ContainsKey(source))
            {
                Dictionary<Type, object> dictionary = cache[source];
                if (dictionary.ContainsKey(destiny))
                {
                    return dictionary[destiny];
                }
            }
            return null;
        }

        private static Property GetProperty(object instance, string propertyName)
        {
            return PropertyHelper.GetProperty(instance, propertyName);
        }

        private static Property GetProperty(object instance, MapAttribute[] attributes)
        {
            Property property = null;
            foreach (MapAttribute attribute in attributes)
            {
                property = PropertyHelper.GetProperty(instance, attribute.PropertyName);
                if (property != null)
                {
                    return property;
                }
            }
            return property;
        }

        private static Type GetType(MapAttribute[] attributes)
        {
            Type type = null;
            foreach (MapAttribute attribute in attributes)
            {
                string[] strArray = attribute.Type.Split(new char[] { ',' });
                if (strArray.Length == 2)
                {
                    Assembly assembly = Assembly.Load(strArray[1].Trim());
                    if (assembly != null)
                    {
                        type = assembly.GetType(strArray[0].Trim());
                    }
                }
                if (type != null)
                {
                    return type;
                }
            }
            return type;
        }

        public static T Transform<T>(this object source)
        {
            return (T)Transform(source, typeof(T));
        }

        public static object Transform(this object source, object target)
        {
            return Transform(source, target.GetType(), target, true, null);
        }

        public static T Transform<T>(this object source, string noMapValue)
        {
            return (T)Transform(source, typeof(T), null, true, noMapValue);
        }

        private static object Transform(object source, Type destiny)
        {
            return Transform(source, destiny, true);
        }

        public static T Transform<T>(this object source, T target)
        {
            return (T)Transform(source, typeof(T), target, true, null);
        }

        private static object Transform(object source, Type destiny, bool useMapAttribute)
        {
            return Transform(source, destiny, null, useMapAttribute, null);
        }

        private static object Transform(object source, Type destiny, object target, bool useMapAttribute, string noMapValue)
        {
            return Transform(source, destiny, target, useMapAttribute, noMapValue, null);
        }

        private static object Transform(object source, Type destiny, object target, bool useMapAttribute, string noMapValue, Dictionary<object, Dictionary<Type, object>> cache)
        {
            Func<NoMapAttribute, bool> predicate = null;
            
            if (source == null)
            {
                return null;
            }
            
            if (cache == null)
            {
                cache = new Dictionary<object, Dictionary<Type, object>>();
            }
            else
            {
                object obj2 = GetCachedObject(source, destiny, cache);
                if (obj2 != null)
                {
                    return obj2;
                }
            }

            MapAttribute[] attributes = null;
            
            if (useMapAttribute)
            {
                attributes = (MapAttribute[])Attribute.GetCustomAttributes(destiny.Assembly, typeof(MapAttribute), false);
            }

            if ((attributes != null) && (attributes.Length > 0))
            {
                destiny = GetType(attributes);
            }
            
            object createdObject = target ?? Activator.CreateInstance(destiny);
            
            if (destiny.IsEnum)
            {
                createdObject = source;
            }
            
            AddObjectToCache(source, destiny, createdObject, cache);
            PropertyInfo[] properties = destiny.GetProperties();
            
            foreach (PropertyInfo info in properties)
            {
                try
                {
                    Property property = null;
                    PropertyInfo propertyInfo = null;

                    if (useMapAttribute)
                    {
                        NoMapAttribute[] customAttributes = Attribute.GetCustomAttributes(info, typeof(NoMapAttribute)) as NoMapAttribute[];
                        bool flag = false;

                        if ((customAttributes != null) && (customAttributes.Length > 0))
                        {
                            if (predicate == null)
                            {
                                predicate = n => string.IsNullOrEmpty(n.NoMapValue) || (n.NoMapValue == noMapValue);
                            }
                            flag = customAttributes.Any<NoMapAttribute>(predicate);
                        }

                        if (flag)
                        {
                            continue;
                        }
                        MapAttribute[] attributeArray3 = (MapAttribute[])Attribute.GetCustomAttributes(info, typeof(MapAttribute));
                        
                        if (attributeArray3 != null)
                        {
                            property = GetProperty(source, attributeArray3);
                        }
                    }

                    if (property == null)
                    {
                        property = GetProperty(source, info.Name);
                    }

                    if (property != null)
                    {
                        propertyInfo = property.PropertyInfo;
                        object obj4 = property.Value;
                        
                        if (obj4 != null)
                        {
                            string[] strArray = propertyInfo.PropertyType.Namespace.Split(new char[] { '.' });
                            string str = string.Empty;
                            if (strArray.Length > 0)
                            {
                                str = strArray[0];
                            }
                            Type[] genericArguments = info.PropertyType.GetGenericArguments();
                            
                            if (propertyInfo.PropertyType.IsGenericType && (genericArguments.Length > 0))
                            {
                                info.SetValue(createdObject, (info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? obj4 : TransformList(obj4 as IEnumerable, genericArguments[0], useMapAttribute, noMapValue, cache), null);
                            }
                            else if (propertyInfo.PropertyType.IsArray && info.PropertyType.IsGenericType)
                            {
                                info.SetValue(createdObject, TransformArrayToGenericList((object[])obj4, info.PropertyType.GetElementType(), useMapAttribute, noMapValue, cache), null);
                            }
                            else if (info.PropertyType.IsArray && propertyInfo.PropertyType.IsGenericType)
                            {
                                info.SetValue(createdObject, TransformListToArray(obj4 as IList, info.PropertyType.GetElementType(), useMapAttribute, noMapValue, cache), null);
                            }
                            else if ((info.PropertyType.IsArray && propertyInfo.PropertyType.IsArray) && (obj4 is int[]))
                            {
                                info.SetValue(createdObject, ((int[])obj4).TransformInt32ArrayToInt32Array(), null);
                            }
                            else if (((info.PropertyType.IsArray && (info.PropertyType.GetElementType() == typeof(byte))) && propertyInfo.PropertyType.IsArray) && (obj4 is byte[]))
                            {
                                info.SetValue(createdObject, obj4, null);
                            }
                            else if (info.PropertyType.IsArray && propertyInfo.PropertyType.IsArray)
                            {
                                info.SetValue(createdObject, TransformArrayToArray((object[])obj4, info.PropertyType.GetElementType(), useMapAttribute, noMapValue, cache), null);
                            }
                            else if (!((propertyInfo.PropertyType.IsPrimitive || str.Equals("System")) || propertyInfo.PropertyType.IsEnum))
                            {
                                info.SetValue(createdObject, Transform(obj4, info.PropertyType, null, useMapAttribute, noMapValue, cache), null);
                            }
                            else if (!(propertyInfo.PropertyType.Equals(typeof(string)) || !info.PropertyType.Equals(typeof(string))))
                            {
                                info.SetValue(createdObject, obj4.ToString(), null);
                            }
                            else
                            {
                                info.SetValue(createdObject, obj4, null);
                            }
                        }
                        else
                        {
                            info.SetValue(createdObject, obj4, null);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return createdObject;
        }

        public static T[] TransformArrayToArray<T>(this object[] array, bool useMapAttribute, string noMapValue)
        {
            return (TransformArrayToArray(array, typeof(T), useMapAttribute, noMapValue) as T[]);
        }

        private static object[] TransformArrayToArray(IList list, Type objectType, bool useMapAttribute, string noMapValue)
        {
            return TransformArrayToArray(list, objectType, useMapAttribute, noMapValue, null);
        }

        private static object[] TransformArrayToArray(IList list, Type objectType, bool useMapAttribute, string noMapValue, Dictionary<object, Dictionary<Type, object>> cache)
        {
            if (cache == null)
            {
                cache = new Dictionary<object, Dictionary<Type, object>>();
            }
            object[] objArray = (object[])Activator.CreateInstance(objectType.MakeArrayType(), new object[] { list.Count });
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    objArray[i] = Transform(list[i], objectType, null, useMapAttribute, noMapValue, cache);
                }
            }
            return objArray;
        }

        private static IList TransformArrayToGenericList(object[] arrayList, Type genericListType, bool useMapAttribute, string noMapValue)
        {
            return TransformArrayToGenericList(arrayList, genericListType, useMapAttribute, noMapValue, null);
        }

        private static IList TransformArrayToGenericList(object[] arrayList, Type genericListType, bool useMapAttribute, string noMapValue, Dictionary<object, Dictionary<Type, object>> cache)
        {
            if (cache == null)
            {
                cache = new Dictionary<object, Dictionary<Type, object>>();
            }
            object[] args = (object[])Activator.CreateInstance(genericListType.MakeArrayType(), new object[] { arrayList.Length });
            for (int i = 0; i < arrayList.Length; i++)
            {
                args[i] = Transform(arrayList[i], genericListType, null, useMapAttribute, noMapValue, cache);
            }
            return (IList)Activator.CreateInstance(genericListType.MakeGenericType(new Type[] { genericListType }), args);
        }

        public static List<T> TransformArrayToList<T>(this object[] array, bool useMapAttribute, string noMapValue)
        {
            return (List<T>)TransformArrayToGenericList(array, typeof(T), useMapAttribute, noMapValue);
        }

        private static object TransformInt32ArrayToInt32Array(this int[] array)
        {
            int[] numArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                numArray[i] = array[i];
            }
            return numArray;
        }

        public static IList<T> TransformList<T>(this IEnumerable sourceList)
        {
            return (IList<T>)TransformList(sourceList, typeof(T), true, null);
        }

        public static IList<T> TransformList<T>(this IEnumerable sourceList, bool useMapAttribute, string noMapValue)
        {
            return (IList<T>)TransformList(sourceList, typeof(T), useMapAttribute, noMapValue);
        }

        private static IList TransformList(IEnumerable sourceList, Type destinyType, bool useMapAttribute, string noMapValue)
        {
            return TransformList(sourceList, destinyType, useMapAttribute, noMapValue, null);
        }

        private static IList TransformList(IEnumerable sourceList, Type destinyType, bool useMapAttribute, string noMapValue, Dictionary<object, Dictionary<Type, object>> cache)
        {
            if (cache == null)
            {
                cache = new Dictionary<object, Dictionary<Type, object>>();
            }
            IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { destinyType }));
            foreach (object obj2 in sourceList)
            {
                string[] strArray = destinyType.Namespace.Split(new char[] { '.' });
                string str = string.Empty;
                if (strArray.Length > 0)
                {
                    str = strArray[0];
                }
                if (!(destinyType.IsPrimitive || !(str != "System")))
                {
                    list.Add(Transform(obj2, destinyType, null, useMapAttribute, noMapValue, cache));
                }
                else
                {
                    list.Add(obj2);
                }
            }
            return list;
        }

        public static IList<T> TransformListNoMap<T>(this IEnumerable sourceList)
        {
            return (IList<T>)TransformList(sourceList, typeof(T), false, null);
        }

        public static IList<T> TransformListNoMap<T>(this IEnumerable sourceList, string noMapValue)
        {
            return (IList<T>)TransformList(sourceList, typeof(T), false, noMapValue);
        }

        public static T[] TransformListToArray<T>(this IList list, bool useMapAttribute, string noMapValue)
        {
            return (TransformListToArray(list, typeof(T), useMapAttribute, noMapValue) as T[]);
        }

        private static object[] TransformListToArray(IList list, Type objectType, bool useMapAttribute, string noMapValue)
        {
            return TransformListToArray(list, objectType, useMapAttribute, noMapValue);
        }

        private static object[] TransformListToArray(IList list, Type objectType, bool useMapAttribute, string noMapValue, Dictionary<object, Dictionary<Type, object>> cache)
        {
            if (cache == null)
            {
                cache = new Dictionary<object, Dictionary<Type, object>>();
            }
            Type type = objectType.MakeArrayType();
            object[] objArray = (object[])Activator.CreateInstance(type, new object[] { list.Count });
            for (int i = 0; i < list.Count; i++)
            {
                if (!(objectType.IsPrimitive || objectType.Namespace.Equals("System")))
                {
                    objArray[i] = Transform(list[i], type.GetElementType(), null, useMapAttribute, noMapValue, cache);
                }
                else
                {
                    objArray[i] = list[i];
                }
            }
            return objArray;
        }

        public static T TransformNoMap<T>(this object source)
        {
            return (T)Transform(source, typeof(T), false);
        }
    }
}
