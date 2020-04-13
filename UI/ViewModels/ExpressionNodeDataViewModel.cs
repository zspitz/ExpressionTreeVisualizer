using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;
using ExpressionTreeVisualizer.Serialization;
using System.Windows.Input;

namespace ExpressionTreeVisualizer.UI {
    public class ExpressionNodeDataViewModel : Selectable<ExpressionNodeData> {
        public List<ExpressionNodeDataViewModel> Children { get; }

        public ExpressionNodeDataViewModel(ExpressionNodeData model, List<ExpressionNodeDataViewModel> allNodes, ICommand? openInNewWindow = null, RelayCommand? copyWatchExpression = null) : base(model) {
            if (model.EnableValueInNewWindow) { OpenInNewWindow = openInNewWindow; }
            CopyWatchExpression = copyWatchExpression;

            Children = model.Children.Select(x => {
                var vm = new ExpressionNodeDataViewModel(x, allNodes, openInNewWindow, CopyWatchExpression);
                allNodes.Add(vm);
                return vm;
            }).ToList();
        }

        public void ClearSelection(params ExpressionNodeDataViewModel[] toSelect) {
            IsSelected = this.In(toSelect);
            Children.ForEach(x => x.ClearSelection(toSelect));
        }

        public ICommand? OpenInNewWindow { get; private set; }
        public RelayCommand? CopyWatchExpression { get; private set; }
    }
}
