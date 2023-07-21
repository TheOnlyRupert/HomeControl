using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonFinances {
    public ObservableCollection<FinanceBlock> financeList { get; set; }
    public int User1Funds { get; set; }
    public int User2Funds { get; set; }
    public int User3Funds { get; set; }
    public int User4Funds { get; set; }
    public int User5Funds { get; set; }
}

public class FinanceBlock {
    public string AddSub { get; set; }
    public string Date { get; set; }
    public string Item { get; set; }
    public string Cost { get; set; }
    public string Category { get; set; }
    public string Person { get; set; }
    public string Details { get; set; }
}

public class DetailedFinanceBlock {
    public string Category { get; set; }
    public double Percentage { get; set; }
    public int Amount { get; set; }
}