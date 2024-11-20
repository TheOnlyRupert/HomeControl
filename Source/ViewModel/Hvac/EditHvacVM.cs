using System.Windows.Input;
using HomeControl.Source.Modules.Hvac;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class EditHvacVM : BaseViewModel {
    private string _programStatus, _fanStatus, _heatingCoolingStatus, _useSchedule, _programStatusColor, _fanStatusColor, _heatingCoolingStatusColor, _useScheduleColor, _temperatureSet,
        _temperatureInside;

    public EditHvacVM() {
        TemperatureDisplay();
        GetButtonColors();
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private void TemperatureDisplay() {
        double f = ReferenceValues.TemperatureSet * 1.8 + 32;
        TemperatureSet = ReferenceValues.TemperatureSet + "°C  or  " + (int)f + "°F";

        if (Math.Abs(ReferenceValues.TemperatureInside - -99) < 1) {
            TemperatureInside = "Unknown";
        } else {
            f = ReferenceValues.TemperatureInside * 1.8 + 32;
            TemperatureInside = ReferenceValues.TemperatureInside + "°C  or  " + (int)f + "°F";
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "programStatus":
            ReferenceValues.IsProgramRunning = !ReferenceValues.IsProgramRunning;

            if (!ReferenceValues.IsProgramRunning) {
                ReferenceValues.UseSchedule = false;
            }

            break;
        case "fanStatus":
            ReferenceValues.IsFanAuto = !ReferenceValues.IsFanAuto;

            break;
        case "heatingCoolingStatus":
            ReferenceValues.IsHeatingMode = !ReferenceValues.IsHeatingMode;

            break;
        case "useSchedule":
            if (ReferenceValues.IsProgramRunning) {
                ReferenceValues.UseSchedule = !ReferenceValues.UseSchedule;
            } else {
                ReferenceValues.UseSchedule = false;
            }

            break;
        case "schedule":
            EditHvacSchedule editHvacSchedule = new();
            editHvacSchedule.ShowDialog();
            editHvacSchedule.Close();

            break;
        case "subTemp":
            if (ReferenceValues.TemperatureSet > 15) {
                ReferenceValues.TemperatureSet--;
            }

            TemperatureDisplay();

            break;
        case "addTemp":
            if (ReferenceValues.TemperatureSet < 30) {
                ReferenceValues.TemperatureSet++;
            }

            TemperatureDisplay();

            break;
        }

        GetButtonColors();
    }

    private void GetButtonColors() {
        if (ReferenceValues.IsProgramRunning) {
            ProgramStatus = "Program:  ON";
            ProgramStatusColor = "Green";
        } else {
            ProgramStatus = "Program:  OFF";
            ProgramStatusColor = "Transparent";
        }

        if (ReferenceValues.IsFanAuto) {
            FanStatus = "Fan Mode:  AUTO";
            FanStatusColor = "Transparent";
        } else {
            FanStatus = "Fan Mode:  ON";
            FanStatusColor = "Green";
        }

        if (ReferenceValues.IsHeatingMode) {
            HeatingCoolingStatus = "Mode:  HEATING";
            HeatingCoolingStatusColor = "Red";
        } else {
            HeatingCoolingStatus = "Mode:  COOLING";
            HeatingCoolingStatusColor = "CornflowerBlue";
        }

        if (ReferenceValues.UseSchedule) {
            UseSchedule = "Follow Schedule:  YES";
            UseScheduleColor = "Green";
        } else {
            UseSchedule = "Follow Schedule:  OFF";
            UseScheduleColor = "Transparent";
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

    public string UseSchedule {
        get => _useSchedule;
        set {
            _useSchedule = value;
            RaisePropertyChangedEvent("UseSchedule");
        }
    }

    public string UseScheduleColor {
        get => _useScheduleColor;
        set {
            _useScheduleColor = value;
            RaisePropertyChangedEvent("UseScheduleColor");
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

    public string TemperatureInside {
        get => _temperatureInside;
        set {
            _temperatureInside = value;
            RaisePropertyChangedEvent("TemperatureInside");
        }
    }

    #endregion
}