using HomeControl.Source.ViewModel.Finances;

namespace HomeControl.Source.Modules.Finances;

public partial class FinancesAdd {
    public FinancesAdd() {
        InitializeComponent();
        DataContext = new FinancesAddVM();
        if (FinancesAddVM.CloseAction == null) {
            FinancesAddVM.CloseAction = Close;
        }
        //Loaded += (s, e) => {
        //    Window.GetWindow(this).Closing += (s1, e1) => HERE;
        //};
    }
}