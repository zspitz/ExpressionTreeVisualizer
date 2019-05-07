using System;
using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category",Literal)]
        public void True() => RunTest(() => true, "() => true", "Function() True");

        [Fact]
        [Trait("Category", Literal)]
        public void False() => RunTest(() => false, "() => false", "Function() False");

        [Fact]
        [Trait("Category", Literal)]
        public void Nothing() => RunTest(() => (string)null, "() => null", "Function() Nothing");

        [Fact]
        [Trait("Category", Literal)]
        public void Integer() => RunTest(() => 5, "() => 5", "Function() 5");

        [Fact]
        [Trait("Category", Literal)]
        public void NonInteger() => RunTest(() => 7.32, "() => 7.32", "Function() 7.32");

        [Fact]
        [Trait("Category", Literal)]
        public void String() => RunTest(() => "abcd", "() => \"abcd\"", "Function() \"abcd\"");

        [Fact]
        [Trait("Category", Literal)]
        public void EscapedString() => RunTest(
            () => "\'\"\\\0\a\b\f\n\r\t\v",
            @"() => ""\'\""\\\0\a\b\f\n\r\t\v""",
            "Function() \"\'\"\"\\\0\a\b\f\n\r\t\v\""
        );

        [Fact]
        [Trait("Category", Literal)]
        public void InterpolatedString() => RunTest(
            () => $"{new DateTime(2001, 1, 1)}",
            "() => $\"{(object)new DateTime(2001, 1, 1)}\"",
            "Function() $\"{CObj(New Date(2001, 1, 1))}\""
        );

        [Fact]
        [Trait("Category", Literal)]
        public void Type() => RunTest(
            () => typeof(string),
            "() => typeof(string)",
            "Function() GetType(String)"
        );
    }
}
