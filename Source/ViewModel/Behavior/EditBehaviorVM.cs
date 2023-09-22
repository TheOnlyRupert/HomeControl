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

namespace HomeControl.Source.ViewModel.Behavior;

public class EditBehaviorVM : BaseViewModel {
    private BitmapImage _imageUser;

    private int _progressBarChildValue, stars, strikes, _tasksCompletedDayProgressValue, _tasksCompletedWeekProgressValue, _tasksCompletedMonthProgressValue,
        _tasksCompletedQuarterProgressValue;

    private string _rewardButtonVisibility, _progressBarChildValueText, _childName, _cashAvailable, _cashAvailableColor, _currentMonthText, _currentWeekText, _currentDayText,
        _currentQuarterText, _childStar1, _childStar2, _childStar3, _childStar4, _childStar5, _childStrike1, _childStrike2, _childStrike3, _tasksCompletedDay, _tasksCompletedWeek,
        _tasksCompletedMonth, _tasksCompletedQuarter, _tasksCompletedDayProgressColor, _tasksCompletedWeekProgressColor, _tasksCompletedMonthProgressColor,
        _tasksCompletedQuarterProgressColor, _tasksCompletedDayProgressText, _tasksCompletedWeekProgressText, _tasksCompletedMonthProgressText, _tasksCompletedQuarterProgressText;

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
            stars = ReferenceValues.JsonBehaviorMaster.User1Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User1Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User1Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User1Progress + "/5";
            Uri uri;

            try {
                uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user1.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
            }

            break;
        case 2:
            ChildName = ReferenceValues.JsonSettingsMaster.User2Name;
            stars = ReferenceValues.JsonBehaviorMaster.User2Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User2Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User2Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User2Progress + "/5";
            try {
                uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user2.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
            }

            break;
        case 3:
            ChildName = ReferenceValues.JsonSettingsMaster.User3Name;
            stars = ReferenceValues.JsonBehaviorMaster.User3Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User3Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User3Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User3Progress + "/5";
            try {
                uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user3.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
            }

            break;
        case 4:
            ChildName = ReferenceValues.JsonSettingsMaster.User4Name;
            stars = ReferenceValues.JsonBehaviorMaster.User4Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User4Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User4Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User4Progress + "/5";
            try {
                uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user4.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
            }

            break;
        case 5:
            ChildName = ReferenceValues.JsonSettingsMaster.User5Name;
            stars = ReferenceValues.JsonBehaviorMaster.User5Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User5Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User5Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User5Progress + "/5";
            try {
                uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user5.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
            }

            break;
        }

        RefreshBehavior();
        RefreshTasks();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void RefreshTasks() {
        int totalDay = 0, totalWeek = 0, totalMonth = 0, totalQuarter = 0;
        int completedDay = 0, completedWeek = 0, completedMonth = 0, completedQuarter = 0;
        int releaseAmountDaily = 0, releaseAmountWeekly = 0, releaseAmountMonthly = 0, releaseAmountQuarterly = 0;
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

            math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
            releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser1);

            math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
            releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser1);

            math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
            releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser1);

            math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
            releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser1);

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

            math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
            releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser2);

            math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
            releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser2);

            math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
            releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser2);

            math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
            releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser2);

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

            math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
            releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser3);

            math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
            releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser3);

            math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
            releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser3);

            math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
            releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser3);

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

            math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
            releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser4);

            math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
            releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser4);

            math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
            releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser4);

            math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
            releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser4);

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

            math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
            releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser5);

            math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
            releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser5);

            math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
            releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser5);

            math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
            releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser5);

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
            TasksCompletedDay = completedDay + "/" + totalDay + "  - $" + releaseAmountDaily;
            TasksCompletedDayProgressText = TasksCompletedDayProgressValue + "%";
        }

        if (totalWeek == 0) {
            TasksCompletedWeek = "None";
        } else {
            TasksCompletedWeek = completedWeek + "/" + totalWeek + "  - $" + releaseAmountWeekly;
            TasksCompletedWeekProgressText = TasksCompletedWeekProgressValue + "%";
        }

        if (totalMonth == 0) {
            TasksCompletedMonth = "None";
        } else {
            TasksCompletedMonth = completedMonth + "/" + totalMonth + "  - $" + releaseAmountMonthly;
            TasksCompletedMonthProgressText = TasksCompletedMonthProgressValue + "%";
        }

        if (totalQuarter == 0) {
            TasksCompletedQuarter = "None";
        } else {
            TasksCompletedQuarter = completedQuarter + "/" + totalQuarter + "  - $" + releaseAmountQuarterly;
            TasksCompletedQuarterProgressText = TasksCompletedQuarterProgressValue + "%";
        }
    }

    private void RefreshBehavior() {
        ReferenceValues.JsonBehaviorMaster.Date = DateTime.Now;
        ChildStar1 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar2 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar3 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar4 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar5 = "../../../Resources/Images/behavior/star_black.png";
        ChildStrike1 = "";
        ChildStrike2 = "";
        ChildStrike3 = "";

        switch (stars) {
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

        switch (strikes) {
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

        RewardButtonVisibility = stars == 5 ? "VISIBLE" : "HIDDEN";

        /* Save Progress */
        switch (ReferenceValues.ActiveBehaviorUser) {
        case 1:
            ReferenceValues.JsonBehaviorMaster.User1Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User1Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User1Progress = ProgressBarChildValue;
            break;
        case 2:
            ReferenceValues.JsonBehaviorMaster.User2Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User2Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User2Progress = ProgressBarChildValue;
            break;
        case 3:
            ReferenceValues.JsonBehaviorMaster.User3Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User3Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User3Progress = ProgressBarChildValue;
            break;
        case 4:
            ReferenceValues.JsonBehaviorMaster.User4Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User4Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User4Progress = ProgressBarChildValue;
            break;
        case 5:
            ReferenceValues.JsonBehaviorMaster.User5Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User5Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User5Progress = ProgressBarChildValue;
            break;
        }

        try {
            FileHelpers.SaveFileText("behavior", JsonSerializer.Serialize(ReferenceValues.JsonBehaviorMaster));
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditBehaviorVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "addStrike":
            if (RewardButtonVisibility == "HIDDEN") {
                if (strikes < 3) {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to add a strike?\nThis will reset all progress (but not stars)", "Confirmation",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes) {
                        strikes++;
                        ProgressBarChildValue = 0;
                        ProgressBarChildValueText = "0/5";
                        ReferenceValues.SoundToPlay = "buzzer";
                        SoundDispatcher.PlaySound();
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditBehaviorVM",
                            Description = "Adding strike to " + ChildName
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));

                        if (strikes == 3) {
                            stars--;
                            if (stars < 0) {
                                stars = 0;
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
            if (RewardButtonVisibility == "HIDDEN") {
                if (strikes != 3) {
                    ProgressBarChildValue++;
                    ProgressBarChildValueText = ProgressBarChildValue + "/5";
                    if (ProgressBarChildValue > 4) {
                        if (stars < 5) {
                            stars++;
                            ProgressBarChildValue = 0;
                            ProgressBarChildValueText = "0/5";

                            if (stars == 5) {
                                ProgressBarChildValue = 5;
                                ProgressBarChildValueText = "5/5";
                            }

                            ReferenceValues.SoundToPlay = "yay";
                            SoundDispatcher.PlaySound();
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditBehaviorVM",
                                Description = "Adding progress (which resulted in a star) to " + ChildName
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
                        } else {
                            ProgressBarChildValue = 5;
                            ProgressBarChildValueText = "5/5";
                        }
                    } else {
                        ReferenceValues.SoundToPlay = "ding";
                        SoundDispatcher.PlaySound();
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditBehaviorVM",
                            Description = "Adding progress to " + ChildName
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
                    }
                } else {
                    ReferenceValues.SoundToPlay = "unable";
                    SoundDispatcher.PlaySound();
                }
            }

            break;
        case "remove1":
            if (RewardButtonVisibility == "HIDDEN" && strikes != 3) {
                ProgressBarChildValue--;
                ProgressBarChildValueText = ProgressBarChildValue + "/5";
                if (ProgressBarChildValue < 0) {
                    if (stars > 0) {
                        stars--;
                        ProgressBarChildValue = 4;
                        ProgressBarChildValueText = "4/5";
                        ReferenceValues.SoundToPlay = "aww";
                        SoundDispatcher.PlaySound();
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditBehaviorVM",
                            Description = "Removing progress (which resulted in a loss of a star) from " + ChildName
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
                    } else {
                        ProgressBarChildValue = 0;
                        ProgressBarChildValueText = "0/5";
                    }
                } else {
                    ReferenceValues.SoundToPlay = "error";
                    SoundDispatcher.PlaySound();
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "EditBehaviorVM",
                        Description = "Removing progress from " + ChildName
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
                }
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
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
            stars = 0;
            ProgressBarChildValue = 0;
            ProgressBarChildValueText = "0/5";
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

    public int ProgressBarChildValue {
        get => _progressBarChildValue;
        set {
            _progressBarChildValue = value;
            RaisePropertyChangedEvent("ProgressBarChildValue");
        }
    }

    public string ProgressBarChildValueText {
        get => _progressBarChildValueText;
        set {
            _progressBarChildValueText = value;
            RaisePropertyChangedEvent("ProgressBarChildValueText");
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

    public string CashAvailable {
        get => _cashAvailable;
        set {
            _cashAvailable = value;
            RaisePropertyChangedEvent("CashAvailable");
        }
    }

    public string CashAvailableColor {
        get => _cashAvailableColor;
        set {
            _cashAvailableColor = value;
            RaisePropertyChangedEvent("CashAvailableColor");
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