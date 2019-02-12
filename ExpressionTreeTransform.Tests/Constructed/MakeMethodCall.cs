using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using static ExpressionTreeTransform.Tests.Globals;
using static ExpressionTreeTransform.Tests.Runners;
using static ExpressionTreeTransform.Util.Functions;
using static System.Linq.Expressions.Expression;

namespace ExpressionTreeTransform.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeMethodCall {
        ParameterExpression s = Parameter(typeof(string), "s");
        ParameterExpression lst = Parameter(typeof(List<string>), "lst");
        ParameterExpression s1 = Parameter(typeof(string), "s1");
        ParameterExpression s2 = Parameter(typeof(string), "s2");

        [Fact]
        public void InstanceMethod0Arguments() => BuildAssert(
            Call(s, GetMethod(() => "".ToString())),
            "s.ToString()",
            "s.ToString"
        );

        [Fact]
        public void StaticMethod0Arguments() => BuildAssert(
            Call(GetMethod(() => Dummy.DummyMethod())),
            "Dummy.DummyMethod()",
            "Dummy.DummyMethod"
        );

        [Fact]
        public void ExtensionMethod0Arguments() => BuildAssert(
            Call(GetMethod(() => ((List<string>)null).Count()), lst),
            "lst.Count()",
            "lst.Count"
        );

        [Fact]
        public void InstanceMethod1Argument() => BuildAssert(
            Call(s, GetMethod(() => "".CompareTo("")), Constant("")),
            "s.CompareTo(\"\")",
            "s.CompareTo(\"\")"
        );

        [Fact]
        public void StaticMethod1Argument() => BuildAssert(
            Call(GetMethod(() => string.Intern("")), Constant("")),
            "string.Intern(\"\")",
            "String.Intern(\"\")"
        );

        [Fact]
        public void ExtensionMethod1Argument() => BuildAssert(
            Call(GetMethod(() => (null as List<string>).Take(1)), lst, Constant(1)),
            "lst.Take(1)",
            "lst.Take(1)"
        );


        [Fact]
        public void InstanceMethod2Arguments() => BuildAssert(
            Call(
                s,
                GetMethod(() => "".Contains('a', StringComparison.OrdinalIgnoreCase)),
                Constant('a'),
                Constant(StringComparison.OrdinalIgnoreCase)
            ),
            "s.Contains('a', StringComparison.OrdinalIgnoreCase)",
            "s.Contains(\"a\"C, StringComparison.OrdinalIgnoreCase)"
        );

        [Fact]
        public void StaticMethod2Arguments() => BuildAssert(
            Call(
                GetMethod(() => string.Join(',', new[] { 'a', 'b' })),
                Constant(','),
                NewArrayInit(typeof(char), Constant('a'), Constant('b'))
            ),
            "string.Join(',', new [] {'a', 'b'})",
            "String.Join(\",\"C, { \"a\"C, \"b\"C })"
        );

        [Fact]
        public void ExtensionMethod2Arguments() {
            var x = Parameter(typeof(string), "x");
            BuildAssert(
                Call(
                    GetMethod(() => (null as List<string>).OrderBy(y => y, StringComparer.OrdinalIgnoreCase)),
                    lst,
                    Lambda(x, x),
                    MakeMemberAccess(null, typeof(StringComparer).GetMember("OrdinalIgnoreCase").Single())
                ),
                "lst.OrderBy((string x) => x, StringComparer.OrdinalIgnoreCase)",
                "lst.OrderBy(Function(x As String) x, StringComparer.OrdinalIgnoreCase)"
            );
        }

        [Fact]
        public void StringConcat() => BuildAssert(
            Call(
                GetMethod(() => string.Concat("", "")),
                s1,
                s2
            ),
            "s1 + s2",
            "s1 + s2"
        );
    }
}
