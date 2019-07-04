using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionToString.Util {
    public static class IEnumerableTupleExtensions {
        public static void AddRangeTo<T1, T2>(this IEnumerable<(T1, T2)> src, IDictionary<T1, T2> dict) => dict.AddRange(src);
        public static IEnumerable<(T1, T2)> ForEachT<T1,T2>(this IEnumerable<(T1, T2)> src, Action<T1,T2> action) => src.ForEach(x => action(x.Item1, x.Item2));
        public static IEnumerable<(T1, T2)> ForEachT<T1, T2>(this IEnumerable<(T1, T2)> src, Action<T1, T2, int> action) => src.ForEach((x, index) => action(x.Item1, x.Item2, index));

        public static string Joined<T1, T2>(this IEnumerable<(T1, T2)> src, string delimiter, Func<T1, T2, string> selector) =>
            src.Joined(delimiter, x => selector(x.Item1, x.Item2));
        public static string Joined<T1, T2>(this IEnumerable<(T1, T2)> src, string delimiter, Func<T1, T2, int, string> selector) =>
            src.Joined(delimiter, (x, index) => selector(x.Item1, x.Item2, index));

        public static IEnumerable<T2> Item2s<T1, T2>(this IEnumerable<(T1, T2)> src) => src.Select(x => x.Item2);
        public static IEnumerable<TResult> SelectT<T1, T2, TResult>(this IEnumerable<ValueTuple<T1, T2>> src, Func<T1, T2, TResult> selector) =>
            src.Select(x => selector(x.Item1, x.Item2));
        public static IEnumerable<TResult> SelectT<T1, T2, T3, TResult>(this IEnumerable<ValueTuple<T1, T2, T3>> src, Func<T1, T2, T3, TResult> selector) =>
            src.Select(x => selector(x.Item1, x.Item2, x.Item3));
        public static IEnumerable<(T1, T2)> WhereT<T1, T2>(this IEnumerable<(T1, T2)> src, Func<T1, T2, bool> predicate) => src.Where(x => predicate(x.Item1, x.Item2));
    }
}
