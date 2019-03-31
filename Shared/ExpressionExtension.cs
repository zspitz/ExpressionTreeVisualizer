using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionToString {
    public static class ExpressionExtension {
        public static string ToString(this Expression expr, string formatter) =>
            CodeWriter.Create(formatter, expr).ToString();

        public static string ToString(this Expression expr, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            CodeWriter.Create(formatter, expr, out visitedParts).ToString();

        public static string ToString(this ElementInit init, string formatter) =>
            CodeWriter.Create(formatter, init).ToString();

        public static string ToString(this ElementInit init, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            CodeWriter.Create(formatter, init, out visitedParts).ToString();

        public static string ToString(this MemberBinding mbind, string formatter) =>
            CodeWriter.Create(formatter, mbind).ToString();

        public static string ToString(this MemberBinding mbind, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            CodeWriter.Create(formatter, mbind, out visitedParts).ToString();

        public static string ToString(this SwitchCase switchCase, string formatter) =>
            CodeWriter.Create(formatter, switchCase).ToString();

        public static string ToString(this SwitchCase switchCase, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            CodeWriter.Create(formatter, switchCase, out visitedParts).ToString();

        public static string ToString(this CatchBlock catchBlock, string formatter) =>
            CodeWriter.Create(formatter, catchBlock).ToString();

        public static string ToString(this CatchBlock catchBlock, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            CodeWriter.Create(formatter, catchBlock, out visitedParts).ToString();
    }
}
