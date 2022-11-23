﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Finances;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class FinancesVM : BaseViewModel {
    private string _cashIncomeText, _cashExpenseText, _cashAvailableText, _cashAvailableTextColor;
    private ObservableCollection<FinanceBlock> _financeList;
    private int expense, income, available;

    public FinancesVM() {
        CashIncomeText = "";
        CashExpenseText = "";
        CashAvailableText = "";
        expense = 0;
        income = 0;
        available = 0;
        CashAvailableTextColor = "CornflowerBlue";
        FinanceList = new ObservableCollection<FinanceBlock>();
        new FinancesFromJson();
        RefreshFinances();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "edit":
            ReferenceValues.JsonFinanceMasterList.financeList = FinanceList;
            FinancesAdd financesAdd = new();
            financesAdd.ShowDialog();
            financesAdd.Close();
            RefreshFinances();
            break;
        }
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "RefreshFinances") {
            RefreshFinances();
        }
    }

    private void RefreshFinances() {
        FinanceList = ReferenceValues.JsonFinanceMasterList.financeList;
        expense = 0;
        income = 0;

        /* Calculate income, expense, and available cash */
        try {
            for (int i = 0; i < FinanceList.Count; i++) {
                if (FinanceList[i].AddSub == "SUB") {
                    try {
                        expense += FinanceList[i].Cost;
                    } catch (Exception) { }
                }
            }

            for (int i = 0; i < FinanceList.Count; i++) {
                if (FinanceList[i].AddSub == "ADD") {
                    try {
                        income += FinanceList[i].Cost;
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

    public ObservableCollection<FinanceBlock> FinanceList {
        get => _financeList;
        set {
            _financeList = value;
            RaisePropertyChangedEvent("FinanceList");
        }
    }

    #endregion
}