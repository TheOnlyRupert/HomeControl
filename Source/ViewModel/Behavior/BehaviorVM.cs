using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Behavior;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Behavior;

public class BehaviorVM : BaseViewModel {
    private BitmapImage _imageUser1, _imageUser2, _imageUser3, _imageUser4, _imageUser5;

    private string _user1Star1, _user1Star2, _user1Star3, _user1Star4, _user1Star5, _user2Star1, _user2Star2, _user2Star3, _user2Star4, _user2Star5, _user3Star1, _user3Star2, _user3Star3, _user3Star4,
        _user3Star5, _user4Star1, _user4Star2, _user4Star3, _user4Star4, _user4Star5, _user5Star1, _user5Star2, _user5Star3, _user5Star4, _user5Star5, _user1Strike1, _user1Strike2, _user1Strike3,
        _user2Strike1, _user2Strike2, _user2Strike3, _user3Strike1, _user3Strike2, _user3Strike3, _user4Strike1, _user4Strike2, _user4Strike3, _user5Strike1, _user5Strike2, _user5Strike3,
        _user1TasksCompletedDayProgressColor, _user1TasksCompletedWeekProgressColor, _user1TasksCompletedMonthProgressColor, _user1TasksCompletedQuarterProgressColor,
        _user2TasksCompletedDayProgressColor, _user2TasksCompletedWeekProgressColor, _user2TasksCompletedMonthProgressColor, _user2TasksCompletedQuarterProgressColor,
        _user3TasksCompletedDayProgressColor, _user3TasksCompletedWeekProgressColor, _user3TasksCompletedMonthProgressColor, _user3TasksCompletedQuarterProgressColor,
        _user4TasksCompletedDayProgressColor, _user4TasksCompletedWeekProgressColor, _user4TasksCompletedMonthProgressColor, _user4TasksCompletedQuarterProgressColor,
        _user5TasksCompletedDayProgressColor, _user5TasksCompletedWeekProgressColor, _user5TasksCompletedMonthProgressColor, _user5TasksCompletedQuarterProgressColor, _currentMonthText,
        _currentWeekText, _currentDayText, _user1CashAvailableColor, _user2CashAvailableColor, _user3CashAvailableColor, _user4CashAvailableColor, _user5CashAvailableColor, _currentQuarterText,
        _user1TasksCompletedWeekProgressText, _user1TasksCompletedDayProgressText, _user1TasksCompletedMonthProgressText, _user1TasksCompletedQuarterProgressText, _user1CashAvailable,
        _user2TasksCompletedWeekProgressText, _user2TasksCompletedDayProgressText, _user2TasksCompletedMonthProgressText, _user2TasksCompletedQuarterProgressText, _user2CashAvailable,
        _user3TasksCompletedWeekProgressText, _user3TasksCompletedDayProgressText, _user3TasksCompletedMonthProgressText, _user3TasksCompletedQuarterProgressText, _user3CashAvailable,
        _user4TasksCompletedWeekProgressText, _user4TasksCompletedDayProgressText, _user4TasksCompletedMonthProgressText, _user4TasksCompletedQuarterProgressText, _user4CashAvailable,
        _user5TasksCompletedWeekProgressText, _user5TasksCompletedDayProgressText, _user5TasksCompletedMonthProgressText, _user5TasksCompletedQuarterProgressText, _user5CashAvailable, _remainingDay,
        _remainingWeek, _remainingMonth, _remainingQuarter, _remainingYear, _user1CashReleased, _user2CashReleased, _user3CashReleased, _user4CashReleased, _user5CashReleased, _user1DayVisibility,
        _user2DayVisibility, _user3DayVisibility, _user4DayVisibility, _user5DayVisibility, _user1WeekVisibility, _user2WeekVisibility, _user3WeekVisibility, _user4WeekVisibility,
        _user5WeekVisibility, _user1MonthVisibility, _user2MonthVisibility, _user3MonthVisibility, _user4MonthVisibility, _user5MonthVisibility, _user1QuarterVisibility, _user2QuarterVisibility,
        _user3QuarterVisibility, _user4QuarterVisibility, _user5QuarterVisibility, _user1BehaviorVisibility, _user2BehaviorVisibility, _user3BehaviorVisibility, _user4BehaviorVisibility,
        _user5BehaviorVisibility, _trashDayVisibility, _user1BackgroundColor, _user2BackgroundColor, _user3BackgroundColor, _user4BackgroundColor, _user5BackgroundColor, _remainingDayColor,
        _remainingWeekColor, _remainingMonthColor, _remainingQuarterColor, _remainingYearColor;

    private int _user1TasksCompletedDayProgressValue, _user1TasksCompletedWeekProgressValue, _user1TasksCompletedMonthProgressValue, _user1TasksCompletedQuarterProgressValue,
        _user2TasksCompletedDayProgressValue, _user2TasksCompletedWeekProgressValue, _user2TasksCompletedMonthProgressValue, _user2TasksCompletedQuarterProgressValue,
        _user3TasksCompletedDayProgressValue, _user3TasksCompletedWeekProgressValue, _user3TasksCompletedMonthProgressValue, _user3TasksCompletedQuarterProgressValue,
        _user4TasksCompletedDayProgressValue, _user4TasksCompletedWeekProgressValue, _user4TasksCompletedMonthProgressValue, _user4TasksCompletedQuarterProgressValue,
        _user5TasksCompletedDayProgressValue, _user5TasksCompletedWeekProgressValue, _user5TasksCompletedMonthProgressValue, _user5TasksCompletedQuarterProgressValue,
        _progressBarUser1, _progressBarUser2, _progressBarUser3, _progressBarUser4, _progressBarUser5;

    private bool blinkDay, blinkWeek, blinkMonth, blinkQuarter, blinkYear;

    public BehaviorVM() {
        try {
            ReferenceValues.JsonBehaviorMaster = JsonSerializer.Deserialize<JsonBehavior>(FileHelpers.LoadFileText("behavior", true));
        } catch (Exception) {
            ReferenceValues.JsonBehaviorMaster = new JsonBehavior();

            FileHelpers.SaveFileText("behavior", JsonSerializer.Serialize(ReferenceValues.JsonBehaviorMaster), true);
        }

        try {
            ReferenceValues.JsonTasksMaster = JsonSerializer.Deserialize<JsonTasks>(FileHelpers.LoadFileText("tasks", true));
        } catch (Exception) {
            ReferenceValues.JsonTasksMaster = new JsonTasks();

            FileHelpers.SaveFileText("tasks", JsonSerializer.Serialize(ReferenceValues.JsonTasksMaster), true);
        }

        ReferenceValues.JsonTasksMaster ??= new JsonTasks();
        ReferenceValues.JsonTasksMaster.JsonTasksDaily ??= new JsonTasksDaily();
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly ??= new JsonTasksWeekly();
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly ??= new JsonTasksMonthly();
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser1 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser2 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser3 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser4 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser5 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly ??= new JsonTasksQuarterly();
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser1 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser2 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser3 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser4 ??= new ObservableCollection<Task>();
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser5 ??= new ObservableCollection<Task>();

        try {
            Uri uri = new(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user1.png", UriKind.RelativeOrAbsolute);
            ImageUser1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user2.png", UriKind.RelativeOrAbsolute);
            ImageUser2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user3.png", UriKind.RelativeOrAbsolute);
            ImageUser3 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user4.png", UriKind.RelativeOrAbsolute);
            ImageUser4 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png", UriKind.RelativeOrAbsolute);
            ImageUser5 = new BitmapImage(uri);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BehaviorVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        User1BackgroundColor = "Transparent";
        User2BackgroundColor = "Transparent";
        User3BackgroundColor = "Transparent";
        User4BackgroundColor = "Transparent";
        User5BackgroundColor = "Transparent";
        ReferenceValues.JsonTasksMaster.User1Blink = false;
        ReferenceValues.JsonTasksMaster.User2Blink = false;
        ReferenceValues.JsonTasksMaster.User3Blink = false;
        ReferenceValues.JsonTasksMaster.User4Blink = false;
        ReferenceValues.JsonTasksMaster.User5Blink = false;

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;

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
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BehaviorVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        RefreshBehavior();
        RefreshTasks(1);
        RefreshTasks(2);
        RefreshTasks(3);
        RefreshTasks(4);
        RefreshTasks(5);
        RefreshCountdown();
        RefreshBlinking();
        TrashDayLogic();
        SaveJsons();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void RefreshTasks(int UserID) {
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        culture.NumberFormat.CurrencyNegativePattern = 1;
        System.Globalization.Calendar cal = DateTimeFormatInfo.CurrentInfo.Calendar;
        DateTime d1 = DateTime.Today;

        try {
            d1 = ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.AddDays(-1 * (int)cal.GetDayOfWeek(ReferenceValues.JsonTasksMaster.UpdatedDateTime));
        } catch (Exception) {
            // ignore?
        }

        DateTime d2 = DateTime.Today.Date.AddDays(-1 * (int)cal.GetDayOfWeek(DateTime.Today));

        int totalDay = 0, totalWeek = 0, totalMonth = 0, totalQuarter = 0;
        int completedDay = 0, completedWeek = 0, completedMonth = 0, completedQuarter = 0;
        int releaseAmountDaily = 0, releaseAmountWeekly = 0, releaseAmountMonthly = 0, releaseAmountQuarterly = 0;
        double math;

        switch (UserID) {
        case 1:
            User1CashReleased = "";
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

            try {
                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay) * 100;
                User1TasksCompletedDayProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User1TasksCompletedDayProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek) * 100;
                User1TasksCompletedWeekProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User1TasksCompletedWeekProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth) * 100;
                User1TasksCompletedMonthProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User1TasksCompletedMonthProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter) * 100;
                User1TasksCompletedQuarterProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User1TasksCompletedQuarterProgressValue = 0;
            }

            if (totalDay != 0) {
                User1TasksCompletedDayProgressText = User1TasksCompletedDayProgressValue + "%";

                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
                releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser1);

                /* Release daily funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    if (releaseAmountDaily != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User1Name + "'s Daily Funds: $" + releaseAmountDaily
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User1Name + "'s Daily Funds",
                                Cost = releaseAmountDaily.ToString(),
                                Category = "User1 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user1.png",
                                UserId = 1
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset daily */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountDaily = 0;
                    User1TasksCompletedDayProgressText = "0%";
                    User1TasksCompletedDayProgressValue = 0;
                }
            } else {
                User1TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User1TasksCompletedWeekProgressText = User1TasksCompletedWeekProgressValue + "%";

                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
                releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser1);

                /* Release weekly funds */
                if (d1 != d2) {
                    if (releaseAmountWeekly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User1Name + "'s Weekly Funds: $" + releaseAmountWeekly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                            AddSub = "SUB",
                            Date = DateTime.Now.ToShortDateString(),
                            Item = ReferenceValues.JsonSettingsMaster.User1Name + "'s Weekly Funds",
                            Cost = releaseAmountWeekly.ToString(),
                            Category = "User1 Fund",
                            Details = "(Automatic)",
                            Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user1.png",
                            UserId = 1
                        });
                    }

                    /* Reset weekly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountWeekly = 0;
                    User1TasksCompletedWeekProgressText = "0%";
                    User1TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User1TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User1TasksCompletedMonthProgressText = User1TasksCompletedMonthProgressValue + "%";

                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
                releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser1);

                /* Release monthly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    if (releaseAmountMonthly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User1Name + "'s Monthly Funds: $" + releaseAmountMonthly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User1Name + "'s Monthly Funds",
                                Cost = releaseAmountMonthly.ToString(),
                                Category = "User1 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user1.png",
                                UserId = 1
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset monthly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser1) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountMonthly = 0;
                    User1TasksCompletedMonthProgressText = "0%";
                    User1TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User1TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User1TasksCompletedQuarterProgressText = User1TasksCompletedQuarterProgressValue + "%";

                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
                releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser1);

                /* Releasing quarterly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        if (releaseAmountQuarterly != 0) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "BehaviorVM",
                                Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User1Name + "'s Quarterly Funds: $" + releaseAmountQuarterly
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            try {
                                ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                    AddSub = "SUB",
                                    Date = DateTime.Now.ToShortDateString(),
                                    Item = ReferenceValues.JsonSettingsMaster.User1Name + "'s Quarterly Funds",
                                    Cost = releaseAmountQuarterly.ToString(),
                                    Category = "User1 Fund",
                                    Details = "(Automatic)",
                                    Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user1.png",
                                    UserId = 1
                                });
                            } catch (Exception) {
                                // ignore
                            }
                        }

                        /* Reset quarter */
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser1) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

                        releaseAmountQuarterly = 0;
                        User1TasksCompletedQuarterProgressText = "0%";
                        User1TasksCompletedQuarterProgressValue = 0;
                    }
                }
            } else {
                User1TasksCompletedQuarterProgressText = "None";
            }

            User1TasksCompletedDayProgressColor = User1TasksCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
            User1TasksCompletedWeekProgressColor = User1TasksCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
            User1TasksCompletedMonthProgressColor = User1TasksCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
            User1TasksCompletedQuarterProgressColor = User1TasksCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";

            int totalRelease = 0;
            if (releaseAmountDaily > 0) {
                totalRelease += releaseAmountDaily;
            }

            if (releaseAmountWeekly > 0 && DateTime.Today.DayOfWeek == DayOfWeek.Saturday) {
                totalRelease += releaseAmountWeekly;
            }

            if (releaseAmountMonthly > 0 && DateTime.Today.Month != DateTime.Today.AddDays(1).Month) {
                totalRelease += releaseAmountMonthly;
            }

            if (releaseAmountQuarterly > 0) {
                switch (DateTime.Today.AddDays(1).Month) {
                case 1:
                case 4:
                case 7:
                case 10:
                    totalRelease += releaseAmountQuarterly;
                    break;
                }
            }

            if (totalRelease > 0) {
                User1CashReleased = "+ $" + totalRelease;
            } else {
                User1CashReleased = "";
            }

            int funds = 0;
            try {
                foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMaster.financeList) {
                    if (financeBlock.Category == "User1 Fund") {
                        try {
                            if (financeBlock.AddSub == "SUB") {
                                funds += int.Parse(financeBlock.Cost);
                            } else {
                                funds -= int.Parse(financeBlock.Cost);
                            }
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "BehaviorVM",
                                Description = e.ToString()
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    }
                }
            } catch (Exception) {
                //blank
            }

            if (ReferenceValues.JsonSettingsMaster.User1Checked) {
                User1CashAvailable = string.Format(culture, "{0:C}", funds);
                User1CashAvailableColor = User1CashAvailable.StartsWith("-") ? "Red" : "CornflowerBlue";
            } else {
                User1CashAvailable = "";
            }

            ProgressBarUser1 = ReferenceValues.JsonBehaviorMaster.User1Progress;

            User1DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1.Count > 0 ? "VISIBLE" : "HIDDEN";
            User1WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1.Count > 0 ? "VISIBLE" : "HIDDEN";
            User1MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser1.Count > 0 ? "VISIBLE" : "HIDDEN";
            User1QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser1.Count > 0 ? "VISIBLE" : "HIDDEN";

            User1BehaviorVisibility = ReferenceValues.JsonSettingsMaster.User1BehaviorChecked ? "VISIBLE" : "HIDDEN";

            break;
        case 2:
            User2CashReleased = "";
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

            try {
                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay) * 100;
                User2TasksCompletedDayProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User2TasksCompletedDayProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek) * 100;
                User2TasksCompletedWeekProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User2TasksCompletedWeekProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth) * 100;
                User2TasksCompletedMonthProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User2TasksCompletedMonthProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter) * 100;
                User2TasksCompletedQuarterProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User2TasksCompletedQuarterProgressValue = 0;
            }

            if (totalDay != 0) {
                User2TasksCompletedDayProgressText = User2TasksCompletedDayProgressValue + "%";

                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
                releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser2);

                /* Release daily funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    if (releaseAmountDaily != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User2Name + "'s Daily Funds: $" + releaseAmountDaily
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User2Name + "'s Daily Funds",
                                Cost = releaseAmountDaily.ToString(),
                                Category = "User2 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user2.png",
                                UserId = 2
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset daily */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountDaily = 0;
                    User2TasksCompletedDayProgressText = "0%";
                    User2TasksCompletedDayProgressValue = 0;
                }
            } else {
                User2TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User2TasksCompletedWeekProgressText = User2TasksCompletedWeekProgressValue + "%";

                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
                releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser2);

                /* Release weekly funds */
                if (d1 != d2) {
                    if (releaseAmountWeekly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User2Name + "'s Weekly Funds: $" + releaseAmountWeekly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User2Name + "'s Weekly Funds",
                                Cost = releaseAmountWeekly.ToString(),
                                Category = "User2 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user2.png",
                                UserId = 2
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset weekly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountWeekly = 0;
                    User2TasksCompletedWeekProgressText = "0%";
                    User2TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User2TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User2TasksCompletedMonthProgressText = User2TasksCompletedMonthProgressValue + "%";

                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
                releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser2);

                /* Release monthly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    if (releaseAmountMonthly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User2Name + "'s Monthly Funds: $" + releaseAmountMonthly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User2Name + "'s Monthly Funds",
                                Cost = releaseAmountMonthly.ToString(),
                                Category = "User2 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user2.png",
                                UserId = 2
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset monthly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser2) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountMonthly = 0;
                    User2TasksCompletedMonthProgressText = "0%";
                    User2TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User2TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User2TasksCompletedQuarterProgressText = User2TasksCompletedQuarterProgressValue + "%";

                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
                releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser2);

                /* Releasing quarterly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        if (releaseAmountQuarterly != 0) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "BehaviorVM",
                                Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User2Name + "'s Quarterly Funds: $" + releaseAmountQuarterly
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                            try {
                                ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                    AddSub = "SUB",
                                    Date = DateTime.Now.ToShortDateString(),
                                    Item = ReferenceValues.JsonSettingsMaster.User2Name + "'s Quarterly Funds",
                                    Cost = releaseAmountQuarterly.ToString(),
                                    Category = "User2 Fund",
                                    Details = "(Automatic)",
                                    Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user2.png",
                                    UserId = 2
                                });
                            } catch (Exception) {
                                // ignore
                            }
                        }

                        /* Reset quarterly */
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser2) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

                        releaseAmountQuarterly = 0;
                        User2TasksCompletedQuarterProgressText = "0%";
                        User2TasksCompletedQuarterProgressValue = 0;
                    }
                }
            } else {
                User2TasksCompletedQuarterProgressText = "None";
            }

            User2TasksCompletedDayProgressColor = User2TasksCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
            User2TasksCompletedWeekProgressColor = User2TasksCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
            User2TasksCompletedMonthProgressColor = User2TasksCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
            User2TasksCompletedQuarterProgressColor = User2TasksCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";

            totalRelease = 0;
            if (releaseAmountDaily > 0) {
                totalRelease += releaseAmountDaily;
            }

            if (releaseAmountWeekly > 0 && DateTime.Today.DayOfWeek == DayOfWeek.Saturday) {
                totalRelease += releaseAmountWeekly;
            }

            if (releaseAmountMonthly > 0 && DateTime.Today.Month != DateTime.Today.AddDays(1).Month) {
                totalRelease += releaseAmountMonthly;
            }

            if (releaseAmountQuarterly > 0) {
                switch (DateTime.Today.AddDays(1).Month) {
                case 1:
                case 4:
                case 7:
                case 10:
                    totalRelease += releaseAmountQuarterly;
                    break;
                }
            }

            if (totalRelease > 0) {
                User2CashReleased = "+ $" + totalRelease;
            } else {
                User2CashReleased = "";
            }

            funds = 0;
            try {
                foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMaster.financeList) {
                    if (financeBlock.Category == "User2 Fund") {
                        try {
                            if (financeBlock.AddSub == "SUB") {
                                funds += int.Parse(financeBlock.Cost);
                            } else {
                                funds -= int.Parse(financeBlock.Cost);
                            }
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "BehaviorVM",
                                Description = e.ToString()
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    }
                }
            } catch (Exception) {
                //blank
            }

            if (ReferenceValues.JsonSettingsMaster.User2Checked) {
                User2CashAvailable = string.Format(culture, "{0:C}", funds);
                User2CashAvailableColor = User2CashAvailable.StartsWith("-") ? "Red" : "CornflowerBlue";
            } else {
                User2CashAvailable = "";
            }

            ProgressBarUser2 = ReferenceValues.JsonBehaviorMaster.User2Progress;

            User2DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2.Count > 0 ? "VISIBLE" : "HIDDEN";
            User2WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2.Count > 0 ? "VISIBLE" : "HIDDEN";
            User2MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser2.Count > 0 ? "VISIBLE" : "HIDDEN";
            User2QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser2.Count > 0 ? "VISIBLE" : "HIDDEN";

            User2BehaviorVisibility = ReferenceValues.JsonSettingsMaster.User2BehaviorChecked ? "VISIBLE" : "HIDDEN";

            break;
        case 3:
            User3CashReleased = "";
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

            try {
                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay) * 100;
                User3TasksCompletedDayProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User3TasksCompletedDayProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek) * 100;
                User3TasksCompletedWeekProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User3TasksCompletedWeekProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth) * 100;
                User3TasksCompletedMonthProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User3TasksCompletedMonthProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter) * 100;
                User3TasksCompletedQuarterProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User3TasksCompletedQuarterProgressValue = 0;
            }

            if (totalDay != 0) {
                User3TasksCompletedDayProgressText = User3TasksCompletedDayProgressValue + "%";

                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
                releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser3);

                /* Release daily funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    if (releaseAmountDaily != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User3Name + "'s Daily Funds: $" + releaseAmountDaily
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User3Name + "'s Daily Funds",
                                Cost = releaseAmountDaily.ToString(),
                                Category = "User3 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user3.png",
                                UserId = 3
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset daily */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountDaily = 0;
                    User3TasksCompletedDayProgressText = "0%";
                    User3TasksCompletedDayProgressValue = 0;
                }
            } else {
                User3TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User3TasksCompletedWeekProgressText = User3TasksCompletedWeekProgressValue + "%";

                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
                releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser3);

                /* Release weekly funds */
                if (d1 != d2) {
                    if (releaseAmountWeekly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User3Name + "'s Weekly Funds: $" + releaseAmountWeekly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User3Name + "'s Weekly Funds",
                                Cost = releaseAmountWeekly.ToString(),
                                Category = "User3 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user3.png",
                                UserId = 3
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset weekly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountWeekly = 0;
                    User3TasksCompletedWeekProgressText = "0%";
                    User3TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User3TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User3TasksCompletedMonthProgressText = User3TasksCompletedMonthProgressValue + "%";

                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
                releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser3);

                /* Release monthly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    if (releaseAmountMonthly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User3Name + "'s Monthly Funds: $" + releaseAmountMonthly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User3Name + "'s Monthly Funds",
                                Cost = releaseAmountMonthly.ToString(),
                                Category = "User3 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user3.png",
                                UserId = 3
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset monthly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser3) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountMonthly = 0;
                    User3TasksCompletedMonthProgressText = "0%";
                    User3TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User3TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User3TasksCompletedQuarterProgressText = User3TasksCompletedQuarterProgressValue + "%";

                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
                releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser3);

                /* Releasing quarterly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        if (releaseAmountQuarterly != 0) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "BehaviorVM",
                                Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User3Name + "'s Quarterly Funds: $" + releaseAmountQuarterly
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            try {
                                ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                    AddSub = "SUB",
                                    Date = DateTime.Now.ToShortDateString(),
                                    Item = ReferenceValues.JsonSettingsMaster.User3Name + "'s Quarterly Funds",
                                    Cost = releaseAmountQuarterly.ToString(),
                                    Category = "User3 Fund",
                                    Details = "(Automatic)",
                                    Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user3.png",
                                    UserId = 3
                                });
                            } catch (Exception) {
                                // ignore
                            }
                        }

                        /* Reset quarterly */
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser3) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

                        releaseAmountQuarterly = 0;
                        User3TasksCompletedQuarterProgressText = "0%";
                        User3TasksCompletedQuarterProgressValue = 0;
                    }
                }
            } else {
                User3TasksCompletedQuarterProgressText = "None";
            }

            totalRelease = 0;
            if (releaseAmountDaily > 0) {
                totalRelease += releaseAmountDaily;
            }

            if (releaseAmountWeekly > 0 && DateTime.Today.DayOfWeek == DayOfWeek.Saturday) {
                totalRelease += releaseAmountWeekly;
            }

            if (releaseAmountMonthly > 0 && DateTime.Today.Month != DateTime.Today.AddDays(1).Month) {
                totalRelease += releaseAmountMonthly;
            }

            if (releaseAmountQuarterly > 0) {
                switch (DateTime.Today.AddDays(1).Month) {
                case 1:
                case 4:
                case 7:
                case 10:
                    totalRelease += releaseAmountQuarterly;
                    break;
                }
            }

            if (totalRelease > 0) {
                User3CashReleased = "+ $" + totalRelease;
            } else {
                User3CashReleased = "";
            }

            User3TasksCompletedDayProgressColor = User3TasksCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
            User3TasksCompletedWeekProgressColor = User3TasksCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
            User3TasksCompletedMonthProgressColor = User3TasksCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
            User3TasksCompletedQuarterProgressColor = User3TasksCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";

            funds = 0;
            try {
                foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMaster.financeList) {
                    if (financeBlock.Category == "User3 Fund") {
                        try {
                            if (financeBlock.AddSub == "SUB") {
                                funds += int.Parse(financeBlock.Cost);
                            } else {
                                funds -= int.Parse(financeBlock.Cost);
                            }
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "BehaviorVM",
                                Description = e.ToString()
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    }
                }
            } catch (Exception) {
                //blank
            }

            if (ReferenceValues.JsonSettingsMaster.User3Checked) {
                User3CashAvailable = string.Format(culture, "{0:C}", funds);
                User3CashAvailableColor = User3CashAvailable.StartsWith("-") ? "Red" : "CornflowerBlue";
            } else {
                User3CashAvailable = "";
            }

            ProgressBarUser3 = ReferenceValues.JsonBehaviorMaster.User3Progress;

            User3DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3.Count > 0 ? "VISIBLE" : "HIDDEN";
            User3WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3.Count > 0 ? "VISIBLE" : "HIDDEN";
            User3MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser3.Count > 0 ? "VISIBLE" : "HIDDEN";
            User3QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser3.Count > 0 ? "VISIBLE" : "HIDDEN";

            User3BehaviorVisibility = ReferenceValues.JsonSettingsMaster.User3BehaviorChecked ? "VISIBLE" : "HIDDEN";

            break;
        case 4:
            User4CashReleased = "";
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

            try {
                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay) * 100;
                User4TasksCompletedDayProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User4TasksCompletedDayProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek) * 100;
                User4TasksCompletedWeekProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User4TasksCompletedWeekProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth) * 100;
                User4TasksCompletedMonthProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User4TasksCompletedMonthProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter) * 100;
                User4TasksCompletedQuarterProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User4TasksCompletedQuarterProgressValue = 0;
            }

            if (totalDay != 0) {
                User4TasksCompletedDayProgressText = User4TasksCompletedDayProgressValue + "%";

                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
                releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser4);

                /* Release daily funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    if (releaseAmountDaily != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User4Name + "'s Daily Funds: $" + releaseAmountDaily
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User4Name + "'s Daily Funds",
                                Cost = releaseAmountDaily.ToString(),
                                Category = "User4 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user4.png",
                                UserId = 4
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset daily */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountDaily = 0;
                    User4TasksCompletedDayProgressText = "0%";
                    User4TasksCompletedDayProgressValue = 0;
                }
            } else {
                User4TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User4TasksCompletedWeekProgressText = User4TasksCompletedWeekProgressValue + "%";

                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
                releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser4);

                /* Release weekly funds */
                if (d1 != d2) {
                    if (releaseAmountWeekly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User4Name + "'s Weekly Funds: $" + releaseAmountWeekly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User4Name + "'s Weekly Funds",
                                Cost = releaseAmountWeekly.ToString(),
                                Category = "User4 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user4.png",
                                UserId = 4
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset weekly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountWeekly = 0;
                    User4TasksCompletedWeekProgressText = "0%";
                    User4TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User4TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User4TasksCompletedMonthProgressText = User4TasksCompletedMonthProgressValue + "%";

                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
                releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser4);

                /* Release monthly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    if (releaseAmountMonthly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User4Name + "'s Monthly Funds: $" + releaseAmountMonthly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User4Name + "'s Monthly Funds",
                                Cost = releaseAmountMonthly.ToString(),
                                Category = "User4 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user4.png",
                                UserId = 4
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset monthly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser4) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountMonthly = 0;
                    User4TasksCompletedMonthProgressText = "0%";
                    User4TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User4TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User4TasksCompletedQuarterProgressText = User4TasksCompletedQuarterProgressValue + "%";

                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
                releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser4);

                /* Releasing quarterly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        if (releaseAmountQuarterly != 0) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "BehaviorVM",
                                Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User4Name + "'s Quarterly Funds: $" + releaseAmountQuarterly
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            try {
                                ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                    AddSub = "SUB",
                                    Date = DateTime.Now.ToShortDateString(),
                                    Item = ReferenceValues.JsonSettingsMaster.User4Name + "'s Quarterly Funds",
                                    Cost = releaseAmountQuarterly.ToString(),
                                    Category = "User4 Fund",
                                    Details = "(Automatic)",
                                    Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user4.png",
                                    UserId = 4
                                });
                            } catch (Exception) {
                                // ignore
                            }
                        }

                        /* Reset quarterly */
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser4) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

                        releaseAmountQuarterly = 0;
                        User4TasksCompletedQuarterProgressText = "0%";
                        User4TasksCompletedQuarterProgressValue = 0;
                    }
                }
            } else {
                User4TasksCompletedQuarterProgressText = "None";
            }

            User4TasksCompletedDayProgressColor = User4TasksCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
            User4TasksCompletedWeekProgressColor = User4TasksCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
            User4TasksCompletedMonthProgressColor = User4TasksCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
            User4TasksCompletedQuarterProgressColor = User4TasksCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";

            totalRelease = 0;
            if (releaseAmountDaily > 0) {
                totalRelease += releaseAmountDaily;
            }

            if (releaseAmountWeekly > 0 && DateTime.Today.DayOfWeek == DayOfWeek.Saturday) {
                totalRelease += releaseAmountWeekly;
            }

            if (releaseAmountMonthly > 0 && DateTime.Today.Month != DateTime.Today.AddDays(1).Month) {
                totalRelease += releaseAmountMonthly;
            }

            if (releaseAmountQuarterly > 0) {
                switch (DateTime.Today.AddDays(1).Month) {
                case 1:
                case 4:
                case 7:
                case 10:
                    totalRelease += releaseAmountQuarterly;
                    break;
                }
            }

            if (totalRelease > 0) {
                User4CashReleased = "+ $" + totalRelease;
            } else {
                User4CashReleased = "";
            }

            funds = 0;
            try {
                foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMaster.financeList) {
                    if (financeBlock.Category == "User4 Fund") {
                        try {
                            if (financeBlock.AddSub == "SUB") {
                                funds += int.Parse(financeBlock.Cost);
                            } else {
                                funds -= int.Parse(financeBlock.Cost);
                            }
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "BehaviorVM",
                                Description = e.ToString()
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    }
                }
            } catch (Exception) {
                //blank
            }

            if (ReferenceValues.JsonSettingsMaster.User4Checked) {
                User4CashAvailable = string.Format(culture, "{0:C}", funds);
                User4CashAvailableColor = User4CashAvailable.StartsWith("-") ? "Red" : "CornflowerBlue";
            } else {
                User4CashAvailable = "";
            }

            ProgressBarUser4 = ReferenceValues.JsonBehaviorMaster.User4Progress;

            User4DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4.Count > 0 ? "VISIBLE" : "HIDDEN";
            User4WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4.Count > 0 ? "VISIBLE" : "HIDDEN";
            User4MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser4.Count > 0 ? "VISIBLE" : "HIDDEN";
            User4QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser4.Count > 0 ? "VISIBLE" : "HIDDEN";

            User4BehaviorVisibility = ReferenceValues.JsonSettingsMaster.User4BehaviorChecked ? "VISIBLE" : "HIDDEN";

            break;
        case 5:
            User5CashReleased = "";
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

            try {
                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay) * 100;
                User5TasksCompletedDayProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User5TasksCompletedDayProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek) * 100;
                User5TasksCompletedWeekProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User5TasksCompletedWeekProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth) * 100;
                User5TasksCompletedMonthProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User5TasksCompletedMonthProgressValue = 0;
            }

            try {
                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter) * 100;
                User5TasksCompletedQuarterProgressValue = (int)math;
            } catch (DivideByZeroException) {
                User5TasksCompletedQuarterProgressValue = 0;
            }

            if (totalDay != 0) {
                User5TasksCompletedDayProgressText = User5TasksCompletedDayProgressValue + "%";

                math = Convert.ToDouble(completedDay) / Convert.ToDouble(totalDay);
                releaseAmountDaily = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksDaily.FundsDailyUser5);

                /* Release daily funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    if (releaseAmountDaily != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User5Name + "'s Daily Funds: $" + releaseAmountDaily
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User5Name + "'s Daily Funds",
                                Cost = releaseAmountDaily.ToString(),
                                Category = "User5 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png",
                                UserId = 5
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset daily */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountDaily = 0;
                    User5TasksCompletedDayProgressText = "0%";
                    User5TasksCompletedDayProgressValue = 0;
                }
            } else {
                User5TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User5TasksCompletedWeekProgressText = User5TasksCompletedWeekProgressValue + "%";

                math = Convert.ToDouble(completedWeek) / Convert.ToDouble(totalWeek);
                releaseAmountWeekly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksWeekly.FundsWeeklyUser5);

                /* Release weekly funds */
                if (d1 != d2) {
                    if (releaseAmountWeekly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User5Name + "'s Weekly Funds: $" + releaseAmountWeekly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User5Name + "'s Weekly Funds",
                                Cost = releaseAmountWeekly.ToString(),
                                Category = "User5 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png",
                                UserId = 5
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset weekly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountWeekly = 0;
                    User5TasksCompletedWeekProgressText = "0%";
                    User5TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User5TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User5TasksCompletedMonthProgressText = User5TasksCompletedMonthProgressValue + "%";

                math = Convert.ToDouble(completedMonth) / Convert.ToDouble(totalMonth);
                releaseAmountMonthly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksMonthly.FundsMonthlyUser5);

                /* Release monthly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    if (releaseAmountMonthly != 0) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "BehaviorVM",
                            Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User5Name + "'s Monthly Funds: $" + releaseAmountMonthly
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        try {
                            ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                AddSub = "SUB",
                                Date = DateTime.Now.ToShortDateString(),
                                Item = ReferenceValues.JsonSettingsMaster.User5Name + "'s Monthly Funds",
                                Cost = releaseAmountMonthly.ToString(),
                                Category = "User5 Fund",
                                Details = "(Automatic)",
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png",
                                UserId = 5
                            });
                        } catch (Exception) {
                            // ignore
                        }
                    }

                    /* Reset monthly */
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser5) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    releaseAmountMonthly = 0;
                    User5TasksCompletedMonthProgressText = "0%";
                    User5TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User5TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User5TasksCompletedQuarterProgressText = User5TasksCompletedQuarterProgressValue + "%";

                math = Convert.ToDouble(completedQuarter) / Convert.ToDouble(totalQuarter);
                releaseAmountQuarterly = (int)(math * ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.FundsQuarterlyUser5);

                /* Releasing quarterly funds */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        if (releaseAmountQuarterly != 0) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "BehaviorVM",
                                Description = "Releasing " + ReferenceValues.JsonSettingsMaster.User5Name + "'s Quarterly Funds: $" + releaseAmountQuarterly
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            try {
                                ReferenceValues.JsonFinanceMaster.financeList.Add(new FinanceBlock {
                                    AddSub = "SUB",
                                    Date = DateTime.Now.ToShortDateString(),
                                    Item = ReferenceValues.JsonSettingsMaster.User5Name + "'s Quarterly Funds",
                                    Cost = releaseAmountQuarterly.ToString(),
                                    Category = "User5 Fund",
                                    Details = "(Automatic)",
                                    Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png",
                                    UserId = 5
                                });
                            } catch (Exception) {
                                // ignore
                            }
                        }

                        /* Reset quarterly */
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser5) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

                        releaseAmountQuarterly = 0;
                        User5TasksCompletedQuarterProgressText = "0%";
                        User5TasksCompletedQuarterProgressValue = 0;
                    }
                }
            } else {
                User5TasksCompletedQuarterProgressText = "None";
            }

            User5TasksCompletedDayProgressColor = User5TasksCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
            User5TasksCompletedWeekProgressColor = User5TasksCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
            User5TasksCompletedMonthProgressColor = User5TasksCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
            User5TasksCompletedQuarterProgressColor = User5TasksCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";

            totalRelease = 0;
            if (releaseAmountDaily > 0) {
                totalRelease += releaseAmountDaily;
            }

            if (releaseAmountWeekly > 0 && DateTime.Today.DayOfWeek == DayOfWeek.Saturday) {
                totalRelease += releaseAmountWeekly;
            }

            if (releaseAmountMonthly > 0 && DateTime.Today.Month != DateTime.Today.AddDays(1).Month) {
                totalRelease += releaseAmountMonthly;
            }

            if (releaseAmountQuarterly > 0) {
                switch (DateTime.Today.AddDays(1).Month) {
                case 1:
                case 4:
                case 7:
                case 10:
                    totalRelease += releaseAmountQuarterly;
                    break;
                }
            }

            if (totalRelease > 0) {
                User5CashReleased = "+ $" + totalRelease;
            } else {
                User5CashReleased = "";
            }

            funds = 0;
            try {
                foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMaster.financeList) {
                    if (financeBlock.Category == "User5 Fund") {
                        try {
                            if (financeBlock.AddSub == "SUB") {
                                funds += int.Parse(financeBlock.Cost);
                            } else {
                                funds -= int.Parse(financeBlock.Cost);
                            }
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "BehaviorVM",
                                Description = e.ToString()
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    }
                }
            } catch (Exception) {
                //blank
            }

            if (ReferenceValues.JsonSettingsMaster.User5Checked) {
                User5CashAvailable = string.Format(culture, "{0:C}", funds);
                User5CashAvailableColor = User5CashAvailable.StartsWith("-") ? "Red" : "CornflowerBlue";
            } else {
                User5CashAvailable = "";
            }

            ProgressBarUser5 = ReferenceValues.JsonBehaviorMaster.User5Progress;

            User5DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5.Count > 0 ? "VISIBLE" : "HIDDEN";
            User5WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5.Count > 0 ? "VISIBLE" : "HIDDEN";
            User5MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser5.Count > 0 ? "VISIBLE" : "HIDDEN";
            User5QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser5.Count > 0 ? "VISIBLE" : "HIDDEN";

            User5BehaviorVisibility = ReferenceValues.JsonSettingsMaster.User5BehaviorChecked ? "VISIBLE" : "HIDDEN";

            break;
        }
    }

    private void SaveJsons() {
        ReferenceValues.JsonTasksMaster.UpdatedDateTime = DateTime.Today;

        try {
            FileHelpers.SaveFileText("behavior", JsonSerializer.Serialize(ReferenceValues.JsonBehaviorMaster), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BehaviorVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        try {
            FileHelpers.SaveFileText("tasks", JsonSerializer.Serialize(ReferenceValues.JsonTasksMaster), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BehaviorVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        try {
            FileHelpers.SaveFileText("finances", JsonSerializer.Serialize(ReferenceValues.JsonFinanceMaster), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BehaviorVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }
    }

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

        User1Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        User1Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        User1Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        User2Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        User2Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        User2Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        User3Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        User3Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        User3Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        User4Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        User4Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        User4Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        User5Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        User5Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        User5Strike3 = "../../../Resources/Images/behavior/strike_black.png";

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
        switch (e.PropertyName) {
        case "DateChanged":
            ReferenceValues.JsonBehaviorMaster.User1Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User2Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User3Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User4Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User5Strikes = 0;
            RefreshBehavior();
            RefreshCountdown();
            RefreshTasks(1);
            RefreshTasks(2);
            RefreshTasks(3);
            RefreshTasks(4);
            RefreshTasks(5);
            TrashDayLogic();
            SaveJsons();
            break;
        case "HourChanged":
            RefreshCountdown();
            RefreshBlinking();
            TrashDayLogic();
            break;
        case "RefreshFinances":
            RefreshTasks(1);
            RefreshTasks(2);
            RefreshTasks(3);
            RefreshTasks(4);
            RefreshTasks(5);
            break;
        case "Refresh":
            if (ReferenceValues.JsonTasksMaster.User1Blink) {
                User1BackgroundColor = User1BackgroundColor == "Transparent" ? "Yellow" : "Transparent";
            }

            if (ReferenceValues.JsonTasksMaster.User2Blink) {
                User2BackgroundColor = User2BackgroundColor == "Transparent" ? "Yellow" : "Transparent";
            }

            if (ReferenceValues.JsonTasksMaster.User3Blink) {
                User3BackgroundColor = User3BackgroundColor == "Transparent" ? "Yellow" : "Transparent";
            }

            if (ReferenceValues.JsonTasksMaster.User4Blink) {
                User4BackgroundColor = User4BackgroundColor == "Transparent" ? "Yellow" : "Transparent";
            }

            if (ReferenceValues.JsonTasksMaster.User5Blink) {
                User5BackgroundColor = User5BackgroundColor == "Transparent" ? "Yellow" : "Transparent";
            }

            if (blinkDay) {
                RemainingDayColor = RemainingDayColor != "Yellow" ? "Yellow" : "Red";
            }

            if (blinkWeek) {
                RemainingWeekColor = RemainingWeekColor != "Yellow" ? "Yellow" : "Red";
            }

            if (blinkMonth) {
                RemainingMonthColor = RemainingMonthColor != "Yellow" ? "Yellow" : "Red";
            }

            if (blinkQuarter) {
                RemainingQuarterColor = RemainingQuarterColor != "Yellow" ? "Yellow" : "Red";
            }

            if (blinkYear) {
                RemainingYearColor = RemainingYearColor != "Yellow" ? "Yellow" : "Red";
            }

            break;
        }
    }

    private void RefreshBlinking() {
        ReferenceValues.JsonTasksMaster.User1Blink = false;
        ReferenceValues.JsonTasksMaster.User2Blink = false;
        ReferenceValues.JsonTasksMaster.User3Blink = false;
        ReferenceValues.JsonTasksMaster.User4Blink = false;
        ReferenceValues.JsonTasksMaster.User5Blink = false;
        User1BackgroundColor = "Transparent";
        User2BackgroundColor = "Transparent";
        User3BackgroundColor = "Transparent";
        User4BackgroundColor = "Transparent";
        User5BackgroundColor = "Transparent";

        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1) {
            if (!task.IsCompleted && DateTime.Now.Hour >= task.RequiredTime) {
                ReferenceValues.JsonTasksMaster.User1Blink = true;
            }
        }

        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2) {
            if (!task.IsCompleted && DateTime.Now.Hour >= task.RequiredTime) {
                ReferenceValues.JsonTasksMaster.User2Blink = true;
            }
        }

        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3) {
            if (!task.IsCompleted && DateTime.Now.Hour >= task.RequiredTime) {
                ReferenceValues.JsonTasksMaster.User3Blink = true;
            }
        }

        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4) {
            if (!task.IsCompleted && DateTime.Now.Hour >= task.RequiredTime) {
                ReferenceValues.JsonTasksMaster.User4Blink = true;
            }
        }

        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5) {
            if (!task.IsCompleted && DateTime.Now.Hour >= task.RequiredTime) {
                ReferenceValues.JsonTasksMaster.User5Blink = true;
            }
        }
    }

    private void ButtonLogic(object param) {
        if (!ReferenceValues.LockUI) {
            switch (param) {
            case "user1":
                ReferenceValues.ActiveBehaviorUser = 1;
                EditBehavior editBehavior = new();
                editBehavior.ShowDialog();
                editBehavior.Close();
                RefreshTasks(1);
                RefreshBlinking();
                break;
            case "user2":
                ReferenceValues.ActiveBehaviorUser = 2;
                EditBehavior editBehavior2 = new();
                editBehavior2.ShowDialog();
                editBehavior2.Close();
                RefreshTasks(2);
                RefreshBlinking();
                break;
            case "user3":
                ReferenceValues.ActiveBehaviorUser = 3;
                EditBehavior editBehavior3 = new();
                editBehavior3.ShowDialog();
                editBehavior3.Close();
                RefreshTasks(3);
                RefreshBlinking();
                break;
            case "user4":
                ReferenceValues.ActiveBehaviorUser = 4;
                EditBehavior editBehavior4 = new();
                editBehavior4.ShowDialog();
                editBehavior4.Close();
                RefreshTasks(4);
                RefreshBlinking();
                break;
            case "user5":
                ReferenceValues.ActiveBehaviorUser = 5;
                EditBehavior editBehavior5 = new();
                editBehavior5.ShowDialog();
                editBehavior5.Close();
                RefreshTasks(5);
                RefreshBlinking();
                break;
            }

            RefreshBehavior();
        } else {
            ReferenceValues.SoundToPlay = "locked";
            SoundDispatcher.PlaySound();
        }
    }

    private void RefreshCountdown() {
        blinkDay = false;
        blinkWeek = false;
        blinkMonth = false;
        blinkQuarter = false;
        blinkYear = false;

        DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        System.Globalization.Calendar calendar = dateTimeFormatInfo.Calendar;

        ReferenceValues.TaskWeekStartDate = DateTime.Now;
        while (ReferenceValues.TaskWeekStartDate.DayOfWeek != DayOfWeek.Sunday) {
            ReferenceValues.TaskWeekStartDate = ReferenceValues.TaskWeekStartDate.AddDays(-1);
        }

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

        /* Day */
        DateTime dateNext = DateTime.Now;
        RemainingDay = (TimeSpan.FromHours(24) - dateNext.TimeOfDay).Hours + " Hours";

        if (RemainingDay == "1 Hours") {
            blinkDay = true;
            RemainingDay = "1 Hour";
        } else if (RemainingDay == "0 Hours") {
            blinkDay = true;
            RemainingDay = "< 1 Hour";
        }

        RemainingDayColor = "White";

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
            blinkWeek = true;

            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            RemainingWeek = timeSpan.Hours + " Hours";
        }

        if (RemainingWeek == "1 Hours") {
            RemainingWeek = "1 Hour";
        } else if (RemainingWeek == "0 Hours") {
            RemainingWeek = "< 1 Hour";
        }

        RemainingWeekColor = "White";

        /* Month */
        dateNext = DateTime.Now;
        while (DateTime.Now.Month == dateNext.Month) {
            dateNext = dateNext.AddDays(1);
        }

        RemainingMonth = (dateNext.Date - DateTime.Now.Date).Days + " Days";

        if (RemainingMonth == "1 Days") {
            blinkMonth = true;

            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            RemainingMonth = timeSpan.Hours + " Hours";
        }

        if (RemainingMonth == "1 Hours") {
            RemainingMonth = "1 Hour";
        } else if (RemainingMonth == "0 Hours") {
            RemainingMonth = "< 1 Hour";
        }

        RemainingMonthColor = "White";

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
            blinkQuarter = true;

            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            RemainingQuarter = timeSpan.Hours + " Hours";
        }

        if (RemainingQuarter == "1 Hours") {
            RemainingQuarter = "1 Hour";
        } else if (RemainingQuarter == "0 Hours") {
            RemainingQuarter = "< 1 Hour";
        }

        RemainingQuarterColor = "White";

        /* Year */
        dateNext = DateTime.Now;
        while (dateNext.Year == DateTime.Now.Year) {
            dateNext = dateNext.AddDays(1);
        }

        RemainingYear = (dateNext.Date - DateTime.Now.Date).TotalDays + " Days";

        if (RemainingYear == "1 Days") {
            blinkYear = true;

            TimeSpan timeSpan = TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay;
            RemainingYear = timeSpan.Hours + " Hours";
        }

        if (RemainingYear == "1 Hours") {
            RemainingYear = "1 Hour";
        } else if (RemainingYear == "0 Hours") {
            RemainingYear = "< 1 Hour";
        }

        RemainingYearColor = "White";
    }

    private void TrashDayLogic() {
        if (ReferenceValues.JsonSettingsMaster.UseTrashDayHolidays) {
            if (ReferenceValues.JsonSettingsMaster.TrashDay == "DISABLED") {
                return;
            }

            bool breakout = false;
            DateTime date = DateTime.Now;
            DayOfWeek trashDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), ReferenceValues.JsonSettingsMaster.TrashDay);

            if (date.DayOfWeek > trashDay) {
                return;
            }

            ReferenceValues.AdjustedTrashDay = ReferenceValues.JsonSettingsMaster.TrashDay;
            
            while (date.DayOfWeek <= trashDay) {
                foreach (Holidays.HolidayBlock holiday in Holidays.GetHolidays(date.Year)) {
                    if (!breakout) {
                        if (holiday.Holiday == "New Year's" || holiday.Holiday == "Memorial" || holiday.Holiday == "Independence" || holiday.Holiday == "Labor" || holiday.Holiday == "Thanksgiving" ||
                            holiday.Holiday == "Christmas") {
                            if (date.Month == holiday.Date.Month && date.Day == holiday.Date.Day) {
                                /* Add one day to trash day */
                                trashDay++;
                                ReferenceValues.AdjustedTrashDay = trashDay.ToString();
                                breakout = true;

                                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                    Date = DateTime.Now,
                                    Level = "INFO",
                                    Module = "BehaviorVM",
                                    Description = "Adjusting trash day to " + trashDay + " because of " + holiday.Holiday
                                });
                                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                            }
                        }
                    }
                }

                /* Subtract a day */
                date = date.AddDays(-1);
            }
        }

        if (DateTime.Now.DayOfWeek.ToString() == ReferenceValues.AdjustedTrashDay && DateTime.Now.Hour > 11) {
            TrashDayVisibility = "VISIBLE";
        } else {
            TrashDayVisibility = "HIDDEN";
        }
    }

    #region Fields

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

    public string User1CashAvailableColor {
        get => _user1CashAvailableColor;
        set {
            _user1CashAvailableColor = value;
            RaisePropertyChangedEvent("User1CashAvailableColor");
        }
    }

    public int User1TasksCompletedDayProgressValue {
        get => _user1TasksCompletedDayProgressValue;
        set {
            _user1TasksCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User1TasksCompletedDayProgressValue");
        }
    }

    public int User1TasksCompletedWeekProgressValue {
        get => _user1TasksCompletedWeekProgressValue;
        set {
            _user1TasksCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User1TasksCompletedWeekProgressValue");
        }
    }

    public int User1TasksCompletedMonthProgressValue {
        get => _user1TasksCompletedMonthProgressValue;
        set {
            _user1TasksCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User1TasksCompletedMonthProgressValue");
        }
    }

    public int User1TasksCompletedQuarterProgressValue {
        get => _user1TasksCompletedQuarterProgressValue;
        set {
            _user1TasksCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User1TasksCompletedQuarterProgressValue");
        }
    }

    public string User1TasksCompletedDayProgressText {
        get => _user1TasksCompletedDayProgressText;
        set {
            _user1TasksCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User1TasksCompletedDayProgressText");
        }
    }

    public string User1TasksCompletedWeekProgressText {
        get => _user1TasksCompletedWeekProgressText;
        set {
            _user1TasksCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User1TasksCompletedWeekProgressText");
        }
    }

    public string User1TasksCompletedMonthProgressText {
        get => _user1TasksCompletedMonthProgressText;
        set {
            _user1TasksCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User1TasksCompletedMonthProgressText");
        }
    }

    public string User1TasksCompletedQuarterProgressText {
        get => _user1TasksCompletedQuarterProgressText;
        set {
            _user1TasksCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User1TasksCompletedQuarterProgressText");
        }
    }

    public string User2CashAvailable {
        get => _user2CashAvailable;
        set {
            _user2CashAvailable = value;
            RaisePropertyChangedEvent("User2CashAvailable");
        }
    }

    public string User2CashAvailableColor {
        get => _user2CashAvailableColor;
        set {
            _user2CashAvailableColor = value;
            RaisePropertyChangedEvent("User2CashAvailableColor");
        }
    }

    public int User2TasksCompletedDayProgressValue {
        get => _user2TasksCompletedDayProgressValue;
        set {
            _user2TasksCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User2TasksCompletedDayProgressValue");
        }
    }

    public int User2TasksCompletedWeekProgressValue {
        get => _user2TasksCompletedWeekProgressValue;
        set {
            _user2TasksCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User2TasksCompletedWeekProgressValue");
        }
    }

    public int User2TasksCompletedMonthProgressValue {
        get => _user2TasksCompletedMonthProgressValue;
        set {
            _user2TasksCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User2TasksCompletedMonthProgressValue");
        }
    }

    public int User2TasksCompletedQuarterProgressValue {
        get => _user2TasksCompletedQuarterProgressValue;
        set {
            _user2TasksCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User2TasksCompletedQuarterProgressValue");
        }
    }

    public string User2TasksCompletedDayProgressText {
        get => _user2TasksCompletedDayProgressText;
        set {
            _user2TasksCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User2TasksCompletedDayProgressText");
        }
    }

    public string User2TasksCompletedWeekProgressText {
        get => _user2TasksCompletedWeekProgressText;
        set {
            _user2TasksCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User2TasksCompletedWeekProgressText");
        }
    }

    public string User2TasksCompletedMonthProgressText {
        get => _user2TasksCompletedMonthProgressText;
        set {
            _user2TasksCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User2TasksCompletedMonthProgressText");
        }
    }

    public string User2TasksCompletedQuarterProgressText {
        get => _user2TasksCompletedQuarterProgressText;
        set {
            _user2TasksCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User2TasksCompletedQuarterProgressText");
        }
    }

    public string User3CashAvailable {
        get => _user3CashAvailable;
        set {
            _user3CashAvailable = value;
            RaisePropertyChangedEvent("User3CashAvailable");
        }
    }

    public string User3CashAvailableColor {
        get => _user3CashAvailableColor;
        set {
            _user3CashAvailableColor = value;
            RaisePropertyChangedEvent("User3CashAvailableColor");
        }
    }

    public int User3TasksCompletedDayProgressValue {
        get => _user3TasksCompletedDayProgressValue;
        set {
            _user3TasksCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User3TasksCompletedDayProgressValue");
        }
    }

    public int User3TasksCompletedWeekProgressValue {
        get => _user3TasksCompletedWeekProgressValue;
        set {
            _user3TasksCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User3TasksCompletedWeekProgressValue");
        }
    }

    public int User3TasksCompletedMonthProgressValue {
        get => _user3TasksCompletedMonthProgressValue;
        set {
            _user3TasksCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User3TasksCompletedMonthProgressValue");
        }
    }

    public int User3TasksCompletedQuarterProgressValue {
        get => _user3TasksCompletedQuarterProgressValue;
        set {
            _user3TasksCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User3TasksCompletedQuarterProgressValue");
        }
    }

    public string User3TasksCompletedDayProgressText {
        get => _user3TasksCompletedDayProgressText;
        set {
            _user3TasksCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User3TasksCompletedDayProgressText");
        }
    }

    public string User3TasksCompletedWeekProgressText {
        get => _user3TasksCompletedWeekProgressText;
        set {
            _user3TasksCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User3TasksCompletedWeekProgressText");
        }
    }

    public string User3TasksCompletedMonthProgressText {
        get => _user3TasksCompletedMonthProgressText;
        set {
            _user3TasksCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User3TasksCompletedMonthProgressText");
        }
    }

    public string User3TasksCompletedQuarterProgressText {
        get => _user3TasksCompletedQuarterProgressText;
        set {
            _user3TasksCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User3TasksCompletedQuarterProgressText");
        }
    }

    public string User4CashAvailableColor {
        get => _user4CashAvailableColor;
        set {
            _user4CashAvailableColor = value;
            RaisePropertyChangedEvent("User4CashAvailableColor");
        }
    }

    public string User4CashAvailable {
        get => _user4CashAvailable;
        set {
            _user4CashAvailable = value;
            RaisePropertyChangedEvent("User4CashAvailable");
        }
    }

    public int User4TasksCompletedDayProgressValue {
        get => _user4TasksCompletedDayProgressValue;
        set {
            _user4TasksCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User4TasksCompletedDayProgressValue");
        }
    }

    public int User4TasksCompletedWeekProgressValue {
        get => _user4TasksCompletedWeekProgressValue;
        set {
            _user4TasksCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User4TasksCompletedWeekProgressValue");
        }
    }

    public int User4TasksCompletedMonthProgressValue {
        get => _user4TasksCompletedMonthProgressValue;
        set {
            _user4TasksCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User4TasksCompletedMonthProgressValue");
        }
    }

    public int User4TasksCompletedQuarterProgressValue {
        get => _user4TasksCompletedQuarterProgressValue;
        set {
            _user4TasksCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User4TasksCompletedQuarterProgressValue");
        }
    }

    public string User4TasksCompletedDayProgressText {
        get => _user4TasksCompletedDayProgressText;
        set {
            _user4TasksCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User4TasksCompletedDayProgressText");
        }
    }

    public string User4TasksCompletedWeekProgressText {
        get => _user4TasksCompletedWeekProgressText;
        set {
            _user4TasksCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User4TasksCompletedWeekProgressText");
        }
    }

    public string User4TasksCompletedMonthProgressText {
        get => _user4TasksCompletedMonthProgressText;
        set {
            _user4TasksCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User4TasksCompletedMonthProgressText");
        }
    }

    public string User4TasksCompletedQuarterProgressText {
        get => _user4TasksCompletedQuarterProgressText;
        set {
            _user4TasksCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User4TasksCompletedQuarterProgressText");
        }
    }

    public string User5CashAvailable {
        get => _user5CashAvailable;
        set {
            _user5CashAvailable = value;
            RaisePropertyChangedEvent("User5CashAvailable");
        }
    }

    public string User5CashAvailableColor {
        get => _user5CashAvailableColor;
        set {
            _user5CashAvailableColor = value;
            RaisePropertyChangedEvent("User5CashAvailableColor");
        }
    }

    public int User5TasksCompletedDayProgressValue {
        get => _user5TasksCompletedDayProgressValue;
        set {
            _user5TasksCompletedDayProgressValue = value;
            RaisePropertyChangedEvent("User5TasksCompletedDayProgressValue");
        }
    }

    public int User5TasksCompletedWeekProgressValue {
        get => _user5TasksCompletedWeekProgressValue;
        set {
            _user5TasksCompletedWeekProgressValue = value;
            RaisePropertyChangedEvent("User5TasksCompletedWeekProgressValue");
        }
    }

    public int User5TasksCompletedMonthProgressValue {
        get => _user5TasksCompletedMonthProgressValue;
        set {
            _user5TasksCompletedMonthProgressValue = value;
            RaisePropertyChangedEvent("User5TasksCompletedMonthProgressValue");
        }
    }

    public int User5TasksCompletedQuarterProgressValue {
        get => _user5TasksCompletedQuarterProgressValue;
        set {
            _user5TasksCompletedQuarterProgressValue = value;
            RaisePropertyChangedEvent("User5TasksCompletedQuarterProgressValue");
        }
    }

    public string User5TasksCompletedDayProgressText {
        get => _user5TasksCompletedDayProgressText;
        set {
            _user5TasksCompletedDayProgressText = value;
            RaisePropertyChangedEvent("User5TasksCompletedDayProgressText");
        }
    }

    public string User5TasksCompletedWeekProgressText {
        get => _user5TasksCompletedWeekProgressText;
        set {
            _user5TasksCompletedWeekProgressText = value;
            RaisePropertyChangedEvent("User5TasksCompletedWeekProgressText");
        }
    }

    public string User5TasksCompletedMonthProgressText {
        get => _user5TasksCompletedMonthProgressText;
        set {
            _user5TasksCompletedMonthProgressText = value;
            RaisePropertyChangedEvent("User5TasksCompletedMonthProgressText");
        }
    }

    public string User5TasksCompletedQuarterProgressText {
        get => _user5TasksCompletedQuarterProgressText;
        set {
            _user5TasksCompletedQuarterProgressText = value;
            RaisePropertyChangedEvent("User5TasksCompletedQuarterProgressText");
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

    public string User1TasksCompletedDayProgressColor {
        get => _user1TasksCompletedDayProgressColor;
        set {
            _user1TasksCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User1TasksCompletedDayProgressColor");
        }
    }

    public string User1TasksCompletedWeekProgressColor {
        get => _user1TasksCompletedWeekProgressColor;
        set {
            _user1TasksCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User1TasksCompletedWeekProgressColor");
        }
    }

    public string User1TasksCompletedMonthProgressColor {
        get => _user1TasksCompletedMonthProgressColor;
        set {
            _user1TasksCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User1TasksCompletedMonthProgressColor");
        }
    }

    public string User1TasksCompletedQuarterProgressColor {
        get => _user1TasksCompletedQuarterProgressColor;
        set {
            _user1TasksCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User1TasksCompletedQuarterProgressColor");
        }
    }

    public string User2TasksCompletedDayProgressColor {
        get => _user2TasksCompletedDayProgressColor;
        set {
            _user2TasksCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User2TasksCompletedDayProgressColor");
        }
    }

    public string User2TasksCompletedWeekProgressColor {
        get => _user2TasksCompletedWeekProgressColor;
        set {
            _user2TasksCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User2TasksCompletedWeekProgressColor");
        }
    }

    public string User2TasksCompletedMonthProgressColor {
        get => _user2TasksCompletedMonthProgressColor;
        set {
            _user2TasksCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User2TasksCompletedMonthProgressColor");
        }
    }

    public string User2TasksCompletedQuarterProgressColor {
        get => _user2TasksCompletedQuarterProgressColor;
        set {
            _user2TasksCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User2TasksCompletedQuarterProgressColor");
        }
    }

    public string User3TasksCompletedDayProgressColor {
        get => _user3TasksCompletedDayProgressColor;
        set {
            _user3TasksCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User3TasksCompletedDayProgressColor");
        }
    }

    public string User3TasksCompletedWeekProgressColor {
        get => _user3TasksCompletedWeekProgressColor;
        set {
            _user3TasksCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User3TasksCompletedWeekProgressColor");
        }
    }

    public string User3TasksCompletedMonthProgressColor {
        get => _user3TasksCompletedMonthProgressColor;
        set {
            _user3TasksCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User3TasksCompletedMonthProgressColor");
        }
    }

    public string User3TasksCompletedQuarterProgressColor {
        get => _user3TasksCompletedQuarterProgressColor;
        set {
            _user3TasksCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User3TasksCompletedQuarterProgressColor");
        }
    }

    public string User4TasksCompletedDayProgressColor {
        get => _user4TasksCompletedDayProgressColor;
        set {
            _user4TasksCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User4TasksCompletedDayProgressColor");
        }
    }

    public string User4TasksCompletedWeekProgressColor {
        get => _user4TasksCompletedWeekProgressColor;
        set {
            _user4TasksCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User4TasksCompletedWeekProgressColor");
        }
    }

    public string User4TasksCompletedMonthProgressColor {
        get => _user4TasksCompletedMonthProgressColor;
        set {
            _user4TasksCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User4TasksCompletedMonthProgressColor");
        }
    }

    public string User4TasksCompletedQuarterProgressColor {
        get => _user4TasksCompletedQuarterProgressColor;
        set {
            _user4TasksCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User4TasksCompletedQuarterProgressColor");
        }
    }

    public string User5TasksCompletedDayProgressColor {
        get => _user5TasksCompletedDayProgressColor;
        set {
            _user5TasksCompletedDayProgressColor = value;
            RaisePropertyChangedEvent("User5TasksCompletedDayProgressColor");
        }
    }

    public string User5TasksCompletedWeekProgressColor {
        get => _user5TasksCompletedWeekProgressColor;
        set {
            _user5TasksCompletedWeekProgressColor = value;
            RaisePropertyChangedEvent("User5TasksCompletedWeekProgressColor");
        }
    }

    public string User5TasksCompletedMonthProgressColor {
        get => _user5TasksCompletedMonthProgressColor;
        set {
            _user5TasksCompletedMonthProgressColor = value;
            RaisePropertyChangedEvent("User5TasksCompletedMonthProgressColor");
        }
    }

    public string User5TasksCompletedQuarterProgressColor {
        get => _user5TasksCompletedQuarterProgressColor;
        set {
            _user5TasksCompletedQuarterProgressColor = value;
            RaisePropertyChangedEvent("User5TasksCompletedQuarterProgressColor");
        }
    }

    public string User1CashReleased {
        get => _user1CashReleased;
        set {
            _user1CashReleased = value;
            RaisePropertyChangedEvent("User1CashReleased");
        }
    }

    public string User2CashReleased {
        get => _user2CashReleased;
        set {
            _user2CashReleased = value;
            RaisePropertyChangedEvent("User2CashReleased");
        }
    }

    public string User3CashReleased {
        get => _user3CashReleased;
        set {
            _user3CashReleased = value;
            RaisePropertyChangedEvent("User3CashReleased");
        }
    }

    public string User4CashReleased {
        get => _user4CashReleased;
        set {
            _user4CashReleased = value;
            RaisePropertyChangedEvent("User4CashReleased");
        }
    }

    public string User5CashReleased {
        get => _user5CashReleased;
        set {
            _user5CashReleased = value;
            RaisePropertyChangedEvent("User5CashReleased");
        }
    }

    public int ProgressBarUser1 {
        get => _progressBarUser1;
        set {
            _progressBarUser1 = value;
            RaisePropertyChangedEvent("ProgressBarUser1");
        }
    }

    public int ProgressBarUser2 {
        get => _progressBarUser2;
        set {
            _progressBarUser2 = value;
            RaisePropertyChangedEvent("ProgressBarUser2");
        }
    }

    public int ProgressBarUser3 {
        get => _progressBarUser3;
        set {
            _progressBarUser3 = value;
            RaisePropertyChangedEvent("ProgressBarUser3");
        }
    }

    public int ProgressBarUser4 {
        get => _progressBarUser4;
        set {
            _progressBarUser4 = value;
            RaisePropertyChangedEvent("ProgressBarUser4");
        }
    }

    public int ProgressBarUser5 {
        get => _progressBarUser5;
        set {
            _progressBarUser5 = value;
            RaisePropertyChangedEvent("ProgressBarUser5");
        }
    }

    public string User1DayVisibility {
        get => _user1DayVisibility;
        set {
            _user1DayVisibility = value;
            RaisePropertyChangedEvent("User1DayVisibility");
        }
    }

    public string User2DayVisibility {
        get => _user2DayVisibility;
        set {
            _user2DayVisibility = value;
            RaisePropertyChangedEvent("User2DayVisibility");
        }
    }

    public string User3DayVisibility {
        get => _user3DayVisibility;
        set {
            _user3DayVisibility = value;
            RaisePropertyChangedEvent("User3DayVisibility");
        }
    }

    public string User4DayVisibility {
        get => _user4DayVisibility;
        set {
            _user4DayVisibility = value;
            RaisePropertyChangedEvent("User4DayVisibility");
        }
    }

    public string User5DayVisibility {
        get => _user5DayVisibility;
        set {
            _user5DayVisibility = value;
            RaisePropertyChangedEvent("User5DayVisibility");
        }
    }

    public string User1WeekVisibility {
        get => _user1WeekVisibility;
        set {
            _user1WeekVisibility = value;
            RaisePropertyChangedEvent("User1WeekVisibility");
        }
    }

    public string User2WeekVisibility {
        get => _user2WeekVisibility;
        set {
            _user2WeekVisibility = value;
            RaisePropertyChangedEvent("User2WeekVisibility");
        }
    }

    public string User3WeekVisibility {
        get => _user3WeekVisibility;
        set {
            _user3WeekVisibility = value;
            RaisePropertyChangedEvent("User3WeekVisibility");
        }
    }

    public string User4WeekVisibility {
        get => _user4WeekVisibility;
        set {
            _user4WeekVisibility = value;
            RaisePropertyChangedEvent("User4WeekVisibility");
        }
    }

    public string User5WeekVisibility {
        get => _user5WeekVisibility;
        set {
            _user5WeekVisibility = value;
            RaisePropertyChangedEvent("User5WeekVisibility");
        }
    }

    public string User1MonthVisibility {
        get => _user1MonthVisibility;
        set {
            _user1MonthVisibility = value;
            RaisePropertyChangedEvent("User1MonthVisibility");
        }
    }

    public string User2MonthVisibility {
        get => _user2MonthVisibility;
        set {
            _user2MonthVisibility = value;
            RaisePropertyChangedEvent("User2MonthVisibility");
        }
    }

    public string User3MonthVisibility {
        get => _user3MonthVisibility;
        set {
            _user3MonthVisibility = value;
            RaisePropertyChangedEvent("User3MonthVisibility");
        }
    }

    public string User4MonthVisibility {
        get => _user4MonthVisibility;
        set {
            _user4MonthVisibility = value;
            RaisePropertyChangedEvent("User4MonthVisibility");
        }
    }

    public string User5MonthVisibility {
        get => _user5MonthVisibility;
        set {
            _user5MonthVisibility = value;
            RaisePropertyChangedEvent("User5MonthVisibility");
        }
    }

    public string User1QuarterVisibility {
        get => _user1QuarterVisibility;
        set {
            _user1QuarterVisibility = value;
            RaisePropertyChangedEvent("User1QuarterVisibility");
        }
    }

    public string User2QuarterVisibility {
        get => _user2QuarterVisibility;
        set {
            _user2QuarterVisibility = value;
            RaisePropertyChangedEvent("User2QuarterVisibility");
        }
    }

    public string User3QuarterVisibility {
        get => _user3QuarterVisibility;
        set {
            _user3QuarterVisibility = value;
            RaisePropertyChangedEvent("User3QuarterVisibility");
        }
    }

    public string User4QuarterVisibility {
        get => _user4QuarterVisibility;
        set {
            _user4QuarterVisibility = value;
            RaisePropertyChangedEvent("User4QuarterVisibility");
        }
    }

    public string User5QuarterVisibility {
        get => _user5QuarterVisibility;
        set {
            _user5QuarterVisibility = value;
            RaisePropertyChangedEvent("User5QuarterVisibility");
        }
    }

    public string User1BehaviorVisibility {
        get => _user1BehaviorVisibility;
        set {
            _user1BehaviorVisibility = value;
            RaisePropertyChangedEvent("User1BehaviorVisibility");
        }
    }

    public string User2BehaviorVisibility {
        get => _user2BehaviorVisibility;
        set {
            _user2BehaviorVisibility = value;
            RaisePropertyChangedEvent("User2BehaviorVisibility");
        }
    }

    public string User3BehaviorVisibility {
        get => _user3BehaviorVisibility;
        set {
            _user3BehaviorVisibility = value;
            RaisePropertyChangedEvent("User3BehaviorVisibility");
        }
    }

    public string User4BehaviorVisibility {
        get => _user4BehaviorVisibility;
        set {
            _user4BehaviorVisibility = value;
            RaisePropertyChangedEvent("User4BehaviorVisibility");
        }
    }

    public string User5BehaviorVisibility {
        get => _user5BehaviorVisibility;
        set {
            _user5BehaviorVisibility = value;
            RaisePropertyChangedEvent("User5BehaviorVisibility");
        }
    }

    public string TrashDayVisibility {
        get => _trashDayVisibility;
        set {
            _trashDayVisibility = value;
            RaisePropertyChangedEvent("TrashDayVisibility");
        }
    }

    public string User1BackgroundColor {
        get => _user1BackgroundColor;
        set {
            _user1BackgroundColor = value;
            RaisePropertyChangedEvent("User1BackgroundColor");
        }
    }

    public string User2BackgroundColor {
        get => _user2BackgroundColor;
        set {
            _user2BackgroundColor = value;
            RaisePropertyChangedEvent("User2BackgroundColor");
        }
    }

    public string User3BackgroundColor {
        get => _user3BackgroundColor;
        set {
            _user3BackgroundColor = value;
            RaisePropertyChangedEvent("User3BackgroundColor");
        }
    }

    public string User4BackgroundColor {
        get => _user4BackgroundColor;
        set {
            _user4BackgroundColor = value;
            RaisePropertyChangedEvent("User4BackgroundColor");
        }
    }

    public string User5BackgroundColor {
        get => _user5BackgroundColor;
        set {
            _user5BackgroundColor = value;
            RaisePropertyChangedEvent("User5BackgroundColor");
        }
    }

    public string RemainingDayColor {
        get => _remainingDayColor;
        set {
            _remainingDayColor = value;
            RaisePropertyChangedEvent("RemainingDayColor");
        }
    }

    public string RemainingWeekColor {
        get => _remainingWeekColor;
        set {
            _remainingWeekColor = value;
            RaisePropertyChangedEvent("RemainingWeekColor");
        }
    }

    public string RemainingMonthColor {
        get => _remainingMonthColor;
        set {
            _remainingMonthColor = value;
            RaisePropertyChangedEvent("RemainingMonthColor");
        }
    }

    public string RemainingQuarterColor {
        get => _remainingQuarterColor;
        set {
            _remainingQuarterColor = value;
            RaisePropertyChangedEvent("RemainingQuarterColor");
        }
    }

    public string RemainingYearColor {
        get => _remainingYearColor;
        set {
            _remainingYearColor = value;
            RaisePropertyChangedEvent("RemainingYearColor");
        }
    }

    #endregion
}