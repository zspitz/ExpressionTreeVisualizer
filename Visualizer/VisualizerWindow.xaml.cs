using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using static ExpressionTreeToString.FormatterNames;
using Microsoft.VisualStudio.DebuggerVisualizers;
using ExpressionTreeVisualizer.Serialization;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;

namespace ExpressionTreeVisualizer {
    public partial class VisualizerWindow : Window {
        private IVisualizerObjectProvider? _objectProvider;
        public IVisualizerObjectProvider? ObjectProvider {
            set {
                if (value is null || value == _objectProvider) { return; }
                _objectProvider = value;
                LoadDataContext();
            }
        }

        private Config? _config;
        public Config? Config {
            get => _config;
            set {
                if (value is null || value == _config) { return; }
                _config = value;
                LoadDataContext();
            }
        }

        private void LoadDataContext() {
            if (_config is null || _objectProvider is null) { return; }
            if (!(_objectProvider.TransferObject(_config) is VisualizerData response)) {
                throw new InvalidOperationException("Unspecified error while serializing/deserializing");
            }

            optionsPopup.DataContext = new ConfigViewModel(response.Config);
            var vm = new VisualizerDataViewModel(response, OpenInNewWindow, Visualizer.CopyWatchExpression);
            DataContext = vm;
        }

        public Action<Config, object>? ConfigTransformer { get; set; }
        public ICommand? OpenInNewWindow { get; private set; }

        private class _OpenInNewWindow : ICommand {
            private readonly VisualizerWindow window;
            public _OpenInNewWindow(VisualizerWindow window) => this.window = window;
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) {
                if (window.ConfigTransformer is null) { throw new ArgumentNullException("Missing 'ConfigTransformer'."); }
                if (window.Config is null) { throw new ArgumentNullException("Missing 'Config'."); }

                var newConfig = window.Config.Clone();
                window.ConfigTransformer(newConfig, parameter);
                var newWindow = new VisualizerWindow {
                    ObjectProvider = window._objectProvider,
                    Config = newConfig,
                    ConfigTransformer = window.ConfigTransformer
                };
                newWindow.ShowDialog();
            }

            public event EventHandler? CanExecuteChanged;
        }

        public VisualizerWindow() {
            OpenInNewWindow = new _OpenInNewWindow(this);

            InitializeComponent();

            // if we could find out which is the current monitor, that would be better
            var workingAreas = Monitor.AllMonitors.Select(x => x.WorkingArea).ToList();
            MaxWidth = workingAreas.Min(x => x.Width) * .90;
            MaxHeight = workingAreas.Min(x => x.Height) * .90;

            PreviewKeyDown += (s, e) => {
                if (e.Key == Key.Escape) { Close(); }
            };

            cmbFormatters.ItemsSource = new[] { CSharp, VisualBasic, FactoryMethods, ObjectNotation, TextualTree };
            cmbLanguages.ItemsSource = new[] { CSharp, VisualBasic };

            Loaded += (s, e) => {
                optionsLink.Click += (s, e) => optionsPopup.IsOpen = true;

                // https://stackoverflow.com/a/21436273/111794
                optionsPopup.CustomPopupPlacementCallback += (popupSize, targetSize, offset) => {
                    return new[] {
                        new CustomPopupPlacement() {
                            Point = new Point(targetSize.Width - popupSize.Width, targetSize.Height)
                        }
                    };
                };

                optionsPopup.Closed += (s, e) => {
                    var configVM = optionsPopup.DataContext<ConfigViewModel>();
                    if (configVM.IsDirty) {
                        Config = configVM.Model;
                    }
                };
            };
        }
    }
}
