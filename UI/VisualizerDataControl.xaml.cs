using ExpressionTreeToString.Util;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExpressionTreeVisualizer.UI;
using static System.Windows.SystemColors;
using ExpressionTreeVisualizer.Serialization;

namespace ExpressionTreeVisualizer {
    public partial class VisualizerDataControl {
        public VisualizerDataControl() {
            InitializeComponent();

            // When a control loses focus, it should look no different from when it had the focus (e.g. selection color)
            Resources[InactiveSelectionHighlightBrushKey] = HighlightBrush;
            Resources[InactiveSelectionHighlightTextBrushKey] = HighlightTextBrush;

            Loaded += (s, e) => {
                // HACK: without the next two lines the selection will only be visible if the textbox currently has the focus
                source.LostFocus += (s1, e1) => e1.Handled = true;
                source.Focus();

                // except if the user makes the window wider, then narrower, the tree doesn't contract
                //tree.LayoutUpdated += (s1, e1) => {
                //    // prevents the whole window from shrinking when nodes are collapsed
                //    if (tree.ActualWidth > tree.MinWidth) { tree.MinWidth = tree.ActualWidth; }
                //    if (tree.ActualHeight > tree.MinHeight) { tree.MinHeight = tree.ActualHeight; }
                //};

            };
        }

        private void HelpContextMenu_Loaded(object sender, RoutedEventArgs e) {
            var menu = (MenuItem)sender;
            var node = (ExpressionNodeData)menu.DataContext;

            if (menu.Items.Any()) { return; }

            var listData = new List<(string header, string url)>();

            if (node.ParentProperty is { }) {
                var (@namespace, typename, propertyname) = node.ParentProperty.Value;
                listData.Add(
                    $"Property: {typename}.{propertyname}",
                    $"{BaseUrl}{new[] { @namespace, typename, propertyname }.Joined(".")}"
                );
            }

            addSeparator();

            if (node.NodeTypesParts is { }) {
                foreach (var (@namespace, typename, membername) in node.NodeTypesParts) {
                    listData.Add(
                        $"Node type: {typename}.{membername}",
                        $"{BaseUrl}{new[] { @namespace, typename }.Joined(".")}#{new[] { @namespace.Replace(".", "_"), typename, membername }.Joined("_")}"
                    );
                }
            }

            addSeparator();

            if (node.BaseTypes is { }) {
                node.BaseTypes.SelectT((@namespace, typename) => (
                    $"Base type: {typename}",
                    $"{BaseUrl}{@namespace}.{typename.Replace("~", "-")}"
                )).AddRangeTo(listData);
            }

            addSeparator();

            if (node.FactoryMethodNames is { }) {
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
    }
}


// TODO don't add context menu entry for new window when not available