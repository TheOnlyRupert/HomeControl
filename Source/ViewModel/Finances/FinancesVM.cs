using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Finances;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class FinancesVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;
    private string _cashIncomeText, _cashExpenseText, _cashAvailableText, _cashAvailableTextColor;
    private int expense, income, available;

    public FinancesVM() {
        CashIncomeText = "";
        CashExpenseText = "";
        CashAvailableText = "";
        expense = 0;
        income = 0;
        available = 0;
        CashAvailableTextColor = "CornflowerBlue";
        new FinancesFromJson();
        RefreshFinances();
        BackupFinances();

        simpleMessenger = CrossViewMessenger.Instance;
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

                simpleMessenger.PushMessage("RefreshFinances", null);
                break;
            }
        } else {
            ReferenceValues.SoundToPlay = "locked";
            SoundDispatcher.PlaySound();
        }
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "RefreshFinances") {
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
            foreach (FinanceBlock t in ReferenceValues.JsonFinanceMasterList.financeList) {
                if (t.AddSub == "SUB") {
                    try {
                        expense += int.Parse(t.Cost);
                    } catch (Exception e) {
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "WARN",
                            Module = "FinancesVM",
                            Description = e.ToString()
                        });
                        SaveDebugFile.Save();
                    }
                }
            }

            foreach (FinanceBlock t in ReferenceValues.JsonFinanceMasterList.financeList) {
                if (t.AddSub == "ADD") {
                    try {
                        if (t.Category is not ("User1 Fund" or "User2 Fund" or "User3 Fund" or "User4 Fund" or "User5 Fund")) {
                            income += int.Parse(t.Cost);
                        }
                    } catch (Exception e) {
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "WARN",
                            Module = "FinancesVM",
                            Description = e.ToString()
                        });
                        SaveDebugFile.Save();
                    }
                }
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "FinancesVM",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }

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
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "FinancesVM",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
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