using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresSpecialUser1 {
    public ChoresSpecialUser1() {
        InitializeComponent();
        DataContext = new ChoresSpecialUser1VM();
    }
}