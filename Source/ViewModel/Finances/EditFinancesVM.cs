using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Control;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class EditFinancesVM : BaseViewModel {
    private readonly PlaySound cashSound;
    private readonly string fileName;
    private ObservableCollection<string> _categoryList;

    private string _dateText, _switchModeButtonText, _switchModeButtonColor, _user1BackgroundColor, _user2BackgroundColor, _childrenBackgroundColor, _homeBackgroundColor,
        _otherBackgroundColor, _user1NameText, _user2NameText, AddOrSub, _costText;

    private ObservableCollection<FinanceBlock> _financeList;
    private FinanceBlock _financeSelected;
    private string selectedPerson, _categorySelected, _descriptionText;

    public EditFinancesVM() {
        fileName = ReferenceValues.FILE_DIRECTORY + "finances.json";
        cashSound = new PlaySound("cash");

        selectedPerson = "Home";
        User1NameText = ReferenceValues.JsonMasterSettings.User1Name;
        User2NameText = ReferenceValues.JsonMasterSettings.User2Name;
        DescriptionText = "";
        CostText = "";
        UserButtonLogic();
        FinanceList = new ObservableCollection<FinanceBlock>();
        try {
            FinanceList = ReferenceValues.JsonFinanceMasterList.financeList;
        } catch (Exception) {
            ReferenceValues.JsonFinanceMasterList = new JsonFinances {
                financeList = new ObservableCollection<FinanceBlock>()
            };
        }

        DateText = DateTime.Now.ToShortDateString();
        SwitchModeButtonText = "Expense  ☹";
        SwitchModeButtonColor = "Red";
        AddOrSub = "SUB";

        /* Populate drop down box with spending categories and set default */
        CategoryList = new ObservableCollection<string>();
        foreach (string VARIABLE in ReferenceValues.CategorySpendingList) {
            CategoryList.Add(VARIABLE);
        }

        CategorySelected = "Billing";
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(DescriptionText)) {
                MessageBox.Show("Missing Description", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else if (string.IsNullOrWhiteSpace(CostText)) {
                MessageBox.Show("Missing Cost", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                FinanceList.Add(new FinanceBlock {
                    AddSub = AddOrSub,
                    Date = DateTime.Parse(DateText).ToShortDateString(),
                    Item = DescriptionText,
                    Cost = CostText,
                    Category = CategorySelected,
                    Person = selectedPerson
                });

                cashSound.Play(false);
                DateText = DateTime.Now.ToShortDateString();
                DescriptionText = "";
                CostText = "";
                SaveJson();
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
                            FinanceList.Insert(FinanceList.IndexOf(FinanceSelected), new FinanceBlock {
                                AddSub = AddOrSub,
                                Date = DateTime.Parse(DateText).ToShortDateString(),
                                Item = DescriptionText,
                                Cost = CostText,
                                Category = CategorySelected,
                                Person = selectedPerson
                            });
                            FinanceList.Remove(FinanceSelected);
                            DateText = DateTime.Now.ToShortDateString();
                            DescriptionText = "";
                            CostText = "";
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
                    }
                }
            } catch (Exception) { }

            break;

        case "user1":
            selectedPerson = ReferenceValues.JsonMasterSettings.User1Name;
            UserButtonLogic();
            break;

        case "user2":
            selectedPerson = ReferenceValues.JsonMasterSettings.User2Name;
            UserButtonLogic();
            break;

        case "children":
            selectedPerson = "Children";
            UserButtonLogic();
            break;

        case "home":
            selectedPerson = "Home";
            UserButtonLogic();
            break;

        case "other":
            selectedPerson = "Other";
            UserButtonLogic();
            break;

        case "switchMode":
            if (AddOrSub == "SUB") {
                SwitchModeButtonText = "Income  ☺";
                SwitchModeButtonColor = "Green";
                AddOrSub = "ADD";

                CategoryList.Clear();
                foreach (string VARIABLE in ReferenceValues.CategoryProfitList) {
                    CategoryList.Add(VARIABLE);
                }

                CategorySelected = "Paycheck";
            } else {
                SwitchModeButtonText = "Expense  ☹";
                SwitchModeButtonColor = "Red";
                AddOrSub = "SUB";

                CategoryList.Clear();
                foreach (string VARIABLE in ReferenceValues.CategorySpendingList) {
                    CategoryList.Add(VARIABLE);
                }

                CategorySelected = "Billing";
            }

            break;
        }
    }

    private void PopulateDetailedView(FinanceBlock value) {
        DescriptionText = value.Item;
        DateText = value.Date;
        CostText = value.Cost;

        if (value.AddSub == "ADD") {
            SwitchModeButtonText = "Income  ☺";
            SwitchModeButtonColor = "Green";
            AddOrSub = "ADD";

            CategoryList.Clear();
            foreach (string VARIABLE in ReferenceValues.CategoryProfitList) {
                CategoryList.Add(VARIABLE);
            }
        } else {
            SwitchModeButtonText = "Expense  ☹";
            SwitchModeButtonColor = "Red";
            AddOrSub = "SUB";

            CategoryList.Clear();
            foreach (string VARIABLE in ReferenceValues.CategorySpendingList) {
                CategoryList.Add(VARIABLE);
            }
        }

        CategorySelected = value.Category;

        if (value.Person == ReferenceValues.JsonMasterSettings.User1Name) {
            selectedPerson = ReferenceValues.JsonMasterSettings.User1Name;
        } else if (value.Person == ReferenceValues.JsonMasterSettings.User2Name) {
            selectedPerson = ReferenceValues.JsonMasterSettings.User2Name;
        } else {
            selectedPerson = value.Person;
        }

        UserButtonLogic();
    }

    private void UserButtonLogic() {
        User1BackgroundColor = "Transparent";
        User2BackgroundColor = "Transparent";
        ChildrenBackgroundColor = "Transparent";
        HomeBackgroundColor = "Transparent";
        OtherBackgroundColor = "Transparent";

        if (selectedPerson == ReferenceValues.JsonMasterSettings.User1Name) {
            User1BackgroundColor = "Green";
        } else if (selectedPerson == ReferenceValues.JsonMasterSettings.User2Name) {
            User2BackgroundColor = "Green";
        } else {
            switch (selectedPerson) {
            case "Children":
                ChildrenBackgroundColor = "Green";
                break;
            case "Home":
                HomeBackgroundColor = "Green";
                break;
            default:
                OtherBackgroundColor = "Green";
                break;
            }
        }
    }

    private void SaveJson() {
        if (FinanceList.Count > 0) {
            try {
                ReferenceValues.JsonFinanceMasterList.financeList = FinanceList;
            } catch (Exception) {
                Console.WriteLine("ReferenceValues Doesnt exist");
            }

            try {
                string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonFinanceMasterList);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save finances.json... " + e.Message);
            }
        } else {
            try {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(fileName);
            } catch (Exception e) {
                Console.WriteLine("Unable to delete finances.json... " + e.Message);
            }
        }
    }

    #region Fields

    public string DescriptionText {
        get => _descriptionText;
        set {
            _descriptionText = VerifyInput.VerifyTextAlphaNumericSpace(value);
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

    public string CostText {
        get => _costText;
        set {
            _costText = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("CostText");
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
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("FinanceSelected");
        }
    }

    public string SwitchModeButtonColor {
        get => _switchModeButtonColor;
        set {
            _switchModeButtonColor = value;
            RaisePropertyChangedEvent("SwitchModeButtonColor");
        }
    }

    public string User1BackgroundColor {
        get => _user1BackgroundColor;
        set {
            _user1BackgroundColor = value;
            RaisePropertyChangedEvent("User1BackgroundColor");
        }
    }

    public string User2BackgroundColor {
        get => _user2BackgroundColor;
        set {
            _user2BackgroundColor = value;
            RaisePropertyChangedEvent("User2BackgroundColor");
        }
    }

    public string ChildrenBackgroundColor {
        get => _childrenBackgroundColor;
        set {
            _childrenBackgroundColor = value;
            RaisePropertyChangedEvent("ChildrenBackgroundColor");
        }
    }

    public string HomeBackgroundColor {
        get => _homeBackgroundColor;
        set {
            _homeBackgroundColor = value;
            RaisePropertyChangedEvent("HomeBackgroundColor");
        }
    }

    public string OtherBackgroundColor {
        get => _otherBackgroundColor;
        set {
            _otherBackgroundColor = value;
            RaisePropertyChangedEvent("OtherBackgroundColor");
        }
    }

    public string User1NameText {
        get => _user1NameText;
        set {
            _user1NameText = value;
            RaisePropertyChangedEvent("User1NameText");
        }
    }

    public string User2NameText {
        get => _user2NameText;
        set {
            _user2NameText = value;
            RaisePropertyChangedEvent("User2NameText");
        }
    }

    #endregion
}