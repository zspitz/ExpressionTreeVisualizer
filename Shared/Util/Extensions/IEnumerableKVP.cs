using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionToString.Util {
    public static class IEnumerableKVPExtensions {
        public static void AddRangeTo<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> toAdd, Dictionary<TKey, TValue> dict) => toAdd.ForEach(kvp => dict.Add(kvp.Key, kvp.Value));
        public static IEnumerable<TValue> Values<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> src) => src.Select(x => x.Value);
    }
}
