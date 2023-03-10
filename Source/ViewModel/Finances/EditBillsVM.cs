using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class EditBillsVM : BaseViewModel {
    private readonly string fileName;

    private ObservableCollection<RecurringFinanceBlock> _financeList;
    private RecurringFinanceBlock _financeSelected;

    private int _recurringDayText;
    private ObservableCollection<string> _recurringMonthList;

    private string _user1BackgroundColor, _user2BackgroundColor, _childrenBackgroundColor, _homeBackgroundColor, selectedPerson, _descriptionText,
        _otherBackgroundColor, _user1NameText, _user2NameText, _costText, _parentsBackgroundColor, _recurringMonthSelected;

    public EditBillsVM() {
        fileName = ReferenceValues.FILE_DIRECTORY + "bills.json";

        selectedPerson = "Home";
        User1NameText = ReferenceValues.JsonMasterSettings.User1Name;
        User2NameText = ReferenceValues.JsonMasterSettings.User2Name;
        DescriptionText = "";
        CostText = "";
        RecurringDayText = 1;
        UserButtonLogic();

        /* Populate drop down box with spending categories and set default */
        RecurringMonthList = new ObservableCollection<string>();
        foreach (string VARIABLE in ReferenceValues.RecurringMonth) {
            RecurringMonthList.Add(VARIABLE);
        }

        RecurringMonthSelected = "MONTHLY";

        FinanceList = new ObservableCollection<RecurringFinanceBlock>();
        try {
            FinanceList = ReferenceValues.JsonRecurringFinanceMasterList.recurringFinanceList;
        } catch (Exception) {
            ReferenceValues.JsonRecurringFinanceMasterList = new JsonRecurringFinances {
                recurringFinanceList = new ObservableCollection<RecurringFinanceBlock>()
            };
        }
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
                try {
                    FinanceList.Add(new RecurringFinanceBlock {
                        Item = DescriptionText,
                        Cost = CostText,
                        Person = selectedPerson,
                        RecurringMonth = RecurringMonthSelected,
                        RecurringDay = RecurringDayText
                    });

                    DescriptionText = "";
                    CostText = "";
                    SaveJson();
                } catch (Exception) { }
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
                        confirmation = MessageBox.Show("Are you sure you want to update recurring charge?", "Confirmation", MessageBoxButton.YesNo);
                        if (confirmation == MessageBoxResult.Yes) {
                            try {
                                FinanceList.Insert(FinanceList.IndexOf(FinanceSelected), new RecurringFinanceBlock {
                                    Item = DescriptionText,
                                    Cost = CostText,
                                    Person = selectedPerson,
                                    RecurringMonth = RecurringMonthSelected,
                                    RecurringDay = RecurringDayText
                                });

                                FinanceList.Remove(FinanceSelected);
                                DescriptionText = "";
                                CostText = "";
                                SaveJson();
                            } catch (Exception) { }
                        }
                    }
                }
            } catch (Exception) { }

            break;
        case "delete":
            try {
                if (FinanceSelected.Item != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete recurring charge?", "Confirmation", MessageBoxButton.YesNo);
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

        case "parents":
            selectedPerson = "Parents";
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

        case "subDate":
            if (RecurringDayText > 1) {
                RecurringDayText--;
            }

            break;

        case "addDate":
            if (RecurringDayText < 28) {
                RecurringDayText++;
            }

            break;
        }
    }

    private void PopulateDetailedView(RecurringFinanceBlock value) {
        DescriptionText = value.Item;
        CostText = value.Cost;
        RecurringMonthSelected = value.RecurringMonth;
        RecurringDayText = value.RecurringDay;

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
        ParentsBackgroundColor = "Transparent";
        ChildrenBackgroundColor = "Transparent";
        HomeBackgroundColor = "Transparent";
        OtherBackgroundColor = "Transparent";

        if (selectedPerson == ReferenceValues.JsonMasterSettings.User1Name) {
            User1BackgroundColor = "Green";
        } else if (selectedPerson == ReferenceValues.JsonMasterSettings.User2Name) {
            User2BackgroundColor = "Green";
        } else {
            switch (selectedPerson) {
            case "Parents":
                ParentsBackgroundColor = "Green";
                break;
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
                ReferenceValues.JsonRecurringFinanceMasterList.recurringFinanceList = FinanceList;
            } catch (Exception) {
                Console.WriteLine("ReferenceValues Doesnt exist");
            }

            try {
                string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonRecurringFinanceMasterList);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save bills.json... " + e.Message);
            }
        } else {
            try {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(fileName);
            } catch (Exception e) {
                Console.WriteLine("Unable to delete bills.json... " + e.Message);
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

    public string CostText {
        get => _costText;
        set {
            _costText = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("CostText");
        }
    }

    public ObservableCollection<string> RecurringMonthList {
        get => _recurringMonthList;
        set {
            _recurringMonthList = value;
            RaisePropertyChangedEvent("RecurringMonthList");
        }
    }

    public string RecurringMonthSelected {
        get => _recurringMonthSelected;
        set {
            _recurringMonthSelected = value;
            RaisePropertyChangedEvent("RecurringMonthSelected");
        }
    }

    public int RecurringDayText {
        get => _recurringDayText;
        set {
            _recurringDayText = value;
            RaisePropertyChangedEvent("RecurringDayText");
        }
    }

    public ObservableCollection<RecurringFinanceBlock> FinanceList {
        get => _financeList;
        set {
            _financeList = value;
            RaisePropertyChangedEvent("FinanceList");
        }
    }

    public RecurringFinanceBlock FinanceSelected {
        get => _financeSelected;
        set {
            _financeSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("FinanceSelected");
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

    public string ParentsBackgroundColor {
        get => _parentsBackgroundColor;
        set {
            _parentsBackgroundColor = value;
            RaisePropertyChangedEvent("ParentsBackgroundColor");
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