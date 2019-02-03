using System;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Util {
    public static class Functions {
        public static string RenderLiteral(object o, string language, out bool rendered) {
            rendered = true;
            if (language.NotIn(CSharp, VisualBasic)) { throw new NotImplementedException("Invalid language"); }

            if (o == null) { return language == CSharp ? "null" : "Nothing"; }
            if (o is bool b) {
                if (language == CSharp) { return b ? "true" : "false"; }
                return b ? "True" : "False";
            }
            var type = o.GetType().UnderlyingIfNullable();
            if (type == typeof(string)) { return $"\"{o.ToString()}\""; }
            if (type.IsNumeric()) { return o.ToString(); }
            if (language == VisualBasic && type.In(typeof(DateTime), typeof(TimeSpan), typeof(bool))) {
                return $"#{o.ToString()}#";
            }
            rendered = false;
            return $"#{type.FriendlyName(language)}";
        }
        public static string RenderLiteral(object o, string language) => RenderLiteral(o, language, out var rendered);
        public static string StringValue(object o, string language) {
            var asLiteral = RenderLiteral(o, language, out var rendered);
            if (!rendered && o.GetType().UnderlyingIfNullable().In(typeof(DateTime), typeof(TimeSpan))) {
                return o.ToString();
            }
            return asLiteral;
        }
    }
}
