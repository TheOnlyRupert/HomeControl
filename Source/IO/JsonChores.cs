using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonTasks {
    public JsonTasksDaily JsonTasksDaily { get; set; }
    public JsonTasksWeekly JsonTasksWeekly { get; set; }
    public JsonTasksMonthly JsonTasksMonthly { get; set; }
    public JsonTasksQuarterly JsonTasksQuarterly { get; set; }
}

public class JsonTasksDaily {
    public ObservableCollection<Task> TaskListDailyUser1 { get; set; }
    public ObservableCollection<Task> TaskListDailyUser2 { get; set; }
    public ObservableCollection<Task> TaskListDailyUser3 { get; set; }
    public ObservableCollection<Task> TaskListDailyUser4 { get; set; }
    public ObservableCollection<Task> TaskListDailyUser5 { get; set; }
}

public class JsonTasksWeekly {
    public ObservableCollection<Task> TaskListWeeklyUser1 { get; set; }
    public ObservableCollection<Task> TaskListWeeklyUser2 { get; set; }
    public ObservableCollection<Task> TaskListWeeklyUser3 { get; set; }
    public ObservableCollection<Task> TaskListWeeklyUser4 { get; set; }
    public ObservableCollection<Task> TaskListWeeklyUser5 { get; set; }
}

public class JsonTasksMonthly {
    public ObservableCollection<Task> TaskListMonthlyUser1 { get; set; }
    public ObservableCollection<Task> TaskListMonthlyUser2 { get; set; }
    public ObservableCollection<Task> TaskListMonthlyUser3 { get; set; }
    public ObservableCollection<Task> TaskListMonthlyUser4 { get; set; }
    public ObservableCollection<Task> TaskListMonthlyUser5 { get; set; }
}

public class JsonTasksQuarterly {
    public ObservableCollection<Task> TaskListQuarterlyUser1 { get; set; }
    public ObservableCollection<Task> TaskListQuarterlyUser2 { get; set; }
    public ObservableCollection<Task> TaskListQuarterlyUser3 { get; set; }
    public ObservableCollection<Task> TaskListQuarterlyUser4 { get; set; }
    public ObservableCollection<Task> TaskListQuarterlyUser5 { get; set; }
}

public class Task {
    public string TaskName { get; set; }
    public string ImageName { get; set; }
    public int RoomNumber { get; set; }
    public bool IsCompleted { get; set; }
    public string DateCompleted { get; set; }
}