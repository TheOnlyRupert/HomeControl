﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using HomeControl.Source.Control;
using HomeControl.Source.Json;

namespace HomeControl.Source;

public static class ReferenceValues {
    #region Enums

    public enum HvacModes {
        Off,
        Running,
        Standby,
        Purging
    }

    #endregion

    public const string COPYRIGHT = "Copyright © 2022-2023  Robert Higgins";
    public const int VERSION_MAJOR = 1;
    public const int VERSION_MINOR = 5;
    public const int VERSION_PATCH = 0;
    public const string VERSION_BRANCH = "release";

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
        "Government",
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
        "Investment",
        "Selling Assets",
        "Other"
    };

    public static readonly List<string> CategoryTaskList = new() {
        "User1 Fund",
        "User2 Fund",
        "User3 Fund",
        "User4 Fund",
        "User5 Fund"
    };

    public static readonly string DOCUMENTS_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TheOnlyRupert/HomeControl/";

    public static bool LockUI;

    public static Screensaver ScreensaverMaster;

    public static SerialPort SerialPort { get; set; }

    public static DateTime CalendarEventDate { get; set; }

    public static JsonCalendar JsonCalendarMaster { get; set; }
    public static JsonFinances JsonFinanceMaster { get; set; }
    public static JsonBehavior JsonBehaviorMaster { get; set; }
    public static DateTime TaskWeekStartDate { get; set; }

    public static int ActiveBehaviorUser { get; set; }
    public static JsonSettings JsonSettingsMaster { get; set; }

    public static double TemperatureInside { get; set; }
    public static int HumidityInside { get; set; }

    public static HvacModes HvacMode { get; set; }

    /* Ranges from 15°C / 59°F to 30°C / 86°F */
    public static double TemperatureSet { get; set; }

    /* On or auto... No other option */
    public static bool IsFanAuto { get; set; }
    public static bool IsHeatingMode { get; set; }
    public static bool IsProgramRunning { get; set; }
    public static bool IsHvacComEstablished { get; set; }
    public static int HvacStateRunTime { get; set; }

    public static bool IsCalendarDupeModeEnabled { get; set; }
    public static CalendarEvents DupeEvent { get; set; }

    public static bool IsGameTimerRunning { get; set; }

    public static JsonGameStats JsonGameStatsMaster { get; set; }

    public static JsonDebug JsonDebugMaster { get; set; }

    public static string SoundToPlay { get; set; }
    public static JsonTasks JsonTasksMaster { get; set; }
    public static JsonExercise JsonExerciseMaster { get; set; }
    public static ObservableCollection<string> IconImageList { get; set; }

    public static JsonTimer JsonTimerMaster { get; set; }

    public static string AppDirectory { get; set; }
    public static Tamagotchi TamagotchiMaster { get; set; }

    public static JsonWeather ForecastSevenDay { get; set; }
    public static JsonWeather ForecastHourly { get; set; }
    public static string AdjustedTrashDay { get; set; }

    public static bool[] CalendarFilterList { get; set; }
}