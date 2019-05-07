using System;
using Xunit;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        public void Random() => RunTest(
            Constant(new Random()), 
            "#Random", 
            "#Random"
        );

        [Fact]
        public void ValueTuple() => RunTest(
            Constant(("abcd", 5)), 
            "(\"abcd\", 5)",
            "(\"abcd\", 5)"
        );

        [Fact]
        public void OldTuple() => RunTest(
            Constant(Tuple.Create("abcd", 5)),
            "(\"abcd\", 5)",
            "(\"abcd\", 5)"
        );

        [Fact]
        public void DateTime() {
            // test rendered VB literal
            var dte = new DateTime(1981, 1, 1);
            RunTest(
                Constant(dte), 
                "#DateTime", 
                $"#{dte.ToString()}#"
            );
        }

        [Fact]
        public void TimeSpan() {
            // test rendered VB literal
            var ts = new TimeSpan(5, 4, 3, 2);
            RunTest(
                Constant(ts), 
                "#TimeSpan", 
                $"#{ts.ToString()}#"
            );
        }

        [Fact]
        public void Array() => RunTest(
            Constant(new object[] { "abcd", 5, new Random() }),
            "new [] { \"abcd\", 5, #Random }",
            "{ \"abcd\", 5, #Random }"
        );

        [Fact]
        public void Type() => RunTest(
            Constant(typeof(string)),
            "typeof(string)",
            "GetType(String)"
        );
    }
}
