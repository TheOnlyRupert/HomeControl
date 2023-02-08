using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonFinances {
    public ObservableCollection<FinanceBlock> financeList { get; set; }
}

public class FinanceBlock {
    public string AddSub { get; set; }
    public string Date { get; set; }
    public string Item { get; set; }
    public string Cost { get; set; }
    public string Category { get; set; }
    public string Person { get; set; }
}