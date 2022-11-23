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
        ReferenceValues.User1Name = settings[1];
        ReferenceValues.User2Name = settings[2];
        ReferenceValues.Child1Name = settings[3];
        ReferenceValues.Child2Name = settings[4];
        ReferenceValues.Child3Name = settings[5];

        if (ReferenceValues.UserAgent == "null") {
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