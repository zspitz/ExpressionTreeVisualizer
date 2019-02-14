using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExpressionToString.Util {
    public static class INotifyPropertyChangedExtensions {
        public static void NotifyChanged<T>(this INotifyPropertyChanged inpc,  ref T current, T newValue, Action<PropertyChangedEventArgs> eventRaiser, [CallerMemberName] string name = null) where T : IEquatable<T> {
            if (current.Equals(newValue)) { return; }
            current = newValue;
            eventRaiser(new PropertyChangedEventArgs(name));
        }
    }
}
