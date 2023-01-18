using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games;

public partial class CoinFlip {
    public CoinFlip() {
        InitializeComponent();
        DataContext = new CoinFlipVM();
    }
}