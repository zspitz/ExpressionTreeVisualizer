using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.FormatterNames;
using Pather.CSharp;
using ExpressionToString.Util;
using System;
using static ExpressionToString.Tests.Functions;
using static System.Environment;

namespace ExpressionToString.Tests {
    public static class Runner {
        // TODO How can we make this happen automatically, once the assembly is loaded?
        static Runner() => RegisterTestObjectContainer(typeof(Objects.VBCompiler));

        public static readonly string[] Formatters = new[] { CSharp, VisualBasic, FactoryMethods, ObjectNotation };

        public static void RunTest(object o, string objectName, ExpectedDataFixture allExpected) {
            var actual = Formatters.Select(formatter => {
                string singleResult;
                Dictionary<string, (int start, int length)> pathSpans;
                switch (o) {
                    case Expression expr:
                        singleResult = expr.ToString(formatter, out pathSpans);
                        break;
                    case MemberBinding mbind:
                        singleResult = mbind.ToString(formatter, out pathSpans);
                        break;
                    case ElementInit init:
                        singleResult = init.ToString(formatter, out pathSpans);
                        break;
                    case SwitchCase switchCase:
                        singleResult = switchCase.ToString(formatter, out pathSpans);
                        break;
                    case CatchBlock catchBlock:
                        singleResult = catchBlock.ToString(formatter, out pathSpans);
                        break;
                    case LabelTarget labelTarget:
                        singleResult = labelTarget.ToString(formatter, out pathSpans);
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                var selector =
                    formatter == ObjectNotation ? x => x :
                    (Func<string,string>)(x => x.Replace("_0", ""));

                var paths = pathSpans.Keys.Select(selector).ToHashSet();

                return (formatter, (singleResult, paths));
            }).ToDictionary();

            var expectedPaths = actual[ObjectNotation].paths;

            // check that all the expected paths can resolve against the original object
            var resolver = new Resolver();
            Assert.All(expectedPaths, path => Assert.NotNull(resolver.Resolve(o, path)));

            foreach (var formatter in Formatters) {
                var expected = allExpected[(formatter, objectName)];
                var actualSingle = actual[formatter].singleResult;

                // check that the actual matches the expected
                Assert.Equal(expected, actualSingle);

                if (formatter != FactoryMethods) { // we're using the paths of the FactoryMethodsFormatter as reference paths
                    var actualPaths = actual[formatter].paths;
                    Assert.True(expectedPaths.IsSupersetOf(actualPaths));
                }
            }
        }
    }
}
