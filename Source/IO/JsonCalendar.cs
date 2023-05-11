using System;
using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonCalendar {
    public ObservableCollection<CalendarEvents> eventsList { get; set; }
}

public class JsonCalendarRecurring {
    public ObservableCollection<CalendarEventsRecurring> eventsListRecurring { get; set; }
}

public class CalendarEvents {
    public string name { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
    public string description { get; set; }
    public string location { get; set; }
    public string person { get; set; }
}

public class CalendarEventsCustom {
    public string name { get; set; }
    public int person { get; set; }
}

public class CalendarEventsRecurring {
    public DateTime date { get; set; }
    public string eventText { get; set; }
}