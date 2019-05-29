using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Constants)]
        public void Random() => RunTest(
            Constant(new Random()), 
            "#Random", 
            "#Random", 
            "Constant(#Random)"
        );

        [Fact]
        [Trait("Category", Constants)]
        public void ValueTuple() => RunTest(
            Constant(("abcd", 5)), 
            "(\"abcd\", 5)",
            "(\"abcd\", 5)", 
            @"Constant((""abcd"", 5))"
        );

        [Fact]
        [Trait("Category", Constants)]
        public void OldTuple() => RunTest(
            Constant(Tuple.Create("abcd", 5)),
            "(\"abcd\", 5)",
            "(\"abcd\", 5)", 
            @"Constant((""abcd"", 5))"
        );

        [Fact]
        [Trait("Category", Constants)]
        public void DateTime() {
            // test rendered VB literal
            var dte = new DateTime(1981, 1, 1);
            RunTest(
                Constant(dte), 
                "#DateTime", 
                $"#{dte.ToString()}#", 
                "Constant(#DateTime)"
            );
        }

        [Fact]
        [Trait("Category", Constants)]
        public void TimeSpan() {
            // test rendered VB literal
            var ts = new TimeSpan(5, 4, 3, 2);
            RunTest(
                Constant(ts), 
                "#TimeSpan", 
                $"#{ts.ToString()}#", 
                "Constant(#TimeSpan)"
            );
        }

        [Fact]
        [Trait("Category", Constants)]
        public void Array() => RunTest(
            Constant(new object[] { "abcd", 5, new Random() }),
            "new[] { \"abcd\", 5, #Random }",
            "{ \"abcd\", 5, #Random }",
            @"Constant(new[] { ""abcd"", 5, #Random })"
        );

        [Fact]
        [Trait("Category", Constants)]
        public void Type() => RunTest(
            Constant(typeof(string)),
            "typeof(string)",
            "GetType(String)", 
            @"Constant(
    typeof(string)
)"
        );

        [Fact]
        [Trait("Category", Constants)]
        public void DifferentTypeForNodeAndValue() => RunTest(
            Constant(new List<string>(), typeof(IEnumerable)),
            "#List<string>",
            "#List(Of String)",
            @"Constant(#List<string>,
    typeof(IEnumerable)
)"
        );
    }
}
