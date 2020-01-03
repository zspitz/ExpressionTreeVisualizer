using ExpressionTreeVisualizer.Serialization;
using ExpressionTreeVisualizer.Util;
using System.Collections.Generic;
using System.Linq;
using static ExpressionTreeVisualizer.Serialization.EndNodeTypes;

namespace ExpressionTreeVisualizer.UI {
    public class VisualizerDataViewModel : ViewModelBase<VisualizerData> {
        public ExpressionNodeDataViewModel Root { get; }

        public List<EndNodeGroupViewModel> Constants { get; }
        public List<EndNodeGroupViewModel> Parameters { get; }
        public List<EndNodeGroupViewModel> ClosedVars { get; }
        public List<EndNodeGroupViewModel> Defaults { get; }

        public VisualizerDataViewModel(VisualizerData model) : base(model) {
            var endNodes = new List<ExpressionNodeDataViewModel>();
            Root = new ExpressionNodeDataViewModel(model.Root, endNodes);

            var grouped =
                endNodes
                    .GroupBy(
                        x => x.Model.EndNodeType!.Value,
                        (endNodeType, grp) => (
                            endNodeType,
                            grp.GroupBy(
                                x => x.Model.EndNodeData,
                                (endNodeData, grp1) => new EndNodeGroupViewModel(endNodeData, grp1)
                            )
                        )
                    )
                    .SelectMany(x => x.Item2.Select(y => (x.endNodeType, y)))
                    .ToLookup(x => x.endNodeType, x =>   x.Item2);

            Constants = grouped[Constant].ToList();
            Parameters = grouped[Parameter].ToList();
            ClosedVars = grouped[ClosedVar].ToList();
            Defaults = grouped[Default].ToList();
        }

        public ExpressionNodeDataViewModel FindNodeBySpan(int start, int length) {
            var end = start + length;
            //if (start < NodeData.Span.start || end > NodeData.SpanEnd) { throw new ArgumentOutOfRangeException(); }
            var current = Root;
            while (true) {
                var child = current.Children.SingleOrDefault(x => x.Model.Span.start <= start && x.Model.SpanEnd >= end);
                if (child == null) { break; }
                current = child;
            }
            return current;
        }

        public RelayCommand? OpenNewWindow { get; set; }
    }
}

// TODO "select group" command -- sets IsSelected for group and all its children