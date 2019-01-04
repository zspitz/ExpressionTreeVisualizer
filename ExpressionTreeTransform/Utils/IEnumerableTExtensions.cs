using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq {
    public static class IEnumerableTExtensions {
        public static bool None<T>(this IEnumerable<T> src, Func<T,bool> predicate = null) {
            if (predicate != null) { return !src.Any(predicate); }
            return !src.Any();
        }
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> src, Action<T> action) {
            foreach (var item in src) {
                action(item);
            }
            return src;
        }
        public static void AddRangeTo<TKey, TValue>(this IEnumerable<(TKey, TValue)> toAdd, Dictionary<TKey, TValue> dict) => toAdd.ForEach(t=> dict.Add(t.Item1, t.Item2));
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> src) => src.ToDictionary(t => t.Item1, t=> t.Item2);
    }
}
