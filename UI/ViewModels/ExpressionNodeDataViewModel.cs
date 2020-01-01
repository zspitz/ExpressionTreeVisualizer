using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionTreeVisualizer.Util;
using ExpressionTreeVisualizer.Serialization;

namespace ExpressionTreeVisualizer.UI {
    public class ExpressionNodeDataViewModel : Selectable<ExpressionNodeData> {
        public List<ExpressionNodeDataViewModel> Children { get; }

        public ExpressionNodeDataViewModel(ExpressionNodeData model, List<ExpressionNodeDataViewModel> endNodes) : base(model) {
            Children = model.Children.Select(x => {
                var vm = new ExpressionNodeDataViewModel(x, endNodes);
                if (x.EndNodeType != null) { endNodes.Add(vm); }
                return vm;
            }).ToList();
        }

        public void ClearSelection() {
            IsSelected = false;
            Children.ForEach(x => x.ClearSelection());
        }
    }
}
