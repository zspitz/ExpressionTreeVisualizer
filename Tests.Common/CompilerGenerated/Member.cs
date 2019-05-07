using Xunit;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        public void InstanceMember() {
            var s = "";
            RunTest(
                () => s.Length,
                "() => s.Length",
                "Function() s.Length"
            );
        }

        [Fact]
        public void ClosedVariable() {
            var s = "";
            RunTest(
                () => s,
                "() => s",
                "Function() s"
            );
        }

        [Fact]
        public void StaticMember() => RunTest(
            () => string.Empty,
            "() => string.Empty",
            "Function() String.Empty"
        );

        [Fact(Skip ="Test for nested closure scopes")]
        public void NestedClosedVariable() {
            Assert.False(true);
        }
    }
}
