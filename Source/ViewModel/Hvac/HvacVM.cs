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
        _currentTimeSecondsText, _currentWeatherCloudIcon, _hvacStatusImage, _fanStatusImage;

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
                CurrentWeatherDescription = forecastHourly.properties.periods[0].shortForecast;
                CurrentWeatherCloudIcon = WeatherHelpers.GetWeatherIcon(forecastHourly.properties.periods[0].shortForecast, forecastHourly.properties.periods[0].isDaytime,
                    forecastHourly.properties.periods[0].temperature, forecastHourly.properties.periods[0].windSpeed, "null");

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

        if (ReferenceValues.InteriorTemp == -99) {
            TempInside = "??";
            TempInsideColor = "Red";
        } else {
            if (ReferenceValues.JsonSettingsMaster.useMetricUnits) {
                TempInside = ReferenceValues.InteriorTemp + "°";
            } else {
                double f = ReferenceValues.InteriorTemp * 1.8 + 32;
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

        if (ReferenceValues.IsProgramRunning) {
            if (ReferenceValues.IsStandby) {
                ProgramStatus = "Standby";
                ProgramStatusColor = "Yellow";
            } else {
                ProgramStatus = "Running";
                ProgramStatusColor = "Green";
            }
        } else {
            ProgramStatus = "Off";
            ProgramStatusColor = "White";
        }

        if (ReferenceValues.IsHeatingMode) {
            HeatingCoolingStatus = "Heating";
            HeatingCoolingStatusColor = "Red";
            HeatingCoolingText = "Heating To";
        } else {
            HeatingCoolingStatus = "Cooling";
            HeatingCoolingStatusColor = "CornflowerBlue";
            HeatingCoolingText = "Cooling To";
        }

        /* Image logic */
        if (ReferenceValues.IsProgramRunning) {
            FanStatusImage = ReferenceValues.IsFanAuto ? "" : "../../../Resources/Images/hvac/fan.gif";

            if (!ReferenceValues.IsStandby) {
                if (ReferenceValues.IsHeatingMode) {
                    HvacStatusImage = "../../../Resources/Images/hvac/heat.gif";
                    FanStatusImage = "../../../Resources/Images/hvac/fan.gif";
                } else {
                    HvacStatusImage = "../../../Resources/Images/hvac/cooling.gif";
                    FanStatusImage = "../../../Resources/Images/hvac/fan.gif";
                }
            } else {
                HvacStatusImage = "";
            }
        } else {
            FanStatusImage = "";
            HvacStatusImage = "";
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "hvac":
            if (ReferenceValues.LockUI) {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            } else if (ReferenceValues.IsHvacComEstablished) {
                /* Save current state before editing. Only update if state changes */
                bool isFanAuto = ReferenceValues.IsFanAuto;
                bool isProgramRunning = ReferenceValues.IsProgramRunning;
                bool isHeatingMode = ReferenceValues.IsHeatingMode;
                int temp = ReferenceValues.TemperatureSet;

                EditHvac editHvac = new();
                editHvac.ShowDialog();
                editHvac.Close();

                if (isFanAuto != ReferenceValues.IsFanAuto) {
                    if (ReferenceValues.IsFanAuto) {
                        /* 2 -> Fan On */
                        ReferenceValues.SerialPort.Write("2");
                    } else {
                        /* 1 -> Fan Auto */
                        ReferenceValues.SerialPort.Write("1");
                    }

                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "HVAC Changing FanModeAuto to " + ReferenceValues.IsFanAuto
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                if (isProgramRunning != ReferenceValues.IsProgramRunning) {
                    if (ReferenceValues.IsProgramRunning) {
                        /* 3 -> Program On */
                        ReferenceValues.SerialPort.Write("3");
                    } else {
                        /* 4 -> Program Off */
                        ReferenceValues.SerialPort.Write("4");
                    }

                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "HVAC Changing ProgramRunning to " + ReferenceValues.IsProgramRunning
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                if (isHeatingMode != ReferenceValues.IsHeatingMode) {
                    if (ReferenceValues.IsHeatingMode) {
                        /* 5 -> Heating Mode */
                        ReferenceValues.SerialPort.Write("5");
                    } else {
                        /* 6 -> Cooling Mode */
                        ReferenceValues.SerialPort.Write("6");
                    }

                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "HVAC Changing IsHeatingMode to " + ReferenceValues.IsHeatingMode
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                }

                if (temp != ReferenceValues.TemperatureSet) {
                    char c = (char)(ReferenceValues.TemperatureSet + 50);
                    ReferenceValues.SerialPort.Write(c.ToString());

                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "HVAC Changing TemperatureSet to: " + ReferenceValues.TemperatureSet + "°C"
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
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

    public string HvacStatusImage {
        get => _hvacStatusImage;
        set {
            _hvacStatusImage = value;
            RaisePropertyChangedEvent("HvacStatusImage");
        }
    }

    public string FanStatusImage {
        get => _fanStatusImage;
        set {
            _fanStatusImage = value;
            RaisePropertyChangedEvent("FanStatusImage");
        }
    }

    #endregion
}