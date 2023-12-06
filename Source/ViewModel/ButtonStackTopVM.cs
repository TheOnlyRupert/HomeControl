using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Modules.Debug;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class ButtonStackTopVM : BaseViewModel {
    private string _lockedImage;

    public ButtonStackTopVM() {
        ReferenceValues.LockUI = !ReferenceValues.JsonSettingsMaster.DebugMode;
        LockedImage = ReferenceValues.LockUI ? "./../../Resources/Images/icons/key_locked.png" : "./../../Resources/Images/icons/key_unlocked.png";

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    #region Fields

    public string LockedImage {
        get => _lockedImage;
        set {
            _lockedImage = value;
            RaisePropertyChangedEvent("LockedImage");
        }
    }

    #endregion

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "ScreenSaverOn") {
            LockedImage = "./../../Resources/Images/icons/key_locked.png";
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "lock":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.LockUI = true;
                LockedImage = "./../../Resources/Images/icons/key_locked.png";
            } else {
                ReferenceValues.LockUI = false;
                LockedImage = "./../../Resources/Images/icons/key_unlocked.png";
            }

            ReferenceValues.SoundToPlay = "unlock";
            SoundDispatcher.PlaySound();

            break;
        case "debug":
            if (!ReferenceValues.LockUI) {
                DebugLog debugLog = new();
                debugLog.ShowDialog();
                debugLog.Close();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        }
    }
}