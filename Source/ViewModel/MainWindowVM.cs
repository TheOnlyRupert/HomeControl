using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Threading;
using HomeControl.Source.Control;
using HomeControl.Source.IO;
using HomeControl.Source.Modules;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

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

        ReferenceValues.DebugTextBlockOutput = new ObservableCollection<DebugTextBlock> {
            new() {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "MainWindowVM",
                Description = ReferenceValues.COPYRIGHT + "  Version: " + ReferenceValues.VERSION
            }
        };

        /* Get Settings */
        new SettingsFromJson();

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
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "MainWindowVM",
                Description = "Starting Screen Saver"
            });
            simpleMessenger.PushMessage("ScreenSaverOn", null);
            ReferenceValues.LockUI = true;
            OnlineColor = "Black";
        }

        void activityTimer_OnActive(object sender, EventArgs e) {
            simpleMessenger.PushMessage("ScreenSaverOff", null);
            OnlineColor = "Transparent";
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
        } else {
            OnlineColor = "Transparent";
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
                OnlineColor = "Transparent";
                if (internetMessage) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "MainWindowVM",
                        Description = "Restored Internet Connection"
                    });
                }

                internetMessage = false;
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
            internetMessage = true;
            OnlineColor = "Red";
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