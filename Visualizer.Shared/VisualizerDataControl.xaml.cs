using ExpressionToString.Util;
using ExpressionTreeVisualizer.Util;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using static ExpressionToString.FormatterNames;


namespace ExpressionTreeVisualizer {
    public partial class VisualizerDataControl {
        private readonly List<DataGrid> endNodeContainers;

        public VisualizerDataControl() {
            InitializeComponent();

            endNodeContainers = endNodes.FindVisualChildren<DataGrid>().ToList();

            Loaded += (s, e) => {
                // https://stackoverflow.com/a/21436273/111794
                optionsPopup.CustomPopupPlacementCallback += (popupSize, targetSize, offset) => {
                    return new[] {
                        new CustomPopupPlacement() {
                            Point = new Point(targetSize.Width - popupSize.Width, targetSize.Height)
                        }
                    };
                };

                Options = Options ?? new VisualizerDataOptions();

                tree.SelectionChanged += (s1, e1) => changeSelection(s1);
                source.SelectionChanged += (s1, e1) => changeSelection(s1);
                endNodeContainers.ForEach(x => x.SelectionChanged += (s1, e1) => changeSelection(s1));

                // if we don't do this, the selection will only be visible if the textbox currently has the focus
                source.Focus();
                source.SelectAll();

                // except if the user makes the window wider, then narrower, the tree doesn't contract
                //tree.LayoutUpdated += (s1, e1) => {
                //    // prevents the whole window from shrinking when nodes are collapsed
                //    if (tree.ActualWidth > tree.MinWidth) { tree.MinWidth = tree.ActualWidth; }
                //    if (tree.ActualHeight > tree.MinHeight) { tree.MinHeight = tree.ActualHeight; }
                //};

                optionsButton.Click += (s1, e1) => optionsPopup.IsOpen = true;
            };

            cmbFormatters.ItemsSource = new[] { CSharp, VisualBasic, FactoryMethods };
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
                tree.SelectedItems<ExpressionNodeData>().AddRangeTo(selected);
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
                    toHighlight = tree.LastSelectedItem<ExpressionNodeData>();
                } else {
                    toHighlight = selected.FirstOrDefault();
                }
                if (toHighlight != null) {
                    source.Select(toHighlight.Span.start, toHighlight.Span.length);
                } else {
                    source.Select(0, 0);
                }
            }
            endNodeContainers.Where(x => x != sender).ForEach(x => x.SelectedValue = endNodeData);

            inChangeSelection = false;
        }

        private IVisualizerObjectProvider _objectProvider;
        public IVisualizerObjectProvider ObjectProvider {
            get => _objectProvider;
            set {
                if (value == null || value == _objectProvider) { return; }
                _objectProvider = value;
                LoadDataContext();
            }
        }

        private VisualizerDataOptions _options;
        public VisualizerDataOptions Options {
            get => _options;
            set {
                if (value == null || value == _options) { return; }
                _options = value;
                optionsPopup.DataContext = _options;
                _options.PropertyChanged += (s, e) => LoadDataContext();
                LoadDataContext();
            }
        }
        public void LoadDataContext() {
            if (_options == null || ObjectProvider == null) { return; }
            DataContext = ObjectProvider.TransferObject(Options);
        }

        private void HelpContextMenu_Loaded(object sender, RoutedEventArgs e) {
            var menu = (MenuItem)sender;
            var node = (ExpressionNodeData)menu.DataContext;

            if (menu.Items.Any()) { return; }

            var listData = new List<(string header, string url)>();

            if (node.ParentProperty.HasValue) {
                var (@namespace, typename, propertyname) = node.ParentProperty.Value;
                listData.Add(
                    $"Property: {typename}.{propertyname}",
                    $"{BaseUrl}{new[] { @namespace, typename, propertyname }.Joined(".")}"
                );
            }

            addSeparator();

            if (node.NodeTypeParts.HasValue) {
                var (@namespace, typename, membername) = node.NodeTypeParts.Value;
                listData.Add(
                    $"Node type: {typename}.{membername}",
                    $"{BaseUrl}{new[] { @namespace, typename }.Joined(".")}#{new[] { @namespace.Replace(".", "_"), typename, membername }.Joined("_")}"
                );
            }

            addSeparator();

            if (node.BaseTypes != null) {
                node.BaseTypes.SelectT((@namespace, typename) => (
                    $"Base type: {typename}",
                    $"{BaseUrl}{@namespace}.{typename.Replace("~", "-")}"
                )).AddRangeTo(listData);
            }

            addSeparator();

            if (node.FactoryMethodNames != null) {
                node.FactoryMethodNames.Select(methodName => (
                    $"Factory method: {methodName}",
                    $"{BaseUrl}system.linq.expressions.expression.{methodName}"
                )).AddRangeTo(listData);
            }

            if (listData.Any() && listData.Last().header == "---") {
                listData.RemoveLast();
            }

            listData.ForEachT((header, url) => {
                if (header == "---") {
                    menu.Items.Add(new Separator());
                    return;
                }

                var mi = new MenuItem() {
                    Header = header
                };
                mi.Click += (s1, e1) => Process.Start(url);
                menu.Items.Add(mi);
            });

            void addSeparator() {
                if (listData.Any() && listData.Last().header != "---") {
                    listData.Add("---", "");
                }
            }
        }

        private const string BaseUrl = "https://docs.microsoft.com/dotnet/api/";

        private void CopyWatchExpression_Click(object sender, RoutedEventArgs e) {
            if (txbRootExpression.Text.IsNullOrWhitespace()) {
                var dlg = new ExpressionRootPrompt();
                dlg.ShowDialog();
                txbRootExpression.Text = dlg.Expression;
            }

            var node = (ExpressionNodeData)((MenuItem)sender).DataContext;
            Clipboard.SetText(string.Format(node.WatchExpressionFormatString, txbRootExpression.Text));
        }

        private void OpenNewWindow_Click(object sender, RoutedEventArgs e) {
            var options = new VisualizerDataOptions(_options);
            options.Path = ((ExpressionNodeData)((MenuItem)sender).DataContext).FullPath;
            var window = new VisualizerWindow();
            var control = window.Content as VisualizerDataControl;
            control.ObjectProvider = ObjectProvider;
            control.Options = options;
            window.ShowDialog();
        }
    }
}
