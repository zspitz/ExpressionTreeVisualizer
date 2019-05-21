using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.FormatterNames;
using Pather.CSharp;
//using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests {
    public static class Runner {
        public static void RunTest(object o, string csharp, string vb) {
            string testCSharpCode="";
            Dictionary<string, (int start, int length)> csharpPathSpans = null;
            string testVBCode="";
            Dictionary<string, (int start, int length)> vbPathSpans = null;
            string factoryMethods = "";
            Dictionary<string, (int start, int length)> factoryMethodsPathSpans = null;

            switch (o) {
                case Expression expr:
                    testCSharpCode = expr.ToString(CSharp, out csharpPathSpans);
                    testVBCode = expr.ToString(VisualBasic, out vbPathSpans);
                    factoryMethods = expr.ToString(FactoryMethods, out factoryMethodsPathSpans);
                    break;
                case MemberBinding mbind:
                    testCSharpCode = mbind.ToString(CSharp, out csharpPathSpans);
                    testVBCode = mbind.ToString(VisualBasic, out vbPathSpans);
                    factoryMethods = mbind.ToString(FactoryMethods, out factoryMethodsPathSpans);
                    break;
                case ElementInit init:
                    testCSharpCode = init.ToString(CSharp, out csharpPathSpans);
                    testVBCode = init.ToString(VisualBasic, out vbPathSpans);
                    factoryMethods = init.ToString(FactoryMethods, out factoryMethodsPathSpans);
                    break;
                case SwitchCase switchCase:
                    testCSharpCode = switchCase.ToString(CSharp, out csharpPathSpans);
                    testVBCode = switchCase.ToString(VisualBasic, out vbPathSpans);
                    factoryMethods = switchCase.ToString(FactoryMethods, out factoryMethodsPathSpans);
                    break;
                case CatchBlock catchBlock:
                    testCSharpCode = catchBlock.ToString(CSharp, out csharpPathSpans);
                    testVBCode = catchBlock.ToString(VisualBasic, out vbPathSpans);
                    factoryMethods = catchBlock.ToString(FactoryMethods, out factoryMethodsPathSpans);
                    break;
                case LabelTarget labelTarget:
                    testCSharpCode = labelTarget.ToString(CSharp, out csharpPathSpans);
                    testVBCode = labelTarget.ToString(VisualBasic, out vbPathSpans);
                    factoryMethods = labelTarget.ToString(FactoryMethods, out factoryMethodsPathSpans);
                    break;
            }

            // check that the string results are equivalent, for both C# and VB code
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);

            // using factory methods formatter as source for paths; other formatters may skip paths or introduce new onee
            var paths = factoryMethodsPathSpans.Keys.ToHashSet();

            // check that all the paths can resolve against the original object
            var resolver = new Resolver();
            Assert.All(paths, path => Assert.NotNull(resolver.Resolve(o, path)));

            // path segments for introduced paths (i.e. paths that don't actually exist on the object) follow a specific pattern - $"{name}_{index}"
            // remove all such variations from the distinct paths
            // TODO this should probably be a bit more robust, using regular expressions
            var csharpPaths = csharpPathSpans.Keys.Select(x => x.Replace("_0", "")).ToHashSet();
            var vbPaths = vbPathSpans.Keys.Select(x => x.Replace("_0", "")).ToHashSet();
            Assert.True(paths.IsSupersetOf(csharpPaths));
            Assert.True(paths.IsSupersetOf(vbPaths));

            //using (var outfile = File.AppendText(@"C:\Users\Spitz\source\repos\zspitz\ExpressionToString\generated test data.txt")) {
            //    outfile.Write()
            //}
        }
    }
}
