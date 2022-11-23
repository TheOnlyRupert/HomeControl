using System.Windows;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class SettingsVM : BaseViewModel {
    private string _userAgentText, _child1Name, _child2Name, _child3Name;

    public SettingsVM() {
        ReferenceValues.UserAgent = "";
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "save":
            if (!string.IsNullOrEmpty(UserAgentText)) {
                ReferenceValues.UserAgent = UserAgentText;
                ReferenceValues.Child1Name = Child1Name;
                ReferenceValues.Child2Name = Child2Name;
                ReferenceValues.Child3Name = Child3Name;
                CsvParser.SaveSettings();
            } else {
                MessageBox.Show("Fields cannot be blank", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


            break;
        }
    }

    #region Fields

    public string UserAgentText {
        get => _userAgentText;
        set {
            _userAgentText = value;
            RaisePropertyChangedEvent("UserAgentText");
        }
    }

    public string Child1Name {
        get => _child1Name;
        set {
            _child1Name = value;
            RaisePropertyChangedEvent("Child1Name");
        }
    }

    public string Child2Name {
        get => _child2Name;
        set {
            _child2Name = value;
            RaisePropertyChangedEvent("Child2Name");
        }
    }

    public string Child3Name {
        get => _child3Name;
        set {
            _child3Name = value;
            RaisePropertyChangedEvent("Child3Name");
        }
    }

    #endregion
}