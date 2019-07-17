using System.Collections.Generic;
using System.Linq;

namespace AopLogging
{
    public static class ExtensionMethods
    {
        public static string ToFlatString<TKey, TValue>(this IDictionary<TKey,TValue> dict)
        {
            return string.Join(Constants.ArrayDelimiter, dict.Select(kvp => kvp.Key?.ToString() + Constants.KeyValueDelimiter + kvp.Value?.ToString()).ToArray());
        }

        public static string ToFlatString(this object[] array)
        {
            return string.Join(Constants.ArrayDelimiter, array);
        }
    }
}