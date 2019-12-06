using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ExpressionToString.FormatterNames;
using static System.Linq.Enumerable;

namespace ExpressionToString.Util {
    public static class TypeExtensions {
        public static Type UnderlyingIfNullable(this Type type) => Nullable.GetUnderlyingType(type) ?? type;

        public static bool IsNullable(this Type t, bool orReferenceType = false) {
            if (orReferenceType && !t.IsValueType) { return true; }
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

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

        public static bool InheritsFromOrImplementsAny(this Type type, IEnumerable<Type> types) => types.Any(t => t.IsAssignableFrom(type));

        public static bool IsClosureClass(this Type type) =>
            type != null && type.HasAttribute<CompilerGeneratedAttribute>() && type.Name.ContainsAny("DisplayClass", "Closure$");

        public static bool IsAnonymous(this Type type) =>
            type.HasAttribute<CompilerGeneratedAttribute>() && type.Name.Contains("Anonymous") && type.Name.ContainsAny("<>", "VB$") && !type.InheritsFromOrImplements<Delegate>();

        public static bool IsVBAnonymousDelegate(this Type type) =>
            type.HasAttribute<CompilerGeneratedAttribute>() && type.Name.Contains("VB$AnonymousDelegate");

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
            {typeof(string), "string"},
            {typeof(void), "void" }
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
            if (language.NotIn(CSharp, VisualBasic)) { return type.Name; }

            if (type.IsClosureClass()) {
                return language == CSharp ? "<closure>" : "<Closure>";
            }

            if (type.IsAnonymous()) {
                return "{ " + type.GetProperties().Joined(", ", p => {
                    var name = p.Name;
                    var typename = p.PropertyType.FriendlyName(language);
                    return language == CSharp ?
                        $"{typename} {name}" :
                        $"{name} As {typename}"; // language == VisualBasic 
                }) + " }";
            }

            if (type.IsArray) {
                (string left, string right) =
                    language == CSharp ?
                        ("[", "]") :
                        ("(", ")"); // language == VisualBasic
                var nestedArrayTypes = type.NestedArrayTypes().ToList();
                string arraySpecifiers = nestedArrayTypes.Joined("",
                    (current, _, index) => left + Repeat("", current.GetArrayRank()).Joined() + right
                );
                return nestedArrayTypes.Last().root.FriendlyName(language) + arraySpecifiers;
            }

            if (!type.IsGenericType) {
                // Not sure if such a thing is possible
                if (type.IsVBAnonymousDelegate()) {
                    return "VB$AnonymousDelegate";
                }

                var dict = language == CSharp ?
                    CSKeywordTypes :
                    VBKeywordTypes; // language == VisualBasic
                if (dict.TryGetValue(type, out var ret)) { return ret; }
                return type.Name;
            }

            if (type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                return type.UnderlyingIfNullable().FriendlyName(language) + "?";
            }

            if (type.IsGenericParameter) { return type.Name; }

            var parts = type.GetGenericArguments().Joined(", ", t => t.FriendlyName(language));
            var backtickIndex = type.Name.IndexOf('`');
            var nongenericName = 
                type.IsVBAnonymousDelegate() ? 
                    "VB$AnonymousDelegate" :
                    type.Name.Substring(0, backtickIndex);
            return language == CSharp ?
                $"{nongenericName}<{parts}>" :
                $"{nongenericName}(Of {parts})";
        }

        public static bool IsTupleType(this Type type) {
            if (!type.IsGenericType) { return false; }
            var openType = type.GetGenericTypeDefinition();
            if (openType.In(
                typeof(ValueTuple<>),
                typeof(ValueTuple<,>),
                typeof(ValueTuple<,,>),
                typeof(ValueTuple<,,,>),
                typeof(ValueTuple<,,,,>),
                typeof(ValueTuple<,,,,,>),
                typeof(ValueTuple<,,,,,,>),
                typeof(Tuple<>),
                typeof(Tuple<,>),
                typeof(Tuple<,,>),
                typeof(Tuple<,,,>),
                typeof(Tuple<,,,,>),
                typeof(Tuple<,,,,,>),
                typeof(Tuple<,,,,,,>)
            )) {
                return true;
            }
            return (openType.In(typeof(ValueTuple<,,,,,,,>), typeof(Tuple<,,,,,,,>))
                && type.GetGenericArguments()[7].IsTupleType());
        }

        public static IEnumerable<(Type current, Type root)> NestedArrayTypes(this Type type) {
            var currentType = type;
            while (currentType.IsArray) {
                var nextType = currentType.GetElementType();
                if (nextType.IsArray) {
                    yield return (currentType, null);
                } else {
                    yield return (currentType, nextType);
                    break;
                }
                currentType = nextType;
            }
        }

        public static IEnumerable<T> GetAttributes<T>(this Type type, bool inherit) where T : Attribute =>
            type.GetCustomAttributes(typeof(T), inherit).Cast<T>();

        public static PropertyInfo[] GetIndexers(this Type type, bool inherit) {
            var memberName = type.GetAttributes<DefaultMemberAttribute>(inherit).FirstOrDefault()?.MemberName;
            if (memberName == null) { return new PropertyInfo[] { }; }
            return type.GetProperties().Where(x => x.Name == memberName).ToArray();
        }

        // https://stackoverflow.com/a/55244482
        /// <summary>Returns T for T[] and types that implement IEnumerable&lt;T&gt;</summary>
        public static Type ItemType(this Type type) {
            if (type.IsArray) {
                return type.GetElementType();
            }

            // type is IEnumerable<T>;
            if (ImplIEnumT(type)) {
                return type.GetGenericArguments().First();
            }

            // type implements/extends IEnumerable<T>;
            var enumType = type.GetInterfaces().Where(ImplIEnumT).Select(t => t.GetGenericArguments().First()).FirstOrDefault();
            if (enumType != null) {
                return enumType;
            }

            // type is IEnumerable
            if (IsIEnum(type) || type.GetInterfaces().Any(IsIEnum)) {
                return typeof(object);
            }

            return null;

            bool IsIEnum(Type t) => t == typeof(System.Collections.IEnumerable);
            bool ImplIEnumT(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static IEnumerable<Type> BaseTypes(this Type t, bool genericDefinitions = false, bool andSelf = false) {
            if (andSelf) {
                yield return t;
            }
            if (t.IsGenericType && genericDefinitions) {
                yield return t.GetGenericTypeDefinition();
            }

            foreach (var i in t.GetInterfaces()) {
                yield return reduceToGeneric(i);
            }
            if (t.BaseType != null) {
                foreach (var baseType in t.BaseType.BaseTypes(genericDefinitions, true)) {
                    yield return reduceToGeneric(baseType);
                }
            }

            Type reduceToGeneric(Type sourceType) {
                if (sourceType.IsGenericType && genericDefinitions) { return sourceType.GetGenericTypeDefinition(); }
                return sourceType;
            }
        }
    }
}
