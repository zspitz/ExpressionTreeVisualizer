using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Diagnostics;
using System.Windows;

[assembly: DebuggerVisualizer(typeof(ExpressionTreeVisualizer.Visualizer), typeof(ExpressionTreeVisualizer.VisualizerDataObjectSource), Target = typeof(System.Linq.Expressions.Expression), Description ="Expression Tree Visualizer")]

namespace ExpressionTreeVisualizer {
    public class Visualizer : DialogDebuggerVisualizer {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            if (windowService == null) { throw new ArgumentNullException(nameof(windowService)); }
            if (objectProvider == null) { throw new ArgumentNullException(nameof(objectProvider)); }
            var data = (VisualizerData)objectProvider.GetObject();

            var control = new VisualizerDataControl { DataContext = data };

            var window = new Window {
                Title = "Expression Tree Visualizer",
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = control,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };

            window.ShowDialog();

        }

        /*
         * WPF usercontrol:
         * Expression - dependency property
         * dependency property for each option
         * 
         */
    }
}
