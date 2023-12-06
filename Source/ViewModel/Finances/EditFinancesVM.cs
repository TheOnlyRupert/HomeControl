using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class EditFinancesVM : BaseViewModel {
    private ObservableCollection<string> _categoryList;

    private string _dateText, _switchModeButtonText, _switchModeButtonColor, _user1BackgroundColor, _user2BackgroundColor, _childrenBackgroundColor, _homeBackgroundColor,
        _otherBackgroundColor, AddOrSub, _costText, _parentsBackgroundColor, _detailsText, _categorySelected, _descriptionText, _cashAvailableText,
        _cashAvailableTextColor, _user1BorderColor, _user2BorderColor, _user3BorderColor, _user4BorderColor, _user5BorderColor, _homeBorderColor;

    private ObservableCollection<DetailedFinanceBlock> _detailedFinanceBlock1, _detailedFinanceBlock2;
    private ObservableCollection<DetailedFinanceBlockUser> _detailedFinanceBlockUser;
    private ObservableCollection<FinanceBlock> _financeList;

    private FinanceBlock _financeSelected;

    private BitmapImage _imageUser1, _imageUser2, _imageUser3, _imageUser4, _imageUser5, _imageHome;

    private int totalBilling, totalGrocery, totalPetrol, totalRestaurantTakeout, totalShopping, totalHealth, totalTravel, totalCoffee, totalEntertainment, totalServices, totalPersonalCare,
        totalHomeImprovement, totalAlcohol, totalFirearms, totalStreamingService, totalInterest, totalCarryOver, totalElectricBill, totalWaterBill, totalPhoneBill, totalGasBill, totalMortgageRent,
        totalChildCare, totalVehiclePayment, totalInternetBill, totalTrashBill, totalInsurance, totalChildSupport, totalGift, totalGovernment, totalPaycheck, totalInvestment, totalSellingAssets,
        totalOther, totalAllProfit, totalAllExpenses, totalUsers, totalGovernmentSpent, totalUser1Spent, totalUser2Spent, totalUser3Spent, totalUser4Spent, totalUser5Spent, totalUser1Profit,
        totalUser2Profit, totalUser3Profit, totalUser4Profit, totalUser5Profit, _user1BorderThickness, _user2BorderThickness, _user3BorderThickness, _user4BorderThickness, _user5BorderThickness,
        _homeBorderThickness, user;

    private double totalPercentageBilling, totalPercentageCarryOver, totalPercentageChildCare, totalPercentageCoffee, totalPercentageElectricBill, totalPercentageEntertainment,
        totalPercentageFirearms, totalPercentageGasBill, totalPercentageGrocery, totalPercentageHealth, totalPercentageAlcohol, totalPercentageHomeImprovement, totalPercentageInsurance,
        totalPercentageInterest, totalPercentageInternetBill, totalPercentageMortgageRent, totalPercentagePersonalCare, totalPercentagePetrol, totalPercentagePhoneBill,
        totalPercentageRestaurantTakeout, totalPercentageServices, totalPercentageShopping, totalPercentageStreamingService, totalPercentageTrashBill, totalPercentageTravel,
        totalPercentageVehiclePayment, totalPercentageWaterBill, totalPercentageChildSupport, totalPercentageGift, totalPercentageGovernment, totalPercentagePaycheck, totalPercentageInvestment,
        totalPercentageSellingAssets, totalPercentageOther, totalPercentageUsers, totalPercentageGovernmentSpent;

    public EditFinancesVM() {
        DescriptionText = "";
        DetailsText = "";
        CostText = "";
        FinanceList = new ObservableCollection<FinanceBlock>();

        try {
            FinanceList = ReferenceValues.JsonFinanceMaster.financeList;
        } catch (Exception) {
            ReferenceValues.JsonFinanceMaster = new JsonFinances {
                financeList = new ObservableCollection<FinanceBlock>()
            };
        }

        /* DEBUG - Cross-platform safe. This fixes the issue with missing icons when transferring files */
        if (ReferenceValues.JsonSettingsMaster.DebugMode) {
            foreach (FinanceBlock block in ReferenceValues.JsonFinanceMaster.financeList) {
                block.Image = block.UserId switch {
                    0 => ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user0.png",
                    1 => ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user1.png",
                    2 => ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user2.png",
                    3 => ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user3.png",
                    4 => ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user4.png",
                    5 => ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png",
                    _ => "null"
                };
            }
        }

        UserLogic(0);

        try {
            Uri uri = new(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user1.png", UriKind.RelativeOrAbsolute);
            ImageUser1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user2.png", UriKind.RelativeOrAbsolute);
            ImageUser2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user3.png", UriKind.RelativeOrAbsolute);
            ImageUser3 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user4.png", UriKind.RelativeOrAbsolute);
            ImageUser4 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png", UriKind.RelativeOrAbsolute);
            ImageUser5 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user0.png", UriKind.RelativeOrAbsolute);
            ImageHome = new BitmapImage(uri);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditFinancesVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        DateText = DateTime.Now.ToShortDateString();
        SwitchModeButtonText = "EXPENSE";
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
        DetailedFinanceBlockUser = new ObservableCollection<DetailedFinanceBlockUser>();

        RefreshDetailedView();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void UserLogic(int button) {
        user = button;

        switch (button) {
        case 0:
            HomeBorderColor = "Green";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 4;
            User1BorderThickness = 1;
            User2BorderThickness = 1;
            User3BorderThickness = 1;
            User4BorderThickness = 1;
            User5BorderThickness = 1;
            break;
        case 1:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "Green";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 1;
            User1BorderThickness = 4;
            User2BorderThickness = 1;
            User3BorderThickness = 1;
            User4BorderThickness = 1;
            User5BorderThickness = 1;
            break;
        case 2:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "Green";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 1;
            User1BorderThickness = 1;
            User2BorderThickness = 4;
            User3BorderThickness = 1;
            User4BorderThickness = 1;
            User5BorderThickness = 1;
            break;
        case 3:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "Green";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 1;
            User1BorderThickness = 1;
            User2BorderThickness = 1;
            User3BorderThickness = 4;
            User4BorderThickness = 1;
            User5BorderThickness = 1;
            break;
        case 4:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "Green";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 1;
            User1BorderThickness = 1;
            User2BorderThickness = 1;
            User3BorderThickness = 1;
            User4BorderThickness = 4;
            User5BorderThickness = 1;
            break;
        case 5:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "Green";
            HomeBorderThickness = 1;
            User1BorderThickness = 1;
            User2BorderThickness = 1;
            User3BorderThickness = 1;
            User4BorderThickness = 1;
            User5BorderThickness = 4;
            break;
        }
    }

    private void ButtonCommandLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(DescriptionText)) {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            } else if (string.IsNullOrWhiteSpace(CostText)) {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            } else {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditFinancesVM",
                    Description = "Adding finance: User" + user + ", " + AddOrSub + ", " + DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText + ", " +
                                  CategorySelected + ", " + DetailsText
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                FinanceList.Add(new FinanceBlock {
                    AddSub = AddOrSub,
                    Date = DateTime.Parse(DateText).ToShortDateString(),
                    Item = DescriptionText,
                    Cost = CostText,
                    Category = CategorySelected,
                    Details = DetailsText,
                    Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + user + ".png",
                    UserId = user
                });

                ReferenceValues.SoundToPlay = "cash";
                SoundDispatcher.PlaySound();
                DescriptionText = "";
                DetailsText = "";
                CostText = "";
                RefreshDetailedView();
                SaveJson();
            }

            break;
        case "update":
            try {
                if (FinanceSelected.Item != null) {
                    if (string.IsNullOrWhiteSpace(DescriptionText)) {
                        ReferenceValues.SoundToPlay = "missing_info";
                        SoundDispatcher.PlaySound();
                    } else if (string.IsNullOrWhiteSpace(CostText)) {
                        ReferenceValues.SoundToPlay = "missing_info";
                        SoundDispatcher.PlaySound();
                    } else {
                        confirmation = MessageBox.Show("Are you sure you want to update charge?", "Confirmation", MessageBoxButton.YesNo);
                        if (confirmation == MessageBoxResult.Yes) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditFinancesVM",
                                Description = "Updating finance: User" + user + ", " + AddOrSub + ", " + DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText +
                                              ", " + CategorySelected + ", " + DetailsText
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            FinanceList.Insert(FinanceList.IndexOf(FinanceSelected), new FinanceBlock {
                                AddSub = AddOrSub,
                                Date = DateTime.Parse(DateText).ToShortDateString(),
                                Item = DescriptionText,
                                Cost = CostText,
                                Category = CategorySelected,
                                Details = DetailsText,
                                Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + user + ".png",
                                UserId = user
                            });

                            ReferenceValues.SoundToPlay = "cash";
                            SoundDispatcher.PlaySound();
                            FinanceList.Remove(FinanceSelected);
                            DescriptionText = "";
                            DetailsText = "";
                            CostText = "";
                            RefreshDetailedView();
                            SaveJson();
                        }
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditFinancesVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case "delete":
            try {
                if (FinanceSelected.Item != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete charge?", "Confirmation", MessageBoxButton.YesNo);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditFinancesVM",
                            Description = "Removing finance: User" + user + ", " + AddOrSub + ", " + DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText + ", " +
                                          CategorySelected + ", " + DetailsText
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        ReferenceValues.SoundToPlay = "cash";
                        SoundDispatcher.PlaySound();
                        FinanceList.Remove(FinanceSelected);
                        RefreshDetailedView();
                        SaveJson();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditFinancesVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;

        case "user1":
            UserLogic(1);
            break;
        case "user2":
            UserLogic(2);
            break;
        case "user3":
            UserLogic(3);
            break;
        case "user4":
            UserLogic(4);
            break;
        case "user5":
            UserLogic(5);
            break;
        case "user0":
            UserLogic(0);
            break;
        case "switchMode":
            if (AddOrSub == "SUB") {
                SwitchModeButtonText = "TASK FUND";
                SwitchModeButtonColor = "Green";
                AddOrSub = "NA";

                CategoryList.Clear();
                foreach (string VARIABLE in ReferenceValues.CategoryTaskList) {
                    CategoryList.Add(VARIABLE);
                }

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
                view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));

                CategorySelected = "User1 Fund";
            } else if (AddOrSub == "NA") {
                SwitchModeButtonText = "INCOME";
                SwitchModeButtonColor = "Blue";
                AddOrSub = "ADD";

                CategoryList.Clear();
                foreach (string VARIABLE in ReferenceValues.CategoryProfitList) {
                    CategoryList.Add(VARIABLE);
                }

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
                view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));

                CategorySelected = "Paycheck";
            } else {
                SwitchModeButtonText = "EXPENSE";
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
        case "subDay":
            try {
                DateText = Convert.ToDateTime(DateText).AddDays(-1).ToShortDateString();
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditFinancesVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case "addDay":
            try {
                DateText = Convert.ToDateTime(DateText).AddDays(1).ToShortDateString();
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditFinancesVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        }
    }

    private void PopulateDetailedView(FinanceBlock value) {
        DescriptionText = value.Item;
        DateText = value.Date;
        CostText = value.Cost;
        DetailsText = value.Details;
        UserLogic(value.UserId);

        if (value.AddSub == "SUB") {
            SwitchModeButtonText = "EXPENSE";
            SwitchModeButtonColor = "Red";
            AddOrSub = "SUB";

            CategoryList.Clear();
            foreach (string VARIABLE in ReferenceValues.CategorySpendingList) {
                CategoryList.Add(VARIABLE);
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
            view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        } else if (value.AddSub == "NA") {
            SwitchModeButtonText = "TASK FUND";
            SwitchModeButtonColor = "Green";
            AddOrSub = "NA";

            CategoryList.Clear();
            foreach (string VARIABLE in ReferenceValues.CategoryTaskList) {
                CategoryList.Add(VARIABLE);
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
            view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        } else {
            SwitchModeButtonText = "INCOME";
            SwitchModeButtonColor = "Blue";
            AddOrSub = "ADD";

            CategoryList.Clear();
            foreach (string VARIABLE in ReferenceValues.CategoryProfitList) {
                CategoryList.Add(VARIABLE);
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CategoryList);
            view.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        }

        CategorySelected = value.Category;
    }

    private void SaveJson() {
        if (FinanceList.Count > 0) {
            IOrderedEnumerable<FinanceBlock> orderByResult = from s in FinanceList orderby s.Date select s;
            FinanceList = new ObservableCollection<FinanceBlock>(orderByResult.ToList());

            try {
                ReferenceValues.JsonFinanceMaster.financeList = FinanceList;
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditFinancesVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            try {
                FileHelpers.SaveFileText("finances", JsonSerializer.Serialize(ReferenceValues.JsonFinanceMaster), true);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditFinancesVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }
        }
    }

    private void RefreshDetailedView() {
        totalAlcohol = 0;
        totalBilling = 0;
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
        totalTrashBill = 0;
        totalTravel = 0;
        totalVehiclePayment = 0;
        totalWaterBill = 0;
        totalGovernmentSpent = 0;

        totalUsers = 0;
        totalUser1Spent = 0;
        totalUser1Profit = 0;
        totalUser2Spent = 0;
        totalUser2Profit = 0;
        totalUser3Spent = 0;
        totalUser3Profit = 0;
        totalUser4Spent = 0;
        totalUser4Profit = 0;
        totalUser5Spent = 0;
        totalUser5Profit = 0;

        totalChildSupport = 0;
        totalGift = 0;
        totalGovernment = 0;
        totalPaycheck = 0;
        totalInvestment = 0;
        totalSellingAssets = 0;
        totalOther = 0;

        DetailedFinanceBlock1.Clear();
        DetailedFinanceBlock2.Clear();
        DetailedFinanceBlockUser.Clear();

        foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMaster.financeList) {
            switch (financeBlock.Category) {
            case "Billing":
                try {
                    totalBilling += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Grocery":
                try {
                    totalGrocery += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Petrol":
                try {
                    totalPetrol += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Restaurant/Takeout":
                try {
                    totalRestaurantTakeout += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Shopping":
                try {
                    totalShopping += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Health":
                try {
                    totalHealth += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Travel":
                try {
                    totalTravel += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Coffee":
                try {
                    totalCoffee += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Entertainment":
                try {
                    totalEntertainment += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Services":
                try {
                    totalServices += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Personal Care":
                try {
                    totalPersonalCare += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Home Improvement":
                try {
                    totalHomeImprovement += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Alcohol":
                try {
                    totalAlcohol += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Firearms":
                try {
                    totalFirearms += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Streaming Service":
                try {
                    totalStreamingService += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Interest":
                try {
                    totalInterest += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Carry Over":
                try {
                    totalCarryOver += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Electric Bill":
                try {
                    totalElectricBill += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Water Bill":
                try {
                    totalWaterBill += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Phone Bill":
                try {
                    totalPhoneBill += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Gas Bill":
                try {
                    totalGasBill += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Mortgage/Rent":
                try {
                    totalMortgageRent += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Child Care":
                try {
                    totalChildCare += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Vehicle Payment":
                try {
                    totalVehiclePayment += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Internet Bill":
                try {
                    totalInternetBill += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Trash Bill":
                try {
                    totalTrashBill += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Insurance":
                try {
                    totalInsurance += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Child Support":
                try {
                    totalChildSupport += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Gift":
                try {
                    totalGift += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Government":
                try {
                    if (financeBlock.AddSub == "ADD") {
                        totalGovernment += int.Parse(financeBlock.Cost);
                    } else {
                        totalGovernmentSpent += int.Parse(financeBlock.Cost);
                    }
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Paycheck":
                try {
                    totalPaycheck += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Investment":
                try {
                    totalInvestment += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Selling Assets":
                try {
                    totalSellingAssets += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "Other":
                try {
                    totalOther += int.Parse(financeBlock.Cost);
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "User1 Fund":
                try {
                    if (financeBlock.AddSub == "SUB") {
                        totalUsers += int.Parse(financeBlock.Cost);
                        totalUser1Profit += int.Parse(financeBlock.Cost);
                    } else {
                        totalUser1Spent += int.Parse(financeBlock.Cost);
                    }
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "User2 Fund":
                try {
                    if (financeBlock.AddSub == "SUB") {
                        totalUsers += int.Parse(financeBlock.Cost);
                        totalUser2Profit += int.Parse(financeBlock.Cost);
                    } else {
                        totalUser2Spent += int.Parse(financeBlock.Cost);
                    }
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "User3 Fund":
                try {
                    if (financeBlock.AddSub == "SUB") {
                        totalUsers += int.Parse(financeBlock.Cost);
                        totalUser3Profit += int.Parse(financeBlock.Cost);
                    } else {
                        totalUser3Spent += int.Parse(financeBlock.Cost);
                    }
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "User4 Fund":
                try {
                    if (financeBlock.AddSub == "SUB") {
                        totalUsers += int.Parse(financeBlock.Cost);
                        totalUser4Profit += int.Parse(financeBlock.Cost);
                    } else {
                        totalUser4Spent += int.Parse(financeBlock.Cost);
                    }
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            case "User5 Fund":
                try {
                    if (financeBlock.AddSub == "SUB") {
                        totalUsers += int.Parse(financeBlock.Cost);
                        totalUser5Profit += int.Parse(financeBlock.Cost);
                    } else {
                        totalUser5Spent += int.Parse(financeBlock.Cost);
                    }
                } catch (Exception e) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "EditFinancesVM",
                        Description = e.ToString()
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                break;
            }
        }

        totalAllExpenses = totalAlcohol + totalBilling + totalChildCare + totalCoffee + totalElectricBill + totalEntertainment + totalFirearms + totalGasBill + totalGrocery +
                           totalHealth + totalHomeImprovement + totalInsurance + totalInterest + totalInternetBill + totalMortgageRent + totalPersonalCare + totalPetrol +
                           totalPhoneBill + totalRestaurantTakeout + totalServices + totalShopping + totalStreamingService + totalTrashBill + totalTravel + totalVehiclePayment +
                           totalWaterBill + totalGovernmentSpent;

        totalPercentageCarryOver = -1;
        totalPercentageAlcohol = Math.Round((double)(100 * totalAlcohol) / totalAllExpenses, 2);
        totalPercentageBilling = Math.Round((double)(100 * totalBilling) / totalAllExpenses, 2);
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
        totalPercentageTrashBill = Math.Round((double)(100 * totalTrashBill) / totalAllExpenses, 2);
        totalPercentageTravel = Math.Round((double)(100 * totalTravel) / totalAllExpenses, 2);
        totalPercentageVehiclePayment = Math.Round((double)(100 * totalVehiclePayment) / totalAllExpenses, 2);
        totalPercentageWaterBill = Math.Round((double)(100 * totalWaterBill) / totalAllExpenses, 2);
        totalPercentageGovernmentSpent = Math.Round((double)(100 * totalGovernmentSpent) / totalAllExpenses, 2);

        totalPercentageUsers = Math.Round((double)(100 * totalUsers) / totalAllExpenses, 2);

        totalAllProfit = totalChildSupport + totalGift + totalGovernment + totalPaycheck + totalInvestment;
        totalPercentageChildSupport = Math.Round((double)(100 * totalChildSupport) / totalAllProfit, 2);
        totalPercentageGift = Math.Round((double)(100 * totalGift) / totalAllProfit, 2);
        totalPercentageGovernment = Math.Round((double)(100 * totalGovernment) / totalAllProfit, 2);
        totalPercentagePaycheck = Math.Round((double)(100 * totalPaycheck) / totalAllProfit, 2);
        totalPercentageInvestment = Math.Round((double)(100 * totalInvestment) / totalAllProfit, 2);
        totalPercentageSellingAssets = Math.Round((double)(100 * totalSellingAssets) / totalAllProfit, 2);
        totalPercentageOther = Math.Round((double)(100 * totalOther) / totalAllProfit, 2);

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

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "User Funds",
            Percentage = totalPercentageUsers,
            Amount = totalUsers
        });

        DetailedFinanceBlock1.Add(new DetailedFinanceBlock {
            Category = "Government",
            Percentage = totalPercentageGovernmentSpent,
            Amount = totalGovernmentSpent
        });

        /* Start of profit */
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
            Category = "Investment",
            Percentage = totalPercentageInvestment,
            Amount = totalInvestment
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Selling Assets",
            Percentage = totalPercentageSellingAssets,
            Amount = totalSellingAssets
        });

        DetailedFinanceBlock2.Add(new DetailedFinanceBlock {
            Category = "Other",
            Percentage = totalPercentageOther,
            Amount = totalOther
        });

        /* Start of users */
        DetailedFinanceBlockUser.Add(new DetailedFinanceBlockUser {
            Id = 1,
            Name = ReferenceValues.JsonSettingsMaster.User1Name,
            TotalSpent = totalUser1Spent,
            TotalEarned = totalUser1Profit,
            Available = totalUser1Profit - totalUser1Spent
        });

        DetailedFinanceBlockUser.Add(new DetailedFinanceBlockUser {
            Id = 2,
            Name = ReferenceValues.JsonSettingsMaster.User2Name,
            TotalSpent = totalUser2Spent,
            TotalEarned = totalUser2Profit,
            Available = totalUser2Profit - totalUser2Spent
        });

        DetailedFinanceBlockUser.Add(new DetailedFinanceBlockUser {
            Id = 3,
            Name = ReferenceValues.JsonSettingsMaster.User3Name,
            TotalSpent = totalUser3Spent,
            TotalEarned = totalUser3Profit,
            Available = totalUser3Profit - totalUser3Spent
        });

        DetailedFinanceBlockUser.Add(new DetailedFinanceBlockUser {
            Id = 4,
            Name = ReferenceValues.JsonSettingsMaster.User4Name,
            TotalSpent = totalUser4Spent,
            TotalEarned = totalUser4Profit,
            Available = totalUser4Profit - totalUser4Spent
        });

        DetailedFinanceBlockUser.Add(new DetailedFinanceBlockUser {
            Id = 5,
            Name = ReferenceValues.JsonSettingsMaster.User5Name,
            TotalSpent = totalUser5Spent,
            TotalEarned = totalUser5Profit,
            Available = totalUser5Profit - totalUser5Spent
        });

        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DetailedFinanceBlock1);
        view.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
        view.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Ascending));

        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(DetailedFinanceBlock2);
        view2.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
        view2.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Ascending));

        /* Available Cash */
        int expense = 0;
        int income = 0;

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


        int available = income - expense;
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        culture.NumberFormat.CurrencyNegativePattern = 1;
        CashAvailableText = string.Format(culture, "{0:C}", available);
        CashAvailableTextColor = CashAvailableText.StartsWith("-") ? "Red" : "CornflowerBlue";
    }

    #region Fields

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

    public string DetailsText {
        get => _detailsText;
        set {
            _detailsText = value;
            RaisePropertyChangedEvent("DetailsText");
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

    public ObservableCollection<DetailedFinanceBlockUser> DetailedFinanceBlockUser {
        get => _detailedFinanceBlockUser;
        set {
            _detailedFinanceBlockUser = value;
            RaisePropertyChangedEvent("DetailedFinanceBlockUser");
        }
    }

    public BitmapImage ImageUser1 {
        get => _imageUser1;
        set {
            _imageUser1 = value;
            RaisePropertyChangedEvent("ImageUser1");
        }
    }

    public BitmapImage ImageUser2 {
        get => _imageUser2;
        set {
            _imageUser2 = value;
            RaisePropertyChangedEvent("ImageUser2");
        }
    }

    public BitmapImage ImageUser3 {
        get => _imageUser3;
        set {
            _imageUser3 = value;
            RaisePropertyChangedEvent("ImageUser3");
        }
    }

    public BitmapImage ImageUser4 {
        get => _imageUser4;
        set {
            _imageUser4 = value;
            RaisePropertyChangedEvent("ImageUser4");
        }
    }

    public BitmapImage ImageUser5 {
        get => _imageUser5;
        set {
            _imageUser5 = value;
            RaisePropertyChangedEvent("ImageUser5");
        }
    }

    public BitmapImage ImageHome {
        get => _imageHome;
        set {
            _imageHome = value;
            RaisePropertyChangedEvent("ImageHome");
        }
    }

    public int User1BorderThickness {
        get => _user1BorderThickness;
        set {
            _user1BorderThickness = value;
            RaisePropertyChangedEvent("User1BorderThickness");
        }
    }

    public int User2BorderThickness {
        get => _user2BorderThickness;
        set {
            _user2BorderThickness = value;
            RaisePropertyChangedEvent("User2BorderThickness");
        }
    }

    public int User3BorderThickness {
        get => _user3BorderThickness;
        set {
            _user3BorderThickness = value;
            RaisePropertyChangedEvent("User3BorderThickness");
        }
    }

    public int User4BorderThickness {
        get => _user4BorderThickness;
        set {
            _user4BorderThickness = value;
            RaisePropertyChangedEvent("User4BorderThickness");
        }
    }

    public int User5BorderThickness {
        get => _user5BorderThickness;
        set {
            _user5BorderThickness = value;
            RaisePropertyChangedEvent("User5BorderThickness");
        }
    }

    public int HomeBorderThickness {
        get => _homeBorderThickness;
        set {
            _homeBorderThickness = value;
            RaisePropertyChangedEvent("HomeBorderThickness");
        }
    }

    public string User1BorderColor {
        get => _user1BorderColor;
        set {
            _user1BorderColor = value;
            RaisePropertyChangedEvent("User1BorderColor");
        }
    }

    public string User2BorderColor {
        get => _user2BorderColor;
        set {
            _user2BorderColor = value;
            RaisePropertyChangedEvent("User2BorderColor");
        }
    }

    public string User3BorderColor {
        get => _user3BorderColor;
        set {
            _user3BorderColor = value;
            RaisePropertyChangedEvent("User3BorderColor");
        }
    }

    public string User4BorderColor {
        get => _user4BorderColor;
        set {
            _user4BorderColor = value;
            RaisePropertyChangedEvent("User4BorderColor");
        }
    }

    public string User5BorderColor {
        get => _user5BorderColor;
        set {
            _user5BorderColor = value;
            RaisePropertyChangedEvent("User5BorderColor");
        }
    }

    public string HomeBorderColor {
        get => _homeBorderColor;
        set {
            _homeBorderColor = value;
            RaisePropertyChangedEvent("HomeBorderColor");
        }
    }

    #endregion
}