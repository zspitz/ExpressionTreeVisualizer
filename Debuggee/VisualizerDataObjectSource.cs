using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using ExpressionTreeVisualizer.Serialization;
using PostSharp.Community.Packer;

[assembly:Packer]
namespace ExpressionTreeVisualizer {
    public class VisualizerDataObjectSource : VisualizerObjectSource {
        public override void GetData(object target, Stream outgoingData) =>
            Serialize(outgoingData, "");

        public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
            var config = (Config)Deserialize(incomingData);
            var serializationModel = new VisualizerData(target, config);
            Serialize(outgoingData, serializationModel);
        }
    }
}
