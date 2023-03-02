using System;
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

public class JsonRecurringFinances {
    public ObservableCollection<RecurringFinanceBlock> recurringFinanceList { get; set; }
    public DateTime dateUpdated { get; set; }
}


public class RecurringFinanceBlock {
    public string RecurringMonth { get; set; }
    public int RecurringDay { get; set; }
    public string Item { get; set; }
    public string Cost { get; set; }
    public string Person { get; set; }
}