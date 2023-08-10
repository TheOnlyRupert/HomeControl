using System;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Net;
using System.Text.Json;
using System.Windows.Threading;
using HomeControl.Source.Control;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Modules;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;
using HomeControl.Source.ViewModel.Hvac;

namespace HomeControl.Source.ViewModel;

public class MainWindowVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;
    private string _iconImage;
    private string _onlineColor;
    private bool changeDate, internetMessage;
    private DateTime currentDate;

    public MainWindowVM() {
        IconImage = "../../Resources/Images/icon.png";
        simpleMessenger = CrossViewMessenger.Instance;
        currentDate = DateTime.Now;
        internetMessage = false;
        OnlineColor = "Black";

        /* Get Debug (MAKE SURE THIS IS FIRST!) */
        new DebugFromJson();

        /* Get Settings */
        new SettingsFromJson();

        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "MainWindowVM",
            Description = ReferenceValues.COPYRIGHT + "  Version: " + ReferenceValues.VERSION
        });
        SaveDebugFile.Save();

        /* Icon Image List (Manual for now until I can figure out how to pull from Resource file without copying images */
        ReferenceValues.IconImageList = new ObservableCollection<string> {
            "alarms",
            "behavior",
            "calendar",
            "chores",
            "clock",
            "coin_flip",
            "contact",
            "events",
            "games",
            "groceries",
            "hvac",
            "key_locked",
            "key_unlocked",
            "meal",
            "money",
            "notes",
            "open_tickets",
            "panic",
            "pictionary",
            "recipes",
            "tamagotchi",
            "temp_burning",
            "temp_cold",
            "temp_cool",
            "temp_freezing",
            "temp_hot",
            "temp_warm",
            "tic_tac_toe",
            "todo",
            "vacation",
            "wifi",
            "workout"
        };

        if (string.IsNullOrEmpty(ReferenceValues.JsonMasterSettings.UserAgent)) {
            Settings settingsDialog = new();
            settingsDialog.ShowDialog();
            settingsDialog.Close();
        }

        ApiStatus();

        /* HVAC Serial Port */
        new HvacFromJson();
        if (string.IsNullOrEmpty(ReferenceValues.JsonMasterSettings.ComPort)) {
            ReferenceValues.JsonMasterSettings.ComPort = "COM5";
        }

        ReferenceValues.SerialPortMaster = new SerialPort(ReferenceValues.JsonMasterSettings.ComPort, 9600);
        HvacCrossPlay.EstablishConnection();

        /* Global DispatcherTimer */
        DispatcherTimer dispatcherTimer = new();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();

        /* Screen Saver */
        AppActivityTimer activityTimer = new(60000, 60000, false);
        activityTimer.OnInactive += activityTimer_OnInactive;
        activityTimer.OnActive += activityTimer_OnActive;

        void activityTimer_OnInactive(object sender, EventArgs e) {
            if (!ReferenceValues.JsonMasterSettings.IsDebugMode) {
                simpleMessenger.PushMessage("ScreenSaverOn", null);
                ReferenceValues.LockUI = true;
            }
        }

        void activityTimer_OnActive(object sender, EventArgs e) {
            simpleMessenger.PushMessage("ScreenSaverOff", null);
        }
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e) {
        changeDate = false;
        /* Min Changes */
        if (!currentDate.Minute.Equals(DateTime.Now.Minute)) {
            simpleMessenger.PushMessage("MinChanged", null);

            ApiStatus();
            if (!ReferenceValues.IsHvacComEstablished) {
                HvacCrossPlay.EstablishConnection();
            }

            changeDate = true;
        }

        /* Hour Changes */
        if (!currentDate.Hour.Equals(DateTime.Now.Hour)) {
            simpleMessenger.PushMessage("HourChanged", null);
            changeDate = true;

            Random random = new();
            ReferenceValues.SoundToPlay = "clock" + random.Next(1, 3);
            SoundDispatcher.PlaySound();
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
                if (internetMessage) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "MainWindowVM",
                        Description = "Restored Internet Connection"
                    });
                    SaveDebugFile.Save();
                }

                internetMessage = false;
                OnlineColor = "Black";
            } else {
                LostInternet();
            }
        } catch (Exception) {
            LostInternet();
        }
    }

    private void LostInternet() {
        if (!internetMessage) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "MainWindowVM",
                Description = "Lost Internet Connection"
            });
            SaveDebugFile.Save();
            internetMessage = true;
            OnlineColor = "Yellow";
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

    public string OnlineColor {
        get => _onlineColor;
        set {
            _onlineColor = value;
            RaisePropertyChangedEvent("OnlineColor");
        }
    }

    #endregion
}