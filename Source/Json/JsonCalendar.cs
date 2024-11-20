using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonCalendar {
    public ObservableCollection<CalendarDates> DatesList { get; set; }
    public ObservableCollection<CalendarEventsRecurring> EventsListRecurring { get; set; }
}

public class CalendarDates {
    /* Date in format: yyyy-MM-dd */
    public string Date { get; set; }
    public ObservableCollection<CalendarEvents> EventsList { get; set; }
}

public class CalendarEvents {
    public string EventName { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int UserId { get; set; }
    public int Priority { get; set; }
    public string Image { get; set; }
}

public class CalendarEventsCustom {
    public string Image { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
}

public class CalendarEventsRecurring {
    public DateTime Date { get; set; }
    public string EventText { get; set; }
    public string Image { get; set; }
}