using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

    private string _dateText, _costText, _detailsText, _categorySelected, _descriptionText, _user1BorderColor, _user2BorderColor, _user3BorderColor, _user4BorderColor, _user5BorderColor,
        _homeBorderColor;

    private ObservableCollection<DetailedFinanceBlock> _detailedFinanceBlock;
    private ObservableCollection<FinanceBlock> _financeList;

    private FinanceBlock _financeSelected;

    private BitmapImage _imageUser1, _imageUser2, _imageUser3, _imageUser4, _imageUser5, _imageHome;

    private int totalCategory1, totalCategory2, totalCategory3, totalCategory4, totalCategory5, totalCategory6, totalCategory7, totalCategory8, totalCategory9, totalAllExpenses,
        _user1BorderThickness, _user2BorderThickness, _user3BorderThickness, _user4BorderThickness, _user5BorderThickness, _homeBorderThickness, user, _categoryID;

    private double totalPercentageCategory1, totalPercentageCategory2, totalPercentageCategory3, totalPercentageCategory4, totalPercentageCategory5, totalPercentageCategory6,
        totalPercentageCategory7, totalPercentageCategory8, totalPercentageCategory9;

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
                    5 => ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png"
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

        /* Populate drop down box with spending categories and set default */
        CategoryList = new ObservableCollection<string> {
            ReferenceValues.JsonSettingsMaster.FinanceBlock1,
            ReferenceValues.JsonSettingsMaster.FinanceBlock2,
            ReferenceValues.JsonSettingsMaster.FinanceBlock3,
            ReferenceValues.JsonSettingsMaster.FinanceBlock4,
            ReferenceValues.JsonSettingsMaster.FinanceBlock5,
            ReferenceValues.JsonSettingsMaster.FinanceBlock6,
            ReferenceValues.JsonSettingsMaster.FinanceBlock7,
            ReferenceValues.JsonSettingsMaster.FinanceBlock8,
            ReferenceValues.JsonSettingsMaster.FinanceBlock9
        };

        CategorySelected = ReferenceValues.JsonSettingsMaster.FinanceBlock1;

        DetailedFinanceBlock = new ObservableCollection<DetailedFinanceBlock>();

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
                    Description = "Adding finance: User" + user + ", " + ", " + DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText + ", " +
                                  CategorySelected + ", " + DetailsText
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                FinanceList.Add(new FinanceBlock {
                    Date = DateTime.Parse(DateText).ToShortDateString(),
                    Item = DescriptionText,
                    Cost = CostText,
                    Category = CategorySelected,
                    CategoryID = CategoryID,
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

            RefreshDetailedView();

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
                                Description = "Updating finance: User" + user + ", " + ", " + DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText +
                                              ", " + CategorySelected + ", " + DetailsText
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            FinanceList.Insert(FinanceList.IndexOf(FinanceSelected), new FinanceBlock {
                                Date = DateTime.Parse(DateText).ToShortDateString(),
                                Item = DescriptionText,
                                Cost = CostText,
                                Category = CategorySelected,
                                CategoryID = CategoryID,
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

            RefreshDetailedView();

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
                            Description = "Removing finance: User" + user + ", " + ", " + DateTime.Parse(DateText).ToShortDateString() + ", " + DescriptionText + ", " + CostText + ", " +
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

            RefreshDetailedView();

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
        CategorySelected = value.Category;
        UserLogic(value.UserId);
    }

    private void SaveJson() {
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

    private void RefreshDetailedView() {
        totalCategory1 = 0;
        totalCategory2 = 0;
        totalCategory3 = 0;
        totalCategory4 = 0;
        totalCategory5 = 0;
        totalCategory6 = 0;
        totalCategory7 = 0;
        totalCategory8 = 0;
        totalCategory9 = 0;

        DetailedFinanceBlock.Clear();

        foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMaster.financeList) {
            switch (financeBlock.CategoryID) {
            case 0:
                try {
                    totalCategory1 += int.Parse(financeBlock.Cost);
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
            case 1:
                try {
                    totalCategory2 += int.Parse(financeBlock.Cost);
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
            case 2:
                try {
                    totalCategory3 += int.Parse(financeBlock.Cost);
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
            case 3:
                try {
                    totalCategory4 += int.Parse(financeBlock.Cost);
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
            case 4:
                try {
                    totalCategory5 += int.Parse(financeBlock.Cost);
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
            case 5:
                try {
                    totalCategory6 += int.Parse(financeBlock.Cost);
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
            case 6:
                try {
                    totalCategory7 += int.Parse(financeBlock.Cost);
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
            case 7:
                try {
                    totalCategory8 += int.Parse(financeBlock.Cost);
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
            case 8:
                try {
                    totalCategory9 += int.Parse(financeBlock.Cost);
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

        totalAllExpenses = totalCategory1 + totalCategory2 + totalCategory3 + totalCategory4 + totalCategory5 + totalCategory6 + totalCategory7 + totalCategory8;

        totalPercentageCategory1 = Math.Round((double)(100 * totalCategory1) / totalAllExpenses, 2);
        totalPercentageCategory2 = Math.Round((double)(100 * totalCategory2) / totalAllExpenses, 2);
        totalPercentageCategory3 = Math.Round((double)(100 * totalCategory3) / totalAllExpenses, 2);
        totalPercentageCategory4 = Math.Round((double)(100 * totalCategory4) / totalAllExpenses, 2);
        totalPercentageCategory5 = Math.Round((double)(100 * totalCategory5) / totalAllExpenses, 2);
        totalPercentageCategory6 = Math.Round((double)(100 * totalCategory6) / totalAllExpenses, 2);
        totalPercentageCategory7 = Math.Round((double)(100 * totalCategory7) / totalAllExpenses, 2);
        totalPercentageCategory8 = Math.Round((double)(100 * totalCategory8) / totalAllExpenses, 2);
        totalPercentageCategory9 = Math.Round((double)(100 * totalCategory9) / totalAllExpenses, 2);

        ReferenceValues.JsonFinanceMaster.Category1Total = totalCategory1;
        ReferenceValues.JsonFinanceMaster.Category2Total = totalCategory2;
        ReferenceValues.JsonFinanceMaster.Category3Total = totalCategory3;
        ReferenceValues.JsonFinanceMaster.Category4Total = totalCategory4;
        ReferenceValues.JsonFinanceMaster.Category5Total = totalCategory5;
        ReferenceValues.JsonFinanceMaster.Category6Total = totalCategory6;
        ReferenceValues.JsonFinanceMaster.Category7Total = totalCategory7;
        ReferenceValues.JsonFinanceMaster.Category8Total = totalCategory8;
        ReferenceValues.JsonFinanceMaster.Category9Total = totalCategory9;
        ReferenceValues.JsonFinanceMaster.Category1Percentage = totalPercentageCategory1;
        ReferenceValues.JsonFinanceMaster.Category2Percentage = totalPercentageCategory2;
        ReferenceValues.JsonFinanceMaster.Category3Percentage = totalPercentageCategory3;
        ReferenceValues.JsonFinanceMaster.Category4Percentage = totalPercentageCategory4;
        ReferenceValues.JsonFinanceMaster.Category5Percentage = totalPercentageCategory5;
        ReferenceValues.JsonFinanceMaster.Category6Percentage = totalPercentageCategory6;
        ReferenceValues.JsonFinanceMaster.Category7Percentage = totalPercentageCategory7;
        ReferenceValues.JsonFinanceMaster.Category8Percentage = totalPercentageCategory8;
        ReferenceValues.JsonFinanceMaster.Category9Percentage = totalPercentageCategory9;

        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock1,
            Percentage = totalPercentageCategory1,
            Amount = totalCategory1
        });
        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock2,
            Percentage = totalPercentageCategory2,
            Amount = totalCategory2
        });
        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock3,
            Percentage = totalPercentageCategory3,
            Amount = totalCategory3
        });
        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock4,
            Percentage = totalPercentageCategory4,
            Amount = totalCategory4
        });
        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock5,
            Percentage = totalPercentageCategory5,
            Amount = totalCategory5
        });
        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock6,
            Percentage = totalPercentageCategory6,
            Amount = totalCategory6
        });
        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock7,
            Percentage = totalPercentageCategory7,
            Amount = totalCategory7
        });
        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock8,
            Percentage = totalPercentageCategory8,
            Amount = totalCategory8
        });
        DetailedFinanceBlock.Add(new DetailedFinanceBlock {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock9,
            Percentage = totalPercentageCategory9,
            Amount = totalCategory9
        });

        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DetailedFinanceBlock);
        view.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
        view.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Ascending));
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

    public ObservableCollection<DetailedFinanceBlock> DetailedFinanceBlock {
        get => _detailedFinanceBlock;
        set {
            _detailedFinanceBlock = value;
            RaisePropertyChangedEvent("DetailedFinanceBlock");
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

    public int CategoryID {
        get => _categoryID;
        set {
            _categoryID = value;
            RaisePropertyChangedEvent("CategoryID");
        }
    }

    #endregion
}