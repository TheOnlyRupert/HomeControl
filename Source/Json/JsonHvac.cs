﻿using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonHvac {
    public int HvacStateTime { get; set; }
    public ObservableCollection<HvacEvent> HvacEvents { get; set; }
    public HvacStates HvacState { get; set; }
}

public class HvacEvent {
    public DateTime EventTime { get; set; }
    public string EventDayOfWeek { get; set; }
    public int EventTemp { get; set; }
}

public enum HvacStates {
    Off,
    Running,
    Standby,
    Purging
}