using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;
using System.Linq;

namespace ExpressionTreeTestObjects {
    internal static class Dummy {
        internal static void DummyMethod() { }
    }

    partial class CSCompiler {

        [Category(Method)]
        internal static readonly Expression InstanceMethod0Arguments = IIFE(() => {
            var s = "";
            return Expr(() => s.ToString());
        });

        [Category(Method)]
        internal static readonly Expression StaticMethod0Arguments = Expr(() => Dummy.DummyMethod());

        [Category(Method)]
        internal static readonly Expression ExtensionMethod0Arguments = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => lst.Count());
        });

        [Category(Method)]
        internal static readonly Expression InstanceMethod1Argument = IIFE(() => {
            var s = "";
            return Expr(() => s.CompareTo(""));
        });

        [Category(Method)]
        internal static readonly Expression StaticMethod1Argument = Expr(() => string.Intern(""));

        [Category(Method)]
        internal static readonly Expression ExtensionMethod1Argument = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => lst.Take(1));
        });

        [Category(Method)]
        internal static readonly Expression InstanceMethod2Arguments = IIFE(() => {
            var s = "";
            return Expr(() => s.IndexOf('a', 2));
        });

        [Category(Method)]
        internal static readonly Expression StaticMethod2Arguments = Expr(() => string.Join(",", new[] { "a", "b" }));

        [Category(Method)]
        internal static readonly Expression ExtensionMethod2Arguments = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => lst.OrderBy(x => x, StringComparer.OrdinalIgnoreCase));
        });

        [Category(Method)]
        internal static readonly Expression StringConcat = Expr((string s1, string s2) => string.Concat(s1, s2));

        [Category(Method)]
        internal static readonly Expression MathPow = Expr((double x, double y) => Math.Pow(x, y));
    }
}