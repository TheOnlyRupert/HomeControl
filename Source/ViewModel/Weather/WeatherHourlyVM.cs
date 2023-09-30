using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
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
                Uri weatherForecastHourlyURL =
                    new(
                        $"https://api.weather.gov/gridpoints/{ReferenceValues.JsonSettingsMaster.GridId}/{ReferenceValues.JsonSettingsMaster.GridX},{ReferenceValues.JsonSettingsMaster.GridY}/forecast/hourly");
                client2.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonSettingsMaster.UserAgent);
                string weatherForecastHourly = client2.DownloadString(weatherForecastHourlyURL);
                JsonWeather forecast = JsonSerializer.Deserialize<JsonWeather>(weatherForecastHourly, options);

                ForecastHourlyList.Clear();

                if (forecast != null) {
                    foreach (JsonWeather.Periods period in forecast.properties.periods) {
                        ForecastHourlyList.Add(new WeatherHourlyBlock {
                            Time = period.startTime.ToString("MM/dd  ") + period.startTime.ToString("HH:mm"),
                            WeatherIcon = WeatherHelpers.GetWeatherIcon(period.shortForecast, period.isDaytime, period.temperature, period.windSpeed, "null"),
                            Temp = period.temperature + "°",
                            RainIcon = WeatherHelpers.GetRainIcon(period.shortForecast),
                            RainChance = period.probabilityOfPrecipitation.value + "%",
                            WindSpeed = period.windSpeed,
                            WindDirectionIcon = WeatherHelpers.GetWindRotation(period.windDirection),
                            Humidity = "Humidity: " + period.relativeHumidity.value + "%",
                            ShortForecast = period.shortForecast
                        });
                    }
                }

                updateForecastHourly = false;
            } catch (WebException) {
                // NORMAL
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "WeatherHourlyVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }
        }
    }
}