using ExpressionTreeToString;
using ExpressionTreeToString.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeToString.Util.Functions;

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
            Source = WriterBase.Create(o, Config.Formatter, Config.Language, out var pathSpans).ToString();
            Root = new ExpressionNodeData(o, ("", ""), this, pathSpans, false);
        }
    }
}
