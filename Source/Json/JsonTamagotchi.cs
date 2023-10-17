namespace HomeControl.Source.Json;

public class Tamagotchi {
    public string Name { get; set; }

    public bool IsMale { get; set; }
    public bool IsLightOn { get; set; }
    public bool IsSleeping { get; set; }
    public bool IsBusy { get; set; }

    /*
     * 100 - 90 is an egg
     * 89 - 80 is a baby
     * 79 -
     */
    public double Age { get; set; }
    public double Health { get; set; }
    public double Hunger { get; set; }
    public double Happiness { get; set; }
    public double Fatigue { get; set; }
    public double Anxiety { get; set; }
    public double Bladder { get; set; }
    public double Weight { get; set; }
    public double Cleanliness { get; set; }

    /*
     * Health multipliers start at 1 and are worse for stats as the multiplier increase.
     * Examples:
     * If HealthMultiplier is 1 then 0.0001 of Age is lost every tick (standard).
     * If HealthMultiplier is 2 then 0.0002 of Age is lost every tick (It will live for half has long).
     */
    public double HealthMultiplier { get; set; }
    public double HungerMultiplier { get; set; }
    public double HappinessMultiplier { get; set; }
    public double FatigueMultiplier { get; set; }
    public double AnxietyMultiplier { get; set; }
    public double BladderMultiplier { get; set; }
    public double WeightMultiplier { get; set; }
    public double CleanlinessMultiplier { get; set; }

    public bool ReverseHealth { get; set; }
    public bool ReverseHunger { get; set; }
    public bool ReverseHappiness { get; set; }
    public bool ReverseFatigue { get; set; }
    public bool ReverseAnxiety { get; set; }
    public bool ReverseBladder { get; set; }
    public bool ReverseWeight { get; set; }
    public bool ReverseCleanliness { get; set; }

    public int ReverseHealthDuration { get; set; }
    public int ReverseHungerDuration { get; set; }
    public int ReverseHappinessDuration { get; set; }
    public int ReverseFatigueDuration { get; set; }
    public int ReverseAnxietyDuration { get; set; }
    public int ReverseBladderDuration { get; set; }
    public int ReverseWeightDuration { get; set; }
    public int ReverseCleanlinessDuration { get; set; }
}