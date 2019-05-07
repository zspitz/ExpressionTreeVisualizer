using System.Linq;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeMember {
        [Fact]
        public void InstanceMember() => RunTest(
            MakeMemberAccess(Constant(""), typeof(string).GetMember("Length").Single()),
            "\"\".Length",
            "\"\".Length"
        );

        [Fact]
        public void StaticMember() => RunTest(
            MakeMemberAccess(null, typeof(string).GetMember("Empty").Single()),
            "string.Empty",
            "String.Empty"
        );
    }
}
