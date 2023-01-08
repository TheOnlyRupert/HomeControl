using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresStatsMonth {
    public ChoresStatsMonth() {
        InitializeComponent();
        DataContext = new ChoresStatsMonthVM();
    }
}