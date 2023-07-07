using System.Windows.Input;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class EditHvacVM : BaseViewModel {
    private string _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor, _fanStatusColor, _heatingCoolingStatusColor, _temperatureSet;

    public EditHvacVM() {
        TemperatureSet = ReferenceValues.JsonHvacSettings.TemperatureSet + "°";
        GetButtonColors();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "programStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacSettings.IsProgramRunning = !ReferenceValues.JsonHvacSettings.IsProgramRunning;

                GetButtonColors();
            }

            break;
        case "fanStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacSettings.IsFanAuto = !ReferenceValues.JsonHvacSettings.IsFanAuto;

                GetButtonColors();
            }

            break;
        case "heatingCoolingStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacSettings.IsHeatingMode = !ReferenceValues.JsonHvacSettings.IsHeatingMode;
            }

            break;
        case "subTemp":
            if (ReferenceValues.JsonHvacSettings.TemperatureSet > 15) {
                ReferenceValues.JsonHvacSettings.TemperatureSet--;
            }


            break;
        case "addTemp":
            if (ReferenceValues.JsonHvacSettings.TemperatureSet < 30) {
                ReferenceValues.JsonHvacSettings.TemperatureSet++;
            }

            break;
        }

        GetButtonColors();
        TemperatureSet = ReferenceValues.JsonHvacSettings.TemperatureSet + "°";
        HvacCrossPlay.SaveJson();
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

        if (ReferenceValues.JsonHvacSettings.IsProgramRunning) {
            ProgramStatus = "System: On";
            ProgramStatusColor = "Green";
        } else {
            ProgramStatus = "System: Off";
            ProgramStatusColor = "Transparent";
        }

        if (ReferenceValues.JsonHvacSettings.IsFanAuto) {
            FanStatus = "Fan Mode: Auto";
            FanStatusColor = "Transparent";
        } else {
            FanStatus = "Fan Mode: On";
            FanStatusColor = "Green";
        }

        if (ReferenceValues.JsonHvacSettings.IsHeatingMode) {
            HeatingCoolingStatus = "Temperature Status: Heating";
            HeatingCoolingStatusColor = "Red";
        } else {
            HeatingCoolingStatus = "Temperature Status: Cooling";
            HeatingCoolingStatusColor = "CornflowerBlue";
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