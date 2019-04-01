using System.Linq;
using Xunit;
using static ExpressionToString.Tests.Runners;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Util.Functions;
using System.Collections.Generic;
using static System.Reflection.BindingFlags;

namespace ExpressionToString.Tests.Constructed {
    internal class DummyMember {
        internal string Foo { get; set; }
    }

    [Trait("Source", FactoryMethods)]
    public class MemberBind {
        [Fact]
        public void MakeMemberBind() => BuildAssert(
            Bind(
                typeof(DummyMember).GetMember("Foo", Instance | NonPublic).Single(), Constant("abcd")
            ),
            "Foo = \"abcd\"",
            ".Foo = \"abcd\""
        );

        [Fact]
        public void MakeElementInit() => BuildAssert(
            ElementInit(
                GetMethod(() => ((List<string>)null).Add("")),
                Constant("abcd")
            ),
            "\"abcd\"",
            "\"abcd\""
        );

        [Fact]
        public void MakeElementInit2Arguments() => BuildAssert(
            ElementInit(
                GetMethod(() => ((Wrapper)null).Add("", "")),
                Constant("abcd"),
                Constant("efgh")
            ),
            @"{
    ""abcd"",
    ""efgh""
}",
            @"{
    ""abcd"",
    ""efgh""
}"
        );



    }
}
