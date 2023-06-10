using System;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class EditHvacVM : BaseViewModel {
    private string _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor, _fanStatusColor, _heatingCoolingStatusColor, _temperatureSet;

    public EditHvacVM() {
        TemperatureSet = ReferenceValues.JsonHvacSettings.TemperatureSet + "°";

        GetButtonColors();
        SaveJson();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "programStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                if (ReferenceValues.JsonHvacSettings.ProgramState == 0) {
                    ReferenceValues.JsonHvacSettings.ProgramState = 1;
                } else {
                    ReferenceValues.JsonHvacSettings.ProgramState = 0;
                }

                GetButtonColors();
            }

            break;
        case "fanStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacSettings.IsFanOnMode = !ReferenceValues.JsonHvacSettings.IsFanOnMode;

                GetButtonColors();
            }

            break;
        case "heatingCoolingStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacSettings.IsHeatingMode = !ReferenceValues.JsonHvacSettings.IsHeatingMode;

                GetButtonColors();
            }

            break;
        case "subTemp":
            if (ReferenceValues.JsonHvacSettings.TemperatureSet > 50) {
                ReferenceValues.JsonHvacSettings.TemperatureSet--;
            }

            break;
        case "addTemp":
            if (ReferenceValues.JsonHvacSettings.TemperatureSet < 80) {
                ReferenceValues.JsonHvacSettings.TemperatureSet++;
            }

            break;
        }

        TemperatureSet = ReferenceValues.JsonHvacSettings.TemperatureSet + "°";
        SaveJson();
    }

    private void GetButtonColors() {
        if (!ReferenceValues.IsHvacComEstablished) {
            ProgramStatus = "Offline";
            ProgramStatusColor = "Transparent";
            FanStatus = "Offline";
            FanStatusColor = "Transparent";
            HeatingCoolingStatus = "Offline";
            HeatingCoolingStatusColor = "Transparent";
            return;
        }

        switch (ReferenceValues.JsonHvacSettings.ProgramState) {
        case 0:
            ProgramStatus = "System: Off";
            ProgramStatusColor = "Transparent";
            break;
        case 1:
        case 2:
            ProgramStatus = "System: Running";
            ProgramStatusColor = "Green";
            break;
        }

        if (ReferenceValues.JsonHvacSettings.IsFanOnMode) {
            FanStatus = "Fan Mode: On";
            FanStatusColor = "Green";
        } else {
            FanStatus = "Fan Mode: Auto";
            FanStatusColor = "Transparent";
        }

        if (ReferenceValues.JsonHvacSettings.IsHeatingMode) {
            HeatingCoolingStatus = "Temperature Status: Heating";
            HeatingCoolingStatusColor = "Red";
        } else {
            HeatingCoolingStatus = "Temperature Status: Cooling";
            HeatingCoolingStatusColor = "CornflowerBlue";
        }
    }

    private void SaveJson() {
        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonHvacSettings);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "hvac.json", jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditHvacVM",
                Description = e.ToString()
            });
        }
    }

    #region Fields

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

    public string TemperatureSet {
        get => _temperatureSet;
        set {
            _temperatureSet = value;
            RaisePropertyChangedEvent("TemperatureSet");
        }
    }

    #endregion
}