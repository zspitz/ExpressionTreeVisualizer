using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    static class Dummy {
        internal static void DummyMethod() { }
    }

    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Method)]
        public void InstanceMethod0Arguments() {
            var s = "";
            RunTest(
                () => s.ToString(),
                "() => s.ToString()",
                "Function() s.ToString"
            );
        }

        [Fact]
        [Trait("Category", Method)]
        public void StaticMethod0Arguments() => RunTest(
            () => Dummy.DummyMethod(),
            "() => Dummy.DummyMethod()",
            "Sub() Dummy.DummyMethod",
            ""
        );

        [Fact]
        [Trait("Category", Method)]
        public void ExtensionMethod0Arguments() {
            var lst = new List<string>();
            RunTest(
                () => lst.Count(),
                "() => lst.Count()",
                "Function() lst.Count"
            );
        }

        [Fact]
        [Trait("Category", Method)]
        public void InstanceMethod1Argument() {
            var s = "";
            RunTest(
                () => s.CompareTo(""),
                "() => s.CompareTo(\"\")",
                "Function() s.CompareTo(\"\")"
            );
        }

        [Fact]
        [Trait("Category", Method)]
        public void StaticMethod1Argument() => RunTest(
            () => string.Intern(""),
            "() => string.Intern(\"\")",
            "Function() String.Intern(\"\")"
        );

        [Fact]
        [Trait("Category", Method)]
        public void ExtensionMethod1Argument() {
            var lst = new List<string>();
            RunTest(
                () => lst.Take(1),
                "() => lst.Take(1)",
                "Function() lst.Take(1)"
            );
        }

        [Fact]
        [Trait("Category", Method)]
        public void InstanceMethod2Arguments() {
            var s = "";
            RunTest(
                () => s.IndexOf('a', 2),
                "() => s.IndexOf('a', 2)",
                "Function() s.IndexOf(\"a\"C, 2)"
            );
        }

        [Fact]
        [Trait("Category", Method)]
        public void StaticMethod2Arguments() => RunTest(
            () => string.Join(",", new[] { "a","b" }),
            "() => string.Join(\",\", new[] { \"a\", \"b\" })",
            "Function() String.Join(\",\", { \"a\", \"b\" })"
        );

        [Fact]
        [Trait("Category", Method)]
        public void ExtensionMethod2Arguments() {
            var lst = new List<string>();
            RunTest(
                () => lst.OrderBy(x=>x, StringComparer.OrdinalIgnoreCase),
                "() => lst.OrderBy((string x) => x, StringComparer.OrdinalIgnoreCase)",
                "Function() lst.OrderBy(Function(x As String) x, StringComparer.OrdinalIgnoreCase)"
            );
        }

        [Fact]
        [Trait("Category", Method)]
        public void StringConcat() => RunTest(
            (string s1, string s2) => string.Concat(s1, s2),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        );

        [Fact]
        [Trait("Category", Method)]
        public void MathPow() => RunTest(
            (double x, double y) => Math.Pow(x, y),
            "(double x, double y) => Math.Pow(x, y)",
            "Function(x As Double, y As Double) x ^ y"
        );
    }
}
