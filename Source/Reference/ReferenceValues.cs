using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using HomeControl.Source.IO;

namespace HomeControl.Source.Reference;

public static class ReferenceValues {
    public const string COPYRIGHT = "Copyright © 2022-2023  Robert Higgins";
    public const string VERSION = "1.0.0";

    public const bool EnableWeather = true;

    public static readonly List<string> CategorySpendingList = new() {
        "Alcohol",
        "Animals/Pets",
        "Billing",
        "Carry Over",
        "Child Care",
        "Coffee",
        "Electric Bill",
        "Entertainment",
        "Firearms",
        "Gas Bill",
        "Grocery",
        "Health",
        "Home Improvement",
        "Insurance",
        "Interest",
        "Internet Bill",
        "Mortgage/Rent",
        "Personal Care",
        "Petrol",
        "Phone Bill",
        "Restaurant/Takeout",
        "Services",
        "Shopping",
        "Streaming Service",
        "Stupid/Dumb",
        "Trash Bill",
        "Travel",
        "Vehicle Payment",
        "Water Bill",
        "User1 Fund",
        "User2 Fund",
        "User3 Fund",
        "User4 Fund",
        "User5 Fund"
    };

    public static readonly List<string> CategoryProfitList = new() {
        "Child Support",
        "Gift",
        "Government",
        "Paycheck",
        "Refund"
    };

    public static readonly string FILE_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TheOnlyRupert/HomeControl/";

    public static bool LockUI;

    public static SerialPort SerialPortMaster { get; set; }

    public static int InteriorTemp { get; set; }
    public static int InteriorHumidity { get; set; }
    public static int ExteriorTemp { get; set; }
    public static int ExteriorHumidity { get; set; }

    public static DateTime CalendarEventDate { get; set; }

    public static JsonCalendar[] JsonCalendarMasterEventList { get; set; }
    public static JsonFinances JsonFinanceMasterList { get; set; }
    public static JsonBehavior JsonBehaviorMaster { get; set; }
    public static DateTime TaskWeekStartDate { get; set; }
    public static int[] TimerMinutes { get; set; }
    public static int[] TimerSeconds { get; set; }
    public static int ActiveTimerEdit { get; set; }
    public static bool IsTimerAlarmActive { get; set; }
    public static bool[] IsTimerRunning { get; set; }
    public static bool[] SwitchTimerDirection { get; set; }

    public static int ActiveBehaviorUser { get; set; }
    public static JsonSettings JsonMasterSettings { get; set; }

    public static JsonHvac JsonHvacSettings { get; set; }
    public static bool IsHvacComEstablished { get; set; }

    public static bool IsCalendarDupeModeEnabled { get; set; }
    public static CalendarEvents DupeEvent { get; set; }

    public static bool IsGameTimerRunning { get; set; }

    public static JsonGameStats JsonGameStatsMaster { get; set; }

    public static JsonCalendarRecurring JsonCalendarRecurringMaster { get; set; }

    public static ObservableCollection<DebugTextBlock> DebugTextBlockOutput { get; set; }

    public static JsonRecipe JsonRecipesMaster { get; set; }
    public static string SoundToPlay { get; set; }
    public static JsonTasks JsonTasksMaster { get; set; }
    public static ObservableCollection<string> IconImageList { get; set; }
}