using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Modules;
using HomeControl.Source.Modules.Debug;
using HomeControl.Source.Modules.Games;
using HomeControl.Source.ViewModel.Base;
using Tamagotchi = HomeControl.Source.Modules.Games.Tamagotchi.Tamagotchi;

namespace HomeControl.Source.ViewModel;

public class ButtonStackVM : BaseViewModel {
    private string _lockedImage;

    public ButtonStackVM() {
        ReferenceValues.LockUi = !ReferenceValues.JsonSettingsMaster.DebugMode;
        UpdateLockedImage();

        CrossViewMessenger.Instance.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    #region Fields

    public string LockedImage {
        get => _lockedImage;
        set => SetProperty(ref _lockedImage, value);
    }

    #endregion

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "ScreenSaverOn") {
            LockedImage = "./../../Resources/Images/icons/key_locked.png";
        }
    }

    private void ButtonCommandLogic(object param) {
        if (param == null) return;

        if (param.ToString() == "lock") {
            ToggleLockUi();
        } else if (ReferenceValues.LockUi) {
            PlayLockedSound();
        } else {
            OpenDialog(param.ToString());
        }
    }

    private void ToggleLockUi() {
        ReferenceValues.LockUi = !ReferenceValues.LockUi;
        UpdateLockedImage();
        PlayUnlockSound();
    }

    private void UpdateLockedImage() {
        LockedImage = ReferenceValues.LockUi
            ? "./../../Resources/Images/icons/key_locked.png"
            : "./../../Resources/Images/icons/key_unlocked.png";
    }

    private void PlayUnlockSound() {
        SoundDispatcher.PlaySound("unlock");
    }

    private void PlayLockedSound() {
        SoundDispatcher.PlaySound("locked");
    }

    private void OpenDialog(string command) {
        Dictionary<string, Action> dialogMap = new() {
            {
                "debug", () => new DebugLog().ShowDialogAndClose()
            }, {
                "contacts", () => new Contacts().ShowDialogAndClose()
            }, {
                "exercise", () => new Modules.Fitness.Fitness().ShowDialogAndClose()
            }, {
                "pictionary", () => new Pictionary().ShowDialogAndClose()
            }, {
                "coinFlip", () => new CoinFlip().ShowDialogAndClose()
            }, {
                "tamagotchi", () => {
                    new Tamagotchi().ShowDialogAndClose();
                    FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
                }
            }, {
                "nhie", () => new Nhie().ShowDialogAndClose()
            }, {
                "trafficLight", () => new TrafficLight().ShowDialogAndClose()
            }
        };

        if (dialogMap.TryGetValue(command, out Action action)) {
            action.Invoke();
        }
    }
}

public static class DialogExtensions {
    public static void ShowDialogAndClose(this Window dialog) {
        dialog.ShowDialog();
        dialog.Close();
    }
}