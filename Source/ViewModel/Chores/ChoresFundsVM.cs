using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Chores;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresFundsVM : BaseViewModel {
    private string _currentMonth, _complianceDay, _complianceWeek, _complianceMonth, _complianceYear, _special1, _special2, _special3, _fundsPrior, _fundsTotal,
        _fundsReturnRate, _cashRemaining, _descriptionText, _costText, _specialDay1Color, _specialDay2Color, _dateText, _cashAvailableTextColor, _detailsText;

    private ObservableCollection<FinanceBlockChoreFund> _financeList;
    private FinanceBlockChoreFund _financeSelected;

    public ChoresFundsVM() {
        CurrentMonth = DateTime.Now.ToString("MMMM");
        DateText = DateTime.Now.ToShortDateString();

        FinanceList = ReferenceValues.JsonChoreFundsMaster.FinanceBlockChoreFundList;
        SpecialDay1Color = ReferenceValues.JsonChoreFundsMaster.SpecialDay1Completed ? "Green" : "Transparent";
        SpecialDay2Color = ReferenceValues.JsonChoreFundsMaster.SpecialDay2Completed ? "Green" : "Transparent";

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

        FundsTotal = "This Month: $" + ReferenceValues.JsonChoreFundsMaster.FundsTotal;
        FundsReturnRate = "Return Rate: 1x";

        int cash = 0;

        foreach (FinanceBlockChoreFund financeBlockShort in ReferenceValues.JsonChoreFundsMaster.FinanceBlockChoreFundList) {
            cash += financeBlockShort.Cost;
        }

        ReferenceValues.JsonChoreFundsMaster.FundsAvailable = ReferenceValues.JsonChoreFundsMaster.FundsTotal;
        ReferenceValues.JsonChoreFundsMaster.FundsAvailable -= cash;

        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        culture.NumberFormat.CurrencyNegativePattern = 1;
        CashRemaining = string.Format(culture, "{0:C}", ReferenceValues.JsonChoreFundsMaster.FundsAvailable);

        CashAvailableTextColor = CashRemaining.StartsWith("-") ? "Red" : "CornflowerBlue";

        SaveJson();
    }

    private void ButtonLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(DescriptionText)) {
                MediaPlayer sound = new();
                sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/missing_info.wav"));
                sound.Play();
            } else if (string.IsNullOrWhiteSpace(CostText)) {
                MediaPlayer sound = new();
                sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/missing_info.wav"));
                sound.Play();
            } else {
                FinanceList.Add(new FinanceBlockChoreFund {
                    Item = DescriptionText,
                    Cost = int.Parse(CostText),
                    Date = DateTime.Parse(DateText).ToShortDateString(),
                    Details = DetailsText
                });

                MediaPlayer sound = new();
                sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/cash.wav"));
                sound.Play();
                DescriptionText = "";
                DetailsText = "";
                CostText = "";
                Refresh();
            }

            break;
        case "update":
            try {
                if (FinanceSelected.Item != null) {
                    if (string.IsNullOrWhiteSpace(DescriptionText)) {
                        MediaPlayer sound = new();
                        sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/missing_info.wav"));
                        sound.Play();
                    } else if (string.IsNullOrWhiteSpace(CostText)) {
                        MediaPlayer sound = new();
                        sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/missing_info.wav"));
                        sound.Play();
                    } else {
                        confirmation = MessageBox.Show("Are you sure you want to update charge?", "Confirmation", MessageBoxButton.YesNo);
                        if (confirmation == MessageBoxResult.Yes) {
                            FinanceList.Insert(FinanceList.IndexOf(FinanceSelected), new FinanceBlockChoreFund {
                                Item = DescriptionText,
                                Cost = int.Parse(CostText),
                                Date = DateTime.Parse(DateText).ToShortDateString(),
                                Details = DetailsText
                            });

                            MediaPlayer sound = new();
                            sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/cash.wav"));
                            sound.Play();
                            FinanceList.Remove(FinanceSelected);
                            DescriptionText = "";
                            DetailsText = "";
                            CostText = "";
                            Refresh();
                        }
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFundsVM",
                    Description = e.ToString()
                });
            }

            break;
        case "delete":
            try {
                if (FinanceSelected.Item != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete charge?", "Confirmation", MessageBoxButton.YesNo);
                    if (confirmation == MessageBoxResult.Yes) {
                        MediaPlayer sound = new();
                        sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/cash.wav"));
                        sound.Play();
                        FinanceList.Remove(FinanceSelected);
                        Refresh();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFundsVM",
                    Description = e.ToString()
                });
            }

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
        case "specialDay1":
            if (SpecialDay1Color == "Transparent") {
                SpecialDay1Color = "Green";
                ReferenceValues.JsonChoreFundsMaster.SpecialDay1Completed = true;
            } else {
                confirmation = MessageBox.Show("Are you sure you want to reset special day?", "Confirmation", MessageBoxButton.YesNo);
                if (confirmation == MessageBoxResult.Yes) {
                    SpecialDay1Color = "Transparent";
                    ReferenceValues.JsonChoreFundsMaster.SpecialDay1Completed = false;
                }
            }

            Refresh();

            break;
        case "specialDay2":
            if (SpecialDay2Color == "Transparent") {
                SpecialDay2Color = "Green";
                ReferenceValues.JsonChoreFundsMaster.SpecialDay2Completed = true;
            } else {
                confirmation = MessageBox.Show("Are you sure you want to reset special day?", "Confirmation", MessageBoxButton.YesNo);
                if (confirmation == MessageBoxResult.Yes) {
                    SpecialDay2Color = "Transparent";
                    ReferenceValues.JsonChoreFundsMaster.SpecialDay2Completed = false;
                }
            }

            Refresh();

            break;
        case "special":
            Refresh();

            break;
        case "savings":
            ChoresSavings choresSavings = new();
            choresSavings.ShowDialog();
            choresSavings.Close();
            Refresh();

            break;
        case "addDay":
            try {
                DateText = Convert.ToDateTime(DateText).AddDays(1).ToShortDateString();
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFundsVM",
                    Description = e.ToString()
                });
            }

            break;
        }
    }

    private void SaveJson() {
        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreFundsMaster);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "chorefunds.json", jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "ChoresFundsVM",
                Description = e.ToString()
            });
        }
    }

    private void PopulateDetailedView(FinanceBlockChoreFund value) {
        DescriptionText = value.Item;
        CostText = value.Cost.ToString();
        DateText = value.Date;
        DetailsText = value.Details;
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

    public string DetailsText {
        get => _detailsText;
        set {
            _detailsText = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("DetailsText");
        }
    }

    public string DateText {
        get => _dateText;
        set {
            if (string.IsNullOrWhiteSpace(value)) {
                value = DateTime.Now.ToShortDateString();
            }

            _dateText = value;
            RaisePropertyChangedEvent("DateText");
        }
    }

    public string SpecialDay1Color {
        get => _specialDay1Color;
        set {
            _specialDay1Color = value;
            RaisePropertyChangedEvent("SpecialDay1Color");
        }
    }

    public string SpecialDay2Color {
        get => _specialDay2Color;
        set {
            _specialDay2Color = value;
            RaisePropertyChangedEvent("SpecialDay2Color");
        }
    }

    public string CostText {
        get => _costText;
        set {
            _costText = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("CostText");
        }
    }

    public ObservableCollection<FinanceBlockChoreFund> FinanceList {
        get => _financeList;
        set {
            _financeList = value;
            RaisePropertyChangedEvent("FinanceList");
        }
    }

    public FinanceBlockChoreFund FinanceSelected {
        get => _financeSelected;
        set {
            _financeSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("FinanceSelected");
        }
    }

    public string CashAvailableTextColor {
        get => _cashAvailableTextColor;
        set {
            _cashAvailableTextColor = value;
            RaisePropertyChangedEvent("CashAvailableTextColor");
        }
    }

    #endregion
}