using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonFinances {
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