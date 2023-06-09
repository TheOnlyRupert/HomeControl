using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonWeatherHourlyBlock {
    public ObservableCollection<FinanceBlock> WeatherHourlyBlock { get; set; }
}

public class WeatherHourlyBlock {
    public string Time { get; set; }
    public string WeatherIcon { get; set; }
    public string Temp { get; set; }
    public string RainIcon { get; set; }
    public string RainChance { get; set; }
    public string WindSpeed { get; set; }
    public int WindDirectionIcon { get; set; }
    public string Humidity { get; set; }
    public string ShortForecast { get; set; }
}