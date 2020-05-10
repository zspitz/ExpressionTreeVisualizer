using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using ExpressionTreeVisualizer.Serialization;

namespace ExpressionTreeVisualizer {
    public class VisualizerDataObjectSource : Periscope.Debuggee.ObjectSourceBase<Config> {
        public override object GetSerializationModel(object target, Config config) =>
            new VisualizerData(target, config);
    }
}
