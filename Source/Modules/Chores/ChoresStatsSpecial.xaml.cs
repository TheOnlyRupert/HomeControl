using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresStatsSpecial {
    public ChoresStatsSpecial() {
        InitializeComponent();
        DataContext = new ChoresStatsSpecialVM();
    }
}