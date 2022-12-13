using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonCalendar {
    public ObservableCollection<CalendarEvents> eventsList { get; set; }
}

public class CalendarEvents {
    public string name { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
    public string description { get; set; }
    public string location { get; set; }
    public string person { get; set; }
}