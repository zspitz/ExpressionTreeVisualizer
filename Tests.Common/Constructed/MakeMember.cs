using System.Linq;
using Xunit;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
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
