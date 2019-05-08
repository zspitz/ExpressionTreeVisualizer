using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.FormatterNames;

namespace ExpressionToString.Tests {
    public static class Runner {
        public static void RunTest(object o, string csharp, string vb) {
            string testCSharpCode="";
            string testVBCode="";

            switch (o) {
                case Expression expr:
                    testCSharpCode = expr.ToString(CSharp);
                    testVBCode = expr.ToString(VisualBasic);
                    break;
                case MemberBinding mbind:
                    testCSharpCode = mbind.ToString(CSharp);
                    testVBCode = mbind.ToString(VisualBasic);
                    break;
                case ElementInit init:
                    testCSharpCode = init.ToString(CSharp);
                    testVBCode = init.ToString(VisualBasic);
                    break;
                case SwitchCase switchCase:
                    testCSharpCode = switchCase.ToString(CSharp);
                    testVBCode = switchCase.ToString(VisualBasic);
                    break;
                case CatchBlock catchBlock:
                    testCSharpCode = catchBlock.ToString(CSharp);
                    testVBCode = catchBlock.ToString(VisualBasic);
                    break;
            }


            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }
    }
}
