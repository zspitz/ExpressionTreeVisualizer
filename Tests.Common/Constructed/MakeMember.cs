using System.Linq;
using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category",Member)]
        public void InstanceMember() => RunTest(
            MakeMemberAccess(Constant(""), typeof(string).GetMember("Length").Single()),
            "\"\".Length",
            "\"\".Length"
        );

        [Fact]
        [Trait("Category", MemberBindings)]
        public void StaticMember() => RunTest(
            MakeMemberAccess(null, typeof(string).GetMember("Empty").Single()),
            "string.Empty",
            "String.Empty"
        );
    }
}
