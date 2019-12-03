using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeTestObjects {
    static internal class Functions {
        internal static MethodInfo GetMethod(Expression<Action> expr, params Type[] typeargs) {
            var ret = (expr.Body as MethodCallExpression).Method;
            // TODO handle partially open generic methods
            if (typeargs.Any() && ret.IsGenericMethod) {
                ret = ret.GetGenericMethodDefinition().MakeGenericMethod(typeargs);
            }
            return ret;
        }

        internal static MemberInfo GetMember<T>(Expression<Func<T>> expr) =>
            (expr.Body as MemberExpression).Member;

        internal static Expression Expr<T>(Expression<Func<T>> expr) => expr;
        internal static Expression Expr<T, T1>(Expression<Func<T, T1>> expr) => expr;
        internal static Expression Expr<T, T1, T2>(Expression<Func<T, T1, T2>> expr) => expr;
        internal static Expression Expr(Expression<Action> expr) => expr;
        internal static Expression Expr<T>(Expression<Action<T>> expr) => expr;
        internal static Expression Expr<T, T1>(Expression<Action<T, T1>> expr) => expr;
        internal static T IIFE<T>(Func<T> fn) => fn();

    }
}
