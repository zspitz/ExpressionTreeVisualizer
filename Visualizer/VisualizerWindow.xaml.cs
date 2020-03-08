using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExpressionTreeVisualizer.Util;
using System.Windows.Controls.Primitives;
using static ExpressionTreeToString.FormatterNames;
using Microsoft.VisualStudio.DebuggerVisualizers;
using ExpressionTreeVisualizer.UI;
using ExpressionTreeVisualizer.Serialization;

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

            var vm = new VisualizerDataViewModel(response);
            vm.OpenNewWindow = new RelayCommand(data => {
                var config = _config.Clone();
                config.Path = (string)data;
                var window = new VisualizerWindow {
                    ObjectProvider = _objectProvider,
                    Config = config
                };
                window.ShowDialog();
            });

            optionsPopup.DataContext = new ConfigViewModel(response.Config);
            DataContext = new VisualizerDataViewModel(response);
        }

        public VisualizerWindow() {
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
