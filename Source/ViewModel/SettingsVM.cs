using System.Windows;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel {
    public class SettingsVM : BaseViewModel {
        private string _userAgentText;

        public SettingsVM() {
            ReferenceValues.UserAgent = "";
        }

        public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

        #region Fields

        public string UserAgentText {
            get => _userAgentText;
            set {
                _userAgentText = value;
                RaisePropertyChangedEvent("UserAgentText");
            }
        }

        #endregion

        private void ButtonCommandLogic(object param) {
            switch (param) {
            case "save":
                if (!string.IsNullOrEmpty(UserAgentText)) {
                    ReferenceValues.UserAgent = UserAgentText;
                    CsvParser.SaveSettings();
                } else {
                    MessageBox.Show("UserAgent cannot be blank", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                break;
            }
        }
    }
}