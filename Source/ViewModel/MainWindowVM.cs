using System.Windows.Input;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel {
    public class MainWindowVM : BaseViewModel {
        private string _iconImage;

        public MainWindowVM() {
            IconImage = "../../Resources/Images/icon.png";
        }

        public ICommand GlobalKeyboardListener => new DelegateCommand(GlobalKeyboardListenerLogic, true);

        #region Fields

        public string IconImage {
            get => _iconImage;
            set {
                _iconImage = value;
                RaisePropertyChangedEvent("IconImage");
            }
        }

        #endregion

        private void GlobalKeyboardListenerLogic(object obj) { }
    }
}