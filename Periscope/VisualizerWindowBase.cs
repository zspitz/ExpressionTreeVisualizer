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
            where TConfig : ConfigBase<TConfig>, new() {

        private readonly VisualizerWindowChrome chrome; 

        private IVisualizerObjectProvider? objectProvider;
        private TConfig? config;
        private TConfig? settingsConfig;

        public void Initialize(IVisualizerObjectProvider objectProvider, TConfig config) {
            var modified = false;
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

            var (windowState, settingsState) = GetViewStates(response, openInNewWindow);
            config =(TConfig)windowState.Config;
            settingsConfig = ((TConfig)settingsState.Config) ?? config.Clone();
            chrome.optionsPopup.DataContext = settingsState.DataContext;
            DataContext = windowState.DataContext;
        }

        protected abstract (ViewState<TConfig> window, ViewState<TConfig> settings) GetViewStates(object response, ICommand? OpenInNewWindow);
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
                chrome.optionsLink.Click += (s, e) => chrome.optionsPopup.IsOpen = true;

                // https://stackoverflow.com/a/21436273/111794
                chrome.optionsPopup.CustomPopupPlacementCallback += (popupSize, targetSize, offset) => {
                    return new[] {
                        new CustomPopupPlacement() {
                            Point = new Point(targetSize.Width - popupSize.Width, targetSize.Height)
                        }
                    };
                };

                chrome.optionsPopup.Closed += (s, e) => {
                    if (objectProvider is null) { throw new ArgumentNullException("Missing object provider"); }
                    if (config is null) { throw new ArgumentNullException("Missing config"); }
                    if (settingsConfig is null) { throw new ArgumentNullException("Missing settingsConfig"); }

                    if (settingsConfig.NeedsTransferData(config)) {
                        Initialize(objectProvider, settingsConfig);
                    }
                };
            };
        }
    }
}
