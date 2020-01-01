using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeVisualizer.Util {
    public class Selectable<TModel> : ViewModelBase<TModel> {
        public Selectable(TModel model) : base(model) { }

        private bool isSelected;
        public bool IsSelected {
            get => isSelected;
            set => NotifyChanged(ref isSelected, value);
        }
    }
}
