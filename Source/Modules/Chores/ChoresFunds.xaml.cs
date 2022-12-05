using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresFunds {
    public ChoresFunds() {
        InitializeComponent();
        DataContext = new ChoresFundsVM();
    }
}