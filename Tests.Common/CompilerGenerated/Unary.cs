using System.Collections.Generic;
using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Unary)]
        public void ArrayLength() {
            var arr = new string[] { };
            RunTest(() => arr.Length, "() => arr.Length", "Function() arr.Length");
        }

        [Fact]
        [Trait("Category", Unary)]
        public void Convert() {
            var lst = new List<string>();
            RunTest(() => (object)lst, "() => (object)lst", "Function() CObj(lst)");
        }

        [Fact]
        [Trait("Category", Unary)]
        public void Negate() {
            var i = 1;
            RunTest(() => -i, "() => -i", "Function() -i");
        }

        [Fact]
        [Trait("Category", Unary)]
        public void BitwiseNot() {
            var i = 1;
            RunTest(() => ~i, "() => ~i", "Function() Not i");
        }

        [Fact]
        [Trait("Category", Unary)]
        public void LogicalNot() {
            var b = true;
            RunTest(() => !b, "() => !b", "Function() Not b");
        }

        [Fact]
        [Trait("Category", Unary)]
        public void TypeAs() {
            object o = null;
            RunTest(() => o as string, "() => o as string", "Function() TryCast(o, String)");
        }
    }
}
