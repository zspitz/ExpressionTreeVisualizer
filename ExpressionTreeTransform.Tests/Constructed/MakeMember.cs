using System.Linq;
using Xunit;
using static ExpressionTreeTransform.Tests.Globals;
using static ExpressionTreeTransform.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionTreeTransform.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeMember {
        [Fact]
        public void InstanceMember() => BuildAssert(
            MakeMemberAccess(Constant(""), typeof(string).GetMember("Length").Single()),
            "\"\".Length",
            "\"\".Length"
        );

        [Fact]
        public void StaticMember() => BuildAssert(
            MakeMemberAccess(null, typeof(string).GetMember("Empty").Single()),
            "string.Empty",
            "String.Empty"
        );
    }
}
