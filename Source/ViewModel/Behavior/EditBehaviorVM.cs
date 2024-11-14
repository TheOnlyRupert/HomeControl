using System;
using System.Globalization;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Behavior;
using HomeControl.Source.ViewModel.Base;
using BloodPressure = HomeControl.Source.Modules.BloodPressure;

namespace HomeControl.Source.ViewModel.Behavior;

public class EditBehaviorVM : BaseViewModel {
    private BitmapImage _imageUser;

    private string _rewardButtonVisibility, _childName, _currentMonthText, _currentWeekText, _currentDayText,
        _currentQuarterText, _childStar1, _childStar2, _childStar3, _childStar4, _childStar5, _childStrike1, _childStrike2, _childStrike3, _tasksCompletedDay, _tasksCompletedWeek,
        _tasksCompletedMonth, _tasksCompletedQuarter, _tasksCompletedDayProgressColor, _tasksCompletedWeekProgressColor, _tasksCompletedMonthProgressColor,
        _tasksCompletedQuarterProgressColor, _tasksCompletedDayProgressText, _tasksCompletedWeekProgressText, _tasksCompletedMonthProgressText, _tasksCompletedQuarterProgressText;

    private byte _stars, _strikes;

    private int _tasksCompletedDayProgressValue, _tasksCompletedWeekProgressValue, _tasksCompletedMonthProgressValue, _tasksCompletedQuarterProgressValue;

    public EditBehaviorVM() {
        DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        System.Globalization.Calendar calendar = dateTimeFormatInfo.Calendar;
        CurrentDayText = DateTime.Now.ToString("dddd");
        CurrentMonthText = DateTime.Now.ToString("MMMM");
        CurrentWeekText = "Week " + calendar.GetWeekOfYear(DateTime.Now, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);

        switch (DateTime.Now.Month) {
        case 1:
        case 2:
        case 3:
            CurrentQuarterText = "Quarter 1";
            break;
        case 4:
        case 5:
        case 6:
            CurrentQuarterText = "Quarter 2";
            break;
        case 7:
        case 8:
        case 9:
            CurrentQuarterText = "Quarter 3";
            break;
        case 10:
        case 11:
        case 12:
            CurrentQuarterText = "Quarter 4";
            break;
        }

        switch (ReferenceValues.ActiveBehaviorUser) {
        case 1:
            ChildName = ReferenceValues.JsonSettingsMaster.User1Name;
            _stars = ReferenceValues.JsonBehaviorMaster.User1Stars;
            _strikes = ReferenceValues.JsonBehaviorMaster.User1Strikes;
            Uri uri;

            try {
                uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user1.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case 2:
            ChildName = ReferenceValues.JsonSettingsMaster.User2Name;
            _stars = ReferenceValues.JsonBehaviorMaster.User2Stars;
            _strikes = ReferenceValues.JsonBehaviorMaster.User2Strikes;
            try {
                uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user2.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case 3:
            ChildName = ReferenceValues.JsonSettingsMaster.User3Name;
            _stars = ReferenceValues.JsonBehaviorMaster.User3Stars;
            _strikes = ReferenceValues.JsonBehaviorMaster.User3Strikes;
            try {
                uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user3.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case 4:
            ChildName = ReferenceValues.JsonSettingsMaster.User4Name;
            _stars = ReferenceValues.JsonBehaviorMaster.User4Stars;
            _strikes = ReferenceValues.JsonBehaviorMaster.User4Strikes;
            try {
                uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user4.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case 5:
            ChildName = ReferenceValues.JsonSettingsMaster.User5Name;
            _stars = ReferenceValues.JsonBehaviorMaster.User5Stars;
            _strikes = ReferenceValues.JsonBehaviorMaster.User5Strikes;
            try {
                uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user5.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        }

        RefreshBehavior();
        RefreshTasks();
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private void RefreshTasks() {
        int totalDay = 0, totalWeek = 0, totalMonth = 0, totalQuarter = 0;
        int completedDay = 0, completedWeek = 0, completedMonth = 0, completedQuarter = 0;
        double math;

        switch (ReferenceValues.ActiveBehaviorUser) {
        case 1:
            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1) {
                totalDay++;
                if (task.IsCompleted) {
                    completedDay++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1) {
                totalWeek++;
                if (task.IsCompleted) {
                    completedWeek++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser1) {
                totalMonth++;
                if (task.IsCompleted) {
                    completedMonth++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser1) {
                totalQuarter++;
                if (task.IsCompleted) {
                    completedQuarter++;
                }
            }

            break;
        case 2:
            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2) {
                totalDay++;
                if (task.IsCompleted) {
                    completedDay++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2) {
                totalWeek++;
                if (task.IsCompleted) {
                    completedWeek++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser2) {
                totalMonth++;
                if (task.IsCompleted) {
                    completedMonth++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser2) {
                totalQuarter++;
                if (task.IsCompleted) {
                    completedQuarter++;
                }
            }

            break;
        case 3:
            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3) {
                totalDay++;
                if (task.IsCompleted) {
                    completedDay++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3) {
                totalWeek++;
                if (task.IsCompleted) {
                    completedWeek++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser3) {
                totalMonth++;
                if (task.IsCompleted) {
                    completedMonth++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser3) {
                totalQuarter++;
                if (task.IsCompleted) {
                    completedQuarter++;
                }
            }

            break;
        case 4:
            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4) {
                totalDay++;
                if (task.IsCompleted) {
                    completedDay++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4) {
                totalWeek++;
                if (task.IsCompleted) {
                    completedWeek++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser4) {
                totalMonth++;
                if (task.IsCompleted) {
                    completedMonth++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser4) {
                totalQuarter++;
                if (task.IsCompleted) {
                    completedQuarter++;
                }
            }

            break;
        case 5:
            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5) {
                totalDay++;
                if (task.IsCompleted) {
                    completedDay++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5) {
                totalWeek++;
                if (task.IsCompleted) {
                    completedWeek++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser5) {
                totalMonth++;
                if (task.IsCompleted) {
                    completedMonth++;
                }
            }

            foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser5) {
                totalQuarter++;
                if (task.IsCompleted) {
                    completedQuarter++;
                }
            }

            break;
        }

        try {
            math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay) * 100;
            TasksCompletedDayProgressValue = (int)math;
        } catch (DivideByZeroException) {
            TasksCompletedDayProgressValue = 0;
        }

        try {
            math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek) * 100;
            TasksCompletedWeekProgressValue = (int)math;
        } catch (DivideByZeroException) {
            TasksCompletedWeekProgressValue = 0;
        }

        try {
            math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth) * 100;
            TasksCompletedMonthProgressValue = (int)math;
        } catch (DivideByZeroException) {
            TasksCompletedMonthProgressValue = 0;
        }

        try {
            math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter) * 100;
            TasksCompletedQuarterProgressValue = (int)math;
        } catch (DivideByZeroException) {
            TasksCompletedQuarterProgressValue = 0;
        }

        TasksCompletedDayProgressColor = TasksCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
        TasksCompletedWeekProgressColor = TasksCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
        TasksCompletedMonthProgressColor = TasksCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
        TasksCompletedQuarterProgressColor = TasksCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";

        /* Shows tasks completed and money estimated */
        if (totalDay == 0) {
            TasksCompletedDay = "None";
        } else {
            TasksCompletedDay = completedDay + "/" + totalDay;
            TasksCompletedDayProgressText = TasksCompletedDayProgressValue + "%";
        }

        if (totalWeek == 0) {
            TasksCompletedWeek = "None";
        } else {
            TasksCompletedWeek = completedWeek + "/" + totalWeek;
            TasksCompletedWeekProgressText = TasksCompletedWeekProgressValue + "%";
        }

        if (totalMonth == 0) {
            TasksCompletedMonth = "None";
        } else {
            TasksCompletedMonth = completedMonth + "/" + totalMonth;
            TasksCompletedMonthProgressText = TasksCompletedMonthProgressValue + "%";
        }

        if (totalQuarter == 0) {
            TasksCompletedQuarter = "None";
        } else {
            TasksCompletedQuarter = completedQuarter + "/" + totalQuarter;
            TasksCompletedQuarterProgressText = TasksCompletedQuarterProgressValue + "%";
        }
    }

    private void RefreshBehavior() {
        ChildStar1 = "";
        ChildStar2 = "";
        ChildStar3 = "";
        ChildStar4 = "";
        ChildStar5 = "";
        ChildStrike1 = "";
        ChildStrike2 = "";
        ChildStrike3 = "";

        switch (_stars) {
        case 1:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar2 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar2 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar3 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar2 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar3 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar4 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (_strikes) {
        case 1:
            ChildStrike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            ChildStrike1 = "../../../Resources/Images/behavior/strike_red.png";
            ChildStrike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            ChildStrike1 = "../../../Resources/Images/behavior/strike_red.png";
            ChildStrike2 = "../../../Resources/Images/behavior/strike_red.png";
            ChildStrike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        RewardButtonVisibility = _stars == 5 ? "VISIBLE" : "HIDDEN";

        /* Save Progress */
        switch (ReferenceValues.ActiveBehaviorUser) {
        case 1:
            ReferenceValues.JsonBehaviorMaster.User1Stars = _stars;
            ReferenceValues.JsonBehaviorMaster.User1Strikes = _strikes;

            break;
        case 2:
            ReferenceValues.JsonBehaviorMaster.User2Stars = _stars;
            ReferenceValues.JsonBehaviorMaster.User2Strikes = _strikes;

            break;
        case 3:
            ReferenceValues.JsonBehaviorMaster.User3Stars = _stars;
            ReferenceValues.JsonBehaviorMaster.User3Strikes = _strikes;

            break;
        case 4:
            ReferenceValues.JsonBehaviorMaster.User4Stars = _stars;
            ReferenceValues.JsonBehaviorMaster.User4Strikes = _strikes;

            break;
        case 5:
            ReferenceValues.JsonBehaviorMaster.User5Stars = _stars;
            ReferenceValues.JsonBehaviorMaster.User5Strikes = _strikes;

            break;
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "addStrike":
            if (RewardButtonVisibility == "HIDDEN") {
                if (_strikes < 3) {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to add a strike?\nThis will reset all progress (but not stars)", "Confirmation",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes) {
                        _strikes++;
                        ReferenceValues.SoundToPlay = "buzzer";
                        SoundDispatcher.PlaySound();
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditBehaviorVM",
                            Description = "Adding strike to " + ChildName
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        if (_strikes == 3) {
                            _stars--;
                            if (_stars < 0) {
                                _stars = 0;
                            }
                        }
                    }
                } else {
                    ReferenceValues.SoundToPlay = "unable";
                    SoundDispatcher.PlaySound();
                }
            }

            break;
        case "add1":
            if (RewardButtonVisibility == "HIDDEN" && _strikes != 3 && _stars < 5) {
                _stars++;

                ReferenceValues.SoundToPlay = "yay";
                SoundDispatcher.PlaySound();
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditBehaviorVM",
                    Description = "Adding star to " + ChildName
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            } else {
                ReferenceValues.SoundToPlay = "unable";
                SoundDispatcher.PlaySound();
            }

            break;
        case "remove1":
            if (RewardButtonVisibility == "HIDDEN" && _strikes != 3 && _stars > 0) {
                _stars--;
                ReferenceValues.SoundToPlay = "aww";
                SoundDispatcher.PlaySound();
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditBehaviorVM",
                    Description = "Removing star from " + ChildName
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            } else {
                ReferenceValues.SoundToPlay = "unable";
                SoundDispatcher.PlaySound();
            }

            break;
        case "reward":
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "EditBehaviorVM",
                Description = ChildName + " claimed their reward!"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            _stars = 0;
            RewardButtonVisibility = "HIDDEN";
            ReferenceValues.SoundToPlay = "reward";
            SoundDispatcher.PlaySound();

            break;
        case "daily":
            TasksDaily tasksDaily = new();
            tasksDaily.ShowDialog();
            tasksDaily.Close();
            RefreshTasks();

            break;
        case "weekly":
            TasksWeekly tasksWeekly = new();
            tasksWeekly.ShowDialog();
            tasksWeekly.Close();
            RefreshTasks();

            break;
        case "monthly":
            TasksMonthly tasksMonthly = new();
            tasksMonthly.ShowDialog();
            tasksMonthly.Close();
            RefreshTasks();

            break;
        case "quarterly":
            TasksQuarterly tasksQuarterly = new();
            tasksQuarterly.ShowDialog();
            tasksQuarterly.Close();
            RefreshTasks();

            break;
        case "bloodPressure":
            BloodPressure bloodPressure = new();
            bloodPressure.ShowDialog();
            bloodPressure.Close();

            break;
        }

        RefreshBehavior();
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


    public string CurrentQuarterText {
        get => _currentQuarterText;
        set {
            _currentQuarterText = value;
            RaisePropertyChangedEvent("CurrentQuarterText");
        }
    }

    public string ChildName {
        get => _childName;
        set {
            _childName = value;
            RaisePropertyChangedEvent("ChildName");
        }
    }

    public string RewardButtonVisibility {
        get => _rewardButtonVisibility;
        set {
            _rewardButtonVisibility = value;
            RaisePropertyChangedEvent("RewardButtonVisibility");
        }
    }

    public BitmapImage ImageUser {
        get => _imageUser;
        set {
            _imageUser = value;
            RaisePropertyChangedEvent("ImageUser");
        }
    }

    public string ChildStar1 {
        get => _childStar1;
        set {
            _childStar1 = value;
            RaisePropertyChangedEvent("ChildStar1");
        }
    }

    public string ChildStar2 {
        get => _childStar2;
        set {
            _childStar2 = value;
            RaisePropertyChangedEvent("ChildStar2");
        }
    }

    public string ChildStar3 {
        get => _childStar3;
        set {
            _childStar3 = value;
            RaisePropertyChangedEvent("ChildStar3");
        }
    }

    public string ChildStar4 {
        get => _childStar4;
        set {
            _childStar4 = value;
            RaisePropertyChangedEvent("ChildStar4");
        }
    }

    public string ChildStar5 {
        get => _childStar5;
        set {
            _childStar5 = value;
            RaisePropertyChangedEvent("ChildStar5");
        }
    }

    public string ChildStrike1 {
        get => _childStrike1;
        set {
            _childStrike1 = value;
            RaisePropertyChangedEvent("ChildStrike1");
        }
    }

    public string ChildStrike2 {
        get => _childStrike2;
        set {
            _childStrike2 = value;
            RaisePropertyChangedEvent("ChildStrike2");
        }
    }

    public string ChildStrike3 {
        get => _childStrike3;
        set {
            _childStrike3 = value;
            RaisePropertyChangedEvent("ChildStrike3");
        }
    }

    public string TasksCompletedDay {
        get => _tasksCompletedDay;
        set {
            _tasksCompletedDay = value;
            RaisePropertyChangedEvent("TasksCompletedDay");
        }
    }

    public string TasksCompletedWeek {
        get => _tasksCompletedWeek;
        set {
            _tasksCompletedWeek = value;
            RaisePropertyChangedEvent("TasksCompletedWeek");
        }
    }

    public string TasksCompletedMonth {
        get => _tasksCompletedMonth;
        set {
            _tasksCompletedMonth = value;
            RaisePropertyChangedEvent("TasksCompletedMonth");
        }
    }

    public string TasksCompletedQuarter {
        get => _tasksCompletedQuarter;
        set {
            _tasksCompletedQuarter = value;
            RaisePropertyChangedEvent("TasksCompletedQuarter");
        }
    }

    public int TasksCompletedDayProgressValue {
        get => _tasksCompletedDayProgressValue;
        set {
            _tasksCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("TasksCompletedDayProgressValue");
        }
    }

    public int TasksCompletedWeekProgressValue {
        get => _tasksCompletedWeekProgressValue;
        set {
            _tasksCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("TasksCompletedWeekProgressValue");
        }
    }

    public int TasksCompletedMonthProgressValue {
        get => _tasksCompletedMonthProgressValue;
        set {
            _tasksCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("TasksCompletedMonthProgressValue");
        }
    }

    public int TasksCompletedQuarterProgressValue {
        get => _tasksCompletedQuarterProgressValue;
        set {
            _tasksCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("TasksCompletedQuarterProgressValue");
        }
    }

    public string TasksCompletedDayProgressColor {
        get => _tasksCompletedDayProgressColor;
        set {
            _tasksCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("TasksCompletedDayProgressColor");
        }
    }

    public string TasksCompletedWeekProgressColor {
        get => _tasksCompletedWeekProgressColor;
        set {
            _tasksCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("TasksCompletedWeekProgressColor");
        }
    }

    public string TasksCompletedMonthProgressColor {
        get => _tasksCompletedMonthProgressColor;
        set {
            _tasksCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("TasksCompletedMonthProgressColor");
        }
    }

    public string TasksCompletedQuarterProgressColor {
        get => _tasksCompletedQuarterProgressColor;
        set {
            _tasksCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("TasksCompletedQuarterProgressColor");
        }
    }

    public string TasksCompletedDayProgressText {
        get => _tasksCompletedDayProgressText;
        set {
            _tasksCompletedDayProgressText = value;
            RaisePropertyChangedEvent("TasksCompletedDayProgressText");
        }
    }

    public string TasksCompletedWeekProgressText {
        get => _tasksCompletedWeekProgressText;
        set {
            _tasksCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("TasksCompletedWeekProgressText");
        }
    }

    public string TasksCompletedMonthProgressText {
        get => _tasksCompletedMonthProgressText;
        set {
            _tasksCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("TasksCompletedMonthProgressText");
        }
    }

    public string TasksCompletedQuarterProgressText {
        get => _tasksCompletedQuarterProgressText;
        set {
            _tasksCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("TasksCompletedQuarterProgressText");
        }
    }

    #endregion
}