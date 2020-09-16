using ExpressionTreeToString;
using System;
using System.Linq.Expressions;
using ZSpitz.Util;
using static ZSpitz.Util.Functions;

namespace ExpressionTreeVisualizer.Serialization {
    [Serializable]
    public class VisualizerData {
        public Config Config { get; set; }
        public string Source { get; set; }
        public ExpressionNodeData Root { get; set; }

        public VisualizerData(object o, Config? config = null) {
            Config = config ?? new Config();
            if (!Config.Path.IsNullOrWhitespace()) {
                o = ((Expression)ResolvePath(o, Config.Path)).ExtractValue();
            }
            Source = Renderers.Invoke(Config.Formatter, o, Config.Language, out var pathSpans);

            var valueExtractor = new ValueExtractor();
            Root = new ExpressionNodeData(o, ("", ""), this, valueExtractor, pathSpans, false);
        }
    }
}
