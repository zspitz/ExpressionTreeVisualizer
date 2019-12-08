using ExpressionTreeVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ExpressionTreeTestObjects;
using ExpressionToString.Tests;

namespace ExpressionToString.Tests.Visualizer {
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
