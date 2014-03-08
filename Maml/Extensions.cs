using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Maml
{
    internal static class Extensions
    {
        public static Dictionary<string, object> ToDictionary(this object source, bool emptyDictionaryOnNull = false)
        {
            if (source != null)
            {
                return source
                    .GetType()
                    .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .ToDictionary(pi => pi.Name, pi => pi.GetValue(source, null));
            }
            else
            {
                return emptyDictionaryOnNull ? new Dictionary<string, object>() : null;
            }
        }

        public static Dictionary<string, string> ToStringDictionary(this object source)
        {
            return source
                .GetType()
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(pi => pi.Name, pi => { var ret = pi.GetValue(source, null); return ret == null ? null : ret.ToString(); });
        }

        public static RT Safe<T, RT>(this T obj, Func<T, RT> getFunc, RT defaultValue = default(RT)) where T : class
        {
            return obj == null ? defaultValue : getFunc(obj);
        }
    }

}
