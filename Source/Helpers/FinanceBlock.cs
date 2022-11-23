﻿using System.Collections.ObjectModel;

namespace HomeControl.Source.Helpers;

public class JsonFinances {
    public ObservableCollection<FinanceBlock> financeList { get; set; }
}

public class FinanceBlock {
    public string AddSub { get; set; }
    public string Date { get; set; }
    public string Item { get; set; }
    public int Cost { get; set; }
    public string Category { get; set; }
    public string Person { get; set; }
}