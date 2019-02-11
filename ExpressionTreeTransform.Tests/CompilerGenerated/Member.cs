using System;
using Xunit;
using static ExpressionTreeTransform.Tests.Globals;
using static ExpressionTreeTransform.Tests.Runners;

namespace ExpressionTreeTransform.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Member {
        [Fact]
        public void InstanceMember() {
            var s = "";
            BuildAssert(
                () => s.Length,
                "() => s.Length",
                "Function() s.Length"
            );
        }

        [Fact]
        public void ClosedVariable() {
            var s = "";
            BuildAssert(
                () => s,
                "() => s",
                "Function() s"
            );
        }

        [Fact]
        public void StaticMember() => BuildAssert(
            () => string.Empty,
            "() => string.Empty",
            "Function() String.Empty"
        );
    }
}
