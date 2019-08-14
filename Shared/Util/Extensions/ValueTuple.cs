using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString.Util {
    public static class ValueTupleExtensions {
        public static IEnumerable<T> Select<T>(this (T,T,T,T) src) {
            yield return src.Item1;
            yield return src.Item2;
            yield return src.Item3;
            yield return src.Item4;
        }

        public static IEnumerable<T> Where<T>(this (T, T, T, T) src, Func<T, bool> selector) => src.Select().Where(selector);
    }
}
