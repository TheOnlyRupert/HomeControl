using System;
using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonTamagotchi {
    public ObservableCollection<Tamagotchi> tamagotchiList { get; set; }
}

public class Tamagotchi {
    public DateTime LastUpdated { get; set; }
    public DateTime HatchDate { get; set; }
    public string Name { get; set; }
    public double Health { get; set; }
    public double Hunger { get; set; }
    public double Happiness { get; set; }
    public double Training { get; set; }
    public double Discipline { get; set; }
    public double Tiredness { get; set; }
    public int DroppingAmount { get; set; }
    public int Weight { get; set; }
    public bool IsMale { get; set; }
}