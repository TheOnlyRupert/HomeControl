using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresStatsDay {
    public ChoresStatsDay() {
        InitializeComponent();
        DataContext = new ChoresStatsDayVM();
    }
}