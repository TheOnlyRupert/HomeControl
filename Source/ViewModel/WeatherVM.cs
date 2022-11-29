using System;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using HomeControl.Source.Helpers;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class WeatherVM : BaseViewModel {
    private int _currentWindDirectionRotation, _hourlyForecastWindDirectionRotation1, _hourlyForecastWindDirectionRotation2, _hourlyForecastWindDirectionRotation3,
        _hourlyForecastWindDirectionRotation4,
        _hourlyForecastWindDirectionRotation5, _hourlyForecastWindDirectionRotation6, _hourlyForecastWindDirectionRotation7, _hourlyForecastWindDirectionRotation8,
        _hourlyForecastWindDirectionRotation9;

    private string _currentWindSpeedText, _currentWeatherDescription, _currentDateText, _currentTimeText, _currentTimeSecondsText, _currentWeatherLocationText,
        _currentWeatherTempText, _currentWeatherCloudIcon, _sunRiseText, _sunSetText, _sunRiseIcon, _sunSetIcon, _hourlyForecastRainIcon1,
        _sevenDayForecastDescription1, _sevenDayForecastWindSpeed1, _sevenDayForecastWindDirectionRotation1, _sevenDayForecastWeatherIcon1a, _sevenDayForecastWeatherIcon1b,
        _sevenDayForecastTemp1,
        _sevenDayForecastName1, _sevenDayForecastDescription2, _sevenDayForecastWindSpeed2, _sevenDayForecastWindDirectionRotation2, _sevenDayForecastWeatherIcon2a,
        _sevenDayForecastWeatherIcon2b,
        _sevenDayForecastTemp2, _sevenDayForecastName2, _sevenDayForecastDescription3, _sevenDayForecastWindSpeed3, _sevenDayForecastWindDirectionRotation3,
        _sevenDayForecastWeatherIcon3a, _sevenDayForecastWeatherIcon3b, _sevenDayForecastTemp3, _sevenDayForecastName3, _sevenDayForecastDescription4, _sevenDayForecastWindSpeed4,
        _sevenDayForecastWindDirectionRotation4, _sevenDayForecastWeatherIcon4a, _sevenDayForecastWeatherIcon4b, _sevenDayForecastTemp4, _sevenDayForecastName4,
        _sevenDayForecastDescription5,
        _sevenDayForecastWindSpeed5, _sevenDayForecastWindDirectionRotation5, _sevenDayForecastWeatherIcon5a, _sevenDayForecastWeatherIcon5b, _sevenDayForecastTemp5,
        _sevenDayForecastName5,
        _sevenDayForecastDescription6, _sevenDayForecastWindSpeed6, _sevenDayForecastWindDirectionRotation6, _sevenDayForecastWeatherIcon6a, _sevenDayForecastWeatherIcon6b,
        _sevenDayForecastTemp6,
        _sevenDayForecastName6,
        _sevenDayForecastDescription7, _sevenDayForecastWindSpeed7, _sevenDayForecastWindDirectionRotation7, _sevenDayForecastWeatherIcon7a, _sevenDayForecastWeatherIcon7b,
        _sevenDayForecastTemp7,
        _sevenDayForecastName7,
        _sevenDayForecastDescription8, _sevenDayForecastWindSpeed8, _sevenDayForecastWindDirectionRotation8, _sevenDayForecastWeatherIcon8a, _sevenDayForecastWeatherIcon8b,
        _sevenDayForecastTemp8,
        _sevenDayForecastName8,
        _sevenDayForecastDescription9, _sevenDayForecastWindSpeed9, _sevenDayForecastWindDirectionRotation9, _sevenDayForecastWeatherIcon9a, _sevenDayForecastWeatherIcon9b,
        _sevenDayForecastTemp9,
        _sevenDayForecastName9,
        _sevenDayForecastDescription10, _sevenDayForecastWindSpeed10, _sevenDayForecastWindDirectionRotation10, _sevenDayForecastWeatherIcon10a, _sevenDayForecastWeatherIcon10b,
        _sevenDayForecastTemp10,
        _sevenDayForecastName10,
        _sevenDayForecastDescription11, _sevenDayForecastWindSpeed11, _sevenDayForecastWindDirectionRotation11, _sevenDayForecastWeatherIcon11a, _sevenDayForecastWeatherIcon11b,
        _sevenDayForecastTemp11,
        _sevenDayForecastName11,
        _sevenDayForecastDescription12, _sevenDayForecastWindSpeed12, _sevenDayForecastWindDirectionRotation12, _sevenDayForecastWeatherIcon12a, _sevenDayForecastWeatherIcon12b,
        _sevenDayForecastTemp12,
        _sevenDayForecastName12,
        _sevenDayForecastDescription13, _sevenDayForecastWindSpeed13, _sevenDayForecastWindDirectionRotation13, _sevenDayForecastWeatherIcon13a, _sevenDayForecastWeatherIcon13b,
        _sevenDayForecastTemp13,
        _sevenDayForecastName13,
        _sevenDayForecastDescription14, _sevenDayForecastWindSpeed14, _sevenDayForecastWindDirectionRotation14, _sevenDayForecastWeatherIcon14a, _sevenDayForecastWeatherIcon14b,
        _sevenDayForecastTemp14,
        _sevenDayForecastName14, _hourlyForecastTime1, _hourlyForecastWeatherIcon1, _hourlyForecastTemp1, _hourlyForecastRainChance1, _hourlyForecastWindSpeed1,
        _hourlyForecastTime2, _hourlyForecastWeatherIcon2, _hourlyForecastTemp2, _hourlyForecastRainChance2, _hourlyForecastWindSpeed2,
        _hourlyForecastTime3, _hourlyForecastWeatherIcon3, _hourlyForecastTemp3, _hourlyForecastRainChance3, _hourlyForecastWindSpeed3,
        _hourlyForecastTime4, _hourlyForecastWeatherIcon4, _hourlyForecastTemp4, _hourlyForecastRainChance4, _hourlyForecastWindSpeed4,
        _hourlyForecastTime5, _hourlyForecastWeatherIcon5, _hourlyForecastTemp5, _hourlyForecastRainChance5, _hourlyForecastWindSpeed5,
        _hourlyForecastTime6, _hourlyForecastWeatherIcon6, _hourlyForecastTemp6, _hourlyForecastRainChance6, _hourlyForecastWindSpeed6,
        _hourlyForecastTime7, _hourlyForecastWeatherIcon7, _hourlyForecastTemp7, _hourlyForecastRainChance7, _hourlyForecastWindSpeed7,
        _hourlyForecastTime8, _hourlyForecastWeatherIcon8, _hourlyForecastTemp8, _hourlyForecastRainChance8, _hourlyForecastWindSpeed8,
        _hourlyForecastTime9, _hourlyForecastWeatherIcon9, _hourlyForecastTemp9, _hourlyForecastRainChance9, _hourlyForecastWindSpeed9,
        _weatherOverlay, _thermometerDisplayIcon, _hourlyForecastRainIcon2, _hourlyForecastRainIcon3, _hourlyForecastRainIcon4, _hourlyForecastRainIcon5,
        _hourlyForecastRainIcon6, _hourlyForecastRainIcon7, _hourlyForecastRainIcon8, _hourlyForecastRainIcon9;

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

        /* Timer used to update time and weather. It also pushes an update to calendar when the date changes. */
        DispatcherTimer dispatcherTimer = new();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e) {
        CurrentDateText = DateTime.Now.ToLongDateString();
        CurrentTimeText = DateTime.Now.ToString("HHmm");
        CurrentTimeSecondsText = DateTime.Now.ToString("ss");
        poolWeather++;

        if (!currentTime.Day.Equals(DateTime.Now.Day)) {
            Console.WriteLine("Updating calendar date");
            CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
            simpleMessenger.PushMessage("RealDateChanged", null);
        }

        /* Update weather every 15 minutes or when hour changes */
#pragma warning disable CS0162
        if ((poolWeather > 900 || updateForecast || updateForecastHourly) && !string.IsNullOrEmpty(ReferenceValues.UserAgent) && ReferenceValues.EnableWeather) {
            Console.WriteLine("Updating weather @" + DateTime.Now.ToLongTimeString());
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

            try {
                using WebClient client2 = new();
                Uri weatherForecastHourlyURL = new("https://api.weather.gov/gridpoints/OHX/42,62/forecast/hourly");
                client2.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.UserAgent);
                string weatherForecastHourly = client2.DownloadString(weatherForecastHourlyURL);
                forecastHourly = JsonSerializer.Deserialize<JsonWeatherForecastHourly>(weatherForecastHourly, options);
                updateForecastHourly = false;
            } catch (Exception) {
                errored = true;
            }

            if (!errored) {
                UpdateWeatherForecast();
            }

            poolWeather = 0;
        }
#pragma warning restore CS0162
    }

    public void UpdateWeatherForecast() {
        string[] weatherIcons = new string[2];

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

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[0].shortForecast);
            SevenDayForecastWeatherIcon1a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[0].isDaytime);
            SevenDayForecastWeatherIcon1b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[0].isDaytime) : "null";

            SevenDayForecastWindSpeed1 = forecast.properties.periods[0].windSpeed;
            SevenDayForecastDescription1 = forecast.properties.periods[0].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName2 = forecast.properties.periods[1].name;
            SevenDayForecastTemp2 = forecast.properties.periods[1].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[1].shortForecast);
            SevenDayForecastWeatherIcon2a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[1].isDaytime);
            SevenDayForecastWeatherIcon2b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[1].isDaytime) : "null";

            SevenDayForecastWindSpeed2 = forecast.properties.periods[1].windSpeed;
            SevenDayForecastDescription2 = forecast.properties.periods[1].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName3 = forecast.properties.periods[2].name;
            SevenDayForecastTemp3 = forecast.properties.periods[2].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[2].shortForecast);
            SevenDayForecastWeatherIcon3a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[2].isDaytime);
            SevenDayForecastWeatherIcon3b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[2].isDaytime) : "null";

            SevenDayForecastWindSpeed3 = forecast.properties.periods[2].windSpeed;
            SevenDayForecastDescription3 = forecast.properties.periods[2].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName4 = forecast.properties.periods[3].name;
            SevenDayForecastTemp4 = forecast.properties.periods[3].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[3].shortForecast);
            SevenDayForecastWeatherIcon4a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[3].isDaytime);
            SevenDayForecastWeatherIcon4b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[3].isDaytime) : "null";

            SevenDayForecastWindSpeed4 = forecast.properties.periods[3].windSpeed;
            SevenDayForecastDescription4 = forecast.properties.periods[3].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName5 = forecast.properties.periods[4].name;
            SevenDayForecastTemp5 = forecast.properties.periods[4].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[4].shortForecast);
            SevenDayForecastWeatherIcon5a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[4].isDaytime);
            SevenDayForecastWeatherIcon5b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[4].isDaytime) : "null";

            SevenDayForecastWindSpeed5 = forecast.properties.periods[4].windSpeed;
            SevenDayForecastDescription5 = forecast.properties.periods[4].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName6 = forecast.properties.periods[5].name;
            SevenDayForecastTemp6 = forecast.properties.periods[5].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[5].shortForecast);
            SevenDayForecastWeatherIcon6a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[5].isDaytime);
            SevenDayForecastWeatherIcon6b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[5].isDaytime) : "null";

            SevenDayForecastWindSpeed6 = forecast.properties.periods[5].windSpeed;
            SevenDayForecastDescription6 = forecast.properties.periods[5].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName7 = forecast.properties.periods[6].name;
            SevenDayForecastTemp7 = forecast.properties.periods[6].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[6].shortForecast);
            SevenDayForecastWeatherIcon7a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[6].isDaytime);
            SevenDayForecastWeatherIcon7b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[6].isDaytime) : "null";

            SevenDayForecastWindSpeed7 = forecast.properties.periods[6].windSpeed;
            SevenDayForecastDescription7 = forecast.properties.periods[6].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName8 = forecast.properties.periods[7].name;
            SevenDayForecastTemp8 = forecast.properties.periods[7].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[7].shortForecast);
            SevenDayForecastWeatherIcon8a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[7].isDaytime);
            SevenDayForecastWeatherIcon8b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[7].isDaytime) : "null";

            SevenDayForecastWindSpeed8 = forecast.properties.periods[7].windSpeed;
            SevenDayForecastDescription8 = forecast.properties.periods[7].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName9 = forecast.properties.periods[8].name;
            SevenDayForecastTemp9 = forecast.properties.periods[8].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[8].shortForecast);
            SevenDayForecastWeatherIcon9a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[8].isDaytime);
            SevenDayForecastWeatherIcon9b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[8].isDaytime) : "null";

            SevenDayForecastWindSpeed9 = forecast.properties.periods[8].windSpeed;
            SevenDayForecastDescription9 = forecast.properties.periods[8].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName10 = forecast.properties.periods[9].name;
            SevenDayForecastTemp10 = forecast.properties.periods[9].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[9].shortForecast);
            SevenDayForecastWeatherIcon10a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[9].isDaytime);
            SevenDayForecastWeatherIcon10b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[9].isDaytime) : "null";

            SevenDayForecastWindSpeed10 = forecast.properties.periods[9].windSpeed;
            SevenDayForecastDescription10 = forecast.properties.periods[9].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName11 = forecast.properties.periods[10].name;
            SevenDayForecastTemp11 = forecast.properties.periods[10].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[10].shortForecast);
            SevenDayForecastWeatherIcon11a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[10].isDaytime);
            SevenDayForecastWeatherIcon11b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[10].isDaytime) : "null";

            SevenDayForecastWindSpeed11 = forecast.properties.periods[10].windSpeed;
            SevenDayForecastDescription11 = forecast.properties.periods[10].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName12 = forecast.properties.periods[11].name;
            SevenDayForecastTemp12 = forecast.properties.periods[11].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[11].shortForecast);
            SevenDayForecastWeatherIcon12a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[11].isDaytime);
            SevenDayForecastWeatherIcon12b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[11].isDaytime) : "null";

            SevenDayForecastWindSpeed12 = forecast.properties.periods[11].windSpeed;
            SevenDayForecastDescription12 = forecast.properties.periods[11].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName13 = forecast.properties.periods[12].name;
            SevenDayForecastTemp13 = forecast.properties.periods[12].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[12].shortForecast);
            SevenDayForecastWeatherIcon13a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[12].isDaytime);
            SevenDayForecastWeatherIcon13b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[12].isDaytime) : "null";

            SevenDayForecastWindSpeed13 = forecast.properties.periods[12].windSpeed;
            SevenDayForecastDescription13 = forecast.properties.periods[12].shortForecast;
        } catch (Exception) { }

        try {
            SevenDayForecastName14 = forecast.properties.periods[13].name;
            SevenDayForecastTemp14 = forecast.properties.periods[13].temperature.ToString() + '°';

            weatherIcons = RegexWeatherForecast(forecast.properties.periods[13].shortForecast);
            SevenDayForecastWeatherIcon14a = GetWeatherIcon(weatherIcons[0], forecast.properties.periods[13].isDaytime);
            SevenDayForecastWeatherIcon14b = weatherIcons.Length > 1 ? GetWeatherIcon(weatherIcons[1], forecast.properties.periods[13].isDaytime) : "null";

            SevenDayForecastWindSpeed14 = forecast.properties.periods[13].windSpeed;
            SevenDayForecastDescription14 = forecast.properties.periods[13].shortForecast;
        } catch (Exception) { }

        try {
            HourlyForecastTime1 = forecastHourly.properties.periods[1].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon1 = GetWeatherIcon(forecastHourly.properties.periods[1].shortForecast, forecastHourly.properties.periods[1].isDaytime);
            HourlyForecastTemp1 = forecastHourly.properties.periods[1].temperature.ToString() + '°';
            HourlyForecastRainChance1 = GetRainChance(forecastHourly.properties.periods[1].icon);
            HourlyForecastRainIcon1 = GetRainIcon(forecastHourly.properties.periods[1].shortForecast);
            HourlyForecastWindSpeed1 = forecastHourly.properties.periods[1].windSpeed;
            HourlyForecastWindDirectionRotation1 = GetWindRotation(forecastHourly.properties.periods[1].windDirection);
        } catch (Exception) { }

        try {
            HourlyForecastTime2 = forecastHourly.properties.periods[2].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon2 = GetWeatherIcon(forecastHourly.properties.periods[2].shortForecast, forecastHourly.properties.periods[2].isDaytime);
            HourlyForecastTemp2 = forecastHourly.properties.periods[2].temperature.ToString() + '°';
            HourlyForecastRainChance2 = GetRainChance(forecastHourly.properties.periods[2].icon);
            HourlyForecastRainIcon2 = GetRainIcon(forecastHourly.properties.periods[2].shortForecast);
            HourlyForecastWindSpeed2 = forecastHourly.properties.periods[2].windSpeed;
            HourlyForecastWindDirectionRotation2 = GetWindRotation(forecastHourly.properties.periods[2].windDirection);
        } catch (Exception) { }

        try {
            HourlyForecastTime3 = forecastHourly.properties.periods[3].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon3 = GetWeatherIcon(forecastHourly.properties.periods[3].shortForecast, forecastHourly.properties.periods[3].isDaytime);
            HourlyForecastTemp3 = forecastHourly.properties.periods[3].temperature.ToString() + '°';
            HourlyForecastRainChance3 = GetRainChance(forecastHourly.properties.periods[3].icon);
            HourlyForecastRainIcon3 = GetRainIcon(forecastHourly.properties.periods[3].shortForecast);
            HourlyForecastWindSpeed3 = forecastHourly.properties.periods[3].windSpeed;
            HourlyForecastWindDirectionRotation3 = GetWindRotation(forecastHourly.properties.periods[3].windDirection);
        } catch (Exception) { }

        try {
            HourlyForecastTime4 = forecastHourly.properties.periods[4].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon4 = GetWeatherIcon(forecastHourly.properties.periods[4].shortForecast, forecastHourly.properties.periods[4].isDaytime);
            HourlyForecastTemp4 = forecastHourly.properties.periods[4].temperature.ToString() + '°';
            HourlyForecastRainChance4 = GetRainChance(forecastHourly.properties.periods[4].icon);
            HourlyForecastRainIcon4 = GetRainIcon(forecastHourly.properties.periods[4].shortForecast);
            HourlyForecastWindSpeed4 = forecastHourly.properties.periods[4].windSpeed;
            HourlyForecastWindDirectionRotation4 = GetWindRotation(forecastHourly.properties.periods[4].windDirection);
        } catch (Exception) { }

        try {
            HourlyForecastTime5 = forecastHourly.properties.periods[5].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon5 = GetWeatherIcon(forecastHourly.properties.periods[5].shortForecast, forecastHourly.properties.periods[5].isDaytime);
            HourlyForecastTemp5 = forecastHourly.properties.periods[5].temperature.ToString() + '°';
            HourlyForecastRainChance5 = GetRainChance(forecastHourly.properties.periods[5].icon);
            HourlyForecastRainIcon5 = GetRainIcon(forecastHourly.properties.periods[5].shortForecast);
            HourlyForecastWindSpeed5 = forecastHourly.properties.periods[5].windSpeed;
            HourlyForecastWindDirectionRotation5 = GetWindRotation(forecastHourly.properties.periods[5].windDirection);
        } catch (Exception) { }

        try {
            HourlyForecastTime6 = forecastHourly.properties.periods[6].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon6 = GetWeatherIcon(forecastHourly.properties.periods[6].shortForecast, forecastHourly.properties.periods[6].isDaytime);
            HourlyForecastTemp6 = forecastHourly.properties.periods[6].temperature.ToString() + '°';
            HourlyForecastRainChance6 = GetRainChance(forecastHourly.properties.periods[6].icon);
            HourlyForecastRainIcon6 = GetRainIcon(forecastHourly.properties.periods[6].shortForecast);
            HourlyForecastWindSpeed6 = forecastHourly.properties.periods[6].windSpeed;
            HourlyForecastWindDirectionRotation6 = GetWindRotation(forecastHourly.properties.periods[6].windDirection);
        } catch (Exception) { }

        try {
            HourlyForecastTime7 = forecastHourly.properties.periods[7].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon7 = GetWeatherIcon(forecastHourly.properties.periods[7].shortForecast, forecastHourly.properties.periods[7].isDaytime);
            HourlyForecastTemp7 = forecastHourly.properties.periods[7].temperature.ToString() + '°';
            HourlyForecastRainChance7 = GetRainChance(forecastHourly.properties.periods[7].icon);
            HourlyForecastRainIcon7 = GetRainIcon(forecastHourly.properties.periods[7].shortForecast);
            HourlyForecastWindSpeed7 = forecastHourly.properties.periods[7].windSpeed;
            HourlyForecastWindDirectionRotation7 = GetWindRotation(forecastHourly.properties.periods[7].windDirection);
        } catch (Exception) { }

        try {
            HourlyForecastTime8 = forecastHourly.properties.periods[8].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon8 = GetWeatherIcon(forecastHourly.properties.periods[8].shortForecast, forecastHourly.properties.periods[8].isDaytime);
            HourlyForecastTemp8 = forecastHourly.properties.periods[8].temperature.ToString() + '°';
            HourlyForecastRainChance8 = GetRainChance(forecastHourly.properties.periods[8].icon);
            HourlyForecastRainIcon8 = GetRainIcon(forecastHourly.properties.periods[8].icon);
            HourlyForecastWindSpeed8 = forecastHourly.properties.periods[8].windSpeed;
            HourlyForecastWindDirectionRotation8 = GetWindRotation(forecastHourly.properties.periods[8].windDirection);
        } catch (Exception) { }

        try {
            HourlyForecastTime9 = forecastHourly.properties.periods[9].startTime.ToString("HHmm");
            HourlyForecastWeatherIcon9 = GetWeatherIcon(forecastHourly.properties.periods[9].shortForecast, forecastHourly.properties.periods[9].isDaytime);
            HourlyForecastTemp9 = forecastHourly.properties.periods[9].temperature.ToString() + '°';
            HourlyForecastRainChance9 = GetRainChance(forecastHourly.properties.periods[9].icon);
            HourlyForecastRainIcon9 = GetRainIcon(forecastHourly.properties.periods[9].shortForecast);
            HourlyForecastWindSpeed9 = forecastHourly.properties.periods[9].windSpeed;
            HourlyForecastWindDirectionRotation9 = GetWindRotation(forecastHourly.properties.periods[9].windDirection);
        } catch (Exception) { }

        /* Add Thermometer if needed */
        if (forecastHourly.properties.periods[0].temperature < 20) {
            ThermometerDisplayIcon = "../../Resources/Images/Icons/temp_freezing.png";
            WeatherOverlay = "../../Resources/Images/weather/cold_border.png";
        } else if (forecastHourly.properties.periods[0].temperature >= 20 && forecastHourly.properties.periods[0].temperature < 35) {
            ThermometerDisplayIcon = "../../Resources/Images/Icons/temp_cold.png";
        } else if (forecastHourly.properties.periods[0].temperature >= 35 && forecastHourly.properties.periods[0].temperature < 60) {
            ThermometerDisplayIcon = "../../Resources/Images/Icons/temp_cool.png";
        } else if (forecastHourly.properties.periods[0].temperature >= 60 && forecastHourly.properties.periods[0].temperature < 85) {
            ThermometerDisplayIcon = "../../Resources/Images/Icons/temp_warm.png";
        } else if (forecastHourly.properties.periods[0].temperature >= 85 && forecastHourly.properties.periods[0].temperature < 100) {
            ThermometerDisplayIcon = "../../Resources/Images/Icons/temp_hot.png";
        } else {
            ThermometerDisplayIcon = "../../Resources/Images/Icons/temp_burning.png";
            WeatherOverlay = "../../Resources/Images/weather/fire_border.png";
        }
    }

    private string[] RegexWeatherForecast(string input) {
        return input.Split(new[] { " then " }, StringSplitOptions.None);
    }

    private string GetRainIcon(string input) {
        switch (input) {
        case "Snow Showers":
        case "Chance Snow Showers":
        case "Scattered Snow Showers":
        case "Isolated Snow Showers":
            return "./../Resources/Images/weather/snow_drop.png";
        default:
            return "../../Resources/Images/weather/rain_drop.png";
        }
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
        case "Partly Sunny":
            return isDayTime
                ? "../../Resources/Images/weather/weather_part_cloudy.png"
                : "../../Resources/Images/weather/weather_cloudy_night.png";
        case "Cloudy":
        case "Mostly Cloudy":
            return "../../Resources/Images/weather/weather_cloudy.png";
        case "Patchy Fog":
        case "Areas Of Fog":
            return "../../Resources/Images/weather/weather_fog.png";
        case "Slight Chance Very Light Rain":
        case "Slight Chance Light Rain":
        case "Chance Very Light Rain":
        case "Chance Light Rain":
        case "Areas Of Drizzle":
            return "../../Resources/Images/weather/weather_rain_light.png";
        case "Rain Showers Likely":
        case "Rain Likely":
        case "Rain Showers":
        case "Chance Rain Showers":
        case "Slight Chance Rain Showers":
            return "../../Resources/Images/weather/weather_rain_medium.png";
        case "Showers And Thunderstorms":
        case "Showers And Thunderstorms Likely":
        case "Chance Showers And Thunderstorms":
        case "Slight Chance Showers And Thunderstorms":
            return "../../Resources/Images/weather/weather_storm.png";
        case "Widespread Frost":
            return "../../Resources/Images/weather/weather_frost.png";
        case "Snow Showers":
        case "Scattered Snow Showers":
        case "Chance Snow Showers":
        case "Isolated Snow Showers":
            return "../../Resources/Images/weather/weather_snow.png";
        default:
            return "null";
        }
    }

    private string GetRainChance(string icon) {
        Regex rg = new(@"[,]\d[0-9]");
        icon = rg.Match(icon).Value;
        if (string.IsNullOrEmpty(icon)) {
            return "0%";
        }

        return icon.Substring(1) + '%';
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

    public string WeatherOverlay {
        get => _weatherOverlay;
        set {
            _weatherOverlay = value;
            RaisePropertyChangedEvent("WeatherOverlay");
        }
    }

    public int HourlyForecastWindDirectionRotation1 {
        get => _hourlyForecastWindDirectionRotation1;
        set {
            _hourlyForecastWindDirectionRotation1 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation1");
        }
    }

    public int HourlyForecastWindDirectionRotation2 {
        get => _hourlyForecastWindDirectionRotation2;
        set {
            _hourlyForecastWindDirectionRotation2 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation2");
        }
    }

    public int HourlyForecastWindDirectionRotation3 {
        get => _hourlyForecastWindDirectionRotation3;
        set {
            _hourlyForecastWindDirectionRotation3 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation3");
        }
    }

    public int HourlyForecastWindDirectionRotation4 {
        get => _hourlyForecastWindDirectionRotation4;
        set {
            _hourlyForecastWindDirectionRotation4 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation4");
        }
    }

    public int HourlyForecastWindDirectionRotation5 {
        get => _hourlyForecastWindDirectionRotation5;
        set {
            _hourlyForecastWindDirectionRotation5 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation5");
        }
    }

    public int HourlyForecastWindDirectionRotation6 {
        get => _hourlyForecastWindDirectionRotation6;
        set {
            _hourlyForecastWindDirectionRotation6 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation6");
        }
    }

    public int HourlyForecastWindDirectionRotation7 {
        get => _hourlyForecastWindDirectionRotation7;
        set {
            _hourlyForecastWindDirectionRotation7 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation7");
        }
    }

    public int HourlyForecastWindDirectionRotation8 {
        get => _hourlyForecastWindDirectionRotation8;
        set {
            _hourlyForecastWindDirectionRotation8 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation8");
        }
    }

    public int HourlyForecastWindDirectionRotation9 {
        get => _hourlyForecastWindDirectionRotation9;
        set {
            _hourlyForecastWindDirectionRotation9 = value;
            RaisePropertyChangedEvent("HourlyForecastWindDirectionRotation9");
        }
    }

    public string ThermometerDisplayIcon {
        get => _thermometerDisplayIcon;
        set {
            _thermometerDisplayIcon = value;
            RaisePropertyChangedEvent("ThermometerDisplayIcon");
        }
    }

    public string HourlyForecastRainIcon1 {
        get => _hourlyForecastRainIcon1;
        set {
            _hourlyForecastRainIcon1 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon1");
        }
    }

    public string HourlyForecastRainIcon2 {
        get => _hourlyForecastRainIcon2;
        set {
            _hourlyForecastRainIcon2 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon2");
        }
    }

    public string HourlyForecastRainIcon3 {
        get => _hourlyForecastRainIcon3;
        set {
            _hourlyForecastRainIcon3 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon3");
        }
    }

    public string HourlyForecastRainIcon4 {
        get => _hourlyForecastRainIcon4;
        set {
            _hourlyForecastRainIcon4 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon4");
        }
    }

    public string HourlyForecastRainIcon5 {
        get => _hourlyForecastRainIcon5;
        set {
            _hourlyForecastRainIcon5 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon5");
        }
    }

    public string HourlyForecastRainIcon6 {
        get => _hourlyForecastRainIcon6;
        set {
            _hourlyForecastRainIcon6 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon6");
        }
    }

    public string HourlyForecastRainIcon7 {
        get => _hourlyForecastRainIcon7;
        set {
            _hourlyForecastRainIcon7 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon7");
        }
    }

    public string HourlyForecastRainIcon8 {
        get => _hourlyForecastRainIcon8;
        set {
            _hourlyForecastRainIcon8 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon8");
        }
    }

    public string HourlyForecastRainIcon9 {
        get => _hourlyForecastRainIcon9;
        set {
            _hourlyForecastRainIcon9 = value;
            RaisePropertyChangedEvent("HourlyForecastRainIcon9");
        }
    }

    #endregion
}