using System.Windows;
using System.Windows.Controls.Primitives;

namespace Periscope {
    public partial class VisualizerWindowChrome {
        public VisualizerWindowChrome() {
            InitializeComponent();

            // https://stackoverflow.com/a/21436273/111794
            CustomPopupPlacement[] popupPlacement(Size popupSize, Size targetSize, Point offset) =>
                new[] {
                    new CustomPopupPlacement() {
                        Point = new Point(targetSize.Width - popupSize.Width, targetSize.Height)
                    }
                };

            Loaded += (s, e) => {
                optionsLink.Click += (s, e) => optionsPopup.IsOpen = true;
                aboutLink.Click += (s, e) => aboutPopup.IsOpen = true;

                optionsPopup.CustomPopupPlacementCallback += popupPlacement;
                aboutPopup.CustomPopupPlacementCallback += popupPlacement;
            };

        }
    }
}
