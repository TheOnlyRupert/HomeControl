using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games;

public partial class TrafficLight {
    public TrafficLight() {
        InitializeComponent();
        DataContext = new TrafficLightVM();
    }
}