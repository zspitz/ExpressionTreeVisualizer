using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace System {
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
    }
}
