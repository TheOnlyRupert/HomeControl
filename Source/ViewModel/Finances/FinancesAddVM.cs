using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances {
    public class FinancesAddVM : BaseViewModel {
        private readonly CrossViewMessenger _crossViewMessenger;
        private ObservableCollection<string> _categoryList;
        private string _categorySelected, _descriptionText, _amountText, _dateText, _switchModeButtonText, _switchModeButtonColor;
        private bool isExpense;

        public FinancesAddVM() {
            _crossViewMessenger = CrossViewMessenger.Instance;
            DescriptionText = AmountText = "";
            DateTime dateTime = DateTime.Now;

            DateText = dateTime.ToShortDateString();

            SwitchModeButtonText = "Current Mode:\nAdd Expense";
            isExpense = true;

            /* Populate drop down box with spending categories and set default */
            CategoryList = new ObservableCollection<string>();
            foreach (string VARIABLE in ReferenceValues.CategorySpendingList) {
                CategoryList.Add(VARIABLE);
            }

            if (string.IsNullOrWhiteSpace(CategorySelected)) {
                CategorySelected = "Billing";
            }
        }

        public static Action CloseAction { get; set; }

        public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

        private void ButtonCommandLogic(object param) {
            switch (param) {
            case "save":
                string missingItemsText = "";
                bool isMissingItems = false;
                string expenseType;

                expenseType = isExpense ? "SUB" : "ADD";

                if (string.IsNullOrEmpty(DescriptionText)) {
                    missingItemsText += "Description, ";
                    isMissingItems = true;
                }

                if (string.IsNullOrEmpty(AmountText)) {
                    missingItemsText += "Amount, ";
                    isMissingItems = true;
                }

                if (string.IsNullOrEmpty(DateText)) {
                    missingItemsText += "Date, ";
                    isMissingItems = true;
                }

                /* Create missing items message */
                if (isMissingItems) {
                    /* Remove ending comma */
                    missingItemsText = missingItemsText.Substring(0, missingItemsText.Length - 2).Trim();
                    MessageBox.Show(missingItemsText, "Missing Required Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else {
                    CsvParser.AddFiance(expenseType + ',' + DateText.Trim() + ',' + DescriptionText.Trim() + ',' + AmountText.Trim() + ',' + CategorySelected + ',' + "Person");
                    _crossViewMessenger.PushMessage("RefreshFinances", null);
                    CloseAction();
                }

                break;
            case "switchMode":
                if (isExpense) {
                    SwitchModeButtonText = "Current Mode:\nAdd Income";
                    SwitchModeButtonColor = "Green";
                    isExpense = false;
                } else {
                    SwitchModeButtonText = "Current Mode:\nAdd Expense";
                    SwitchModeButtonColor = "Red";
                    isExpense = true;
                }

                break;
            }
        }

        #region Fields

        public string DescriptionText {
            get => _descriptionText;
            set {
                _descriptionText = value;
                RaisePropertyChangedEvent("DescriptionText");
            }
        }

        public string DateText {
            get => _dateText;
            set {
                _dateText = value;
                RaisePropertyChangedEvent("DateText");
            }
        }

        public string AmountText {
            get => _amountText;
            set {
                _amountText = value;
                RaisePropertyChangedEvent("AmountText");
            }
        }

        public string SwitchModeButtonText {
            get => _switchModeButtonText;
            set {
                _switchModeButtonText = value;
                RaisePropertyChangedEvent("SwitchModeButtonText");
            }
        }

        public ObservableCollection<string> CategoryList {
            get => _categoryList;
            set {
                _categoryList = value;
                RaisePropertyChangedEvent("CategoryList");
            }
        }

        public string CategorySelected {
            get => _categorySelected;
            set {
                _categorySelected = value;
                RaisePropertyChangedEvent("CategorySelected");
            }
        }

        public string SwitchModeButtonColor {
            get => _switchModeButtonColor;
            set {
                _switchModeButtonColor = value;
                RaisePropertyChangedEvent("SwitchModeButtonColor");
            }
        }

        #endregion
    }
}