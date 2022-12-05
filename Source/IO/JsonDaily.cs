using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonDaily {
    public ObservableCollection<DailyDetails> dailyList { get; set; }
}

public class DailyDetails {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public string Time { get; set; }
}