using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Chores;
using HomeControl.Source.ViewModel.Base;
using static HomeControl.Source.Reference.ReferenceValues;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresVM : BaseViewModel {
    private string _currentMonthText, _currentWeekText, _choresCompletedWeekText, _choresCompletedMonthText, _choresTitleText, _currentWeekSpanText,
        _choresCompletedWeekProgressText, _projectedFundMonthText, _projectedFundLevelText, _projectedFundCashText;

    private int choresCompletedWeek, choresCompletedMonth, _choresCompletedWeekProgressValue;

    public ChoresVM() {
        ChoreWeekStartDate = DateTime.Now;

        RefreshFields();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            RefreshFields();
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
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
        case "funds":
            break;
        }
    }

    private void RefreshFields() {
        DateTime dateTime = DateTime.Now;
        DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        System.Globalization.Calendar calendar = dateTimeFormatInfo.Calendar;
        JsonChoreWeekMasterList = new JsonChores {
            choreList = new ObservableCollection<ChoreDetails>()
        };
        choresCompletedWeek = 0;
        choresCompletedMonth = 0;

        while (ChoreWeekStartDate.DayOfWeek != DayOfWeek.Sunday) {
            ChoreWeekStartDate = ChoreWeekStartDate.AddDays(-1);
        }

        ChoreMonthStartDate = DateTime.Now;

        ChoresFromJson choresFromJson = new();
        choresFromJson.ChoresWeekFromJson(ChoreWeekStartDate);
        choresFromJson.ChoresMonthFromJson(ChoreMonthStartDate);

        CurrentMonthText = dateTime.ToString("MMMM");
        CurrentWeekText = "Week: " + calendar.GetWeekOfYear(dateTime, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);

        ChoresTitleText = User2Name + " Monthly Chores";

        CurrentWeekSpanText = ChoreWeekStartDate.ToString("MMM dd") + " - " + ChoreWeekStartDate.AddDays(6).ToString("MMM dd");
        ProjectedFundMonthText = dateTime.AddMonths(1).ToString("MMM") + " Projected Funds";
        ChoresCompletedWeekProgressText = "0%";

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

        ChoresCompletedWeekText = choresCompletedWeek + " / " + JsonChoreWeekMasterList.choreList.Count;
        ChoresCompletedMonthText = choresCompletedMonth + " / " + JsonChoreMonthMasterList.choreList.Count;

        double progress = Convert.ToDouble(choresCompletedWeek) / Convert.ToDouble(JsonChoreWeekMasterList.choreList.Count) * 100;
        try {
            ChoresCompletedWeekProgressText = Convert.ToInt16(progress) + "%";
            ChoresCompletedWeekProgressValue = Convert.ToInt16(progress);
        } catch (Exception) { }
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

    public string ChoresTitleText {
        get => _choresTitleText;
        set {
            _choresTitleText = value;
            RaisePropertyChangedEvent("ChoresTitleText");
        }
    }

    public string CurrentWeekSpanText {
        get => _currentWeekSpanText;
        set {
            _currentWeekSpanText = value;
            RaisePropertyChangedEvent("CurrentWeekSpanText");
        }
    }

    public string ChoresCompletedWeekProgressText {
        get => _choresCompletedWeekProgressText;
        set {
            _choresCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("ChoresCompletedWeekProgressText");
        }
    }

    public int ChoresCompletedWeekProgressValue {
        get => _choresCompletedWeekProgressValue;
        set {
            _choresCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("ChoresCompletedWeekProgressValue");
        }
    }

    public string ProjectedFundMonthText {
        get => _projectedFundMonthText;
        set {
            _projectedFundMonthText = value;
            RaisePropertyChangedEvent("ProjectedFundMonthText");
        }
    }

    public string ProjectedFundLevelText {
        get => _projectedFundLevelText;
        set {
            _projectedFundLevelText = value;
            RaisePropertyChangedEvent("ProjectedFundLevelText");
        }
    }

    public string ProjectedFundCashText {
        get => _projectedFundCashText;
        set {
            _projectedFundCashText = value;
            RaisePropertyChangedEvent("ProjectedFundCashText");
        }
    }

    #endregion
}