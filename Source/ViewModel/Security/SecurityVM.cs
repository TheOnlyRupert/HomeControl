using System.Windows.Input;
using HomeControl.Source.Modules.Security;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Security;

public class SecurityVM : BaseViewModel {
    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "security":
            EditSecurity editSecurity = new();
            editSecurity.ShowDialog();
            editSecurity.Close();
            break;
        }
    }
}