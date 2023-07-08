using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonChores {
    public ObservableCollection<ChoreDetails> user1ChoreList { get; set; }
    public ObservableCollection<ChoreDetails> user2ChoreList { get; set; }
    public ObservableCollection<ChoreDetails> user3ChoreList { get; set; }
    public ObservableCollection<ChoreDetails> user4ChoreList { get; set; }
    public ObservableCollection<ChoreDetails> user5ChoreList { get; set; }
}

public class ChoreDetails {
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public string Date { get; set; }
}