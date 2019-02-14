using ExpressionToString.Util;
using ExpressionTreeVisualizer.Util;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using static ExpressionToString.Util.FormatterNames;

namespace ExpressionTreeVisualizer {
    public partial class VisualizerDataControl {
        private readonly List<DataGrid> endNodeContainers;

        public VisualizerDataControl() {
            InitializeComponent();

            endNodeContainers = endNodes.FindVisualChildren<DataGrid>().ToList();

            Loaded += (s, e) => {
                Options = Options ?? new VisualizerDataOptions();

                tree.SelectionChanged += (s1, e1) => changeSelection(s1);
                source.SelectionChanged += (s1, e1) => changeSelection(s1);
                endNodeContainers.ForEach(x => x.SelectionChanged += (s1, e1) => changeSelection(s1));

                // if we don't do this, the selection will only be visible if the textbox currently has the focus
                source.Focus();
                source.SelectAll();
            };

            cmbLanguages.ItemsSource = new[] { CSharp, VisualBasic };
        }

        private VisualizerData visualizerData => (VisualizerData)DataContext;

        private bool inChangeSelection;
        private void changeSelection(object sender) {
            if (inChangeSelection) { return; }
            inChangeSelection = true;

            // determine the selected ExpressionNodeData based on the sender
            // determine also the distinct EndNodeData from the selected nodes

            // apply selection changed to treeview, textbox, and endnode datagrids
            // if sender is treeview, apply to textbox and datagrids
            // if sender is textbox, apply to treeview, then apply to datagrids
            // if sender is datagrid, apply to other datagrids, apply to treeview, then to textbox

            List<ExpressionNodeData> selected = new List<ExpressionNodeData>();
            if (sender == tree) {
                tree.SelectedItems<KeyValuePair<string, ExpressionNodeData>>().Values().AddRangeTo(selected);
            } else if (sender == source) {
                var singleNode = visualizerData.FindNodeBySpan(source.SelectionStart, source.SelectionLength);
                if (singleNode != null) { selected.Add(singleNode); }
            } else if (sender is DataGrid dg) {
                dg.SelectedItem<KeyValuePair<EndNodeData, List<ExpressionNodeData>>?>()?.Value.AddRangeTo(selected);
            }

            var endNodeData = selected.Select(x => x.EndNodeData).Distinct().SingleOrDefaultExt();
            if (sender != tree) {
                visualizerData.NodeData.ClearSelection();
                selected.ForEach(x => x.IsSelected = true);
            }
            if (sender != source) {
                // Until we implement https://github.com/zspitz/ExpressionToSyntaxNode/issues/25, we can only select one ExpressionNodeData in the source code
                ExpressionNodeData toHighlight;
                if (sender == tree) {
                    // use the last selected from the tree
                    toHighlight = tree.LastSelectedItem<KeyValuePair<string, ExpressionNodeData>?>()?.Value;
                } else {
                    toHighlight = selected.FirstOrDefault();
                }
                if (toHighlight!= null) {
                    source.Select(toHighlight.Span.start, toHighlight.Span.length);
                } else {
                    source.Select(0, 0);
                }
            }
            endNodeContainers.Where(x => x != sender).ForEach(x => x.SelectedValue = endNodeData);

            inChangeSelection = false;
        }

        public IVisualizerObjectProvider ObjectProvider { get; set; }

        private VisualizerDataOptions _options;
        public VisualizerDataOptions Options {
            get => _options;
            set {
                if (value == null || value == _options) { return; }
                _options = value;
                cmbLanguages.DataContext = _options;
                _options.PropertyChanged += (s, e) => LoadDataContext();
                LoadDataContext();
            }
        }
        public void LoadDataContext() =>DataContext = ObjectProvider.TransferObject(Options);
    }
}
