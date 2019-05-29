using System.Collections.Generic;
using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category",Indexer)]
        public void ArraySingleIndex() {
            var arr = new string[] { };
            RunTest(
                () => arr[5],
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
            var arr = new string[,] { };
            RunTest(
                () => arr[5, 6],
                "() => arr[5, 6]",
                "Function() arr(5, 6)", 
                @"Lambda(
    ArrayIndex(arr, new[] {
        Constant(5),
        Constant(6)
    })
)"
            );
        }

        [Fact]
        [Trait("Category", Indexer)]
        public void TypeIndexer() {
            var lst = new List<string>();
            RunTest(
                () => lst[3],
                "() => lst[3]",
                "Function() lst(3)", 
                @"Lambda(
    Property(lst,
        typeof(List<string>).GetMethod(""Item""),
        new[] {
            Constant(3)
        }
    )
)"
            );
        }
    }
}
