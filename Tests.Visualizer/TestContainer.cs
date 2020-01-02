using System.Linq;
using Xunit;
using ExpressionTreeTestObjects;
using ExpressionTreeVisualizer.Serialization;

namespace ExpressionTreeVisualizer.Tests {
    public class TestContainer {
        [Theory]
        [MemberData(nameof(TestObjects))]
        public void TestMethod(string objectName, object o) {
            var vd = new VisualizerData(o);
        }

        public static TheoryData<string, object> TestObjects =>
            Objects.Get().Select(x => ($"{x.source}.{x.name}", x.o)).ToTheoryData();
    }
}
