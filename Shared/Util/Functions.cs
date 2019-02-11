using System;
using System.Linq.Expressions;
using System.Reflection;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Util {
    public static class Functions {
        public static (bool isLiteral, string repr) TryRenderLiteral(object o, string language) {
            if (language.NotIn(CSharp, VisualBasic)) { throw new NotImplementedException("Invalid language"); }

            if (o == null) { return (true, language == CSharp ? "null" : "Nothing"); }
            if (o is bool b) {
                if (language == CSharp) { return (true, b ? "true" : "false"); }
                return (true, b ? "True" : "False");
            }
            var type = o.GetType().UnderlyingIfNullable();
            if (type == typeof(string)) { return (true, $"\"{o.ToString()}\""); }
            if (type.IsNumeric()) { return (true, o.ToString()); }
            if (language == VisualBasic && type.In(typeof(DateTime), typeof(TimeSpan))) {
                return (true, $"#{o.ToString()}#");
            }
            return (false, $"#{type.FriendlyName(language)}");
        }

        public static string RenderLiteral(object o, string language) => TryRenderLiteral(o, language).repr;

        /// <summary>Returns a string representation of the value, which may or may not be a valid literal in the language</summary>
        public static string StringValue(object o, string language) {
            var (isLiteral, repr) = TryRenderLiteral(o, language);
            if (!isLiteral && o.GetType().GetMethod("ToString").DeclaringType != typeof(object)) {
                return o.ToString();
            }
            return repr;
        }

        public static MethodInfo GetMethod(Expression<Action> expr, params Type[] typeargs) {
            var ret = (expr.Body as MethodCallExpression).Method;
            if (ret.IsGenericMethod) {
                ret = ret.GetGenericMethodDefinition().MakeGenericMethod(typeargs);
            }
            return ret;
        }
    }
}
