using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeTestObjects {
    internal static class Extensions {
        internal static IEnumerable<T> GetAttributes<T>(this Type type, bool inherit) where T : Attribute =>
            type.GetCustomAttributes(typeof(T), inherit).Cast<T>();

        internal static PropertyInfo[] GetIndexers(this Type type, bool inherit) {
            var memberName = type.GetAttributes<DefaultMemberAttribute>(inherit).FirstOrDefault()?.MemberName;
            if (memberName == null) { return new PropertyInfo[] { }; }
            return type.GetProperties().Where(x => x.Name == memberName).ToArray();
        }
    }
}
