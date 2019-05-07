using System;
using static System.Linq.Expressions.Expression;
using Xunit;
using static ExpressionToString.Tests.Runners;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Literals {
        [Fact]
        public void True() => RunTest(() => true, "() => true", "Function() True");

        [Fact]
        public void False() => RunTest(() => false, "() => false", "Function() False");

        [Fact]
        public void Nothing() => RunTest(() => (string)null, "() => null", "Function() Nothing");

        [Fact]
        public void Integer() => RunTest(() => 5, "() => 5", "Function() 5");

        [Fact]
        public void NonInteger() => RunTest(() => 7.32, "() => 7.32", "Function() 7.32");

        [Fact]
        public void String() => RunTest(() => "abcd", "() => \"abcd\"", "Function() \"abcd\"");

        [Fact]
        public void EscapedString() => RunTest(
            () => "\'\"\\\0\a\b\f\n\r\t\v",
            @"() => ""\'\""\\\0\a\b\f\n\r\t\v""",
            "Function() \"\'\"\"\\\0\a\b\f\n\r\t\v\""
        );

        [Fact]
        public void InterpolatedString() => RunTest(
            () => $"{new DateTime(2001, 1, 1)}",
            "() => $\"{(object)new DateTime(2001, 1, 1)}\"",
            "Function() $\"{CObj(New Date(2001, 1, 1))}\""
        );

        [Fact]
        public void Type() => RunTest(
            () => typeof(string),
            "() => typeof(string)",
            "Function() GetType(String)"
        );
    }
}
