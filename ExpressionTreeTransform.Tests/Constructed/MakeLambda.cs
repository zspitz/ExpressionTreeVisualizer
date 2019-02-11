using System;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;
using static ExpressionTreeTransform.Tests.Globals;
using static ExpressionTreeTransform.Tests.Runners;
using static ExpressionTreeTransform.Util.Functions;
using static System.Linq.Expressions.Expression;

namespace ExpressionTreeTransform.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeLambda {
        MethodInfo writeline0 = GetMethod(() => Console.WriteLine());
        MethodInfo writeline1 = GetMethod(() => Console.WriteLine(""));

        MethodInfo concat = GetMethod(() => string.Concat("", ""));

        ParameterExpression s = Parameter(typeof(string), "s");
        ParameterExpression s1 = Parameter(typeof(string), "s1");
        ParameterExpression s2 = Parameter(typeof(string), "s2");

        [Fact]
        public void NoParametersVoidReturn() => BuildAssert(
            Lambda(Call(writeline0)), 
            "() => Console.WriteLine()", 
            "Sub() Console.WriteLine"
        );

        [Fact]
        public void OneParameterVoidReturn() => BuildAssert(
            Lambda(Call(writeline1, s), s), 
            "(string s) => Console.WriteLine(s)", 
            "Sub(s As String) Console.WriteLine(s)"
        );

        [Fact]
        public void TwoParametersVoidReturn() => BuildAssert(
            Lambda(Call(writeline1, Add(s1, s2, concat)), s1, s2),
            "(string s1, string s2) => Console.WriteLine(s1 + s2)",
            "Sub(s1 As String, s2 As String) Console.WriteLine(s1 + s2)"
        );

        [Fact]
        public void NoParametersNonVoidReturn() => BuildAssert(
            Lambda(Constant("abcd")),
            "() => \"abcd\"",
            "Function() \"abcd\""
        );

        [Fact]
        public void OneParameterNonVoidReturn() => BuildAssert(
            Lambda(s, s),
            "(string s) => s",
            "Function(s As String) s"
        );

        [Fact]
        public void TwoParametersNonVoidReturn() => BuildAssert(
            Lambda(Add(s1, s2, concat), s1, s2),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        );
    }
}
