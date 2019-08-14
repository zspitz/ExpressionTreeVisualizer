using ExpressionToString.Tests.Objects;
using ExpressionToString.Util;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Xunit;

namespace ExpressionToString.Tests {
    public abstract class TestsBase {
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
