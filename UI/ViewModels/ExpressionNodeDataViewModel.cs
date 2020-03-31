using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;
using ExpressionTreeVisualizer.Serialization;

namespace ExpressionTreeVisualizer.UI {
    public class ExpressionNodeDataViewModel : Selectable<ExpressionNodeData> {
        public List<ExpressionNodeDataViewModel> Children { get; }

        public ExpressionNodeDataViewModel(ExpressionNodeData model, List<ExpressionNodeDataViewModel> allNodes) : base(model) {
            Children = model.Children.Select(x => {
                var vm = new ExpressionNodeDataViewModel(x, allNodes);
                allNodes.Add(vm);
                return vm;
            }).ToList();
        }

        public void ClearSelection(params ExpressionNodeDataViewModel[] toSelect) {
            IsSelected = this.In(toSelect);
            Children.ForEach(x => x.ClearSelection(toSelect));
        }
    }
}
