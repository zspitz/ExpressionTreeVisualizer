using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;
using System.Linq;

namespace ExpressionToString.Tests.Objects {
    static class Dummy {
        internal static void DummyMethod() { }
    }

    partial class CSCompiler {

        [Category(Method)]
        public static readonly Expression InstanceMethod0Arguments = IIFE(() => {
            var s = "";
            return Expr(() => s.ToString());
        });

        [Category(Method)]
        public static readonly Expression StaticMethod0Arguments = Expr(() => Dummy.DummyMethod());

        [Category(Method)]
        public static readonly Expression ExtensionMethod0Arguments = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => lst.Count());
        });

        [Category(Method)]
        public static readonly Expression InstanceMethod1Argument = IIFE(() => {
            var s = "";
            return Expr(() => s.CompareTo(""));
        });

        [Category(Method)]
        public static readonly Expression StaticMethod1Argument = Expr(() => string.Intern(""));

        [Category(Method)]
        public static readonly Expression ExtensionMethod1Argument = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => lst.Take(1));
        });

        [Category(Method)]
        public static readonly Expression InstanceMethod2Arguments = IIFE(() => {
            var s = "";
            return Expr(() => s.IndexOf('a', 2));
        });

        [Category(Method)]
        public static readonly Expression StaticMethod2Arguments = Expr(() => string.Join(",", new[] { "a", "b" }));

        [Category(Method)]
        public static readonly Expression ExtensionMethod2Arguments = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => lst.OrderBy(x => x, StringComparer.OrdinalIgnoreCase));
        });

        [Category(Method)]
        public static readonly Expression StringConcat = Expr((string s1, string s2) => string.Concat(s1, s2));

        [Category(Method)]
        public static readonly Expression MathPow = Expr((double x, double y) => Math.Pow(x, y));
    }
}