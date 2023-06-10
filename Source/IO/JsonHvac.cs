using System;

namespace HomeControl.Source.IO;

public class JsonHvac {
    public DateTime Updated { get; set; }

    /* 0 = off, 1 = standby, 2 = running */
    public int ProgramState { get; set; }
    public bool IsFanOnMode { get; set; }
    public bool IsHeatingMode { get; set; }
    public int TemperatureSet { get; set; }
}