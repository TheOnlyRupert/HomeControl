using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeControl.Source.ViewModel.Base;

public abstract class BaseViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChangedEvent(string propertyName) {
        PropertyChangedEventHandler? handler = PropertyChanged;
        handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null) {
        // Check if the new value is different from the current value
        if (EqualityComparer<T>.Default.Equals(backingField, value)) return false;

        backingField = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}