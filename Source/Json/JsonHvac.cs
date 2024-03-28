using System;
using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonHvac {
    public ReferenceValues.HvacStates HvacState { get; set; }
    public int HvacStateTime { get; set; }
    public ObservableCollection<HvacEvent> HvacEvents { get; set; }
}

public class HvacEvent {
    public DateTime EventTime { get; set; }
    public string EventDayOfWeek { get; set; }
    public int EventTemp { get; set; }
}