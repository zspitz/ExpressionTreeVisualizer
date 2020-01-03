using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeVisualizer.Util {
    public abstract class ViewModelBase<TModel> : INotifyPropertyChanged {
        public TModel Model { get; }

        protected ViewModelBase(TModel model) => Model = model;

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool IsEqual<T>(T current, T newValue) {
            if (current is IEquatable<T> equatable) {
                return equatable.Equals(newValue);
            } else if (current is null) {
                if (newValue is null) { return true; }
            } else {
                if (current.Equals(newValue)) { return true; }
            }
            return false;
        }
        private void Invoke(string? name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>Raises change notification for fields defined in the current class</summary>
        protected void NotifyChanged<T>(ref T current, T newValue, [CallerMemberName] string? name = null) {
            if (IsEqual(current, newValue)) { return; }
            current = newValue;
            Invoke(name);
        }

        /// <summary>Raises change notification for fields not defined in the current class (e.g. the model class)</summary>
        protected void NotifyChanged<T>(T current, T newValue, Action? setter = null, [CallerMemberName] string? name = null) {
            if (IsEqual(current, newValue)) { return; }
            setter?.Invoke(); 
            Invoke(name);
        }
    }
}
