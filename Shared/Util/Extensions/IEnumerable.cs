using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionToString.Util {
    public static class IEnumerableExtensions {
        public static bool Any(this IEnumerable src) {
            if (src == null) { return false; }
            foreach (var item in src) {
                return true;
            }
            return false;
        }
        public static bool None(this IEnumerable src) => !src.Any();

        public static List<object> ToObjectList(this IEnumerable src) => src.Cast<object>().ToList();
    }
}
