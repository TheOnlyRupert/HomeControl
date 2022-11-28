using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class Chores {
    public Chores() {
        InitializeComponent();
        DataContext = new ChoresVM();
    }
}