using static ExpressionTreeToString.FormatterNames;

namespace ExpressionTreeVisualizer {
    public partial class SettingsControl {
        public SettingsControl() {
            InitializeComponent();

            cmbFormatters.ItemsSource = new[] { CSharp, VisualBasic, FactoryMethods, ObjectNotation, TextualTree };
            cmbLanguages.ItemsSource = new[] { CSharp, VisualBasic };
        }
    }
}
