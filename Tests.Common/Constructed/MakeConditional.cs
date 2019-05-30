using System.Linq.Expressions;
using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {

        // note that the NodeType of the expression constructed Conditional factory method can be either typeof(void) or some other type
        // the NodeTypeof IfThen and IfThenElse is always typeof(void)

        [Fact]
        [Trait("Category", Conditionals)]
        public void VoidConditionalWithElse() => RunTest(
            Condition(
                Constant(true),
                writeLineTrue,
                writeLineFalse
            ),
            "if (true) Console.WriteLine(true); else Console.WriteLine(false);",
            "If True Then Console.WriteLine(True) Else Console.WriteLine(False)", 
            @"IfThenElse(
    Constant(true),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(true)
    ),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(false)
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
        public void VoidConditional1WithElse() => RunTest(
            IfThenElse(
                Constant(true),
                writeLineTrue,
                writeLineFalse
            ),
            @"if (true) Console.WriteLine(true); else Console.WriteLine(false);",
            @"If True Then Console.WriteLine(True) Else Console.WriteLine(False)", 
            @"IfThenElse(
    Constant(true),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(true)
    ),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(false)
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
        public void VoidConditionalWithoutElse() => RunTest(
            Condition(
                Constant(true),
                writeLineTrue,
                Empty()
            ), 
            "if (true) Console.WriteLine(true);", 
            "If True Then Console.WriteLine(True)", 
            @"IfThen(
    Constant(true),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(true)
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
        public void VoidConditional1WithoutElse() => RunTest(
            IfThen(
                Constant(true),
                writeLineTrue
            ), 
            "if (true) Console.WriteLine(true);", 
            "If True Then Console.WriteLine(True)", 
            @"IfThen(
    Constant(true),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(true)
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
        public void NonVoidConditionalWithElse() => RunTest(
            Condition(
                Constant(true),
                trueLength,
                falseLength
            ),
            "true ? \"true\".Length : \"false\".Length",
            "If(True, \"true\".Length, \"false\".Length)", 
            @"Condition(
    Constant(true),
    MakeMemberAccess(
        Constant(""true""),
        typeof(string).GetProperty(""Length"")
    ),
    MakeMemberAccess(
        Constant(""false""),
        typeof(string).GetProperty(""Length"")
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
        public void NonVoidConditionalWithoutElse() => RunTest(
            Condition(
                Constant(true),
                trueLength,
                Expression.Default(typeof(int))
            ),
            "true ? \"true\".Length : default(int)",
            "If(True, \"true\".Length, CType(Nothing, Integer))", 
            @"Condition(
    Constant(true),
    MakeMemberAccess(
        Constant(""true""),
        typeof(string).GetProperty(""Length"")
    ),
    Default(
        typeof(int)
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
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
End Block, ""true"".Length, ""false"".Length)", 
            @"Condition(
    Block(
        Constant(true),
        Constant(true)
    ),
    MakeMemberAccess(
        Constant(""true""),
        typeof(string).GetProperty(""Length"")
    ),
    MakeMemberAccess(
        Constant(""false""),
        typeof(string).GetProperty(""Length"")
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
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
Then Console.WriteLine(True)", 
            @"IfThen(
    Block(
        Constant(true),
        Constant(true)
    ),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(true)
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
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
End If", 
            @"IfThen(
    Constant(true),
    Block(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        ),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        )
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
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
End If", 
                @"IfThen(
    Constant(true),
    IfThen(
        Constant(true),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        )
    )
)"
        );

        [Fact]
        [Trait("Category", Conditionals)]
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
End If", 
            @"IfThenElse(
    Constant(true),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(true)
    ),
    IfThen(
        Constant(true),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        )
    )
)"
        );
    }
}
