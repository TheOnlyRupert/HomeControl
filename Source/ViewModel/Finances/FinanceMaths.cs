using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Windows.Data;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public static class FinanceMaths {
    private static CrossViewMessenger simpleMessenger;

    public static void RefreshFinances() {
        simpleMessenger = CrossViewMessenger.Instance;
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Clear();
        int totalCategory1 = 0;
        int totalCategory2 = 0;
        int totalCategory3 = 0;
        int totalCategory4 = 0;
        int totalCategory5 = 0;
        int totalCategory6 = 0;
        int totalCategory7 = 0;
        int totalCategory8 = 0;
        int totalCategory9 = 0;

        foreach (FinanceBlock financeBlock in ReferenceValues.JsonFinanceMaster.FinanceList) {
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

        int totalAllExpenses = totalCategory1 + totalCategory2 + totalCategory3 + totalCategory4 + totalCategory5 + totalCategory6 + totalCategory7 + totalCategory8 + totalCategory9;
        double totalPercentageCategory1 = Math.Round((double)(100 * totalCategory1) / totalAllExpenses, 2);
        double totalPercentageCategory2 = Math.Round((double)(100 * totalCategory2) / totalAllExpenses, 2);
        double totalPercentageCategory3 = Math.Round((double)(100 * totalCategory3) / totalAllExpenses, 2);
        double totalPercentageCategory4 = Math.Round((double)(100 * totalCategory4) / totalAllExpenses, 2);
        double totalPercentageCategory5 = Math.Round((double)(100 * totalCategory5) / totalAllExpenses, 2);
        double totalPercentageCategory6 = Math.Round((double)(100 * totalCategory6) / totalAllExpenses, 2);
        double totalPercentageCategory7 = Math.Round((double)(100 * totalCategory7) / totalAllExpenses, 2);
        double totalPercentageCategory8 = Math.Round((double)(100 * totalCategory8) / totalAllExpenses, 2);
        double totalPercentageCategory9 = Math.Round((double)(100 * totalCategory9) / totalAllExpenses, 2);

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

        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock1,
            Percentage = totalPercentageCategory1,
            Amount = totalCategory1
        });
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock2,
            Percentage = totalPercentageCategory2,
            Amount = totalCategory2
        });
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock3,
            Percentage = totalPercentageCategory3,
            Amount = totalCategory3
        });
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock4,
            Percentage = totalPercentageCategory4,
            Amount = totalCategory4
        });
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock5,
            Percentage = totalPercentageCategory5,
            Amount = totalCategory5
        });
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock6,
            Percentage = totalPercentageCategory6,
            Amount = totalCategory6
        });
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock7,
            Percentage = totalPercentageCategory7,
            Amount = totalCategory7
        });
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock8,
            Percentage = totalPercentageCategory8,
            Amount = totalCategory8
        });
        ReferenceValues.JsonFinanceMaster.FinanceListDetailed.Add(new FinanceBlockDetailed {
            Category = ReferenceValues.JsonSettingsMaster.FinanceBlock9,
            Percentage = totalPercentageCategory9,
            Amount = totalCategory9
        });

        IOrderedEnumerable<FinanceBlock> orderByResult = from s in ReferenceValues.JsonFinanceMaster.FinanceList orderby s.Date select s;
        ReferenceValues.JsonFinanceMaster.FinanceList = new ObservableCollection<FinanceBlock>(orderByResult.ToList());

        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ReferenceValues.JsonFinanceMaster.FinanceListDetailed);
        view.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
        view.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Ascending));

        ReferenceValues.JsonFinanceMaster.TotalAmount = ReferenceValues.JsonFinanceMaster.TotalMonthlyAmount - totalCategory1 + totalCategory2 + totalCategory3 + totalCategory4 + totalCategory5 +
                                                        totalCategory6 + totalCategory7 + totalCategory8 + totalCategory9;

        try {
            double math = Convert.ToDouble(totalCategory1 + totalCategory2 + totalCategory3 + totalCategory4 + totalCategory5 + totalCategory6 + totalCategory7 + totalCategory8 + totalCategory9) /
                Convert.ToDouble(ReferenceValues.JsonFinanceMaster.TotalMonthlyAmount) * 100;
            ReferenceValues.JsonFinanceMaster.TotalPercentage = (int)math;
        } catch (DivideByZeroException) {
            ReferenceValues.JsonFinanceMaster.TotalPercentage = 0;
        }

        simpleMessenger.PushMessage("RefreshFinances", null);

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