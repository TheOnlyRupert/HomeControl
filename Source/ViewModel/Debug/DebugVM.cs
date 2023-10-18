using System.Windows.Input;
using HomeControl.Source.Modules.Debug;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Debug;

public class DebugVM : BaseViewModel {
    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "debug":
            DebugLog debugLog = new();
            debugLog.ShowDialog();
            debugLog.Close();
            break;
        }
    }
}