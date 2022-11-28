using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using HomeControl.Source.Helpers;
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
        ChoreWeekStartDate = new DateTime();

        RefreshFields();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

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
        JsonChoreMasterList = new JsonChoresWeek {
            choreList = new ObservableCollection<ChoreDetails>()
        };
        choresCompletedWeek = 0;
        choresCompletedMonth = 0;

        while (ChoreWeekStartDate.DayOfWeek != DayOfWeek.Sunday) {
            ChoreWeekStartDate = dateTime.AddDays(-1);
        }

        new ChoresFromJson(ChoreWeekStartDate);

        CurrentMonthText = dateTime.ToString("MMMM");
        CurrentWeekText = "Week: " + calendar.GetWeekOfYear(dateTime, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);

        ChoresTitleText = User2Name + " Monthly Chores";

        CurrentWeekSpanText = "TODO";
        ProjectedFundMonthText = dateTime.AddMonths(1).ToString("MMM") + " Projected Funds";
        ProjectedFundLevelText = "Level TODO";
        ProjectedFundCashText = "Cash TODO";
        ChoresCompletedWeekProgressText = "0%";

        if (JsonChoreMasterList != null) {
            foreach (ChoreDetails choreDetails in JsonChoreMasterList.choreList) {
                if (choreDetails.IsComplete) {
                    choresCompletedWeek++;
                }
            }
        }


        if (JsonChoreMasterList != null) {
            ChoresCompletedWeekText = choresCompletedWeek + " / " + JsonChoreMasterList.choreList.Count;
            double progress = Convert.ToDouble(choresCompletedWeek) / Convert.ToDouble(JsonChoreMasterList.choreList.Count) * 100;
            try {
                ChoresCompletedWeekProgressText = Convert.ToInt16(progress) + "%";
            } catch (Exception) { }
        }
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