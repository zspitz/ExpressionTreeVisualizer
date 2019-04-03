using System;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Misc {
        [Fact]
        public void Conditional() => BuildAssert(
            (int i) => i > 10 ? i : i + 10,
            "(int i) => i > 10 ? i : i + 10",
            "Function(i As Integer) If(i > 10, i, i + 10)"
        );

        [Fact]
        public void TypeCheck() {
            object o = "";
            BuildAssert(
                () => o is string,
                "() => o is string",
                "Function() TypeOf o Is String"
            );
        }

        [Fact]
        public void InvocationNoArguments() {
            Func<int> del = () => DateTime.Now.Day;
            BuildAssert(
                () => del(),
                "() => del()",
                "Function() del()"
            );
        }

        [Fact]
        public void InvocationOneArgument() {
            Func<int, int> del = (int i) => DateTime.Now.Day;
            BuildAssert(
                () => del(5),
                "() => del(5)",
                "Function() del(5)"
            );
        }
    }
}
