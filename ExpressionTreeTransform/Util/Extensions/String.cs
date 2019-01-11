using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionTreeTransform.Util {
    public static class StringExtensions {
        public static bool IsNullOrWhitespace(this string s) => String.IsNullOrWhiteSpace(s);
    }
}
