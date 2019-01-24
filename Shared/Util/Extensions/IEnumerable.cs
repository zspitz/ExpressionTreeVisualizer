using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExpressionTreeTransform.Util {
    public static class IEnumerableExtensions {
        public static bool Any(this IEnumerable src) {
            if (src == null) { return false; }
            foreach (var item in src) {
                return true;
            }
            return false;
        }
    }
}
