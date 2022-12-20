using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonDaily {
    public ObservableCollection<DailyDetails> dailyListUser1 { get; set; }
    public ObservableCollection<DailyDetails> dailyListUser2 { get; set; }
    public ObservableCollection<DailyDetails> dailyListUser3 { get; set; }
    public ObservableCollection<DailyDetails> dailyListUser4 { get; set; }
    public ObservableCollection<DailyDetails> dailyListUser5 { get; set; }
}

public class DailyDetails {
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public string Time { get; set; }
}