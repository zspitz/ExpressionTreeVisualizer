using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionToString.Util {
    public static class ObjectExtensions {
        public static string Formatted(this object o, string format) => string.Format(format, o);
        public static string Formatted(this IEnumerable<object> objects, string format) => string.Format(format, objects.ToArray()); // we need ToArray to use the params overload
    }
}
