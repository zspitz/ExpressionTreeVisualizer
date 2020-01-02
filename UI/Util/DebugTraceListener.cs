using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExpressionTreeVisualizer.Util {
    public class DebugTraceListener : TraceListener {
        private readonly static List<string> ignoreMessages = new List<string> {
            "Cannot find source for binding with reference 'RelativeSource FindAncestor, AncestorType='System.Windows.Controls.DataGrid', AncestorLevel='1''. BindingExpression:Path=AreRowDetailsFrozen; DataItem=null; target element is 'DataGridDetailsPresenter' (Name=''); target property is 'SelectiveScrollingOrientation' (type 'SelectiveScrollingOrientation')",
            "Cannot find source for binding with reference 'RelativeSource FindAncestor, AncestorType='System.Windows.Controls.DataGrid', AncestorLevel='1''. BindingExpression:Path=HeadersVisibility; DataItem=null; target element is 'DataGridRowHeader' (Name=''); target property is 'Visibility' (type 'Visibility')"
        };
        public override void Write(string message) { }
        public override void WriteLine(string message) {
            if (ignoreMessages.Contains(message)) { return; }
            throw new Exception($"Binding error: {message}");
        }
    }
}
