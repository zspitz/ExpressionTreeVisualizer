using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq {
    public static class IEnumerableTExtensions {
        public static bool None<T>(this IEnumerable<T> src, Func<T,bool> predicate = null) {
            if (predicate != null) { return !src.Any(predicate); }
            return !src.Any();
        }
    }
}
