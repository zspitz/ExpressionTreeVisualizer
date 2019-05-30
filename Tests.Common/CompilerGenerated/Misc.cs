using System;
using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Conditionals)]
        public void Conditional() => RunTest(
            (int i) => i > 10 ? i : i + 10,
            "(int i) => i > 10 ? i : i + 10",
            "Function(i As Integer) If(i > 10, i, i + 10)", 
            @"Lambda(
    Condition(
        GreaterThan(i,
            Constant(10)
        ),
        i,
        Add(i,
            Constant(10)
        )
    ),
    var i = Parameter(
        typeof(int),
        ""i""
    )
)"
        );

        [Fact]
        public void TypeCheck() {
            object o = "";
            RunTest(
                () => o is string,
                "() => o is string",
                "Function() TypeOf o Is String", 
                @"Lambda(
    TypeIs(o,
        typeof(string)
    )
)"
            );
        }

        [Fact]
        [Trait("Category", Invocation)]
        public void InvocationNoArguments() {
            Func<int> del = () => DateTime.Now.Day;
            RunTest(
                () => del(),
                "() => del()",
                "Function() del()", 
                @"Lambda(
    Invoke(del)
)"
            );
        }

        [Fact]
        [Trait("Category", Invocation)]
        public void InvocationOneArgument() {
            Func<int, int> del = (int i) => DateTime.Now.Day;
            RunTest(
                () => del(5),
                "() => del(5)",
                "Function() del(5)",
                @"Lambda(
    Invoke(del, new[] {
        Constant(5)
    })
)"
            );
        }
    }
}
