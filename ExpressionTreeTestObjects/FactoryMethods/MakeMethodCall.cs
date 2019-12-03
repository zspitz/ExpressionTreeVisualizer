using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTestObjects.Globals;
using System.Linq;

namespace ExpressionTreeTestObjects {
    partial class FactoryMethods {
        [Category(Method)]
        internal static readonly Expression InstanceMethod0Arguments = Call(s, GetMethod(() => "".ToString()));

        [Category(Method)]
        internal static readonly Expression StaticMethod0Arguments = Call(GetMethod(() => Dummy.DummyMethod()));

        [Category(Method)]
        internal static readonly Expression ExtensionMethod0Arguments = Call(GetMethod(() => ((List<string>)null).Count()), lstString);

        [Category(Method)]
        internal static readonly Expression InstanceMethod1Argument = Call(s, GetMethod(() => "".CompareTo("")), Constant(""));

        [Category(Method)]
        internal static readonly Expression StaticMethod1Argument = Call(GetMethod(() => string.Intern("")), Constant(""));

        [Category(Method)]
        internal static readonly Expression ExtensionMethod1Argument = Call(GetMethod(() => (null as List<string>).Take(1)), lstString, Constant(1));

        [Category(Method)]
        internal static readonly Expression InstanceMethod2Arguments = Call(
            s,
            GetMethod(() => "".IndexOf('a', 2)),
            Constant('a'),
            Constant(2)
        );

        [Category(Method)]
        internal static readonly Expression StaticMethod2Arguments = Call(
            GetMethod(() => string.Join(",", new[] { "a", "b" })),
            Constant(","),
            NewArrayInit(typeof(string), Constant("a"), Constant("b"))
        );

        [Category(Method)]
        internal static readonly Expression ExtensionMethod2Arguments = IIFE(() => {
            var x = Parameter(typeof(string), "x");
            return Call(
                GetMethod(() => (null as List<string>).OrderBy(y => y, StringComparer.OrdinalIgnoreCase)),
                lstString,
                Lambda(x, x),
                MakeMemberAccess(null, typeof(StringComparer).GetMember("OrdinalIgnoreCase").Single())
            );
        });

        [Category(Method)]
        internal static readonly Expression StringConcat = Call(
            GetMethod(() => string.Concat("", "")),
            s1,
            s2
        );
    }
}
