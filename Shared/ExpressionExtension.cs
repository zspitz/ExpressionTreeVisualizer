using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionToString {
    public static class ExpressionExtension {
        public static string ToString(this Expression expr, string formatter) =>
            CodeWriter.Create(formatter, expr).ToString();

        public static string ToString(this Expression expr, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            CodeWriter.Create(formatter, expr, out visitedParts).ToString();
    }
}
