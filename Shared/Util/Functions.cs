using System;
using System.Linq.Expressions;
using System.Reflection;
using static ExpressionToString.FormatterNames;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionToString.Util {
    public static class Functions {
        public static (bool isLiteral, string repr) TryRenderLiteral(object o, string language) {
            if (language.NotIn(CSharp, VisualBasic)) { throw new NotImplementedException("Invalid language"); }

            var type = o?.GetType().UnderlyingIfNullable();
            bool rendered = true;
            string ret = null;

            if (o == null) {
                ret = language == CSharp ? "null" : "Nothing";
            } else if (o is bool b) {
                if (language == CSharp) {
                    ret = b ? "true" : "false";
                } else {
                    ret = b ? "True" : "False";
                }
            } else if (o is char c) {
                if (language == CSharp) {
                    ret = $"'{c}'";
                } else {
                    ret = $"\"{c}\"C";
                }
            } else if ((o is DateTime || o is TimeSpan) && language == VisualBasic) {
                ret = $"#{o.ToString()}#";
            } else if (o is string s) {
                ret = s.ToVerbatimString(language);
            } else if (o is Enum e) {
                ret = $"{e.GetType().Name}.{e.ToString()}";
            } else if (type.IsArray && !type.GetElementType().IsArray && type.GetArrayRank() == 1) {
                var values = (o as dynamic[]).Joined(", ", x => RenderLiteral(x, language));
                if (language == CSharp) {
                    ret = $"new [] {{ {values} }}";
                } else {
                    ret = $"{{ {values} }}";
                }
            } else if (type.IsTupleType()) {
                ret = "(" + TupleValues(o).Select(x => RenderLiteral(x, language)).Joined(", ") + ")";
            } else if (type.IsNumeric()) {
                ret = o.ToString();
            } else {
                rendered = false;
                ret = $"#{type.FriendlyName(language)}";
            }
            return (rendered, ret);
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
            // TODO handle partially open generic methods
            if (typeargs.Any() && ret.IsGenericMethod) {
                ret = ret.GetGenericMethodDefinition().MakeGenericMethod(typeargs);
            }
            return ret;
        }

        // TODO handle more than 8 values
        public static object[] TupleValues(object tuple) {
            if (!tuple.GetType().IsTupleType()) { throw new InvalidOperationException(); }
            var fields = tuple.GetType().GetFields();
            if(fields.Any()) { return tuple.GetType().GetFields().Select(x => x.GetValue(tuple)).ToArray(); }
            return tuple.GetType().GetProperties().Select(x => x.GetValue(tuple)).ToArray();
        }
    }
}
