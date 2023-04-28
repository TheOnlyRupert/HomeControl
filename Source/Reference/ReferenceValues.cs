using System;
using System.Collections.Generic;
using HomeControl.Source.Control;
using HomeControl.Source.IO;

namespace HomeControl.Source.Reference;

public static class ReferenceValues {
    public const string COPYRIGHT = "Copyright © 2022-2023  Robert Higgins";
    public const string VERSION = "1.0.0";

    public const bool EnableWeather = true;

    public static readonly List<string> CategorySpendingList = new() {
        "Billing",
        "Grocery",
        "Petrol",
        "Takeout",
        "Shopping",
        "Health",
        "Travel",
        "Coffee",
        "Entertainment",
        "Services",
        "Personal Care",
        "Home Improvement",
        "Alcohol",
        "Firearms",
        "Streaming Service",
        "Brittany Fund",
        "Stupid/Dumb",
        "Interest",
        "Carry Over",
        "Other"
    };

    public static readonly List<string> CategoryProfitList = new() {
        "Paycheck",
        "Gift",
        "Government",
        "Child Support",
        "Refund",
        "Other"
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
    public static PlaySound TimerSound { get; set; }

    public static int ActiveBehaviorUser { get; set; }
    public static JsonSettings JsonMasterSettings { get; set; }
    public static bool IsScreenSaverEnabled { get; set; }

    public static bool IsCalendarDupeModeEnabled { get; set; }
    public static CalendarEvents DupeEvent { get; set; }

    public static bool IsGameTimerRunning { get; set; }

    public static string DebugText { get; set; }

    public static JsonGameStats JsonGameStatsMaster { get; set; }
}