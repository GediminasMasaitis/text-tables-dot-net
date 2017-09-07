using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextTables.Internal
{
    internal static class ExtensionMethods
    {
        public static TValue AddOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            return AddOrCreate(dict, key, () => new TValue());
        }

        public static TValue AddOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> factory)
        {
            var exists = dict.TryGetValue(key, out var value);
            if (!exists)
            {
                value = factory();
                dict.Add(key, value);
            }
            return value;
        }
    }
}
