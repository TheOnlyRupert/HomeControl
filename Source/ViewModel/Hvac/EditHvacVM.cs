using System.Windows.Input;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class EditHvacVM : BaseViewModel {
    private string _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor, _fanStatusColor, _heatingCoolingStatusColor, _temperatureSet, _override, _overrideColor;

    public EditHvacVM() {
        if (!ReferenceValues.JsonSettingsMaster.useMetricUnits) {
            double f = ReferenceValues.JsonHvacMaster.TemperatureSet * 1.8 + 32;
            TemperatureSet = (int)f + "°";
        } else {
            TemperatureSet = ReferenceValues.JsonHvacMaster.TemperatureSet + "°";
        }

        GetButtonColors();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "override":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacMaster.IsOverride = !ReferenceValues.JsonHvacMaster.IsOverride;
            }

            break;
        case "programStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacMaster.IsProgramRunning = !ReferenceValues.JsonHvacMaster.IsProgramRunning;
            }

            break;
        case "fanStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacMaster.IsFanAuto = !ReferenceValues.JsonHvacMaster.IsFanAuto;
            }

            break;
        case "heatingCoolingStatus":
            if (ReferenceValues.IsHvacComEstablished) {
                ReferenceValues.JsonHvacMaster.IsHeatingMode = !ReferenceValues.JsonHvacMaster.IsHeatingMode;
            }

            break;
        case "subTemp":
            if (ReferenceValues.JsonHvacMaster.TemperatureSet > 15) {
                ReferenceValues.JsonHvacMaster.TemperatureSet--;
            }


            break;
        case "addTemp":
            if (ReferenceValues.JsonHvacMaster.TemperatureSet < 30) {
                ReferenceValues.JsonHvacMaster.TemperatureSet++;
            }

            break;
        }

        GetButtonColors();

        if (!ReferenceValues.JsonSettingsMaster.useMetricUnits) {
            double f = ReferenceValues.JsonHvacMaster.TemperatureSet * 1.8 + 32;
            TemperatureSet = (int)f + "°";
        } else {
            TemperatureSet = ReferenceValues.JsonHvacMaster.TemperatureSet + "°";
        }

        HvacCrossPlay.SaveJson();
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonHvacMaster.IsOverride) {
            Override = "Override: ENABLED";
            OverrideColor = "Green";
        } else {
            Override = "Override: DISABLED";
            OverrideColor = "Transparent";
        }

        if (ReferenceValues.JsonHvacMaster.IsProgramRunning) {
            ProgramStatus = "System: On";
            ProgramStatusColor = "Green";
        } else {
            ProgramStatus = "System: Off";
            ProgramStatusColor = "Transparent";
        }

        if (ReferenceValues.JsonHvacMaster.IsFanAuto) {
            FanStatus = "Fan Mode: Auto";
            FanStatusColor = "Transparent";
        } else {
            FanStatus = "Fan Mode: On";
            FanStatusColor = "Green";
        }

        if (ReferenceValues.JsonHvacMaster.IsHeatingMode) {
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

    public string Override {
        get => _override;
        set {
            _override = value;
            RaisePropertyChangedEvent("Override");
        }
    }

    public string OverrideColor {
        get => _overrideColor;
        set {
            _overrideColor = value;
            RaisePropertyChangedEvent("OverrideColor");
        }
    }

    #endregion
}