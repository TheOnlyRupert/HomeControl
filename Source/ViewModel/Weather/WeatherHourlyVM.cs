using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
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
        UpdateWeatherForecastHourlyPart1();

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
        if (e.PropertyName == "Refresh" && updateForecastHourly) {
            UpdateWeatherForecastHourlyPart1();
        }

        if (e.PropertyName == "MinChanged") {
            updateForecastHourly = true;
        }
    }

    private void UpdateWeatherForecastHourlyPart1() {
        if (ReferenceValues.EnableWeather) {
            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            try {
                using WebClient client2 = new();
                Uri weatherForecastHourlyURL = new("https://api.weather.gov/gridpoints/OHX/42,62/forecast/hourly");
                client2.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.UserAgent);
                string weatherForecastHourly = client2.DownloadString(weatherForecastHourlyURL);
                JsonWeatherForecastHourly forecastHourly = JsonSerializer.Deserialize<JsonWeatherForecastHourly>(weatherForecastHourly, options);

                ForecastHourlyList.Clear();

                if (forecastHourly != null) {
                    foreach (JsonWeatherForecastHourly.Periods period in forecastHourly.properties.periods) {
                        ForecastHourlyList.Add(new WeatherHourlyBlock {
                            Time = period.startTime.ToString("HH:mm"),
                            WeatherIcon = GetWeatherIcon(period.shortForecast, period.isDaytime),
                            Temp = period.temperature + "°",
                            RainIcon = GetRainIcon(period.shortForecast),
                            RainChance = GetRainChance(period.icon),
                            WindSpeed = period.windSpeed,
                            WindDirectionIcon = GetWindRotation(period.windDirection)
                        });
                    }
                }

                updateForecastHourly = false;
            } catch (Exception) { }
        }
    }

    private string GetRainIcon(string input) {
        switch (input) {
        case "Snow Showers":
        case "Chance Snow Showers":
        case "Chance Rain And Snow Showers":
        case "Scattered Snow Showers":
        case "Isolated Snow Showers":
            return "../../Resources/Images/weather/snow_drop.png";
        default:
            return "../../../Resources/Images/weather/rain_drop.png";
        }
    }

    private string GetWeatherIcon(string weather, bool isDayTime) {
        switch (weather) {
        case "Sunny":
        case "Mostly Sunny":
        case "Clear":
        case "Mostly Clear":
            return isDayTime
                ? "../../../Resources/Images/weather/weather_clear.png"
                : "../../../Resources/Images/weather/weather_clear_night.png";
        case "Partly Cloudy":
        case "Partly Sunny":
            return isDayTime
                ? "../../../Resources/Images/weather/weather_part_cloudy.png"
                : "../../../Resources/Images/weather/weather_cloudy_night.png";
        case "Cloudy":
        case "Mostly Cloudy":
            return "../../../Resources/Images/weather/weather_cloudy.png";
        case "Patchy Fog":
        case "Areas Of Fog":
            return "../../../Resources/Images/weather/weather_fog.png";
        case "Slight Chance Very Light Rain":
        case "Slight Chance Light Rain":
        case "Chance Very Light Rain":
        case "Chance Light Rain":
        case "Areas Of Drizzle":
            return "../../../Resources/Images/weather/weather_rain_light.png";
        case "Rain Showers Likely":
        case "Rain Likely":
        case "Rain Showers":
        case "Chance Rain Showers":
        case "Slight Chance Rain Showers":
            return "../../../Resources/Images/weather/weather_rain_medium.png";
        case "Showers And Thunderstorms":
        case "Showers And Thunderstorms Likely":
        case "Chance Showers And Thunderstorms":
        case "Slight Chance Showers And Thunderstorms":
            return "../../../Resources/Images/weather/weather_storm.png";
        case "Widespread Frost":
            return "../../../Resources/Images/weather/weather_frost.png";
        case "Snow Showers":
        case "Scattered Snow Showers":
        case "Chance Snow Showers":
        case "Isolated Snow Showers":
            return "../../../Resources/Images/weather/weather_snow.png";
        case "Chance Rain And Snow Showers":
            return "../../../Resources/Images/weather/weather_snow_rain_mixed.png";
        default:
            return "null";
        }
    }

    private string GetRainChance(string icon) {
        Regex rg = new(@"[,]\d+");
        string output = "";

        foreach (Match match in rg.Matches(icon)) {
            output += match.Value.Substring(1) + "% - ";
        }

        if (string.IsNullOrEmpty(output)) {
            return "0%";
        }

        return output.Substring(0, output.Length - 2);
    }

    private int GetWindRotation(string direction) {
        switch (direction) {
        case "N":
            return 0;
        case "S":
            return 180;
        case "E":
            return 90;
        case "W":
            return 270;
        case "NE":
            return 45;
        case "SE":
            return 135;
        case "SW":
            return 225;
        case "NW":
            return 315;
        case "SSE":
            return 158;
        case "SSW":
            return 202;
        case "ENE":
            return 68;
        case "ESE":
            return 112;
        case "NNW":
            return 338;
        case "NNE":
            return 22;
        case "WSW":
            return 248;
        case "WNW":
            return 292;
        default:
            return 0;
        }
    }
}