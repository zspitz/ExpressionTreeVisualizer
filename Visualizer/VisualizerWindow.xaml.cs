using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ExpressionTreeVisualizer.Serialization;
using Periscope;

namespace ExpressionTreeVisualizer {
    public partial class VisualizerWindow : VisualizerWindowBase {
        protected override void TransformConfig(Config config, object parameter) => 
            config.Path = parameter switch {
                ExpressionNodeDataViewModel nodeVM => nodeVM.Model.FullPath,
                _ => throw new ArgumentException("Unrecognized parameter type."),
            };

        public VisualizerWindow() => InitializeComponent();

        protected override (object windowContext, object optionsContext, Config config) GetViewState(object response, ICommand? OpenInNewWindow) {
            if (!(response is VisualizerData vd)) {
                throw new InvalidOperationException("Unrecognized response type; expected VisualizerData.");
            }

            return (
                new VisualizerDataViewModel(vd, OpenInNewWindow, Periscope.Visualizer.CopyWatchExpression),
                new ConfigViewModel(vd.Config),
                vd.Config
            );
        }
    }
}
