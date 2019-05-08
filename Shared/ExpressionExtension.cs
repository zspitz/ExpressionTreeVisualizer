using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionToString {
    public static class ExpressionExtension {
        public static string ToString(this Expression expr, string formatter) =>
            FormatterBase.Create(formatter, expr).ToString();

        public static string ToString(this Expression expr, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            FormatterBase.Create(formatter, expr, out visitedParts).ToString();

        public static string ToString(this ElementInit init, string formatter) =>
            FormatterBase.Create(formatter, init).ToString();

        public static string ToString(this ElementInit init, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            FormatterBase.Create(formatter, init, out visitedParts).ToString();

        public static string ToString(this MemberBinding mbind, string formatter) =>
            FormatterBase.Create(formatter, mbind).ToString();

        public static string ToString(this MemberBinding mbind, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            FormatterBase.Create(formatter, mbind, out visitedParts).ToString();

        public static string ToString(this SwitchCase switchCase, string formatter) =>
            FormatterBase.Create(formatter, switchCase).ToString();

        public static string ToString(this SwitchCase switchCase, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            FormatterBase.Create(formatter, switchCase, out visitedParts).ToString();

        public static string ToString(this CatchBlock catchBlock, string formatter) =>
            FormatterBase.Create(formatter, catchBlock).ToString();

        public static string ToString(this CatchBlock catchBlock, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            FormatterBase.Create(formatter, catchBlock, out visitedParts).ToString();

        public static string ToString(this LabelTarget labelTarget, string formatter) =>
            FormatterBase.Create(formatter, labelTarget).ToString();

        public static string ToString(this LabelTarget labelTarget, string formatter, out Dictionary<object, List<(int start, int length)>> visitedParts) =>
            FormatterBase.Create(formatter, labelTarget, out visitedParts).ToString();
    }
}
