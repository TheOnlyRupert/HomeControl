namespace HomeControl.Source.Json;

public class JsonHvac {
    public bool IsOverride { get; set; }
    public bool IsProgramRunning { get; set; }
    public bool IsStandby { get; set; }
    public bool IsFanAuto { get; set; }
    public bool IsHeatingMode { get; set; }
    public int TemperatureSet { get; set; }
}