using System;
using System.Globalization;
using System.Windows.Input;
using HomeControl.Source.Modules.Chores;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresVM : BaseViewModel {
    private string _currentMonthText, _currentWeekText, _choresCompletedWeekText, _choresCompletedMonthText, _choresTitleText, _currentWeekSpanText,
        _choresCompletedWeekProgressText, _choresCompletedWeekProgressValue, _projectedFundMonthText, _projectedFundLevelText, _projectedFundCashText;

    public ChoresVM() {
        DateTime dateTime = DateTime.Now;
        DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        System.Globalization.Calendar calendar = dateTimeFormatInfo.Calendar;

        CurrentMonthText = dateTime.ToString("MMMM");
        CurrentWeekText = "Week: " + calendar.GetWeekOfYear(dateTime, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);
        ChoresCompletedWeekText = ReferenceValues.ChoresWeekCompleted + " / 24";
        ChoresCompletedMonthText = ReferenceValues.ChoresMonthCompleted + " / 32";
        ChoresTitleText = ReferenceValues.User2Name + " Monthly Chores";

        CurrentWeekSpanText = "TODO";
        ChoresCompletedWeekProgressText = ReferenceValues.ChoresWeekCompleted / 24 * 100 + "%";
        ChoresCompletedWeekProgressValue = "1";
        ProjectedFundMonthText = dateTime.AddMonths(1).ToString("MMM") + " Projected Funds";
        ProjectedFundLevelText = "Level TODO";
        ProjectedFundCashText = "Cash TODO";
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "choresWeek":
            ChoresWeek choresWeek = new();
            choresWeek.ShowDialog();
            choresWeek.Close();
            break;
        case "choresMonth":
            break;
        case "funds":
            break;
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

    public string ChoresCompletedWeekProgressValue {
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