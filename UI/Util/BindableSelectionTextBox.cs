using System.Windows;
using System.Windows.Controls;
using static System.Windows.FrameworkPropertyMetadataOptions;
using static ExpressionTreeVisualizer.Util.Functions;

namespace ExpressionTreeVisualizer.Util {
    public class BindableSelectionTextBox : TextBox {
        public static readonly DependencyProperty BindableSelectionStartProperty =
            DPRegister<int, BindableSelectionTextBox>(0, BindsTwoWayByDefault, OnBindableSelectionStartChanged);

        public static readonly DependencyProperty BindableSelectionLengthProperty =
            DPRegister<int, BindableSelectionTextBox>(0, BindsTwoWayByDefault, OnBindableSelectionLengthChanged);

        private bool changeFromUI;

        public BindableSelectionTextBox() : base() {
            SelectionChanged += OnSelectionChanged;
        }

        public int BindableSelectionStart {
            get => (int)GetValue(BindableSelectionStartProperty);
            set => SetValue(BindableSelectionStartProperty, value);
        }

        public int BindableSelectionLength {
            get => (int)GetValue(BindableSelectionLengthProperty);
            set => SetValue(BindableSelectionLengthProperty, value);
        }

        private static void OnBindableSelectionStartChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            var textBox = (dependencyObject as BindableSelectionTextBox)!;

            if (!textBox.changeFromUI) {
                int newValue = (int)args.NewValue;
                textBox.SelectionStart = newValue;
            } else {
                textBox.changeFromUI = false;
            }
        }

        private static void OnBindableSelectionLengthChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            var textBox = (dependencyObject as BindableSelectionTextBox)!;

            if (!textBox.changeFromUI) {
                int newValue = (int)args.NewValue;
                textBox.SelectionLength = newValue;
            } else {
                textBox.changeFromUI = false;
            }
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e) {
            if (BindableSelectionStart != SelectionStart) {
                changeFromUI = true;
                BindableSelectionStart = SelectionStart;
            }

            if (BindableSelectionLength != SelectionLength) {
                changeFromUI = true;
                BindableSelectionLength = SelectionLength;
            }
        }
    }
}
