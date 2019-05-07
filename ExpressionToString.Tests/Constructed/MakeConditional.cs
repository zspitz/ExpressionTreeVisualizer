using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeConditional {

        // note that the NodeType of the expression constructed Conditional factory method can be either typeof(void) or some other type
        // the NodeTypeof IfThen and IfThenElse is always typeof(void)

        [Fact]
        public void VoidConditionalWithElse() => RunTest(
            Condition(
                Constant(true),
                writeLineTrue,
                writeLineFalse
            ),
            @"if (true) Console.WriteLine(true); else Console.WriteLine(false);",
            @"If True Then Console.WriteLine(True) Else Console.WriteLine(False)"
        );

        [Fact]
        public void VoidConditional1WithElse() => RunTest(
            IfThenElse(
                Constant(true),
                writeLineTrue,
                writeLineFalse
            ),
            @"if (true) Console.WriteLine(true); else Console.WriteLine(false);",
            @"If True Then Console.WriteLine(True) Else Console.WriteLine(False)"
        );

        [Fact]
        public void VoidConditionalWithoutElse() => RunTest(
            Condition(
                Constant(true),
                writeLineTrue,
                Empty()
            ), 
            @"if (true) Console.WriteLine(true);", 
            @"If True Then Console.WriteLine(True)"
        );

        [Fact]
        public void VoidConditional1WithoutElse() => RunTest(
            IfThen(
                Constant(true),
                writeLineTrue
            ), 
            @"if (true) Console.WriteLine(true);", 
            @"If True Then Console.WriteLine(True)"
        );

        [Fact]
        public void NonVoidConditionalWithElse() => RunTest(
            Condition(
                Constant(true),
                trueLength,
                falseLength
            ),
            "true ? \"true\".Length : \"false\".Length",
            "If(True, \"true\".Length, \"false\".Length)"
        );

        [Fact]
        public void NonVoidConditionalWithoutElse() => RunTest(
            Condition(
                Constant(true),
                trueLength,
                Expression.Default(typeof(int))
            ),
            "true ? \"true\".Length : default(int)",
            "If(True, \"true\".Length, CType(Nothing, Integer))"
        );

        [Fact]
        public void MultilineTestPart() => RunTest(
            Condition(
                Block(Constant(true), Constant(true)),
                trueLength,
                falseLength
            ),
            @"{
    true;
    true;
} ? ""true"".Length : ""false"".Length",
            @"If(Block
    True
    True
End Block, ""true"".Length, ""false"".Length)"
        );

        [Fact]
        public void MultilineTestPart1() => RunTest(
            IfThen(
                Block(Constant(true), Constant(true)),
                writeLineTrue
            ),
            @"if ({
    true;
    true;
}) Console.WriteLine(true);",
            @"If
    True
    True
Then Console.WriteLine(True)"
        );

        [Fact]
        public void MultilineIfTrue() => RunTest(
            IfThen(
                Constant(true),
                Block(writeLineTrue, writeLineTrue)
            ),
            @"if (true) {
    Console.WriteLine(true);
    Console.WriteLine(true);
}",
            @"If True Then
    Console.WriteLine(True)
    Console.WriteLine(True)
End If"
        );

        [Fact]
        public void NestedIfThen() => RunTest(
                IfThen(
                    Constant(true),
                    IfThen(
                        Constant(true),
                        writeLineTrue
                    )
                ),
                @"if (true) if (true) Console.WriteLine(true);",
                @"If True Then
    If True Then Console.WriteLine(True)
End If"
        );

        [Fact]
        public void NestedElse() => RunTest(
            IfThenElse(
                Constant(true),
                writeLineTrue,
                IfThen(
                    Constant(true),
                    writeLineTrue
                )
            ),
            @"if (true) Console.WriteLine(true); else if (true) Console.WriteLine(true);",
            @"If True Then Console.WriteLine(True) Else
    If True Then Console.WriteLine(True)
End If"
        );
    }
}
