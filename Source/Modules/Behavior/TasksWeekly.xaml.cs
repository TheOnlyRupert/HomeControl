using HomeControl.Source.ViewModel.Behavior;

namespace HomeControl.Source.Modules.Behavior;

public partial class TasksWeekly {
    public TasksWeekly() {
        InitializeComponent();
        DataContext = new TasksWeeklyVM();
    }
}