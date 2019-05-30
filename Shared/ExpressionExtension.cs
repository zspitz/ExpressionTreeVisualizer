using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionToString {
    public static class ExpressionExtension {
        public static string ToString(this Expression expr, string formatter, string language = "Formatter default") =>
            WriterBase.Create(expr, formatter, language).ToString();

        public static string ToString(this Expression expr, string formatter, out Dictionary<string, (int start, int length)> pathSpans, string language = "Formatter default") =>
            WriterBase.Create(expr, formatter, language, out pathSpans).ToString();

        public static string ToString(this ElementInit init, string formatter, string language = "Formatter default") =>
            WriterBase.Create(init, formatter, language).ToString();

        public static string ToString(this ElementInit init, string formatter, out Dictionary<string, (int start, int length)> pathSpans, string language = "Formatter default") =>
            WriterBase.Create(init, formatter, language, out pathSpans).ToString();

        public static string ToString(this MemberBinding mbind, string formatter, string language = "Formatter default") =>
            WriterBase.Create(mbind, formatter, language).ToString();

        public static string ToString(this MemberBinding mbind, string formatter, out Dictionary<string, (int start, int length)> pathSpans, string language = "Formatter default") =>
            WriterBase.Create(mbind, formatter, language, out pathSpans).ToString();

        public static string ToString(this SwitchCase switchCase, string formatter, string language = "Formatter default") =>
            WriterBase.Create(switchCase, formatter, language).ToString();

        public static string ToString(this SwitchCase switchCase, string formatter, out Dictionary<string, (int start, int length)> pathSpans, string language = "Formatter default") =>
            WriterBase.Create(switchCase, formatter, language, out pathSpans).ToString();

        public static string ToString(this CatchBlock catchBlock, string formatter, string language = "Formatter default") =>
            WriterBase.Create(catchBlock, formatter, language).ToString();

        public static string ToString(this CatchBlock catchBlock, string formatter, out Dictionary<string, (int start, int length)> pathSpans, string language = "Formatter default") =>
            WriterBase.Create(catchBlock, formatter, language, out pathSpans).ToString();

        public static string ToString(this LabelTarget labelTarget, string formatter, string language = "Formatter default") =>
            WriterBase.Create(labelTarget, formatter, language).ToString();

        public static string ToString(this LabelTarget labelTarget, string formatter, out Dictionary<string, (int start, int length)> pathSpans, string language = "Formatter default") =>
            WriterBase.Create(labelTarget, formatter, language, out pathSpans).ToString();
    }
}
