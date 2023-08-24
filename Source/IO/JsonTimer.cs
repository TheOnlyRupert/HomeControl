namespace HomeControl.Source.IO;

public class JsonTimer {
    public bool IsTimer1Running { get; set; }
    public bool IsTimer2Running { get; set; }
    public bool IsTimer3Running { get; set; }
    public bool IsTimer4Running { get; set; }

    public int Timer1Seconds { get; set; }
    public int Timer2Seconds { get; set; }
    public int Timer3Seconds { get; set; }
    public int Timer4Seconds { get; set; }

    public bool IsAlarmSounding { get; set; }
}