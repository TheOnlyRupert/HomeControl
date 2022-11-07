using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules; 

public partial class Weather {
    public Weather() {
        InitializeComponent();
        DataContext = new WeatherVM();
    }
}