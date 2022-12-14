using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Chores;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresFundsVM : BaseViewModel {
    private readonly string fileName;

    private string _currentMonth, _complianceDay, _complianceWeek, _complianceMonth, _complianceYear, _special1, _special2, _special3, _fundsPrior, _fundsTotal,
        _fundsReturnRate, _cashRemaining, _descriptionText, _costText;

    private ObservableCollection<FinanceBlockShort> _financeList;
    private FinanceBlockShort _financeSelected;

    public ChoresFundsVM() {
        fileName = ReferenceValues.FILE_DIRECTORY + "financesChoreFund.json";

        FinanceList = new ObservableCollection<FinanceBlockShort>();
        try {
            FinanceList = ReferenceValues.JsonFinanceShortMasterList.financeListShort;
        } catch (Exception) {
            ReferenceValues.JsonFinanceShortMasterList = new JsonFinancesShort {
                financeListShort = new ObservableCollection<FinanceBlockShort>()
            };
        }

        CurrentMonth = DateTime.Now.ToString("MMMM");

        Refresh();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void Refresh() {
        ComplianceDay = "Daily: 0%";
        ComplianceWeek = "Weekly: 0%";
        ComplianceMonth = "Monthly: 0%";
        ComplianceYear = "Yearly: 0%";

        Special1 = "BFF: 0";
        Special2 = "Any: 0";
        Special3 = "New: 0";

        FundsPrior = "Prior Month: $" + ReferenceValues.JsonChoreFundsMaster.FundsPrior;
        FundsTotal = "This Month: $" + ReferenceValues.JsonChoreFundsMaster.FundsTotal;
        FundsReturnRate = "Return Rate: 1x";

        int cash = 0;
        foreach (FinanceBlockShort financeBlockShort in ReferenceValues.JsonFinanceShortMasterList.financeListShort) {
            cash += int.Parse(financeBlockShort.Cost);
        }

        ReferenceValues.JsonChoreFundsMaster.FundsAvailable = ReferenceValues.JsonChoreFundsMaster.FundsTotal;
        ReferenceValues.JsonChoreFundsMaster.FundsAvailable -= cash;

        CashRemaining = "Cash Remaining: $" + ReferenceValues.JsonChoreFundsMaster.FundsAvailable;

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreFundsMaster);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "chorefunds.json", jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save chorefunds.json... " + e.Message);
        }
    }

    private void ButtonLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(DescriptionText)) {
                MessageBox.Show("Missing Description", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else if (string.IsNullOrWhiteSpace(CostText)) {
                MessageBox.Show("Missing Cost", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                FinanceList.Add(new FinanceBlockShort {
                    Item = DescriptionText,
                    Cost = CostText
                });

                DescriptionText = "";
                CostText = "";
                SaveJson();
                Refresh();
            }

            break;
        case "update":
            try {
                if (FinanceSelected.Item != null) {
                    if (string.IsNullOrWhiteSpace(DescriptionText)) {
                        MessageBox.Show("Missing Description", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                    } else if (string.IsNullOrWhiteSpace(CostText)) {
                        MessageBox.Show("Missing Cost", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                    } else {
                        confirmation = MessageBox.Show("Are you sure you want to update charge?", "Confirmation", MessageBoxButton.YesNo);
                        if (confirmation == MessageBoxResult.Yes) {
                            FinanceList.Insert(FinanceList.IndexOf(FinanceSelected), new FinanceBlockShort {
                                Item = DescriptionText,
                                Cost = CostText
                            });

                            FinanceList.Remove(FinanceSelected);
                            DescriptionText = "";
                            CostText = "";
                            Refresh();
                            SaveJson();
                        }
                    }
                }
            } catch (Exception) { }

            break;
        case "delete":
            try {
                if (FinanceSelected.Item != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete charge?", "Confirmation", MessageBoxButton.YesNo);
                    if (confirmation == MessageBoxResult.Yes) {
                        FinanceList.Remove(FinanceSelected);
                        SaveJson();
                        Refresh();
                    }
                }
            } catch (Exception) { }

            break;

        case "logsDay":
            ChoresStatsDay choresStatsDay = new();
            choresStatsDay.ShowDialog();
            choresStatsDay.Close();

            break;
        case "logsWeek":
            ChoresStatsWeek choresStatsWeek = new();
            choresStatsWeek.ShowDialog();
            choresStatsWeek.Close();

            break;
        case "logsMonth":
            ChoresStatsMonth choresStatsMonth = new();
            choresStatsMonth.ShowDialog();
            choresStatsMonth.Close();

            break;
        case "logsSpecial":
            ChoresStatsSpecial choresStatsSpecial = new();
            choresStatsSpecial.ShowDialog();
            choresStatsSpecial.Close();

            break;
        }
    }

    private void SaveJson() {
        if (FinanceList.Count > 0) {
            try {
                ReferenceValues.JsonFinanceShortMasterList.financeListShort = FinanceList;
            } catch (Exception) {
                Console.WriteLine("ReferenceValues Doesnt exist");
            }

            try {
                string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonFinanceShortMasterList);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save financesChoreFund.json... " + e.Message);
            }
        } else {
            try {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(fileName);
            } catch (Exception e) {
                Console.WriteLine("Unable to delete financesChoreFund.json... " + e.Message);
            }
        }
    }

    private void PopulateDetailedView(FinanceBlockShort value) {
        DescriptionText = value.Item;
        CostText = value.Cost;
    }

    #region Fields

    public string CurrentMonth {
        get => _currentMonth;
        set {
            _currentMonth = value;
            RaisePropertyChangedEvent("CurrentMonth");
        }
    }

    public string ComplianceDay {
        get => _complianceDay;
        set {
            _complianceDay = value;
            RaisePropertyChangedEvent("ComplianceDay");
        }
    }

    public string ComplianceWeek {
        get => _complianceWeek;
        set {
            _complianceWeek = value;
            RaisePropertyChangedEvent("ComplianceWeek");
        }
    }

    public string ComplianceMonth {
        get => _complianceMonth;
        set {
            _complianceMonth = value;
            RaisePropertyChangedEvent("ComplianceMonth");
        }
    }

    public string ComplianceYear {
        get => _complianceYear;
        set {
            _complianceYear = value;
            RaisePropertyChangedEvent("ComplianceYear");
        }
    }

    public string Special1 {
        get => _special1;
        set {
            _special1 = value;
            RaisePropertyChangedEvent("Special1");
        }
    }

    public string Special2 {
        get => _special2;
        set {
            _special2 = value;
            RaisePropertyChangedEvent("Special2");
        }
    }

    public string Special3 {
        get => _special3;
        set {
            _special3 = value;
            RaisePropertyChangedEvent("Special3");
        }
    }

    public string FundsPrior {
        get => _fundsPrior;
        set {
            _fundsPrior = value;
            RaisePropertyChangedEvent("FundsPrior");
        }
    }

    public string FundsTotal {
        get => _fundsTotal;
        set {
            _fundsTotal = value;
            RaisePropertyChangedEvent("FundsTotal");
        }
    }

    public string FundsReturnRate {
        get => _fundsReturnRate;
        set {
            _fundsReturnRate = value;
            RaisePropertyChangedEvent("FundsReturnRate");
        }
    }

    public string CashRemaining {
        get => _cashRemaining;
        set {
            _cashRemaining = value;
            RaisePropertyChangedEvent("CashRemaining");
        }
    }

    public string DescriptionText {
        get => _descriptionText;
        set {
            _descriptionText = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("DescriptionText");
        }
    }

    public string CostText {
        get => _costText;
        set {
            _costText = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("CostText");
        }
    }

    public ObservableCollection<FinanceBlockShort> FinanceList {
        get => _financeList;
        set {
            _financeList = value;
            RaisePropertyChangedEvent("FinanceList");
        }
    }

    public FinanceBlockShort FinanceSelected {
        get => _financeSelected;
        set {
            _financeSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("FinanceSelected");
        }
    }

    #endregion
}