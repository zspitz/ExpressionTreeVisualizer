using System.Collections.Generic;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Indexer {
        [Fact]
        public void ArraySingleIndex() {
            var arr = new string[] { };
            RunTest(
                () => arr[5],
                "() => arr[5]",
                "Function() arr(5)"
            );
        }

        [Fact]
        public void ArrayMultipleIndex() {
            var arr = new string[,] { };
            RunTest(
                () => arr[5, 6],
                "() => arr[5, 6]",
                "Function() arr(5, 6)"
            );
        }

        [Fact]
        public void TypeIndexer() {
            var lst = new List<string>();
            RunTest(
                () => lst[3],
                "() => lst[3]",
                "Function() lst(3)"
            );
        }
    }
}
