using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Modules.Security;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Security;

public class SecurityVM : BaseViewModel {
    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "security":
            if (!ReferenceValues.LockUI) {
                EditSecurity editSecurity = new();
                editSecurity.ShowDialog();
                editSecurity.Close();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        }
    }
}