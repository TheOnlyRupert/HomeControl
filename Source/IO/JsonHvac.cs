namespace HomeControl.Source.IO;

public class JsonHvac {
    /* 0 = off, 1 = standby, 2 = running */
    public bool IsProgramRunning { get; set; }
    public bool IsStandby { get; set; }
    public bool IsFanAuto { get; set; }
    public bool IsHeatingMode { get; set; }
    public int TemperatureSet { get; set; }
}