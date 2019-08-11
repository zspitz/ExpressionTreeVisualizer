using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.BindingFlags;
using ExpressionToString.Util;

namespace ExpressionToString.Tests {
    public static class Functions {
        public static Expression Expr<T>(Expression<Func<T>> expr) => expr;
        public static Expression Expr<T, T1>(Expression<Func<T, T1>> expr) => expr;
        public static Expression Expr<T, T1, T2>(Expression<Func<T, T1, T2>> expr) => expr;
        public static Expression Expr(Expression<Action> expr) => expr;
        public static Expression Expr<T>(Expression<Action<T>> expr) => expr;
        public static Expression Expr<T, T1>(Expression<Action<T, T1>> expr) => expr;
        public static T IIFE<T>(Func<T> fn) => fn();
        public static string GetFullFilename(string filename) {
            string executable = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(executable), filename));
        }

        private static (object o, string objectName, string category)[] typeSelector(Type t) =>
            t.GetFields().Select(fld => (
                fld.GetValue(null),
                $"{t.Name}.{fld.Name}",
                fld.GetCustomAttribute<Objects.CategoryAttribute>()?.Category
            )).ToArray();

        private static Dictionary<Type, (object o, string objectName, string category)[]> _objectsData = new[] {
            typeof(Objects.CSCompiler),
            typeof(Objects.FactoryMethods)
        }.Select(x => (x, typeSelector(x))).ToDictionary();

        public static void RegisterTestObjectContainer(Type t) {
            if (_objectsData == null) { throw new Exception(); }
            if (_objectsData.ContainsKey(t)) { return; }
            _objectsData.Add(t, typeSelector(t));
        }

        public static (object o, string objectName, string category)[] GetObjects() => _objectsData.Values.SelectMany().ToArray();
    }
}
