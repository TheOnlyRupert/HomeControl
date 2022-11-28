using System;
using System.Collections.Generic;
using HomeControl.Source.Helpers;

namespace HomeControl.Source.Reference;

public static class ReferenceValues {
    public const string COPYRIGHT = "Copyright © 2022  Robert Higgins";
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
        "Stupid/Dumb",
        "Interest",
        "Other"
    };

    public static readonly List<string> CategoryProfitList = new() {
        "Paycheck",
        "Gift",
        "Government",
        "Child Support"
    };

    public static readonly string FILE_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TheOnlyRupert/HomeControl/";

    public static string UserAgent { get; set; }

    public static DateTime CalendarEventDate { get; set; }

    public static JsonCalendar[] JsonCalendarMasterEventList { get; set; }

    public static JsonFinances JsonFinanceMasterList { get; set; }

    public static string User1Name { get; set; }
    public static string User2Name { get; set; }

    public static string Child1Name { get; set; }
    public static string Child2Name { get; set; }
    public static string Child3Name { get; set; }
    public static int Child1Progress { get; set; }
    public static int Child2Progress { get; set; }
    public static int Child3Progress { get; set; }
    public static int Child1Stars { get; set; }
    public static int Child2Stars { get; set; }
    public static int Child3Stars { get; set; }
    public static int Child1Strikes { get; set; }
    public static int Child2Strikes { get; set; }
    public static int Child3Strikes { get; set; }


    public static int ChoresWeekCompleted { get; set; }
    public static int ChoresMonthCompleted { get; set; }
}