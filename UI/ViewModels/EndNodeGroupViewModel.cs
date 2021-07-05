using ExpressionTreeVisualizer.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;

namespace ExpressionTreeVisualizer {
    public class EndNodeGroupViewModel : Selectable<EndNodeData> {
        public readonly ReadOnlyCollection<ExpressionNodeDataViewModel> Nodes;

        public EndNodeGroupViewModel(EndNodeData model, IEnumerable<ExpressionNodeDataViewModel> nodes) : base(model) {
            Nodes =  nodes.ToReadOnlyCollection();

            void handler(object? s, PropertyChangedEventArgs e) {
                if (e.PropertyName != nameof(IsSelected)) { return; }
                IsSelected = Nodes.Any(x => x.IsSelected);
            }

            foreach (var node in Nodes) {
                node.PropertyChanged += handler;
            }
        }

        //public void IsSelectedAndChildren(bool selected) {
        //    foreach (var node in Nodes) {
        //        node.IsSelected = selected;
        //    }
        //}
    }
}
