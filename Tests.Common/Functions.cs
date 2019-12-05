using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public static (object o, string objectName, string category)[] GetObjects() => 
            ExpressionTreeTestObjects.Objects.Get().SelectT((category, source, name, o) => (o, $"{source}.{name}", category))
            .ToArray();
    }
}
