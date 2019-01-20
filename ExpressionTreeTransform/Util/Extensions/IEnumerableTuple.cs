using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionTreeTransform.Util {
    public static class IEnumerableTupleExtensions {
        public static void AddRangeTo<T1, T2>(this IEnumerable<(T1, T2)> src, IDictionary<T1, T2> dict) => dict.AddRange(src);
    }
}
