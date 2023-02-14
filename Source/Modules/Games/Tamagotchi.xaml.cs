using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games;

public partial class Tamagotchi {
    public Tamagotchi() {
        InitializeComponent();
        DataContext = new TamagotchiVM();
    }
}