using System.Windows.Controls;
using HomeControl.Source.ViewModel.Weather;

namespace HomeControl.Source.Modules.Weather;

public partial class WeatherHourly {
    public WeatherHourly() {
        InitializeComponent();
        DataContext = new WeatherHourlyVM();
    }
}