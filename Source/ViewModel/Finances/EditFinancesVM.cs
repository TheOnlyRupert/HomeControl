using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using HomeControl.Source.Control;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class EditFinancesVM : BaseViewModel {
    private readonly PlaySound cashSound, missingInfoSound;
    private readonly string fileName;
    private ObservableCollection<string> _categoryList;

    private string _dateText, _switchModeButtonText, _switchModeButtonColor, _user1BackgroundColor, _user2BackgroundColor, _childrenBackgroundColor, _homeBackgroundColor,
        _otherBackgroundColor, _user1NameText, _user2NameText, AddOrSub, _costText, _parentsBackgroundColor;

    private ObservableCollection<DetailedFinanceBlock> _detailedFinanceBlock1, _detailedFinanceBlock2;

    private ObservableCollection<FinanceBlock> _financeList;
    private FinanceBlock _financeSelected;
    private string selectedPerson, _categorySelected, _descriptionText;

    private int totalBilling, totalGrocery, totalPetrol, totalRestaurantTakeout, totalShopping, totalHealth, totalTravel, totalCoffee, totalEntertainment, totalServices,
        totalPersonalCare,
        totalHomeImprovement, totalAlcohol, totalAlcoholBar, totalFirearms, totalStreamingService, totalBrittanyFund, totalStupidDumb, totalInterest, totalCarryOver,
        totalElectricBill, totalWaterBill,
        totalPhoneBill, totalGasBill, totalMortgageRent, totalChildCare, totalVehiclePayment, totalInternetBill, totalTrashBill, totalInsurance, totalChildSupport, totalGift,
        totalGovernment, totalPaycheck, totalRefund;

    public EditFinancesVM() {
        fileName = ReferenceValues.FILE_DIRECTORY + "finances.json";
        cashSound = new PlaySound("cash");
        missingInfoSound = new PlaySound("missing_info");

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

        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
        view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));

        CategorySelected = "Billing";

        DetailedFinanceBlock1 = new ObservableCollection<DetailedFinanceBlock>();
        DetailedFinanceBlock2 = new ObservableCollection<DetailedFinanceBlock>();

        RefreshDetailedView();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(DescriptionText)) {
                missingInfoSound.Play(false);
            } else if (string.IsNullOrWhiteSpace(CostText)) {
                missingInfoSound.Play(false);
            } else {
                FinanceList.Add(new FinanceBlock {
                    AddSub = AddOrSub,
                    Date = DateTime.Parse(DateText).ToShortDateString(),
                    Item = DescriptionText,
                    Cost = CostText,
                    Category = CategorySelected,
                    Person = selectedPerson
                });

                ReferenceValues.DebugText += "[" + DateTime.Now.ToString("yyyy-MM-dd_HHMM") + "] [ " + "EditFinances/INFO] ADDING finance: " + AddOrSub + ", " +
                                             DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText + ", " + CategorySelected + ", " +
                                             selectedPerson + "\n";

                cashSound.Play(false);
                DescriptionText = "";
                CostText = "";
                RefreshDetailedView();
                SaveJson();
            }

            break;
        case "update":
            try {
                if (FinanceSelected.Item != null) {
                    if (string.IsNullOrWhiteSpace(DescriptionText)) {
                        missingInfoSound.Play(false);
                    } else if (string.IsNullOrWhiteSpace(CostText)) {
                        missingInfoSound.Play(false);
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

                            ReferenceValues.DebugText += "[" + DateTime.Now.ToString("yyyy-MM-dd_HHMM") + "] [ " + "EditFinances/INFO] UPDATING finance: " + AddOrSub + ", " +
                                                         DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText + ", " + CategorySelected + ", " +
                                                         selectedPerson + "\n";

                            cashSound.Play(false);
                            FinanceList.Remove(FinanceSelected);
                            DescriptionText = "";
                            CostText = "";
                            RefreshDetailedView();
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
                        cashSound.Play(false);

                        ReferenceValues.DebugText += "[" + DateTime.Now.ToString("yyyy-MM-dd_HHMM") + "] [ " + "EditFinances/INFO] REMOVING finance: " + AddOrSub + ", " +
                                                     DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText + ", " + CategorySelected + ", " +
                                                     selectedPerson + "\n";

                        FinanceList.Remove(FinanceSelected);
                        RefreshDetailedView();
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

        case "switchMode":
            if (AddOrSub == "SUB") {
                SwitchModeButtonText = "Income  ☺";
                SwitchModeButtonColor = "Green";
                AddOrSub = "ADD";

                CategoryList.Clear();
                foreach (string VARIABLE in ReferenceValues.CategoryProfitList) {
                    CategoryList.Add(VARIABLE);
                }

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
                view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));

                CategorySelected = "Paycheck";
            } else {
                SwitchModeButtonText = "Expense  ☹";
                SwitchModeButtonColor = "Red";
                AddOrSub = "SUB";

                CategoryList.Clear();
                foreach (string VARIABLE in ReferenceValues.CategorySpendingList) {
                    CategoryList.Add(VARIABLE);
                }

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
                view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));

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

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
            view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        } else {
            SwitchModeButtonText = "Expense  ☹";
            SwitchModeButtonColor = "Red";
            AddOrSub = "SUB";

            CategoryList.Clear();
            foreach (string VARIABLE in ReferenceValues.CategorySpendingList) {
                CategoryList.Add(VARIABLE);
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
            view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
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
                ReferenceValues.JsonFinanceMasterList.financeList = FinanceList;
            } catch (Exception) { }

            try {
                string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonFinanceMasterList);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                ReferenceValues.DebugText += "[" + DateTime.Now.ToString("yyyy-MM-dd_HHMM") + "] [ " + "EditFinances/ERROR] Unable to save finances.json... " + e.Message + "\n";
            }
        }
    }

    private void RefreshDetailedView() {
        totalAlcohol = 0;
        totalAlcoholBar = 0;
        totalBilling = 0;
        totalBrittanyFund = 0;
        totalCarryOver = 0;
        totalChildCare = 0;
        totalCoffee = 0;
        totalElectricBill = 0;
        totalEntertainment = 0;
        totalFirearms = 0;
        totalGasBill = 0;
        totalGrocery = 0;
        totalHealth = 0;
        totalHomeImprovement = 0;
        totalInsurance = 0;
        totalInterest = 0;
        totalInternetBill = 0;
        totalMortgageRent = 0;
        totalPersonalCare = 0;
        totalPetrol = 0;
        totalPhoneBill = 0;
        totalRestaurantTakeout = 0;
        totalServices = 0;
        totalShopping = 0;
        totalStreamingService = 0;
        totalStupidDumb = 0;
        totalTrashBill = 0;
        totalTravel = 0;
        totalVehiclePayment = 0;
        totalWaterBill = 0;

        totalChildSupport = 0;
        totalGift = 0;
        totalGovernment = 0;
        totalPaycheck = 0;
        totalRefund = 0;

        DetailedFinanceBlock1.Clear();
        DetailedFinanceBlock2.Clear();

        foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMasterList.financeList) {
            switch (financeBlock.Category) {
            case "Billing":
                try {
                    totalBilling += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Grocery":
                try {
                    totalGrocery += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Petrol":
                try {
                    totalPetrol += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Restaurant/Takeout":
                try {
                    totalRestaurantTakeout += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Shopping":
                try {
                    totalShopping += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Health":
                try {
                    totalHealth += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Travel":
                try {
                    totalTravel += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Coffee":
                try {
                    totalCoffee += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Entertainment":
                try {
                    totalEntertainment += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Services":
                try {
                    totalServices += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Personal Care":
                try {
                    totalPersonalCare += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Home Improvement":
                try {
                    totalHomeImprovement += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Alcohol":
                try {
                    totalAlcohol += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Alcohol Bar":
                try {
                    totalAlcoholBar += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Firearms":
                try {
                    totalFirearms += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Streaming Service":
                try {
                    totalStreamingService += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Brittany Fund":
                try {
                    totalBrittanyFund += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Stupid/Dumb":
                try {
                    totalStupidDumb += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Interest":
                try {
                    totalInterest += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Carry Over":
                try {
                    totalCarryOver += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Electric Bill":
                try {
                    totalElectricBill += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Water Bill":
                try {
                    totalWaterBill += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Phone Bill":
                try {
                    totalPhoneBill += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Gas Bill":
                try {
                    totalGasBill += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Mortgage/Rent":
                try {
                    totalMortgageRent += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Child Care":
                try {
                    totalChildCare += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Vehicle Payment":
                try {
                    totalVehiclePayment += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Internet Bill":
                try {
                    totalInternetBill += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Trash Bill":
                try {
                    totalTrashBill += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Insurance":
                try {
                    totalInsurance += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Child Support":
                try {
                    totalChildSupport += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Gift":
                try {
                    totalGift += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Government":
                try {
                    totalGovernment += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Paycheck":
                try {
                    totalPaycheck += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            case "Refund":
                try {
                    totalRefund += int.Parse(financeBlock.Cost);
                } catch (Exception) { }

                break;
            }
        }

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Billing",
            Percentage = 0,
            Amount = totalBilling
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Grocery",
            Percentage = 0,
            Amount = totalGrocery
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Petrol",
            Percentage = 0,
            Amount = totalPetrol
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Restaurant/Takeout",
            Percentage = 0,
            Amount = totalRestaurantTakeout
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Shopping",
            Percentage = 0,
            Amount = totalShopping
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Health",
            Percentage = 0,
            Amount = totalHealth
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Travel",
            Percentage = 0,
            Amount = totalTravel
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Coffee",
            Percentage = 0,
            Amount = totalCoffee
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Entertainment",
            Percentage = 0,
            Amount = totalEntertainment
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Services",
            Percentage = 0,
            Amount = totalServices
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Personal Care",
            Percentage = 0,
            Amount = totalPersonalCare
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Home Improvement",
            Percentage = 0,
            Amount = totalHomeImprovement
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Alcohol",
            Percentage = 0,
            Amount = totalAlcohol
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Alcohol Bar",
            Percentage = 0,
            Amount = totalAlcoholBar
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Firearms",
            Percentage = 0,
            Amount = totalFirearms
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Streaming Service",
            Percentage = 0,
            Amount = totalStreamingService
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Brittany Fund",
            Percentage = 0,
            Amount = totalBrittanyFund
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Stupid/Dumb",
            Percentage = 0,
            Amount = totalStupidDumb
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Interest",
            Percentage = 0,
            Amount = totalInterest
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Carry Over",
            Percentage = 0,
            Amount = totalCarryOver
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Electric Bill",
            Percentage = 0,
            Amount = totalElectricBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Water Bill",
            Percentage = 0,
            Amount = totalWaterBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Phone Bill",
            Percentage = 0,
            Amount = totalPhoneBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Gas Bill",
            Percentage = 0,
            Amount = totalGasBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Mortgage/Rent",
            Percentage = 0,
            Amount = totalMortgageRent
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Child Care",
            Percentage = 0,
            Amount = totalChildCare
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Vehicle Payment",
            Percentage = 0,
            Amount = totalVehiclePayment
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Internet Bill",
            Percentage = 0,
            Amount = totalInternetBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Trash Bill",
            Percentage = 0,
            Amount = totalTrashBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Insurance",
            Percentage = 0,
            Amount = totalInsurance
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Child Support",
            Percentage = 0,
            Amount = totalChildSupport
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Gift",
            Percentage = 0,
            Amount = totalGift
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Government",
            Percentage = 0,
            Amount = totalGovernment
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Paycheck",
            Percentage = 0,
            Amount = totalPaycheck
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Refund",
            Percentage = 0,
            Amount = totalRefund
        });

        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DetailedFinanceBlock1);
        view.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
        view.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Ascending));

        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(DetailedFinanceBlock2);
        view2.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
        view2.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Ascending));
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
            if (string.IsNullOrWhiteSpace(value)) {
                value = DateTime.Now.ToShortDateString();
            }

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

    public ObservableCollection<DetailedFinanceBlock> DetailedFinanceBlock1 {
        get => _detailedFinanceBlock1;
        set {
            _detailedFinanceBlock1 = value;
            RaisePropertyChangedEvent("DetailedFinanceBlock1");
        }
    }

    public ObservableCollection<DetailedFinanceBlock> DetailedFinanceBlock2 {
        get => _detailedFinanceBlock2;
        set {
            _detailedFinanceBlock2 = value;
            RaisePropertyChangedEvent("DetailedFinanceBlock2");
        }
    }

    #endregion
}