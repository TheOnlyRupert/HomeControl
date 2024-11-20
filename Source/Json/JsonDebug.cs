using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonDebug {
    public ObservableCollection<DebugTextBlock> DebugBlockList { get; set; }
}

public class DebugTextBlock {
    public DateTime Date { get; set; }
    public string Level { get; set; }
    public string Module { get; set; }
    public string Description { get; set; }
}