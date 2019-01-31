using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using System.Linq.Expressions;

namespace ExpressionTreeVisualizer {
    public class VisualizerDataObjectSource : VisualizerObjectSource {
        public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
            var expr = (Expression)target;
            var options = (VisualizerDataOptions)Deserialize(incomingData);
            var visualizerData = new VisualizerData(expr, options);
            Serialize(outgoingData, visualizerData);
        }
    }
}
