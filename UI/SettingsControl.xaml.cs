using static ExpressionTreeToString.Renderers;
using static ZSpitz.Util.LanguageNames;

namespace ExpressionTreeVisualizer {
    public partial class SettingsControl {
        public SettingsControl() {
            InitializeComponent();

            cmbFormatters.ItemsSource = RendererKeys;
            cmbLanguages.ItemsSource = new[] { CSharp, VisualBasic };
        }
    }
}
