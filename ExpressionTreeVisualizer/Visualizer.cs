using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Diagnostics;
using System.Windows;

[assembly: DebuggerVisualizer(typeof(ExpressionTreeVisualizer.Visualizer), typeof(ExpressionTreeVisualizer.VisualizerDataObjectSource), Target = typeof(System.Linq.Expressions.Expression), Description ="Expression Tree Visualizer")]

namespace ExpressionTreeVisualizer {
    public class Visualizer : DialogDebuggerVisualizer {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            if (windowService == null) { throw new ArgumentNullException(nameof(windowService)); }

            var data = (VisualizerData)objectProvider.GetObject();

            FrameworkCompatibilityPreferences.AreInactiveSelectionHighlightBrushKeysSupported = false;

            var window = new Window {
                Content = new VisualizerDataControl { DataContext = data },
                Title = "Expression Tree Visualizer",
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };

            window.ShowDialog();
        }
    }
}
