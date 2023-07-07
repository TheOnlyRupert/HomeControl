using System;
using System.IO.Ports;
using System.Net;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Threading;
using HomeControl.Source.Control;
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

        /* Get Debug */
        new DebugFromJson();

        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = "MainWindowVM",
            Description = ReferenceValues.COPYRIGHT + "  Version: " + ReferenceValues.VERSION
        });
        SaveDebugFile.Save();

        /* Get Settings */
        new SettingsFromJson();

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
            simpleMessenger.PushMessage("ScreenSaverOn", null);
            ReferenceValues.LockUI = true;
            OnlineColor = internetMessage ? "DarkRed" : "Black";
        }

        void activityTimer_OnActive(object sender, EventArgs e) {
            simpleMessenger.PushMessage("ScreenSaverOff", null);
            if (!ReferenceValues.IsFunnyModeActive) {
                OnlineColor = internetMessage ? "DarkRed" : "Black";
            }
        }

        /* Joke DispatcherTimer */
        DispatcherTimer dispatcherTimer2 = new();
        dispatcherTimer2.Tick += dispatcherTimer_Tick2;
        dispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 0, 200);
        dispatcherTimer2.Start();
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

    private void dispatcherTimer_Tick2(object sender, EventArgs e) {
        if (ReferenceValues.IsFunnyModeActive) {
            Random randomNumber = new();
            Color myColor = Color.FromRgb((byte)randomNumber.Next(256), (byte)randomNumber.Next(256), (byte)randomNumber.Next(256));
            OnlineColor = "#FF" + myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
        }
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
            OnlineColor = "DarkRed";
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