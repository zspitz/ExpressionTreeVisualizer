using ExpressionTreeTestObjects;
using Pather.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static ExpressionToString.FormatterNames;

namespace ExpressionToString.Tests {
    public class TestContainer : IClassFixture<ExpectedDataFixture> {
        public static readonly string[] Formatters = new[] { CSharp, VisualBasic, FactoryMethods, ObjectNotation, TextualTree };

        private ExpectedDataFixture fixture;
        public TestContainer(ExpectedDataFixture fixture) => this.fixture = fixture;

        private (string toString, HashSet<string> paths) GetToString(string formatter, object o) {
            string ret;
            Dictionary<string, (int start, int length)> pathSpans;

            switch (o) {
                case Expression expr:
                    ret = expr.ToString(formatter, out pathSpans);
                    break;
                case MemberBinding mbind:
                    ret = mbind.ToString(formatter, out pathSpans);
                    break;
                case ElementInit init:
                    ret = init.ToString(formatter, out pathSpans);
                    break;
                case SwitchCase switchCase:
                    ret = switchCase.ToString(formatter, out pathSpans);
                    break;
                case CatchBlock catchBlock:
                    ret = catchBlock.ToString(formatter, out pathSpans);
                    break;
                case LabelTarget labelTarget:
                    ret = labelTarget.ToString(formatter, out pathSpans);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return (
                ret,
                pathSpans.Keys.Select(x => {
                    if (formatter != ObjectNotation) {
                        x = x.Replace("_0", "");
                    }
                    return x;
                }).ToHashSet()
            );
        }

        [Theory]
        [MemberData(nameof(TestObjectsData))]
        public void TestMethod(string formatter, string objectName, string category, object o) {
            var expected = fixture.expectedStrings[(formatter, objectName)];
            var (actual, paths) = GetToString(formatter, o);
            // test that the string result is correct
            Assert.Equal(expected, actual);

            var resolver = new Resolver();
            // make sure that all paths resolve to a valid object
            Assert.All(paths, path => Assert.NotNull(resolver.Resolve(o, path)));

            // the paths from the Object Notation formatter serve as a reference for all the other formatters

            if (formatter == ObjectNotation) {
                fixture.expectedPaths[objectName] = paths;
                return;
            }

            if (!fixture.expectedPaths.TryGetValue(objectName, out var expectedPaths)) {
                (_, expectedPaths) = GetToString(ObjectNotation, o);
                fixture.expectedPaths[objectName] = expectedPaths;
            }

            Assert.True(expectedPaths.IsSupersetOf(paths));
        }

        public static TheoryData<string, string, string, object> TestObjectsData => Objects.Get().SelectMany(x => 
            Formatters.Select(formatter => (formatter, $"{x.source}.{x.name}", x.category, x.o))
        ).ToTheoryData();

        [Fact]
        public void CheckMissingObjects() {
            var objectNames = fixture.expectedStrings.GroupBy(x => x.Key.objectName, (key, grp) => (key, grp.Select(x => x.Key.formatter).ToList()));
            foreach (var (name, formatters) in objectNames) {
                var o = Objects.ByName(name);
            }
        }
    }
}
