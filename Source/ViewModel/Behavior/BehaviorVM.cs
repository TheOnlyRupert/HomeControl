using System;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Behavior;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Behavior;

public class BehaviorVM : BaseViewModel {
    /* Integrating Chores */
    private string _currentMonthText, _currentWeekText, _currentDayText, _currentQuarterText, _user1ChoresCompletedWeekProgressText, _user1ChoresCompletedDayProgressText,
        _user1ChoresCompletedMonthProgressText, _user1ChoresCompletedQuarterProgressText, _user1CashAvailable, _user2ChoresCompletedWeekProgressText,
        _user2ChoresCompletedDayProgressText,
        _user2ChoresCompletedMonthProgressText, _user2ChoresCompletedQuarterProgressText, _user2CashAvailable, _user3ChoresCompletedWeekProgressText,
        _user3ChoresCompletedDayProgressText,
        _user3ChoresCompletedMonthProgressText, _user3ChoresCompletedQuarterProgressText, _user3CashAvailable, _user4ChoresCompletedWeekProgressText,
        _user4ChoresCompletedDayProgressText,
        _user4ChoresCompletedMonthProgressText, _user4ChoresCompletedQuarterProgressText, _user4CashAvailable, _user5ChoresCompletedWeekProgressText,
        _user5ChoresCompletedDayProgressText,
        _user5ChoresCompletedMonthProgressText, _user5ChoresCompletedQuarterProgressText, _user5CashAvailable, _user1CashAvailableTextColor, _user2CashAvailableTextColor,
        _user3CashAvailableTextColor, _user4CashAvailableTextColor, _user5CashAvailableTextColor, _remainingDay, _remainingWeek, _remainingMonth, _remainingQuarter, _remainingYear;

    private BitmapImage _imageUser1, _imageUser2, _imageUser3, _imageUser4, _imageUser5;

    private string _user1Name, _user2Name, _user3Name, _user4Name, _user5Name, _user1Star1, _user1Star2, _user1Star3, _user1Star4, _user1Star5, _user2Star1, _user2Star2,
        _user2Star3, _user2Star4, _user2Star5, _user3Star1, _user3Star2, _user3Star3, _user3Star4, _user3Star5, _user4Star1, _user4Star2, _user4Star3, _user4Star4,
        _user4Star5, _user5Star1, _user5Star2, _user5Star3, _user5Star4, _user5Star5, _user1Strike1, _user1Strike2, _user1Strike3, _user2Strike1, _user2Strike2,
        _user2Strike3, _user3Strike1, _user3Strike2, _user3Strike3, _user4Strike1, _user4Strike2, _user4Strike3, _user5Strike1, _user5Strike2, _user5Strike3,
        _user1ChoresCompletedDayProgressColor, _user1ChoresCompletedWeekProgressColor, _user1ChoresCompletedMonthProgressColor, _user1ChoresCompletedQuarterProgressColor,
        _user2ChoresCompletedDayProgressColor, _user2ChoresCompletedWeekProgressColor, _user2ChoresCompletedMonthProgressColor, _user2ChoresCompletedQuarterProgressColor,
        _user3ChoresCompletedDayProgressColor, _user3ChoresCompletedWeekProgressColor, _user3ChoresCompletedMonthProgressColor, _user3ChoresCompletedQuarterProgressColor,
        _user4ChoresCompletedDayProgressColor, _user4ChoresCompletedWeekProgressColor, _user4ChoresCompletedMonthProgressColor, _user4ChoresCompletedQuarterProgressColor,
        _user5ChoresCompletedDayProgressColor, _user5ChoresCompletedWeekProgressColor, _user5ChoresCompletedMonthProgressColor, _user5ChoresCompletedQuarterProgressColor;

    private int user1ChoresCompletedDay, user1ChoresCompletedWeek, user1ChoresCompletedMonth, user1ChoresCompletedQuarter, _user1ChoresCompletedDayProgressValue,
        _user1ChoresCompletedWeekProgressValue, _user1ChoresCompletedMonthProgressValue, _user1ChoresCompletedQuarterProgressValue, user2ChoresCompletedDay,
        user2ChoresCompletedWeek, user2ChoresCompletedMonth, user2ChoresCompletedQuarter, _user2ChoresCompletedDayProgressValue, _user2ChoresCompletedWeekProgressValue,
        _user2ChoresCompletedMonthProgressValue, _user2ChoresCompletedQuarterProgressValue, user3ChoresCompletedDay, user3ChoresCompletedWeek, user3ChoresCompletedMonth,
        user3ChoresCompletedQuarter, _user3ChoresCompletedDayProgressValue, _user3ChoresCompletedWeekProgressValue, _user3ChoresCompletedMonthProgressValue,
        _user3ChoresCompletedQuarterProgressValue, user4ChoresCompletedDay, user4ChoresCompletedWeek, user4ChoresCompletedMonth, user4ChoresCompletedQuarter,
        _user4ChoresCompletedDayProgressValue, _user4ChoresCompletedWeekProgressValue, _user4ChoresCompletedMonthProgressValue, _user4ChoresCompletedQuarterProgressValue,
        user5ChoresCompletedDay, user5ChoresCompletedWeek, user5ChoresCompletedMonth, user5ChoresCompletedQuarter, _user5ChoresCompletedDayProgressValue,
        _user5ChoresCompletedWeekProgressValue, _user5ChoresCompletedMonthProgressValue, _user5ChoresCompletedQuarterProgressValue;

    public BehaviorVM() {
        User1Name = ReferenceValues.JsonMasterSettings.User1Name;
        User2Name = ReferenceValues.JsonMasterSettings.User2Name;
        User3Name = ReferenceValues.JsonMasterSettings.User3Name;
        User4Name = ReferenceValues.JsonMasterSettings.User4Name;
        User5Name = ReferenceValues.JsonMasterSettings.User5Name;

        try {
            Uri uri = new(ReferenceValues.FILE_DIRECTORY + "icons/user1.png", UriKind.RelativeOrAbsolute);
            ImageUser1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user2.png", UriKind.RelativeOrAbsolute);
            ImageUser2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user3.png", UriKind.RelativeOrAbsolute);
            ImageUser3 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user4.png", UriKind.RelativeOrAbsolute);
            ImageUser4 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user5.png", UriKind.RelativeOrAbsolute);
            ImageUser5 = new BitmapImage(uri);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BehaviorVM",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;

        new BehaviorFromJson();

        /* Remove strikes if program was closed before midnight */
        try {
            if (!ReferenceValues.JsonBehaviorMaster.Date.Day.Equals(DateTime.Now.Day)) {
                ReferenceValues.JsonBehaviorMaster.User1Strikes = 0;
                ReferenceValues.JsonBehaviorMaster.User2Strikes = 0;
                ReferenceValues.JsonBehaviorMaster.User3Strikes = 0;
                ReferenceValues.JsonBehaviorMaster.User4Strikes = 0;
                ReferenceValues.JsonBehaviorMaster.User5Strikes = 0;
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BehaviorVM",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }

        RefreshBehavior();
        RefreshFields();
        RefreshCountdown();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void RefreshBehavior() {
        ReferenceValues.JsonBehaviorMaster.Date = DateTime.Now;
        User1Star1 = "../../../Resources/Images/behavior/star_black.png";
        User1Star2 = "../../../Resources/Images/behavior/star_black.png";
        User1Star3 = "../../../Resources/Images/behavior/star_black.png";
        User1Star4 = "../../../Resources/Images/behavior/star_black.png";
        User1Star5 = "../../../Resources/Images/behavior/star_black.png";
        User2Star1 = "../../../Resources/Images/behavior/star_black.png";
        User2Star2 = "../../../Resources/Images/behavior/star_black.png";
        User2Star3 = "../../../Resources/Images/behavior/star_black.png";
        User2Star4 = "../../../Resources/Images/behavior/star_black.png";
        User2Star5 = "../../../Resources/Images/behavior/star_black.png";
        User3Star1 = "../../../Resources/Images/behavior/star_black.png";
        User3Star2 = "../../../Resources/Images/behavior/star_black.png";
        User3Star3 = "../../../Resources/Images/behavior/star_black.png";
        User3Star4 = "../../../Resources/Images/behavior/star_black.png";
        User3Star5 = "../../../Resources/Images/behavior/star_black.png";
        User4Star1 = "../../../Resources/Images/behavior/star_black.png";
        User4Star2 = "../../../Resources/Images/behavior/star_black.png";
        User4Star3 = "../../../Resources/Images/behavior/star_black.png";
        User4Star4 = "../../../Resources/Images/behavior/star_black.png";
        User4Star5 = "../../../Resources/Images/behavior/star_black.png";
        User5Star1 = "../../../Resources/Images/behavior/star_black.png";
        User5Star2 = "../../../Resources/Images/behavior/star_black.png";
        User5Star3 = "../../../Resources/Images/behavior/star_black.png";
        User5Star4 = "../../../Resources/Images/behavior/star_black.png";
        User5Star5 = "../../../Resources/Images/behavior/star_black.png";

        User1Strike1 = "";
        User1Strike2 = "";
        User1Strike3 = "";
        User2Strike1 = "";
        User2Strike2 = "";
        User2Strike3 = "";
        User3Strike1 = "";
        User3Strike2 = "";
        User3Strike3 = "";
        User4Strike1 = "";
        User4Strike2 = "";
        User4Strike3 = "";
        User5Strike1 = "";
        User5Strike2 = "";
        User5Strike3 = "";

        switch (ReferenceValues.JsonBehaviorMaster.User1Stars) {
        case 1:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star4 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User2Stars) {
        case 1:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star4 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User3Stars) {
        case 1:
            User3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            User3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            User3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            User3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            User3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star4 = "../../../Resources/Images/behavior/star_gold.png";
            User3Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User4Stars) {
        case 1:
            User4Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            User4Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            User4Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            User4Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            User4Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star4 = "../../../Resources/Images/behavior/star_gold.png";
            User4Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User5Stars) {
        case 1:
            User5Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            User5Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            User5Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            User5Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            User5Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star4 = "../../../Resources/Images/behavior/star_gold.png";
            User5Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User1Strikes) {
        case 1:
            User1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            User1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User1Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            User1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User1Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            User1Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User2Strikes) {
        case 1:
            User2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            User2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User2Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            User2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User2Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            User2Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User3Strikes) {
        case 1:
            User3Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            User3Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User3Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            User3Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User3Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            User3Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User4Strikes) {
        case 1:
            User4Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            User4Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User4Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            User4Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User4Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            User4Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User5Strikes) {
        case 1:
            User5Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            User5Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User5Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            User5Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User5Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            User5Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            ReferenceValues.JsonBehaviorMaster.User1Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User2Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User3Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User4Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User5Strikes = 0;
            RefreshBehavior();
        }
    }

    private void ButtonLogic(object param) {
        if (!ReferenceValues.LockUI) {
            switch (param) {
            case "user1":
                ReferenceValues.ActiveBehaviorUser = 0;
                break;
            case "user2":
                ReferenceValues.ActiveBehaviorUser = 1;
                break;
            case "user3":
                ReferenceValues.ActiveBehaviorUser = 2;
                break;
            case "user4":
                ReferenceValues.ActiveBehaviorUser = 3;
                break;
            case "user5":
                ReferenceValues.ActiveBehaviorUser = 4;
                break;
            }

            EditBehavior editBehavior = new();
            editBehavior.ShowDialog();
            editBehavior.Close();
            RefreshBehavior();
        } else {
            ReferenceValues.SoundToPlay = "locked";
            SoundDispatcher.PlaySound();
        }
    }

    private void RefreshFields() {
        DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        System.Globalization.Calendar calendar = dateTimeFormatInfo.Calendar;

        User1CashAvailable = "$0";
        user1ChoresCompletedDay = 0;
        user1ChoresCompletedWeek = 0;
        user1ChoresCompletedMonth = 0;
        user1ChoresCompletedQuarter = 0;
        User1CashAvailableTextColor = "White";

        User2CashAvailable = "$0";
        user2ChoresCompletedDay = 0;
        user2ChoresCompletedWeek = 0;
        user2ChoresCompletedMonth = 0;
        user2ChoresCompletedQuarter = 0;
        User2CashAvailableTextColor = "White";

        User3CashAvailable = "$0";
        user3ChoresCompletedDay = 0;
        user3ChoresCompletedWeek = 0;
        user3ChoresCompletedMonth = 0;
        user3ChoresCompletedQuarter = 0;
        User3CashAvailableTextColor = "White";

        User4CashAvailable = "$0";
        user4ChoresCompletedDay = 0;
        user4ChoresCompletedWeek = 0;
        user4ChoresCompletedMonth = 0;
        user4ChoresCompletedQuarter = 0;
        User4CashAvailableTextColor = "White";

        User5CashAvailable = "$0";
        user5ChoresCompletedDay = 0;
        user5ChoresCompletedWeek = 0;
        user5ChoresCompletedMonth = 0;
        user5ChoresCompletedQuarter = 0;
        User5CashAvailableTextColor = "White";

        ReferenceValues.ChoreWeekStartDate = DateTime.Now;
        while (ReferenceValues.ChoreWeekStartDate.DayOfWeek != DayOfWeek.Sunday) {
            ReferenceValues.ChoreWeekStartDate = ReferenceValues.ChoreWeekStartDate.AddDays(-1);
        }

        CurrentDayText = DateTime.Now.ToString("dddd");
        CurrentMonthText = DateTime.Now.ToString("MMMM");
        CurrentWeekText = "Week: " + calendar.GetWeekOfYear(DateTime.Now, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);
        CurrentQuarterText = "Quarter 1";

        CurrentQuarterText = DateTime.Now.Month switch {
            > 0 and < 3 => "Quarter: 1",
            > 2 and < 6 => "Quarter: 2",
            > 5 and < 9 => "Quarter: 3",
            _ => "Quarter: 4"
        };

        User1ChoresCompletedWeekProgressValue = 10;
        User1ChoresCompletedWeekProgressText = User1ChoresCompletedWeekProgressValue + "%";
        User1ChoresCompletedMonthProgressValue = 25;
        User1ChoresCompletedMonthProgressText = User1ChoresCompletedMonthProgressValue + "%";
        User1ChoresCompletedDayProgressValue = 50;
        User1ChoresCompletedDayProgressText = User1ChoresCompletedDayProgressValue + "%";
        User1ChoresCompletedQuarterProgressValue = 100;
        User1ChoresCompletedQuarterProgressText = User1ChoresCompletedQuarterProgressValue + "%";
        
        User2ChoresCompletedWeekProgressValue = 10;
        User2ChoresCompletedWeekProgressText = User2ChoresCompletedWeekProgressValue + "%";
        User2ChoresCompletedMonthProgressValue = 25;
        User2ChoresCompletedMonthProgressText = User2ChoresCompletedMonthProgressValue + "%";
        User2ChoresCompletedDayProgressValue = 50;
        User2ChoresCompletedDayProgressText = User2ChoresCompletedDayProgressValue + "%";
        User2ChoresCompletedQuarterProgressValue = 100;
        User2ChoresCompletedQuarterProgressText = User2ChoresCompletedQuarterProgressValue + "%";
        
        User3ChoresCompletedWeekProgressValue = 10;
        User3ChoresCompletedWeekProgressText = User3ChoresCompletedWeekProgressValue + "%";
        User3ChoresCompletedMonthProgressValue = 25;
        User3ChoresCompletedMonthProgressText = User3ChoresCompletedMonthProgressValue + "%";
        User3ChoresCompletedDayProgressValue = 50;
        User3ChoresCompletedDayProgressText = User3ChoresCompletedDayProgressValue + "%";
        User3ChoresCompletedQuarterProgressValue = 100;
        User3ChoresCompletedQuarterProgressText = User3ChoresCompletedQuarterProgressValue + "%";

        User4ChoresCompletedWeekProgressValue = 10;
        User4ChoresCompletedWeekProgressText = User4ChoresCompletedWeekProgressValue + "%";
        User4ChoresCompletedMonthProgressValue = 25;
        User4ChoresCompletedMonthProgressText = User4ChoresCompletedMonthProgressValue + "%";
        User4ChoresCompletedDayProgressValue = 50;
        User4ChoresCompletedDayProgressText = User4ChoresCompletedDayProgressValue + "%";
        User4ChoresCompletedQuarterProgressValue = 100;
        User4ChoresCompletedQuarterProgressText = User4ChoresCompletedQuarterProgressValue + "%";
        
        User5ChoresCompletedWeekProgressValue = 10;
        User5ChoresCompletedWeekProgressText = User5ChoresCompletedWeekProgressValue + "%";
        User5ChoresCompletedMonthProgressValue = 25;
        User5ChoresCompletedMonthProgressText = User5ChoresCompletedMonthProgressValue + "%";
        User5ChoresCompletedDayProgressValue = 50;
        User5ChoresCompletedDayProgressText = User5ChoresCompletedDayProgressValue + "%";
        User5ChoresCompletedQuarterProgressValue = 100;
        User5ChoresCompletedQuarterProgressText = User5ChoresCompletedQuarterProgressValue + "%";

        User1ChoresCompletedDayProgressColor = User1ChoresCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
        User1ChoresCompletedWeekProgressColor = User1ChoresCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
        User1ChoresCompletedMonthProgressColor = User1ChoresCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
        User1ChoresCompletedQuarterProgressColor = User1ChoresCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";
        
        User2ChoresCompletedDayProgressColor = User2ChoresCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
        User2ChoresCompletedWeekProgressColor = User2ChoresCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
        User2ChoresCompletedMonthProgressColor = User2ChoresCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
        User2ChoresCompletedQuarterProgressColor = User2ChoresCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";
        
        User3ChoresCompletedDayProgressColor = User3ChoresCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
        User3ChoresCompletedWeekProgressColor = User3ChoresCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
        User3ChoresCompletedMonthProgressColor = User3ChoresCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
        User3ChoresCompletedQuarterProgressColor = User3ChoresCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";
        
        User4ChoresCompletedDayProgressColor = User4ChoresCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
        User4ChoresCompletedWeekProgressColor = User4ChoresCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
        User4ChoresCompletedMonthProgressColor = User4ChoresCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
        User4ChoresCompletedQuarterProgressColor = User4ChoresCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";
        
        User5ChoresCompletedDayProgressColor = User5ChoresCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
        User5ChoresCompletedWeekProgressColor = User5ChoresCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
        User5ChoresCompletedMonthProgressColor = User5ChoresCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
        User5ChoresCompletedQuarterProgressColor = User5ChoresCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";
    }

    private void RefreshCountdown() {
        /* Day */
        DateTime dateNext = DateTime.Now;
        RemainingDay = (TimeSpan.FromHours(24) - dateNext.TimeOfDay).Hours + " Hours";

        if (RemainingDay == "1 Hours") {
            RemainingDay = "1 Hour";
        }
        
        /* Week */
        dateNext = DateTime.Now;
        if (dateNext.DayOfWeek == DayOfWeek.Sunday) {
            dateNext = dateNext.AddDays(1);
        }

        while (dateNext.DayOfWeek != DayOfWeek.Sunday) {
            dateNext = dateNext.AddDays(1);
        }

        RemainingWeek = (dateNext.Date - DateTime.Now.Date).Days + " Days";

        if (RemainingWeek == "1 Days") {
            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            RemainingWeek = timeSpan.Hours + " Hours";
        }

        if (RemainingWeek == "1 Hours") {
            RemainingWeek = "1 Hour";
        }
        
        /* Month */
        dateNext = DateTime.Now;
        while (DateTime.Now.Month == dateNext.Month) {
            dateNext = dateNext.AddDays(1);
        }

        RemainingMonth = (dateNext.Date - DateTime.Now.Date).Days + " Days";

        if (RemainingMonth == "1 Days") {
            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            RemainingMonth = timeSpan.Hours + " Hours";
        }

        if (RemainingMonth == "1 Hours") {
            RemainingMonth = "1 Hour";
        }

        /* Quarter */
        dateNext = DateTime.Now;
        switch (dateNext.Month) {
        case 1:
        case 2:
        case 3:
            while (dateNext.Month != 4) {
                dateNext = dateNext.AddDays(1);
            }

            break;
        case 4:
        case 5:
        case 6:
            while (dateNext.Month != 7) {
                dateNext = dateNext.AddDays(1);
            }

            break;
        case 7:
        case 8:
        case 9:
            while (dateNext.Month != 10) {
                dateNext = dateNext.AddDays(1);
            }

            break;
        case 10:
        case 11:
        case 12:
            while (dateNext.Month != 1) {
                dateNext = dateNext.AddDays(1);
            }

            break;
        }
        
        RemainingQuarter = (dateNext.Date - DateTime.Now.Date).TotalDays + " Days";

        if (RemainingQuarter == "1 Days") {
            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            RemainingQuarter = timeSpan.Hours + " Hours";
        }

        if (RemainingQuarter == "1 Hours") {
            RemainingQuarter = "1 Hour";
        }

        /* Year */
        dateNext = DateTime.Now;
        while (dateNext.Year == DateTime.Now.Year) {
            dateNext = dateNext.AddDays(1);
        }

        RemainingYear = (dateNext.Date - DateTime.Now.Date).TotalDays + " Days";

        if (RemainingYear == "1 Days") {
            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            RemainingYear = timeSpan.Hours + " Hours";
        }

        if (RemainingYear == "1 Hours") {
            RemainingYear = "1 Hour";
        }
    }

    #region Fields

    public string User1Name {
        get => _user1Name;
        set {
            _user1Name = value;
            RaisePropertyChangedEvent("User1Name");
        }
    }

    public string User2Name {
        get => _user2Name;
        set {
            _user2Name = value;
            RaisePropertyChangedEvent("User2Name");
        }
    }

    public string User3Name {
        get => _user3Name;
        set {
            _user3Name = value;
            RaisePropertyChangedEvent("User3Name");
        }
    }

    public string User4Name {
        get => _user4Name;
        set {
            _user4Name = value;
            RaisePropertyChangedEvent("User4Name");
        }
    }

    public string User5Name {
        get => _user5Name;
        set {
            _user5Name = value;
            RaisePropertyChangedEvent("User5Name");
        }
    }

    public string User1Star1 {
        get => _user1Star1;
        set {
            _user1Star1 = value;
            RaisePropertyChangedEvent("User1Star1");
        }
    }

    public string User1Star2 {
        get => _user1Star2;
        set {
            _user1Star2 = value;
            RaisePropertyChangedEvent("User1Star2");
        }
    }

    public string User1Star3 {
        get => _user1Star3;
        set {
            _user1Star3 = value;
            RaisePropertyChangedEvent("User1Star3");
        }
    }

    public string User1Star4 {
        get => _user1Star4;
        set {
            _user1Star4 = value;
            RaisePropertyChangedEvent("User1Star4");
        }
    }

    public string User1Star5 {
        get => _user1Star5;
        set {
            _user1Star5 = value;
            RaisePropertyChangedEvent("User1Star5");
        }
    }

    public string User2Star1 {
        get => _user2Star1;
        set {
            _user2Star1 = value;
            RaisePropertyChangedEvent("User2Star1");
        }
    }

    public string User2Star2 {
        get => _user2Star2;
        set {
            _user2Star2 = value;
            RaisePropertyChangedEvent("User2Star2");
        }
    }

    public string User2Star3 {
        get => _user2Star3;
        set {
            _user2Star3 = value;
            RaisePropertyChangedEvent("User2Star3");
        }
    }

    public string User2Star4 {
        get => _user2Star4;
        set {
            _user2Star4 = value;
            RaisePropertyChangedEvent("User2Star4");
        }
    }

    public string User2Star5 {
        get => _user2Star5;
        set {
            _user2Star5 = value;
            RaisePropertyChangedEvent("User2Star5");
        }
    }

    public string User3Star1 {
        get => _user3Star1;
        set {
            _user3Star1 = value;
            RaisePropertyChangedEvent("User3Star1");
        }
    }

    public string User3Star2 {
        get => _user3Star2;
        set {
            _user3Star2 = value;
            RaisePropertyChangedEvent("User3Star2");
        }
    }

    public string User3Star3 {
        get => _user3Star3;
        set {
            _user3Star3 = value;
            RaisePropertyChangedEvent("User3Star3");
        }
    }

    public string User3Star4 {
        get => _user3Star4;
        set {
            _user3Star4 = value;
            RaisePropertyChangedEvent("User3Star4");
        }
    }

    public string User3Star5 {
        get => _user3Star5;
        set {
            _user3Star5 = value;
            RaisePropertyChangedEvent("User3Star5");
        }
    }

    public string User4Star1 {
        get => _user4Star1;
        set {
            _user4Star1 = value;
            RaisePropertyChangedEvent("User4Star1");
        }
    }

    public string User4Star2 {
        get => _user4Star2;
        set {
            _user4Star2 = value;
            RaisePropertyChangedEvent("User4Star2");
        }
    }

    public string User4Star3 {
        get => _user4Star3;
        set {
            _user4Star3 = value;
            RaisePropertyChangedEvent("User4Star3");
        }
    }

    public string User4Star4 {
        get => _user4Star4;
        set {
            _user4Star4 = value;
            RaisePropertyChangedEvent("User4Star4");
        }
    }

    public string User4Star5 {
        get => _user4Star5;
        set {
            _user4Star5 = value;
            RaisePropertyChangedEvent("User4Star5");
        }
    }

    public string User5Star1 {
        get => _user5Star1;
        set {
            _user5Star1 = value;
            RaisePropertyChangedEvent("User5Star1");
        }
    }

    public string User5Star2 {
        get => _user5Star2;
        set {
            _user5Star2 = value;
            RaisePropertyChangedEvent("User5Star2");
        }
    }

    public string User5Star3 {
        get => _user5Star3;
        set {
            _user5Star3 = value;
            RaisePropertyChangedEvent("User5Star3");
        }
    }

    public string User5Star4 {
        get => _user5Star4;
        set {
            _user5Star4 = value;
            RaisePropertyChangedEvent("User5Star4");
        }
    }

    public string User5Star5 {
        get => _user5Star5;
        set {
            _user5Star5 = value;
            RaisePropertyChangedEvent("User5Star5");
        }
    }

    public string User1Strike1 {
        get => _user1Strike1;
        set {
            _user1Strike1 = value;
            RaisePropertyChangedEvent("User1Strike1");
        }
    }

    public string User1Strike2 {
        get => _user1Strike2;
        set {
            _user1Strike2 = value;
            RaisePropertyChangedEvent("User1Strike2");
        }
    }

    public string User1Strike3 {
        get => _user1Strike3;
        set {
            _user1Strike3 = value;
            RaisePropertyChangedEvent("User1Strike3");
        }
    }

    public string User2Strike1 {
        get => _user2Strike1;
        set {
            _user2Strike1 = value;
            RaisePropertyChangedEvent("User2Strike1");
        }
    }

    public string User2Strike2 {
        get => _user2Strike2;
        set {
            _user2Strike2 = value;
            RaisePropertyChangedEvent("User2Strike2");
        }
    }

    public string User2Strike3 {
        get => _user2Strike3;
        set {
            _user2Strike3 = value;
            RaisePropertyChangedEvent("User2Strike3");
        }
    }

    public string User3Strike1 {
        get => _user3Strike1;
        set {
            _user3Strike1 = value;
            RaisePropertyChangedEvent("User3Strike1");
        }
    }

    public string User3Strike2 {
        get => _user3Strike2;
        set {
            _user3Strike2 = value;
            RaisePropertyChangedEvent("User3Strike2");
        }
    }

    public string User3Strike3 {
        get => _user3Strike3;
        set {
            _user3Strike3 = value;
            RaisePropertyChangedEvent("User3Strike3");
        }
    }

    public string User4Strike1 {
        get => _user4Strike1;
        set {
            _user4Strike1 = value;
            RaisePropertyChangedEvent("User4Strike1");
        }
    }

    public string User4Strike2 {
        get => _user4Strike2;
        set {
            _user4Strike2 = value;
            RaisePropertyChangedEvent("User4Strike2");
        }
    }

    public string User4Strike3 {
        get => _user4Strike3;
        set {
            _user4Strike3 = value;
            RaisePropertyChangedEvent("User4Strike3");
        }
    }

    public string User5Strike1 {
        get => _user5Strike1;
        set {
            _user5Strike1 = value;
            RaisePropertyChangedEvent("User5Strike1");
        }
    }

    public string User5Strike2 {
        get => _user5Strike2;
        set {
            _user5Strike2 = value;
            RaisePropertyChangedEvent("User5Strike2");
        }
    }

    public string User5Strike3 {
        get => _user5Strike3;
        set {
            _user5Strike3 = value;
            RaisePropertyChangedEvent("User5Strike3");
        }
    }

    public BitmapImage ImageUser1 {
        get => _imageUser1;
        set {
            _imageUser1 = value;
            RaisePropertyChangedEvent("ImageUser1");
        }
    }

    public BitmapImage ImageUser2 {
        get => _imageUser2;
        set {
            _imageUser2 = value;
            RaisePropertyChangedEvent("ImageUser2");
        }
    }

    public BitmapImage ImageUser3 {
        get => _imageUser3;
        set {
            _imageUser3 = value;
            RaisePropertyChangedEvent("ImageUser3");
        }
    }

    public BitmapImage ImageUser4 {
        get => _imageUser4;
        set {
            _imageUser4 = value;
            RaisePropertyChangedEvent("ImageUser4");
        }
    }

    public BitmapImage ImageUser5 {
        get => _imageUser5;
        set {
            _imageUser5 = value;
            RaisePropertyChangedEvent("ImageUser5");
        }
    }

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


    public string CurrentQuarterText {
        get => _currentQuarterText;
        set {
            _currentQuarterText = value;
            RaisePropertyChangedEvent("CurrentQuarterText");
        }
    }

    public string User1CashAvailable {
        get => _user1CashAvailable;
        set {
            _user1CashAvailable = value;
            RaisePropertyChangedEvent("User1CashAvailable");
        }
    }

    public int User1ChoresCompletedDayProgressValue {
        get => _user1ChoresCompletedDayProgressValue;
        set {
            _user1ChoresCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User1ChoresCompletedDayProgressValue");
        }
    }

    public int User1ChoresCompletedWeekProgressValue {
        get => _user1ChoresCompletedWeekProgressValue;
        set {
            _user1ChoresCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User1ChoresCompletedWeekProgressValue");
        }
    }

    public int User1ChoresCompletedMonthProgressValue {
        get => _user1ChoresCompletedMonthProgressValue;
        set {
            _user1ChoresCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User1ChoresCompletedMonthProgressValue");
        }
    }

    public int User1ChoresCompletedQuarterProgressValue {
        get => _user1ChoresCompletedQuarterProgressValue;
        set {
            _user1ChoresCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User1ChoresCompletedQuarterProgressValue");
        }
    }

    public string User1ChoresCompletedDayProgressText {
        get => _user1ChoresCompletedDayProgressText;
        set {
            _user1ChoresCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User1ChoresCompletedDayProgressText");
        }
    }

    public string User1ChoresCompletedWeekProgressText {
        get => _user1ChoresCompletedWeekProgressText;
        set {
            _user1ChoresCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User1ChoresCompletedWeekProgressText");
        }
    }

    public string User1ChoresCompletedMonthProgressText {
        get => _user1ChoresCompletedMonthProgressText;
        set {
            _user1ChoresCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User1ChoresCompletedMonthProgressText");
        }
    }

    public string User1ChoresCompletedQuarterProgressText {
        get => _user1ChoresCompletedQuarterProgressText;
        set {
            _user1ChoresCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User1ChoresCompletedQuarterProgressText");
        }
    }

    public string User2CashAvailable {
        get => _user2CashAvailable;
        set {
            _user2CashAvailable = value;
            RaisePropertyChangedEvent("User2CashAvailable");
        }
    }

    public int User2ChoresCompletedDayProgressValue {
        get => _user2ChoresCompletedDayProgressValue;
        set {
            _user2ChoresCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User2ChoresCompletedDayProgressValue");
        }
    }

    public int User2ChoresCompletedWeekProgressValue {
        get => _user2ChoresCompletedWeekProgressValue;
        set {
            _user2ChoresCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User2ChoresCompletedWeekProgressValue");
        }
    }

    public int User2ChoresCompletedMonthProgressValue {
        get => _user2ChoresCompletedMonthProgressValue;
        set {
            _user2ChoresCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User2ChoresCompletedMonthProgressValue");
        }
    }

    public int User2ChoresCompletedQuarterProgressValue {
        get => _user2ChoresCompletedQuarterProgressValue;
        set {
            _user2ChoresCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User2ChoresCompletedQuarterProgressValue");
        }
    }

    public string User2ChoresCompletedDayProgressText {
        get => _user2ChoresCompletedDayProgressText;
        set {
            _user2ChoresCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User2ChoresCompletedDayProgressText");
        }
    }

    public string User2ChoresCompletedWeekProgressText {
        get => _user2ChoresCompletedWeekProgressText;
        set {
            _user2ChoresCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User2ChoresCompletedWeekProgressText");
        }
    }

    public string User2ChoresCompletedMonthProgressText {
        get => _user2ChoresCompletedMonthProgressText;
        set {
            _user2ChoresCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User2ChoresCompletedMonthProgressText");
        }
    }

    public string User2ChoresCompletedQuarterProgressText {
        get => _user2ChoresCompletedQuarterProgressText;
        set {
            _user2ChoresCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User2ChoresCompletedQuarterProgressText");
        }
    }

    public string User3CashAvailable {
        get => _user3CashAvailable;
        set {
            _user3CashAvailable = value;
            RaisePropertyChangedEvent("User3CashAvailable");
        }
    }

    public int User3ChoresCompletedDayProgressValue {
        get => _user3ChoresCompletedDayProgressValue;
        set {
            _user3ChoresCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User3ChoresCompletedDayProgressValue");
        }
    }

    public int User3ChoresCompletedWeekProgressValue {
        get => _user3ChoresCompletedWeekProgressValue;
        set {
            _user3ChoresCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User3ChoresCompletedWeekProgressValue");
        }
    }

    public int User3ChoresCompletedMonthProgressValue {
        get => _user3ChoresCompletedMonthProgressValue;
        set {
            _user3ChoresCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User3ChoresCompletedMonthProgressValue");
        }
    }

    public int User3ChoresCompletedQuarterProgressValue {
        get => _user3ChoresCompletedQuarterProgressValue;
        set {
            _user3ChoresCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User3ChoresCompletedQuarterProgressValue");
        }
    }

    public string User3ChoresCompletedDayProgressText {
        get => _user3ChoresCompletedDayProgressText;
        set {
            _user3ChoresCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User3ChoresCompletedDayProgressText");
        }
    }

    public string User3ChoresCompletedWeekProgressText {
        get => _user3ChoresCompletedWeekProgressText;
        set {
            _user3ChoresCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User3ChoresCompletedWeekProgressText");
        }
    }

    public string User3ChoresCompletedMonthProgressText {
        get => _user3ChoresCompletedMonthProgressText;
        set {
            _user3ChoresCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User3ChoresCompletedMonthProgressText");
        }
    }

    public string User3ChoresCompletedQuarterProgressText {
        get => _user3ChoresCompletedQuarterProgressText;
        set {
            _user3ChoresCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User3ChoresCompletedQuarterProgressText");
        }
    }

    public string User4CashAvailable {
        get => _user4CashAvailable;
        set {
            _user4CashAvailable = value;
            RaisePropertyChangedEvent("User4CashAvailable");
        }
    }

    public int User4ChoresCompletedDayProgressValue {
        get => _user4ChoresCompletedDayProgressValue;
        set {
            _user4ChoresCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User4ChoresCompletedDayProgressValue");
        }
    }

    public int User4ChoresCompletedWeekProgressValue {
        get => _user4ChoresCompletedWeekProgressValue;
        set {
            _user4ChoresCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User4ChoresCompletedWeekProgressValue");
        }
    }

    public int User4ChoresCompletedMonthProgressValue {
        get => _user4ChoresCompletedMonthProgressValue;
        set {
            _user4ChoresCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User4ChoresCompletedMonthProgressValue");
        }
    }

    public int User4ChoresCompletedQuarterProgressValue {
        get => _user4ChoresCompletedQuarterProgressValue;
        set {
            _user4ChoresCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User4ChoresCompletedQuarterProgressValue");
        }
    }

    public string User4ChoresCompletedDayProgressText {
        get => _user4ChoresCompletedDayProgressText;
        set {
            _user4ChoresCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User4ChoresCompletedDayProgressText");
        }
    }

    public string User4ChoresCompletedWeekProgressText {
        get => _user4ChoresCompletedWeekProgressText;
        set {
            _user4ChoresCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User4ChoresCompletedWeekProgressText");
        }
    }

    public string User4ChoresCompletedMonthProgressText {
        get => _user4ChoresCompletedMonthProgressText;
        set {
            _user4ChoresCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User4ChoresCompletedMonthProgressText");
        }
    }

    public string User4ChoresCompletedQuarterProgressText {
        get => _user4ChoresCompletedQuarterProgressText;
        set {
            _user4ChoresCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User4ChoresCompletedQuarterProgressText");
        }
    }

    public string User5CashAvailable {
        get => _user5CashAvailable;
        set {
            _user5CashAvailable = value;
            RaisePropertyChangedEvent("User5CashAvailable");
        }
    }

    public int User5ChoresCompletedDayProgressValue {
        get => _user5ChoresCompletedDayProgressValue;
        set {
            _user5ChoresCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User5ChoresCompletedDayProgressValue");
        }
    }

    public int User5ChoresCompletedWeekProgressValue {
        get => _user5ChoresCompletedWeekProgressValue;
        set {
            _user5ChoresCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User5ChoresCompletedWeekProgressValue");
        }
    }

    public int User5ChoresCompletedMonthProgressValue {
        get => _user5ChoresCompletedMonthProgressValue;
        set {
            _user5ChoresCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User5ChoresCompletedMonthProgressValue");
        }
    }

    public int User5ChoresCompletedQuarterProgressValue {
        get => _user5ChoresCompletedQuarterProgressValue;
        set {
            _user5ChoresCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User5ChoresCompletedQuarterProgressValue");
        }
    }

    public string User5ChoresCompletedDayProgressText {
        get => _user5ChoresCompletedDayProgressText;
        set {
            _user5ChoresCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User5ChoresCompletedDayProgressText");
        }
    }

    public string User5ChoresCompletedWeekProgressText {
        get => _user5ChoresCompletedWeekProgressText;
        set {
            _user5ChoresCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User5ChoresCompletedWeekProgressText");
        }
    }

    public string User5ChoresCompletedMonthProgressText {
        get => _user5ChoresCompletedMonthProgressText;
        set {
            _user5ChoresCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User5ChoresCompletedMonthProgressText");
        }
    }

    public string User5ChoresCompletedQuarterProgressText {
        get => _user5ChoresCompletedQuarterProgressText;
        set {
            _user5ChoresCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User5ChoresCompletedQuarterProgressText");
        }
    }

    public string User1CashAvailableTextColor {
        get => _user1CashAvailableTextColor;
        set {
            _user1CashAvailableTextColor = value;
            RaisePropertyChangedEvent("User1CashAvailableTextColor");
        }
    }

    public string User2CashAvailableTextColor {
        get => _user2CashAvailableTextColor;
        set {
            _user2CashAvailableTextColor = value;
            RaisePropertyChangedEvent("User2CashAvailableTextColor");
        }
    }

    public string User3CashAvailableTextColor {
        get => _user3CashAvailableTextColor;
        set {
            _user3CashAvailableTextColor = value;
            RaisePropertyChangedEvent("User3CashAvailableTextColor");
        }
    }

    public string User4CashAvailableTextColor {
        get => _user4CashAvailableTextColor;
        set {
            _user4CashAvailableTextColor = value;
            RaisePropertyChangedEvent("User4CashAvailableTextColor");
        }
    }

    public string User5CashAvailableTextColor {
        get => _user5CashAvailableTextColor;
        set {
            _user5CashAvailableTextColor = value;
            RaisePropertyChangedEvent("User5CashAvailableTextColor");
        }
    }

    public string RemainingDay {
        get => _remainingDay;
        set {
            _remainingDay = value;
            RaisePropertyChangedEvent("RemainingDay");
        }
    }

    public string RemainingWeek {
        get => _remainingWeek;
        set {
            _remainingWeek = value;
            RaisePropertyChangedEvent("RemainingWeek");
        }
    }

    public string RemainingMonth {
        get => _remainingMonth;
        set {
            _remainingMonth = value;
            RaisePropertyChangedEvent("RemainingMonth");
        }
    }

    public string RemainingQuarter {
        get => _remainingQuarter;
        set {
            _remainingQuarter = value;
            RaisePropertyChangedEvent("RemainingQuarter");
        }
    }

    public string RemainingYear {
        get => _remainingYear;
        set {
            _remainingYear = value;
            RaisePropertyChangedEvent("RemainingYear");
        }
    }

    public string User1ChoresCompletedDayProgressColor {
        get => _user1ChoresCompletedDayProgressColor;
        set {
            _user1ChoresCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User1ChoresCompletedDayProgressColor");
        }
    }

    public string User1ChoresCompletedWeekProgressColor {
        get => _user1ChoresCompletedWeekProgressColor;
        set {
            _user1ChoresCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User1ChoresCompletedWeekProgressColor");
        }
    }

    public string User1ChoresCompletedMonthProgressColor {
        get => _user1ChoresCompletedMonthProgressColor;
        set {
            _user1ChoresCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User1ChoresCompletedMonthProgressColor");
        }
    }

    public string User1ChoresCompletedQuarterProgressColor {
        get => _user1ChoresCompletedQuarterProgressColor;
        set {
            _user1ChoresCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User1ChoresCompletedQuarterProgressColor");
        }
    }
    
    public string User2ChoresCompletedDayProgressColor {
        get => _user2ChoresCompletedDayProgressColor;
        set {
            _user2ChoresCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User2ChoresCompletedDayProgressColor");
        }
    }

    public string User2ChoresCompletedWeekProgressColor {
        get => _user2ChoresCompletedWeekProgressColor;
        set {
            _user2ChoresCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User2ChoresCompletedWeekProgressColor");
        }
    }

    public string User2ChoresCompletedMonthProgressColor {
        get => _user2ChoresCompletedMonthProgressColor;
        set {
            _user2ChoresCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User2ChoresCompletedMonthProgressColor");
        }
    }

    public string User2ChoresCompletedQuarterProgressColor {
        get => _user2ChoresCompletedQuarterProgressColor;
        set {
            _user2ChoresCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User2ChoresCompletedQuarterProgressColor");
        }
    }

    public string User3ChoresCompletedDayProgressColor {
        get => _user3ChoresCompletedDayProgressColor;
        set {
            _user3ChoresCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User3ChoresCompletedDayProgressColor");
        }
    }

    public string User3ChoresCompletedWeekProgressColor {
        get => _user3ChoresCompletedWeekProgressColor;
        set {
            _user3ChoresCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User3ChoresCompletedWeekProgressColor");
        }
    }

    public string User3ChoresCompletedMonthProgressColor {
        get => _user3ChoresCompletedMonthProgressColor;
        set {
            _user3ChoresCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User3ChoresCompletedMonthProgressColor");
        }
    }

    public string User3ChoresCompletedQuarterProgressColor {
        get => _user3ChoresCompletedQuarterProgressColor;
        set {
            _user3ChoresCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User3ChoresCompletedQuarterProgressColor");
        }
    }
    
    public string User4ChoresCompletedDayProgressColor {
        get => _user4ChoresCompletedDayProgressColor;
        set {
            _user4ChoresCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User4ChoresCompletedDayProgressColor");
        }
    }

    public string User4ChoresCompletedWeekProgressColor {
        get => _user4ChoresCompletedWeekProgressColor;
        set {
            _user4ChoresCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User4ChoresCompletedWeekProgressColor");
        }
    }

    public string User4ChoresCompletedMonthProgressColor {
        get => _user4ChoresCompletedMonthProgressColor;
        set {
            _user4ChoresCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User4ChoresCompletedMonthProgressColor");
        }
    }

    public string User4ChoresCompletedQuarterProgressColor {
        get => _user4ChoresCompletedQuarterProgressColor;
        set {
            _user4ChoresCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User4ChoresCompletedQuarterProgressColor");
        }
    }

    public string User5ChoresCompletedDayProgressColor {
        get => _user5ChoresCompletedDayProgressColor;
        set {
            _user5ChoresCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User5ChoresCompletedDayProgressColor");
        }
    }

    public string User5ChoresCompletedWeekProgressColor {
        get => _user5ChoresCompletedWeekProgressColor;
        set {
            _user5ChoresCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User5ChoresCompletedWeekProgressColor");
        }
    }

    public string User5ChoresCompletedMonthProgressColor {
        get => _user5ChoresCompletedMonthProgressColor;
        set {
            _user5ChoresCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User5ChoresCompletedMonthProgressColor");
        }
    }

    public string User5ChoresCompletedQuarterProgressColor {
        get => _user5ChoresCompletedQuarterProgressColor;
        set {
            _user5ChoresCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User5ChoresCompletedQuarterProgressColor");
        }
    }

    #endregion
}