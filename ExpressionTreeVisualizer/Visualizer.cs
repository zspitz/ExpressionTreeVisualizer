using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Diagnostics;
using System.Windows;

[assembly: DebuggerVisualizer(typeof(ExpressionTreeVisualizer.Visualizer), Target = typeof(System.Linq.Expressions.Expression))]

namespace ExpressionTreeVisualizer {
    public class Visualizer : DialogDebuggerVisualizer {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            if (windowService == null) { throw new ArgumentNullException(nameof(windowService)); }
            if (objectProvider == null) { throw new ArgumentNullException(nameof(objectProvider)); }
            var data = objectProvider.GetObject() as VisualizerData;

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
