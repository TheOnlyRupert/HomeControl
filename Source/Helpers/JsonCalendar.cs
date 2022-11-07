using System;
using System.Collections.Generic;

namespace HomeControl.Source.Helpers; 

public class JsonCalendar {
    public List<Events> eventsList { get; set; }
}

public class Events {
    public DateTime date { get; set; }
    public string eventName { get; set; }

    public string description { get; set; }

    /* 0 = Default (black), 1 = Father (blue), 2 = Mother (green), 3 = Children (gray) */
    public int personID { get; set; }
}