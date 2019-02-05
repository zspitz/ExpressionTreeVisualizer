using System.Collections.Generic;
using Xunit;
using static ExpressionTreeTransform.Tests.Runners;
using static ExpressionTreeTransform.Tests.Globals;

namespace ExpressionTreeTransform.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Unary {
        [Fact]
        public void ArrayLength() {
            var arr = new string[] { };
            BuildAssert(() => arr.Length, "() => arr.Length", "Function() arr.Length");
        }

        [Fact]
        public void Convert() {
            var lst = new List<string>();
            BuildAssert(() => (object)lst, "() => (object)lst", "Function() CObj(lst)");
        }

        [Fact]
        public void Negate() {
            var i = 1;
            BuildAssert(() => -i, "() => -i", "Function() -i");
        }

        [Fact]
        public void BitwiseNot() {
            var i = 1;
            BuildAssert(() => ~i, "() => ~i", "Function() Not i");
        }

        [Fact]
        public void LogicalNot() {
            var b = true;
            BuildAssert(() => !b, "() => !b", "Function() Not b");
        }

        [Fact]
        public void TypeAs() {
            object o = null;
            BuildAssert(() => o as string, "() => o as string", "Function() TryCast(o, String)");
        }
    }
}
