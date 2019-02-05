using Xunit;
using static ExpressionTreeTransform.Tests.Runners;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Binary {
        [Fact]
        public void Add() {
            double x = 0, y = 0;
            BuildAssert(() => x + y, "() => x + y", "Function() x + y");
        }

        [Fact]
        public void AddChecked() {
            double x = 0, y = 0;
            BuildAssert(() => x + y, "() => x + y", "Function() x + y");
        }

        [Fact]
        public void Divide() {
            double x = 0, y = 0;
            BuildAssert(() => x / y, "() => x / y", "Function() x / y");
        }

        [Fact]
        public void Modulo() {
            double x = 0, y = 0;
            BuildAssert(() => x % y, "() => x % y", "Function() x Mod y");
        }

        [Fact]
        public void Multiply() {
            double x = 0, y = 0;
            BuildAssert(() => x * y, "() => x * y", "Function() x * y");
        }

        [Fact]
        public void MultiplyChecked() {
            double x = 0, y = 0;
            BuildAssert(() => x * y, "() => x * y", "Function() x * y");
        }

        [Fact]
        public void Subtract() {
            double x = 0, y = 0;
            BuildAssert(() => x - y, "() => x - y", "Function() x - y");
        }

        [Fact]
        public void SubtractChecked() {
            double x = 0, y = 0;
            BuildAssert(() => x - y, "() => x - y", "Function() x - y");
        }

        [Fact]
        public void AndBitwise() {
            int i = 0, j = 0;
            BuildAssert(() => i & j, "() => i & j", "Function() i And j");
        }

        [Fact]
        public void OrBitwise() {
            int i = 0, j = 0;
            BuildAssert(() => i | j, "() => i | j", "Function() i Or j");
        }

        [Fact]
        public void ExclusiveOrBitwise() {
            int i = 0, j = 0;
            BuildAssert(() => i ^ j, "() => i ^ j", "Function() i Xor j");
        }

        [Fact]
        public void AndLogical() {
            bool b1 = true, b2 = true;
            BuildAssert(() => b1 & b2, "() => b1 & b2", "Function() b1 And b2");
        }

        [Fact]
        public void OrLogical() {
            bool b1 = true, b2 = true;
            BuildAssert(() => b1 | b2, "() => b1 | b2", "Function() b1 Or b2");
        }

        [Fact]
        public void ExclusiveOrLogical() {
            bool b1 = true, b2 = true;
            BuildAssert(() => b1 ^ b2, "() => b1 ^ b2", "Function() b1 Xor b2");
        }

        [Fact]
        public void AndAlso() {
            bool b1 = true, b2 = true;
            BuildAssert(() => b1 && b2, "() => b1 && b2", "Function() b1 AndAlso b2");
        }

        [Fact]
        public void OrElse() {
            bool b1 = true, b2 = true;
            BuildAssert(() => b1 || b2, "() => b1 || b2", "Function() b1 OrElse b2");
        }

        [Fact]
        public void Equal() {
            int i = 0, j = 0;
            BuildAssert(() => i == j, "() => i == j", "Function() i = j");
        }

        [Fact]
        public void NotEqual() {
            int i = 0, j = 0;
            BuildAssert(() => i != j, "() => i != j", "Function() i <> j");
        }

        [Fact]
        public void GreaterThanOrEqual() {
            int i = 0, j = 0;
            BuildAssert(() => i >= j, "() => i >= j", "Function() i >= j");
        }

        [Fact]
        public void GreaterThan() {
            int i = 0, j = 0;
            BuildAssert(() => i > j, "() => i > j", "Function() i > j");
        }

        [Fact]
        public void LessThan() {
            int i = 0, j = 0;
            BuildAssert(() => i < j, "() => i < j", "Function() i < j");
        }

        [Fact]
        public void LessThanOrEqual() {
            int i = 0, j = 0;
            BuildAssert(() => i <= j, "() => i <= j", "Function() i <= j");
        }

        [Fact]
        public void Coalesce() {
            string s1 = null, s2 = null;
            BuildAssert(() => s1 ?? s2, "() => s1 ?? s2", "Function() If(s1, s2)");
        }

        [Fact]
        public void LeftShift() {
            int i = 0, j = 0;
            BuildAssert(() => i << j, "() => i << j", "Function() i << j");
        }

        [Fact]
        public void RightShift() {
            int i = 0, j = 0;
            BuildAssert(() => i >> j, "() => i >> j", "Function() i >> j");
        }

        [Fact]
        public void ArrayIndex() {
            var arr = new string[] { };
            BuildAssert(() => arr[0], "() => arr[0]", "Function() arr(0)");
        }
    }
}
