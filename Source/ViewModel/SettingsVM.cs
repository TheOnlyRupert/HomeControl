using System.Windows;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class SettingsVM : BaseViewModel {
    private string _userAgentText, _user1Name, _user2Name, _child1Name, _child2Name, _child3Name;

    public SettingsVM() {
        ReferenceValues.UserAgent = "null";
        ReferenceValues.User1Name = "null";
        ReferenceValues.User2Name = "null";
        ReferenceValues.ChildName = new string[3];
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "save":
            if (!string.IsNullOrEmpty(UserAgentText) || !string.IsNullOrEmpty(User1Name) || !string.IsNullOrEmpty(User2Name)) {
                ReferenceValues.UserAgent = UserAgentText;
                ReferenceValues.User1Name = User1Name;
                ReferenceValues.User2Name = User2Name;
                ReferenceValues.ChildName[0] = Child1Name;
                ReferenceValues.ChildName[1] = Child2Name;
                ReferenceValues.ChildName[2] = Child3Name;
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
            _userAgentText = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("UserAgentText");
        }
    }

    public string User1Name {
        get => _user1Name;
        set {
            _user1Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User1Name");
        }
    }

    public string User2Name {
        get => _user2Name;
        set {
            _user2Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User2Name");
        }
    }

    public string Child1Name {
        get => _child1Name;
        set {
            _child1Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("Child1Name");
        }
    }

    public string Child2Name {
        get => _child2Name;
        set {
            _child2Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("Child2Name");
        }
    }

    public string Child3Name {
        get => _child3Name;
        set {
            _child3Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("Child3Name");
        }
    }

    #endregion
}