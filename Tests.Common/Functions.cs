using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
    }
}
