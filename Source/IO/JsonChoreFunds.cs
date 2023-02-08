using System;
using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonChoreFunds {
    public DateTime UpdatedDate { get; set; }
    public int FundsAvailable { get; set; }
    public int FundsLocked { get; set; }
    public int FundsPrior { get; set; }
    public int FundsTotal { get; set; }
    public bool SpecialDay1Completed { get; set; }
    public bool SpecialDay2Completed { get; set; }
    public ObservableCollection<FinanceBlockChoreFund> FinanceBlockChoreFundList { get; set; }
}

public class FinanceBlockChoreFund {
    public string Item { get; set; }
    public int Cost { get; set; }
}