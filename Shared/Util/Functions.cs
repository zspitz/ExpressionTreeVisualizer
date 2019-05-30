using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static ExpressionToString.FormatterNames;

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
            } else if (o is MemberInfo mi) {
                bool isType;
                bool isByRef = false;
                if (mi is Type t1) {
                    isType = true;
                    if (t1.IsByRef) {
                        isByRef = true;
                        t1 = t1.GetElementType();
                    }
                } else {
                    isType = false;
                    t1 = mi.DeclaringType;
                }
                if (language == CSharp) {
                    ret = $"typeof({t1.FriendlyName(CSharp)})";
                } else {
                    ret = $"GetType({t1.FriendlyName(VisualBasic)})";
                }
                if (isByRef) { ret += ".MakeByRef()"; }
                if (!isType) {
                    if (mi is ConstructorInfo) {
                        ret += ".GetConstructor()";
                    } else {
                        string methodName = null;
                        switch (mi) {
                            case EventInfo _:
                                methodName = "GetEvent";
                                break;
                            case FieldInfo _:
                                methodName = "GetField";
                                break;
                            case MethodInfo _:
                                methodName = "GetMethod";
                                break;
                            case PropertyInfo _:
                                methodName = "GetProperty";
                                break;
                        }
                        ret += $".{methodName}(\"{mi.Name}\")";
                    }
                }
            } else if (type.IsArray && !type.GetElementType().IsArray && type.GetArrayRank() == 1) {
                var values = (o as dynamic[]).Joined(", ", x => RenderLiteral(x, language));
                if (language == CSharp) {
                    ret = $"new[] {{ {values} }}";
                } else {
                    ret = $"{{ {values} }}";
                }
            } else if (type.IsTupleType()) {
                // TODO render System.Tuple using Tuple.Create("abcd",5) ? #Tuple?
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
            if (!isLiteral && o.GetType().GetMethods().Where(x => x.Name == "ToString" && x.GetParameters().None() && x.DeclaringType != typeof(object)).Any()) {
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

        public static MemberInfo GetMember<T>(Expression<Func<T>> expr) =>
            (expr.Body as MemberExpression).Member;

        // TODO handle more than 8 values
        public static object[] TupleValues(object tuple) {
            if (!tuple.GetType().IsTupleType()) { throw new InvalidOperationException(); }
            var fields = tuple.GetType().GetFields();
            if (fields.Any()) { return tuple.GetType().GetFields().Select(x => x.GetValue(tuple)).ToArray(); }
            return tuple.GetType().GetProperties().Select(x => x.GetValue(tuple)).ToArray();
        }

        public static bool TryTupleValues(object tuple, out object[] values) {
            var isTupleType = tuple.GetType().IsTupleType();
            values = isTupleType ? TupleValues(tuple) : null;
            return isTupleType;
        }

        // based on https://github.com/dotnet/corefx/blob/7cf002ec36109943c048ec8da8ef80621190b4be/src/Common/src/CoreLib/System/Text/StringBuilder.cs#L1514
        public static (string literal, int? index, int? alignment, string itemFormat)[] ParseFormatString(string format) {
            if (format == null) { throw new ArgumentNullException("format"); }

            const int indexLimit = 1000000;
            const int alignmentLimit = 100000;

            int pos = -1;
            char ch = '\x0';
            int lastPos = format.Length - 1;

            var parts = new List<(string literal, int? index, int? alignment, string itemFormat)>();

            while (pos <= lastPos) {

                // Parse literal until argument placeholder
                string literal = "";
                while (pos < lastPos) {
                    advanceChar();

                    if (ch == '}') {
                        advanceChar();
                        if (ch == '}') {
                            literal += '}';
                        } else {
                            throw new FormatException("Missing start brace");
                        }
                    } else if (ch == '{') {
                        advanceChar();
                        if (ch == '{') {
                            literal += '{';
                        } else {
                            break;
                        }
                    } else {
                        literal += ch;
                    }
                }

                if (pos == lastPos) {
                    if (literal != "") {
                        parts.Add((literal, (int?)null, (int?)null, (string)null));
                    }
                    break;
                }

                // Parse index section; required
                int index = getNumber(indexLimit);

                // Parse alignment; optional
                int? alignment = null;
                if (ch == ',') {
                    advanceChar();
                    alignment = getNumber(alignmentLimit, true);
                }

                // Parse item format; optional
                string itemFormat = null;
                if (ch == ':') {
                    itemFormat = "";
                    while (true) {
                        advanceChar();
                        if (ch == '{') {
                            advanceChar();
                            if (ch == '{') {
                                itemFormat += '{';
                            } else {
                                throw new FormatException("Nested placeholders not allowed");
                            }
                        } else if (ch == '}') {
                            advanceChar(true);
                            if (ch == '}') {
                                itemFormat += '}';
                            } else {
                                break;
                            }
                        } else {
                            itemFormat += ch;
                        }
                    }
                }

                parts.Add((literal, index, alignment, itemFormat));
            }

            return parts.ToArray();

            void advanceChar(bool ignoreEnd = false) {
                pos += 1;
                if (pos <= lastPos) {
                    ch = format[pos];
                } else if (ignoreEnd) {
                    ch = '\x0';
                } else {
                    throw new FormatException("Unexpected end of text");
                }
            }

            void skipWhitespace() {
                while (ch == ' ') {
                    advanceChar(true);
                }
            }

            int getNumber(int limit, bool allowNegative = false) {
                skipWhitespace();

                bool isNegative = false;
                if (ch == '-') {
                    if (!allowNegative) { throw new FormatException("Negative number not allowed"); }
                    isNegative = true;
                    advanceChar();
                }
                if (ch < '0' || ch > '9') { throw new FormatException("Expected digit"); }
                int ret = 0;
                do {
                    ret = ret * 10 + ch - '0';
                    advanceChar();
                } while (ch >= '0' && ch <= '9' && ret < limit);

                skipWhitespace();

                return ret * (isNegative ? -1 : 1);
            }
        }

        public static KeyValuePair<TKey, TValue> KVP<TKey, TValue>(TKey key, TValue value) => new KeyValuePair<TKey, TValue>(key, value);

        public static bool IsReferenceComparison(ExpressionType nodeType, Expression left, Expression right, bool hasMethod) =>
            (nodeType == ExpressionType.Equal || nodeType == ExpressionType.NotEqual) &&
            !hasMethod &&
            !left.Type.IsValueType &&
            !right.Type.IsValueType;

        public static string ResolveLanguage(string language) {
            switch (language) {
                case CSharp:
                case VisualBasic:
                    return language;
                default:
                    return CSharp;
            }
        }
    }
}
