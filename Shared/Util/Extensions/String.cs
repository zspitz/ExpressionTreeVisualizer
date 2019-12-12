using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static ExpressionToString.FormatterNames;
using System.Diagnostics.CodeAnalysis;

namespace ExpressionToString.Util {
    public static class StringExtensions {
        public static bool IsNullOrWhitespace([NotNullWhen(false)]this string? s) => string.IsNullOrWhiteSpace(s);
        public static bool ContainsAny(this string s, params string[] testStrings) => testStrings.Any(x => s.Contains(x));
        public static void AppendTo(this string s, StringBuilder sb) => sb.Append(s);

        // https://stackoverflow.com/a/14502246/111794
        private static string ToCSharpLiteral(this string input) {
            var literal = new StringBuilder("\"", input.Length + 2);
            foreach (var c in input) {
                switch (c) {
                    case '\'': literal.Append(@"\'"); break;
                    case '\"': literal.Append("\\\""); break;
                    case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\0"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        if (char.GetUnicodeCategory(c) != UnicodeCategory.Control) {
                            literal.Append(c);
                        } else {
                            literal.Append(@"\u");
                            literal.Append(((ushort)c).ToString("x4"));
                        }
                        break;
                }
            }
            literal.Append("\"");
            return literal.ToString();
        }

        private static readonly char[] specialChars = new[] {
            '\'','\"', '\\','\0','\a','\b','\f','\n','\r', '\t','\v'
        };
        public static bool HasSpecialCharacters(this string s) =>
            s.IndexOfAny(specialChars) > -1;

        public static string ToVerbatimString(this string s, string language) =>
            language switch {
                CSharp => s.ToCSharpLiteral(),
                VisualBasic => $"\"{s.Replace("\"", "\"\"")}\"",
                _ => throw new ArgumentException("Invalid language"),
            };

        public static void AppendLineTo(this string s, StringBuilder sb, int indentationLevel = 0) {
            s = (s ?? "").TrimEnd();
            var toAppend = new string(' ', indentationLevel * 4) + s.TrimEnd();
            sb.AppendLine(toAppend);
        }

        public static string? ToCamelCase(this string s) {
            if (s == null || s.Length == 0) { return s; }
            return char.ToLowerInvariant(s[0]) + s.Substring(1);
        }
    }
}
