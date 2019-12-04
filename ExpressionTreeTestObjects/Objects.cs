using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;
using static System.Linq.Enumerable;

namespace ExpressionTreeTestObjects {
    public static class Objects {
        static Objects() {
            var safeTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("ExpressionTreeTestObjects"))
                .SelectMany(x => {
                    var ret = Empty<Type>();
                    try {
                        ret = x.GetTypes();
                    } catch { }
                    return ret;
                }).Where(x => x.HasAttribute<ObjectContainerAttribute>());

            foreach (var t in safeTypes) {
                var source = t.Name;
                t.GetFields(Static | NonPublic).Select(fld => (
                    fld.GetCustomAttribute<CategoryAttribute>()?.Category,
                    source,
                    fld.Name,
                    fld.GetValue(null)
                )).AddRangeTo(_objects);
            }
        }

        private static List<(string category, string source, string name, object o)> _objects = new List<(string category, string source, string name, object o)>();

        public static (string category, string source, string name, object o)[] Get() => _objects.ToArray();
    }
}
