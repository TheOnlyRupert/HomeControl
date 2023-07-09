using HomeControl.Source.ViewModel.Behavior;

namespace HomeControl.Source.Modules.Behavior;

public partial class TasksDaily {
    public TasksDaily() {
        InitializeComponent();
        DataContext = new TasksDailyVM();
    }
}