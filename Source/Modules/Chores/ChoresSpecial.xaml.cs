using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresSpecial {
    public ChoresSpecial() {
        InitializeComponent();
        DataContext = new ChoresSpecialVM();
    }
}