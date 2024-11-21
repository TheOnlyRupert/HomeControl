using System.Collections.ObjectModel;
using System.IO.Ports;
using HomeControl.Source.Control;
using HomeControl.Source.Json;
using HvacStates = HomeControl.Source.Json.HvacStates;

namespace HomeControl.Source;

public static class ReferenceValues {
    public const string Copyright = "Copyright © 2022-2024  Robert Higgins";
    public const int VersionMajor = 1;
    public const int VersionMinor = 5;
    public const int VersionPatch = 0;
    public const string VersionBranch = "release";

    public static readonly string DocumentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TheOnlyRupert/HomeControl/";

    public static bool LockUi;
    public static Screensaver ScreensaverMaster;

    public static SerialPort SerialPort { get; set; }

    public static DateTime CalendarEventDate { get; set; }

    public static JsonCalendar JsonCalendarMaster { get; set; }
    public static JsonFinances JsonFinanceMaster { get; set; }
    public static JsonBehavior JsonBehaviorMaster { get; set; }
    public static JsonHvac? JsonHvacMaster { get; set; }

    public static int ActiveBehaviorUser { get; set; }
    public static JsonSettings JsonSettingsMaster { get; set; }

    public static double TemperatureInside { get; set; }
    public static float HumidityInside { get; set; }

    public static HvacStates HvacState { get; set; }

    /* Ranges from 15°C (59°F) to 30°C (86°F) */
    public static double TemperatureSet { get; set; }

    public static bool IsFanAuto { get; set; }
    public static bool IsHeatingMode { get; set; }
    public static bool UseSchedule { get; set; }
    public static bool IsProgramRunning { get; set; }
    public static bool IsHvacComEstablished { get; set; }
    public static int HvacStateTime { get; set; }

    public static bool IsCalendarDupeModeEnabled { get; set; }
    public static CalendarEvents DupeEvent { get; set; }

    public static bool IsGameTimerRunning { get; set; }

    public static JsonGameStats JsonGameStatsMaster { get; set; }

    public static JsonDebug JsonDebugMaster { get; set; }
    public static JsonTasks JsonTasksMaster { get; set; }
    public static JsonExercise JsonExerciseMaster { get; set; }
    public static ObservableCollection<string> IconImageList { get; set; }

    public static JsonTimer JsonTimerMaster { get; set; }

    public static string AppDirectory { get; set; }
    public static Tamagotchi TamagotchiMaster { get; set; }

    public static JsonWeather ForecastSevenDay { get; set; }
    public static JsonWeather? ForecastHourly { get; set; }

    public static bool[] CalendarFilterList { get; set; }

    public static bool IsWeatherApiOnline { get; set; }
    public static string DatabaseConnectionString { get; set; }
    public static bool DisableBehaviorPolling { get; set; }
}