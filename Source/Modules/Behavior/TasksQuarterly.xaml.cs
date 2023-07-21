using HomeControl.Source.ViewModel.Behavior;

namespace HomeControl.Source.Modules.Behavior;

public partial class TasksQuarterly {
    public TasksQuarterly() {
        InitializeComponent();
        DataContext = new TasksQuarterlyVM();
    }
}