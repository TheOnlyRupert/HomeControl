using System;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Hvac;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class HvacVM : BaseViewModel {
    private string _tempInside, _heatingCoolingText, _tempAdjusted, _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor,
        _tempInsideColor, _tempAdjustedColor, _fanStatusColor, _heatingCoolingStatusColor, _intHumidity;

    public HvacVM() {
        try {
            ReferenceValues.JsonHvacMaster = JsonSerializer.Deserialize<JsonHvac>(FileHelpers.LoadFileText("hvac"));
        } catch (Exception) {
            ReferenceValues.JsonHvacMaster = new JsonHvac();
        }

        if (ReferenceValues.JsonHvacMaster.TemperatureSet == 0) {
            ReferenceValues.JsonHvacMaster.TemperatureSet = 21;
        }

        TempInsideColor = "White";

        GetButtonColors();
        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "HvacUpdated") {
            GetButtonColors();
        }
    }

    private void GetButtonColors() {
        if (!ReferenceValues.IsHvacComEstablished) {
            TempAdjusted = "N/A";
            TempAdjustedColor = "White";
            TempInside = "N/A";
            TempInsideColor = "White";
            ProgramStatus = "Offline";
            ProgramStatusColor = "Red";
            FanStatus = "";
            HeatingCoolingText = "Adjust To";
            HeatingCoolingStatus = "Offline";
            HeatingCoolingStatusColor = "Red";
            IntHumidity = "Humidity: Offline";
            return;
        }

        if (ReferenceValues.JsonSettingsMaster.IsImperialMode) {
            double f = ReferenceValues.JsonHvacMaster.TemperatureSet * 1.8 + 32;
            TempAdjusted = (int)f + "°";
        } else {
            TempAdjusted = ReferenceValues.JsonHvacMaster.TemperatureSet + "°";
        }

        TempAdjustedColor = "White";

        if (ReferenceValues.InteriorTemp == -99) {
            TempInside = "??";
            TempInsideColor = "Red";
        } else {
            if (ReferenceValues.JsonSettingsMaster.IsImperialMode) {
                double f = ReferenceValues.InteriorTemp * 1.8 + 32;
                TempInside = (int)f + "°";
            } else {
                TempInside = ReferenceValues.InteriorTemp + "°";
            }

            TempInsideColor = "White";
        }

        if (ReferenceValues.InteriorHumidity == -99) {
            IntHumidity = "Humidity: ??";
        } else {
            IntHumidity = "Humidity: " + ReferenceValues.InteriorHumidity + "%";
        }

        if (ReferenceValues.JsonHvacMaster.IsProgramRunning) {
            if (ReferenceValues.JsonHvacMaster.IsStandby) {
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

        if (ReferenceValues.JsonHvacMaster.IsFanAuto) {
            FanStatus = "Auto";
            FanStatusColor = "White";
        } else {
            FanStatus = "On";
            FanStatusColor = "Green";
        }

        if (ReferenceValues.JsonHvacMaster.IsHeatingMode) {
            HeatingCoolingStatus = "Heating";
            HeatingCoolingStatusColor = "White";
            HeatingCoolingText = "Heating To";
        } else {
            HeatingCoolingStatus = "Cooling";
            HeatingCoolingStatusColor = "CornflowerBlue";
            HeatingCoolingText = "Cooling To";
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "hvac":
            if (ReferenceValues.IsHvacComEstablished) {
                bool isFanAuto = ReferenceValues.JsonHvacMaster.IsFanAuto;
                bool isProgramRunning = ReferenceValues.JsonHvacMaster.IsProgramRunning;
                bool isHeatingMode = ReferenceValues.JsonHvacMaster.IsHeatingMode;
                int temp = ReferenceValues.JsonHvacMaster.TemperatureSet;

                EditHvac editHvac = new();
                editHvac.ShowDialog();
                editHvac.Close();

                if (isFanAuto != ReferenceValues.JsonHvacMaster.IsFanAuto) {
                    ReferenceValues.SerialPort.Write(ReferenceValues.JsonHvacMaster.IsFanAuto ? "2" : "1");
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "Changing HVAC Fan Mode: FanModeAuto " + ReferenceValues.JsonHvacMaster.IsFanAuto
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
                }

                if (isProgramRunning != ReferenceValues.JsonHvacMaster.IsProgramRunning) {
                    if (ReferenceValues.JsonHvacMaster.IsProgramRunning) {
                        ReferenceValues.SerialPort.Write(ReferenceValues.JsonHvacMaster.IsHeatingMode ? "4" : "3");
                    } else {
                        ReferenceValues.SerialPort.Write("5");
                    }

                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "Changing HVAC Program Mode: ProgramRunning " + ReferenceValues.JsonHvacMaster.IsProgramRunning
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
                }

                if (isHeatingMode != ReferenceValues.JsonHvacMaster.IsHeatingMode) {
                    ReferenceValues.SerialPort.Write("6");

                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "Changing HVAC Heating/Cooling Mode: IsHeatingMode " + ReferenceValues.JsonHvacMaster.IsHeatingMode
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
                }

                if (temp != ReferenceValues.JsonHvacMaster.TemperatureSet) {
                    char c = (char)(ReferenceValues.JsonHvacMaster.TemperatureSet + 50);
                    ReferenceValues.SerialPort.Write(c.ToString());

                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "Changing HVAC Temperature Set to: " + ReferenceValues.JsonHvacMaster.TemperatureSet + "°C"
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
                }

                GetButtonColors();
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

    #endregion
}