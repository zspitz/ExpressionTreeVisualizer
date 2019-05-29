using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString.Util {
    public static class MethodInfoExtensions {
        public static bool IsIndexerMethod(this MethodInfo mi) =>
            mi.In(
                mi.ReflectedType
                    .GetIndexers(true)
                    .SelectMany(x => new[] { x.GetMethod, x.SetMethod })
            );

        public static bool IsIndexerMethod(this MethodInfo mi, out PropertyInfo pi) {
            var indexerMethods = mi.ReflectedType
                .GetIndexers(true)
                .SelectMany(x => new[] {
                    (x, x.GetMethod),
                    (x, x.SetMethod)
                });
            foreach (var (x, method) in indexerMethods) {
                if (method == mi) {
                    pi = x;
                    return true;
                }
            }
            pi = null;
            return false;
        }
    }
}
