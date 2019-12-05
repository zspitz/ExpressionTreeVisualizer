using ExpressionToString.Util;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Xunit;
using ExpressionTreeTestObjects;

namespace ExpressionToString.Tests {
    [Obsolete]
    public abstract class TestsBase {
        [Obsolete]
        protected abstract void RunTest(object o, string objectName);

        [Obsolete]
        protected void PreRunTest([CallerMemberName] string methodName = null) {
            var t = GetObjectSource();
            var objectName = $"{GetObjectSource()}.{methodName}";
            var o = Objects.ByName(objectName);
            RunTest(o, objectName);
        }

        [Obsolete]
        protected virtual string GetObjectSource() =>
            this is CompilerGeneratedBase ? "CSCompiler" :
            this is ConstructedBase ? "FactoryMethods" :
            throw new InvalidOperationException();
    }
}
