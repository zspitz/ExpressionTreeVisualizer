using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionTreeToString;
using ExpressionTreeToString.Util;
using static ExpressionTreeToString.Util.Functions;
using static ExpressionTreeVisualizer.Serialization.EndNodeTypes;

namespace ExpressionTreeVisualizer.Serialization.ViewModels {
    [Serializable]
    [Obsolete("Use ExpressionTreeVisualizer.Serialization.VisualizerData or ExpressionTreeVisualizer.UI.VisualizerDataViewModel")]
    public class VisualizerData {
        public string Source { get; set; }
        public VisualizerDataOptions Options { get; set; }
        public ExpressionNodeData NodeData { get; set; }

        [NonSerialized] // the items in this List are grouped and serialized into separate collections
        public List<ExpressionNodeData> CollectedEndNodes;
        [NonSerialized]
        public Dictionary<string, (int start, int length)> PathSpans;

        public Dictionary<EndNodeData, List<ExpressionNodeData>> Constants { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> Parameters { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> ClosedVars { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> Defaults { get; }

        public ExpressionNodeData FindNodeBySpan(int start, int length) {
            var end = start + length;
            //if (start < NodeData.Span.start || end > NodeData.SpanEnd) { throw new ArgumentOutOfRangeException(); }
            var current = NodeData;
            while (true) {
                var child = current.Children.SingleOrDefault(x => x.Span.start <= start && x.SpanEnd >= end);
                if (child == null) { break; }
                current = child;
            }
            return current;
        }

        public VisualizerData(object o, VisualizerDataOptions? options = null) {
            Options = options ?? new VisualizerDataOptions();
            if (!Options.Path.IsNullOrWhitespace()) {
                o = ((Expression)ResolvePath(o, Options.Path)).ExtractValue();
            }
            Source = WriterBase.Create(o, Options.Formatter, Options.Language, out var pathSpans).ToString();
            PathSpans = pathSpans;
            CollectedEndNodes = new List<ExpressionNodeData>();
            NodeData = new ExpressionNodeData(o, ("", ""), this, false);

            // TODO it should be possible to write the following using LINQ
            Constants = new Dictionary<EndNodeData, List<ExpressionNodeData>>();
            Parameters = new Dictionary<EndNodeData, List<ExpressionNodeData>>();
            ClosedVars = new Dictionary<EndNodeData, List<ExpressionNodeData>>();
            Defaults = new Dictionary<EndNodeData, List<ExpressionNodeData>>();

            foreach (var x in CollectedEndNodes) {
                var dict = x.EndNodeType switch
                {
                    Constant => Constants,
                    Parameter => Parameters,
                    ClosedVar => ClosedVars,
                    Default => Defaults,
                    _ => throw new InvalidOperationException(),
                };
                if (!dict.TryGetValue(x.EndNodeData, out var lst)) {
                    lst = new List<ExpressionNodeData>();
                    dict[x.EndNodeData] = lst;
                }
                lst.Add(x);
            }
        }
    }

}
