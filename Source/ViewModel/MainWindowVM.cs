using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class MainWindowVM : BaseViewModel {
    private string _iconImage;

    public MainWindowVM() {
        IconImage = "../../Resources/Images/icon.png";

        /* Get Settings */
        string[] settings = CsvParser.GetSettings();
        ReferenceValues.UserAgent = settings[0];

        if (string.IsNullOrEmpty(settings[0])) {
            do {
                Settings settingsDialog = new();
                settingsDialog.ShowDialog();
                settingsDialog.Close();
            } while (string.IsNullOrEmpty(ReferenceValues.UserAgent));
        }
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