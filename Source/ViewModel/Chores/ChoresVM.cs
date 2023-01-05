﻿using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Chores;
using HomeControl.Source.ViewModel.Base;
using static HomeControl.Source.Reference.ReferenceValues;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresVM : BaseViewModel {
    private string _currentMonthText, _currentWeekText, _currentDayText, _choresCompletedDayText, _choresCompletedWeekText, _choresCompletedMonthText, _user1Text,
        _choresCompletedWeekProgressText, _projectedFundMonthText, _choresCompletedDayProgressText, _user2Text, _choresCompletedMonthProgressText,
        _choresCompletedSpecialProgressText, _dayButtonColor, _weekButtonColor, _monthButtonColor, _specialButtonColor, _choresCompletedSpecialText, _cashAvailable,
        _fundsProgressText, _fundsExpire, _specialDay1Text, _specialDay2Text, _currentModeText, _choresCompletedDayTextUser1, _choresCompletedWeekTextUser1,
        _choresCompletedMonthTextUser1, _choresCompletedSpecialTextUser1, _choresCompletedDayProgressTextUser1, _choresCompletedWeekProgressTextUser1,
        _choresCompletedMonthProgressTextUser1, _choresCompletedSpecialProgressTextUser1, _dayButtonColorUser1, _weekButtonColorUser1, _monthButtonColorUser1,
        _specialButtonColorUser1;

    private int choresCompletedDay, choresCompletedWeek, choresCompletedMonth, choresCompletedSpecial, choresCompletedDayUser1, choresCompletedWeekUser1, choresCompletedMonthUser1,
        choresCompletedSpecialUser1, _choresCompletedDayProgressValue, _choresCompletedWeekProgressValue, _choresCompletedDayProgressValueUser1,
        _choresCompletedWeekProgressValueUser1, _choresCompletedMonthProgressValueUser1, _choresCompletedSpecialProgressValueUser1,
        _choresCompletedMonthProgressValue, _choresCompletedSpecialProgressValue, calculatedReleaseFunds, _fundsProgressValue;

    public ChoresVM() {
        RefreshFields();

        SpecialDay1Text = "Not Complete";
        SpecialDay2Text = "Not Complete";
        CurrentModeText = "Normal";

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            RefreshFields();
        }

        if (e.PropertyName == "HourChanged") {
            RefreshCountdown();
        }

        if (e.PropertyName == "MonthChanged") {
            ReleaseFunds();
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "choresDay":
            ChoresDay choresDay = new();
            choresDay.ShowDialog();
            choresDay.Close();

            RefreshFields();
            break;
        case "choresWeek":
            ChoresWeek choresWeek = new();
            choresWeek.ShowDialog();
            choresWeek.Close();

            RefreshFields();
            break;
        case "choresMonth":
            ChoresMonth choresMonth = new();
            choresMonth.ShowDialog();
            choresMonth.Close();

            RefreshFields();
            break;
        case "choresSpecial":
            ChoresSpecial choresSpecial = new();
            choresSpecial.ShowDialog();
            choresSpecial.Close();

            RefreshFields();
            break;
        case "funds":
            ChoresFunds choresFunds = new();
            choresFunds.ShowDialog();
            choresFunds.Close();
            break;


        case "choresDayUser1":
            ChoresDayUser1 choresDayUser1 = new();
            choresDayUser1.ShowDialog();
            choresDayUser1.Close();

            RefreshFields();
            break;
        case "choresWeekUser1":
            ChoresWeekUser1 choresWeekUser1 = new();
            choresWeekUser1.ShowDialog();
            choresWeekUser1.Close();

            RefreshFields();
            break;
        case "choresMonthUser1":
            ChoresMonthUser1 choresMonthUser1 = new();
            choresMonthUser1.ShowDialog();
            choresMonthUser1.Close();

            RefreshFields();
            break;
        case "choresSpecialUser1":
            ChoresSpecialUser1 choresSpecialUser1 = new();
            choresSpecialUser1.ShowDialog();
            choresSpecialUser1.Close();

            RefreshFields();
            break;
        }
    }

    private void RefreshFields() {
        ChoreWeekStartDate = DateTime.Now;
        ChoreMonthStartDate = DateTime.Now;
        DateTime dateTime = DateTime.Now;
        DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        System.Globalization.Calendar calendar = dateTimeFormatInfo.Calendar;

        choresCompletedDay = 0;
        choresCompletedWeek = 0;
        choresCompletedMonth = 0;
        choresCompletedSpecial = 0;
        choresCompletedDayUser1 = 0;
        choresCompletedWeekUser1 = 0;
        choresCompletedMonthUser1 = 0;
        choresCompletedSpecialUser1 = 0;

        while (ChoreWeekStartDate.DayOfWeek != DayOfWeek.Sunday) {
            ChoreWeekStartDate = ChoreWeekStartDate.AddDays(-1);
        }

        ChoresFromJson choresFromJson = new();
        choresFromJson.ChoresDayFromJson(ChoreMonthStartDate);
        choresFromJson.ChoresWeekFromJson(ChoreWeekStartDate);
        choresFromJson.ChoresMonthFromJson(ChoreMonthStartDate);
        choresFromJson.ChoresSpecialFromJson(ChoreMonthStartDate);

        CurrentDayText = dateTime.ToString("dddd");
        CurrentMonthText = dateTime.ToString("MMMM");
        CurrentWeekText = "Week: " + calendar.GetWeekOfYear(dateTime, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);

        User1Text = JsonMasterSettings.User1Name;
        User2Text = JsonMasterSettings.User2Name;

        ProjectedFundMonthText = dateTime.AddMonths(1).ToString("MMM") + " Projected Funds";
        ChoresCompletedWeekProgressText = "0%";
        ChoresCompletedMonthProgressText = "0%";
        ChoresCompletedDayProgressText = "0%";
        ChoresCompletedSpecialProgressText = "0%";

        foreach (ChoreDetails choreDetails in JsonChoreDayMasterList.choreList) {
            if (choreDetails.IsComplete) {
                choresCompletedDay++;
            }
        }

        foreach (ChoreDetails choreDetails in JsonChoreWeekMasterList.choreList) {
            if (choreDetails.IsComplete) {
                choresCompletedWeek++;
            }
        }

        foreach (ChoreDetails choreDetails in JsonChoreMonthMasterList.choreList) {
            if (choreDetails.IsComplete) {
                choresCompletedMonth++;
            }
        }

        foreach (ChoreDetails choreDetails in JsonChoreSpecialMasterList.choreList) {
            if (choreDetails.IsComplete) {
                choresCompletedSpecial++;
            }
        }

        foreach (ChoreDetails choreDetails in JsonChoreDayUser1MasterList.choreList) {
            if (choreDetails.IsComplete) {
                choresCompletedDayUser1++;
            }
        }

        foreach (ChoreDetails choreDetails in JsonChoreWeekUser1MasterList.choreList) {
            if (choreDetails.IsComplete) {
                choresCompletedWeekUser1++;
            }
        }

        foreach (ChoreDetails choreDetails in JsonChoreMonthUser1MasterList.choreList) {
            if (choreDetails.IsComplete) {
                choresCompletedMonthUser1++;
            }
        }

        foreach (ChoreDetails choreDetails in JsonChoreSpecialUser1MasterList.choreList) {
            if (choreDetails.IsComplete) {
                choresCompletedSpecialUser1++;
            }
        }

        ChoresCompletedDayText = choresCompletedDay + " / " + JsonChoreDayMasterList.choreList.Count;
        ChoresCompletedWeekText = choresCompletedWeek + " / " + JsonChoreWeekMasterList.choreList.Count;
        ChoresCompletedMonthText = choresCompletedMonth + " / " + JsonChoreMonthMasterList.choreList.Count;
        ChoresCompletedSpecialText = choresCompletedSpecial + " / " + JsonChoreSpecialMasterList.choreList.Count;
        ChoresCompletedDayTextUser1 = choresCompletedDayUser1 + " / " + JsonChoreDayUser1MasterList.choreList.Count;
        ChoresCompletedWeekTextUser1 = choresCompletedWeekUser1 + " / " + JsonChoreWeekUser1MasterList.choreList.Count;
        ChoresCompletedMonthTextUser1 = choresCompletedMonthUser1 + " / " + JsonChoreMonthUser1MasterList.choreList.Count;
        ChoresCompletedSpecialTextUser1 = choresCompletedSpecialUser1 + " / " + JsonChoreSpecialUser1MasterList.choreList.Count;

        double progress = Convert.ToDouble(choresCompletedWeek) / Convert.ToDouble(JsonChoreWeekMasterList.choreList.Count) * 100;
        try {
            ChoresCompletedWeekProgressText = Convert.ToInt16(progress) + "%";
            ChoresCompletedWeekProgressValue = Convert.ToInt16(progress);
        } catch (Exception) { }

        progress = Convert.ToDouble(choresCompletedDay) / Convert.ToDouble(JsonChoreDayMasterList.choreList.Count) * 100;
        try {
            ChoresCompletedDayProgressText = Convert.ToInt16(progress) + "%";
            ChoresCompletedDayProgressValue = Convert.ToInt16(progress);
        } catch (Exception) { }

        progress = Convert.ToDouble(choresCompletedMonth) / Convert.ToDouble(JsonChoreMonthMasterList.choreList.Count) * 100;
        try {
            ChoresCompletedMonthProgressText = Convert.ToInt16(progress) + "%";
            ChoresCompletedMonthProgressValue = Convert.ToInt16(progress);
        } catch (Exception) { }

        progress = Convert.ToDouble(choresCompletedSpecial) / Convert.ToDouble(JsonChoreSpecialMasterList.choreList.Count) * 100;
        try {
            ChoresCompletedSpecialProgressText = Convert.ToInt16(progress) + "%";
            ChoresCompletedSpecialProgressValue = Convert.ToInt16(progress);
        } catch (Exception) { }

        progress = Convert.ToDouble(choresCompletedWeekUser1) / Convert.ToDouble(JsonChoreWeekUser1MasterList.choreList.Count) * 100;
        try {
            ChoresCompletedWeekProgressTextUser1 = Convert.ToInt16(progress) + "%";
            ChoresCompletedWeekProgressValueUser1 = Convert.ToInt16(progress);
        } catch (Exception) { }

        progress = Convert.ToDouble(choresCompletedDayUser1) / Convert.ToDouble(JsonChoreDayUser1MasterList.choreList.Count) * 100;
        try {
            ChoresCompletedDayProgressTextUser1 = Convert.ToInt16(progress) + "%";
            ChoresCompletedDayProgressValueUser1 = Convert.ToInt16(progress);
        } catch (Exception) { }

        progress = Convert.ToDouble(choresCompletedMonthUser1) / Convert.ToDouble(JsonChoreMonthUser1MasterList.choreList.Count) * 100;
        try {
            ChoresCompletedMonthProgressTextUser1 = Convert.ToInt16(progress) + "%";
            ChoresCompletedMonthProgressValueUser1 = Convert.ToInt16(progress);
        } catch (Exception) { }

        progress = Convert.ToDouble(choresCompletedSpecialUser1) / Convert.ToDouble(JsonChoreSpecialUser1MasterList.choreList.Count) * 100;
        try {
            ChoresCompletedSpecialProgressTextUser1 = Convert.ToInt16(progress) + "%";
            ChoresCompletedSpecialProgressValueUser1 = Convert.ToInt16(progress);
        } catch (Exception) { }

        DayButtonColor = "Transparent";
        WeekButtonColor = "Transparent";
        MonthButtonColor = "Transparent";
        SpecialButtonColor = "Transparent";

        DayButtonColorUser1 = "Transparent";
        WeekButtonColorUser1 = "Transparent";
        MonthButtonColorUser1 = "Transparent";
        SpecialButtonColorUser1 = "Transparent";

        if (ChoresCompletedDayProgressValue == 100) {
            DayButtonColor = "Green";
        }

        if (ChoresCompletedWeekProgressValue == 100) {
            WeekButtonColor = "Green";
        }

        if (ChoresCompletedMonthProgressValue == 100) {
            MonthButtonColor = "Green";
        }

        if (choresCompletedSpecial > 0) {
            SpecialButtonColor = "Green";
        }

        if (ChoresCompletedDayProgressValueUser1 == 100) {
            DayButtonColorUser1 = "Green";
        }

        if (ChoresCompletedWeekProgressValueUser1 == 100) {
            WeekButtonColorUser1 = "Green";
        }

        if (ChoresCompletedMonthProgressValueUser1 == 100) {
            MonthButtonColorUser1 = "Green";
        }

        if (choresCompletedSpecialUser1 > 0) {
            SpecialButtonColorUser1 = "Green";
        }

        try {
            SpecialDay1Text = JsonChoreFundsMaster.SpecialDay1Completed ? "Completed" : "Not Complete";
            SpecialDay2Text = JsonChoreFundsMaster.SpecialDay2Completed ? "Completed" : "Not Complete";
        } catch (Exception) { }

        RefreshCountdown();

        CalculateFunds();
    }

    private void RefreshCountdown() {
        DateTime dateNextMonth = DateTime.Now;
        while (DateTime.Now.Month == dateNextMonth.Month) {
            dateNextMonth = dateNextMonth.AddDays(1);
        }

        FundsExpire = (dateNextMonth.Date - DateTime.Now.Date).Days + " Days";

        if (FundsExpire == "1 Days") {
            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            FundsExpire = timeSpan.Hours + " Hours";
        }

        if (FundsExpire == "1 Hours") {
            FundsExpire = "1 Hour";
        }
    }

    private void CalculateFunds() {
        calculatedReleaseFunds = 0;

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        //check all files in week directory. If they start with the current month: open, calculate,
        //then add $50 if task compliance is >= 95%.
        foreach (string fileName in Directory.GetFiles(FILE_DIRECTORY + "chores/", "chores_week_" + DateTime.Now.ToString("yyyy_MM") + "*.json")) {
            try {
                StreamReader streamReader = new(fileName);
                string fileString = null;
                while (!streamReader.EndOfStream) {
                    fileString = streamReader.ReadToEnd();
                }

                streamReader.Close();

                if (fileString != null) {
                    JsonChores jsonChores = JsonSerializer.Deserialize<JsonChores>(fileString, options);

                    int weekCompleted = 0;
                    foreach (ChoreDetails choreDetails in jsonChores.choreList) {
                        if (choreDetails.IsComplete) {
                            weekCompleted++;
                        }
                    }

                    if (Convert.ToDouble(weekCompleted) / Convert.ToDouble(jsonChores.choreList.Count) * 100 >= 95) {
                        calculatedReleaseFunds += 50;
                    }
                }
            } catch (Exception) { }
        }

        //check month file. open, calculate, then add $100 if task compliance is >= 95%.
        try {
            StreamReader streamReader = new(FILE_DIRECTORY + "chores/chores_month_" + DateTime.Now.ToString("yyyy_MM") + ".json");
            string fileString = null;
            while (!streamReader.EndOfStream) {
                fileString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (fileString != null) {
                JsonChores jsonChores = JsonSerializer.Deserialize<JsonChores>(fileString, options);

                int monthCompleted = 0;
                foreach (ChoreDetails choreDetails in jsonChores.choreList) {
                    if (choreDetails.IsComplete) {
                        monthCompleted++;
                    }
                }

                if (Convert.ToDouble(monthCompleted) / Convert.ToDouble(jsonChores.choreList.Count) * 100 >= 95) {
                    calculatedReleaseFunds += 100;
                }
            }
        } catch (Exception) { }

        //check special file. add $100 per completed task.
        try {
            StreamReader streamReader = new(FILE_DIRECTORY + "chores/chores_special_" + DateTime.Now.ToString("yyyy_MM") + ".json");
            string fileString = null;
            while (!streamReader.EndOfStream) {
                fileString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (fileString != null) {
                JsonChores jsonChores = JsonSerializer.Deserialize<JsonChores>(fileString, options);

                int specialCompleted = 0;
                foreach (ChoreDetails choreDetails in jsonChores.choreList) {
                    if (choreDetails.IsComplete) {
                        specialCompleted++;
                    }
                }

                calculatedReleaseFunds += 100 * specialCompleted;
            }
        } catch (Exception) { }

        FundsProgressValue = calculatedReleaseFunds;
        FundsProgressText = "$" + calculatedReleaseFunds;

        ChoreFundsFromJson choreFundsFromJson = new();
        CashAvailable = "$" + JsonChoreFundsMaster.FundsAvailable;

        JsonChoreFundsMaster.FundsLocked = calculatedReleaseFunds;

        try {
            string jsonString = JsonSerializer.Serialize(JsonChoreFundsMaster);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(FILE_DIRECTORY + "chorefunds.json", jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save chorefunds.json... " + e.Message);
        }
    }

    private void ReleaseFunds() {
        int switchCash = JsonChoreFundsMaster.FundsLocked;
        JsonChoreFundsMaster.FundsLocked = 0;
        CashAvailable = "$" + switchCash;
    }

    #region Fields

    public string CurrentMonthText {
        get => _currentMonthText;
        set {
            _currentMonthText = value;
            RaisePropertyChangedEvent("CurrentMonthText");
        }
    }

    public string CurrentWeekText {
        get => _currentWeekText;
        set {
            _currentWeekText = value;
            RaisePropertyChangedEvent("CurrentWeekText");
        }
    }

    public string CurrentDayText {
        get => _currentDayText;
        set {
            _currentDayText = value;
            RaisePropertyChangedEvent("CurrentDayText");
        }
    }

    public string ChoresCompletedDayText {
        get => _choresCompletedDayText;
        set {
            _choresCompletedDayText = value;
            RaisePropertyChangedEvent("ChoresCompletedDayText");
        }
    }

    public string ChoresCompletedWeekText {
        get => _choresCompletedWeekText;
        set {
            _choresCompletedWeekText = value;
            RaisePropertyChangedEvent("ChoresCompletedWeekText");
        }
    }

    public string ChoresCompletedMonthText {
        get => _choresCompletedMonthText;
        set {
            _choresCompletedMonthText = value;
            RaisePropertyChangedEvent("ChoresCompletedMonthText");
        }
    }

    public string User1Text {
        get => _user1Text;
        set {
            _user1Text = value;
            RaisePropertyChangedEvent("User1Text");
        }
    }

    public string User2Text {
        get => _user2Text;
        set {
            _user2Text = value;
            RaisePropertyChangedEvent("User2Text");
        }
    }

    public string ChoresCompletedSpecialText {
        get => _choresCompletedSpecialText;
        set {
            _choresCompletedSpecialText = value;
            RaisePropertyChangedEvent("ChoresCompletedSpecialText");
        }
    }

    public string ChoresCompletedWeekProgressText {
        get => _choresCompletedWeekProgressText;
        set {
            _choresCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("ChoresCompletedWeekProgressText");
        }
    }

    public string ChoresCompletedDayProgressText {
        get => _choresCompletedDayProgressText;
        set {
            _choresCompletedDayProgressText = value;
            RaisePropertyChangedEvent("ChoresCompletedDayProgressText");
        }
    }

    public int ChoresCompletedWeekProgressValue {
        get => _choresCompletedWeekProgressValue;
        set {
            _choresCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("ChoresCompletedWeekProgressValue");
        }
    }

    public string ChoresCompletedMonthProgressText {
        get => _choresCompletedMonthProgressText;
        set {
            _choresCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("ChoresCompletedMonthProgressText");
        }
    }

    public string ChoresCompletedSpecialProgressText {
        get => _choresCompletedSpecialProgressText;
        set {
            _choresCompletedSpecialProgressText = value;
            RaisePropertyChangedEvent("ChoresCompletedSpecialProgressText");
        }
    }

    public int ChoresCompletedDayProgressValue {
        get => _choresCompletedDayProgressValue;
        set {
            _choresCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("ChoresCompletedDayProgressValue");
        }
    }

    public int ChoresCompletedMonthProgressValue {
        get => _choresCompletedMonthProgressValue;
        set {
            _choresCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("ChoresCompletedMonthProgressValue");
        }
    }

    public int ChoresCompletedSpecialProgressValue {
        get => _choresCompletedSpecialProgressValue;
        set {
            _choresCompletedSpecialProgressValue = value;
            RaisePropertyChangedEvent("ChoresCompletedSpecialProgressValue");
        }
    }

    public string ProjectedFundMonthText {
        get => _projectedFundMonthText;
        set {
            _projectedFundMonthText = value;
            RaisePropertyChangedEvent("ProjectedFundMonthText");
        }
    }

    public string DayButtonColor {
        get => _dayButtonColor;
        set {
            _dayButtonColor = value;
            RaisePropertyChangedEvent("DayButtonColor");
        }
    }

    public string WeekButtonColor {
        get => _weekButtonColor;
        set {
            _weekButtonColor = value;
            RaisePropertyChangedEvent("WeekButtonColor");
        }
    }

    public string MonthButtonColor {
        get => _monthButtonColor;
        set {
            _monthButtonColor = value;
            RaisePropertyChangedEvent("MonthButtonColor");
        }
    }

    public string SpecialButtonColor {
        get => _specialButtonColor;
        set {
            _specialButtonColor = value;
            RaisePropertyChangedEvent("SpecialButtonColor");
        }
    }

    public string CashAvailable {
        get => _cashAvailable;
        set {
            _cashAvailable = value;
            RaisePropertyChangedEvent("CashAvailable");
        }
    }

    public int FundsProgressValue {
        get => _fundsProgressValue;
        set {
            _fundsProgressValue = value;
            RaisePropertyChangedEvent("FundsProgressValue");
        }
    }

    public string FundsProgressText {
        get => _fundsProgressText;
        set {
            _fundsProgressText = value;
            RaisePropertyChangedEvent("FundsProgressText");
        }
    }

    public string FundsExpire {
        get => _fundsExpire;
        set {
            _fundsExpire = value;
            RaisePropertyChangedEvent("FundsExpire");
        }
    }

    public string SpecialDay1Text {
        get => _specialDay1Text;
        set {
            _specialDay1Text = value;
            RaisePropertyChangedEvent("SpecialDay1Text");
        }
    }

    public string SpecialDay2Text {
        get => _specialDay2Text;
        set {
            _specialDay2Text = value;
            RaisePropertyChangedEvent("SpecialDay2Text");
        }
    }

    public string CurrentModeText {
        get => _currentModeText;
        set {
            _currentModeText = value;
            RaisePropertyChangedEvent("CurrentModeText");
        }
    }

    public string ChoresCompletedDayTextUser1 {
        get => _choresCompletedDayTextUser1;
        set {
            _choresCompletedDayTextUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedDayTextUser1");
        }
    }

    public string ChoresCompletedWeekTextUser1 {
        get => _choresCompletedWeekTextUser1;
        set {
            _choresCompletedWeekTextUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedWeekTextUser1");
        }
    }

    public string ChoresCompletedMonthTextUser1 {
        get => _choresCompletedMonthTextUser1;
        set {
            _choresCompletedMonthTextUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedMonthTextUser1");
        }
    }

    public string ChoresCompletedSpecialTextUser1 {
        get => _choresCompletedSpecialTextUser1;
        set {
            _choresCompletedSpecialTextUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedSpecialTextUser1");
        }
    }

    public string ChoresCompletedDayProgressTextUser1 {
        get => _choresCompletedDayProgressTextUser1;
        set {
            _choresCompletedDayProgressTextUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedDayProgressTextUser1");
        }
    }

    public string ChoresCompletedWeekProgressTextUser1 {
        get => _choresCompletedWeekProgressTextUser1;
        set {
            _choresCompletedWeekProgressTextUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedWeekProgressTextUser1");
        }
    }

    public string ChoresCompletedMonthProgressTextUser1 {
        get => _choresCompletedMonthProgressTextUser1;
        set {
            _choresCompletedMonthProgressTextUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedMonthProgressTextUser1");
        }
    }

    public string ChoresCompletedSpecialProgressTextUser1 {
        get => _choresCompletedSpecialProgressTextUser1;
        set {
            _choresCompletedSpecialProgressTextUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedSpecialProgressTextUser1");
        }
    }

    public int ChoresCompletedDayProgressValueUser1 {
        get => _choresCompletedDayProgressValueUser1;
        set {
            _choresCompletedDayProgressValueUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedDayProgressValueUser1");
        }
    }

    public int ChoresCompletedWeekProgressValueUser1 {
        get => _choresCompletedWeekProgressValueUser1;
        set {
            _choresCompletedWeekProgressValueUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedWeekProgressValueUser1");
        }
    }

    public int ChoresCompletedMonthProgressValueUser1 {
        get => _choresCompletedMonthProgressValueUser1;
        set {
            _choresCompletedMonthProgressValueUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedMonthProgressValueUser1");
        }
    }

    public int ChoresCompletedSpecialProgressValueUser1 {
        get => _choresCompletedSpecialProgressValueUser1;
        set {
            _choresCompletedSpecialProgressValueUser1 = value;
            RaisePropertyChangedEvent("ChoresCompletedSpecialProgressValueUser1");
        }
    }

    public string DayButtonColorUser1 {
        get => _dayButtonColorUser1;
        set {
            _dayButtonColorUser1 = value;
            RaisePropertyChangedEvent("DayButtonColorUser1");
        }
    }

    public string WeekButtonColorUser1 {
        get => _weekButtonColorUser1;
        set {
            _weekButtonColorUser1 = value;
            RaisePropertyChangedEvent("WeekButtonColorUser1");
        }
    }

    public string MonthButtonColorUser1 {
        get => _monthButtonColorUser1;
        set {
            _monthButtonColorUser1 = value;
            RaisePropertyChangedEvent("MonthButtonColorUser1");
        }
    }

    public string SpecialButtonColorUser1 {
        get => _specialButtonColorUser1;
        set {
            _specialButtonColorUser1 = value;
            RaisePropertyChangedEvent("SpecialButtonColorUser1");
        }
    }

    #endregion
}