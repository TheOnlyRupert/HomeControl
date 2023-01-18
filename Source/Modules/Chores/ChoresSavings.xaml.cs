using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresSavings {
    public ChoresSavings() {
        InitializeComponent();
        DataContext = new ChoresSavingsVM();
    }
}