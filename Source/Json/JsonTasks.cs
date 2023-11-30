using System;
using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonTasks {
    public JsonTasksDaily JsonTasksDaily { get; set; }
    public JsonTasksWeekly JsonTasksWeekly { get; set; }
    public JsonTasksMonthly JsonTasksMonthly { get; set; }
    public JsonTasksQuarterly JsonTasksQuarterly { get; set; }
    public DateTime UpdatedDateTime { get; set; }
    public bool User1Blink { get; set; }
    public bool User2Blink { get; set; }
    public bool User3Blink { get; set; }
    public bool User4Blink { get; set; }
    public bool User5Blink { get; set; }
}

public class JsonTasksDaily {
    public ObservableCollection<Task> TaskListDailyUser1 { get; set; }
    public ObservableCollection<Task> TaskListDailyUser2 { get; set; }
    public ObservableCollection<Task> TaskListDailyUser3 { get; set; }
    public ObservableCollection<Task> TaskListDailyUser4 { get; set; }
    public ObservableCollection<Task> TaskListDailyUser5 { get; set; }
    public int FundsDailyUser1 { get; set; }
    public int FundsDailyUser2 { get; set; }
    public int FundsDailyUser3 { get; set; }
    public int FundsDailyUser4 { get; set; }
    public int FundsDailyUser5 { get; set; }
}

public class JsonTasksWeekly {
    public ObservableCollection<Task> TaskListWeeklyUser1 { get; set; }
    public ObservableCollection<Task> TaskListWeeklyUser2 { get; set; }
    public ObservableCollection<Task> TaskListWeeklyUser3 { get; set; }
    public ObservableCollection<Task> TaskListWeeklyUser4 { get; set; }
    public ObservableCollection<Task> TaskListWeeklyUser5 { get; set; }
    public int FundsWeeklyUser1 { get; set; }
    public int FundsWeeklyUser2 { get; set; }
    public int FundsWeeklyUser3 { get; set; }
    public int FundsWeeklyUser4 { get; set; }
    public int FundsWeeklyUser5 { get; set; }
}

public class JsonTasksMonthly {
    public ObservableCollection<Task> TaskListMonthlyUser1 { get; set; }
    public ObservableCollection<Task> TaskListMonthlyUser2 { get; set; }
    public ObservableCollection<Task> TaskListMonthlyUser3 { get; set; }
    public ObservableCollection<Task> TaskListMonthlyUser4 { get; set; }
    public ObservableCollection<Task> TaskListMonthlyUser5 { get; set; }
    public int FundsMonthlyUser1 { get; set; }
    public int FundsMonthlyUser2 { get; set; }
    public int FundsMonthlyUser3 { get; set; }
    public int FundsMonthlyUser4 { get; set; }
    public int FundsMonthlyUser5 { get; set; }
}

public class JsonTasksQuarterly {
    public ObservableCollection<Task> TaskListQuarterlyUser1 { get; set; }
    public ObservableCollection<Task> TaskListQuarterlyUser2 { get; set; }
    public ObservableCollection<Task> TaskListQuarterlyUser3 { get; set; }
    public ObservableCollection<Task> TaskListQuarterlyUser4 { get; set; }
    public ObservableCollection<Task> TaskListQuarterlyUser5 { get; set; }
    public int FundsQuarterlyUser1 { get; set; }
    public int FundsQuarterlyUser2 { get; set; }
    public int FundsQuarterlyUser3 { get; set; }
    public int FundsQuarterlyUser4 { get; set; }
    public int FundsQuarterlyUser5 { get; set; }
}

public class Task {
    public string TaskName { get; set; }
    public string ImageName { get; set; }
    public int RoomNumber { get; set; }
    public bool IsCompleted { get; set; }
    public string DateCompleted { get; set; }
    public int RequiredTime { get; set; }
}