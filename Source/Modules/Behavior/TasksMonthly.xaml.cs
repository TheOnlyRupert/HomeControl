using HomeControl.Source.ViewModel.Behavior;

namespace HomeControl.Source.Modules.Behavior;

public partial class TasksMonthly {
    public TasksMonthly() {
        InitializeComponent();
        DataContext = new TasksMonthlyVM();
    }
}