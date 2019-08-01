using System.Collections.Generic;
using Xunit;
using static ExpressionToString.Tests.Categories;
using ExpressionToString.Tests.Objects;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category",Indexer)]
        public void ArraySingleIndex() {
            //var arr = new string[] { };
            RunTest(
                CSCompiler.ArraySingleIndex,
                "() => arr[5]",
                "Function() arr(5)", 
                @"Lambda(
    ArrayIndex(arr,
        Constant(5)
    )
)"
            );
        }

        [Fact]
        [Trait("Category", Indexer)]
        public void ArrayMultipleIndex() {
            //var arr = new string[,] { };
            RunTest(
                CSCompiler.ArrayMultipleIndex,
                "() => arr[5, 6]",
                "Function() arr(5, 6)", 
                @"Lambda(
    ArrayIndex(arr,
        Constant(5),
        Constant(6)
    )
)"
            );
        }

        [Fact]
        [Trait("Category", Indexer)]
        public void TypeIndexer() {
            //var lst = new List<string>();
            RunTest(
                CSCompiler.TypeIndexer,
                "() => lst[3]",
                "Function() lst(3)", 
                @"Lambda(
    Property(lst,
        typeof(List<string>).GetProperty(""Item""),
        Constant(3)
    )
)"
            );
        }
    }
}
