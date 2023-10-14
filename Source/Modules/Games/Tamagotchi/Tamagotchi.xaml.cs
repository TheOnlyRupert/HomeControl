using HomeControl.Source.ViewModel.Games.Tamagotchi;

namespace HomeControl.Source.Modules.Games.Tamagotchi;

public partial class Tamagotchi {
    public Tamagotchi() {
        InitializeComponent();
        DataContext = new TamagotchiVM();
    }
}