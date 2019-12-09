using Xunit;
using static ExpressionTreeTestObjects.Functions;

namespace ExpressionToString.Tests {
    public class Tests {
        [Fact]
        public void EscapedString() {
            var expr = Expr(() => "\'\"\\\0\a\b\f\n\r\t\v");
            Assert.Equal(@"() => ""\'\""\\\0\a\b\f\n\r\t\v""", expr.ToString("C#"));
        }
    }
}
