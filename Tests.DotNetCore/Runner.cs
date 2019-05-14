using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.FormatterNames;
using Pather.CSharp;

namespace ExpressionToString.Tests {
    public static class Runner {
        public static void RunTest(object o, string csharp, string vb) {
            string testCSharpCode="";
            Dictionary<string, (int start, int length)> csharpPathSpans = null;
            string testVBCode="";
            Dictionary<string, (int start, int length)> vbPathSpans = null;

            switch (o) {
                case Expression expr:
                    testCSharpCode = expr.ToString(CSharp, out csharpPathSpans);
                    testVBCode = expr.ToString(VisualBasic, out vbPathSpans);
                    break;
                case MemberBinding mbind:
                    testCSharpCode = mbind.ToString(CSharp, out csharpPathSpans);
                    testVBCode = mbind.ToString(VisualBasic, out vbPathSpans);
                    break;
                case ElementInit init:
                    testCSharpCode = init.ToString(CSharp, out csharpPathSpans);
                    testVBCode = init.ToString(VisualBasic, out vbPathSpans);
                    break;
                case SwitchCase switchCase:
                    testCSharpCode = switchCase.ToString(CSharp, out csharpPathSpans);
                    testVBCode = switchCase.ToString(VisualBasic, out vbPathSpans);
                    break;
                case CatchBlock catchBlock:
                    testCSharpCode = catchBlock.ToString(CSharp, out csharpPathSpans);
                    testVBCode = catchBlock.ToString(VisualBasic, out vbPathSpans);
                    break;
                case LabelTarget labelTarget:
                    testCSharpCode = labelTarget.ToString(CSharp, out csharpPathSpans);
                    testVBCode = labelTarget.ToString(VisualBasic, out vbPathSpans);
                    break;
            }


            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);

            var csharpPaths = csharpPathSpans.Keys.Select(x => x.Replace("_0", "")).ToHashSet();
            var vbPaths = vbPathSpans.Keys.Select(x => x.Replace("_0", "")).ToHashSet();
            Assert.Equal(csharpPaths, vbPaths);

            var resolver = new Resolver();
            Assert.All(csharpPaths, path => Assert.NotNull(resolver.Resolve(o, path)));
        }
    }
}
