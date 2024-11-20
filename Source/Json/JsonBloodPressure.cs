using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonBloodPressure {
    public ObservableCollection<BloodPressure> User1List { get; set; } = [];
    public ObservableCollection<BloodPressure> User2List { get; set; } = [];
    public ObservableCollection<BloodPressure> User3List { get; set; } = [];
    public ObservableCollection<BloodPressure> User4List { get; set; } = [];
    public ObservableCollection<BloodPressure> User5List { get; set; } = [];
}

public class BloodPressure {
    public DateTime Date { get; set; }
    public string PressureText { get; set; }
    public string NoteText { get; set; }
}