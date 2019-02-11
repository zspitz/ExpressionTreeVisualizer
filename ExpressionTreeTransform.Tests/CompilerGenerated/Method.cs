using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using static ExpressionTreeTransform.Tests.Globals;
using static ExpressionTreeTransform.Tests.Runners;

namespace ExpressionTreeTransform.Tests {
    static class Dummy {
        internal static void DummyMethod() { }
    }

    [Trait("Source", CSharpCompiler)]
    public class Method {
        [Fact]
        public void InstanceMethod0Arguments() {
            var s = "";
            BuildAssert(
                () => s.ToString(),
                "() => s.ToString()",
                "Function() s.ToString"
            );
        }

        [Fact]
        public void StaticMethod0Arguments() {
            BuildAssert(
                () => Dummy.DummyMethod(),
                "() => Dummy.DummyMethod()",
                "Sub() Dummy.DummyMethod"
            );
        }

        [Fact]
        public void ExtensionMethod0Arguments() {
            var lst = new List<string>();
            BuildAssert(
                () => lst.Count(),
                "() => lst.Count()",
                "Function() lst.Count"
            );
        }

        [Fact]
        public void InstanceMethod1Argument() {
            var s = "";
            BuildAssert(
                () => s.CompareTo(""),
                "() => s.CompareTo(\"\")",
                "Function() s.CompareTo(\"\")"
            );
        }

        [Fact]
        public void StaticMethod1Argument() {
            BuildAssert(
                () => string.Intern(""),
                "() => string.Intern(\"\")",
                "Function() String.Intern(\"\")"
            );
        }

        [Fact]
        public void ExtensionMethod1Argument() {
            var lst = new List<string>();
            BuildAssert(
                () => lst.Take(1),
                "() => lst.Take(1)",
                "Function() lst.Take(1)"
            );
        }

        [Fact]
        public void InstanceMethod2Arguments() {
            var s = "";
            BuildAssert(
                () => s.Contains('a', StringComparison.InvariantCultureIgnoreCase),
                "() => s.Contains('a', StringComparison.InvariantCultureIgnoreCase)",
                "Function() s.Contains(\"a\"C, StringComparison.InvariantCultureIgnoreCase)"
            );
        }

        [Fact]
        public void StaticMethod2Arguments() {
            BuildAssert(
                () => string.Join(',', new[] { 'a', 'b' }),
                "() => string.Join(',', new [] {'a', 'b'})",
                "Function() String.Join(\",\"C, { \"a\"C, \"b\"C })"
            );
        }

        [Fact]
        public void ExtensionMethod2Arguments() {
            var lst = new List<string>();
            BuildAssert(
                () => lst.OrderBy(x=>x, StringComparer.OrdinalIgnoreCase),
                "() => lst.OrderBy((string x) => x, StringComparer.OrdinalIgnoreCase)",
                "Function() lst.OrderBy(Function(x As String) x, StringComparer.OrdinalIgnoreCase)"
            );
        }
    }
}
