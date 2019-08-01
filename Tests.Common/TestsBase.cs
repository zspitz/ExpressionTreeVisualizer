using ExpressionToString.Tests.Objects;
using ExpressionToString.Util;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Xunit;

namespace ExpressionToString.Tests {
    public abstract class TestsBase {
        [Obsolete] protected void RunTest(Expression<Action> expr, string csharp, string vb, string factoryMethods) => RunTest(expr as Expression, csharp, vb, factoryMethods);

        [Obsolete] protected void RunTest<T>(Expression<Action<T>> expr, string csharp, string vb, string factoryMethods) => RunTest(expr as Expression, csharp, vb, factoryMethods);

        [Obsolete] protected void RunTest<T1, T2>(Expression<Action<T1, T2>> expr, string csharp, string vb, string factoryMethods) => RunTest(expr as Expression, csharp, vb, factoryMethods);

        [Obsolete] protected void RunTest<T>(Expression<Func<T>> expr, string csharp, string vb, string factoryMethods) => RunTest(expr as Expression, csharp, vb, factoryMethods);

        [Obsolete] protected void RunTest<T1, T2>(Expression<Func<T1, T2>> expr, string csharp, string vb, string factoryMethods) => RunTest(expr as Expression, csharp, vb, factoryMethods);

        [Obsolete] protected void RunTest<T1, T2, T3>(Expression<Func<T1, T2, T3>> expr, string csharp, string vb, string factoryMethods) => RunTest(expr as Expression, csharp, vb, factoryMethods);

        [Obsolete] protected abstract void RunTest(object o, string csharp, string vb, string factoryMethods);

        protected abstract void RunTest(object o, string objectName);

        protected void PreRunTest([CallerMemberName] string methodName = null) {
            var t = GetObjectContainerType();
            var o = t.GetField(methodName).GetValue(null);
            var objectName = $"{t.Name}.{methodName}";
            RunTest(o, objectName);
        }

        protected virtual Type GetObjectContainerType() =>
            this is CompilerGeneratedBase ? typeof(CSCompiler) :
            this is ConstructedBase ? typeof(FactoryMethods) :
            throw new InvalidOperationException();
    }
}
