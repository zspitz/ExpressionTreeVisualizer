using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.ComponentModel;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;
using System.Windows;
using System.Diagnostics;
using Periscope.Debuggee;

namespace Periscope {
    public class Visualizer : INotifyPropertyChanged {
        public static RelayCommand CopyWatchExpression = new RelayCommand(parameter => {
            if (!(parameter is string formatString)) { throw new ArgumentException("'parameter' is not a string."); }

            var rootExpression = Current?.GetRootExpression();
            if (rootExpression.IsNullOrWhitespace()) { return; }

            Clipboard.SetText(string.Format(formatString, rootExpression));
        });

        public static Visualizer? Current;

        private string? rootExpression;

        public string? RootExpression {
            get => rootExpression;
            set => this.NotifyChanged(ref rootExpression, value, PropertyChanged);
        }

        public string? GetRootExpression() {
            if (rootExpression.IsNullOrWhitespace()) {
                new ExpressionRootPrompt().ShowDialog();
            }
            return rootExpression;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        static Visualizer() => Current = new Visualizer();
    }

    public abstract class VisualizerBase<TWindow, TConfig> : DialogDebuggerVisualizer 
            where TWindow : VisualizerWindowBase<TWindow, TConfig>, new()
            where TConfig : ConfigBase<TConfig>, new() {


        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            if (windowService == null) { throw new ArgumentNullException(nameof(windowService)); }

            // TODO move this to an extension point -- an event, or an overridable protected method
            PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());

            var window = new TWindow();
            window.Initialize(objectProvider, GetInitialConfig());
            window.ShowDialog();
        }

        public abstract TConfig GetInitialConfig();
    }
}
