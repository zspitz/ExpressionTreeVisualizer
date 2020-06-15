using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using ExpressionTreeVisualizer.Serialization;
using Periscope.Debuggee;

namespace ExpressionTreeVisualizer {
    public class VisualizerDataObjectSource : VisualizerObjectSourceBase<object, Config> {
        static VisualizerDataObjectSource() => SubfolderAssemblyResolver.Hook("ExpressionTreeVisualizer");
        public override object GenerateResponse(object target, Config config) => new VisualizerData(target, config);
    }
}
