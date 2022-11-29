using System.Collections.ObjectModel;

namespace HomeControl.Source.Helpers;

public class JsonChores {
    public ObservableCollection<ChoreDetails> choreList { get; set; }
}

public class ChoreDetails {
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public string Date { get; set; }
}