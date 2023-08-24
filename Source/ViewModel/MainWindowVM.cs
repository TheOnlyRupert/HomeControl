using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
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

        ReferenceValues.IconImageList = new ObservableCollection<string>();
        ResourceManager resourceManager = new("HomeControl.g", Assembly.GetExecutingAssembly());
        ResourceSet resources = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        foreach (string key in from object res in resources select ((DictionaryEntry)res).Key.ToString() into key where key.Contains("resources/images/chores/") select key) {
            ReferenceValues.IconImageList.Add(key.Substring(24, key.Length - 28));
        }

        ReferenceValues.IconImageList = new ObservableCollection<string>(ReferenceValues.IconImageList.OrderBy(i => i));

        if (string.IsNullOrEmpty(ReferenceValues.JsonMasterSettings.UserAgent)) {
            Settings settingsDialog = new();
            settingsDialog.ShowDialog();
            settingsDialog.Close();
        }

        ApiStatus();

        /* HVAC Serial Port */
        new HvacFromJson();

        if (!string.IsNullOrEmpty(ReferenceValues.JsonMasterSettings.ComPort)) {
            ReferenceValues.SerialPortMaster = new SerialPort(ReferenceValues.JsonMasterSettings.ComPort, 9600);
            HvacCrossPlay.EstablishConnection();
        }

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

        if (ReferenceValues.JsonTimerSettings.IsTimer1Running) {
            ReferenceValues.JsonTimerSettings.Timer1Seconds--;

            if (ReferenceValues.JsonTimerSettings.Timer1Seconds == 0) {
                ReferenceValues.JsonTimerSettings.IsAlarmSounding = true;
            }
        }

        if (ReferenceValues.JsonTimerSettings.IsTimer2Running) {
            ReferenceValues.JsonTimerSettings.Timer2Seconds--;

            if (ReferenceValues.JsonTimerSettings.Timer2Seconds == 0) {
                ReferenceValues.JsonTimerSettings.IsAlarmSounding = true;
            }
        }

        if (ReferenceValues.JsonTimerSettings.IsTimer3Running) {
            ReferenceValues.JsonTimerSettings.Timer3Seconds--;

            if (ReferenceValues.JsonTimerSettings.Timer3Seconds == 0) {
                ReferenceValues.JsonTimerSettings.IsAlarmSounding = true;
            }
        }

        if (ReferenceValues.JsonTimerSettings.IsTimer4Running) {
            ReferenceValues.JsonTimerSettings.Timer4Seconds--;

            if (ReferenceValues.JsonTimerSettings.Timer4Seconds == 0) {
                ReferenceValues.JsonTimerSettings.IsAlarmSounding = true;
            }
        }

        if (ReferenceValues.JsonTimerSettings.IsTimer1Running || ReferenceValues.JsonTimerSettings.IsTimer2Running || ReferenceValues.JsonTimerSettings.IsTimer3Running ||
            ReferenceValues.JsonTimerSettings.IsTimer4Running) {
            simpleMessenger.PushMessage("RefreshTimer", null);
        }

        if (ReferenceValues.JsonTimerSettings.IsAlarmSounding) {
            ReferenceValues.SoundToPlay = "timerDone";
            SoundDispatcher.PlaySound();
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