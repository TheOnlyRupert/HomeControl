using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games;

public partial class Nhie {
    public Nhie() {
        InitializeComponent();
        DataContext = new NhieVM();
    }
}