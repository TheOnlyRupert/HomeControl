using System;
using System.Net;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Hvac;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class HvacVM : BaseViewModel {
    private int _currentWindDirectionRotation;

    private string _tempInside, _heatingCoolingText, _tempAdjusted, _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor, _extHumidity, _tempInsideColor, _tempAdjustedColor,
        _fanStatusColor, _heatingCoolingStatusColor, _intHumidity, _tempOutside, _tempOutsideColor, _currentWindSpeedText, _currentWeatherDescription, _currentDateText, _currentTimeText,
        _currentTimeSecondsText, _currentWeatherCloudIcon, _runTime, _currentRainChanceText;

    private JsonWeather forecastHourly;

    public HvacVM() {
        CurrentDateText = DateTime.Now.DayOfWeek + "\n" + DateTime.Now.ToString("MMMM dd yyyy");
        CurrentTimeText = DateTime.Now.ToString("HH:mm");
        ReferenceValues.TemperatureSet = 22;

        UpdateWeather();
        UpdateHvac();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private async void UpdateWeather() {
        if (ReferenceValues.EnableWeather) {
            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            try {
                Uri weatherForecastHourlyURL =
                    new(
                        $"https://api.weather.gov/gridpoints/{ReferenceValues.JsonSettingsMaster.GridId}/{ReferenceValues.JsonSettingsMaster.GridX},{ReferenceValues.JsonSettingsMaster.GridY}/forecast/hourly");

                using WebClient client = new();
                client.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonSettingsMaster.UserAgent);
                string weatherForecastHourly = await client.DownloadStringTaskAsync(weatherForecastHourlyURL);
                forecastHourly = JsonSerializer.Deserialize<JsonWeather>(weatherForecastHourly, options);

                for (int i = 0; i < forecastHourly.properties.periods.Count; i++) {
                    if (forecastHourly.properties.periods[i].startTime < DateTime.Now) {
                        forecastHourly.properties.periods.RemoveAt(i);
                    }
                }

                CurrentWindDirectionRotation = WeatherHelpers.GetWindRotation(forecastHourly.properties.periods[0].windDirection);
                CurrentWindSpeedText = forecastHourly.properties.periods[0].windSpeed;
                CurrentRainChanceText = forecastHourly.properties.periods[0].probabilityOfPrecipitation.value + "%";
                CurrentWeatherDescription = forecastHourly.properties.periods[0].shortForecast;
                CurrentWeatherCloudIcon = WeatherHelpers.GetWeatherIcon(forecastHourly.properties.periods[0].shortForecast, forecastHourly.properties.periods[0].isDaytime,
                    forecastHourly.properties.periods[0].temperature, forecastHourly.properties.periods[0].windSpeed, "");

                if (ReferenceValues.JsonSettingsMaster.useMetricUnits) {
                    double c = (forecastHourly.properties.periods[0].temperature - 32) * 0.556;
                    TempOutside = (int)c + "°";
                } else {
                    TempOutside = forecastHourly.properties.periods[0].temperature + "°";
                }

                TempOutsideColor = "Yellow";

                ExtHumidity = forecastHourly.properties.periods[0].relativeHumidity.value + "%";
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "WeatherVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }
        }
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "Refresh":
            CurrentTimeSecondsText = DateTime.Now.ToString("ss");
            TimeSpan time = TimeSpan.FromSeconds(ReferenceValues.HvacStateRunTime);
            RunTime = ReferenceValues.HvacMode + ": " + time.ToString(@"hh\:mm\:ss");

            break;
        case "HvacUpdated":
            UpdateHvac();
            break;
        case "MinChanged":
            CurrentDateText = DateTime.Now.DayOfWeek + "\n" + DateTime.Now.ToString("MMMM dd yyyy");
            CurrentTimeText = DateTime.Now.ToString("HH:mm");
            UpdateWeather();
            break;
        }
    }

    private void UpdateHvac() {
        if (!ReferenceValues.IsHvacComEstablished) {
            TempAdjusted = "N/A";
            TempAdjustedColor = "White";
            TempInside = "N/A";
            TempInsideColor = "White";
            ProgramStatus = "Offline";
            ProgramStatusColor = "Red";
            FanStatus = "Off";
            FanStatusColor = "Red";
            HeatingCoolingText = "Adjust To";
            HeatingCoolingStatus = "Offline";
            HeatingCoolingStatusColor = "Red";
            IntHumidity = "Offline";

            return;
        }

        if (ReferenceValues.JsonSettingsMaster.useMetricUnits) {
            TempAdjusted = ReferenceValues.TemperatureSet + "°";
        } else {
            double f = ReferenceValues.TemperatureSet * 1.8 + 32;
            TempAdjusted = (int)f + "°";
        }

        TempAdjustedColor = "White";

        if (ReferenceValues.TemperatureInside == -99) {
            TempInside = "??";
            TempInsideColor = "Red";
        } else {
            if (ReferenceValues.JsonSettingsMaster.useMetricUnits) {
                TempInside = ReferenceValues.TemperatureInside + "°";
            } else {
                double f = ReferenceValues.TemperatureInside * 1.8 + 32;
                TempInside = (int)f + "°";
            }

            TempInsideColor = "White";
        }

        if (ReferenceValues.InteriorHumidity == -99) {
            IntHumidity = "??";
        } else {
            IntHumidity = ReferenceValues.InteriorHumidity + "%";
        }

        if (ReferenceValues.IsFanAuto) {
            FanStatus = "Auto";
            FanStatusColor = "White";
        } else {
            FanStatus = "On";
            FanStatusColor = "Green";
        }

        if (ReferenceValues.IsHeatingMode) {
            switch (ReferenceValues.HvacMode) {
            case ReferenceValues.HvacModes.Off:
                ProgramStatus = "Heating Off";
                ProgramStatusColor = "White";
                break;
            case ReferenceValues.HvacModes.Running:
                ProgramStatus = "Heating Running";
                ProgramStatusColor = "Green";
                break;
            case ReferenceValues.HvacModes.Standby:
                ProgramStatus = "Heating Standby";
                ProgramStatusColor = "Yellow";
                break;
            case ReferenceValues.HvacModes.Purging:
                ProgramStatus = "Heating Purging";
                ProgramStatusColor = "Yellow";
                break;
            }
        } else {
            switch (ReferenceValues.HvacMode) {
            case ReferenceValues.HvacModes.Off:
                ProgramStatus = "Cooling Off";
                ProgramStatusColor = "White";
                break;
            case ReferenceValues.HvacModes.Running:
                ProgramStatus = "Cooling Running";
                ProgramStatusColor = "Green";
                break;
            case ReferenceValues.HvacModes.Standby:
                ProgramStatus = "Cooling Standby";
                ProgramStatusColor = "Yellow";
                break;
            case ReferenceValues.HvacModes.Purging:
                ProgramStatus = "Cooling Purging";
                ProgramStatusColor = "Yellow";
                break;
            }
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "hvac":
            if (ReferenceValues.LockUI) {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            } else if (ReferenceValues.IsHvacComEstablished || ReferenceValues.JsonSettingsMaster.DebugMode) {
                /* Save current state before editing. Only update if state changes */
                bool IsProgramRunningOld = ReferenceValues.IsProgramRunning;
                bool isHeatingModeOld = ReferenceValues.IsHeatingMode;
                bool isFanAutoOld = ReferenceValues.IsFanAuto;
                int tempOld = ReferenceValues.TemperatureSet;

                EditHvac editHvac = new();
                editHvac.ShowDialog();
                editHvac.Close();

                if (isFanAutoOld != ReferenceValues.IsFanAuto) {
                    if (ReferenceValues.IsFanAuto) {
                        /* 2 -> Fan On */
                        ReferenceValues.SerialPort.Write("2");
                    } else {
                        /* 1 -> Fan Auto */
                        ReferenceValues.SerialPort.Write("1");
                    }
                }

                if (IsProgramRunningOld != ReferenceValues.IsProgramRunning) {
                    if (ReferenceValues.IsProgramRunning) {
                        /* 3 -> Program On */
                        ReferenceValues.SerialPort.Write("3");
                        ReferenceValues.HvacMode = ReferenceValues.HvacModes.Standby;
                    } else {
                        /* 4 -> Program Off */
                        ReferenceValues.SerialPort.Write("4");
                        ReferenceValues.HvacMode = ReferenceValues.HvacModes.Off;
                    }
                }

                if (isHeatingModeOld != ReferenceValues.IsHeatingMode) {
                    if (ReferenceValues.IsHeatingMode) {
                        /* 3 -> Heating Mode */
                        ReferenceValues.SerialPort.Write("5");
                        ReferenceValues.HvacMode = ReferenceValues.HvacModes.Standby;
                    } else {
                        /* 4 -> Cooling Mode */
                        ReferenceValues.SerialPort.Write("6");
                        ReferenceValues.HvacMode = ReferenceValues.HvacModes.Standby;
                    }
                }

                if (tempOld != ReferenceValues.TemperatureSet) {
                    char c = (char)(ReferenceValues.TemperatureSet + 50);
                    ReferenceValues.SerialPort.Write(c.ToString());
                }

                UpdateHvac();
            } else {
                ReferenceValues.SoundToPlay = "offline";
                SoundDispatcher.PlaySound();
            }

            break;
        }
    }

    #region Fields

    public string TempInside {
        get => _tempInside;
        set {
            _tempInside = value;
            RaisePropertyChangedEvent("TempInside");
        }
    }

    public string TempInsideColor {
        get => _tempInsideColor;
        set {
            _tempInsideColor = value;
            RaisePropertyChangedEvent("TempInsideColor");
        }
    }

    public string TempOutside {
        get => _tempOutside;
        set {
            _tempOutside = value;
            RaisePropertyChangedEvent("TempOutside");
        }
    }

    public string TempOutsideColor {
        get => _tempOutsideColor;
        set {
            _tempOutsideColor = value;
            RaisePropertyChangedEvent("TempOutsideColor");
        }
    }

    public string HeatingCoolingText {
        get => _heatingCoolingText;
        set {
            _heatingCoolingText = value;
            RaisePropertyChangedEvent("HeatingCoolingText");
        }
    }

    public string TempAdjusted {
        get => _tempAdjusted;
        set {
            _tempAdjusted = value;
            RaisePropertyChangedEvent("TempAdjusted");
        }
    }

    public string TempAdjustedColor {
        get => _tempAdjustedColor;
        set {
            _tempAdjustedColor = value;
            RaisePropertyChangedEvent("TempAdjustedColor");
        }
    }

    public string ProgramStatus {
        get => _programStatus;
        set {
            _programStatus = value;
            RaisePropertyChangedEvent("ProgramStatus");
        }
    }

    public string ProgramStatusColor {
        get => _programStatusColor;
        set {
            _programStatusColor = value;
            RaisePropertyChangedEvent("ProgramStatusColor");
        }
    }

    public string FanStatus {
        get => _fanStatus;
        set {
            _fanStatus = value;
            RaisePropertyChangedEvent("FanStatus");
        }
    }

    public string FanStatusColor {
        get => _fanStatusColor;
        set {
            _fanStatusColor = value;
            RaisePropertyChangedEvent("FanStatusColor");
        }
    }

    public string HeatingCoolingStatus {
        get => _heatingCoolingStatus;
        set {
            _heatingCoolingStatus = value;
            RaisePropertyChangedEvent("HeatingCoolingStatus");
        }
    }

    public string HeatingCoolingStatusColor {
        get => _heatingCoolingStatusColor;
        set {
            _heatingCoolingStatusColor = value;
            RaisePropertyChangedEvent("HeatingCoolingStatusColor");
        }
    }

    public string IntHumidity {
        get => _intHumidity;
        set {
            _intHumidity = value;
            RaisePropertyChangedEvent("IntHumidity");
        }
    }

    public string ExtHumidity {
        get => _extHumidity;
        set {
            _extHumidity = value;
            RaisePropertyChangedEvent("ExtHumidity");
        }
    }

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

    public string RunTime {
        get => _runTime;
        set {
            _runTime = value;
            RaisePropertyChangedEvent("RunTime");
        }
    }

    public string CurrentRainChanceText {
        get => _currentRainChanceText;
        set {
            _currentRainChanceText = value;
            RaisePropertyChangedEvent("CurrentRainChanceText");
        }
    }

    #endregion
}