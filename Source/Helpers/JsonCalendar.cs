using System;
using System.Collections.Generic;

namespace HomeControl.Source.Helpers;

public class JsonCalendar {
    public List<Events> eventsList { get; set; }
}

public class Events {
    public DateTime date { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string location { get; set; }
    public string person { get; set; }
}