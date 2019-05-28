using System;
using System.Linq.Expressions;

namespace ExpressionToString.Tests {
    public abstract class TestsBase {
        protected void RunTest(Expression<Action> expr, string csharp, string vb, string factoryMethods) => RunTest(expr as Expression, csharp, vb, factoryMethods);
        protected void RunTest<T>(Expression<Action<T>> expr, string csharp, string vb) => RunTest(expr as Expression, csharp, vb, "");
        protected void RunTest<T1, T2>(Expression<Action<T1, T2>> expr, string csharp, string vb) => RunTest(expr as Expression, csharp, vb, "");

        protected void RunTest<T>(Expression<Func<T>> expr, string csharp, string vb) => RunTest(expr as Expression, csharp, vb, "");
        protected void RunTest<T1, T2>(Expression<Func<T1, T2>> expr, string csharp, string vb) => RunTest(expr as Expression, csharp, vb, "");
        protected void RunTest<T1, T2, T3>(Expression<Func<T1, T2, T3>> expr, string csharp, string vb) => RunTest(expr as Expression, csharp, vb, "");

        [Obsolete("Pass factory methods formatter output test data")]
        protected void RunTest(object o, string csharp, string vb) => RunTest(o, csharp, vb, "");

        protected abstract void RunTest(object o, string csharp, string vb, string factoryMethods);
    }
}
