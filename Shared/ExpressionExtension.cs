using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionToString {
    public static class ExpressionExtension {
        public static string ToString(this Expression expr, string formatter) =>
            WriterBase.Create(formatter, expr).ToString();

        public static string ToString(this Expression expr, string formatter, out Dictionary<string, (int start, int length)> pathSpans) =>
            WriterBase.Create(formatter, expr, out pathSpans).ToString();

        public static string ToString(this ElementInit init, string formatter) =>
            WriterBase.Create(formatter, init).ToString();

        public static string ToString(this ElementInit init, string formatter, out Dictionary<string, (int start, int length)> pathSpans) =>
            WriterBase.Create(formatter, init, out pathSpans).ToString();

        public static string ToString(this MemberBinding mbind, string formatter) =>
            WriterBase.Create(formatter, mbind).ToString();

        public static string ToString(this MemberBinding mbind, string formatter, out Dictionary<string, (int start, int length)> pathSpans) =>
            WriterBase.Create(formatter, mbind, out pathSpans).ToString();

        public static string ToString(this SwitchCase switchCase, string formatter) =>
            WriterBase.Create(formatter, switchCase).ToString();

        public static string ToString(this SwitchCase switchCase, string formatter, out Dictionary<string, (int start, int length)> pathSpans) =>
            WriterBase.Create(formatter, switchCase, out pathSpans).ToString();

        public static string ToString(this CatchBlock catchBlock, string formatter) =>
            WriterBase.Create(formatter, catchBlock).ToString();

        public static string ToString(this CatchBlock catchBlock, string formatter, out Dictionary<string, (int start, int length)> pathSpans) =>
            WriterBase.Create(formatter, catchBlock, out pathSpans).ToString();

        public static string ToString(this LabelTarget labelTarget, string formatter) =>
            WriterBase.Create(formatter, labelTarget).ToString();

        public static string ToString(this LabelTarget labelTarget, string formatter, out Dictionary<string, (int start, int length)> pathSpans) =>
            WriterBase.Create(formatter, labelTarget, out pathSpans).ToString();
    }
}
