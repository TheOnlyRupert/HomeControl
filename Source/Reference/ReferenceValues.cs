using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HomeControl.Source.IO;

namespace HomeControl.Source.Reference;

public static class ReferenceValues {
    public const string COPYRIGHT = "Copyright © 2022-2023  Robert Higgins";
    public const string VERSION = "1.0.0";

    public const bool EnableWeather = true;

    public static readonly List<string> CategorySpendingList = new() {
        "Alcohol",
        "Alcohol Bar",
        "Billing",
        "Brittany Fund",
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
        "Water Bill"
    };

    public static readonly List<string> CategoryProfitList = new() {
        "Child Support",
        "Gift",
        "Government",
        "Paycheck",
        "Refund"
    };

    public static readonly List<string> RecurringMonth = new() {
        "MONTHLY",
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
    };

    public static readonly string FILE_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TheOnlyRupert/HomeControl/";

    public static bool LockUI;
    public static bool IsFunnyModeActive { get; set; }

    public static DateTime CalendarEventDate { get; set; }

    public static JsonCalendar[] JsonCalendarMasterEventList { get; set; }
    public static JsonFinances JsonFinanceMasterList { get; set; }
    public static JsonRecurringFinances JsonRecurringFinanceMasterList { get; set; }
    public static JsonChores JsonChoreDayMasterList { get; set; }
    public static JsonChores JsonChoreDayUser1MasterList { get; set; }
    public static JsonChores JsonChoreWeekMasterList { get; set; }
    public static JsonChores JsonChoreWeekUser1MasterList { get; set; }
    public static JsonChores JsonChoreMonthMasterList { get; set; }
    public static JsonChores JsonChoreQuarterMasterList { get; set; }
    public static JsonChores JsonChoreQuarterUser1MasterList { get; set; }
    public static JsonChores JsonChoreMonthUser1MasterList { get; set; }
    public static JsonChores JsonChoreSpecialMasterList { get; set; }
    public static JsonChoreFunds JsonChoreFundsMaster { get; set; }
    public static JsonBehavior JsonBehaviorMaster { get; set; }
    public static DateTime ChoreWeekStartDate { get; set; }
    public static int[] TimerMinutes { get; set; }
    public static int[] TimerSeconds { get; set; }
    public static int ActiveTimerEdit { get; set; }
    public static bool IsTimerAlarmActive { get; set; }
    public static bool[] IsTimerRunning { get; set; }
    public static bool[] SwitchTimerDirection { get; set; }

    public static int ActiveBehaviorUser { get; set; }
    public static JsonSettings JsonMasterSettings { get; set; }

    public static bool IsCalendarDupeModeEnabled { get; set; }
    public static CalendarEvents DupeEvent { get; set; }

    public static bool IsGameTimerRunning { get; set; }

    public static JsonGameStats JsonGameStatsMaster { get; set; }

    public static JsonCalendarRecurring JsonCalendarRecurringMaster { get; set; }

    public static ObservableCollection<DebugTextBlock> DebugTextBlockOutput { get; set; }

    //TEMP
    //public static string DebugText { get; set; }
}