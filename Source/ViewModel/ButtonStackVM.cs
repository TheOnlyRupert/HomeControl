using System;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Modules;
using HomeControl.Source.Modules.Debug;
using HomeControl.Source.Modules.Games;
using HomeControl.Source.ViewModel.Base;
using Tamagotchi = HomeControl.Source.Modules.Games.Tamagotchi.Tamagotchi;

namespace HomeControl.Source.ViewModel;

public class ButtonStackVM : BaseViewModel {
    private string _lockedImage, _weatherStatus, _weatherColor, _serverStatus, _serverColor;

    public ButtonStackVM() {
        ReferenceValues.LockUi = !ReferenceValues.JsonSettingsMaster.DebugMode;
        LockedImage = ReferenceValues.LockUi ? "./../../Resources/Images/icons/key_locked.png" : "./../../Resources/Images/icons/key_unlocked.png";

        UpdateInternetStatus();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "ScreenSaverOn":
            LockedImage = "./../../Resources/Images/icons/key_locked.png";
            break;
        case "UpdateInternetStatus":
            UpdateInternetStatus();

            break;
        }
    }

    private void UpdateInternetStatus() {
        if (ReferenceValues.IsWeatherApiOnline) {
            WeatherStatus = "Connected";
            WeatherColor = "Green";
        } else {
            WeatherStatus = "Offline";
            WeatherColor = "Red";
        }

        try {
            if (ReferenceValues.JsonSettingsMaster.IsOff) {
                ServerStatus = "Disabled In Settings";
                ServerColor = "DarkSlateGray";
            } else if (ReferenceValues.ClientInfo.Closed) {
                ServerStatus = "Offline";
                ServerColor = "Red";
            } else {
                ServerStatus = "Connected";
                ServerColor = "Green";
            }
        } catch (Exception) {
            ServerStatus = "Offline";
            ServerColor = "Red";
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "lock":
            if (!ReferenceValues.LockUi) {
                ReferenceValues.LockUi = true;
                LockedImage = "./../../Resources/Images/icons/key_locked.png";
            } else {
                ReferenceValues.LockUi = false;
                LockedImage = "./../../Resources/Images/icons/key_unlocked.png";
            }

            ReferenceValues.SoundToPlay = "unlock";
            SoundDispatcher.PlaySound();

            break;
        case "debug":
            if (!ReferenceValues.LockUi) {
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

    #region Fields

    public string LockedImage {
        get => _lockedImage;
        set {
            _lockedImage = value;
            RaisePropertyChangedEvent("LockedImage");
        }
    }

    public string WeatherStatus {
        get => _weatherStatus;
        set {
            _weatherStatus = value;
            RaisePropertyChangedEvent("WeatherStatus");
        }
    }

    public string WeatherColor {
        get => _weatherColor;
        set {
            _weatherColor = value;
            RaisePropertyChangedEvent("WeatherColor");
        }
    }

    public string ServerStatus {
        get => _serverStatus;
        set {
            _serverStatus = value;
            RaisePropertyChangedEvent("ServerStatus");
        }
    }

    public string ServerColor {
        get => _serverColor;
        set {
            _serverColor = value;
            RaisePropertyChangedEvent("ServerColor");
        }
    }

    #endregion
}