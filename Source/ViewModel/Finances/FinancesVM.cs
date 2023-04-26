using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Control;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Finances;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class FinancesVM : BaseViewModel {
    private readonly PlaySound uiLocked;
    private string _cashIncomeText, _cashExpenseText, _cashAvailableText, _cashAvailableTextColor;
    private int expense, income, available;

    public FinancesVM() {
        uiLocked = new PlaySound("locked");
        CashIncomeText = "";
        CashExpenseText = "";
        CashAvailableText = "";
        expense = 0;
        income = 0;
        available = 0;
        CashAvailableTextColor = "CornflowerBlue";
        FinancesFromJson financesFromJson = new();
        financesFromJson.FinancesFromJsonMain();
        RefreshFinances();
        BackupFinances();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        if (!ReferenceValues.LockUI) {
            switch (param) {
            case "edit":
                EditFinances editFinances = new();
                editFinances.ShowDialog();
                editFinances.Close();
                RefreshFinances();
                break;
            }
        } else {
            uiLocked.Play(false);
        }
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "RefreshFundAmount") {
            RefreshFinances();
        }

        if (e.PropertyName == "DateChanged") {
            BackupFinances();
        }
    }

    private void RefreshFinances() {
        expense = 0;
        income = 0;

        /* Calculate income, expense, and available cash */
        try {
            for (int i = 0; i < ReferenceValues.JsonFinanceMasterList.financeList.Count; i++) {
                if (ReferenceValues.JsonFinanceMasterList.financeList[i].AddSub == "SUB") {
                    try {
                        expense += int.Parse(ReferenceValues.JsonFinanceMasterList.financeList[i].Cost);
                    } catch (Exception) { }
                }
            }

            for (int i = 0; i < ReferenceValues.JsonFinanceMasterList.financeList.Count; i++) {
                if (ReferenceValues.JsonFinanceMasterList.financeList[i].AddSub == "ADD") {
                    try {
                        income += int.Parse(ReferenceValues.JsonFinanceMasterList.financeList[i].Cost);
                    } catch (Exception) { }
                }
            }
        } catch (Exception) { }

        available = income - expense;

        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        culture.NumberFormat.CurrencyNegativePattern = 1;
        CashExpenseText = string.Format(culture, "{0:C}", expense);
        CashIncomeText = string.Format(culture, "{0:C}", income);
        CashAvailableText = string.Format(culture, "{0:C}", available);

        CashAvailableTextColor = CashAvailableText.StartsWith("-") ? "Red" : "CornflowerBlue";
    }

    private void BackupFinances() {
        Directory.CreateDirectory(ReferenceValues.FILE_DIRECTORY + "finances_backup/");
        string fileName = ReferenceValues.FILE_DIRECTORY + "finances_backup/finances_" + DateTime.Now.ToString("yyyy_MM_dd") + ".json";

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonFinanceMasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
        }
    }

    #region Fields

    public string CashIncomeText {
        get => _cashIncomeText;
        set {
            _cashIncomeText = value;
            RaisePropertyChangedEvent("CashIncomeText");
        }
    }

    public string CashExpenseText {
        get => _cashExpenseText;
        set {
            _cashExpenseText = value;
            RaisePropertyChangedEvent("CashExpenseText");
        }
    }

    public string CashAvailableText {
        get => _cashAvailableText;
        set {
            _cashAvailableText = value;
            RaisePropertyChangedEvent("CashAvailableText");
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