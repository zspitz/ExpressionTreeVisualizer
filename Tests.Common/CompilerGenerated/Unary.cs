using System.Collections.Generic;
using Xunit;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        public void ArrayLength() {
            var arr = new string[] { };
            RunTest(() => arr.Length, "() => arr.Length", "Function() arr.Length");
        }

        [Fact]
        public void Convert() {
            var lst = new List<string>();
            RunTest(() => (object)lst, "() => (object)lst", "Function() CObj(lst)");
        }

        [Fact]
        public void Negate() {
            var i = 1;
            RunTest(() => -i, "() => -i", "Function() -i");
        }

        [Fact]
        public void BitwiseNot() {
            var i = 1;
            RunTest(() => ~i, "() => ~i", "Function() Not i");
        }

        [Fact]
        public void LogicalNot() {
            var b = true;
            RunTest(() => !b, "() => !b", "Function() Not b");
        }

        [Fact]
        public void TypeAs() {
            object o = null;
            RunTest(() => o as string, "() => o as string", "Function() TryCast(o, String)");
        }
    }
}
