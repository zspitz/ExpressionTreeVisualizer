using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeVisualizer.Util {
    internal static class TExtensions {
        internal static bool In<T>(this T val, IEnumerable<T> vals) => vals.Contains(val);
        internal static bool In<T>(this T val, params T[] vals) => vals.Contains(val);
        internal static bool In(this char c, string s) => s.IndexOf(c) > -1;
        internal static bool In<T>(this T val, HashSet<T> vals) => vals.Contains(val);

        internal static bool NotIn<T>(this T val, IEnumerable<T> vals) => !vals.Contains(val);
        internal static bool NotIn<T>(this T val, params T[] vals) => !vals.Contains(val);
        internal static bool NotIn(this char c, string s) => s.IndexOf(c) == -1;
        internal static bool NotIn<T>(this T val, HashSet<T> vals) => !vals.Contains(val);
    }
}
