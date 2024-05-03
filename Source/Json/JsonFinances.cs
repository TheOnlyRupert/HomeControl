using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonFinances {
    public double Category1Percentage { get; set; }
    public int Category1Total { get; set; }
    public double Category2Percentage { get; set; }
    public int Category2Total { get; set; }
    public double Category3Percentage { get; set; }
    public int Category3Total { get; set; }
    public double Category4Percentage { get; set; }
    public int Category4Total { get; set; }
    public double Category5Percentage { get; set; }
    public int Category5Total { get; set; }
    public double Category6Percentage { get; set; }
    public int Category6Total { get; set; }
    public double Category7Percentage { get; set; }
    public int Category7Total { get; set; }
    public double Category8Percentage { get; set; }
    public int Category8Total { get; set; }
    public double Category9Percentage { get; set; }
    public int Category9Total { get; set; }
    public int TotalAmount { get; set; }
    public int TotalMonthlyAmount { get; set; }
    public double TotalPercentage { get; set; }
    public ObservableCollection<FinanceBlock> FinanceList { get; set; }
    public ObservableCollection<FinanceBlockDetailed> FinanceListDetailed { get; set; }
}

public class FinanceBlock {
    public string Date { get; set; }
    public string Item { get; set; }
    public string Cost { get; set; }
    public string Category { get; set; }
    public int CategoryID { get; set; }
    public string Details { get; set; }
    public string Image { get; set; }
    public int UserId { get; set; }
}

public class FinanceBlockDetailed {
    public string Category { get; set; }
    public double Percentage { get; set; }
    public int Amount { get; set; }
}