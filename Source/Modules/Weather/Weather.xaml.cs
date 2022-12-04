using HomeControl.Source.ViewModel.Weather;

namespace HomeControl.Source.Modules.Weather;

public partial class Weather {
    public Weather() {
        InitializeComponent();
        DataContext = new WeatherVM();
    }
}