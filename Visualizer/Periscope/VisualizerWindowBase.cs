using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using ZSpitz.Util.Wpf;
using Periscope.Debuggee;

namespace Periscope {
    [ContentProperty("MainContent")]
    public abstract class VisualizerWindowBase<TWindow, TConfig> : Window 
            where TWindow : VisualizerWindowBase<TWindow, TConfig>, new()
            where TConfig : ConfigBase<TConfig> {

        private readonly VisualizerWindowChrome chrome; 

        private IVisualizerObjectProvider? objectProvider;
        private TConfig? config;

        public void Initialize(IVisualizerObjectProvider objectProvider, TConfig config) => Initialize(objectProvider, config, false);

        private void Initialize(IVisualizerObjectProvider objectProvider, TConfig config, bool isConfigModified) {
            var modified = isConfigModified;
            if (this.objectProvider != objectProvider) {
                this.objectProvider = objectProvider;
                modified = true;
            }
            if (this.config != config) {
                this.config = config;
                modified = true;
            }
            if (!modified || config is null || objectProvider is null) { return; }

            if (config is null || objectProvider is null) { return; }
            var response = objectProvider.TransferObject(config);
            if (response is null) {
                throw new InvalidOperationException("Unspecified error while serializing/deserializing");
            }

            object windowContext;
            object optionsContext;
            (windowContext, optionsContext, this.config) = GetViewState(response, openInNewWindow);

            Persistence.Write(this.config, Visualizer.ConfigKey);

            chrome.optionsPopup.DataContext = optionsContext;
            DataContext = windowContext;
        }

        protected abstract (object windowContext, object optionsContext, TConfig config) GetViewState(object response, ICommand? OpenInNewWindow);
        protected abstract void TransformConfig(TConfig config, object parameter);

        private class _OpenInNewWindow : ICommand {
            private readonly TWindow window;
            public _OpenInNewWindow(TWindow window) => this.window = window;
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) {
                if (window.objectProvider is null) { throw new ArgumentNullException("Missing object provider"); }
                if (window.config is null) { throw new ArgumentNullException("Missing config."); }

                var newConfig = window.config.Clone();
                window.TransformConfig(newConfig, parameter);
                var newWindow = new TWindow();
                newWindow.Initialize(window.objectProvider, newConfig);
                newWindow.ShowDialog();
            }

            public event EventHandler? CanExecuteChanged;
        }

        private ICommand openInNewWindow;

        public UIElement MainContent { 
            get => chrome.mainContent.Child;
            set => chrome.mainContent.Child = value; 
        }
        public UIElement OptionsPopup {
            get => chrome.optionsBorder.Child;
            set => chrome.optionsBorder.Child = value; 
        }

        public VisualizerWindowBase() {
            openInNewWindow = new _OpenInNewWindow((TWindow)this);

            SizeToContent = SizeToContent.WidthAndHeight;

            chrome = new VisualizerWindowChrome();
            Content = chrome;

            // if we could find out which is the current monitor, that would be better
            var workingAreas = Monitor.AllMonitors.Select(x => x.WorkingArea).ToList();
            MaxWidth = workingAreas.Min(x => x.Width) * .90;
            MaxHeight = workingAreas.Min(x => x.Height) * .90;

            PreviewKeyDown += (s, e) => {
                if (e.Key == Key.Escape) { Close(); }
            };

            Loaded += (s, e) => {
                TConfig? _baseline = null;

                void popupOpenHandler(object s, EventArgs e) {
                    if (config is null) { throw new ArgumentNullException(nameof(config)); }
                    _baseline = config.Clone();
                }

                void popupClosedHandler(object s, EventArgs e) {
                    if (objectProvider is null) { throw new ArgumentNullException(nameof(objectProvider)); }
                    if (config is null) { throw new ArgumentNullException(nameof(config)); }
                    if (_baseline is null) { throw new ArgumentNullException(nameof(_baseline)); }

                    var configState = config.Diff(_baseline);
                    if (configState.HasFlag(ConfigDiffStates.NeedsTransfer)) {
                        Initialize(objectProvider, config, true);
                    } else if (configState.HasFlag(ConfigDiffStates.NeedsWrite)) {
                        // only "else if", because Iniitialize writes the config on its own
                        Persistence.Write(config, Visualizer.ConfigKey);
                    }
                    _baseline = null;
                }

                chrome.optionsPopup.Opened += popupOpenHandler;
                chrome.optionsPopup.Closed += popupClosedHandler;
                chrome.aboutPopup.Opened += popupOpenHandler;
                chrome.aboutPopup.Closed += popupClosedHandler;

                Unloaded += (s, e) => {
                    if (config is null) { return; }
                    Persistence.Write(config, Visualizer.ConfigKey);
                };
            };
        }
    }
}
