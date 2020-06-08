using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Periscope {
    public partial class ExpressionRootPrompt {
        public string? Expression { get; private set; }

        public ExpressionRootPrompt() {
            InitializeComponent();

            link.RequestNavigate += (s, e) => Process.Start(link.NavigateUri.ToString());
        }

        private void Window_ContentRendered(object sender, EventArgs e) => txbExpression.Focus();

        private void OK_Click(object sender, RoutedEventArgs e) => Close();
    }
}
