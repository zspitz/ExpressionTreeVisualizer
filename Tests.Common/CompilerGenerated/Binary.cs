using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Binary)]
        public void Add() {
            double x = 0, y = 0;
            RunTest(
                () => x + y,
                "() => x + y",
                "Function() x + y",
                @"Lambda(
    Add(x, y)
)");
        }

        [Fact]
        [Trait("Category", Binary)]
        public void AddChecked() {
            // TODO wrap in checked block
            double x = 0, y = 0;
            RunTest(
                () => x + y,
                "() => x + y",
                "Function() x + y",
                @"Lambda(
    Add(x, y)
)");
        }

        [Fact]
        [Trait("Category", Binary)]
        public void Divide() {
            double x = 0, y = 0;
            RunTest(
                () => x / y,
                "() => x / y",
                "Function() x / y",
                @"Lambda(
    Divide(x, y)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void Modulo() {
            double x = 0, y = 0;
            RunTest(
                () => x % y, 
                "() => x % y", 
                "Function() x Mod y", 
                @"Lambda(
    Modulo(x, y)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void Multiply() {
            double x = 0, y = 0;
            RunTest(
                () => x * y, 
                "() => x * y", 
                "Function() x * y", 
                @"Lambda(
    Multiply(x, y)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void MultiplyChecked() {
            // TODO wrap in checked block, otherwise this test is exactly the same as Multiply
            double x = 0, y = 0;
            RunTest(
                () => x * y, 
                "() => x * y", 
                "Function() x * y", 
                @"Lambda(
    MultiplyChecked(x, y)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void Subtract() {
            double x = 0, y = 0;
            RunTest(
                () => x - y, 
                "() => x - y", 
                "Function() x - y", 
                @"Lambda(
    Subtract(x, y)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void SubtractChecked() {
            // TODO wrap in checked block, otherwise this test is exactly the same as Subtract
            double x = 0, y = 0;
            RunTest(
                () => x - y, 
                "() => x - y", 
                "Function() x - y", 
                @"Lambda(
    SubtractChecked(x, y)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void AndBitwise() {
            int i = 0, j = 0;
            RunTest(
                () => i & j,
                "() => i & j",
                "Function() i And j",
                @"Lambda(
    And(i, j)
)");
        }

        [Fact]
        [Trait("Category", Binary)]
        public void OrBitwise() {
            int i = 0, j = 0;
            RunTest(
                () => i | j, 
                "() => i | j", 
                "Function() i Or j", 
                @"Lambda(
    Or(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void ExclusiveOrBitwise() {
            int i = 0, j = 0;
            RunTest(
                () => i ^ j,
                "() => i ^ j",
                "Function() i Xor j",
                @"Lambda(
    ExclusiveOr(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void AndLogical() {
            bool b1 = true, b2 = true;
            RunTest(
                () => b1 & b2,
                "() => b1 & b2",
                "Function() b1 And b2",
                @"Lambda(
    And(b1, b2)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void OrLogical() {
            bool b1 = true, b2 = true;
            RunTest(
                () => b1 | b2, 
                "() => b1 | b2", 
                "Function() b1 Or b2", 
                @"Lambda(
    Or(b1, b2)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void ExclusiveOrLogical() {
            bool b1 = true, b2 = true;
            RunTest(
                () => b1 ^ b2,
                "() => b1 ^ b2",
                "Function() b1 Xor b2",
                @"Lambda(
    ExclusiveOr(b1, b2)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void AndAlso() {
            bool b1 = true, b2 = true;
            RunTest(
                () => b1 && b2,
                "() => b1 && b2",
                "Function() b1 AndAlso b2",
                @"Lambda(
    AndAlso(b1, b2)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void OrElse() {
            bool b1 = true, b2 = true;
            RunTest(
                () => b1 || b2, 
                "() => b1 || b2", 
                "Function() b1 OrElse b2", 
                @"Lambda(
    OrElse(b1, b2)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void Equal() {
            int i = 0, j = 0;
            RunTest(
                () => i == j,
                "() => i == j",
                "Function() i = j",
                @"Lambda(
    Equal(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void NotEqual() {
            int i = 0, j = 0;
            RunTest(
                () => i != j, 
                "() => i != j", 
                "Function() i <> j", 
                @"Lambda(
    NotEqual(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void GreaterThanOrEqual() {
            int i = 0, j = 0;
            RunTest(
                () => i >= j,
                "() => i >= j",
                "Function() i >= j",
                @"Lambda(
    GreaterThanOrEqual(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void GreaterThan() {
            int i = 0, j = 0;
            RunTest(
                () => i > j,
                "() => i > j",
                "Function() i > j",
                @"Lambda(
    GreaterThan(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void LessThan() {
            int i = 0, j = 0;
            RunTest(
                () => i < j, 
                "() => i < j", 
                "Function() i < j", 
                @"Lambda(
    LessThan(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void LessThanOrEqual() {
            int i = 0, j = 0;
            RunTest(
                () => i <= j, 
                "() => i <= j", 
                "Function() i <= j", 
                @"Lambda(
    LessThanOrEqual(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void Coalesce() {
            string s1 = null, s2 = null;
            RunTest(
                () => s1 ?? s2,
                "() => s1 ?? s2",
                "Function() If(s1, s2)",
                @"Lambda(
    Coalesce(s1, s2)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void LeftShift() {
            int i = 0, j = 0;
            RunTest(
                () => i << j, 
                "() => i << j", 
                "Function() i << j", 
                @"Lambda(
    LeftShift(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void RightShift() {
            int i = 0, j = 0;
            RunTest(
                () => i >> j, 
                "() => i >> j", 
                "Function() i >> j", 
                @"Lambda(
    RightShift(i, j)
)"
            );
        }

        [Fact]
        [Trait("Category", Binary)]
        public void ArrayIndex() {
            var arr = new string[] { };
            RunTest(
                () => arr[0],
                "() => arr[0]",
                "Function() arr(0)",
                @"Lambda(
    ArrayIndex(arr,
        Constant(0)
    )
)"
            );
        }
    }
}
