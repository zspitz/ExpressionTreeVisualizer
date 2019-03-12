using System;
using static System.Linq.Expressions.Expression;
using Xunit;
using static ExpressionToString.Tests.Runners;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Literals {
        [Fact]
        public void True() => BuildAssert(() => true, "() => true", "Function() True");

        [Fact]
        public void False() => BuildAssert(() => false, "() => false", "Function() False");

        [Fact]
        public void Nothing() => BuildAssert(() => (string)null, "() => null", "Function() Nothing");

        [Fact]
        public void Integer() => BuildAssert(() => 5, "() => 5", "Function() 5");

        [Fact]
        public void NonInteger() => BuildAssert(() => 7.32, "() => 7.32", "Function() 7.32");

        [Fact]
        public void String() => BuildAssert(() => "abcd", "() => \"abcd\"", "Function() \"abcd\"");

        [Fact]
        public void EscapedString() => BuildAssert(
            () => "\'\"\\\0\a\b\f\n\r\t\v",
            @"() => ""\'\""\\\0\a\b\f\n\r\t\v""",
            "Function() \"\'\"\"\\\0\a\b\f\n\r\t\v\""
        );

        [Fact]
        public void InterpolatedString() => BuildAssert(
            () => $"{new DateTime(2001, 1, 1)}",
            "() => $\"{new DateTime(2001, 1, 1)}\"",
            "Function() $\"{New DateTime( 2001, 1, 1)}\""
        );
    }
}
