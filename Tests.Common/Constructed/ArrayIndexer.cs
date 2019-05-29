using ExpressionToString.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Indexer)]
        public void MakeArrayIndex() => RunTest(
            ArrayIndex(arr, Constant(0)),
            "arr[0]",
            "arr(0)",
            @"ArrayIndex(arr,
    Constant(0)
)"
        );

        [Fact]
        [Trait("Category", Indexer)]
        public void MakeArrayMultipleIndex() => RunTest(
            ArrayIndex(arr2D, Constant(0), Constant(1)),
            "arr2d[0, 1]",
            "arr2d(0, 1)", 
            @"ArrayIndex(arr2d, new[] {
    Constant(0),
    Constant(1)
})"
        );

        [Fact]
        [Trait("Category", Indexer)]
        public void MakeArrayAccess() => RunTest(
            ArrayAccess(arr, Constant(0)),
            "arr[0]",
            "arr(0)",
            @"ArrayAccess(arr, new[] {
    Constant(0)
})"
        );

        [Fact]
        [Trait("Category", Indexer)]
        public void InstanceIndexer() => RunTest(
            MakeIndex(
                lstString, listIndexer, new[] { Constant(0) as Expression }
            ),
            "lstString[0]",
            "lstString(0)", 
            @"MakeIndex(lstString,
    typeof(List<string>).GetProperty(""Item""),
    new[] {
        Constant(0)
    }
)"
        );

        [Fact]
        [Trait("Category", Indexer)]
        public void PropertyIndexer() => RunTest(
            Property(lstString, listIndexer, Constant(0)),
            "lst[0]",
            "lst(0)", 
            @"MakeIndex(lstString,
    typeof(List<string>).GetProperty(""Item""),
    new[] {
        Constant(0)
    }
)"
        );
    }
}
