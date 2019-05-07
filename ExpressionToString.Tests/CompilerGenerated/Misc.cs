using System;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Misc {
        [Fact]
        public void Conditional() => RunTest(
            (int i) => i > 10 ? i : i + 10,
            "(int i) => i > 10 ? i : i + 10",
            "Function(i As Integer) If(i > 10, i, i + 10)"
        );

        [Fact]
        public void TypeCheck() {
            object o = "";
            RunTest(
                () => o is string,
                "() => o is string",
                "Function() TypeOf o Is String"
            );
        }

        [Fact]
        public void InvocationNoArguments() {
            Func<int> del = () => DateTime.Now.Day;
            RunTest(
                () => del(),
                "() => del()",
                "Function() del()"
            );
        }

        [Fact]
        public void InvocationOneArgument() {
            Func<int, int> del = (int i) => DateTime.Now.Day;
            RunTest(
                () => del(5),
                "() => del(5)",
                "Function() del(5)"
            );
        }
    }
}
