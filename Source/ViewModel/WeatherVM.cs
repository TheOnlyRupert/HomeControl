﻿using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class WeatherVM : BaseViewModel {
    private ObservableCollection<WeatherHourlyBlock> _forecastHourlyList;

    private string _sevenDayForecastTemp1, _sevenDayForecastName1;

    private string _sevenDayForecastTemp10, _sevenDayForecastName10;

    private string _sevenDayForecastTemp11, _sevenDayForecastName11;

    private string _sevenDayForecastTemp12, _sevenDayForecastName12;

    private string _sevenDayForecastTemp13, _sevenDayForecastName13;

    private string _sevenDayForecastTemp14, _sevenDayForecastName14, _sevenDayForecastRainChance1,
        _sevenDayForecastRainChance2, _sevenDayForecastRainChance3, _sevenDayForecastRainChance4, _sevenDayForecastRainChance5, _sevenDayForecastRainChance6, _sevenDayForecastRainChance7,
        _sevenDayForecastRainChance8, _sevenDayForecastRainChance9, _sevenDayForecastRainChance10, _sevenDayForecastRainChance11, _sevenDayForecastRainChance12, _sevenDayForecastRainChance13,
        _sevenDayForecastRainChance14, _forecastHourlyVisibility, _easterEggVisibility;

    private string _sevenDayForecastTemp2, _sevenDayForecastName2;

    private string _sevenDayForecastTemp3, _sevenDayForecastName3;

    private string _sevenDayForecastTemp4,
        _sevenDayForecastName4;

    private string _sevenDayForecastTemp5, _sevenDayForecastName5;

    private string _sevenDayForecastTemp6, _sevenDayForecastName6;

    private string _sevenDayForecastTemp7, _sevenDayForecastName7;

    private string _sevenDayForecastTemp8,
        _sevenDayForecastName8;

    private string _sevenDayForecastTemp9, _sevenDayForecastName9;

    private string? _sevenDayForecastWeatherIcon10a;

    private string? _sevenDayForecastWeatherIcon10b;

    private string? _sevenDayForecastWeatherIcon11a;

    private string? _sevenDayForecastWeatherIcon11b;

    private string? _sevenDayForecastWeatherIcon12a;

    private string? _sevenDayForecastWeatherIcon12b;

    private string? _sevenDayForecastWeatherIcon13a;

    private string? _sevenDayForecastWeatherIcon13b;

    private string? _sevenDayForecastWeatherIcon14a;

    private string? _sevenDayForecastWeatherIcon14b;

    private string? _sevenDayForecastWeatherIcon1a;

    private string? _sevenDayForecastWeatherIcon1b;

    private string? _sevenDayForecastWeatherIcon2a;

    private string? _sevenDayForecastWeatherIcon2b;

    private string? _sevenDayForecastWeatherIcon3a;

    private string? _sevenDayForecastWeatherIcon3b;

    private string? _sevenDayForecastWeatherIcon4a;

    private string? _sevenDayForecastWeatherIcon4b;

    private string? _sevenDayForecastWeatherIcon5a;

    private string? _sevenDayForecastWeatherIcon5b;

    private string? _sevenDayForecastWeatherIcon6a;

    private string? _sevenDayForecastWeatherIcon6b;

    private string? _sevenDayForecastWeatherIcon7a;

    private string? _sevenDayForecastWeatherIcon7b;

    private string? _sevenDayForecastWeatherIcon8a;

    private string? _sevenDayForecastWeatherIcon8b;

    private string? _sevenDayForecastWeatherIcon9a;

    private string? _sevenDayForecastWeatherIcon9b;

    private int _sevenDayForecastWindDirectionIcon1, _sevenDayForecastWindDirectionIcon2, _sevenDayForecastWindDirectionIcon3, _sevenDayForecastWindDirectionIcon4, _sevenDayForecastWindDirectionIcon5,
        _sevenDayForecastWindDirectionIcon6, _sevenDayForecastWindDirectionIcon7, _sevenDayForecastWindDirectionIcon8, _sevenDayForecastWindDirectionIcon9, _sevenDayForecastWindDirectionIcon10,
        _sevenDayForecastWindDirectionIcon11, _sevenDayForecastWindDirectionIcon12, _sevenDayForecastWindDirectionIcon13, _sevenDayForecastWindDirectionIcon14, _tempMin, _tempMax;

    private string? _sevenDayForecastWindSpeed1;
    private string? _sevenDayForecastWindSpeed10;
    private string? _sevenDayForecastWindSpeed11;
    private string? _sevenDayForecastWindSpeed12;
    private string? _sevenDayForecastWindSpeed13;

    private string? _sevenDayForecastWindSpeed14;
    private string? _sevenDayForecastWindSpeed2;
    private string? _sevenDayForecastWindSpeed3;
    private string? _sevenDayForecastWindSpeed4;

    private string? _sevenDayForecastWindSpeed5;
    private string? _sevenDayForecastWindSpeed6;
    private string? _sevenDayForecastWindSpeed7;
    private string? _sevenDayForecastWindSpeed8;

    private string? _sevenDayForecastWindSpeed9;

    public WeatherVM() {
        ForecastHourlyList = [];
        UpdateWeather();

        /* Show Elf on Dec 24rd (Christmas Eve) */
        if (ReferenceValues.JsonSettingsMaster.EnableEasterEggs && DateTime.Now.Month == 12 && DateTime.Now.Day == 24) {
            EasterEggVisibility = "VISIBLE";
        } else {
            EasterEggVisibility = "HIDDEN";
        }

        ForecastHourlyVisibility = "HIDDEN";

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "MinChanged":
            UpdateWeather();
            break;
        }
    }

    private async void UpdateWeather() {
        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            Uri weatherForecastURL =
                new($"https://api.weather.gov/gridpoints/{ReferenceValues.JsonSettingsMaster.GridId}/{ReferenceValues.JsonSettingsMaster.GridX},{ReferenceValues.JsonSettingsMaster.GridY}/forecast");

            using WebClient client = new();
            client.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonSettingsMaster.UserAgent);
            string weatherForecast = await client.DownloadStringTaskAsync(weatherForecastURL);
            ReferenceValues.ForecastSevenDay = JsonSerializer.Deserialize<JsonWeather>(weatherForecast, options);

            SevenDayForecastName1 = ReferenceValues.ForecastSevenDay.properties.periods[0].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[0].startTime.ToString("MM/dd");
            SevenDayForecastTemp1 = ReferenceValues.ForecastSevenDay.properties.periods[0].temperature + "°";

            string?[] weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[0].shortForecast);
            SevenDayForecastWeatherIcon1a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[0].isDaytime,
                ReferenceValues.ForecastSevenDay.properties.periods[0].temperature,
                ReferenceValues.ForecastSevenDay.properties.periods[0].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon1b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[0].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[0].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[0].windSpeed);
            } else {
                SevenDayForecastWeatherIcon1b = "";
            }

            SevenDayForecastRainChance1 = ReferenceValues.ForecastSevenDay.properties.periods[0].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance1)) {
                SevenDayForecastRainChance1 += "%";
            } else {
                SevenDayForecastRainChance1 = "None";
            }

            SevenDayForecastWindDirectionIcon1 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[0].windDirection);
            SevenDayForecastWindSpeed1 = ReferenceValues.ForecastSevenDay.properties.periods[0].windSpeed;

            SevenDayForecastName2 = ReferenceValues.ForecastSevenDay.properties.periods[1].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[1].startTime.ToString("MM/dd");
            SevenDayForecastTemp2 = ReferenceValues.ForecastSevenDay.properties.periods[1].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[1].shortForecast);
            SevenDayForecastWeatherIcon2a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[1].isDaytime, ReferenceValues.ForecastSevenDay.properties
                .periods[1]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[1].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon2b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[1].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[1].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[1].windSpeed);
            } else {
                SevenDayForecastWeatherIcon2b = "";
            }

            SevenDayForecastRainChance2 = ReferenceValues.ForecastSevenDay.properties.periods[1].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance2)) {
                SevenDayForecastRainChance2 += "%";
            } else {
                SevenDayForecastRainChance2 = "None";
            }

            SevenDayForecastWindDirectionIcon2 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[1].windDirection);
            SevenDayForecastWindSpeed2 = ReferenceValues.ForecastSevenDay.properties.periods[1].windSpeed;

            SevenDayForecastName3 = ReferenceValues.ForecastSevenDay.properties.periods[2].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[2].startTime.ToString("MM/dd");
            SevenDayForecastTemp3 = ReferenceValues.ForecastSevenDay.properties.periods[2].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[2].shortForecast);
            SevenDayForecastWeatherIcon3a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[2].isDaytime, ReferenceValues.ForecastSevenDay.properties
                .periods[2]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[2].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon3b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[2].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[2].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[2].windSpeed);
            } else {
                SevenDayForecastWeatherIcon3b = "";
            }

            SevenDayForecastRainChance3 = ReferenceValues.ForecastSevenDay.properties.periods[2].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance3)) {
                SevenDayForecastRainChance3 += "%";
            } else {
                SevenDayForecastRainChance3 = "None";
            }

            SevenDayForecastWindDirectionIcon3 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[2].windDirection);
            SevenDayForecastWindSpeed3 = ReferenceValues.ForecastSevenDay.properties.periods[2].windSpeed;

            SevenDayForecastName4 = ReferenceValues.ForecastSevenDay.properties.periods[3].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[3].startTime.ToString("MM/dd");
            SevenDayForecastTemp4 = ReferenceValues.ForecastSevenDay.properties.periods[3].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[3].shortForecast);
            SevenDayForecastWeatherIcon4a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[3].isDaytime, ReferenceValues.ForecastSevenDay.properties
                .periods[3]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[3].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon4b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[3].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[3].temperature, ReferenceValues.ForecastSevenDay.properties.periods[3].windSpeed);
            } else {
                SevenDayForecastWeatherIcon4b = "";
            }

            SevenDayForecastRainChance4 = ReferenceValues.ForecastSevenDay.properties.periods[3].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance4)) {
                SevenDayForecastRainChance4 += "%";
            } else {
                SevenDayForecastRainChance4 = "None";
            }

            SevenDayForecastWindDirectionIcon4 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[3].windDirection);
            SevenDayForecastWindSpeed4 = ReferenceValues.ForecastSevenDay.properties.periods[3].windSpeed;

            SevenDayForecastName5 = ReferenceValues.ForecastSevenDay.properties.periods[4].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[4].startTime.ToString("MM/dd");
            SevenDayForecastTemp5 = ReferenceValues.ForecastSevenDay.properties.periods[4].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[4].shortForecast);
            SevenDayForecastWeatherIcon5a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[4].isDaytime, ReferenceValues.ForecastSevenDay.properties
                .periods[4]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[4].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon5b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[4].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[4].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[4].windSpeed);
            } else {
                SevenDayForecastWeatherIcon5b = "";
            }

            SevenDayForecastRainChance5 = ReferenceValues.ForecastSevenDay.properties.periods[4].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance5)) {
                SevenDayForecastRainChance5 += "%";
            } else {
                SevenDayForecastRainChance5 = "None";
            }

            SevenDayForecastWindDirectionIcon5 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[4].windDirection);
            SevenDayForecastWindSpeed5 = ReferenceValues.ForecastSevenDay.properties.periods[4].windSpeed;

            SevenDayForecastName6 = ReferenceValues.ForecastSevenDay.properties.periods[5].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[5].startTime.ToString("MM/dd");
            SevenDayForecastTemp6 = ReferenceValues.ForecastSevenDay.properties.periods[5].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[5].shortForecast);
            SevenDayForecastWeatherIcon6a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[5].isDaytime, ReferenceValues.ForecastSevenDay.properties
                .periods[5]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[5].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon6b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[5].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[5].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[5].windSpeed);
            } else {
                SevenDayForecastWeatherIcon6b = "";
            }

            SevenDayForecastRainChance6 = ReferenceValues.ForecastSevenDay.properties.periods[5].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance6)) {
                SevenDayForecastRainChance6 += "%";
            } else {
                SevenDayForecastRainChance6 = "None";
            }

            SevenDayForecastWindDirectionIcon6 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[5].windDirection);
            SevenDayForecastWindSpeed6 = ReferenceValues.ForecastSevenDay.properties.periods[5].windSpeed;

            SevenDayForecastName7 = ReferenceValues.ForecastSevenDay.properties.periods[6].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[6].startTime.ToString("MM/dd");
            SevenDayForecastTemp7 = ReferenceValues.ForecastSevenDay.properties.periods[6].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[6].shortForecast);
            SevenDayForecastWeatherIcon7a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[6].isDaytime, ReferenceValues.ForecastSevenDay.properties
                .periods[6]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[6].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon7b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[6].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[6].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[6].windSpeed);
            } else {
                SevenDayForecastWeatherIcon7b = "";
            }

            SevenDayForecastRainChance7 = ReferenceValues.ForecastSevenDay.properties.periods[6].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance7)) {
                SevenDayForecastRainChance7 += "%";
            } else {
                SevenDayForecastRainChance7 = "None";
            }

            SevenDayForecastWindDirectionIcon7 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[6].windDirection);
            SevenDayForecastWindSpeed7 = ReferenceValues.ForecastSevenDay.properties.periods[6].windSpeed;

            SevenDayForecastName8 = ReferenceValues.ForecastSevenDay.properties.periods[7].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[7].startTime.ToString("MM/dd");
            SevenDayForecastTemp8 = ReferenceValues.ForecastSevenDay.properties.periods[7].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[7].shortForecast);
            SevenDayForecastWeatherIcon8a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[7].isDaytime, ReferenceValues.ForecastSevenDay.properties
                .periods[7]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[7].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon8b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[7].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[7].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[7].windSpeed);
            } else {
                SevenDayForecastWeatherIcon8b = "";
            }

            SevenDayForecastRainChance8 = ReferenceValues.ForecastSevenDay.properties.periods[7].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance8)) {
                SevenDayForecastRainChance8 += "%";
            } else {
                SevenDayForecastRainChance8 = "None";
            }

            SevenDayForecastWindDirectionIcon8 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[7].windDirection);
            SevenDayForecastWindSpeed8 = ReferenceValues.ForecastSevenDay.properties.periods[7].windSpeed;

            SevenDayForecastName9 = ReferenceValues.ForecastSevenDay.properties.periods[8].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[8].startTime.ToString("MM/dd");
            SevenDayForecastTemp9 = ReferenceValues.ForecastSevenDay.properties.periods[8].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[8].shortForecast);
            SevenDayForecastWeatherIcon9a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[8].isDaytime, ReferenceValues.ForecastSevenDay.properties
                .periods[8]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[8].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon9b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[8].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[8].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[8].windSpeed);
            } else {
                SevenDayForecastWeatherIcon9b = "";
            }

            SevenDayForecastRainChance9 = ReferenceValues.ForecastSevenDay.properties.periods[8].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance9)) {
                SevenDayForecastRainChance9 += "%";
            } else {
                SevenDayForecastRainChance9 = "None";
            }

            SevenDayForecastWindDirectionIcon9 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[8].windDirection);
            SevenDayForecastWindSpeed9 = ReferenceValues.ForecastSevenDay.properties.periods[8].windSpeed;

            SevenDayForecastName10 = ReferenceValues.ForecastSevenDay.properties.periods[9].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[9].startTime.ToString("MM/dd");
            SevenDayForecastTemp10 = ReferenceValues.ForecastSevenDay.properties.periods[9].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[9].shortForecast);
            SevenDayForecastWeatherIcon10a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[9].isDaytime,
                ReferenceValues.ForecastSevenDay.properties.periods[9].temperature, ReferenceValues.ForecastSevenDay.properties.periods[9].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon10b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[9].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[9].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[9].windSpeed);
            } else {
                SevenDayForecastWeatherIcon10b = "";
            }

            SevenDayForecastRainChance10 = ReferenceValues.ForecastSevenDay.properties.periods[9].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance10)) {
                SevenDayForecastRainChance10 += "%";
            } else {
                SevenDayForecastRainChance10 = "None";
            }

            SevenDayForecastWindDirectionIcon10 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[9].windDirection);
            SevenDayForecastWindSpeed10 = ReferenceValues.ForecastSevenDay.properties.periods[9].windSpeed;

            SevenDayForecastName11 = ReferenceValues.ForecastSevenDay.properties.periods[10].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[10].startTime.ToString("MM/dd");
            SevenDayForecastTemp11 = ReferenceValues.ForecastSevenDay.properties.periods[10].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[10].shortForecast);
            SevenDayForecastWeatherIcon11a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[10].isDaytime,
                ReferenceValues.ForecastSevenDay.properties.periods[10].temperature, ReferenceValues.ForecastSevenDay.properties.periods[10].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon11b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[10].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[10].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[10].windSpeed);
            } else {
                SevenDayForecastWeatherIcon11b = "";
            }

            SevenDayForecastRainChance11 = ReferenceValues.ForecastSevenDay.properties.periods[10].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance11)) {
                SevenDayForecastRainChance11 += "%";
            } else {
                SevenDayForecastRainChance11 = "None";
            }

            SevenDayForecastWindDirectionIcon11 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[10].windDirection);
            SevenDayForecastWindSpeed11 = ReferenceValues.ForecastSevenDay.properties.periods[10].windSpeed;

            SevenDayForecastName12 = ReferenceValues.ForecastSevenDay.properties.periods[11].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[11].startTime.ToString("MM/dd");
            SevenDayForecastTemp12 = ReferenceValues.ForecastSevenDay.properties.periods[11].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[11].shortForecast);
            SevenDayForecastWeatherIcon12a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[11].isDaytime, ReferenceValues.ForecastSevenDay
                .properties.periods[11]
                .temperature, ReferenceValues.ForecastSevenDay.properties.periods[11].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon12b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[11].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[11].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[11].windSpeed);
            } else {
                SevenDayForecastWeatherIcon12b = "";
            }

            SevenDayForecastRainChance12 = ReferenceValues.ForecastSevenDay.properties.periods[11].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance12)) {
                SevenDayForecastRainChance12 += "%";
            } else {
                SevenDayForecastRainChance12 = "None";
            }

            SevenDayForecastWindDirectionIcon12 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[11].windDirection);
            SevenDayForecastWindSpeed12 = ReferenceValues.ForecastSevenDay.properties.periods[11].windSpeed;

            SevenDayForecastName13 = ReferenceValues.ForecastSevenDay.properties.periods[12].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[12].startTime.ToString("MM/dd");
            SevenDayForecastTemp13 = ReferenceValues.ForecastSevenDay.properties.periods[12].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[12].shortForecast);
            SevenDayForecastWeatherIcon13a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[12].isDaytime,
                ReferenceValues.ForecastSevenDay.properties.periods[12].temperature, ReferenceValues.ForecastSevenDay.properties.periods[12].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon13b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[12].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[12].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[12].windSpeed);
            } else {
                SevenDayForecastWeatherIcon13b = "";
            }

            SevenDayForecastRainChance13 = ReferenceValues.ForecastSevenDay.properties.periods[12].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance13)) {
                SevenDayForecastRainChance13 += "%";
            } else {
                SevenDayForecastRainChance13 = "None";
            }

            SevenDayForecastWindDirectionIcon13 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[12].windDirection);
            SevenDayForecastWindSpeed13 = ReferenceValues.ForecastSevenDay.properties.periods[12].windSpeed;

            SevenDayForecastName14 = ReferenceValues.ForecastSevenDay.properties.periods[13].name + "    " + ReferenceValues.ForecastSevenDay.properties.periods[13].startTime.ToString("MM/dd");
            SevenDayForecastTemp14 = ReferenceValues.ForecastSevenDay.properties.periods[13].temperature + "°";

            weatherIcons = WeatherHelpers.RegexWeatherForecast(ReferenceValues.ForecastSevenDay.properties.periods[13].shortForecast);
            SevenDayForecastWeatherIcon14a = WeatherHelpers.GetWeatherIcon(weatherIcons[0], ReferenceValues.ForecastSevenDay.properties.periods[13].isDaytime,
                ReferenceValues.ForecastSevenDay.properties.periods[13].temperature, ReferenceValues.ForecastSevenDay.properties.periods[13].windSpeed);
            if (weatherIcons.Length > 1) {
                SevenDayForecastWeatherIcon14b = WeatherHelpers.GetWeatherIcon(weatherIcons[1], ReferenceValues.ForecastSevenDay.properties.periods[13].isDaytime,
                    ReferenceValues.ForecastSevenDay.properties.periods[13].temperature,
                    ReferenceValues.ForecastSevenDay.properties.periods[13].windSpeed);
            } else {
                SevenDayForecastWeatherIcon14b = "";
            }

            SevenDayForecastRainChance14 = ReferenceValues.ForecastSevenDay.properties.periods[13].probabilityOfPrecipitation.value?.ToString();

            if (!string.IsNullOrEmpty(SevenDayForecastRainChance14)) {
                SevenDayForecastRainChance14 += "%";
            } else {
                SevenDayForecastRainChance14 = "None";
            }

            SevenDayForecastWindDirectionIcon14 = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastSevenDay.properties.periods[13].windDirection);
            SevenDayForecastWindSpeed14 = ReferenceValues.ForecastSevenDay.properties.periods[13].windSpeed;
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "WeatherVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        try {
            using WebClient client2 = new();
            Uri weatherForecastHourlyURL =
                new(
                    $"https://api.weather.gov/gridpoints/{ReferenceValues.JsonSettingsMaster.GridId}/{ReferenceValues.JsonSettingsMaster.GridX},{ReferenceValues.JsonSettingsMaster.GridY}/forecast/hourly");
            client2.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonSettingsMaster.UserAgent);
            string weatherForecastHourly = client2.DownloadString(weatherForecastHourlyURL);
            JsonWeather forecastHourly = JsonSerializer.Deserialize<JsonWeather>(weatherForecastHourly, options);

            ForecastHourlyList.Clear();

            if (forecastHourly != null) {
                TempMin = -200;
                TempMax = 200;

                int max = -200;
                int min = 200;

                foreach (JsonWeather.Periods period in forecastHourly.properties.periods) {
                    if (period.startTime >= DateTime.Now) {
                        ForecastHourlyList.Add(new WeatherHourlyBlock {
                            Number = period.number,
                            Time = period.startTime,
                            WeatherIcon = WeatherHelpers.GetWeatherIcon(period.shortForecast, period.isDaytime, period.temperature, period.windSpeed),
                            Temp = period.temperature,
                            RainIcon = WeatherHelpers.GetRainIcon(period.shortForecast),
                            RainChance = Convert.ToInt32(period.probabilityOfPrecipitation.value.ToString()),
                            WindSpeed = period.windSpeed,
                            WindDirectionIcon = WeatherHelpers.GetWindRotation(period.windDirection),
                            Humidity = period.relativeHumidity.value,
                            ShortForecast = period.shortForecast
                        });
                    }

                    ForecastHourlyVisibility = ForecastHourlyList.Count == 0 ? "HIDDEN" : "VISIBLE";

                    if (period.temperature < min) {
                        min = period.temperature;
                    }

                    if (period.temperature > max) {
                        max = period.temperature;
                    }
                }

                TempMin = min - 5;
                TempMax = max + 5;
            }
        } catch (WebException) {
            //normal
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

    #region Fields

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

    public string? SevenDayForecastWeatherIcon1a {
        get => _sevenDayForecastWeatherIcon1a;
        set {
            _sevenDayForecastWeatherIcon1a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon1a");
        }
    }

    public string? SevenDayForecastWeatherIcon1b {
        get => _sevenDayForecastWeatherIcon1b;
        set {
            _sevenDayForecastWeatherIcon1b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon1b");
        }
    }

    public string? SevenDayForecastWindSpeed1 {
        get => _sevenDayForecastWindSpeed1;
        set {
            _sevenDayForecastWindSpeed1 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed1");
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

    public string? SevenDayForecastWeatherIcon2a {
        get => _sevenDayForecastWeatherIcon2a;
        set {
            _sevenDayForecastWeatherIcon2a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon2a");
        }
    }

    public string? SevenDayForecastWeatherIcon2b {
        get => _sevenDayForecastWeatherIcon2b;
        set {
            _sevenDayForecastWeatherIcon2b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon2b");
        }
    }

    public string? SevenDayForecastWindSpeed2 {
        get => _sevenDayForecastWindSpeed2;
        set {
            _sevenDayForecastWindSpeed2 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed2");
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

    public string? SevenDayForecastWeatherIcon3a {
        get => _sevenDayForecastWeatherIcon3a;
        set {
            _sevenDayForecastWeatherIcon3a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon3a");
        }
    }

    public string? SevenDayForecastWeatherIcon3b {
        get => _sevenDayForecastWeatherIcon3b;
        set {
            _sevenDayForecastWeatherIcon3b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon3b");
        }
    }

    public string? SevenDayForecastWindSpeed3 {
        get => _sevenDayForecastWindSpeed3;
        set {
            _sevenDayForecastWindSpeed3 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed3");
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

    public string? SevenDayForecastWeatherIcon4a {
        get => _sevenDayForecastWeatherIcon4a;
        set {
            _sevenDayForecastWeatherIcon4a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon4a");
        }
    }

    public string? SevenDayForecastWeatherIcon4b {
        get => _sevenDayForecastWeatherIcon4b;
        set {
            _sevenDayForecastWeatherIcon4b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon4b");
        }
    }

    public string? SevenDayForecastWindSpeed4 {
        get => _sevenDayForecastWindSpeed4;
        set {
            _sevenDayForecastWindSpeed4 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed4");
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

    public string? SevenDayForecastWeatherIcon5a {
        get => _sevenDayForecastWeatherIcon5a;
        set {
            _sevenDayForecastWeatherIcon5a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon5a");
        }
    }

    public string? SevenDayForecastWeatherIcon5b {
        get => _sevenDayForecastWeatherIcon5b;
        set {
            _sevenDayForecastWeatherIcon5b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon5b");
        }
    }

    public string? SevenDayForecastWindSpeed5 {
        get => _sevenDayForecastWindSpeed5;
        set {
            _sevenDayForecastWindSpeed5 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed5");
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

    public string? SevenDayForecastWeatherIcon6a {
        get => _sevenDayForecastWeatherIcon6a;
        set {
            _sevenDayForecastWeatherIcon6a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon6a");
        }
    }

    public string? SevenDayForecastWeatherIcon6b {
        get => _sevenDayForecastWeatherIcon6b;
        set {
            _sevenDayForecastWeatherIcon6b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon6b");
        }
    }

    public string? SevenDayForecastWindSpeed6 {
        get => _sevenDayForecastWindSpeed6;
        set {
            _sevenDayForecastWindSpeed6 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed6");
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

    public string? SevenDayForecastWeatherIcon7a {
        get => _sevenDayForecastWeatherIcon7a;
        set {
            _sevenDayForecastWeatherIcon7a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon7a");
        }
    }

    public string? SevenDayForecastWeatherIcon7b {
        get => _sevenDayForecastWeatherIcon7b;
        set {
            _sevenDayForecastWeatherIcon7b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon7b");
        }
    }

    public string? SevenDayForecastWindSpeed7 {
        get => _sevenDayForecastWindSpeed7;
        set {
            _sevenDayForecastWindSpeed7 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed7");
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

    public string? SevenDayForecastWeatherIcon8a {
        get => _sevenDayForecastWeatherIcon8a;
        set {
            _sevenDayForecastWeatherIcon8a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon8a");
        }
    }

    public string? SevenDayForecastWeatherIcon8b {
        get => _sevenDayForecastWeatherIcon8b;
        set {
            _sevenDayForecastWeatherIcon8b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon8b");
        }
    }

    public string? SevenDayForecastWindSpeed8 {
        get => _sevenDayForecastWindSpeed8;
        set {
            _sevenDayForecastWindSpeed8 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed8");
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

    public string? SevenDayForecastWeatherIcon9a {
        get => _sevenDayForecastWeatherIcon9a;
        set {
            _sevenDayForecastWeatherIcon9a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon9a");
        }
    }

    public string? SevenDayForecastWeatherIcon9b {
        get => _sevenDayForecastWeatherIcon9b;
        set {
            _sevenDayForecastWeatherIcon9b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon9b");
        }
    }

    public string? SevenDayForecastWindSpeed9 {
        get => _sevenDayForecastWindSpeed9;
        set {
            _sevenDayForecastWindSpeed9 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed9");
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

    public string? SevenDayForecastWeatherIcon10a {
        get => _sevenDayForecastWeatherIcon10a;
        set {
            _sevenDayForecastWeatherIcon10a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon10a");
        }
    }

    public string? SevenDayForecastWeatherIcon10b {
        get => _sevenDayForecastWeatherIcon10b;
        set {
            _sevenDayForecastWeatherIcon10b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon10b");
        }
    }

    public string? SevenDayForecastWindSpeed10 {
        get => _sevenDayForecastWindSpeed10;
        set {
            _sevenDayForecastWindSpeed10 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed10");
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

    public string? SevenDayForecastWeatherIcon11a {
        get => _sevenDayForecastWeatherIcon11a;
        set {
            _sevenDayForecastWeatherIcon11a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon11a");
        }
    }

    public string? SevenDayForecastWeatherIcon11b {
        get => _sevenDayForecastWeatherIcon11b;
        set {
            _sevenDayForecastWeatherIcon11b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon11b");
        }
    }

    public string? SevenDayForecastWindSpeed11 {
        get => _sevenDayForecastWindSpeed11;
        set {
            _sevenDayForecastWindSpeed11 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed11");
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

    public string? SevenDayForecastWeatherIcon12a {
        get => _sevenDayForecastWeatherIcon12a;
        set {
            _sevenDayForecastWeatherIcon12a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon12a");
        }
    }

    public string? SevenDayForecastWeatherIcon12b {
        get => _sevenDayForecastWeatherIcon12b;
        set {
            _sevenDayForecastWeatherIcon12b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon12b");
        }
    }

    public string? SevenDayForecastWindSpeed12 {
        get => _sevenDayForecastWindSpeed12;
        set {
            _sevenDayForecastWindSpeed12 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed12");
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

    public string? SevenDayForecastWeatherIcon13a {
        get => _sevenDayForecastWeatherIcon13a;
        set {
            _sevenDayForecastWeatherIcon13a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon13a");
        }
    }

    public string? SevenDayForecastWeatherIcon13b {
        get => _sevenDayForecastWeatherIcon13b;
        set {
            _sevenDayForecastWeatherIcon13b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon13b");
        }
    }

    public string? SevenDayForecastWindSpeed13 {
        get => _sevenDayForecastWindSpeed13;
        set {
            _sevenDayForecastWindSpeed13 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed13");
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

    public string? SevenDayForecastWeatherIcon14a {
        get => _sevenDayForecastWeatherIcon14a;
        set {
            _sevenDayForecastWeatherIcon14a = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon14a");
        }
    }

    public string? SevenDayForecastWeatherIcon14b {
        get => _sevenDayForecastWeatherIcon14b;
        set {
            _sevenDayForecastWeatherIcon14b = value;
            RaisePropertyChangedEvent("SevenDayForecastWeatherIcon14b");
        }
    }

    public string? SevenDayForecastWindSpeed14 {
        get => _sevenDayForecastWindSpeed14;
        set {
            _sevenDayForecastWindSpeed14 = value;
            RaisePropertyChangedEvent("SevenDayForecastWindSpeed14");
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

    public ObservableCollection<WeatherHourlyBlock> ForecastHourlyList {
        get => _forecastHourlyList;
        set {
            _forecastHourlyList = value;
            RaisePropertyChangedEvent("ForecastHourlyList");
        }
    }

    public int TempMin {
        get => _tempMin;
        set {
            _tempMin = value;
            RaisePropertyChangedEvent("TempMin");
        }
    }

    public int TempMax {
        get => _tempMax;
        set {
            _tempMax = value;
            RaisePropertyChangedEvent("TempMax");
        }
    }

    public string ForecastHourlyVisibility {
        get => _forecastHourlyVisibility;
        set {
            _forecastHourlyVisibility = value;
            RaisePropertyChangedEvent("ForecastHourlyVisibility");
        }
    }

    public string EasterEggVisibility {
        get => _easterEggVisibility;
        set {
            _easterEggVisibility = value;
            RaisePropertyChangedEvent("EasterEggVisibility");
        }
    }

    #endregion
}