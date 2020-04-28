using System;
using System.Windows.Input;
using ExpressionTreeVisualizer.Serialization;
using Periscope;

namespace ExpressionTreeVisualizer {
    public partial class VisualizerWindow : VisualizerWindowBase {
        protected override (ViewState<Config>, ViewState<Config>) GetViewStates(object response, ICommand? OpenInNewWindow) {
            if (!(response is VisualizerData vd)) {
                throw new InvalidOperationException("Unrecognized response type; expected VisualizerData.");
            }

            var windowState = new ViewState<Config> (
                new VisualizerDataViewModel(vd, OpenInNewWindow, Periscope.Visualizer.CopyWatchExpression),
                vd.Config
            );
            var settingsContext = new ConfigViewModel(windowState.Config);
            var settingsState = new ViewState<Config>(
                settingsContext,
                settingsContext.Model
            );
            return (windowState, settingsState);
        }

        protected override void TransformConfig(Config config, object parameter) => 
            config.Path = parameter switch {
                ExpressionNodeDataViewModel nodeVM => nodeVM.Model.FullPath,
                _ => throw new ArgumentException("Unrecognized parameter type."),
            };

        public VisualizerWindow() => InitializeComponent();
    }
}
