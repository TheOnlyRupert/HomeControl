﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Finances;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class FinancesVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;
    private string _cashIncomeText, _cashExpenseText, _cashAvailableText, _cashAvailableTextColor;
    private int expense, income, available;

    public FinancesVM() {
        try {
            ReferenceValues.JsonFinanceMaster = JsonSerializer.Deserialize<JsonFinances>(FileHelpers.LoadFileText("finances", true));
        } catch (Exception) {
            ReferenceValues.JsonFinanceMaster = new JsonFinances {
                financeList = new ObservableCollection<FinanceBlock>()
            };

            FileHelpers.SaveFileText("finances", JsonSerializer.Serialize(ReferenceValues.JsonFinanceMaster), true);
        }

        CashIncomeText = "";
        CashExpenseText = "";
        CashAvailableText = "";
        expense = 0;
        income = 0;
        available = 0;
        CashAvailableTextColor = "CornflowerBlue";
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
        switch (e.PropertyName) {
        case "RefreshFinances":
            RefreshFinances();
            break;
        case "DateChanged":
            BackupFinances();
            break;
        }
    }

    private void RefreshFinances() {
        expense = 0;
        income = 0;

        /* Calculate income, expense, and available cash */
        try {
            foreach (FinanceBlock t in ReferenceValues.JsonFinanceMaster.financeList) {
                if (t.AddSub == "SUB") {
                    try {
                        expense += int.Parse(t.Cost);
                    } catch (Exception e) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "WARN",
                            Module = "FinancesVM",
                            Description = e.ToString()
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                    }
                }
            }

            foreach (FinanceBlock t in ReferenceValues.JsonFinanceMaster.financeList) {
                if (t.AddSub == "ADD") {
                    try {
                        if (t.Category is not ("User1 Fund" or "User2 Fund" or "User3 Fund" or "User4 Fund" or "User5 Fund")) {
                            income += int.Parse(t.Cost);
                        }
                    } catch (Exception e) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "WARN",
                            Module = "FinancesVM",
                            Description = e.ToString()
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                    }
                }
            }
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "FinancesVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        available = income - expense;

        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        culture.NumberFormat.CurrencyNegativePattern = 1;
        CashExpenseText = string.Format(culture, "{0:C}", expense);
        CashIncomeText = string.Format(culture, "{0:C}", income);
        CashAvailableText = string.Format(culture, "{0:C}", available);

        CashAvailableTextColor = CashAvailableText.StartsWith("-") ? "Red" : "CornflowerBlue";
    }

    private static void BackupFinances() {
        Directory.CreateDirectory(ReferenceValues.DOCUMENTS_DIRECTORY + "backups/");

        try {
            FileHelpers.SaveFileText("backups/finance_backup_" + DateTime.Now.ToString("yyyy_MM_dd"), JsonSerializer.Serialize(ReferenceValues.JsonFinanceMaster), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "FinancesVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
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