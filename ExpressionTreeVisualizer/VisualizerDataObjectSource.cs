using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using System.Linq.Expressions;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeVisualizer {
    class VisualizerDataObjectSource : VisualizerObjectSource {
        public override void GetData(object target, Stream outgoingData) {
            var expr = (Expression)target;
            var visualizerData = new VisualizerData(expr, CSharp);
            base.GetData(visualizerData, outgoingData);
        }
    }
}
