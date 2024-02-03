namespace HomeControl.Source.Json;

public class JsonWorkout {
    public int TimerMain { get; set; }
    public int TimerState { get; set; }
    public CurrentState CurrentState { get; set; }
    public int Difficulty { get; set; }
    public string Soundtrack { get; set; }
}

/* External file*/
public class JsonWorkoutStats { }

public class JsonWorkoutType {
    public string Name { get; set; }
    public int Difficulty { get; set; }
    public BodyPart BodyPart { get; set; }
    public bool NeedsEquipment { get; set; }
    public int MinTime { get; set; }
    public int MaxTime { get; set; }
    public bool CanHold { get; set; }
    public int MinHoldTime { get; set; }
    public int MaxHoldTime { get; set; }
}

public enum BodyPart {
    ARMS,
    LEGS,
    CHEST,
    BACK,
    ABS,
    SHOULDERS
}

public enum CurrentState {
    PAUSED,
    COOLDOWN,
    ACTIVE,
    REST
}