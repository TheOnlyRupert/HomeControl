using System;
using System.Net;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel; 

public class WeatherVM : BaseViewModel {
    public string _currentDateText, _currentTimeText, _currentTimeSecondsText, _currentWeatherLocationText,
        _currentWeatherTempText, _currentWeatherCloudIcon, _sunRiseText,
        _sunSetText, _sunRiseIcon, _sunSetIcon;

    public int _currentWindDirectionRotation;

    public string _currentWindSpeedText, _currentWeatherDescription,
        _sevenDayForecastDescription1, _sevenDayForecastWindSpeed1, _sevenDayForecastWindDirectionRotation1, _sevenDayForecastWeatherIcon1, _sevenDayForecastTemp1,
        _sevenDayForecastName1, _sevenDayForecastDescription2, _sevenDayForecastWindSpeed2, _sevenDayForecastWindDirectionRotation2, _sevenDayForecastWeatherIcon2,
        _sevenDayForecastTemp2, _sevenDayForecastName2, _sevenDayForecastDescription3, _sevenDayForecastWindSpeed3, _sevenDayForecastWindDirectionRotation3,
        _sevenDayForecastWeatherIcon3, _sevenDayForecastTemp3, _sevenDayForecastName3, _sevenDayForecastDescription4, _sevenDayForecastWindSpeed4,
        _sevenDayForecastWindDirectionRotation4, _sevenDayForecastWeatherIcon4, _sevenDayForecastTemp4, _sevenDayForecastName4, _sevenDayForecastDescription5,
        _sevenDayForecastWindSpeed5, _sevenDayForecastWindDirectionRotation5, _sevenDayForecastWeatherIcon5, _sevenDayForecastTemp5, _sevenDayForecastName5,
        _sevenDayForecastDescription6, _sevenDayForecastWindSpeed6, _sevenDayForecastWindDirectionRotation6, _sevenDayForecastWeatherIcon6, _sevenDayForecastTemp6,
        _sevenDayForecastName6,
        _sevenDayForecastDescription7, _sevenDayForecastWindSpeed7, _sevenDayForecastWindDirectionRotation7, _sevenDayForecastWeatherIcon7, _sevenDayForecastTemp7,
        _sevenDayForecastName7,
        _sevenDayForecastDescription8, _sevenDayForecastWindSpeed8, _sevenDayForecastWindDirectionRotation8, _sevenDayForecastWeatherIcon8, _sevenDayForecastTemp8,
        _sevenDayForecastName8,
        _sevenDayForecastDescription9, _sevenDayForecastWindSpeed9, _sevenDayForecastWindDirectionRotation9, _sevenDayForecastWeatherIcon9, _sevenDayForecastTemp9,
        _sevenDayForecastName9,
        _sevenDayForecastDescription10, _sevenDayForecastWindSpeed10, _sevenDayForecastWindDirectionRotation10, _sevenDayForecastWeatherIcon10, _sevenDayForecastTemp10,
        _sevenDayForecastName10,
        _sevenDayForecastDescription11, _sevenDayForecastWindSpeed11, _sevenDayForecastWindDirectionRotation11, _sevenDayForecastWeatherIcon11, _sevenDayForecastTemp11,
        _sevenDayForecastName11,
        _sevenDayForecastDescription12, _sevenDayForecastWindSpeed12, _sevenDayForecastWindDirectionRotation12, _sevenDayForecastWeatherIcon12, _sevenDayForecastTemp12,
        _sevenDayForecastName12,
        _sevenDayForecastDescription13, _sevenDayForecastWindSpeed13, _sevenDayForecastWindDirectionRotation13, _sevenDayForecastWeatherIcon13, _sevenDayForecastTemp13,
        _sevenDayForecastName13,
        _sevenDayForecastDescription14, _sevenDayForecastWindSpeed14, _sevenDayForecastWindDirectionRotation14, _sevenDayForecastWeatherIcon14, _sevenDayForecastTemp14,
        _sevenDayForecastName14, _hourlyForecastTime1, _hourlyForecastWeatherIcon1, _hourlyForecastTemp1, _hourlyForecastRainChance1, _hourlyForecastWindSpeed1,
        _hourlyForecastTime2, _hourlyForecastWeatherIcon2, _hourlyForecastTemp2, _hourlyForecastRainChance2, _hourlyForecastWindSpeed2,
        _hourlyForecastTime3, _hourlyForecastWeatherIcon3, _hourlyForecastTemp3, _hourlyForecastRainChance3, _hourlyForecastWindSpeed3,
        _hourlyForecastTime4, _hourlyForecastWeatherIcon4, _hourlyForecastTemp4, _hourlyForecastRainChance4, _hourlyForecastWindSpeed4,
        _hourlyForecastTime5, _hourlyForecastWeatherIcon5, _hourlyForecastTemp5, _hourlyForecastRainChance5, _hourlyForecastWindSpeed5,
        _hourlyForecastTime6, _hourlyForecastWeatherIcon6, _hourlyForecastTemp6, _hourlyForecastRainChance6, _hourlyForecastWindSpeed6,
        _hourlyForecastTime7, _hourlyForecastWeatherIcon7, _hourlyForecastTemp7, _hourlyForecastRainChance7, _hourlyForecastWindSpeed7,
        _hourlyForecastTime8, _hourlyForecastWeatherIcon8, _hourlyForecastTemp8, _hourlyForecastRainChance8, _hourlyForecastWindSpeed8,
        _hourlyForecastTime9, _hourlyForecastWeatherIcon9, _hourlyForecastTemp9, _hourlyForecastRainChance9, _hourlyForecastWindSpeed9;

    private DateTime currentTime;
    private JsonWeatherForecast forecast;
    private JsonWeatherForecastHourly forecastHourly;
    private int poolWeather;
    private bool updateForecast, updateForecastHourly;

    public WeatherVM() {
        currentTime = DateTime.Now;
        poolWeather = 0;
        updateForecast = true;
        updateForecastHourly = true;

        /* Timer used to update time and weather.
         * It also pushes a crossViewMessage to update calendar, finances, etc.. when the date changes.
         */
        //DispatcherTimer dispatcherTimer = new();
        //dispatcherTimer.Tick += dispatcherTimer_Tick;
        //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        //dispatcherTimer.Start();
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e) {
        CurrentDateText = DateTime.Now.ToLongDateString();
        CurrentTimeText = DateTime.Now.ToShortTimeString();
        CurrentTimeSecondsText = DateTime.Now.Second.ToString();
        poolWeather++;

        if (!currentTime.Day.Equals(DateTime.Now.Day)) {
            //update date stuff
        }

        /* Update weather every 15 minutes or when */
        if ((poolWeather > 900 || updateForecast || updateForecastHourly) && !string.IsNullOrEmpty(ReferenceValues.UserAgent)) {
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
                Console.WriteLine("JsonWeatherForecast Errored");
                errored = true;
            }

            try {
                using WebClient client2 = new();
                Uri weatherForecastHourlyURL = new("https://api.weather.gov/gridpoints/OHX/42,62/forecast/hourly");
                client2.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.UserAgent);
                string weatherForecastHourly = client2.DownloadString(weatherForecastHourlyURL);
                forecastHourly = JsonSerializer.Deserialize<JsonWeatherForecastHourly>(weatherForecastHourly, options);
                updateForecastHourly = false;
            } catch (Exception) {
                Console.WriteLine("JsonWeatherForecastHourly Errored");
                errored = true;
            }

            if (!errored) {
                UpdateWeatherForecast();
            }

            poolWeather = 0;
        }
    }

    public void UpdateWeatherForecast() {
        //TODO: Add more places
        CurrentWeatherLocationText = "Ashland City, TN";
        CurrentWeatherTempText = forecastHourly.properties.periods[0].temperature.ToString() + '°';
        CurrentWindDirectionRotation = GetWindRotation(forecastHourly.properties.periods[0].windDirection);
        CurrentWindSpeedText = forecastHourly.properties.periods[0].windSpeed;
        CurrentWeatherDescription = forecastHourly.properties.periods[0].shortForecast;
        CurrentWeatherCloudIcon = GetWeatherIcon(forecastHourly.properties.periods[0].shortForecast, forecast.properties.periods[0].isDaytime);

        try {
            SevenDayForecastName1 = forecast.properties.periods[0].name;
            SevenDayForecastTemp1 = forecast.properties.periods[0].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon1 = GetWeatherIcon(forecast.properties.periods[0].shortForecast, forecast.properties.periods[0].isDaytime);
            SevenDayForecastWindSpeed1 = forecast.properties.periods[0].windSpeed;
            SevenDayForecastDescription1 = forecast.properties.periods[0].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName2 = forecast.properties.periods[1].name;
            SevenDayForecastTemp2 = forecast.properties.periods[1].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon2 = GetWeatherIcon(forecast.properties.periods[1].shortForecast, forecast.properties.periods[1].isDaytime);
            SevenDayForecastWindSpeed2 = forecast.properties.periods[1].windSpeed;
            SevenDayForecastDescription2 = forecast.properties.periods[1].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName3 = forecast.properties.periods[2].name;
            SevenDayForecastTemp3 = forecast.properties.periods[2].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon3 = GetWeatherIcon(forecast.properties.periods[2].shortForecast, forecast.properties.periods[2].isDaytime);
            SevenDayForecastWindSpeed3 = forecast.properties.periods[2].windSpeed;
            SevenDayForecastDescription3 = forecast.properties.periods[2].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName4 = forecast.properties.periods[3].name;
            SevenDayForecastTemp4 = forecast.properties.periods[3].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon4 = GetWeatherIcon(forecast.properties.periods[3].shortForecast, forecast.properties.periods[3].isDaytime);
            SevenDayForecastWindSpeed4 = forecast.properties.periods[3].windSpeed;
            SevenDayForecastDescription4 = forecast.properties.periods[3].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName5 = forecast.properties.periods[4].name;
            SevenDayForecastTemp5 = forecast.properties.periods[4].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon5 = GetWeatherIcon(forecast.properties.periods[4].shortForecast, forecast.properties.periods[4].isDaytime);
            SevenDayForecastWindSpeed5 = forecast.properties.periods[4].windSpeed;
            SevenDayForecastDescription5 = forecast.properties.periods[4].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName6 = forecast.properties.periods[5].name;
            SevenDayForecastTemp6 = forecast.properties.periods[5].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon6 = GetWeatherIcon(forecast.properties.periods[5].shortForecast, forecast.properties.periods[5].isDaytime);
            SevenDayForecastWindSpeed6 = forecast.properties.periods[5].windSpeed;
            SevenDayForecastDescription6 = forecast.properties.periods[5].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName7 = forecast.properties.periods[6].name;
            SevenDayForecastTemp7 = forecast.properties.periods[6].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon7 = GetWeatherIcon(forecast.properties.periods[6].shortForecast, forecast.properties.periods[6].isDaytime);
            SevenDayForecastWindSpeed7 = forecast.properties.periods[6].windSpeed;
            SevenDayForecastDescription7 = forecast.properties.periods[6].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName8 = forecast.properties.periods[7].name;
            SevenDayForecastTemp8 = forecast.properties.periods[7].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon8 = GetWeatherIcon(forecast.properties.periods[7].shortForecast, forecast.properties.periods[7].isDaytime);
            SevenDayForecastWindSpeed8 = forecast.properties.periods[7].windSpeed;
            SevenDayForecastDescription8 = forecast.properties.periods[7].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName9 = forecast.properties.periods[8].name;
            SevenDayForecastTemp9 = forecast.properties.periods[8].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon9 = GetWeatherIcon(forecast.properties.periods[8].shortForecast, forecast.properties.periods[8].isDaytime);
            SevenDayForecastWindSpeed9 = forecast.properties.periods[8].windSpeed;
            SevenDayForecastDescription9 = forecast.properties.periods[8].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName10 = forecast.properties.periods[9].name;
            SevenDayForecastTemp10 = forecast.properties.periods[9].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon10 = GetWeatherIcon(forecast.properties.periods[9].shortForecast, forecast.properties.periods[9].isDaytime);
            SevenDayForecastWindSpeed10 = forecast.properties.periods[9].windSpeed;
            SevenDayForecastDescription10 = forecast.properties.periods[9].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName11 = forecast.properties.periods[10].name;
            SevenDayForecastTemp11 = forecast.properties.periods[10].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon11 = GetWeatherIcon(forecast.properties.periods[10].shortForecast, forecast.properties.periods[10].isDaytime);
            SevenDayForecastWindSpeed11 = forecast.properties.periods[10].windSpeed;
            SevenDayForecastDescription11 = forecast.properties.periods[10].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName12 = forecast.properties.periods[11].name;
            SevenDayForecastTemp12 = forecast.properties.periods[11].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon12 = GetWeatherIcon(forecast.properties.periods[11].shortForecast, forecast.properties.periods[11].isDaytime);
            SevenDayForecastWindSpeed12 = forecast.properties.periods[11].windSpeed;
            SevenDayForecastDescription12 = forecast.properties.periods[11].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName13 = forecast.properties.periods[12].name;
            SevenDayForecastTemp13 = forecast.properties.periods[12].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon13 = GetWeatherIcon(forecast.properties.periods[12].shortForecast, forecast.properties.periods[12].isDaytime);
            SevenDayForecastWindSpeed13 = forecast.properties.periods[12].windSpeed;
            SevenDayForecastDescription13 = forecast.properties.periods[12].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName14 = forecast.properties.periods[13].name;
            SevenDayForecastTemp14 = forecast.properties.periods[13].temperature.ToString() + '°';
            SevenDayForecastWeatherIcon14 = GetWeatherIcon(forecast.properties.periods[13].shortForecast, forecast.properties.periods[13].isDaytime);
            SevenDayForecastWindSpeed14 = forecast.properties.periods[13].windSpeed;
            SevenDayForecastDescription14 = forecast.properties.periods[13].shortForecast;
        } catch (Exception) { }

        try {
            HourlyForecastTime1 = forecastHourly.properties.periods[1].startTime.Hour + "00";
            HourlyForecastWeatherIcon1 = GetWeatherIcon(forecastHourly.properties.periods[1].shortForecast, forecastHourly.properties.periods[1].isDaytime);
            HourlyForecastTemp1 = forecastHourly.properties.periods[1].temperature.ToString() + '°';
            HourlyForecastRainChance1 = GetRainChance(forecastHourly.properties.periods[1].icon);
            HourlyForecastWindSpeed1 = forecastHourly.properties.periods[1].windSpeed;
        } catch (Exception) { }

        try {
            HourlyForecastTime2 = forecastHourly.properties.periods[2].startTime.Hour + "00";
            HourlyForecastWeatherIcon2 = GetWeatherIcon(forecastHourly.properties.periods[2].shortForecast, forecastHourly.properties.periods[2].isDaytime);
            HourlyForecastTemp2 = forecastHourly.properties.periods[2].temperature.ToString() + '°';
            HourlyForecastRainChance2 = GetRainChance(forecastHourly.properties.periods[2].icon);
            HourlyForecastWindSpeed2 = forecastHourly.properties.periods[2].windSpeed;
        } catch (Exception) { }

        try {
            HourlyForecastTime3 = forecastHourly.properties.periods[3].startTime.Hour + "00";
            HourlyForecastWeatherIcon3 = GetWeatherIcon(forecastHourly.properties.periods[3].shortForecast, forecastHourly.properties.periods[3].isDaytime);
            HourlyForecastTemp3 = forecastHourly.properties.periods[3].temperature.ToString() + '°';
            HourlyForecastRainChance3 = GetRainChance(forecastHourly.properties.periods[3].icon);
            HourlyForecastWindSpeed3 = forecastHourly.properties.periods[3].windSpeed;
        } catch (Exception) { }

        try {
            HourlyForecastTime4 = forecastHourly.properties.periods[4].startTime.Hour + "00";
            HourlyForecastWeatherIcon4 = GetWeatherIcon(forecastHourly.properties.periods[4].shortForecast, forecastHourly.properties.periods[4].isDaytime);
            HourlyForecastTemp4 = forecastHourly.properties.periods[4].temperature.ToString() + '°';
            HourlyForecastRainChance4 = GetRainChance(forecastHourly.properties.periods[4].icon);
            HourlyForecastWindSpeed4 = forecastHourly.properties.periods[4].windSpeed;
        } catch (Exception) { }

        try {
            HourlyForecastTime5 = forecastHourly.properties.periods[5].startTime.Hour + "00";
            HourlyForecastWeatherIcon5 = GetWeatherIcon(forecastHourly.properties.periods[5].shortForecast, forecastHourly.properties.periods[5].isDaytime);
            HourlyForecastTemp5 = forecastHourly.properties.periods[5].temperature.ToString() + '°';
            HourlyForecastRainChance5 = GetRainChance(forecastHourly.properties.periods[5].icon);
            HourlyForecastWindSpeed5 = forecastHourly.properties.periods[5].windSpeed;
        } catch (Exception) { }

        try {
            HourlyForecastTime6 = forecastHourly.properties.periods[6].startTime.Hour + "00";
            HourlyForecastWeatherIcon6 = GetWeatherIcon(forecastHourly.properties.periods[6].shortForecast, forecastHourly.properties.periods[6].isDaytime);
            HourlyForecastTemp6 = forecastHourly.properties.periods[6].temperature.ToString() + '°';
            HourlyForecastRainChance6 = GetRainChance(forecastHourly.properties.periods[6].icon);
            HourlyForecastWindSpeed6 = forecastHourly.properties.periods[6].windSpeed;
        } catch (Exception) { }

        try {
            HourlyForecastTime7 = forecastHourly.properties.periods[7].startTime.Hour + "00";
            HourlyForecastWeatherIcon7 = GetWeatherIcon(forecastHourly.properties.periods[7].shortForecast, forecastHourly.properties.periods[7].isDaytime);
            HourlyForecastTemp7 = forecastHourly.properties.periods[7].temperature.ToString() + '°';
            HourlyForecastRainChance7 = GetRainChance(forecastHourly.properties.periods[7].icon);
            HourlyForecastWindSpeed7 = forecastHourly.properties.periods[7].windSpeed;
        } catch (Exception) { }

        try {
            HourlyForecastTime8 = forecastHourly.properties.periods[8].startTime.Hour + "00";
            HourlyForecastWeatherIcon8 = GetWeatherIcon(forecastHourly.properties.periods[8].shortForecast, forecastHourly.properties.periods[8].isDaytime);
            HourlyForecastTemp8 = forecastHourly.properties.periods[8].temperature.ToString() + '°';
            HourlyForecastRainChance8 = GetRainChance(forecastHourly.properties.periods[8].icon);
            HourlyForecastWindSpeed8 = forecastHourly.properties.periods[8].windSpeed;
        } catch (Exception) { }

        try {
            HourlyForecastTime9 = forecastHourly.properties.periods[9].startTime.Hour + "00";
            HourlyForecastWeatherIcon9 = GetWeatherIcon(forecastHourly.properties.periods[9].shortForecast, forecastHourly.properties.periods[9].isDaytime);
            HourlyForecastTemp9 = forecastHourly.properties.periods[9].temperature.ToString() + '°';
            HourlyForecastRainChance9 = GetRainChance(forecastHourly.properties.periods[9].icon);
            HourlyForecastWindSpeed9 = forecastHourly.properties.periods[9].windSpeed;
        } catch (Exception) { }
    }

    private string GetWeatherIcon(string weather, bool isDayTime) {
        switch (weather) {
        case "Sunny":
        case "Mostly Sunny":
        case "Clear":
        case "Mostly Clear":
            return isDayTime
                ? "../../Resources/Images/weather/weather_clear.png"
                : "../../Resources/Images/weather/weather_clear_night.png";
        case "Partly Cloudy":
        case "Partly Cloudy then Slight Chance Rain Showers":
            return isDayTime
                ? "../../Resources/Images/weather/weather_part_cloudy.png"
                : "../../Resources/Images/weather/weather_cloudy_night.png";
        case "Patchy Fog":
        case "Areas Of Fog":
            return "../../Resources/Images/weather/weather_fog.png";
        case "Chance Rain Showers":
        case "Rain Showers Likely":
        case "Chance Rain Showers then Mostly Cloudy":
            return "../../Resources/Images/weather/weather_rain_light.png";
        case "Rain Showers":
            return "../../Resources/Images/weather/weather_rain_medium.png";
        case "Chance Showers And Thunderstorms":
        case "Slight Chance Rain Showers":
            return "../../Resources/Images/weather/weather_storm.png";
        default:
            return isDayTime
                ? "../../Resources/Images/weather/weather_part_cloudy.png"
                : "../../Resources/Images/weather/weather_cloudy_night.png";
        }
    }

    private string GetRainChance(string icon) {
        return "0%";
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

    public string SunRiseText {
        get => _sunRiseText;
        set {
            _sunRiseText = value;
            RaisePropertyChangedEvent("SunRiseText");
        }
    }

    public string SunSetText {
        get => _sunSetText;
        set {
            _sunSetText = value;
            RaisePropertyChangedEvent("SunSetText");
        }
    }

    public string SunRiseIcon {
        get => _sunRiseIcon;
        set {
            _sunRiseIcon = value;
            RaisePropertyChangedEvent("SunRiseIcon");
        }
    }

    public string SunSetIcon {
        get => _sunSetIcon;
        set {
            _sunSetIcon = value;
            RaisePropertyChangedEvent("SunSetIcon");
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

    public string SevenDayForecastWeatherIcon1 {
        get => _sevenDayForecastWeatherIcon1;
        set {
            _sevenDayForecastWeatherIcon1 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon1");
        }
    }

    public string SevenDayForecastWindDirectionRotation1 {
        get => _sevenDayForecastWindDirectionRotation1;
        set {
            _sevenDayForecastWindDirectionRotation1 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation1");
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

    public string SevenDayForecastWeatherIcon2 {
        get => _sevenDayForecastWeatherIcon2;
        set {
            _sevenDayForecastWeatherIcon2 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon2");
        }
    }

    public string SevenDayForecastWindDirectionRotation2 {
        get => _sevenDayForecastWindDirectionRotation2;
        set {
            _sevenDayForecastWindDirectionRotation2 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation2");
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

    public string SevenDayForecastWeatherIcon3 {
        get => _sevenDayForecastWeatherIcon3;
        set {
            _sevenDayForecastWeatherIcon3 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon3");
        }
    }

    public string SevenDayForecastWindDirectionRotation3 {
        get => _sevenDayForecastWindDirectionRotation3;
        set {
            _sevenDayForecastWindDirectionRotation3 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation3");
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

    public string SevenDayForecastWeatherIcon4 {
        get => _sevenDayForecastWeatherIcon4;
        set {
            _sevenDayForecastWeatherIcon4 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon4");
        }
    }

    public string SevenDayForecastWindDirectionRotation4 {
        get => _sevenDayForecastWindDirectionRotation4;
        set {
            _sevenDayForecastWindDirectionRotation4 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation4");
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

    public string SevenDayForecastWeatherIcon5 {
        get => _sevenDayForecastWeatherIcon5;
        set {
            _sevenDayForecastWeatherIcon5 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon5");
        }
    }

    public string SevenDayForecastWindDirectionRotation5 {
        get => _sevenDayForecastWindDirectionRotation5;
        set {
            _sevenDayForecastWindDirectionRotation5 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation5");
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

    public string SevenDayForecastWeatherIcon6 {
        get => _sevenDayForecastWeatherIcon6;
        set {
            _sevenDayForecastWeatherIcon6 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon6");
        }
    }

    public string SevenDayForecastWindDirectionRotation6 {
        get => _sevenDayForecastWindDirectionRotation6;
        set {
            _sevenDayForecastWindDirectionRotation6 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation6");
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

    public string SevenDayForecastWeatherIcon7 {
        get => _sevenDayForecastWeatherIcon7;
        set {
            _sevenDayForecastWeatherIcon7 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon7");
        }
    }

    public string SevenDayForecastWindDirectionRotation7 {
        get => _sevenDayForecastWindDirectionRotation7;
        set {
            _sevenDayForecastWindDirectionRotation7 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation7");
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

    public string SevenDayForecastWeatherIcon8 {
        get => _sevenDayForecastWeatherIcon8;
        set {
            _sevenDayForecastWeatherIcon8 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon8");
        }
    }

    public string SevenDayForecastWindDirectionRotation8 {
        get => _sevenDayForecastWindDirectionRotation8;
        set {
            _sevenDayForecastWindDirectionRotation8 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation8");
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

    public string SevenDayForecastWeatherIcon9 {
        get => _sevenDayForecastWeatherIcon9;
        set {
            _sevenDayForecastWeatherIcon9 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon9");
        }
    }

    public string SevenDayForecastWindDirectionRotation9 {
        get => _sevenDayForecastWindDirectionRotation9;
        set {
            _sevenDayForecastWindDirectionRotation9 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation9");
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

    public string SevenDayForecastWeatherIcon10 {
        get => _sevenDayForecastWeatherIcon10;
        set {
            _sevenDayForecastWeatherIcon10 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon10");
        }
    }

    public string SevenDayForecastWindDirectionRotation10 {
        get => _sevenDayForecastWindDirectionRotation10;
        set {
            _sevenDayForecastWindDirectionRotation10 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation10");
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

    public string SevenDayForecastWeatherIcon11 {
        get => _sevenDayForecastWeatherIcon11;
        set {
            _sevenDayForecastWeatherIcon11 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon11");
        }
    }

    public string SevenDayForecastWindDirectionRotation11 {
        get => _sevenDayForecastWindDirectionRotation11;
        set {
            _sevenDayForecastWindDirectionRotation11 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation11");
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

    public string SevenDayForecastWeatherIcon12 {
        get => _sevenDayForecastWeatherIcon12;
        set {
            _sevenDayForecastWeatherIcon12 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon12");
        }
    }

    public string SevenDayForecastWindDirectionRotation12 {
        get => _sevenDayForecastWindDirectionRotation12;
        set {
            _sevenDayForecastWindDirectionRotation12 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation12");
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

    public string SevenDayForecastWeatherIcon13 {
        get => _sevenDayForecastWeatherIcon13;
        set {
            _sevenDayForecastWeatherIcon13 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon13");
        }
    }

    public string SevenDayForecastWindDirectionRotation13 {
        get => _sevenDayForecastWindDirectionRotation13;
        set {
            _sevenDayForecastWindDirectionRotation13 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation13");
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

    public string SevenDayForecastWeatherIcon14 {
        get => _sevenDayForecastWeatherIcon14;
        set {
            _sevenDayForecastWeatherIcon14 = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon14");
        }
    }

    public string SevenDayForecastWindDirectionRotation14 {
        get => _sevenDayForecastWindDirectionRotation14;
        set {
            _sevenDayForecastWindDirectionRotation14 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindDirectionRotation14");
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

    public string HourlyForecastTime1 {
        get => _hourlyForecastTime1;
        set {
            _hourlyForecastTime1 = value;
            RaisePropertyChangedEvent("HourlyForecastTime1");
        }
    }

    public string HourlyForecastWeatherIcon1 {
        get => _hourlyForecastWeatherIcon1;
        set {
            _hourlyForecastWeatherIcon1 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon1");
        }
    }

    public string HourlyForecastTemp1 {
        get => _hourlyForecastTemp1;
        set {
            _hourlyForecastTemp1 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp1");
        }
    }

    public string HourlyForecastRainChance1 {
        get => _hourlyForecastRainChance1;
        set {
            _hourlyForecastRainChance1 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance1");
        }
    }

    public string HourlyForecastWindSpeed1 {
        get => _hourlyForecastWindSpeed1;
        set {
            _hourlyForecastWindSpeed1 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed1");
        }
    }

    public string HourlyForecastTime2 {
        get => _hourlyForecastTime2;
        set {
            _hourlyForecastTime2 = value;
            RaisePropertyChangedEvent("HourlyForecastTime2");
        }
    }

    public string HourlyForecastWeatherIcon2 {
        get => _hourlyForecastWeatherIcon2;
        set {
            _hourlyForecastWeatherIcon2 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon2");
        }
    }

    public string HourlyForecastTemp2 {
        get => _hourlyForecastTemp2;
        set {
            _hourlyForecastTemp2 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp2");
        }
    }

    public string HourlyForecastRainChance2 {
        get => _hourlyForecastRainChance2;
        set {
            _hourlyForecastRainChance2 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance2");
        }
    }

    public string HourlyForecastWindSpeed2 {
        get => _hourlyForecastWindSpeed2;
        set {
            _hourlyForecastWindSpeed2 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed2");
        }
    }

    public string HourlyForecastTime3 {
        get => _hourlyForecastTime3;
        set {
            _hourlyForecastTime3 = value;
            RaisePropertyChangedEvent("HourlyForecastTime3");
        }
    }

    public string HourlyForecastWeatherIcon3 {
        get => _hourlyForecastWeatherIcon3;
        set {
            _hourlyForecastWeatherIcon3 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon3");
        }
    }

    public string HourlyForecastTemp3 {
        get => _hourlyForecastTemp3;
        set {
            _hourlyForecastTemp3 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp3");
        }
    }

    public string HourlyForecastRainChance3 {
        get => _hourlyForecastRainChance3;
        set {
            _hourlyForecastRainChance3 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance3");
        }
    }

    public string HourlyForecastWindSpeed3 {
        get => _hourlyForecastWindSpeed3;
        set {
            _hourlyForecastWindSpeed3 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed3");
        }
    }

    public string HourlyForecastTime4 {
        get => _hourlyForecastTime4;
        set {
            _hourlyForecastTime4 = value;
            RaisePropertyChangedEvent("HourlyForecastTime4");
        }
    }

    public string HourlyForecastWeatherIcon4 {
        get => _hourlyForecastWeatherIcon4;
        set {
            _hourlyForecastWeatherIcon4 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon4");
        }
    }

    public string HourlyForecastTemp4 {
        get => _hourlyForecastTemp4;
        set {
            _hourlyForecastTemp4 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp4");
        }
    }

    public string HourlyForecastRainChance4 {
        get => _hourlyForecastRainChance4;
        set {
            _hourlyForecastRainChance4 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance4");
        }
    }

    public string HourlyForecastWindSpeed4 {
        get => _hourlyForecastWindSpeed4;
        set {
            _hourlyForecastWindSpeed4 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed4");
        }
    }

    public string HourlyForecastTime5 {
        get => _hourlyForecastTime5;
        set {
            _hourlyForecastTime5 = value;
            RaisePropertyChangedEvent("HourlyForecastTime5");
        }
    }

    public string HourlyForecastWeatherIcon5 {
        get => _hourlyForecastWeatherIcon5;
        set {
            _hourlyForecastWeatherIcon5 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon5");
        }
    }

    public string HourlyForecastTemp5 {
        get => _hourlyForecastTemp5;
        set {
            _hourlyForecastTemp5 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp5");
        }
    }

    public string HourlyForecastRainChance5 {
        get => _hourlyForecastRainChance5;
        set {
            _hourlyForecastRainChance5 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance5");
        }
    }

    public string HourlyForecastWindSpeed5 {
        get => _hourlyForecastWindSpeed5;
        set {
            _hourlyForecastWindSpeed5 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed5");
        }
    }

    public string HourlyForecastTime6 {
        get => _hourlyForecastTime6;
        set {
            _hourlyForecastTime6 = value;
            RaisePropertyChangedEvent("HourlyForecastTime6");
        }
    }

    public string HourlyForecastWeatherIcon6 {
        get => _hourlyForecastWeatherIcon6;
        set {
            _hourlyForecastWeatherIcon6 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon6");
        }
    }

    public string HourlyForecastTemp6 {
        get => _hourlyForecastTemp6;
        set {
            _hourlyForecastTemp6 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp6");
        }
    }

    public string HourlyForecastRainChance6 {
        get => _hourlyForecastRainChance6;
        set {
            _hourlyForecastRainChance6 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance6");
        }
    }

    public string HourlyForecastWindSpeed6 {
        get => _hourlyForecastWindSpeed6;
        set {
            _hourlyForecastWindSpeed6 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed6");
        }
    }

    public string HourlyForecastTime7 {
        get => _hourlyForecastTime7;
        set {
            _hourlyForecastTime7 = value;
            RaisePropertyChangedEvent("HourlyForecastTime7");
        }
    }

    public string HourlyForecastWeatherIcon7 {
        get => _hourlyForecastWeatherIcon7;
        set {
            _hourlyForecastWeatherIcon7 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon7");
        }
    }

    public string HourlyForecastTemp7 {
        get => _hourlyForecastTemp7;
        set {
            _hourlyForecastTemp7 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp7");
        }
    }

    public string HourlyForecastRainChance7 {
        get => _hourlyForecastRainChance7;
        set {
            _hourlyForecastRainChance7 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance7");
        }
    }

    public string HourlyForecastWindSpeed7 {
        get => _hourlyForecastWindSpeed7;
        set {
            _hourlyForecastWindSpeed7 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed7");
        }
    }

    public string HourlyForecastTime8 {
        get => _hourlyForecastTime8;
        set {
            _hourlyForecastTime8 = value;
            RaisePropertyChangedEvent("HourlyForecastTime8");
        }
    }

    public string HourlyForecastWeatherIcon8 {
        get => _hourlyForecastWeatherIcon8;
        set {
            _hourlyForecastWeatherIcon8 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon8");
        }
    }

    public string HourlyForecastTemp8 {
        get => _hourlyForecastTemp8;
        set {
            _hourlyForecastTemp8 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp8");
        }
    }

    public string HourlyForecastRainChance8 {
        get => _hourlyForecastRainChance8;
        set {
            _hourlyForecastRainChance8 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance8");
        }
    }

    public string HourlyForecastWindSpeed8 {
        get => _hourlyForecastWindSpeed8;
        set {
            _hourlyForecastWindSpeed8 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed8");
        }
    }


    public string HourlyForecastTime9 {
        get => _hourlyForecastTime9;
        set {
            _hourlyForecastTime9 = value;
            RaisePropertyChangedEvent("HourlyForecastTime9");
        }
    }

    public string HourlyForecastWeatherIcon9 {
        get => _hourlyForecastWeatherIcon9;
        set {
            _hourlyForecastWeatherIcon9 = value;
            RaisePropertyChangedEvent("HourlyForecastWeatherIcon9");
        }
    }

    public string HourlyForecastTemp9 {
        get => _hourlyForecastTemp9;
        set {
            _hourlyForecastTemp9 = value;
            RaisePropertyChangedEvent("HourlyForecastTemp9");
        }
    }

    public string HourlyForecastRainChance9 {
        get => _hourlyForecastRainChance9;
        set {
            _hourlyForecastRainChance9 = value;
            RaisePropertyChangedEvent("HourlyForecastRainChance9");
        }
    }

    public string HourlyForecastWindSpeed9 {
        get => _hourlyForecastWindSpeed9;
        set {
            _hourlyForecastWindSpeed9 = value;
            RaisePropertyChangedEvent("HourlyForecastWindSpeed9");
        }
    }

    #endregion
}