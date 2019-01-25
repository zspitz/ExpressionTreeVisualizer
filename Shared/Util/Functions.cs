using System;
using System.Collections.Generic;
using System.Text;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Util {
    public static class Functions {
        public static string RenderLiteral(object o, string language, out bool rendered) {
            rendered = true;
            if (language.NotIn(CSharp, VisualBasic)) { throw new NotImplementedException("Invalid language"); }

            if (o == null) { return language == CSharp ? "null" : "Nothing"; }
            if (language==CSharp && o is bool b) { return b ? "true" : "false"; }
            var type = o.GetType().UnderlyingIfNullable();
            if (type == typeof(string)) { return $"\"{o.ToString()}\""; }
            if (type.IsNumeric()) { return o.ToString(); }
            if (language == VisualBasic && type.In(typeof(DateTime), typeof(TimeSpan))) {
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
