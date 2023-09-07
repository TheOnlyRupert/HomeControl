using System;
using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonCalendar {
    public ObservableCollection<CalendarEvents> eventsList { get; set; }
    public ObservableCollection<CalendarEventsRecurring> eventsListRecurring { get; set; }
}

public class CalendarEvents {
    public DateTime Date { get; set; }
    public string EventName { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int UserId { get; set; }
    public int Priority { get; set; }
}

public class CalendarEventsCustom {
    public string Icon { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
}

public class CalendarEventsRecurring {
    public DateTime Date { get; set; }
    public string EventText { get; set; }
}