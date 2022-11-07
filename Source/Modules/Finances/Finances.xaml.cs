using HomeControl.Source.ViewModel.Finances;

namespace HomeControl.Source.Modules.Finances; 

public partial class Finances {
    public Finances() {
        InitializeComponent();
        DataContext = new FinancesVM();
    }
}