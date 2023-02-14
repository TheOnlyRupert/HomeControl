using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games;

public partial class TamagotchiFeed {
    public TamagotchiFeed() {
        InitializeComponent();
        DataContext = new TamagotchiFeedVM();
    }
}