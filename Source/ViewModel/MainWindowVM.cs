using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.Json;
using System.Windows.Input;
using System.Windows.Threading;
using HomeControl.Source.Control;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules;
using HomeControl.Source.Modules.Finances;
using HomeControl.Source.ViewModel.Base;
using HomeControl.Source.ViewModel.Games.Tamagotchi;
using HomeControl.Source.ViewModel.Hvac;
using Task = System.Threading.Tasks.Task;

namespace HomeControl.Source.ViewModel;

public class MainWindowVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;
    private string _iconImage, _onlineColor, _christmasVisibility;
    private bool changeDate, internetMessage;
    private DateTime currentDate;
    private int trashInt;

    public MainWindowVM() {
        IconImage = "../../Resources/Images/icons/behavior.png";
        simpleMessenger = CrossViewMessenger.Instance;
        currentDate = DateTime.Now;
        internetMessage = false;
        OnlineColor = "Black";

        /* Create Documents Directory */
        Directory.CreateDirectory(ReferenceValues.DOCUMENTS_DIRECTORY);

        /* Create App Directory */
        try {
            ReferenceValues.AppDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location + "/");
            if (ReferenceValues.AppDirectory != null) {
                ReferenceValues.AppDirectory = ReferenceValues.AppDirectory.Substring(0, ReferenceValues.AppDirectory.Length - 15);
            }
        } catch (Exception) {
            ReferenceValues.AppDirectory = Environment.CurrentDirectory;
        }

        /* Get Debug (MAKE SURE THIS IS FIRST!) */
        try {
            ReferenceValues.JsonDebugMaster = JsonSerializer.Deserialize<JsonDebug>(FileHelpers.LoadFileText("debug", true));
        } catch (Exception) {
            ReferenceValues.JsonDebugMaster = new JsonDebug {
                DebugBlockList = new ObservableCollection<DebugTextBlock>()
            };

            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        /* Get Settings */
        try {
            ReferenceValues.JsonSettingsMaster = JsonSerializer.Deserialize<JsonSettings>(FileHelpers.LoadFileText("settings", true));
        } catch (Exception) {
            ReferenceValues.JsonSettingsMaster = new JsonSettings();

            FileHelpers.SaveFileText("settings", JsonSerializer.Serialize(ReferenceValues.JsonSettingsMaster), true);
        }

        /* Set Version */
        JsonVersion jsonVersion = new() {
            versionMajor = ReferenceValues.VERSION_MAJOR,
            versionMinor = ReferenceValues.VERSION_MINOR,
            versionPatch = ReferenceValues.VERSION_PATCH,
            versionBranch = ReferenceValues.VERSION_BRANCH
        };

        FileHelpers.SaveFileText("version", JsonSerializer.Serialize(jsonVersion), false);

        ReferenceValues.IconImageList = new ObservableCollection<string>();
        ResourceManager resourceManager = new("HomeControl.g", Assembly.GetExecutingAssembly());
        ResourceSet resources = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        foreach (string key in from object res in resources select ((DictionaryEntry)res).Key.ToString() into key where key.Contains("resources/images/icons/") select key) {
            ReferenceValues.IconImageList.Add(key.Substring(23, key.Length - 27));
        }

        ReferenceValues.IconImageList = new ObservableCollection<string>(ReferenceValues.IconImageList.OrderBy(i => i));

        if (string.IsNullOrEmpty(ReferenceValues.JsonSettingsMaster.UserAgent)) {
            Settings settingsDialog = new();
            settingsDialog.ShowDialog();
            settingsDialog.Close();
        }

        ApiStatus();

        if (!string.IsNullOrEmpty(ReferenceValues.JsonSettingsMaster.ComPort)) {
            ReferenceValues.SerialPort = new SerialPort(ReferenceValues.JsonSettingsMaster.ComPort, 9600);
            HvacCrossPlay.EstablishConnection();
        }

        UpdateChristmasVisibility();

        /* Global DispatcherTimer */
        DispatcherTimer dispatcherTimer = new();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();

        /* Screen Saver */
        AppActivityTimer activityTimer = new(60000, 60000, false);
        activityTimer.OnInactive += activityTimer_OnInactive;
        activityTimer.OnActive += activityTimer_OnActive;
        return;

        void activityTimer_OnInactive(object sender, EventArgs e) {
            if (!ReferenceValues.JsonSettingsMaster.DebugMode) {
                simpleMessenger.PushMessage("ScreenSaverOn", null);
                ReferenceValues.LockUI = true;
                ReferenceValues.ScreensaverMaster.Start();
            }
        }

        void activityTimer_OnActive(object sender, EventArgs e) {
            simpleMessenger.PushMessage("ScreenSaverOff", null);
            ReferenceValues.ScreensaverMaster.Stop();
        }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        if (!ReferenceValues.LockUI) {
            switch (param) {
            case "finances":
                EditFinances editFinances = new();
                editFinances.ShowDialog();
                editFinances.Close();

                simpleMessenger.PushMessage("RefreshFinances", null);
                break;
            }
        } else {
            ReferenceValues.SoundToPlay = "locked";
            SoundDispatcher.PlaySound();
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

            if (ReferenceValues.JsonTasksMaster.User1Blink || ReferenceValues.JsonTasksMaster.User2Blink || ReferenceValues.JsonTasksMaster.User3Blink || ReferenceValues.JsonTasksMaster.User4Blink
                || ReferenceValues.JsonTasksMaster.User5Blink) {
                ReferenceValues.SoundToPlay = "beep";
                SoundDispatcher.PlaySound();
            }

            changeDate = true;
        }

        /* Hour Changes */
        if (!currentDate.Hour.Equals(DateTime.Now.Hour)) {
            simpleMessenger.PushMessage("HourChanged", null);
            changeDate = true;

            if (DateTime.Now.Month == 12) {
                ReferenceValues.SoundToPlay = "jingle_bells";
                SoundDispatcher.PlaySound();
            } else {
                Random random = new();
                ReferenceValues.SoundToPlay = "clock" + random.Next(1, 3);
                SoundDispatcher.PlaySound();
            }

            /* Trash Night Sound */
            if (DateTime.Now.DayOfWeek.ToString() == ReferenceValues.AdjustedTrashDay && DateTime.Now.Hour > 11) {
                trashInt = 10;
            }
        }

        /* Date Changes */
        if (!currentDate.Day.Equals(DateTime.Now.Day)) {
            simpleMessenger.PushMessage("DateChanged", null);
            changeDate = true;
            UpdateChristmasVisibility();
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

        if (ReferenceValues.JsonTimerMaster.IsTimer1Running) {
            ReferenceValues.JsonTimerMaster.Timer1Seconds--;

            if (ReferenceValues.JsonTimerMaster.Timer1Seconds == 0) {
                ReferenceValues.JsonTimerMaster.IsAlarmSounding = true;
            }
        }

        if (ReferenceValues.JsonTimerMaster.IsTimer2Running) {
            ReferenceValues.JsonTimerMaster.Timer2Seconds--;

            if (ReferenceValues.JsonTimerMaster.Timer2Seconds == 0) {
                ReferenceValues.JsonTimerMaster.IsAlarmSounding = true;
            }
        }

        if (ReferenceValues.JsonTimerMaster.IsTimer3Running) {
            ReferenceValues.JsonTimerMaster.Timer3Seconds--;

            if (ReferenceValues.JsonTimerMaster.Timer3Seconds == 0) {
                ReferenceValues.JsonTimerMaster.IsAlarmSounding = true;
            }
        }

        if (ReferenceValues.JsonTimerMaster.IsTimer4Running) {
            ReferenceValues.JsonTimerMaster.Timer4Seconds--;

            if (ReferenceValues.JsonTimerMaster.Timer4Seconds == 0) {
                ReferenceValues.JsonTimerMaster.IsAlarmSounding = true;
            }
        }

        if (ReferenceValues.JsonTimerMaster.IsTimer1Running || ReferenceValues.JsonTimerMaster.IsTimer2Running || ReferenceValues.JsonTimerMaster.IsTimer3Running ||
            ReferenceValues.JsonTimerMaster.IsTimer4Running) {
            simpleMessenger.PushMessage("RefreshTimer", null);
        }

        if (ReferenceValues.JsonTimerMaster.IsAlarmSounding) {
            ReferenceValues.SoundToPlay = "timerDone";
            SoundDispatcher.PlaySound();
        }

        /* Tick Tamagotchi Every Second */
        TamagotchiLogic.TickLogic();
        ReferenceValues.HvacStateTime++;

        /* Check Trash */
        if (trashInt > 0) {
            trashInt--;
            if (trashInt == 0) {
                ReferenceValues.SoundToPlay = "trash";
                SoundDispatcher.PlaySound();
            }
        }

        simpleMessenger.PushMessage("Refresh", null);
    }

    private void UpdateChristmasVisibility() {
        if (DateTime.Now.Month == 12 && DateTime.Now.Day < 26) {
            ChristmasVisibility = "VISIBLE";
        } else {
            ChristmasVisibility = "COLLAPSED";
        }
    }

    private async Task ApiStatus() {
        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            Uri api = new("https://api.weather.gov/");

            using WebClient client = new();
            client.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonSettingsMaster.UserAgent);
            string apiStatusString = await client.DownloadStringTaskAsync(api);
            ApiStatus apiStatus = JsonSerializer.Deserialize<ApiStatus>(apiStatusString, options);

            if (apiStatus is { status: "OK" }) {
                if (internetMessage) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "MainWindowVM",
                        Description = "Restored Internet Connection"
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
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
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "MainWindowVM",
                Description = "Lost Internet Connection"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
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

    public string ChristmasVisibility {
        get => _christmasVisibility;
        set {
            _christmasVisibility = value;
            RaisePropertyChangedEvent("ChristmasVisibility");
        }
    }

    #endregion
}