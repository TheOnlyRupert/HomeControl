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

    private string _temperatureInside, _heatingCoolingText, _temperatureAdjusted, _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor, _humidityOutside, _temperatureInsideColor,
        _temperatureAdjustedColor, _fanStatusColor, _heatingCoolingStatusColor, _humidityInsideInside, _temperatureOutside, _temperatureOutsideColor, _currentWindSpeedText, _currentWeatherDescription,
        _currentDateText, _currentTimeText, _currentTimeSecondsText, _currentWeatherCloudIcon, _runTime, _currentRainChanceText;

    public HvacVM() {
        CurrentDateText = DateTime.Now.DayOfWeek + "\n" + DateTime.Now.ToString("MMMM dd yyyy");
        CurrentTimeText = DateTime.Now.ToString("HH:mm");
        ReferenceValues.TemperatureSet = 22;

        UpdateWeather();
        UpdateHvac();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private async void UpdateWeather() {
        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            Uri weatherForecastHourlyUrl =
                new($"https://api.weather.gov/gridpoints/{ReferenceValues.JsonSettingsMaster.GridId}/{ReferenceValues.JsonSettingsMaster.GridX},{ReferenceValues.JsonSettingsMaster.GridY}/forecast/hourly");

            using WebClient client = new();
            client.Headers.Add("User-Agent", "Home Control, " + ReferenceValues.JsonSettingsMaster.UserAgent);
            string weatherForecastHourly = await client.DownloadStringTaskAsync(weatherForecastHourlyUrl);
            ReferenceValues.ForecastHourly = JsonSerializer.Deserialize<JsonWeather>(weatherForecastHourly, options);

            for (int i = 0; i < ReferenceValues.ForecastHourly.properties.periods.Count; i++) {
                if (ReferenceValues.ForecastHourly.properties.periods[i].startTime < DateTime.Now) {
                    ReferenceValues.ForecastHourly.properties.periods.RemoveAt(i);
                }
            }

            CurrentWindDirectionRotation = WeatherHelpers.GetWindRotation(ReferenceValues.ForecastHourly.properties.periods[0].windDirection);
            CurrentWindSpeedText = ReferenceValues.ForecastHourly.properties.periods[0].windSpeed;
            CurrentRainChanceText = ReferenceValues.ForecastHourly.properties.periods[0].probabilityOfPrecipitation.value + "%";
            CurrentWeatherDescription = ReferenceValues.ForecastHourly.properties.periods[0].shortForecast;
            CurrentWeatherCloudIcon = WeatherHelpers.GetWeatherIcon(ReferenceValues.ForecastHourly.properties.periods[0].shortForecast,
                ReferenceValues.ForecastHourly.properties.periods[0].isDaytime,
                ReferenceValues.ForecastHourly.properties.periods[0].temperature, ReferenceValues.ForecastHourly.properties.periods[0].windSpeed);

            if (ReferenceValues.JsonSettingsMaster.UseMetricUnits) {
                double c = (ReferenceValues.ForecastHourly.properties.periods[0].temperature - 32) * 0.556;
                TemperatureOutside = (int)c + "°";
            } else {
                TemperatureOutside = ReferenceValues.ForecastHourly.properties.periods[0].temperature + "°";
            }

            TemperatureOutsideColor = "Yellow";

            HumidityOutside = ReferenceValues.ForecastHourly.properties.periods[0].relativeHumidity.value + "%";
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

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "Refresh":
            CurrentTimeSecondsText = DateTime.Now.ToString("ss");
            TimeSpan time = TimeSpan.FromSeconds(ReferenceValues.HvacStateTime);
            RunTime = ReferenceValues.HvacState + ": " + time.ToString(@"hh\:mm\:ss");

            break;
        case "HvacUpdated":
            UpdateHvac();

            break;
        case "MinChanged":
            CurrentDateText = DateTime.Now.DayOfWeek + "\n" + DateTime.Now.ToString("MMMM dd yyyy");
            CurrentTimeText = DateTime.Now.ToString("HH:mm");
            UpdateWeather();

            break;
        case "HourChanged":
            //Check Schedule Logic
            if (ReferenceValues.UseSchedule) {
                foreach (HvacEvent hvacEvent in ReferenceValues.JsonHvacMaster.HvacEvents) {
                    if (hvacEvent.EventTime.Hour == DateTime.Now.Hour) {
                        ReferenceValues.TemperatureSet = hvacEvent.EventTemp;
                    }
                }

                UpdateWeather();
            }

            break;
        }
    }

    private void UpdateHvac() {
        if (!ReferenceValues.IsHvacComEstablished) {
            TemperatureAdjusted = "";
            TemperatureAdjustedColor = "White";
            TemperatureInside = "";
            TemperatureInsideColor = "White";
            ProgramStatus = "Offline";
            ProgramStatusColor = "Red";
            FanStatus = "Off";
            FanStatusColor = "Red";
            HeatingCoolingText = "Adjust To";
            HeatingCoolingStatus = "Offline";
            HeatingCoolingStatusColor = "Red";
            HumidityInside = "Offline";

            return;
        }

        if (ReferenceValues.JsonSettingsMaster.UseMetricUnits) {
            TemperatureAdjusted = ReferenceValues.TemperatureSet + "°";
        } else {
            double f = ReferenceValues.TemperatureSet * 1.8 + 32;
            TemperatureAdjusted = (int)f + "°";
        }

        TemperatureAdjustedColor = "White";

        if (Math.Abs(ReferenceValues.TemperatureInside - -99) < 1) {
            TemperatureInside = "??";
            TemperatureInsideColor = "Red";
        } else {
            if (ReferenceValues.JsonSettingsMaster.UseMetricUnits) {
                TemperatureInside = ReferenceValues.TemperatureInside + "°";
            } else {
                double f = ReferenceValues.TemperatureInside * 1.8 + 32;
                TemperatureInside = (int)f + "°";
            }

            TemperatureInsideColor = "White";
        }

        if (ReferenceValues.HumidityInside == -99) {
            HumidityInside = "??";
        } else {
            HumidityInside = ReferenceValues.HumidityInside + "%";
        }

        if (ReferenceValues.IsFanAuto) {
            FanStatus = "Auto";
            FanStatusColor = "White";
        } else {
            FanStatus = "On";
            FanStatusColor = "Green";
        }

        if (ReferenceValues.IsHeatingMode) {
            switch (ReferenceValues.HvacState) {
            case HvacStates.Off:
                ProgramStatus = "Off";
                ProgramStatusColor = "White";
                HeatingCoolingStatus = "Heating";
                HeatingCoolingStatusColor = "Red";
                HeatingCoolingText = "Heating To";

                break;
            case HvacStates.Running:
                ProgramStatus = "Running";
                ProgramStatusColor = "Green";
                HeatingCoolingStatus = "Heating";
                HeatingCoolingStatusColor = "Red";
                HeatingCoolingText = "Heating To";

                break;
            case HvacStates.Standby:
                ProgramStatus = "Standby";
                ProgramStatusColor = "Yellow";
                HeatingCoolingStatus = "Heating";
                HeatingCoolingStatusColor = "Red";
                HeatingCoolingText = "Heating To";

                break;
            case HvacStates.Purging:
                ProgramStatus = "Purging";
                ProgramStatusColor = "Yellow";
                HeatingCoolingStatus = "Heating";
                HeatingCoolingStatusColor = "Red";
                HeatingCoolingText = "Heating To";

                break;
            }
        } else {
            switch (ReferenceValues.HvacState) {
            case HvacStates.Off:
                ProgramStatus = "Off";
                ProgramStatusColor = "White";
                HeatingCoolingStatus = "Cooling";
                HeatingCoolingStatusColor = "CornflowerBlue";
                HeatingCoolingText = "Cooling To";

                break;
            case HvacStates.Running:
                ProgramStatus = "Running";
                ProgramStatusColor = "Green";
                HeatingCoolingStatus = "Cooling";
                HeatingCoolingStatusColor = "CornflowerBlue";
                HeatingCoolingText = "Cooling To";

                break;
            case HvacStates.Standby:
                ProgramStatus = "Standby";
                ProgramStatusColor = "Yellow";
                HeatingCoolingStatus = "Cooling";
                HeatingCoolingStatusColor = "CornflowerBlue";
                HeatingCoolingText = "Cooling To";

                break;
            case HvacStates.Purging:
                ProgramStatus = "Purging";
                ProgramStatusColor = "Yellow";
                HeatingCoolingStatus = "Cooling";
                HeatingCoolingStatusColor = "CornflowerBlue";
                HeatingCoolingText = "Cooling To";

                break;
            }
        }
    }

    /* 0 -> Force Refresh,
     * 1 -> Fan On, 2 -> Fan Auto,
     * 3 -> Program On, 4 -> Program Off,
     * 5 -> Heating Mode, 6 -> Cooling Mode */
    private void ButtonLogic(object param) {
        switch (param) {
        case "hvac":
            if (ReferenceValues.LockUi) {
                SoundDispatcher.PlaySound("locked");
            } else if (ReferenceValues.IsHvacComEstablished || ReferenceValues.JsonSettingsMaster.DebugMode) {
                /* Save current state before editing. Only update if state changes */
                bool isProgramRunningOld = ReferenceValues.IsProgramRunning;
                bool isHeatingModeOld = ReferenceValues.IsHeatingMode;
                bool isFanAutoOld = ReferenceValues.IsFanAuto;
                double tempOld = ReferenceValues.TemperatureSet;

                EditHvac editHvac = new();
                editHvac.ShowDialog();
                editHvac.Close();

                if (isFanAutoOld != ReferenceValues.IsFanAuto) {
                    if (ReferenceValues.IsFanAuto) {
                        /* 2 -> Fan On */
                        try {
                            ReferenceValues.SerialPort.Write("2");
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "HvacVM",
                                Description = "Unable to change HVAC state\n" + e
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    } else {
                        /* 1 -> Fan Auto */
                        try {
                            ReferenceValues.SerialPort.Write("1");
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "HvacVM",
                                Description = "Unable to change HVAC state\n" + e
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    }
                }

                if (isProgramRunningOld != ReferenceValues.IsProgramRunning) {
                    if (ReferenceValues.IsProgramRunning) {
                        /* 3 -> Program On */
                        try {
                            ReferenceValues.SerialPort.Write("3");
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "HvacVM",
                                Description = "Unable to change HVAC state\n" + e
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }

                        ReferenceValues.HvacState = HvacStates.Standby;
                    } else {
                        /* 4 -> Program Off */
                        try {
                            ReferenceValues.SerialPort.Write("4");
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "HvacVM",
                                Description = "Unable to change HVAC state\n" + e
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }

                        ReferenceValues.HvacState = HvacStates.Off;
                    }
                }

                if (isHeatingModeOld != ReferenceValues.IsHeatingMode) {
                    if (ReferenceValues.IsHeatingMode) {
                        /* 3 -> Heating Mode */
                        try {
                            ReferenceValues.SerialPort.Write("5");
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "HvacVM",
                                Description = "Unable to change HVAC state\n" + e
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    } else {
                        /* 4 -> Cooling Mode */
                        try {
                            ReferenceValues.SerialPort.Write("6");
                        } catch (Exception e) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "WARN",
                                Module = "HvacVM",
                                Description = "Unable to change HVAC state\n" + e
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                        }
                    }
                }

                if (Math.Abs(tempOld - ReferenceValues.TemperatureSet) > 0.2) {
                    try {
                        char c = (char)(ReferenceValues.TemperatureSet + 50);
                        ReferenceValues.SerialPort.Write(c.ToString());
                    } catch (Exception e) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "WARN",
                            Module = "HvacVM",
                            Description = "Unable to change HVAC state\n" + e
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
                    }
                }

                UpdateHvac();
            } else {
                SoundDispatcher.PlaySound("offline");
            }

            break;
        }
    }

    #region Fields

    public string TemperatureInside {
        get => _temperatureInside;
        set {
            _temperatureInside = value;
            RaisePropertyChangedEvent("TemperatureInside");
        }
    }

    public string TemperatureInsideColor {
        get => _temperatureInsideColor;
        set {
            _temperatureInsideColor = value;
            RaisePropertyChangedEvent("TemperatureInsideColor");
        }
    }

    public string TemperatureOutside {
        get => _temperatureOutside;
        set {
            _temperatureOutside = value;
            RaisePropertyChangedEvent("TemperatureOutside");
        }
    }

    public string TemperatureOutsideColor {
        get => _temperatureOutsideColor;
        set {
            _temperatureOutsideColor = value;
            RaisePropertyChangedEvent("TemperatureOutsideColor");
        }
    }

    public string HeatingCoolingText {
        get => _heatingCoolingText;
        set {
            _heatingCoolingText = value;
            RaisePropertyChangedEvent("HeatingCoolingText");
        }
    }

    public string TemperatureAdjusted {
        get => _temperatureAdjusted;
        set {
            _temperatureAdjusted = value;
            RaisePropertyChangedEvent("TemperatureAdjusted");
        }
    }

    public string TemperatureAdjustedColor {
        get => _temperatureAdjustedColor;
        set {
            _temperatureAdjustedColor = value;
            RaisePropertyChangedEvent("TemperatureAdjustedColor");
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

    public string HumidityInside {
        get => _humidityInsideInside;
        set {
            _humidityInsideInside = value;
            RaisePropertyChangedEvent("HumidityInside");
        }
    }

    public string HumidityOutside {
        get => _humidityOutside;
        set {
            _humidityOutside = value;
            RaisePropertyChangedEvent("HumidityOutside");
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