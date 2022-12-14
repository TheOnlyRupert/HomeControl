using System;
using System.Net;
using System.Text.Json;
using System.Windows.Input;
using System.Windows.Threading;
using HomeControl.Source.IO;
using HomeControl.Source.Modules;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class MainWindowVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;
    private string _iconImage, _lockedText, _lockedColor, _onlineText, _onlineColor;
    private DateTime currentDate;

    public MainWindowVM() {
        IconImage = "../../Resources/Images/icon.png";
        simpleMessenger = CrossViewMessenger.Instance;
        currentDate = DateTime.Now;

        /* Get Settings */
        string[] settings = CsvParser.GetSettings();
        ReferenceValues.UserAgent = settings[0];
        ReferenceValues.User1Name = settings[1];
        ReferenceValues.User2Name = settings[2];
        ReferenceValues.ChildName = new string[3];
        ReferenceValues.ChildName[0] = settings[3];
        ReferenceValues.ChildName[1] = settings[4];
        ReferenceValues.ChildName[2] = settings[5];

        ReferenceValues.LockUI = true;

        LockedColor = "Transparent";
        LockedText = "LOCKED";

        if (ReferenceValues.UserAgent == "null") {
            do {
                Settings settingsDialog = new();
                settingsDialog.ShowDialog();
                settingsDialog.Close();
            } while (string.IsNullOrEmpty(ReferenceValues.UserAgent));
        }

        ApiStatus();

        /* Global DispatcherTimer */
        DispatcherTimer dispatcherTimer = new();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "lockUi":
            if (ReferenceValues.LockUI) {
                ReferenceValues.LockUI = false;
                LockedColor = "Red";
                LockedText = "UNLOCKED";
            } else {
                ReferenceValues.LockUI = true;
                LockedColor = "Transparent";
                LockedText = "LOCKED";
            }

            break;
        }
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e) {
        /* Date Changes */
        if (!currentDate.Day.Equals(DateTime.Now.Day)) {
            currentDate = DateTime.Now;
            simpleMessenger.PushMessage("DateChanged", null);
        }

        if (!currentDate.Minute.Equals(DateTime.Now.Minute)) {
            currentDate = DateTime.Now;
            simpleMessenger.PushMessage("MinChanged", null);
            ApiStatus();
        }

        /* Alarms Module - Prevents dupes between different windows */
        if (ReferenceValues.IsTimerRunning[0]) {
            if (!ReferenceValues.SwitchTimerDirection[0]) {
                ReferenceValues.TimerSeconds[0]--;
                if (ReferenceValues.TimerSeconds[0] < 0) {
                    ReferenceValues.TimerSeconds[0] = 59;
                    --ReferenceValues.TimerMinutes[0];
                    if (ReferenceValues.TimerMinutes[0] < 0) {
                        ReferenceValues.TimerMinutes[0] = 0;
                        ReferenceValues.TimerSeconds[0] = 1;
                        ReferenceValues.SwitchTimerDirection[0] = true;
                        ReferenceValues.IsTimerAlarmActive = true;
                    }
                }
            } else {
                ReferenceValues.TimerSeconds[0] = ++ReferenceValues.TimerSeconds[0];
                if (ReferenceValues.TimerSeconds[0] > 59) {
                    ReferenceValues.TimerSeconds[0] = 0;
                    ++ReferenceValues.TimerMinutes[0];
                }
            }
        }

        if (ReferenceValues.IsTimerRunning[1]) {
            if (!ReferenceValues.SwitchTimerDirection[1]) {
                ReferenceValues.TimerSeconds[1]--;
                if (ReferenceValues.TimerSeconds[1] < 0) {
                    ReferenceValues.TimerSeconds[1] = 59;
                    --ReferenceValues.TimerMinutes[1];
                    if (ReferenceValues.TimerMinutes[1] < 0) {
                        ReferenceValues.TimerMinutes[1] = 0;
                        ReferenceValues.TimerSeconds[1] = 1;
                        ReferenceValues.SwitchTimerDirection[1] = true;
                        ReferenceValues.IsTimerAlarmActive = true;
                    }
                }
            } else {
                ReferenceValues.TimerSeconds[1] = ++ReferenceValues.TimerSeconds[1];
                if (ReferenceValues.TimerSeconds[1] > 59) {
                    ReferenceValues.TimerSeconds[1] = 0;
                    ++ReferenceValues.TimerMinutes[1];
                }
            }
        }

        if (ReferenceValues.IsTimerRunning[2]) {
            if (!ReferenceValues.SwitchTimerDirection[2]) {
                ReferenceValues.TimerSeconds[2]--;
                if (ReferenceValues.TimerSeconds[2] < 0) {
                    ReferenceValues.TimerSeconds[2] = 59;
                    --ReferenceValues.TimerMinutes[2];
                    if (ReferenceValues.TimerMinutes[2] < 0) {
                        ReferenceValues.TimerMinutes[2] = 0;
                        ReferenceValues.TimerSeconds[2] = 1;
                        ReferenceValues.SwitchTimerDirection[2] = true;
                        ReferenceValues.IsTimerAlarmActive = true;
                    }
                }
            } else {
                ReferenceValues.TimerSeconds[2] = ++ReferenceValues.TimerSeconds[2];
                if (ReferenceValues.TimerSeconds[2] > 59) {
                    ReferenceValues.TimerSeconds[2] = 0;
                    ++ReferenceValues.TimerMinutes[2];
                }
            }
        }

        if (ReferenceValues.IsTimerRunning[3]) {
            if (!ReferenceValues.SwitchTimerDirection[3]) {
                ReferenceValues.TimerSeconds[3]--;
                if (ReferenceValues.TimerSeconds[3] < 0) {
                    ReferenceValues.TimerSeconds[3] = 59;
                    --ReferenceValues.TimerMinutes[3];
                    if (ReferenceValues.TimerMinutes[3] < 0) {
                        ReferenceValues.TimerMinutes[3] = 0;
                        ReferenceValues.TimerSeconds[3] = 1;
                        ReferenceValues.SwitchTimerDirection[3] = true;
                        ReferenceValues.IsTimerAlarmActive = true;
                    }
                }
            } else {
                ReferenceValues.TimerSeconds[3] = ++ReferenceValues.TimerSeconds[3];
                if (ReferenceValues.TimerSeconds[3] > 59) {
                    ReferenceValues.TimerSeconds[3] = 0;
                    ++ReferenceValues.TimerMinutes[3];
                }
            }
        }

        simpleMessenger.PushMessage("Refresh", null);
    }

    private void ApiStatus() {
        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            using WebClient client1 = new();
            const string hostUrl = "https://api.weather.gov/";
            client1.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.UserAgent);
            string apiStatusString = client1.DownloadString(hostUrl);
            ApiStatus apiStatus = JsonSerializer.Deserialize<ApiStatus>(apiStatusString, options);

            if (apiStatus is { status: "OK" }) {
                OnlineColor = "Transparent";
                OnlineText = "ONLINE";
            } else {
                OnlineColor = "Red";
                OnlineText = "OFFLINE";
            }
        } catch (Exception) {
            OnlineColor = "Red";
            OnlineText = "OFFLINE";
        }
    }

    #region Fields

    public string IconImage {
        get => _iconImage;
        set {
            _iconImage = value;
            RaisePropertyChangedEvent("IconImage");
        }
    }

    public string LockedText {
        get => _lockedText;
        set {
            _lockedText = value;
            RaisePropertyChangedEvent("LockedText");
        }
    }

    public string LockedColor {
        get => _lockedColor;
        set {
            _lockedColor = value;
            RaisePropertyChangedEvent("LockedColor");
        }
    }

    public string OnlineText {
        get => _onlineText;
        set {
            _onlineText = value;
            RaisePropertyChangedEvent("OnlineText");
        }
    }

    public string OnlineColor {
        get => _onlineColor;
        set {
            _onlineColor = value;
            RaisePropertyChangedEvent("OnlineColor");
        }
    }

    #endregion
}