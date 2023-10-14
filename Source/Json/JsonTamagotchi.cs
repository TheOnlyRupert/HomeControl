namespace HomeControl.Source.Json;

public class Tamagotchi {
    public string Name { get; set; }

    public bool IsMale { get; set; }
    public bool IsLightOn { get; set; }
    public bool IsSleeping { get; set; }
    public bool IsBusy { get; set; }

    public double Age { get; set; }
    public double Health { get; set; }
    public double Hunger { get; set; }
    public double Happiness { get; set; }
    public double Fatigue { get; set; }
    public double Anxiety { get; set; }
    public double Bladder { get; set; }
    public double Weight { get; set; }
    public double Cleanliness { get; set; }

    public int HealthMultiplier { get; set; }
    public int HungerMultiplier { get; set; }
    public int HappinessMultiplier { get; set; }
    public int FatigueMultiplier { get; set; }
    public int AnxietyMultiplier { get; set; }
    public int BladderMultiplier { get; set; }
    public int WeightMultiplier { get; set; }
    public int CleanlinessMultiplier { get; set; }

    public int HealthMultiplierDuration { get; set; }
    public int HungerMultiplierDuration { get; set; }
    public int HappinessMultiplierDuration { get; set; }
    public int FatigueMultiplierDuration { get; set; }
    public int AnxietyMultiplierDuration { get; set; }
    public int BladderMultiplierDuration { get; set; }
    public int WeightMultiplierDuration { get; set; }
    public int CleanlinessMultiplierDuration { get; set; }

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