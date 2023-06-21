﻿using System.Windows.Input;
using HomeControl.Source.Modules.Hvac;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class HvacVM : BaseViewModel {
    private string _tempInside, _heatingCoolingText, _tempAdjusted, _hvacIcon, _programStatus, _fanStatus, _heatingCoolingStatus, _programStatusColor,
        _tempInsideColor, _tempAdjustedColor, _fanStatusColor, _heatingCoolingStatusColor;

    public HvacVM() {
        if (ReferenceValues.JsonHvacSettings.TemperatureSet == 0) {
            ReferenceValues.JsonHvacSettings.TemperatureSet = 70;
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
            return;
        }

        TempAdjusted = ReferenceValues.JsonHvacSettings.TemperatureSet + "°";
        if (ReferenceValues.InteriorTemp == -99) {
            TempInside = "??";
        } else {
            TempInside = ReferenceValues.InteriorTemp + "°";
        }

        TempAdjustedColor = "White";

        if (ReferenceValues.JsonHvacSettings.IsProgramRunning) {
            if (ReferenceValues.JsonHvacSettings.IsStandby) {
                ProgramStatus = "Standby";
                ProgramStatusColor = "Yellow";
                HvacIcon = "../../../Resources/Images/hvac/hvac.gif";
            } else {
                ProgramStatus = "Running";
                ProgramStatusColor = "Green";
                HvacIcon = "../../../Resources/Images/hvac/hvac.gif";
            }
        } else {
            ProgramStatus = "Off";
            ProgramStatusColor = "White";
            HvacIcon = "../../../Resources/Images/hvac/hvac.gif";
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
            EditHvac editHvac = new();
            editHvac.ShowDialog();
            editHvac.Close();

            GetButtonColors();
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

    public string HvacIcon {
        get => _hvacIcon;
        set {
            _hvacIcon = value;
            RaisePropertyChangedEvent("HvacIcon");
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

    #endregion
}