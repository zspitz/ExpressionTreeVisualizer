using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionTreeTransform.Util {
    public static class IEnumerableKVPExtensions {
        public static void AddRangeTo<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> toAdd, Dictionary<TKey, TValue> dict) => toAdd.ForEach(kvp => dict.Add(kvp.Key, kvp.Value));
    }
}
