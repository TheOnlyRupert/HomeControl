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
        totalHomeImprovement, totalAlcohol, totalFirearms, totalStreamingService, totalBrittanyFund, totalStupidDumb, totalInterest, totalCarryOver,
        totalElectricBill, totalWaterBill,
        totalPhoneBill, totalGasBill, totalMortgageRent, totalChildCare, totalVehiclePayment, totalInternetBill, totalTrashBill, totalInsurance, totalChildSupport, totalGift,
        totalGovernment, totalPaycheck, totalRefund, totalAllProfit, totalAllExpenses;


    private double totalPercentageBilling, totalPercentageBrittanyFund, totalPercentageCarryOver, totalPercentageChildCare,
        totalPercentageCoffee, totalPercentageElectricBill, totalPercentageEntertainment, totalPercentageFirearms, totalPercentageGasBill, totalPercentageGrocery,
        totalPercentageHealth, totalPercentageAlcohol,
        totalPercentageHomeImprovement, totalPercentageInsurance, totalPercentageInterest, totalPercentageInternetBill, totalPercentageMortgageRent, totalPercentagePersonalCare,
        totalPercentagePetrol, totalPercentagePhoneBill, totalPercentageRestaurantTakeout, totalPercentageServices, totalPercentageShopping, totalPercentageStreamingService,
        totalPercentageStupidDumb, totalPercentageTrashBill, totalPercentageTravel, totalPercentageVehiclePayment, totalPercentageWaterBill, totalPercentageChildSupport,
        totalPercentageGift, totalPercentageGovernment, totalPercentagePaycheck, totalPercentageRefund;

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

        totalAllExpenses = totalAlcohol + totalBilling + totalBrittanyFund + totalChildCare + totalCoffee + totalElectricBill + totalEntertainment + totalFirearms
                           + totalGasBill + totalGrocery + totalHealth + totalHomeImprovement + totalInsurance + totalInterest + totalInternetBill + totalMortgageRent +
                           totalPersonalCare
                           + totalPetrol + totalPhoneBill + totalRestaurantTakeout + totalServices + totalShopping + totalStreamingService + totalStupidDumb + totalTrashBill +
                           totalTravel
                           + totalVehiclePayment + totalWaterBill;

        totalPercentageCarryOver = -1;
        totalPercentageAlcohol = Math.Round((double)(100 * totalAlcohol) / totalAllExpenses, 2);
        totalPercentageBilling = Math.Round((double)(100 * totalBilling) / totalAllExpenses, 2);
        totalPercentageBrittanyFund = Math.Round((double)(100 * totalBrittanyFund) / totalAllExpenses, 2);
        totalPercentageChildCare = Math.Round((double)(100 * totalChildCare) / totalAllExpenses, 2);
        totalPercentageCoffee = Math.Round((double)(100 * totalCoffee) / totalAllExpenses, 2);
        totalPercentageElectricBill = Math.Round((double)(100 * totalElectricBill) / totalAllExpenses, 2);
        totalPercentageEntertainment = Math.Round((double)(100 * totalEntertainment) / totalAllExpenses, 2);
        totalPercentageFirearms = Math.Round((double)(100 * totalFirearms) / totalAllExpenses, 2);
        totalPercentageGasBill = Math.Round((double)(100 * totalGasBill) / totalAllExpenses, 2);
        totalPercentageGrocery = Math.Round((double)(100 * totalGrocery) / totalAllExpenses, 2);
        totalPercentageHealth = Math.Round((double)(100 * totalHealth) / totalAllExpenses, 2);
        totalPercentageHomeImprovement = Math.Round((double)(100 * totalHomeImprovement) / totalAllExpenses, 2);
        totalPercentageInsurance = Math.Round((double)(100 * totalInsurance) / totalAllExpenses, 2);
        totalPercentageInterest = Math.Round((double)(100 * totalInterest) / totalAllExpenses, 2);
        totalPercentageInternetBill = Math.Round((double)(100 * totalInternetBill) / totalAllExpenses, 2);
        totalPercentageMortgageRent = Math.Round((double)(100 * totalMortgageRent) / totalAllExpenses, 2);
        totalPercentagePersonalCare = Math.Round((double)(100 * totalPersonalCare) / totalAllExpenses, 2);
        totalPercentagePetrol = Math.Round((double)(100 * totalPetrol) / totalAllExpenses, 2);
        totalPercentagePhoneBill = Math.Round((double)(100 * totalPhoneBill) / totalAllExpenses, 2);
        totalPercentageRestaurantTakeout = Math.Round((double)(100 * totalRestaurantTakeout) / totalAllExpenses, 2);
        totalPercentageServices = Math.Round((double)(100 * totalServices) / totalAllExpenses, 2);
        totalPercentageShopping = Math.Round((double)(100 * totalShopping) / totalAllExpenses, 2);
        totalPercentageStreamingService = Math.Round((double)(100 * totalStreamingService) / totalAllExpenses, 2);
        totalPercentageStupidDumb = Math.Round((double)(100 * totalStupidDumb) / totalAllExpenses, 2);
        totalPercentageTrashBill = Math.Round((double)(100 * totalTrashBill) / totalAllExpenses, 2);
        totalPercentageTravel = Math.Round((double)(100 * totalTravel) / totalAllExpenses, 2);
        totalPercentageVehiclePayment = Math.Round((double)(100 * totalVehiclePayment) / totalAllExpenses, 2);
        totalPercentageWaterBill = Math.Round((double)(100 * totalWaterBill) / totalAllExpenses, 2);

        totalAllProfit = totalChildSupport + totalGift + totalGovernment + totalPaycheck + totalRefund;
        totalPercentageChildSupport = Math.Round((double)(100 * totalChildSupport) / totalAllProfit, 2);
        totalPercentageGift = Math.Round((double)(100 * totalGift) / totalAllProfit, 2);
        totalPercentageGovernment = Math.Round((double)(100 * totalGovernment) / totalAllProfit, 2);
        totalPercentagePaycheck = Math.Round((double)(100 * totalPaycheck) / totalAllProfit, 2);
        totalPercentageRefund = Math.Round((double)(100 * totalRefund) / totalAllProfit, 2);

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Billing",
            Percentage = totalPercentageBilling,
            Amount = totalBilling
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Grocery",
            Percentage = totalPercentageGrocery,
            Amount = totalGrocery
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Petrol",
            Percentage = totalPercentagePetrol,
            Amount = totalPetrol
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Restaurant/Takeout",
            Percentage = totalPercentageRestaurantTakeout,
            Amount = totalRestaurantTakeout
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Shopping",
            Percentage = totalPercentageShopping,
            Amount = totalShopping
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Health",
            Percentage = totalPercentageHealth,
            Amount = totalHealth
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Travel",
            Percentage = totalPercentageTravel,
            Amount = totalTravel
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Coffee",
            Percentage = totalPercentageCoffee,
            Amount = totalCoffee
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Entertainment",
            Percentage = totalPercentageEntertainment,
            Amount = totalEntertainment
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Services",
            Percentage = totalPercentageServices,
            Amount = totalServices
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Personal Care",
            Percentage = totalPercentagePersonalCare,
            Amount = totalPersonalCare
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Home Improvement",
            Percentage = totalPercentageHomeImprovement,
            Amount = totalHomeImprovement
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Alcohol",
            Percentage = totalPercentageAlcohol,
            Amount = totalAlcohol
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Firearms",
            Percentage = totalPercentageFirearms,
            Amount = totalFirearms
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Streaming Service",
            Percentage = totalPercentageStreamingService,
            Amount = totalStreamingService
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Brittany Fund",
            Percentage = totalPercentageBrittanyFund,
            Amount = totalBrittanyFund
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Stupid/Dumb",
            Percentage = totalPercentageStupidDumb,
            Amount = totalStupidDumb
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Interest",
            Percentage = totalPercentageInterest,
            Amount = totalInterest
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Carry Over",
            Percentage = totalPercentageCarryOver,
            Amount = totalCarryOver
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Electric Bill",
            Percentage = totalPercentageElectricBill,
            Amount = totalElectricBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Water Bill",
            Percentage = totalPercentageWaterBill,
            Amount = totalWaterBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Phone Bill",
            Percentage = totalPercentagePhoneBill,
            Amount = totalPhoneBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Gas Bill",
            Percentage = totalPercentageGasBill,
            Amount = totalGasBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Mortgage/Rent",
            Percentage = totalPercentageMortgageRent,
            Amount = totalMortgageRent
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Child Care",
            Percentage = totalPercentageChildCare,
            Amount = totalChildCare
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Vehicle Payment",
            Percentage = totalPercentageVehiclePayment,
            Amount = totalVehiclePayment
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Internet Bill",
            Percentage = totalPercentageInternetBill,
            Amount = totalInternetBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Trash Bill",
            Percentage = totalPercentageTrashBill,
            Amount = totalTrashBill
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Insurance",
            Percentage = totalPercentageInsurance,
            Amount = totalInsurance
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Child Support",
            Percentage = totalPercentageChildSupport,
            Amount = totalChildSupport
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Gift",
            Percentage = totalPercentageGift,
            Amount = totalGift
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Government",
            Percentage = totalPercentageGovernment,
            Amount = totalGovernment
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Paycheck",
            Percentage = totalPercentagePaycheck,
            Amount = totalPaycheck
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Refund",
            Percentage = totalPercentageRefund,
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