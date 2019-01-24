using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Util {
    public static class TypeExtensions {
        public static Type UnderlyingIfNullable(this Type type) => Nullable.GetUnderlyingType(type) ?? type;

        private static readonly HashSet<Type> numericTypes = new HashSet<Type>() {
            typeof(byte),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(sbyte),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(BigInteger),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        public static bool IsNumeric(this Type type) => type.UnderlyingIfNullable().In(numericTypes);

        public static bool InheritsFromOrImplements<T>(this Type type) => typeof(T).IsAssignableFrom(type);

        public static bool IsClosureClass(this Type type) =>
            type.HasAttribute<CompilerGeneratedAttribute>() && type.Name.ContainsAny("DisplayClass", "Closure$");

        public static bool IsAnonymous(this Type type) =>
            type.HasAttribute<CompilerGeneratedAttribute>() && type.Name.Contains("Anonymous") && type.Name.ContainsAny("<>", "VB$");

        private static readonly Dictionary<Type, string> CSKeywordTypes = new Dictionary<Type, string> {
            {typeof(bool), "bool"},
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(char), "char"},
            {typeof(decimal), "decimal"},
            {typeof(double), "double"},
            {typeof(float), "float"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(object), "object"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(string), "string"}
        };

        private static readonly Dictionary<Type, string> VBKeywordTypes = new Dictionary<Type, string> {
            {typeof(bool), "Boolean"},
            {typeof(byte), "Byte"},
            {typeof(char), "Char"},
            {typeof(DateTime), "Date"},
            {typeof(decimal), "Decimal"},
            {typeof(double), "Double"},
            {typeof(int), "Integer"},
            {typeof(long), "Long"},
            {typeof(object), "Object"},
            {typeof(sbyte), "SByte"},
            {typeof(short), "Short"},
            {typeof(float), "Single"},
            {typeof(string), "String"},
            {typeof(uint), "UInteger"},
            {typeof(ulong), "ULong"},
            {typeof(ushort), "UShort"}
        };

        public static string FriendlyName(this Type type, string language) {
            if (type.IsAnonymous()) { return ""; }
            if (!type.IsGenericType) {
                var dict = language == CSharp ? CSKeywordTypes :
                    language == VisualBasic ? VBKeywordTypes :
                    throw new ArgumentException("Invalid language");
                if (dict.TryGetValue(type, out var ret)) { return ret; }
                return type.Name;
            }
            if (type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                return type.UnderlyingIfNullable().FriendlyName(language) + "?";
            }
            if (type.IsGenericParameter) { return type.Name; }
            var parts = type.GetGenericArguments().Joined(", ", t => t.FriendlyName(language));
            var backtickIndex = type.Name.IndexOf('`');
            var nongenericName = type.Name.Substring(0, backtickIndex);
            if (language == CSharp) {
                return $"{nongenericName}<{parts}>";
            } else if (language == VisualBasic) {
                return $"{nongenericName}(Of {parts})";
            } else {
                throw new NotImplementedException("Invalid language");
            }
        }
    }
}
