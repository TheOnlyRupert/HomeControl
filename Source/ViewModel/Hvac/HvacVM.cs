using System.Net;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Hvac;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class HvacVM : BaseViewModel {
    private readonly CrossViewMessenger _simpleMessenger;
    private int _currentWindDirectionRotation;

    private string _temperatureInside, _heatingCoolingText, _temperatureAdjusted, _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor, _fanStatusColor,
        _heatingCoolingStatusColor, _humidityInside, _temperatureOutside, _temperatureOutsideColor, _currentWindSpeedText, _currentWeatherDescription, _currentDateText, _currentTimeText,
        _currentTimeSecondsText, _currentWeatherCloudIcon, _runTime, _currentRainChanceText, _humidityOutside, _temperatureAdjustedColor, _temperatureInsideColor;

    public HvacVM() {
        CurrentDateText = DateTime.Now.DayOfWeek + "\n" + DateTime.Now.ToString("MMMM dd yyyy");
        CurrentTimeText = DateTime.Now.ToString("HH:mm");
        ReferenceValues.TemperatureSet = 22;

        UpdateWeather();
        UpdateHvac();

        _simpleMessenger = CrossViewMessenger.Instance;
        CrossViewMessenger.Instance.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    public int CurrentWindDirectionRotation {
        get => _currentWindDirectionRotation;
        set => SetProperty(ref _currentWindDirectionRotation, value);
    }

    public string TemperatureInside {
        get => _temperatureInside;
        set => SetProperty(ref _temperatureInside, value);
    }

    public string HeatingCoolingText {
        get => _heatingCoolingText;
        set => SetProperty(ref _heatingCoolingText, value);
    }

    public string TemperatureAdjusted {
        get => _temperatureAdjusted;
        set => SetProperty(ref _temperatureAdjusted, value);
    }

    public string ProgramStatus {
        get => _programStatus;
        set => SetProperty(ref _programStatus, value);
    }

    public string FanStatus {
        get => _fanStatus;
        set => SetProperty(ref _fanStatus, value);
    }

    public string HeatingCoolingStatus {
        get => _heatingCoolingStatus;
        set => SetProperty(ref _heatingCoolingStatus, value);
    }

    public string ProgramStatusColor {
        get => _programStatusColor;
        set => SetProperty(ref _programStatusColor, value);
    }

    public string FanStatusColor {
        get => _fanStatusColor;
        set => SetProperty(ref _fanStatusColor, value);
    }

    public string HeatingCoolingStatusColor {
        get => _heatingCoolingStatusColor;
        set => SetProperty(ref _heatingCoolingStatusColor, value);
    }

    public string HumidityInside {
        get => _humidityInside;
        set => SetProperty(ref _humidityInside, value);
    }

    public string TemperatureOutside {
        get => _temperatureOutside;
        set => SetProperty(ref _temperatureOutside, value);
    }

    public string TemperatureOutsideColor {
        get => _temperatureOutsideColor;
        set => SetProperty(ref _temperatureOutsideColor, value);
    }

    public string CurrentWindSpeedText {
        get => _currentWindSpeedText;
        set => SetProperty(ref _currentWindSpeedText, value);
    }

    public string CurrentWeatherDescription {
        get => _currentWeatherDescription;
        set => SetProperty(ref _currentWeatherDescription, value);
    }

    public string CurrentDateText {
        get => _currentDateText;
        set => SetProperty(ref _currentDateText, value);
    }

    public string CurrentTimeText {
        get => _currentTimeText;
        set => SetProperty(ref _currentTimeText, value);
    }

    public string CurrentTimeSecondsText {
        get => _currentTimeSecondsText;
        set => SetProperty(ref _currentTimeSecondsText, value);
    }

    public string CurrentWeatherCloudIcon {
        get => _currentWeatherCloudIcon;
        set => SetProperty(ref _currentWeatherCloudIcon, value);
    }

    public string RunTime {
        get => _runTime;
        set => SetProperty(ref _runTime, value);
    }

    public string CurrentRainChanceText {
        get => _currentRainChanceText;
        set => SetProperty(ref _currentRainChanceText, value);
    }

    public string HumidityOutside {
        get => _humidityOutside;
        set => SetProperty(ref _humidityOutside, value);
    }

    public string TemperatureAdjustedColor {
        get => _temperatureAdjustedColor;
        set => SetProperty(ref _temperatureAdjustedColor, value);
    }

    public string TemperatureInsideColor {
        get => _temperatureInsideColor;
        set => SetProperty(ref _temperatureInsideColor, value);
    }

    private async void UpdateWeather() {
        try {
            Uri weatherUrl = new(
                $"https://api.weather.gov/gridpoints/{ReferenceValues.JsonSettingsMaster.GridId}/{ReferenceValues.JsonSettingsMaster.GridX},{ReferenceValues.JsonSettingsMaster.GridY}/forecast/hourly");
            using WebClient client = new();
            client.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonSettingsMaster.UserAgent);
            string weatherData = await client.DownloadStringTaskAsync(weatherUrl);

            JsonWeather forecast = JsonSerializer.Deserialize<JsonWeather>(weatherData, new JsonSerializerOptions {
                IncludeFields = true
            });

            ReferenceValues.ForecastHourly = forecast;
            ReferenceValues.ForecastHourly.properties.periods.RemoveAll(p => p.startTime < DateTime.Now);

            JsonWeather.Periods firstPeriod = ReferenceValues.ForecastHourly.properties.periods[0];
            SetWeatherProperties(firstPeriod);
        } catch (Exception e) {
            LogError(e.ToString());
        }
    }

    private void SetWeatherProperties(JsonWeather.Periods firstPeriod) {
        CurrentWindDirectionRotation = WeatherHelpers.GetWindRotation(firstPeriod.windDirection);
        CurrentWindSpeedText = firstPeriod.windSpeed;
        CurrentRainChanceText = $"{firstPeriod.probabilityOfPrecipitation.value}%";
        CurrentWeatherDescription = firstPeriod.shortForecast;
        CurrentWeatherCloudIcon = WeatherHelpers.GetWeatherIcon(firstPeriod.shortForecast, firstPeriod.isDaytime, firstPeriod.temperature, firstPeriod.windSpeed);

        TemperatureOutside = ReferenceValues.JsonSettingsMaster.UseMetricUnits
            ? $"{(int)((firstPeriod.temperature - 32) * 0.556)}°"
            : $"{firstPeriod.temperature}°";

        TemperatureOutsideColor = "Yellow";
        HumidityOutside = $"{firstPeriod.relativeHumidity.value}%";
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "Refresh":
            UpdateTimeAndRuntime();
            break;
        case "HvacUpdated":
            UpdateHvac();
            break;
        case "MinChanged":
            UpdateDateTime();
            break;
        case "HourChanged":
            UpdateSchedule();
            break;
        }
    }

    private void UpdateTimeAndRuntime() {
        CurrentTimeSecondsText = DateTime.Now.ToString("ss");
        RunTime = $"{ReferenceValues.HvacState}: {TimeSpan.FromSeconds(ReferenceValues.HvacStateTime):hh\\:mm\\:ss}";
    }

    private void UpdateDateTime() {
        CurrentDateText = DateTime.Now.ToString("dddd\nMMMM dd yyyy");
        CurrentTimeText = DateTime.Now.ToString("HH:mm");
        UpdateWeather();
    }

    private void UpdateSchedule() {
        if (ReferenceValues.UseSchedule) {
            foreach (HvacEvent hvacEvent in ReferenceValues.JsonHvacMaster.HvacEvents) {
                if (hvacEvent.EventTime.Hour == DateTime.Now.Hour) {
                    ReferenceValues.TemperatureSet = hvacEvent.EventTemp;
                }
            }
        }

        UpdateWeather();
    }

    private void UpdateHvac() {
        if (!ReferenceValues.IsHvacComEstablished) {
            ResetHvacState();
            return;
        }

        TemperatureAdjusted = FormatTemperature(ReferenceValues.TemperatureSet);
        TemperatureInside = FormatTemperature(ReferenceValues.TemperatureInside);
        HumidityInside = ReferenceValues.HumidityInside > -99 ? $"{ReferenceValues.HumidityInside}%" : "??";

        FanStatus = ReferenceValues.IsFanAuto ? "Auto" : "On";
        FanStatusColor = ReferenceValues.IsFanAuto ? "White" : "Green";

        UpdateHeatingCoolingStatus();
    }

    private static string FormatTemperature(double temperature) {
        return ReferenceValues.JsonSettingsMaster.UseMetricUnits
            ? $"{temperature}°"
            : $"{(int)(temperature * 1.8 + 32)}°";
    }

    private void ResetHvacState() {
        TemperatureAdjusted = "Offline";
        TemperatureAdjustedColor = "White";
        TemperatureInside = "Offline";
        TemperatureInsideColor = "White";
        ProgramStatus = "Offline";
        ProgramStatusColor = "Red";
        FanStatus = "Off";
        FanStatusColor = "Red";
        HeatingCoolingText = "Adjust To";
        HeatingCoolingStatus = "Offline";
        HeatingCoolingStatusColor = "Red";
        HumidityInside = "Offline";
    }

    private void UpdateHeatingCoolingStatus() {
        string heatingCoolingColor = ReferenceValues.IsHeatingMode ? "Red" : "CornflowerBlue";
        string statusText = ReferenceValues.IsHeatingMode ? "Heating" : "Cooling";

        switch (ReferenceValues.HvacState) {
        case 0: // Off
            ProgramStatus = "Off";
            ProgramStatusColor = "White";
            HeatingCoolingStatus = statusText;
            HeatingCoolingStatusColor = heatingCoolingColor;
            HeatingCoolingText = $"{statusText} To";
            break;
        case 1: // Running
            ProgramStatus = "Running";
            ProgramStatusColor = "Green";
            HeatingCoolingStatus = statusText;
            HeatingCoolingStatusColor = heatingCoolingColor;
            HeatingCoolingText = $"{statusText} To";
            break;
        case 2: // Standby
            ProgramStatus = "Standby";
            ProgramStatusColor = "Yellow";
            HeatingCoolingStatus = statusText;
            HeatingCoolingStatusColor = heatingCoolingColor;
            HeatingCoolingText = $"{statusText} To";
            break;
        case 3: // Purging
            ProgramStatus = "Purging";
            ProgramStatusColor = "Yellow";
            HeatingCoolingStatus = statusText;
            HeatingCoolingStatusColor = heatingCoolingColor;
            HeatingCoolingText = $"{statusText} To";
            break;
        }
    }

    private static void LogError(string errorDetails) {
        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "WARN",
            Module = "WeatherVM",
            Description = errorDetails
        });
        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "hvac":
            EditHvac editHvac = new();
            editHvac.ShowDialog();
            editHvac.Close();
            ToggleHvacState();
            break;
        case "heating":
            SetHeatingMode();
            break;
        case "cooling":
            SetCoolingMode();
            break;
        }
    }

    private void ToggleHvacState() {
        ReferenceValues.HvacState = ReferenceValues.HvacState == 0 ? 1 : 0;
        _simpleMessenger.PushMessage("HvacUpdated", null);
    }

    private void SetHeatingMode() {
        ReferenceValues.IsHeatingMode = true;
        ReferenceValues.HvacState = 1;
        _simpleMessenger.PushMessage("HvacUpdated", null);
    }

    private void SetCoolingMode() {
        ReferenceValues.IsHeatingMode = false;
        ReferenceValues.HvacState = 1;
        _simpleMessenger.PushMessage("HvacUpdated", null);
    }
}