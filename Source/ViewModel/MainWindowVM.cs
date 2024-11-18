using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Windows.Threading;
using HomeControl.Source.Control;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules;
using HomeControl.Source.Modules.Finances;
using HomeControl.Source.ViewModel.Base;
using HomeControl.Source.ViewModel.Finances;
using MySql.Data.MySqlClient;
using Task = System.Threading.Tasks.Task;

namespace HomeControl.Source.ViewModel;

public class MainWindowVM : BaseViewModel {
    private readonly MySqlConnection _connection;
    private readonly CrossViewMessenger _simpleMessenger;
    private bool _changeDate, _internetMessage, _isChecking;
    private DateTime _currentDate;
    private readonly DateTime _lastChecked;
    private string _iconImage;

    private bool _isDatabaseInitialized;
    private int _trashInt, _hourInt;

    public MainWindowVM() {
        IconImage = "../../Resources/Images/icons/behavior.png";
        _simpleMessenger = CrossViewMessenger.Instance;
        _currentDate = DateTime.Now;
        _internetMessage = false;
        _lastChecked = DateTime.UtcNow;

        /* Create Documents Directory */
        Directory.CreateDirectory(ReferenceValues.DocumentsDirectory);

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
                DebugBlockList = []
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

        /* Get Finances */
        try {
            ReferenceValues.JsonFinanceMaster = JsonSerializer.Deserialize<JsonFinances>(FileHelpers.LoadFileText("finances", true));
        } catch (Exception) {
            ReferenceValues.JsonFinanceMaster = new JsonFinances {
                FinanceList = [],
                FinanceListDetailed = [],
                TotalMonthlyAmount = 0
            };

            FileHelpers.SaveFileText("finances", JsonSerializer.Serialize(ReferenceValues.JsonFinanceMaster), true);
        }

        FinanceMaths.RefreshFinances();

        /* Set Version */
        JsonVersion jsonVersion = new() {
            versionMajor = ReferenceValues.VersionMajor,
            versionMinor = ReferenceValues.VersionMinor,
            versionPatch = ReferenceValues.VersionPatch,
            versionBranch = ReferenceValues.VersionBranch
        };

        FileHelpers.SaveFileText("version", JsonSerializer.Serialize(jsonVersion), false);

        ReferenceValues.IconImageList = [];
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

        /* Do this here so I don't get null references in the future */
        ReferenceValues.JsonBehaviorMaster = new JsonBehavior {
            User1Strikes = 0,
            User2Strikes = 0,
            User3Strikes = 0,
            User4Strikes = 0,
            User5Strikes = 0,
            User1Stars = 0,
            User2Stars = 0,
            User3Stars = 0,
            User4Stars = 0,
            User5Stars = 0
        };

        /* Database Connection */
        ReferenceValues.DatabaseConnectionString = @"Server=" + ReferenceValues.JsonSettingsMaster.DatabaseHost + @";Database=HomeControl;Uid=" + ReferenceValues.JsonSettingsMaster.DatabaseUsername +
                                                   @";Pwd=" +
                                                   ReferenceValues.JsonSettingsMaster.DatabasePassword;
        _connection = new MySqlConnection(ReferenceValues.DatabaseConnectionString);
        InitializeDatabaseConnection();

        /* GameStats File */
        try {
            ReferenceValues.JsonGameStatsMaster = JsonSerializer.Deserialize<JsonGameStats>(FileHelpers.LoadFileText("gameStats", true));
        } catch (Exception) {
            ReferenceValues.JsonGameStatsMaster = new JsonGameStats();

            FileHelpers.SaveFileText("gameStats", JsonSerializer.Serialize(ReferenceValues.JsonGameStatsMaster), true);
        }

        _ = ApiStatus();

        /* COM Port */
        if (!string.IsNullOrEmpty(ReferenceValues.JsonSettingsMaster.ComPort)) {
            ReferenceValues.SerialPort = new SerialPort(ReferenceValues.JsonSettingsMaster.ComPort, 9600);
            HvacCrossPlay.EstablishConnection();
        }

        /* Global DispatcherTimer */
        DispatcherTimer dispatcherTimer = new();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();

        /* Screen Saver */
        AppActivityTimer activityTimer = new(60000, 60000, false);
        activityTimer.OnInactive += ActivityTimerOnInactive;
        activityTimer.OnActive += ActivityTimerOnActive;
        return;

        void ActivityTimerOnInactive(object sender, EventArgs e) {
            if (!ReferenceValues.JsonSettingsMaster.DebugMode) {
                _simpleMessenger.PushMessage("ScreenSaverOn", null);
                ReferenceValues.LockUi = true;
                ReferenceValues.ScreensaverMaster.Start();
            }
        }

        void ActivityTimerOnActive(object sender, EventArgs e) {
            _simpleMessenger.PushMessage("ScreenSaverOff", null);
            ReferenceValues.ScreensaverMaster.Stop();
        }
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    #region Fields

    public string IconImage {
        get => _iconImage;
        set {
            _iconImage = value;
            RaisePropertyChangedEvent("IconImage");
        }
    }

    #endregion

    private async void InitializeDatabaseConnection() {
        try {
            await _connection.OpenAsync();
            Console.WriteLine("Database connection established.");
            _isDatabaseInitialized = true;
        } catch (Exception ex) {
            Console.WriteLine($"Error opening database connection: {ex}");
            _isDatabaseInitialized = false;
        }
    }

    private void ButtonCommandLogic(object param) {
        if (!ReferenceValues.LockUi) {
            switch (param) {
            case "finances":
                EditFinances editFinances = new();
                editFinances.ShowDialog();
                editFinances.Close();

                _simpleMessenger.PushMessage("RefreshFinances", null);
                break;
            }
        } else {
            ReferenceValues.SoundToPlay = "locked";
            SoundDispatcher.PlaySound();
        }
    }

    private async void dispatcherTimer_Tick(object sender, EventArgs e) {
        _changeDate = false;
        /* Min Changes */
        if (!_currentDate.Minute.Equals(DateTime.Now.Minute)) {
            _simpleMessenger.PushMessage("MinChanged", null);

            _ = ApiStatus();
            if (!ReferenceValues.IsHvacComEstablished) {
                HvacCrossPlay.EstablishConnection();
            }

            if (ReferenceValues.JsonTasksMaster.User1Blink || ReferenceValues.JsonTasksMaster.User2Blink || ReferenceValues.JsonTasksMaster.User3Blink || ReferenceValues.JsonTasksMaster.User4Blink
                || ReferenceValues.JsonTasksMaster.User5Blink) {
                ReferenceValues.SoundToPlay = "beep";
                SoundDispatcher.PlaySound();
            }

            _changeDate = true;
        }

        /* Hour Changes */
        if (!_currentDate.Hour.Equals(DateTime.Now.Hour)) {
            _simpleMessenger.PushMessage("HourChanged", null);
            _changeDate = true;

            if (DateTime.Now.Month == 12) {
                ReferenceValues.SoundToPlay = "jingle_bells";
            } else {
                Random random = new();
                ReferenceValues.SoundToPlay = "clock" + random.Next(1, 3);
            }

            SoundDispatcher.PlaySound();

            _hourInt = 10;

            /* Trash Night Sound */
            if (DateTime.Now.DayOfWeek.ToString() == ReferenceValues.JsonSettingsMaster.TrashDay && DateTime.Now.Hour > 16) {
                _trashInt = 12;
            }
        }

        /* Date Changes */
        if (!_currentDate.Day.Equals(DateTime.Now.Day)) {
            _simpleMessenger.PushMessage("DateChanged", null);
            _changeDate = true;
        }

        /* Month Changes */
        if (!_currentDate.Month.Equals(DateTime.Now.Month)) {
            _simpleMessenger.PushMessage("MonthChanged", null);
            _changeDate = true;
        }

        if (_changeDate) {
            _currentDate = DateTime.Now;
            _changeDate = false;
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
            _simpleMessenger.PushMessage("RefreshTimer", null);
        }

        if (ReferenceValues.JsonTimerMaster.IsAlarmSounding) {
            ReferenceValues.SoundToPlay = "timerDone";
            SoundDispatcher.PlaySound();
        }

        /* HVAC Logic */
        ReferenceValues.HvacStateTime++;

        /* Check Trash */
        if (_trashInt > 0) {
            _trashInt--;
            if (_trashInt == 0) {
                ReferenceValues.SoundToPlay = "trash";
                SoundDispatcher.PlaySound();
            }
        }

        if (_hourInt > 0) {
            _hourInt--;
            if (_hourInt == 0) {
                ReferenceValues.SoundToPlay = "hour" + DateTime.Now.Hour;
                SoundDispatcher.PlaySound();
            }
        }

        /* Check Database Changes */
        await CheckForDatabaseChanges();

        _simpleMessenger.PushMessage("Refresh", null);
    }

    private async Task CheckForDatabaseChanges() {
        if (_isChecking || !_isDatabaseInitialized) {
            return;
        }

        _isChecking = true;

        if (_connection.State != ConnectionState.Open) {
            Console.WriteLine("Database connection is null.");
            return;
        }

        if (_connection == null) {
            Console.WriteLine("Database connection is null.");
            return;
        }

        const string query = @"SELECT id, table_name, action_type, affected_id, created_at FROM notifications WHERE created_at > @LastChecked ORDER BY created_at ASC";

        try {
            await using MySqlCommand command = new(query, _connection);
            command.Parameters.AddWithValue("@LastChecked", _lastChecked);

            using DbDataReader reader = await command.ExecuteReaderAsync();
            if (reader == null) {
                Console.WriteLine("No data returned from the query.");
                return;
            }

            DateTime latestTimestamp = _lastChecked;

            while (await reader.ReadAsync()) {
                string tableName = reader.GetString(1);
                string actionType = reader.GetString(2);
                int affectedId = reader.GetInt32(3);
                DateTime createdAt = reader.GetDateTime(4);

                // Process the notification
                Console.WriteLine($"Change detected: {tableName} - {actionType} on ID {affectedId}");

                // Update the latest timestamp
                if (createdAt > latestTimestamp) {
                    latestTimestamp = createdAt;
                }
            }

            //_lastChecked = latestTimestamp;
        } catch (Exception ex) {
            Console.WriteLine($"Error polling notifications: {ex}");
        }
        finally {
            _isChecking = false;
        }
    }

    private async Task ApiStatus() {
        JsonSerializerOptions options = new() {
            IncludeFields = true,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        try {
            Uri api = new("https://api.weather.gov/");

            using WebClient client = new();
            client.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonSettingsMaster.UserAgent);
            string apiStatusString = await client.DownloadStringTaskAsync(api);
            ApiStatus apiStatus = JsonSerializer.Deserialize<ApiStatus>(apiStatusString, options);

            if (apiStatus is { status: "OK" }) {
                ReferenceValues.IsWeatherApiOnline = true;
                _simpleMessenger.PushMessage("UpdateInternetStatus", null);

                if (_internetMessage) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "MainWindowVM",
                        Description = "Restored Internet Connection"
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                _internetMessage = false;
            } else {
                LostInternet();
            }
        } catch (Exception) {
            LostInternet();
        }
    }

    private void LostInternet() {
        if (!_internetMessage) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "MainWindowVM",
                Description = "Lost Internet Connection"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            _internetMessage = true;
            ReferenceValues.IsWeatherApiOnline = false;
            _simpleMessenger.PushMessage("UpdateInternetStatus", null);
        }
    }
}