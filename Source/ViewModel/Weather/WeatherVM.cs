using System;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Weather;

public class WeatherVM : BaseViewModel {
    private int _currentWindDirectionRotation, _sevenDayForecastWindDirectionIcon1, _sevenDayForecastWindDirectionIcon2, _sevenDayForecastWindDirectionIcon3,
        _sevenDayForecastWindDirectionIcon4, _sevenDayForecastWindDirectionIcon5, _sevenDayForecastWindDirectionIcon6, _sevenDayForecastWindDirectionIcon7,
        _sevenDayForecastWindDirectionIcon8, _sevenDayForecastWindDirectionIcon9, _sevenDayForecastWindDirectionIcon10, _sevenDayForecastWindDirectionIcon11,
        _sevenDayForecastWindDirectionIcon12, _sevenDayForecastWindDirectionIcon13, _sevenDayForecastWindDirectionIcon14;

    private string _currentWindSpeedText, _currentWeatherDescription, _currentDateText, _currentTimeText, _currentTimeSecondsText, _currentWeatherLocationText,
        _currentWeatherTempText, _currentWeatherCloudIcon, _sevenDayForecastDescription1, _sevenDayForecastWindSpeed1, _sevenDayForecastWeatherIcon1a,
        _sevenDayForecastWeatherIcon1b, _sevenDayForecastTemp1, _sevenDayForecastName1, _sevenDayForecastDescription2, _sevenDayForecastWindSpeed2, _sevenDayForecastWeatherIcon2a,
        _sevenDayForecastWeatherIcon2b, _sevenDayForecastTemp2, _sevenDayForecastName2, _sevenDayForecastDescription3, _sevenDayForecastWindSpeed3, _sevenDayForecastWeatherIcon3a,
        _sevenDayForecastWeatherIcon3b, _sevenDayForecastTemp3, _sevenDayForecastName3, _sevenDayForecastDescription4, _sevenDayForecastWindSpeed4, _sevenDayForecastWeatherIcon4a,
        _sevenDayForecastWeatherIcon4b, _sevenDayForecastTemp4, _sevenDayForecastName4, _sevenDayForecastDescription5, _sevenDayForecastWindSpeed5, _sevenDayForecastWeatherIcon5a,
        _sevenDayForecastWeatherIcon5b, _sevenDayForecastTemp5, _sevenDayForecastName5, _sevenDayForecastDescription6, _sevenDayForecastWindSpeed6, _sevenDayForecastWeatherIcon6a,
        _sevenDayForecastWeatherIcon6b, _sevenDayForecastTemp6, _sevenDayForecastName6, _sevenDayForecastDescription7, _sevenDayForecastWindSpeed7, _sevenDayForecastWeatherIcon7a,
        _sevenDayForecastWeatherIcon7b, _sevenDayForecastTemp7, _sevenDayForecastName7, _sevenDayForecastDescription8, _sevenDayForecastWindSpeed8, _sevenDayForecastWeatherIcon8a,
        _sevenDayForecastWeatherIcon8b, _sevenDayForecastTemp8, _sevenDayForecastName8, _sevenDayForecastDescription9, _sevenDayForecastWindSpeed9, _sevenDayForecastWeatherIcon9a,
        _sevenDayForecastWeatherIcon9b, _sevenDayForecastTemp9, _sevenDayForecastName9, _sevenDayForecastDescription10, _sevenDayForecastWindSpeed10,
        _sevenDayForecastWeatherIcon10a, _sevenDayForecastWeatherIcon10b, _sevenDayForecastTemp10, _sevenDayForecastName10, _sevenDayForecastDescription11,
        _sevenDayForecastWindSpeed11, _sevenDayForecastWeatherIcon11a, _sevenDayForecastWeatherIcon11b, _sevenDayForecastTemp11, _sevenDayForecastName11,
        _sevenDayForecastDescription12, _sevenDayForecastWindSpeed12, _sevenDayForecastWeatherIcon12a, _sevenDayForecastWeatherIcon12b, _sevenDayForecastTemp12,
        _sevenDayForecastName12, _sevenDayForecastDescription13, _sevenDayForecastWindSpeed13, _sevenDayForecastWeatherIcon13a, _sevenDayForecastWeatherIcon13b,
        _sevenDayForecastTemp13, _sevenDayForecastName13, _sevenDayForecastDescription14, _sevenDayForecastWindSpeed14, _sevenDayForecastWeatherIcon14a,
        _sevenDayForecastWeatherIcon14b, _sevenDayForecastTemp14, _sevenDayForecastName14, _weatherOverlay, _thermometerDisplayIcon, _sevenDayForecastRainChance1,
        _sevenDayForecastRainChance2, _sevenDayForecastRainChance3, _sevenDayForecastRainChance4, _sevenDayForecastRainChance5, _sevenDayForecastRainChance6,
        _sevenDayForecastRainChance7, _sevenDayForecastRainChance8, _sevenDayForecastRainChance9, _sevenDayForecastRainChance10, _sevenDayForecastRainChance11,
        _sevenDayForecastRainChance12, _sevenDayForecastRainChance13, _sevenDayForecastRainChance14;

    private JsonWeatherForecast forecast;
    private bool updateForecast;

    public WeatherVM() {
        updateForecast = true;
        UpdateWeatherForecastPart1();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "Refresh") {
            CurrentDateText = DateTime.Now.ToLongDateString();
            CurrentTimeText = DateTime.Now.ToString("HH:mm");
            CurrentTimeSecondsText = DateTime.Now.ToString("ss");
            if (updateForecast) {
                UpdateWeatherForecastPart1();
            }
        }

        /* Update weather every minute */
        if (e.PropertyName == "MinChanged") {
            updateForecast = true;
        }
    }

    private void UpdateWeatherForecastPart1() {
        if (ReferenceValues.EnableWeather) {
            bool errored = false;
            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            try {
                using WebClient client1 = new();
                Uri weatherForecastURL = new("https://api.weather.gov/gridpoints/OHX/42,62/forecast");
                client1.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.UserAgent);
                string weatherForecast = client1.DownloadString(weatherForecastURL);
                forecast = JsonSerializer.Deserialize<JsonWeatherForecast>(weatherForecast, options);
                updateForecast = false;
            } catch (Exception) {
                errored = true;
            }

            if (!errored) {
                UpdateWeatherForecastPart2();
            }
        }
    }


    private void UpdateWeatherForecastPart2() {
        string[] weatherIcons;

        //TODO: Add more places
        CurrentWeatherLocationText = "Ashland City, TN";
        CurrentWeatherTempText = forecast.properties.periods[0].temperature + "°";
        CurrentWindDirectionRotation = GetWindRotation(forecast.properties.periods[0].windDirection);
        CurrentWindSpeedText = forecast.properties.periods[0].windSpeed;
        CurrentWeatherDescription = forecast.properties.periods[0].shortForecast;
        CurrentWeatherCloudIcon = GetWeatherIcon(forecast.properties.periods[0].shortForecast, forecast.properties.periods[0].isDaytime);

        try {
            SevenDayForecastName1 = forecast.properties.periods[0].name;
            SevenDayForecastTemp1 = forecast.properties.periods[0].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[0].shortForecast);
            SevenDayForecastWeatherIcon1a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[0].isDaytime);
            SevenDayForecastWeatherIcon1b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[0].isDaytime) : "null";
            SevenDayForecastRainChance1 = GetRainChance(forecast.properties.periods[0].icon);
            SevenDayForecastWindDirectionIcon1 = GetWindRotation(forecast.properties.periods[0].windDirection);

            SevenDayForecastWindSpeed1 = forecast.properties.periods[0].windSpeed;
            SevenDayForecastDescription1 = forecast.properties.periods[0].shortForecast;
        } catch (Exception e) { }

        try {
            SevenDayForecastName2 = forecast.properties.periods[1].name;
            SevenDayForecastTemp2 = forecast.properties.periods[1].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[1].shortForecast);
            SevenDayForecastWeatherIcon2a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[1].isDaytime);
            SevenDayForecastWeatherIcon2b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[1].isDaytime) : "null";
            SevenDayForecastRainChance2 = GetRainChance(forecast.properties.periods[1].icon);
            SevenDayForecastWindDirectionIcon2 = GetWindRotation(forecast.properties.periods[1].windDirection);

            SevenDayForecastWindSpeed2 = forecast.properties.periods[1].windSpeed;
            SevenDayForecastDescription2 = forecast.properties.periods[1].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName3 = forecast.properties.periods[2].name;
            SevenDayForecastTemp3 = forecast.properties.periods[2].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[2].shortForecast);
            SevenDayForecastWeatherIcon3a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[2].isDaytime);
            SevenDayForecastWeatherIcon3b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[2].isDaytime) : "null";
            SevenDayForecastRainChance3 = GetRainChance(forecast.properties.periods[2].icon);
            SevenDayForecastWindDirectionIcon3 = GetWindRotation(forecast.properties.periods[2].windDirection);

            SevenDayForecastWindSpeed3 = forecast.properties.periods[2].windSpeed;
            SevenDayForecastDescription3 = forecast.properties.periods[2].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName4 = forecast.properties.periods[3].name;
            SevenDayForecastTemp4 = forecast.properties.periods[3].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[3].shortForecast);
            SevenDayForecastWeatherIcon4a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[3].isDaytime);
            SevenDayForecastWeatherIcon4b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[3].isDaytime) : "null";
            SevenDayForecastRainChance4 = GetRainChance(forecast.properties.periods[3].icon);
            SevenDayForecastWindDirectionIcon4 = GetWindRotation(forecast.properties.periods[3].windDirection);

            SevenDayForecastWindSpeed4 = forecast.properties.periods[3].windSpeed;
            SevenDayForecastDescription4 = forecast.properties.periods[3].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName5 = forecast.properties.periods[4].name;
            SevenDayForecastTemp5 = forecast.properties.periods[4].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[4].shortForecast);
            SevenDayForecastWeatherIcon5a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[4].isDaytime);
            SevenDayForecastWeatherIcon5b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[4].isDaytime) : "null";
            SevenDayForecastRainChance5 = GetRainChance(forecast.properties.periods[4].icon);
            SevenDayForecastWindDirectionIcon5 = GetWindRotation(forecast.properties.periods[4].windDirection);

            SevenDayForecastWindSpeed5 = forecast.properties.periods[4].windSpeed;
            SevenDayForecastDescription5 = forecast.properties.periods[4].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName6 = forecast.properties.periods[5].name;
            SevenDayForecastTemp6 = forecast.properties.periods[5].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[5].shortForecast);
            SevenDayForecastWeatherIcon6a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[5].isDaytime);
            SevenDayForecastWeatherIcon6b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[5].isDaytime) : "null";
            SevenDayForecastRainChance6 = GetRainChance(forecast.properties.periods[5].icon);
            SevenDayForecastWindDirectionIcon6 = GetWindRotation(forecast.properties.periods[5].windDirection);

            SevenDayForecastWindSpeed6 = forecast.properties.periods[5].windSpeed;
            SevenDayForecastDescription6 = forecast.properties.periods[5].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName7 = forecast.properties.periods[6].name;
            SevenDayForecastTemp7 = forecast.properties.periods[6].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[6].shortForecast);
            SevenDayForecastWeatherIcon7a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[6].isDaytime);
            SevenDayForecastWeatherIcon7b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[6].isDaytime) : "null";
            SevenDayForecastRainChance7 = GetRainChance(forecast.properties.periods[6].icon);
            SevenDayForecastWindDirectionIcon7 = GetWindRotation(forecast.properties.periods[6].windDirection);

            SevenDayForecastWindSpeed7 = forecast.properties.periods[6].windSpeed;
            SevenDayForecastDescription7 = forecast.properties.periods[6].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName8 = forecast.properties.periods[7].name;
            SevenDayForecastTemp8 = forecast.properties.periods[7].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[7].shortForecast);
            SevenDayForecastWeatherIcon8a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[7].isDaytime);
            SevenDayForecastWeatherIcon8b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[7].isDaytime) : "null";
            SevenDayForecastRainChance8 = GetRainChance(forecast.properties.periods[7].icon);
            SevenDayForecastWindDirectionIcon8 = GetWindRotation(forecast.properties.periods[7].windDirection);

            SevenDayForecastWindSpeed8 = forecast.properties.periods[7].windSpeed;
            SevenDayForecastDescription8 = forecast.properties.periods[7].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName9 = forecast.properties.periods[8].name;
            SevenDayForecastTemp9 = forecast.properties.periods[8].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[8].shortForecast);
            SevenDayForecastWeatherIcon9a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[8].isDaytime);
            SevenDayForecastWeatherIcon9b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[8].isDaytime) : "null";
            SevenDayForecastRainChance9 = GetRainChance(forecast.properties.periods[8].icon);
            SevenDayForecastWindDirectionIcon9 = GetWindRotation(forecast.properties.periods[8].windDirection);

            SevenDayForecastWindSpeed9 = forecast.properties.periods[8].windSpeed;
            SevenDayForecastDescription9 = forecast.properties.periods[8].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName10 = forecast.properties.periods[9].name;
            SevenDayForecastTemp10 = forecast.properties.periods[9].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[9].shortForecast);
            SevenDayForecastWeatherIcon10a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[9].isDaytime);
            SevenDayForecastWeatherIcon10b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[9].isDaytime) : "null";
            SevenDayForecastRainChance10 = GetRainChance(forecast.properties.periods[9].icon);
            SevenDayForecastWindDirectionIcon10 = GetWindRotation(forecast.properties.periods[9].windDirection);

            SevenDayForecastWindSpeed10 = forecast.properties.periods[9].windSpeed;
            SevenDayForecastDescription10 = forecast.properties.periods[9].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName11 = forecast.properties.periods[10].name;
            SevenDayForecastTemp11 = forecast.properties.periods[10].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[10].shortForecast);
            SevenDayForecastWeatherIcon11a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[10].isDaytime);
            SevenDayForecastWeatherIcon11b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[10].isDaytime) : "null";
            SevenDayForecastRainChance11 = GetRainChance(forecast.properties.periods[10].icon);
            SevenDayForecastWindDirectionIcon11 = GetWindRotation(forecast.properties.periods[10].windDirection);

            SevenDayForecastWindSpeed11 = forecast.properties.periods[10].windSpeed;
            SevenDayForecastDescription11 = forecast.properties.periods[10].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName12 = forecast.properties.periods[11].name;
            SevenDayForecastTemp12 = forecast.properties.periods[11].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[11].shortForecast);
            SevenDayForecastWeatherIcon12a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[11].isDaytime);
            SevenDayForecastWeatherIcon12b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[11].isDaytime) : "null";
            SevenDayForecastRainChance12 = GetRainChance(forecast.properties.periods[11].icon);
            SevenDayForecastWindDirectionIcon12 = GetWindRotation(forecast.properties.periods[11].windDirection);

            SevenDayForecastWindSpeed12 = forecast.properties.periods[11].windSpeed;
            SevenDayForecastDescription12 = forecast.properties.periods[11].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName13 = forecast.properties.periods[12].name;
            SevenDayForecastTemp13 = forecast.properties.periods[12].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[12].shortForecast);
            SevenDayForecastWeatherIcon13a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[12].isDaytime);
            SevenDayForecastWeatherIcon13b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[12].isDaytime) : "null";
            SevenDayForecastRainChance13 = GetRainChance(forecast.properties.periods[12].icon);
            SevenDayForecastWindDirectionIcon13 = GetWindRotation(forecast.properties.periods[12].windDirection);

            SevenDayForecastWindSpeed13 = forecast.properties.periods[12].windSpeed;
            SevenDayForecastDescription13 = forecast.properties.periods[12].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName14 = forecast.properties.periods[13].name;
            SevenDayForecastTemp14 = forecast.properties.periods[13].temperature + "°";

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[13].shortForecast);
            SevenDayForecastWeatherIcon14a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[13].isDaytime);
            SevenDayForecastWeatherIcon14b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[13].isDaytime) : "null";
            SevenDayForecastRainChance14 = GetRainChance(forecast.properties.periods[13].icon);
            SevenDayForecastWindDirectionIcon14 = GetWindRotation(forecast.properties.periods[13].windDirection);

            SevenDayForecastWindSpeed14 = forecast.properties.periods[13].windSpeed;
            SevenDayForecastDescription14 = forecast.properties.periods[13].shortForecast;
        } catch (Exception) { }


        /* Update thermometer image */
        if (forecast.properties.periods[0].temperature < 20) {
            ThermometerDisplayIcon = "../../../Resources/Images/Icons/temp_freezing.png";
            WeatherOverlay = "../../../Resources/Images/weather/cold_border.png";
        } else if (forecast.properties.periods[0].temperature is >= 20 and < 35) {
            ThermometerDisplayIcon = "../../../Resources/Images/Icons/temp_cold.png";
        } else if (forecast.properties.periods[0].temperature is >= 35 and < 60) {
            ThermometerDisplayIcon = "../../../Resources/Images/Icons/temp_cool.png";
        } else if (forecast.properties.periods[0].temperature is >= 60 and < 85) {
            ThermometerDisplayIcon = "../../../Resources/Images/Icons/temp_warm.png";
        } else if (forecast.properties.periods[0].temperature is >= 85 and < 100) {
            ThermometerDisplayIcon = "../../../Resources/Images/Icons/temp_hot.png";
        } else {
            ThermometerDisplayIcon = "../../../Resources/Images/Icons/temp_burning.png";
            WeatherOverlay = "../../../Resources/Images/weather/fire_border.png";
        }
    }

    private string[] RegexWeatherForecast(string input) {
        return input.Split(new[] { " then " }, StringSplitOptions.None);
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

    #region Fields

    public string CurrentDateText {
        get => _currentDateText;
        set {
            _currentDateText = value;
            RaisePropertyChangedEvent("CurrentDateText");
        }
    }

    public string CurrentTimeText {
        get => _currentTimeText;
        set {
            _currentTimeText = value;
            RaisePropertyChangedEvent("CurrentTimeText");
        }
    }

    public string CurrentTimeSecondsText {
        get => _currentTimeSecondsText;
        set {
            _currentTimeSecondsText = value;
            RaisePropertyChangedEvent("CurrentTimeSecondsText");
        }
    }

    public string CurrentWeatherLocationText {
        get => _currentWeatherLocationText;
        set {
            _currentWeatherLocationText = value;
            RaisePropertyChangedEvent("CurrentWeatherLocationText");
        }
    }

    public string CurrentWeatherTempText {
        get => _currentWeatherTempText;
        set {
            _currentWeatherTempText = value;
            RaisePropertyChangedEvent("CurrentWeatherTempText");
        }
    }

    public string CurrentWeatherCloudIcon {
        get => _currentWeatherCloudIcon;
        set {
            _currentWeatherCloudIcon = value;
            RaisePropertyChangedEvent("CurrentWeatherCloudIcon");
        }
    }

    public int CurrentWindDirectionRotation {
        get => _currentWindDirectionRotation;
        set {
            _currentWindDirectionRotation = value;
            RaisePropertyChangedEvent("CurrentWindDirectionRotation");
        }
    }

    public string CurrentWindSpeedText {
        get => _currentWindSpeedText;
        set {
            _currentWindSpeedText = value;
            RaisePropertyChangedEvent("CurrentWindSpeedText");
        }
    }

    public string CurrentWeatherDescription {
        get => _currentWeatherDescription;
        set {
            _currentWeatherDescription = value;
            RaisePropertyChangedEvent("CurrentWeatherDescription");
        }
    }

    public string SevenDayForecastName1 {
        get => _sevenDayForecastName1;
        set {
            _sevenDayForecastName1 = value;
            RaisePropertyChangedEvent("SevenDayForecastName1");
        }
    }

    public string SevenDayForecastTemp1 {
        get => _sevenDayForecastTemp1;
        set {
            _sevenDayForecastTemp1 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp1");
        }
    }

    public string SevenDayForecastWeatherIcon1a {
        get => _sevenDayForecastWeatherIcon1a;
        set {
            _sevenDayForecastWeatherIcon1a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon1a");
        }
    }

    public string SevenDayForecastWeatherIcon1b {
        get => _sevenDayForecastWeatherIcon1b;
        set {
            _sevenDayForecastWeatherIcon1b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon1b");
        }
    }

    public string SevenDayForecastWindSpeed1 {
        get => _sevenDayForecastWindSpeed1;
        set {
            _sevenDayForecastWindSpeed1 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed1");
        }
    }

    public string SevenDayForecastDescription1 {
        get => _sevenDayForecastDescription1;
        set {
            _sevenDayForecastDescription1 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription1");
        }
    }

    public string SevenDayForecastName2 {
        get => _sevenDayForecastName2;
        set {
            _sevenDayForecastName2 = value;
            RaisePropertyChangedEvent("SevenDayForecastName2");
        }
    }

    public string SevenDayForecastTemp2 {
        get => _sevenDayForecastTemp2;
        set {
            _sevenDayForecastTemp2 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp2");
        }
    }

    public string SevenDayForecastWeatherIcon2a {
        get => _sevenDayForecastWeatherIcon2a;
        set {
            _sevenDayForecastWeatherIcon2a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon2a");
        }
    }

    public string SevenDayForecastWeatherIcon2b {
        get => _sevenDayForecastWeatherIcon2b;
        set {
            _sevenDayForecastWeatherIcon2b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon2b");
        }
    }

    public string SevenDayForecastWindSpeed2 {
        get => _sevenDayForecastWindSpeed2;
        set {
            _sevenDayForecastWindSpeed2 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed2");
        }
    }

    public string SevenDayForecastDescription2 {
        get => _sevenDayForecastDescription2;
        set {
            _sevenDayForecastDescription2 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription2");
        }
    }

    public string SevenDayForecastName3 {
        get => _sevenDayForecastName3;
        set {
            _sevenDayForecastName3 = value;
            RaisePropertyChangedEvent("SevenDayForecastName3");
        }
    }

    public string SevenDayForecastTemp3 {
        get => _sevenDayForecastTemp3;
        set {
            _sevenDayForecastTemp3 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp3");
        }
    }

    public string SevenDayForecastWeatherIcon3a {
        get => _sevenDayForecastWeatherIcon3a;
        set {
            _sevenDayForecastWeatherIcon3a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon3a");
        }
    }

    public string SevenDayForecastWeatherIcon3b {
        get => _sevenDayForecastWeatherIcon3b;
        set {
            _sevenDayForecastWeatherIcon3b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon3b");
        }
    }

    public string SevenDayForecastWindSpeed3 {
        get => _sevenDayForecastWindSpeed3;
        set {
            _sevenDayForecastWindSpeed3 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed3");
        }
    }

    public string SevenDayForecastDescription3 {
        get => _sevenDayForecastDescription3;
        set {
            _sevenDayForecastDescription3 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription3");
        }
    }

    public string SevenDayForecastName4 {
        get => _sevenDayForecastName4;
        set {
            _sevenDayForecastName4 = value;
            RaisePropertyChangedEvent("SevenDayForecastName4");
        }
    }

    public string SevenDayForecastTemp4 {
        get => _sevenDayForecastTemp4;
        set {
            _sevenDayForecastTemp4 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp4");
        }
    }

    public string SevenDayForecastWeatherIcon4a {
        get => _sevenDayForecastWeatherIcon4a;
        set {
            _sevenDayForecastWeatherIcon4a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon4a");
        }
    }

    public string SevenDayForecastWeatherIcon4b {
        get => _sevenDayForecastWeatherIcon4b;
        set {
            _sevenDayForecastWeatherIcon4b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon4b");
        }
    }

    public string SevenDayForecastWindSpeed4 {
        get => _sevenDayForecastWindSpeed4;
        set {
            _sevenDayForecastWindSpeed4 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed4");
        }
    }

    public string SevenDayForecastDescription4 {
        get => _sevenDayForecastDescription4;
        set {
            _sevenDayForecastDescription4 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription4");
        }
    }

    public string SevenDayForecastName5 {
        get => _sevenDayForecastName5;
        set {
            _sevenDayForecastName5 = value;
            RaisePropertyChangedEvent("SevenDayForecastName5");
        }
    }

    public string SevenDayForecastTemp5 {
        get => _sevenDayForecastTemp5;
        set {
            _sevenDayForecastTemp5 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp5");
        }
    }

    public string SevenDayForecastWeatherIcon5a {
        get => _sevenDayForecastWeatherIcon5a;
        set {
            _sevenDayForecastWeatherIcon5a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon5a");
        }
    }

    public string SevenDayForecastWeatherIcon5b {
        get => _sevenDayForecastWeatherIcon5b;
        set {
            _sevenDayForecastWeatherIcon5b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon5b");
        }
    }

    public string SevenDayForecastWindSpeed5 {
        get => _sevenDayForecastWindSpeed5;
        set {
            _sevenDayForecastWindSpeed5 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed5");
        }
    }

    public string SevenDayForecastDescription5 {
        get => _sevenDayForecastDescription5;
        set {
            _sevenDayForecastDescription5 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription5");
        }
    }

    public string SevenDayForecastName6 {
        get => _sevenDayForecastName6;
        set {
            _sevenDayForecastName6 = value;
            RaisePropertyChangedEvent("SevenDayForecastName6");
        }
    }

    public string SevenDayForecastTemp6 {
        get => _sevenDayForecastTemp6;
        set {
            _sevenDayForecastTemp6 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp6");
        }
    }

    public string SevenDayForecastWeatherIcon6a {
        get => _sevenDayForecastWeatherIcon6a;
        set {
            _sevenDayForecastWeatherIcon6a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon6a");
        }
    }

    public string SevenDayForecastWeatherIcon6b {
        get => _sevenDayForecastWeatherIcon6b;
        set {
            _sevenDayForecastWeatherIcon6b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon6b");
        }
    }

    public string SevenDayForecastWindSpeed6 {
        get => _sevenDayForecastWindSpeed6;
        set {
            _sevenDayForecastWindSpeed6 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed6");
        }
    }

    public string SevenDayForecastDescription6 {
        get => _sevenDayForecastDescription6;
        set {
            _sevenDayForecastDescription6 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription6");
        }
    }

    public string SevenDayForecastName7 {
        get => _sevenDayForecastName7;
        set {
            _sevenDayForecastName7 = value;
            RaisePropertyChangedEvent("SevenDayForecastName7");
        }
    }

    public string SevenDayForecastTemp7 {
        get => _sevenDayForecastTemp7;
        set {
            _sevenDayForecastTemp7 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp7");
        }
    }

    public string SevenDayForecastWeatherIcon7a {
        get => _sevenDayForecastWeatherIcon7a;
        set {
            _sevenDayForecastWeatherIcon7a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon7a");
        }
    }

    public string SevenDayForecastWeatherIcon7b {
        get => _sevenDayForecastWeatherIcon7b;
        set {
            _sevenDayForecastWeatherIcon7b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon7b");
        }
    }

    public string SevenDayForecastWindSpeed7 {
        get => _sevenDayForecastWindSpeed7;
        set {
            _sevenDayForecastWindSpeed7 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed7");
        }
    }

    public string SevenDayForecastDescription7 {
        get => _sevenDayForecastDescription7;
        set {
            _sevenDayForecastDescription7 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription7");
        }
    }

    public string SevenDayForecastName8 {
        get => _sevenDayForecastName8;
        set {
            _sevenDayForecastName8 = value;
            RaisePropertyChangedEvent("SevenDayForecastName8");
        }
    }

    public string SevenDayForecastTemp8 {
        get => _sevenDayForecastTemp8;
        set {
            _sevenDayForecastTemp8 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp8");
        }
    }

    public string SevenDayForecastWeatherIcon8a {
        get => _sevenDayForecastWeatherIcon8a;
        set {
            _sevenDayForecastWeatherIcon8a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon8a");
        }
    }

    public string SevenDayForecastWeatherIcon8b {
        get => _sevenDayForecastWeatherIcon8b;
        set {
            _sevenDayForecastWeatherIcon8b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon8b");
        }
    }

    public string SevenDayForecastWindSpeed8 {
        get => _sevenDayForecastWindSpeed8;
        set {
            _sevenDayForecastWindSpeed8 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed8");
        }
    }

    public string SevenDayForecastDescription8 {
        get => _sevenDayForecastDescription8;
        set {
            _sevenDayForecastDescription8 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription8");
        }
    }

    public string SevenDayForecastName9 {
        get => _sevenDayForecastName9;
        set {
            _sevenDayForecastName9 = value;
            RaisePropertyChangedEvent("SevenDayForecastName9");
        }
    }

    public string SevenDayForecastTemp9 {
        get => _sevenDayForecastTemp9;
        set {
            _sevenDayForecastTemp9 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp9");
        }
    }

    public string SevenDayForecastWeatherIcon9a {
        get => _sevenDayForecastWeatherIcon9a;
        set {
            _sevenDayForecastWeatherIcon9a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon9a");
        }
    }

    public string SevenDayForecastWeatherIcon9b {
        get => _sevenDayForecastWeatherIcon9b;
        set {
            _sevenDayForecastWeatherIcon9b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon9b");
        }
    }

    public string SevenDayForecastWindSpeed9 {
        get => _sevenDayForecastWindSpeed9;
        set {
            _sevenDayForecastWindSpeed9 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed9");
        }
    }

    public string SevenDayForecastDescription9 {
        get => _sevenDayForecastDescription9;
        set {
            _sevenDayForecastDescription9 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription9");
        }
    }

    public string SevenDayForecastName10 {
        get => _sevenDayForecastName10;
        set {
            _sevenDayForecastName10 = value;
            RaisePropertyChangedEvent("SevenDayForecastName10");
        }
    }

    public string SevenDayForecastTemp10 {
        get => _sevenDayForecastTemp10;
        set {
            _sevenDayForecastTemp10 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp10");
        }
    }

    public string SevenDayForecastWeatherIcon10a {
        get => _sevenDayForecastWeatherIcon10a;
        set {
            _sevenDayForecastWeatherIcon10a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon10a");
        }
    }

    public string SevenDayForecastWeatherIcon10b {
        get => _sevenDayForecastWeatherIcon10b;
        set {
            _sevenDayForecastWeatherIcon10b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon10b");
        }
    }

    public string SevenDayForecastWindSpeed10 {
        get => _sevenDayForecastWindSpeed10;
        set {
            _sevenDayForecastWindSpeed10 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed10");
        }
    }

    public string SevenDayForecastDescription10 {
        get => _sevenDayForecastDescription10;
        set {
            _sevenDayForecastDescription10 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription10");
        }
    }

    public string SevenDayForecastName11 {
        get => _sevenDayForecastName11;
        set {
            _sevenDayForecastName11 = value;
            RaisePropertyChangedEvent("SevenDayForecastName11");
        }
    }

    public string SevenDayForecastTemp11 {
        get => _sevenDayForecastTemp11;
        set {
            _sevenDayForecastTemp11 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp11");
        }
    }

    public string SevenDayForecastWeatherIcon11a {
        get => _sevenDayForecastWeatherIcon11a;
        set {
            _sevenDayForecastWeatherIcon11a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon11a");
        }
    }

    public string SevenDayForecastWeatherIcon11b {
        get => _sevenDayForecastWeatherIcon11b;
        set {
            _sevenDayForecastWeatherIcon11b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon11b");
        }
    }

    public string SevenDayForecastWindSpeed11 {
        get => _sevenDayForecastWindSpeed11;
        set {
            _sevenDayForecastWindSpeed11 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed11");
        }
    }

    public string SevenDayForecastDescription11 {
        get => _sevenDayForecastDescription11;
        set {
            _sevenDayForecastDescription11 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription11");
        }
    }

    public string SevenDayForecastName12 {
        get => _sevenDayForecastName12;
        set {
            _sevenDayForecastName12 = value;
            RaisePropertyChangedEvent("SevenDayForecastName12");
        }
    }

    public string SevenDayForecastTemp12 {
        get => _sevenDayForecastTemp12;
        set {
            _sevenDayForecastTemp12 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp12");
        }
    }

    public string SevenDayForecastWeatherIcon12a {
        get => _sevenDayForecastWeatherIcon12a;
        set {
            _sevenDayForecastWeatherIcon12a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon12a");
        }
    }

    public string SevenDayForecastWeatherIcon12b {
        get => _sevenDayForecastWeatherIcon12b;
        set {
            _sevenDayForecastWeatherIcon12b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon12b");
        }
    }

    public string SevenDayForecastWindSpeed12 {
        get => _sevenDayForecastWindSpeed12;
        set {
            _sevenDayForecastWindSpeed12 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed12");
        }
    }

    public string SevenDayForecastDescription12 {
        get => _sevenDayForecastDescription12;
        set {
            _sevenDayForecastDescription12 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription12");
        }
    }

    public string SevenDayForecastName13 {
        get => _sevenDayForecastName13;
        set {
            _sevenDayForecastName13 = value;
            RaisePropertyChangedEvent("SevenDayForecastName13");
        }
    }

    public string SevenDayForecastTemp13 {
        get => _sevenDayForecastTemp13;
        set {
            _sevenDayForecastTemp13 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp13");
        }
    }

    public string SevenDayForecastWeatherIcon13a {
        get => _sevenDayForecastWeatherIcon13a;
        set {
            _sevenDayForecastWeatherIcon13a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon13a");
        }
    }

    public string SevenDayForecastWeatherIcon13b {
        get => _sevenDayForecastWeatherIcon13b;
        set {
            _sevenDayForecastWeatherIcon13b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon13b");
        }
    }

    public string SevenDayForecastWindSpeed13 {
        get => _sevenDayForecastWindSpeed13;
        set {
            _sevenDayForecastWindSpeed13 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed13");
        }
    }

    public string SevenDayForecastDescription13 {
        get => _sevenDayForecastDescription13;
        set {
            _sevenDayForecastDescription13 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription13");
        }
    }

    public string SevenDayForecastName14 {
        get => _sevenDayForecastName14;
        set {
            _sevenDayForecastName14 = value;
            RaisePropertyChangedEvent("SevenDayForecastName14");
        }
    }

    public string SevenDayForecastTemp14 {
        get => _sevenDayForecastTemp14;
        set {
            _sevenDayForecastTemp14 = value;
            RaisePropertyChangedEvent("SevenDayForecastTemp14");
        }
    }

    public string SevenDayForecastWeatherIcon14a {
        get => _sevenDayForecastWeatherIcon14a;
        set {
            _sevenDayForecastWeatherIcon14a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon14a");
        }
    }

    public string SevenDayForecastWeatherIcon14b {
        get => _sevenDayForecastWeatherIcon14b;
        set {
            _sevenDayForecastWeatherIcon14b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon14b");
        }
    }

    public string SevenDayForecastWindSpeed14 {
        get => _sevenDayForecastWindSpeed14;
        set {
            _sevenDayForecastWindSpeed14 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed14");
        }
    }

    public string SevenDayForecastDescription14 {
        get => _sevenDayForecastDescription14;
        set {
            _sevenDayForecastDescription14 = value;
            RaisePropertyChangedEvent("SevenDayForecastDescription14");
        }
    }

    public string WeatherOverlay {
        get => _weatherOverlay;
        set {
            _weatherOverlay = value;
            RaisePropertyChangedEvent("WeatherOverlay");
        }
    }

    public string ThermometerDisplayIcon {
        get => _thermometerDisplayIcon;
        set {
            _thermometerDisplayIcon = value;
            RaisePropertyChangedEvent("ThermometerDisplayIcon");
        }
    }

    public string SevenDayForecastRainChance1 {
        get => _sevenDayForecastRainChance1;
        set {
            _sevenDayForecastRainChance1 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance1");
        }
    }

    public string SevenDayForecastRainChance2 {
        get => _sevenDayForecastRainChance2;
        set {
            _sevenDayForecastRainChance2 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance2");
        }
    }

    public string SevenDayForecastRainChance3 {
        get => _sevenDayForecastRainChance3;
        set {
            _sevenDayForecastRainChance3 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance3");
        }
    }

    public string SevenDayForecastRainChance4 {
        get => _sevenDayForecastRainChance4;
        set {
            _sevenDayForecastRainChance4 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance4");
        }
    }

    public string SevenDayForecastRainChance5 {
        get => _sevenDayForecastRainChance5;
        set {
            _sevenDayForecastRainChance5 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance5");
        }
    }

    public string SevenDayForecastRainChance6 {
        get => _sevenDayForecastRainChance6;
        set {
            _sevenDayForecastRainChance6 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance6");
        }
    }

    public string SevenDayForecastRainChance7 {
        get => _sevenDayForecastRainChance7;
        set {
            _sevenDayForecastRainChance7 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance7");
        }
    }

    public string SevenDayForecastRainChance8 {
        get => _sevenDayForecastRainChance8;
        set {
            _sevenDayForecastRainChance8 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance8");
        }
    }

    public string SevenDayForecastRainChance9 {
        get => _sevenDayForecastRainChance9;
        set {
            _sevenDayForecastRainChance9 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance9");
        }
    }

    public string SevenDayForecastRainChance10 {
        get => _sevenDayForecastRainChance10;
        set {
            _sevenDayForecastRainChance10 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance10");
        }
    }

    public string SevenDayForecastRainChance11 {
        get => _sevenDayForecastRainChance11;
        set {
            _sevenDayForecastRainChance11 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance11");
        }
    }

    public string SevenDayForecastRainChance12 {
        get => _sevenDayForecastRainChance12;
        set {
            _sevenDayForecastRainChance12 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance12");
        }
    }

    public string SevenDayForecastRainChance13 {
        get => _sevenDayForecastRainChance13;
        set {
            _sevenDayForecastRainChance13 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance13");
        }
    }

    public string SevenDayForecastRainChance14 {
        get => _sevenDayForecastRainChance14;
        set {
            _sevenDayForecastRainChance14 = value;
            RaisePropertyChangedEvent("SevenDayForecastRainChance14");
        }
    }

    public int SevenDayForecastWindDirectionIcon1 {
        get => _sevenDayForecastWindDirectionIcon1;
        set {
            _sevenDayForecastWindDirectionIcon1 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon1");
        }
    }

    public int SevenDayForecastWindDirectionIcon2 {
        get => _sevenDayForecastWindDirectionIcon2;
        set {
            _sevenDayForecastWindDirectionIcon2 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon2");
        }
    }

    public int SevenDayForecastWindDirectionIcon3 {
        get => _sevenDayForecastWindDirectionIcon3;
        set {
            _sevenDayForecastWindDirectionIcon3 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon3");
        }
    }

    public int SevenDayForecastWindDirectionIcon4 {
        get => _sevenDayForecastWindDirectionIcon4;
        set {
            _sevenDayForecastWindDirectionIcon4 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon4");
        }
    }

    public int SevenDayForecastWindDirectionIcon5 {
        get => _sevenDayForecastWindDirectionIcon5;
        set {
            _sevenDayForecastWindDirectionIcon5 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon5");
        }
    }

    public int SevenDayForecastWindDirectionIcon6 {
        get => _sevenDayForecastWindDirectionIcon6;
        set {
            _sevenDayForecastWindDirectionIcon6 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon6");
        }
    }

    public int SevenDayForecastWindDirectionIcon7 {
        get => _sevenDayForecastWindDirectionIcon7;
        set {
            _sevenDayForecastWindDirectionIcon7 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon7");
        }
    }

    public int SevenDayForecastWindDirectionIcon8 {
        get => _sevenDayForecastWindDirectionIcon8;
        set {
            _sevenDayForecastWindDirectionIcon8 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon8");
        }
    }

    public int SevenDayForecastWindDirectionIcon9 {
        get => _sevenDayForecastWindDirectionIcon9;
        set {
            _sevenDayForecastWindDirectionIcon9 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon9");
        }
    }

    public int SevenDayForecastWindDirectionIcon10 {
        get => _sevenDayForecastWindDirectionIcon10;
        set {
            _sevenDayForecastWindDirectionIcon10 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon10");
        }
    }

    public int SevenDayForecastWindDirectionIcon11 {
        get => _sevenDayForecastWindDirectionIcon11;
        set {
            _sevenDayForecastWindDirectionIcon11 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon11");
        }
    }

    public int SevenDayForecastWindDirectionIcon12 {
        get => _sevenDayForecastWindDirectionIcon12;
        set {
            _sevenDayForecastWindDirectionIcon12 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon12");
        }
    }

    public int SevenDayForecastWindDirectionIcon13 {
        get => _sevenDayForecastWindDirectionIcon13;
        set {
            _sevenDayForecastWindDirectionIcon13 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon13");
        }
    }

    public int SevenDayForecastWindDirectionIcon14 {
        get => _sevenDayForecastWindDirectionIcon14;
        set {
            _sevenDayForecastWindDirectionIcon14 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionIcon14");
        }
    }

    #endregion
}