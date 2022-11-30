using HomeControl.Source.ViewModel.Finances;

namespace HomeControl.Source.Modules.Finances;

public partial class EditFinances {
    public EditFinances() {
        InitializeComponent();
        DataContext = new EditFinancesVM();
    }
}