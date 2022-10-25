using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Finances;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances {
    public class FinancesVM : BaseViewModel {
        private string _cashIncome, _cashExpenses, _cashAvailable;
        private ObservableCollection<FinanceBlock> _financeList = new ObservableCollection<FinanceBlock>();
        private FinanceBlock _financeSelected = new FinanceBlock();

        public FinancesVM() {
            CashIncome = "Income";
            CashExpenses = "Expenses";
            CashAvailable = "Available";

            /* CSV to list */
            FinanceList = CsvParser.GetFinanceList();

            CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
            simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
        }

        public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

        private void ButtonCommandLogic(object param) {
            switch (param) {
            case "add":
                FinancesAdd financesAdd = new FinancesAdd();
                financesAdd.ShowDialog();
                financesAdd.Close();
                break;
            case "edit":
                break;
            }
        }

        private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
            if (e.PropertyName == "RefreshFinances") {
                /* Refresh list */
                FinanceList.Clear();
                FinanceList = CsvParser.GetFinanceList();
            }
        }

        #region Fields

        public string CashIncome {
            get => _cashIncome;
            set {
                _cashIncome = value;
                RaisePropertyChangedEvent("CashIncome");
            }
        }

        public string CashExpenses {
            get => _cashExpenses;
            set {
                _cashExpenses = value;
                RaisePropertyChangedEvent("CashExpenses");
            }
        }

        public string CashAvailable {
            get => _cashAvailable;
            set {
                _cashAvailable = value;
                RaisePropertyChangedEvent("CashAvailable");
            }
        }

        public ObservableCollection<FinanceBlock> FinanceList {
            get => _financeList;
            set {
                _financeList = value;
                RaisePropertyChangedEvent("FinanceList");
            }
        }

        public FinanceBlock FinanceSelected {
            get => _financeSelected;
            set {
                _financeSelected = value;
                RaisePropertyChangedEvent("FinanceSelected");
            }
        }

        #endregion
    }
}