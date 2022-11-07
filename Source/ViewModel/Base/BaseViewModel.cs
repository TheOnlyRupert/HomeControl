using System.ComponentModel;

/**
 *  This interface allows update messages to be passed to the View.
 *  All ViewModel classes that are bound to by the View should implement this as
 *  there is a known memory leak that may occur if they don’t.
 */
namespace HomeControl.Source.ViewModel.Base; 

public abstract class BaseViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChangedEvent(string propertyName) {
        PropertyChangedEventHandler handler = PropertyChanged;
        handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}