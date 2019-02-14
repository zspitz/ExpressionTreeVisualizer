using System;
using System.Collections;
using System.Collections.Generic;
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
        public static object First(this IEnumerable src) {
            foreach (var item in src) {
                return item;
            }
            return null;
        }
    }
}
