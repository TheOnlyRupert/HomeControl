using System.Windows.Input;

namespace HomeControl.Source.ViewModel.Base;

public class DelegateCommand : ICommand {
    private readonly Action<object> _action;
    private readonly bool _canExecute;

    public DelegateCommand(Action<object> action, bool canExecute) {
        _action = action;
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter) {
        _action(parameter);
    }

    public bool CanExecute(object parameter) {
        return _canExecute;
    }
}