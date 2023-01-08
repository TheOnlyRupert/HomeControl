using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresStatsWeek {
    public ChoresStatsWeek() {
        InitializeComponent();
        DataContext = new ChoresStatsWeekVM();
    }
}