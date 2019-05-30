using System;
using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category",Literal)]
        public void True() => RunTest(
            () => true, 
            "() => true", 
            "Function() True", 
            @"Lambda(
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Literal)]
        public void False() => RunTest(
            () => false, 
            "() => false", 
            "Function() False", 
            @"Lambda(
    Constant(false)
)"
        );

        [Fact]
        [Trait("Category", Literal)]
        public void Nothing() => RunTest(
            () => (string)null, 
            "() => null", 
            "Function() Nothing", 
            @"Lambda(
    Constant(null,
        typeof(string)
    )
)"
        );

        [Fact]
        [Trait("Category", Literal)]
        public void Integer() => RunTest(
            () => 5, 
            "() => 5", 
            "Function() 5", 
            @"Lambda(
    Constant(5)
)"
        );

        [Fact]
        [Trait("Category", Literal)]
        public void NonInteger() => RunTest(
            () => 7.32, 
            "() => 7.32", 
            "Function() 7.32", 
            @"Lambda(
    Constant(7.32)
)"
        );

        [Fact]
        [Trait("Category", Literal)]
        public void String() => RunTest(
            () => "abcd", 
            "() => \"abcd\"", 
            "Function() \"abcd\"",
            @"Lambda(
    Constant(""abcd"")
)"
        );

        [Fact]
        [Trait("Category", Literal)]
        public void EscapedString() => RunTest(
            () => "\'\"\\\0\a\b\f\n\r\t\v",
            @"() => ""\'\""\\\0\a\b\f\n\r\t\v""",
            "Function() \"\'\"\"\\\0\a\b\f\n\r\t\v\"", 
            @"Lambda(
    Constant(""\'\""\\\0\a\b\f\n\r\t\v"")
)"
        );

        [Fact]
        [Trait("Category", Literal)]
        public void InterpolatedString() => RunTest(
            () => $"{new DateTime(2001, 1, 1)}",
            "() => $\"{(object)new DateTime(2001, 1, 1)}\"",
            "Function() $\"{CObj(New Date(2001, 1, 1))}\"", 
            @"Lambda(
    Call(
        typeof(string).GetMethod(""Format""),
        new[] {
            Constant(""{0}""),
            Convert(
                New(
                    typeof(DateTime).GetConstructor(),
                    Constant(2001),
                    Constant(1),
                    Constant(1)
                ),
                typeof(object)
            )
        }
    )
)"
        );

        [Fact]
        [Trait("Category", Literal)]
        public void Type() => RunTest(
            () => typeof(string),
            "() => typeof(string)",
            "Function() GetType(String)", 
            @"Lambda(
    Constant(
        typeof(string),
        typeof(Type)
    )
)"
        );
    }
}
