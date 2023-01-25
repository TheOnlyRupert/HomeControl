using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Weather;

public class WeatherHourlyVM : BaseViewModel {
    private ObservableCollection<WeatherHourlyBlock> _forecastHourlyList;
    private bool updateForecastHourly;

    public WeatherHourlyVM() {
        ForecastHourlyList = new ObservableCollection<WeatherHourlyBlock>();
        updateForecastHourly = true;
        UpdateWeatherForecastHourly();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    #region Fields

    public ObservableCollection<WeatherHourlyBlock> ForecastHourlyList {
        get => _forecastHourlyList;
        set {
            _forecastHourlyList = value;
            RaisePropertyChangedEvent("ForecastHourlyList");
        }
    }

    #endregion

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "Refresh" when updateForecastHourly:
            UpdateWeatherForecastHourly();
            break;
        case "MinChanged":
            updateForecastHourly = true;
            break;
        }
    }

    private void UpdateWeatherForecastHourly() {
        if (ReferenceValues.EnableWeather) {
            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            try {
                using WebClient client2 = new();
                Uri weatherForecastHourlyURL = new("https://api.weather.gov/gridpoints/OHX/42,62/forecast/hourly");
                client2.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonMasterSettings.UserAgent);
                string weatherForecastHourly = client2.DownloadString(weatherForecastHourlyURL);
                JsonWeatherForecastHourly forecastHourly = JsonSerializer.Deserialize<JsonWeatherForecastHourly>(weatherForecastHourly, options);

                ForecastHourlyList.Clear();

                if (forecastHourly != null) {
                    foreach (JsonWeatherForecastHourly.Periods period in forecastHourly.properties.periods) {
                        ForecastHourlyList.Add(new WeatherHourlyBlock {
                            Time = period.startTime.ToString("MM/dd  ") + period.startTime.ToString("HH:mm"),
                            WeatherIcon = WeatherHelpers.GetWeatherIcon(period.shortForecast, period.isDaytime),
                            Temp = period.temperature + "°",
                            RainIcon = WeatherHelpers.GetRainIcon(period.shortForecast),
                            RainChance = WeatherHelpers.GetRainChance(period.icon),
                            WindSpeed = period.windSpeed,
                            WindDirectionIcon = WeatherHelpers.GetWindRotation(period.windDirection)
                        });
                    }
                }

                updateForecastHourly = false;
            } catch (Exception) { }
        }
    }
}