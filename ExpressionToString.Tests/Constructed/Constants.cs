using System;
using Xunit;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class Constants {
        [Fact]
        public void Random() => BuildAssert(
            Constant(new Random()), 
            "#Random", 
            "#Random"
        );

        [Fact]
        public void ValueTuple() => BuildAssert(
            Constant(("abcd", 5)), 
            "(\"abcd\", 5)",
            "(\"abcd\", 5)"
        );

        [Fact]
        public void OldTuple() => BuildAssert(
            Constant(Tuple.Create("abcd", 5)),
            "(\"abcd\", 5)",
            "(\"abcd\", 5)"
        );

        [Fact]
        public void DateTime() {
            // test rendered VB literal
            var dte = new DateTime(1981, 1, 1);
            BuildAssert(
                Constant(dte), 
                "#DateTime", 
                $"#{dte.ToString()}#"
            );
        }

        [Fact]
        public void TimeSpan() {
            // test rendered VB literal
            var ts = new TimeSpan(5, 4, 3, 2);
            BuildAssert(
                Constant(ts), 
                "#TimeSpan", 
                $"#{ts.ToString()}#"
            );
        }

        [Fact]
        public void Array() => BuildAssert(
            Constant(new object[] { "abcd", 5, new Random() }),
            "new [] { \"abcd\", 5, #Random }",
            "{ \"abcd\", 5, #Random }"
        );
    }
}
