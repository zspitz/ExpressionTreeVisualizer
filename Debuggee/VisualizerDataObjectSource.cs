using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using ExpressionTreeVisualizer.Serialization;

namespace ExpressionTreeVisualizer {
    public class VisualizerDataObjectSource : VisualizerObjectSource {
        public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
            var options = (VisualizerDataOptions)Deserialize(incomingData);
            var visualizerData = new VisualizerData(target, options);
            Serialize(outgoingData, visualizerData);
        }
    }
}
