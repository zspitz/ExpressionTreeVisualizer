using ExpressionToString.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        public void MakeArrayIndex() => RunTest(
            ArrayIndex(arr, Constant(0)),
            "arr[0]",
            "arr(0)"
        );

        [Fact]
        public void MakeArrayMultipleIndex() => RunTest(
            ArrayIndex(arr2D, Constant(0), Constant(1)),
            "arr[0, 1]",
            "arr(0, 1)"
        );

        [Fact]
        public void MakeArrayAccess() => RunTest(
            ArrayAccess(arr, Constant(0)),
            "arr[0]",
            "arr(0)"
        );

        [Fact]
        public void InstanceIndexer() => RunTest(
            MakeIndex(
                lstString, listIndexer, new[] { Constant(0) as Expression }
            ),
            "lst[0]",
            "lst(0)"
        );

        [Fact]
        public void PropertyIndexer() => RunTest(
            Property(lstString, listIndexer, Constant(0)),
            "lst[0]",
            "lst(0)"
        );
    }
}
