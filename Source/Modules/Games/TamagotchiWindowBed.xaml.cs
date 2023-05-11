using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games;

public partial class TamagotchiWindowBed {
    public TamagotchiWindowBed() {
        InitializeComponent();
        DataContext = new TamagotchiWindowBedVM();
    }
}