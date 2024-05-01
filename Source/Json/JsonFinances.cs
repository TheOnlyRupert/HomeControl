using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonFinances {
    public double Category1Percentage;
    public int Category1Total;
    public double Category2Percentage;
    public int Category2Total;
    public double Category3Percentage;
    public int Category3Total;
    public double Category4Percentage;
    public int Category4Total;
    public double Category5Percentage;
    public int Category5Total;
    public double Category6Percentage;
    public int Category6Total;
    public double Category7Percentage;
    public int Category7Total;
    public double Category8Percentage;
    public int Category8Total;
    public double Category9Percentage;
    public int Category9Total;
    public ObservableCollection<FinanceBlock> financeList { get; set; }
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

public class DetailedFinanceBlock {
    public string Category { get; set; }
    public double Percentage { get; set; }
    public int Amount { get; set; }
}