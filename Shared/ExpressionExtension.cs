using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString {
    public static class ExpressionExtension {
        public static string ToCode(this Expression expr, string language) =>
            CodeWriter.Create(language, expr).ToString();

        public static string ToCode(this Expression expr, string language, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            CodeWriter.Create(language, expr, out visitedParts).ToString();
    }
}
