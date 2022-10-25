using HomeControl.Source.ViewModel.Finances;

namespace HomeControl.Source.Modules.Finances {
    public partial class FinancesAdd {
        public FinancesAdd() {
            InitializeComponent();
            DataContext = new FinancesAddVM();
            if (FinancesAddVM.CloseAction == null) {
                FinancesAddVM.CloseAction = Close;
            }
        }
    }
}