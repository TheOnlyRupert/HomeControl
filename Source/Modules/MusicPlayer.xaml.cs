using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class MusicPlayer {
    public MusicPlayer() {
        InitializeComponent();
        DataContext = new MusicPlayerVM();
    }
}