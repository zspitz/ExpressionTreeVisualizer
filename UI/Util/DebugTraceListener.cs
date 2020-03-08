using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExpressionTreeVisualizer.Util {
    public class DebugTraceListener : TraceListener {
        private readonly static List<string> ignoreMessages = new List<string> {
            "Cannot find source for binding with reference 'RelativeSource FindAncestor, AncestorType='System.Windows.Controls.DataGrid', AncestorLevel='1''. BindingExpression:Path=AreRowDetailsFrozen; DataItem=null; target element is 'DataGridDetailsPresenter' (Name=''); target property is 'SelectiveScrollingOrientation' (type 'SelectiveScrollingOrientation')",
            "Cannot find source for binding with reference 'RelativeSource FindAncestor, AncestorType='System.Windows.Controls.DataGrid', AncestorLevel='1''. BindingExpression:Path=HeadersVisibility; DataItem=null; target element is 'DataGridRowHeader' (Name=''); target property is 'Visibility' (type 'Visibility')",
            "(Controls:MultiSelectTreeView.IsKeyboardMode)' property not found on 'object' ''MultiSelectTreeView'",
            "(Controls:MultiSelectTreeView.ItemIndent)' property not found on 'object' ''MultiSelectTreeView'",
            "(Controls:MultiSelectTreeView.HoverHighlighting)' property not found on 'object' ''MultiSelectTreeView'"
        };
        public override void Write(string message) { }
        public override void WriteLine(string message) {
            if (ignoreMessages.Any(ignore => message.Contains(ignore))) { return; }
            throw new Exception($"Binding error: {message}");
        }
    }
}
