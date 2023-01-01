using System;

namespace HomeControl.Source.IO;

public class JsonChoreFunds {
    public DateTime UpdatedDate { get; set; }
    public int FundsAvailable { get; set; }
    public int FundsLocked { get; set; }
    public bool SpecialDay1Completed { get; set; }
    public bool SpecialDay2Completed { get; set; }
    public string CurrentMode { get; set; }
}