using System;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules;
using HomeControl.Source.Modules.Debug;
using HomeControl.Source.Modules.Games;
using HomeControl.Source.ViewModel.Base;
using Tamagotchi = HomeControl.Source.Modules.Games.Tamagotchi.Tamagotchi;

namespace HomeControl.Source.ViewModel;

public class ButtonStackVM : BaseViewModel {
    private string _lockedImage;

    public ButtonStackVM() {
        ReferenceValues.LockUI = !ReferenceValues.JsonSettingsMaster.DebugMode;
        LockedImage = ReferenceValues.LockUI ? "./../../Resources/Images/icons/key_locked.png" : "./../../Resources/Images/icons/key_unlocked.png";

        try {
            ReferenceValues.JsonGameStatsMaster = JsonSerializer.Deserialize<JsonGameStats>(FileHelpers.LoadFileText("gameStats", true));
        } catch (Exception) {
            ReferenceValues.JsonGameStatsMaster = new JsonGameStats();

            FileHelpers.SaveFileText("gameStats", JsonSerializer.Serialize(ReferenceValues.JsonGameStatsMaster), true);
        }

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

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

        case "contacts":
            Contacts contacts = new();
            contacts.ShowDialog();
            contacts.Close();
            break;
        case "exercise":
            Modules.Exercise.Exercise exercise = new();
            exercise.ShowDialog();
            exercise.Close();
            break;
        case "pictionary":
            Pictionary pictionary = new();
            pictionary.ShowDialog();
            pictionary.Close();
            break;
        case "coinFlip":
            CoinFlip coinFlip = new();
            coinFlip.ShowDialog();
            coinFlip.Close();
            break;
        case "tamagotchi":
            Tamagotchi tamagotchi = new();
            tamagotchi.ShowDialog();
            tamagotchi.Close();

            FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
            break;
        case "nhie":
            Nhie nhie = new();
            nhie.ShowDialog();
            nhie.Close();
            break;
        case "trafficLight":
            TrafficLight trafficLight = new();
            trafficLight.ShowDialog();
            trafficLight.Close();
            break;
        }
    }
}