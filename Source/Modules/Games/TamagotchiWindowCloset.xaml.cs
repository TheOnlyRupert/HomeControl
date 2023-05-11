using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games;

public partial class TamagotchiWindowCloset {
    public TamagotchiWindowCloset() {
        InitializeComponent();
        DataContext = new TamagotchiWindowClosetVM();
    }
}