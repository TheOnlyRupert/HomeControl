﻿using System.Globalization;
using System.Text.Json;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Behavior;
using HomeControl.Source.ViewModel.Base;
using MySql.Data.MySqlClient;
using Task = HomeControl.Source.Json.Task;

namespace HomeControl.Source.ViewModel.Behavior;

public class BehaviorVM : BaseViewModel {
    private BitmapImage _imageUser1, _imageUser2, _imageUser3, _imageUser4, _imageUser5;

    private string _user1Star1, _user1Star2, _user1Star3, _user1Star4, _user1Star5, _user2Star1, _user2Star2, _user2Star3, _user2Star4, _user2Star5, _user3Star1, _user3Star2, _user3Star3, _user3Star4,
        _user3Star5, _user4Star1, _user4Star2, _user4Star3, _user4Star4, _user4Star5, _user5Star1, _user5Star2, _user5Star3, _user5Star4, _user5Star5, _user1Strike1, _user1Strike2, _user1Strike3,
        _user2Strike1, _user2Strike2, _user2Strike3, _user3Strike1, _user3Strike2, _user3Strike3, _user4Strike1, _user4Strike2, _user4Strike3, _user5Strike1, _user5Strike2, _user5Strike3,
        _user1TasksCompletedDayProgressColor, _user1TasksCompletedWeekProgressColor, _user1TasksCompletedMonthProgressColor, _user1TasksCompletedQuarterProgressColor, _user2TasksCompletedDayProgressColor,
        _user2TasksCompletedWeekProgressColor, _user2TasksCompletedMonthProgressColor, _user2TasksCompletedQuarterProgressColor, _user3TasksCompletedDayProgressColor, _user3TasksCompletedWeekProgressColor,
        _user3TasksCompletedMonthProgressColor, _user3TasksCompletedQuarterProgressColor, _user4TasksCompletedDayProgressColor, _user4TasksCompletedWeekProgressColor, _user4TasksCompletedMonthProgressColor,
        _user4TasksCompletedQuarterProgressColor, _user5TasksCompletedDayProgressColor, _user5TasksCompletedWeekProgressColor, _user5TasksCompletedMonthProgressColor, _user5TasksCompletedQuarterProgressColor,
        _currentMonthText, _currentWeekText, _currentDayText, _currentQuarterText, _user1TasksCompletedWeekProgressText, _user1TasksCompletedDayProgressText, _user1TasksCompletedMonthProgressText,
        _user1TasksCompletedQuarterProgressText, _user2TasksCompletedWeekProgressText, _user2TasksCompletedDayProgressText, _user2TasksCompletedMonthProgressText, _user2TasksCompletedQuarterProgressText,
        _user3TasksCompletedWeekProgressText, _user3TasksCompletedDayProgressText, _user3TasksCompletedMonthProgressText, _user3TasksCompletedQuarterProgressText, _user4TasksCompletedWeekProgressText,
        _user4TasksCompletedDayProgressText, _user4TasksCompletedMonthProgressText, _user4TasksCompletedQuarterProgressText, _user5TasksCompletedWeekProgressText, _user5TasksCompletedDayProgressText,
        _user5TasksCompletedMonthProgressText, _user5TasksCompletedQuarterProgressText, _user1DayVisibility, _user2DayVisibility, _user3DayVisibility, _user4DayVisibility, _user5DayVisibility,
        _user1WeekVisibility, _user2WeekVisibility, _user3WeekVisibility, _user4WeekVisibility, _user5WeekVisibility, _user1MonthVisibility, _user2MonthVisibility, _user3MonthVisibility, _user4MonthVisibility,
        _user5MonthVisibility, _user1QuarterVisibility, _user2QuarterVisibility, _user3QuarterVisibility, _user4QuarterVisibility, _user5QuarterVisibility, _user1BackgroundColor, _user2BackgroundColor,
        _user3BackgroundColor, _user4BackgroundColor, _user5BackgroundColor, _user1Visibility, _user2Visibility, _user3Visibility, _user4Visibility, _user5Visibility;

    private int _user1TasksCompletedDayProgressValue, _user1TasksCompletedWeekProgressValue, _user1TasksCompletedMonthProgressValue, _user1TasksCompletedQuarterProgressValue,
        _user2TasksCompletedDayProgressValue, _user2TasksCompletedWeekProgressValue, _user2TasksCompletedMonthProgressValue, _user2TasksCompletedQuarterProgressValue,
        _user3TasksCompletedDayProgressValue, _user3TasksCompletedWeekProgressValue, _user3TasksCompletedMonthProgressValue, _user3TasksCompletedQuarterProgressValue,
        _user4TasksCompletedDayProgressValue, _user4TasksCompletedWeekProgressValue, _user4TasksCompletedMonthProgressValue, _user4TasksCompletedQuarterProgressValue,
        _user5TasksCompletedDayProgressValue, _user5TasksCompletedWeekProgressValue, _user5TasksCompletedMonthProgressValue, _user5TasksCompletedQuarterProgressValue;

    public BehaviorVM() {
        /* Do this here so I don't get null references in the future */
        ReferenceValues.JsonBehaviorMaster = new JsonBehavior {
            User1Strikes = 0,
            User2Strikes = 0,
            User3Strikes = 0,
            User4Strikes = 0,
            User5Strikes = 0,
            User1Stars = 0,
            User2Stars = 0,
            User3Stars = 0,
            User4Stars = 0,
            User5Stars = 0
        };

        try {
            ReferenceValues.JsonTasksMaster = JsonSerializer.Deserialize<JsonTasks>(FileHelpers.LoadFileText("tasks", true));
        } catch (Exception) {
            ReferenceValues.JsonTasksMaster = new JsonTasks();

            FileHelpers.SaveFileText("tasks", JsonSerializer.Serialize(ReferenceValues.JsonTasksMaster), true);
        }

        ReferenceValues.JsonTasksMaster ??= new JsonTasks();
        ReferenceValues.JsonTasksMaster.JsonTasksDaily ??= new JsonTasksDaily();
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly ??= new JsonTasksWeekly();
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly ??= new JsonTasksMonthly();
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser1 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser2 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser3 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser4 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser5 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly ??= new JsonTasksQuarterly();
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser1 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser2 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser3 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser4 ??= [];
        ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser5 ??= [];

        try {
            Uri uri = new(ReferenceValues.DocumentsDirectory + "icons/user1.png", UriKind.RelativeOrAbsolute);
            ImageUser1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user2.png", UriKind.RelativeOrAbsolute);
            ImageUser2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user3.png", UriKind.RelativeOrAbsolute);
            ImageUser3 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user4.png", UriKind.RelativeOrAbsolute);
            ImageUser4 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user5.png", UriKind.RelativeOrAbsolute);
            ImageUser5 = new BitmapImage(uri);
        } catch (Exception ex) {
            FileHelpers.LogDebugMessage("WARN", "BehaviorVM.BehaviorVM", $"An error occurred:\n{ex.Message}");
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

        RefreshBehavior();
        RefreshTasks(1);
        RefreshTasks(2);
        RefreshTasks(3);
        RefreshTasks(4);
        RefreshTasks(5);
        RefreshBlinking();
        SaveJsons();
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private void RefreshTasks(int userId) {
        int totalDay = 0, totalWeek = 0, totalMonth = 0, totalQuarter = 0, completedDay = 0, completedWeek = 0, completedMonth = 0, completedQuarter = 0;
        double math;

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

        switch (userId) {
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

                /* Reset daily */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User1TasksCompletedDayProgressText = "0%";
                    User1TasksCompletedDayProgressValue = 0;
                }
            } else {
                User1TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User1TasksCompletedWeekProgressText = User1TasksCompletedWeekProgressValue + "%";

                /* Reset weekly */
                if (d1 != d2) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User1TasksCompletedWeekProgressText = "0%";
                    User1TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User1TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User1TasksCompletedMonthProgressText = User1TasksCompletedMonthProgressValue + "%";

                /* Reset monthly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser1) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User1TasksCompletedMonthProgressText = "0%";
                    User1TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User1TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User1TasksCompletedQuarterProgressText = User1TasksCompletedQuarterProgressValue + "%";

                /* Reset quarterly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser1) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

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

            User1DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1.Count > 0 ? "VISIBLE" : "HIDDEN";
            User1WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1.Count > 0 ? "VISIBLE" : "HIDDEN";
            User1MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser1.Count > 0 ? "VISIBLE" : "HIDDEN";
            User1QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser1.Count > 0 ? "VISIBLE" : "HIDDEN";

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

                /* Reset daily */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User2TasksCompletedDayProgressText = "0%";
                    User2TasksCompletedDayProgressValue = 0;
                }
            } else {
                User2TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User2TasksCompletedWeekProgressText = User2TasksCompletedWeekProgressValue + "%";

                /* Reset weekly */
                if (d1 != d2) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User2TasksCompletedWeekProgressText = "0%";
                    User2TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User2TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User2TasksCompletedMonthProgressText = User2TasksCompletedMonthProgressValue + "%";

                /* Reset monthly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser2) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User2TasksCompletedMonthProgressText = "0%";
                    User2TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User2TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User2TasksCompletedQuarterProgressText = User2TasksCompletedQuarterProgressValue + "%";

                /* Reset quarterly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser2) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

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

            User2DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2.Count > 0 ? "VISIBLE" : "HIDDEN";
            User2WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2.Count > 0 ? "VISIBLE" : "HIDDEN";
            User2MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser2.Count > 0 ? "VISIBLE" : "HIDDEN";
            User2QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser2.Count > 0 ? "VISIBLE" : "HIDDEN";

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

                /* Reset daily */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User3TasksCompletedDayProgressText = "0%";
                    User3TasksCompletedDayProgressValue = 0;
                }
            } else {
                User3TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User3TasksCompletedWeekProgressText = User3TasksCompletedWeekProgressValue + "%";

                /* Reset weekly */
                if (d1 != d2) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User3TasksCompletedWeekProgressText = "0%";
                    User3TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User3TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User3TasksCompletedMonthProgressText = User3TasksCompletedMonthProgressValue + "%";

                /* Reset monthly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser3) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User3TasksCompletedMonthProgressText = "0%";
                    User3TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User3TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User3TasksCompletedQuarterProgressText = User3TasksCompletedQuarterProgressValue + "%";

                /* Reset quarterly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser3) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

                        User3TasksCompletedQuarterProgressText = "0%";
                        User3TasksCompletedQuarterProgressValue = 0;
                    }
                }
            } else {
                User3TasksCompletedQuarterProgressText = "None";
            }

            User3TasksCompletedDayProgressColor = User3TasksCompletedDayProgressValue == 100 ? "Green" : "CornflowerBlue";
            User3TasksCompletedWeekProgressColor = User3TasksCompletedWeekProgressValue == 100 ? "Green" : "CornflowerBlue";
            User3TasksCompletedMonthProgressColor = User3TasksCompletedMonthProgressValue == 100 ? "Green" : "CornflowerBlue";
            User3TasksCompletedQuarterProgressColor = User3TasksCompletedQuarterProgressValue == 100 ? "Green" : "CornflowerBlue";

            User3DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3.Count > 0 ? "VISIBLE" : "HIDDEN";
            User3WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3.Count > 0 ? "VISIBLE" : "HIDDEN";
            User3MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser3.Count > 0 ? "VISIBLE" : "HIDDEN";
            User3QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser3.Count > 0 ? "VISIBLE" : "HIDDEN";

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

                /* Reset daily */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User4TasksCompletedDayProgressText = "0%";
                    User4TasksCompletedDayProgressValue = 0;
                }
            } else {
                User4TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User4TasksCompletedWeekProgressText = User4TasksCompletedWeekProgressValue + "%";

                /* Reset weekly */
                if (d1 != d2) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User4TasksCompletedWeekProgressText = "0%";
                    User4TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User4TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User4TasksCompletedMonthProgressText = User4TasksCompletedMonthProgressValue + "%";

                /* Reset monthly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser4) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User4TasksCompletedMonthProgressText = "0%";
                    User4TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User4TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User4TasksCompletedQuarterProgressText = User4TasksCompletedQuarterProgressValue + "%";

                /* Reset quarterly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser4) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

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

            User4DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4.Count > 0 ? "VISIBLE" : "HIDDEN";
            User4WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4.Count > 0 ? "VISIBLE" : "HIDDEN";
            User4MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser4.Count > 0 ? "VISIBLE" : "HIDDEN";
            User4QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser4.Count > 0 ? "VISIBLE" : "HIDDEN";

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

                /* Reset daily */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Date.Day != DateTime.Today.Day) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }
                }

                User5TasksCompletedDayProgressText = "0%";
                User5TasksCompletedDayProgressValue = 0;
            } else {
                User5TasksCompletedDayProgressText = "None";
            }

            if (totalWeek != 0) {
                User5TasksCompletedWeekProgressText = User5TasksCompletedWeekProgressValue + "%";

                /* Reset weekly */
                if (d1 != d2) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User5TasksCompletedWeekProgressText = "0%";
                    User5TasksCompletedWeekProgressValue = 0;
                }
            } else {
                User5TasksCompletedWeekProgressText = "None";
            }

            if (totalMonth != 0) {
                User5TasksCompletedMonthProgressText = User5TasksCompletedMonthProgressValue + "%";

                /* Reset monthly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Now.Month) {
                    foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser5) {
                        task.IsCompleted = false;
                        task.DateCompleted = "";
                    }

                    User5TasksCompletedMonthProgressText = "0%";
                    User5TasksCompletedMonthProgressValue = 0;
                }
            } else {
                User5TasksCompletedMonthProgressText = "None";
            }

            if (totalQuarter != 0) {
                User5TasksCompletedQuarterProgressText = User5TasksCompletedQuarterProgressValue + "%";

                /* Reset quarterly */
                if (ReferenceValues.JsonTasksMaster.UpdatedDateTime.Month != DateTime.Today.Month) {
                    if (DateTime.Today.Month == 1 || DateTime.Today.Month == 4 || DateTime.Today.Month == 7 || DateTime.Today.Month == 10) {
                        foreach (Task task in ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser5) {
                            task.IsCompleted = false;
                            task.DateCompleted = "";
                        }

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

            User5DayVisibility = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5.Count > 0 ? "VISIBLE" : "HIDDEN";
            User5WeekVisibility = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5.Count > 0 ? "VISIBLE" : "HIDDEN";
            User5MonthVisibility = ReferenceValues.JsonTasksMaster.JsonTasksMonthly.TaskListMonthlyUser5.Count > 0 ? "VISIBLE" : "HIDDEN";
            User5QuarterVisibility = ReferenceValues.JsonTasksMaster.JsonTasksQuarterly.TaskListQuarterlyUser5.Count > 0 ? "VISIBLE" : "HIDDEN";

            break;
        }
    }

    private void SaveJsons() {
        ReferenceValues.JsonTasksMaster.UpdatedDateTime = DateTime.Today;

        try {
            FileHelpers.SaveFileText("tasks", JsonSerializer.Serialize(ReferenceValues.JsonTasksMaster), true);
        } catch (Exception ex) {
            FileHelpers.LogDebugMessage("WARN", "BehaviorVM.SaveJsons", $"An error occurred:\n{ex.Message}");
        }
    }


    private void RefreshBehavior() {
        User1Star1 = "";
        User1Star2 = "";
        User1Star3 = "";
        User1Star4 = "";
        User1Star5 = "";
        User2Star1 = "";
        User2Star2 = "";
        User2Star3 = "";
        User2Star4 = "";
        User2Star5 = "";
        User3Star1 = "";
        User3Star2 = "";
        User3Star3 = "";
        User3Star4 = "";
        User3Star5 = "";
        User4Star1 = "";
        User4Star2 = "";
        User4Star3 = "";
        User4Star4 = "";
        User4Star5 = "";
        User5Star1 = "";
        User5Star2 = "";
        User5Star3 = "";
        User5Star4 = "";
        User5Star5 = "";

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
        switch (e.PropertyName) {
        case "RefreshBehavior":
        case "DatabaseUpdated":
            RefreshBehavior();
            break;
        case "DateChanged":
            ReferenceValues.JsonBehaviorMaster.User1Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User2Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User3Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User4Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User5Strikes = 0;
            RefreshBehavior();
            RefreshTasks(1);
            RefreshTasks(2);
            RefreshTasks(3);
            RefreshTasks(4);
            RefreshTasks(5);
            SaveJsons();
            break;
        case "HourChanged":
            RefreshBlinking();
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
        if (!ReferenceValues.LockUi) {
            byte strikes;
            byte stars;

            switch (param) {
            case "user1":
                ReferenceValues.ActiveBehaviorUser = 1;
                User1Visibility = "HIDDEN";

                // Check to see if EditBehavior changed them
                stars = ReferenceValues.JsonBehaviorMaster.User1Stars;
                strikes = ReferenceValues.JsonBehaviorMaster.User1Strikes;

                EditBehavior editBehavior = new();
                editBehavior.ShowDialog();
                editBehavior.Close();

                if (stars != ReferenceValues.JsonBehaviorMaster.User1Stars || strikes != ReferenceValues.JsonBehaviorMaster.User1Strikes) {
                    UpdateDatabase(1, ReferenceValues.JsonBehaviorMaster.User1Stars, ReferenceValues.JsonBehaviorMaster.User1Strikes);
                }

                User1Visibility = "VISIBLE";
                RefreshTasks(1);
                RefreshBlinking();
                break;
            case "user2":
                ReferenceValues.ActiveBehaviorUser = 2;
                User2Visibility = "HIDDEN";

                // Check to see if EditBehavior changed them
                stars = ReferenceValues.JsonBehaviorMaster.User2Stars;
                strikes = ReferenceValues.JsonBehaviorMaster.User2Strikes;

                EditBehavior editBehavior2 = new();
                editBehavior2.ShowDialog();
                editBehavior2.Close();

                if (stars != ReferenceValues.JsonBehaviorMaster.User2Stars || strikes != ReferenceValues.JsonBehaviorMaster.User2Strikes) {
                    UpdateDatabase(2, ReferenceValues.JsonBehaviorMaster.User2Stars, ReferenceValues.JsonBehaviorMaster.User2Strikes);
                }

                User2Visibility = "VISIBLE";
                RefreshTasks(2);
                RefreshBlinking();
                break;
            case "user3":
                ReferenceValues.ActiveBehaviorUser = 3;
                User3Visibility = "HIDDEN";

                // Check to see if EditBehavior changed them
                stars = ReferenceValues.JsonBehaviorMaster.User3Stars;
                strikes = ReferenceValues.JsonBehaviorMaster.User3Strikes;

                EditBehavior editBehavior3 = new();
                editBehavior3.ShowDialog();
                editBehavior3.Close();

                if (stars != ReferenceValues.JsonBehaviorMaster.User3Stars || strikes != ReferenceValues.JsonBehaviorMaster.User3Strikes) {
                    UpdateDatabase(3, ReferenceValues.JsonBehaviorMaster.User3Stars, ReferenceValues.JsonBehaviorMaster.User3Strikes);
                }

                User3Visibility = "VISIBLE";
                RefreshTasks(3);
                RefreshBlinking();
                break;
            case "user4":
                ReferenceValues.ActiveBehaviorUser = 4;
                User4Visibility = "HIDDEN";

                // Check to see if EditBehavior changed them
                stars = ReferenceValues.JsonBehaviorMaster.User4Stars;
                strikes = ReferenceValues.JsonBehaviorMaster.User4Strikes;

                EditBehavior editBehavior4 = new();
                editBehavior4.ShowDialog();
                editBehavior4.Close();

                if (stars != ReferenceValues.JsonBehaviorMaster.User4Stars || strikes != ReferenceValues.JsonBehaviorMaster.User4Strikes) {
                    UpdateDatabase(4, ReferenceValues.JsonBehaviorMaster.User4Stars, ReferenceValues.JsonBehaviorMaster.User4Strikes);
                }

                User4Visibility = "VISIBLE";
                RefreshTasks(4);
                RefreshBlinking();
                break;
            case "user5":
                ReferenceValues.ActiveBehaviorUser = 5;
                User5Visibility = "HIDDEN";

                // Check to see if EditBehavior changed them
                stars = ReferenceValues.JsonBehaviorMaster.User5Stars;
                strikes = ReferenceValues.JsonBehaviorMaster.User5Strikes;

                EditBehavior editBehavior5 = new();
                editBehavior5.ShowDialog();
                editBehavior5.Close();

                if (stars != ReferenceValues.JsonBehaviorMaster.User5Stars || strikes != ReferenceValues.JsonBehaviorMaster.User5Strikes) {
                    UpdateDatabase(5, ReferenceValues.JsonBehaviorMaster.User5Stars, ReferenceValues.JsonBehaviorMaster.User5Strikes);
                }

                User5Visibility = "VISIBLE";
                RefreshTasks(5);
                RefreshBlinking();
                break;
            }

            RefreshBehavior();
        } else {
            SoundDispatcher.PlaySound("locked");
        }
    }

    private static void UpdateDatabase(int id, int stars, int strikes) {
        using MySqlConnection connection = new(ReferenceValues.DatabaseConnectionString);
        const string query = "UPDATE behavior SET stars = @stars, strikes = @strikes WHERE id = @id";

        try {
            connection.Open();

            using MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@stars", stars);
            command.Parameters.AddWithValue("@strikes", strikes);
            command.ExecuteNonQuery();

            connection.Close();
        } catch (Exception) {
            //todo: this
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

    public string User1Visibility {
        get => _user1Visibility;
        set {
            _user1Visibility = value;
            RaisePropertyChangedEvent("User1Visibility");
        }
    }

    public string User2Visibility {
        get => _user2Visibility;
        set {
            _user2Visibility = value;
            RaisePropertyChangedEvent("User2Visibility");
        }
    }

    public string User3Visibility {
        get => _user3Visibility;
        set {
            _user3Visibility = value;
            RaisePropertyChangedEvent("User3Visibility");
        }
    }

    public string User4Visibility {
        get => _user4Visibility;
        set {
            _user4Visibility = value;
            RaisePropertyChangedEvent("User4Visibility");
        }
    }

    public string User5Visibility {
        get => _user5Visibility;
        set {
            _user5Visibility = value;
            RaisePropertyChangedEvent("User5Visibility");
        }
    }

    #endregion
}