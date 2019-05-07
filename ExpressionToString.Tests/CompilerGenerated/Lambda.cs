using System;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Lambda {
        [Fact]
        public void NoParametersVoidReturn() => RunTest(
            () => Console.WriteLine(), 
            "() => Console.WriteLine()", 
            "Sub() Console.WriteLine"
        );

        [Fact]
        public void OneParameterVoidReturn() => RunTest<string>(
            s => Console.WriteLine(s), 
            "(string s) => Console.WriteLine(s)", 
            "Sub(s As String) Console.WriteLine(s)"
        );

        [Fact]
        public void TwoParametersVoidReturn() => RunTest<string, string>(
            (s1, s2) => Console.WriteLine(s1 + s2), 
            "(string s1, string s2) => Console.WriteLine(s1 + s2)", 
            "Sub(s1 As String, s2 As String) Console.WriteLine(s1 + s2)"
        );

        [Fact]
        public void NoParametersNonVoidReturn() => RunTest(
            () => "abcd", 
            "() => \"abcd\"", 
            "Function() \"abcd\""
        );

        [Fact]
        public void OneParameterNonVoidReturn() => RunTest<string, string>(
            s => s, 
            "(string s) => s", 
            "Function(s As String) s"
        );

        [Fact]
        public void TwoParametersNonVoidReturn() => RunTest<string, string, string>(
            (s1, s2) => s1 + s2, 
            "(string s1, string s2) => s1 + s2", 
            "Function(s1 As String, s2 As String) s1 + s2"
        );
    }
}
