using System;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Hvac;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class HvacVM : BaseViewModel {
    private string _tempInside, _heatingCoolingText, _tempAdjusted, _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor,
        _tempInsideColor, _tempAdjustedColor, _fanStatusColor, _heatingCoolingStatusColor, _intHumidity;

    public HvacVM() {
        ReferenceValues.JsonHvacSettings.IsFanAuto = true;
        ReferenceValues.JsonHvacSettings.IsProgramRunning = false;
        ReferenceValues.JsonHvacSettings.IsHeatingMode = false;
        if (ReferenceValues.JsonHvacSettings.TemperatureSet == 0) {
            ReferenceValues.JsonHvacSettings.TemperatureSet = 26;
        }

        TempInsideColor = "White";
        ReferenceValues.JsonHvacSettings.IsStandby = true;

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

        TempAdjusted = ReferenceValues.JsonHvacSettings.TemperatureSet + "°";
        TempAdjustedColor = "White";

        if (ReferenceValues.InteriorTemp == -99) {
            TempInside = "??";
            TempInsideColor = "Red";
        } else {
            TempInside = ReferenceValues.InteriorTemp + "°";
            TempInsideColor = "White";
        }

        if (ReferenceValues.InteriorHumidity == -99) {
            IntHumidity = "??";
        } else {
            IntHumidity = ReferenceValues.InteriorHumidity + "%";
        }

        if (ReferenceValues.JsonHvacSettings.IsProgramRunning) {
            if (ReferenceValues.JsonHvacSettings.IsStandby) {
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

        if (ReferenceValues.JsonHvacSettings.IsFanAuto) {
            FanStatus = "Auto";
            FanStatusColor = "White";
        } else {
            FanStatus = "On";
            FanStatusColor = "Green";
        }

        if (ReferenceValues.JsonHvacSettings.IsHeatingMode) {
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
                bool isFanAuto = ReferenceValues.JsonHvacSettings.IsFanAuto;
                bool isProgramRunning = ReferenceValues.JsonHvacSettings.IsProgramRunning;
                bool isHeatingMode = ReferenceValues.JsonHvacSettings.IsHeatingMode;
                int temp = ReferenceValues.JsonHvacSettings.TemperatureSet;

                EditHvac editHvac = new();
                editHvac.ShowDialog();
                editHvac.Close();

                if (isFanAuto != ReferenceValues.JsonHvacSettings.IsFanAuto) {
                    ReferenceValues.SerialPortMaster.Write(ReferenceValues.JsonHvacSettings.IsFanAuto ? "1" : "2");
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "Changing HVAC Fan Mode: FanModeAuto " + ReferenceValues.JsonHvacSettings.IsFanAuto
                    });
                    SaveDebugFile.Save();
                }

                if (isProgramRunning != ReferenceValues.JsonHvacSettings.IsProgramRunning) {
                    if (ReferenceValues.JsonHvacSettings.IsProgramRunning) {
                        ReferenceValues.SerialPortMaster.Write(ReferenceValues.JsonHvacSettings.IsHeatingMode ? "4" : "3");
                    } else {
                        ReferenceValues.SerialPortMaster.Write("5");
                    }

                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "Changing HVAC Program Mode: ProgramRunning " + ReferenceValues.JsonHvacSettings.IsProgramRunning
                    });
                    SaveDebugFile.Save();
                }

                if (isHeatingMode != ReferenceValues.JsonHvacSettings.IsHeatingMode) {
                    ReferenceValues.SerialPortMaster.Write("8");

                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "Changing HVAC Heating/Cooling Mode: IsHeatingMode " + ReferenceValues.JsonHvacSettings.IsHeatingMode
                    });
                    SaveDebugFile.Save();
                }

                if (temp != ReferenceValues.JsonHvacSettings.TemperatureSet) {
                    char c = (char)(ReferenceValues.JsonHvacSettings.TemperatureSet + 15);
                    ReferenceValues.SerialPortMaster.Write(c.ToString());

                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "HvacVM",
                        Description = "Changing HVAC Temperature Set to: " + ReferenceValues.JsonHvacSettings.TemperatureSet + "°C"
                    });
                    SaveDebugFile.Save();
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