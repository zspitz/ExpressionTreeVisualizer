using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionTreeTransform.Util {
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
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> src, Action<T, int> action) {
            var current = 0;
            foreach (var item in src) {
                action(item, current);
                current += 1;
            }
            return src;
        }

        public static void AddRangeTo<TKey, TValue>(this IEnumerable<(TKey, TValue)> toAdd, Dictionary<TKey, TValue> dict) => toAdd.ForEach(t=> dict.Add(t.Item1, t.Item2));
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> src) => src.ToDictionary(t => t.Item1, t=> t.Item2);

        public static string Joined<T>(this IEnumerable<T> source, string delimiter = ",", Func<T, string> selector = null) {
            if (source == null) { return ""; }
            if (selector == null) { return string.Join(delimiter, source); }
            return string.Join(delimiter, source.Select(selector));
        }
        public static string Joined<T>(this IEnumerable<T> source, string delimiter, Func<T, int, string> selector) {
            if (source == null) { return ""; }
            if (selector == null) { return string.Join(delimiter, source); }
            return string.Join(delimiter, source.Select(selector));
        }

        public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second) => first.Zip(second, (x, y) => (x, y));
    }
}
