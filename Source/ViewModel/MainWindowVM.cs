using System;
using System.Net;
using System.Text.Json;
using System.Windows.Threading;
using HomeControl.Source.Control;
using HomeControl.Source.IO;
using HomeControl.Source.Modules;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class MainWindowVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;
    private string _iconImage, _lockedText, _lockedColor, _onlineText, _onlineColor;
    private bool changeDate;
    private DateTime currentDate;

    public MainWindowVM() {
        IconImage = "../../Resources/Images/icon.png";
        simpleMessenger = CrossViewMessenger.Instance;
        currentDate = DateTime.Now;
        ReferenceValues.TimerSound = new PlaySound("timerDone");
        ReferenceValues.IsScreenSaverEnabled = false;

        /* Get Settings */
        new SettingsFromJson();

        ReferenceValues.LockUI = true;

        LockedColor = "Transparent";
        LockedText = "LOCKED";

        if (string.IsNullOrEmpty(ReferenceValues.JsonMasterSettings.UserAgent)) {
            Settings settingsDialog = new();
            settingsDialog.ShowDialog();
            settingsDialog.Close();
        }

        ApiStatus();

        /* Global DispatcherTimer */
        DispatcherTimer dispatcherTimer = new();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();

        AppActivityTimer activityTimer = new(360000, 360000, false);
        activityTimer.OnInactive += activityTimer_OnInactive;
        activityTimer.OnActive += activityTimer_OnActive;

        void activityTimer_OnInactive(object sender, EventArgs e) {
            Console.WriteLine("Start ScreenSaver: " + DateTime.Now);
            simpleMessenger.PushMessage("ScreenSaverOn", null);
            ReferenceValues.LockUI = true;
        }

        void activityTimer_OnActive(object sender, EventArgs e) {
            Console.WriteLine("End ScreenSaver: " + DateTime.Now);
            simpleMessenger.PushMessage("ScreenSaverOff", null);
        }
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e) {
        changeDate = false;
        /* Min Changes */
        if (!currentDate.Minute.Equals(DateTime.Now.Minute)) {
            simpleMessenger.PushMessage("MinChanged", null);
            ApiStatus();
            changeDate = true;
        }

        if (!currentDate.Hour.Equals(DateTime.Now.Hour)) {
            simpleMessenger.PushMessage("HourChanged", null);
            changeDate = true;
        }

        /* Date Changes */
        if (!currentDate.Day.Equals(DateTime.Now.Day)) {
            simpleMessenger.PushMessage("DateChanged", null);
            changeDate = true;
        }

        /* Month Changes */
        if (!currentDate.Month.Equals(DateTime.Now.Month)) {
            simpleMessenger.PushMessage("MonthChanged", null);
            changeDate = true;
        }

        if (changeDate) {
            currentDate = DateTime.Now;
            changeDate = false;
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

        if (ReferenceValues.IsTimerAlarmActive && !ReferenceValues.TimerSound.IsPlaying()) {
            ReferenceValues.TimerSound.Play(true);
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
            client1.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonMasterSettings.UserAgent);
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