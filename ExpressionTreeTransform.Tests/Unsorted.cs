using System;
using System.Linq.Expressions;
using Xunit;
using static ExpressionTreeTransform.Tests.Runners;

namespace ExpressionTreeTransform.Tests {
    public class Unsorted {
        [Fact]
        public void ReturnMemberAccess() => BuildAssert(() => "abcd".Length, "() => \"abcd\".Length", "Function() \"abcd\".Length");

        [Fact]
        public void ReturnObjectCreation() => BuildAssert(() => new DateTime(1980, 1, 1), "() => new DateTime(1980, 1, 1)", "Function() New Date(1980, 1, 1)");
    }
}
