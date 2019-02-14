using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionToString.Util {
    public static class StringExtensions {
        public static bool IsNullOrWhitespace(this string s) => string.IsNullOrWhiteSpace(s);
        public static bool ContainsAny(this string s, params string[] testStrings) => testStrings.Any(x => s.Contains(x));
        public static void AppendTo(this string s, StringBuilder sb) => sb.Append(s);
    }
}
