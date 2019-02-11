using System;
using Xunit;
using static ExpressionTreeTransform.Tests.Globals;
using static ExpressionTreeTransform.Tests.Runners;

namespace ExpressionTreeTransform.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Lambda {
        [Fact]
        public void NoParametersVoidReturn() => BuildAssert(
            () => Console.WriteLine(), 
            "() => Console.WriteLine()", 
            "Sub() Console.WriteLine"
        );

        [Fact]
        public void OneParameterVoidReturn() => BuildAssert<string>(
            s => Console.WriteLine(s), 
            "(string s) => Console.WriteLine(s)", 
            "Sub(s As String) Console.WriteLine(s)"
        );

        [Fact]
        public void TwoParametersVoidReturn() => BuildAssert<string, string>(
            (s1, s2) => Console.WriteLine(s1 + s2), 
            "(string s1, string s2) => Console.WriteLine(s1 + s2)", 
            "Sub(s1 As String, s2 As String) Console.WriteLine(s1 + s2)"
        );

        [Fact]
        public void NoParametersNonVoidReturn() => BuildAssert(
            () => "abcd", 
            "() => \"abcd\"", 
            "Function() \"abcd\""
        );

        [Fact]
        public void OneParameterNonVoidReturn() => BuildAssert<string, string>(
            s => s, 
            "(string s) => s", 
            "Function(s As String) s"
        );

        [Fact]
        public void TwoParametersNonVoidReturn() => BuildAssert<string, string, string>(
            (s1, s2) => s1 + s2, 
            "(string s1, string s2) => s1 + s2", 
            "Function(s1 As String, s2 As String) s1 + s2"
        );
    }
}
