using ExpressionTreeTransform.Util;
using ExpressionTreeVisualizer.Util;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfAutoGrid;
using static ExpressionTreeVisualizer.EndNodeTypes;

namespace ExpressionTreeVisualizer {
    public partial class VisualizerDataControl : AutoGrid {
        private Dictionary<EndNodeTypes?, DataGrid> endNodeContainers;

        public VisualizerDataControl() {
            InitializeComponent();

            endNodeContainers = new Dictionary<EndNodeTypes?, DataGrid>() {
                [ClosedVar] = dgClosedVars,
                [Parameter] = dgParameters,
                [Constant] = dgConstants
            };

            Loaded += (s, e) => {
                tree.SelectedItemChanged += (s1, e1) => {
                    if (selected == null) {
                        source.Select(0, 0);
                        return;
                    }
                    (int start, int length) = selected.Span;
                    source.Select(start, length);

                    DataGrid container = null;
                    if (selected.EndNodeType != null) { endNodeContainers.TryGetValue(selected.EndNodeType, out container); }
                    agEndNodes.FindVisualChildren<DataGrid>().Where(x => x != container).ForEach(x => x.SelectedItem = null);
                    if (container != null) { container.SelectedItem = selected.EndNodeData; }
                };

                // if we don't do this, the selection will only be visible if the textbox currently has the focus
                source.Focus();
                source.SelectAll();
            };
        }

        private VisualizerData visualizerData => (VisualizerData)DataContext;
        private ExpressionNodeData selected => tree.SelectedItem<KeyValuePair<string, ExpressionNodeData>?>()?.Value;
    }
}
