﻿using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Net;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class SettingsVM : BaseViewModel {
    private List<string> _trashDayList;

    private string _userAgent, _user1Name, _user2Name, _user3Name, _user4Name, _user5Name, _user1NameLegal, _user2NameLegal, _user3NameLegal, _user4NameLegal, _user5NameLegal,
        _user1Phone1, _user1Phone2, _user2Phone1, _user2Phone2, _petNames, _neighbor1Location, _neighbor1Name, _neighbor1Phone1, _neighbor1Phone2, _neighbor2Location,
        _neighbor2Name, _neighbor2Phone1, _neighbor2Phone2, _addressLine1, _addressLine2, _fireExtinguisherLocation, _hospitalAddressLine1, _hospitalAddressLine2, _wifiGuestName,
        _wifiGuestPassword, _wifiPrivateName, _wifiPrivatePassword, _policeName, _policePhone, _emergencyContact1Name, _emergencyContact1Phone1, _emergencyContact1Phone2,
        _emergencyContact2Name, _emergencyContact2Phone1, _emergencyContact2Phone2, _alarmCode, _comPort, _trashDaySelected, _weatherLocation, _gridId;

    private bool _valueImperialChecked, _valueMetricChecked, _isEditTasksMode, _isNormalMode, _isDebugMode, _user1Checked, _user2Checked, _user3Checked, _user4Checked,
        _user5Checked, _user1BehaviorChecked, _user2BehaviorChecked, _user3BehaviorChecked, _user4BehaviorChecked, _user5BehaviorChecked;

    private double _weatherLat, _weatherLon;

    private int gridX, gridY;

    public SettingsVM() {
        TrashDayList = new List<string>(new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "DISABLED" });
        TrashDaySelected = "Wednesday";

        UserAgent = ReferenceValues.JsonSettingsMaster.UserAgent;
        User1Name = ReferenceValues.JsonSettingsMaster.User1Name;
        User2Name = ReferenceValues.JsonSettingsMaster.User2Name;
        User3Name = ReferenceValues.JsonSettingsMaster.User3Name;
        User4Name = ReferenceValues.JsonSettingsMaster.User4Name;
        User5Name = ReferenceValues.JsonSettingsMaster.User5Name;
        User1NameLegal = ReferenceValues.JsonSettingsMaster.User1NameLegal;
        User2NameLegal = ReferenceValues.JsonSettingsMaster.User2NameLegal;
        User3NameLegal = ReferenceValues.JsonSettingsMaster.User3NameLegal;
        User4NameLegal = ReferenceValues.JsonSettingsMaster.User4NameLegal;
        User5NameLegal = ReferenceValues.JsonSettingsMaster.User5NameLegal;
        User1Phone1 = ReferenceValues.JsonSettingsMaster.User1Phone1;
        User1Phone2 = ReferenceValues.JsonSettingsMaster.User1Phone2;
        User2Phone1 = ReferenceValues.JsonSettingsMaster.User2Phone1;
        User2Phone2 = ReferenceValues.JsonSettingsMaster.User2Phone2;
        PetNames = ReferenceValues.JsonSettingsMaster.PetNames;
        Neighbor1Location = ReferenceValues.JsonSettingsMaster.Neighbor1Location;
        Neighbor1Name = ReferenceValues.JsonSettingsMaster.Neighbor1Name;
        Neighbor1Phone1 = ReferenceValues.JsonSettingsMaster.Neighbor1Phone1;
        Neighbor1Phone2 = ReferenceValues.JsonSettingsMaster.Neighbor1Phone2;
        Neighbor2Location = ReferenceValues.JsonSettingsMaster.Neighbor2Location;
        Neighbor2Name = ReferenceValues.JsonSettingsMaster.Neighbor2Name;
        Neighbor2Phone1 = ReferenceValues.JsonSettingsMaster.Neighbor2Phone1;
        Neighbor2Phone2 = ReferenceValues.JsonSettingsMaster.Neighbor2Phone2;
        AddressLine1 = ReferenceValues.JsonSettingsMaster.AddressLine1;
        AddressLine2 = ReferenceValues.JsonSettingsMaster.AddressLine2;
        FireExtinguisherLocation = ReferenceValues.JsonSettingsMaster.FireExtinguisherLocation;
        HospitalAddressLine1 = ReferenceValues.JsonSettingsMaster.HospitalAddressLine1;
        HospitalAddressLine2 = ReferenceValues.JsonSettingsMaster.HospitalAddressLine2;
        WifiGuestName = ReferenceValues.JsonSettingsMaster.WifiGuestName;
        WifiGuestPassword = ReferenceValues.JsonSettingsMaster.WifiGuestPassword;
        WifiPrivateName = ReferenceValues.JsonSettingsMaster.WifiPrivateName;
        WifiPrivatePassword = ReferenceValues.JsonSettingsMaster.WifiPrivatePassword;
        PoliceName = ReferenceValues.JsonSettingsMaster.PoliceName;
        PolicePhone = ReferenceValues.JsonSettingsMaster.PolicePhone;
        EmergencyContact1Name = ReferenceValues.JsonSettingsMaster.EmergencyContact1Name;
        EmergencyContact1Phone1 = ReferenceValues.JsonSettingsMaster.EmergencyContact1Phone1;
        EmergencyContact1Phone2 = ReferenceValues.JsonSettingsMaster.EmergencyContact1Phone2;
        EmergencyContact2Name = ReferenceValues.JsonSettingsMaster.EmergencyContact2Name;
        EmergencyContact2Phone1 = ReferenceValues.JsonSettingsMaster.EmergencyContact2Phone1;
        EmergencyContact2Phone2 = ReferenceValues.JsonSettingsMaster.EmergencyContact2Phone2;
        AlarmCode = ReferenceValues.JsonSettingsMaster.AlarmCode;
        ComPort = ReferenceValues.JsonSettingsMaster.ComPort;
        ValueImperialChecked = ReferenceValues.JsonSettingsMaster.IsImperialMode;
        ValueMetricChecked = !ValueImperialChecked;
        IsNormalMode = ReferenceValues.JsonSettingsMaster.IsNormalMode;
        IsEditTasksMode = ReferenceValues.JsonSettingsMaster.IsEditTasksMode;
        IsDebugMode = ReferenceValues.JsonSettingsMaster.IsDebugMode;
        TrashDaySelected = ReferenceValues.JsonSettingsMaster.TrashDay;
        User1Checked = ReferenceValues.JsonSettingsMaster.User1Checked;
        User2Checked = ReferenceValues.JsonSettingsMaster.User2Checked;
        User3Checked = ReferenceValues.JsonSettingsMaster.User3Checked;
        User4Checked = ReferenceValues.JsonSettingsMaster.User4Checked;
        User5Checked = ReferenceValues.JsonSettingsMaster.User5Checked;
        User1BehaviorChecked = ReferenceValues.JsonSettingsMaster.User1BehaviorChecked;
        User2BehaviorChecked = ReferenceValues.JsonSettingsMaster.User2BehaviorChecked;
        User3BehaviorChecked = ReferenceValues.JsonSettingsMaster.User3BehaviorChecked;
        User4BehaviorChecked = ReferenceValues.JsonSettingsMaster.User4BehaviorChecked;
        User5BehaviorChecked = ReferenceValues.JsonSettingsMaster.User5BehaviorChecked;
        WeatherLat = ReferenceValues.JsonSettingsMaster.WeatherLat;
        WeatherLon = ReferenceValues.JsonSettingsMaster.WeatherLon;
        GridX = ReferenceValues.JsonSettingsMaster.GridX;
        GridY = ReferenceValues.JsonSettingsMaster.GridY;
        GridId = ReferenceValues.JsonSettingsMaster.GridId;
        WeatherLocation = ReferenceValues.JsonSettingsMaster.WeatherLocation;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "save":
            ReferenceValues.JsonSettingsMaster.UserAgent = UserAgent;
            ReferenceValues.JsonSettingsMaster.User1Name = User1Name;
            ReferenceValues.JsonSettingsMaster.User2Name = User2Name;
            ReferenceValues.JsonSettingsMaster.User3Name = User3Name;
            ReferenceValues.JsonSettingsMaster.User4Name = User4Name;
            ReferenceValues.JsonSettingsMaster.User5Name = User5Name;
            ReferenceValues.JsonSettingsMaster.User1NameLegal = User1NameLegal;
            ReferenceValues.JsonSettingsMaster.User2NameLegal = User2NameLegal;
            ReferenceValues.JsonSettingsMaster.User3NameLegal = User3NameLegal;
            ReferenceValues.JsonSettingsMaster.User4NameLegal = User4NameLegal;
            ReferenceValues.JsonSettingsMaster.User5NameLegal = User5NameLegal;
            ReferenceValues.JsonSettingsMaster.User1Phone1 = User1Phone1;
            ReferenceValues.JsonSettingsMaster.User1Phone2 = User1Phone2;
            ReferenceValues.JsonSettingsMaster.User2Phone1 = User2Phone1;
            ReferenceValues.JsonSettingsMaster.User2Phone2 = User2Phone2;
            ReferenceValues.JsonSettingsMaster.PetNames = PetNames;
            ReferenceValues.JsonSettingsMaster.Neighbor1Location = Neighbor1Location;
            ReferenceValues.JsonSettingsMaster.Neighbor1Name = Neighbor1Name;
            ReferenceValues.JsonSettingsMaster.Neighbor1Phone1 = Neighbor1Phone1;
            ReferenceValues.JsonSettingsMaster.Neighbor1Phone2 = Neighbor1Phone2;
            ReferenceValues.JsonSettingsMaster.Neighbor2Location = Neighbor2Location;
            ReferenceValues.JsonSettingsMaster.Neighbor2Name = Neighbor2Name;
            ReferenceValues.JsonSettingsMaster.Neighbor2Phone1 = Neighbor2Phone1;
            ReferenceValues.JsonSettingsMaster.Neighbor2Phone2 = Neighbor2Phone2;
            ReferenceValues.JsonSettingsMaster.AddressLine1 = AddressLine1;
            ReferenceValues.JsonSettingsMaster.AddressLine2 = AddressLine2;
            ReferenceValues.JsonSettingsMaster.FireExtinguisherLocation = FireExtinguisherLocation;
            ReferenceValues.JsonSettingsMaster.HospitalAddressLine1 = HospitalAddressLine1;
            ReferenceValues.JsonSettingsMaster.HospitalAddressLine2 = HospitalAddressLine2;
            ReferenceValues.JsonSettingsMaster.WifiGuestName = WifiGuestName;
            ReferenceValues.JsonSettingsMaster.WifiGuestPassword = WifiGuestPassword;
            ReferenceValues.JsonSettingsMaster.WifiPrivateName = WifiPrivateName;
            ReferenceValues.JsonSettingsMaster.WifiPrivatePassword = WifiPrivatePassword;
            ReferenceValues.JsonSettingsMaster.PoliceName = PoliceName;
            ReferenceValues.JsonSettingsMaster.PolicePhone = PolicePhone;
            ReferenceValues.JsonSettingsMaster.EmergencyContact1Name = EmergencyContact1Name;
            ReferenceValues.JsonSettingsMaster.EmergencyContact1Phone1 = EmergencyContact1Phone1;
            ReferenceValues.JsonSettingsMaster.EmergencyContact1Phone2 = EmergencyContact1Phone2;
            ReferenceValues.JsonSettingsMaster.EmergencyContact2Name = EmergencyContact2Name;
            ReferenceValues.JsonSettingsMaster.EmergencyContact2Phone1 = EmergencyContact2Phone1;
            ReferenceValues.JsonSettingsMaster.EmergencyContact2Phone2 = EmergencyContact2Phone2;
            ReferenceValues.JsonSettingsMaster.AlarmCode = AlarmCode;
            ReferenceValues.JsonSettingsMaster.ComPort = ComPort;
            ReferenceValues.JsonSettingsMaster.IsImperialMode = ValueImperialChecked;
            ValueMetricChecked = !ValueImperialChecked;
            ReferenceValues.JsonSettingsMaster.IsNormalMode = IsNormalMode;
            ReferenceValues.JsonSettingsMaster.IsEditTasksMode = IsEditTasksMode;
            ReferenceValues.JsonSettingsMaster.IsDebugMode = IsDebugMode;
            ReferenceValues.JsonSettingsMaster.TrashDay = TrashDaySelected;
            ReferenceValues.JsonSettingsMaster.User1Checked = User1Checked;
            ReferenceValues.JsonSettingsMaster.User2Checked = User2Checked;
            ReferenceValues.JsonSettingsMaster.User3Checked = User3Checked;
            ReferenceValues.JsonSettingsMaster.User4Checked = User4Checked;
            ReferenceValues.JsonSettingsMaster.User5Checked = User5Checked;
            ReferenceValues.JsonSettingsMaster.User1BehaviorChecked = User1BehaviorChecked;
            ReferenceValues.JsonSettingsMaster.User2BehaviorChecked = User2BehaviorChecked;
            ReferenceValues.JsonSettingsMaster.User3BehaviorChecked = User3BehaviorChecked;
            ReferenceValues.JsonSettingsMaster.User4BehaviorChecked = User4BehaviorChecked;
            ReferenceValues.JsonSettingsMaster.User5BehaviorChecked = User5BehaviorChecked;
            ReferenceValues.JsonSettingsMaster.WeatherLat = WeatherLat;
            ReferenceValues.JsonSettingsMaster.WeatherLon = WeatherLon;
            ReferenceValues.JsonSettingsMaster.GridX = GridX;
            ReferenceValues.JsonSettingsMaster.GridY = GridY;
            ReferenceValues.JsonSettingsMaster.GridId = GridId;
            ReferenceValues.JsonSettingsMaster.WeatherLocation = WeatherLocation;

            if (!IsNormalMode && !IsEditTasksMode && !IsDebugMode) {
                ReferenceValues.JsonSettingsMaster.IsNormalMode = true;
            }

            try {
                FileHelpers.SaveFileText("settings", JsonSerializer.Serialize(ReferenceValues.JsonSettingsMaster), true);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "SettingsVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case "generate": {
            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            try {
                using WebClient client1 = new();
                Uri weatherLocationURL = new($"https://api.weather.gov/points/{WeatherLat},{WeatherLon}");
                client1.Headers.Add("User-Agent", "Home Control, " + UserAgent);
                string weatherLocation = client1.DownloadString(weatherLocationURL);
                JsonWeatherLocation location = JsonSerializer.Deserialize<JsonWeatherLocation>(weatherLocation, options);

                GridX = location.properties.gridX;
                GridY = location.properties.gridY;
                GridId = location.properties.gridId;
                WeatherLocation = location.properties.relativeLocation.properties.city + ", " + location.properties.relativeLocation.properties.state;
            } catch (Exception) {
                ReferenceValues.SoundToPlay = "unable";
                SoundDispatcher.PlaySound();
            }

            break;
        }
        case "location":
            GeoCoordinateWatcher watcher = new();

            watcher.PositionChanged += (s, e) => {
                WeatherLat = e.Position.Location.Latitude;
                WeatherLon = e.Position.Location.Longitude;
            };

            watcher.MovementThreshold = 100;
            watcher.Start();

            break;
        }
    }

    #region Fields

    public string UserAgent {
        get => _userAgent;
        set {
            _userAgent = value;
            RaisePropertyChangedEvent("UserAgent");
        }
    }

    public string User3Name {
        get => _user3Name;
        set {
            _user3Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User3Name");
        }
    }

    public string User4Name {
        get => _user4Name;
        set {
            _user4Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User4Name");
        }
    }

    public string User5Name {
        get => _user5Name;
        set {
            _user5Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User5Name");
        }
    }

    public string User3NameLegal {
        get => _user3NameLegal;
        set {
            _user3NameLegal = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User3NameLegal");
        }
    }

    public string User4NameLegal {
        get => _user4NameLegal;
        set {
            _user4NameLegal = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User4NameLegal");
        }
    }

    public string User5NameLegal {
        get => _user5NameLegal;
        set {
            _user5NameLegal = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User5NameLegal");
        }
    }

    public string User1Name {
        get => _user1Name;
        set {
            _user1Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User1Name");
        }
    }

    public string User1NameLegal {
        get => _user1NameLegal;
        set {
            _user1NameLegal = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User1NameLegal");
        }
    }

    public string User1Phone1 {
        get => _user1Phone1;
        set {
            _user1Phone1 = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User1Phone1");
        }
    }

    public string User1Phone2 {
        get => _user1Phone2;
        set {
            _user1Phone2 = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User1Phone2");
        }
    }

    public string User2Name {
        get => _user2Name;
        set {
            _user2Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User2Name");
        }
    }

    public string User2NameLegal {
        get => _user2NameLegal;
        set {
            _user2NameLegal = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("User2NameLegal");
        }
    }

    public string User2Phone1 {
        get => _user2Phone1;
        set {
            _user2Phone1 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("User2Phone1");
        }
    }

    public string User2Phone2 {
        get => _user2Phone2;
        set {
            _user2Phone2 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("User2Phone2");
        }
    }

    public string PetNames {
        get => _petNames;
        set {
            _petNames = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("PetNames");
        }
    }

    public string Neighbor1Location {
        get => _neighbor1Location;
        set {
            _neighbor1Location = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("Neighbor1Location");
        }
    }

    public string Neighbor1Name {
        get => _neighbor1Name;
        set {
            _neighbor1Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("Neighbor1Name");
        }
    }

    public string Neighbor1Phone1 {
        get => _neighbor1Phone1;
        set {
            _neighbor1Phone1 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("Neighbor1Phone1");
        }
    }

    public string Neighbor1Phone2 {
        get => _neighbor1Phone2;
        set {
            _neighbor1Phone2 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("Neighbor1Phone2");
        }
    }

    public string Neighbor2Location {
        get => _neighbor2Location;
        set {
            _neighbor2Location = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("Neighbor2Location");
        }
    }

    public string Neighbor2Name {
        get => _neighbor2Name;
        set {
            _neighbor2Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("Neighbor2Name");
        }
    }

    public string Neighbor2Phone1 {
        get => _neighbor2Phone1;
        set {
            _neighbor2Phone1 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("Neighbor2Phone1");
        }
    }

    public string Neighbor2Phone2 {
        get => _neighbor2Phone2;
        set {
            _neighbor2Phone2 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("Neighbor2Phone2");
        }
    }

    public string AddressLine1 {
        get => _addressLine1;
        set {
            _addressLine1 = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("AddressLine1");
        }
    }

    public string AddressLine2 {
        get => _addressLine2;
        set {
            _addressLine2 = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("AddressLine2");
        }
    }

    public string FireExtinguisherLocation {
        get => _fireExtinguisherLocation;
        set {
            _fireExtinguisherLocation = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("FireExtinguisherLocation");
        }
    }

    public string HospitalAddressLine1 {
        get => _hospitalAddressLine1;
        set {
            _hospitalAddressLine1 = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("HospitalAddressLine1");
        }
    }

    public string HospitalAddressLine2 {
        get => _hospitalAddressLine2;
        set {
            _hospitalAddressLine2 = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("HospitalAddressLine2");
        }
    }

    public string WifiGuestName {
        get => _wifiGuestName;
        set {
            _wifiGuestName = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("WifiGuestName");
        }
    }

    public string WifiGuestPassword {
        get => _wifiGuestPassword;
        set {
            _wifiGuestPassword = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("WifiGuestPassword");
        }
    }

    public string WifiPrivateName {
        get => _wifiPrivateName;
        set {
            _wifiPrivateName = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("WifiPrivateName");
        }
    }

    public string WifiPrivatePassword {
        get => _wifiPrivatePassword;
        set {
            _wifiPrivatePassword = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("WifiPrivatePassword");
        }
    }

    public string PoliceName {
        get => _policeName;
        set {
            _policeName = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("PoliceName");
        }
    }

    public string PolicePhone {
        get => _policePhone;
        set {
            _policePhone = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("PolicePhone");
        }
    }

    public string EmergencyContact1Name {
        get => _emergencyContact1Name;
        set {
            _emergencyContact1Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("EmergencyContact1Name");
        }
    }

    public string EmergencyContact1Phone1 {
        get => _emergencyContact1Phone1;
        set {
            _emergencyContact1Phone1 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("EmergencyContact1Phone1");
        }
    }

    public string EmergencyContact1Phone2 {
        get => _emergencyContact1Phone2;
        set {
            _emergencyContact1Phone2 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("EmergencyContact1Phone2");
        }
    }

    public string EmergencyContact2Name {
        get => _emergencyContact2Name;
        set {
            _emergencyContact2Name = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("EmergencyContact2Name");
        }
    }

    public string EmergencyContact2Phone1 {
        get => _emergencyContact2Phone1;
        set {
            _emergencyContact2Phone1 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("EmergencyContact2Phone1");
        }
    }

    public string EmergencyContact2Phone2 {
        get => _emergencyContact2Phone2;
        set {
            _emergencyContact2Phone2 = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("EmergencyContact2Phone2");
        }
    }

    public string AlarmCode {
        get => _alarmCode;
        set {
            _alarmCode = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("AlarmCode");
        }
    }

    public string ComPort {
        get => _comPort;
        set {
            _comPort = value;
            RaisePropertyChangedEvent("ComPort");
        }
    }

    public bool ValueImperialChecked {
        get => _valueImperialChecked;
        set {
            _valueImperialChecked = value;
            RaisePropertyChangedEvent("ValueImperialChecked");
        }
    }

    public bool ValueMetricChecked {
        get => _valueMetricChecked;
        set {
            _valueMetricChecked = value;
            RaisePropertyChangedEvent("ValueMetricChecked");
        }
    }

    public bool IsNormalMode {
        get => _isNormalMode;
        set {
            _isNormalMode = value;
            RaisePropertyChangedEvent("IsNormalMode");
        }
    }

    public bool IsEditTasksMode {
        get => _isEditTasksMode;
        set {
            _isEditTasksMode = value;
            RaisePropertyChangedEvent("IsEditTasksMode");
        }
    }

    public bool IsDebugMode {
        get => _isDebugMode;
        set {
            _isDebugMode = value;
            RaisePropertyChangedEvent("IsDebugMode");
        }
    }

    public string TrashDaySelected {
        get => _trashDaySelected;
        set {
            _trashDaySelected = value;
            RaisePropertyChangedEvent("TrashDaySelected");
        }
    }

    public List<string> TrashDayList {
        get => _trashDayList;
        set {
            _trashDayList = value;
            RaisePropertyChangedEvent("TrashDayList");
        }
    }

    public bool User1Checked {
        get => _user1Checked;
        set {
            _user1Checked = value;
            RaisePropertyChangedEvent("User1Checked");
        }
    }

    public bool User2Checked {
        get => _user2Checked;
        set {
            _user2Checked = value;
            RaisePropertyChangedEvent("User2Checked");
        }
    }

    public bool User3Checked {
        get => _user3Checked;
        set {
            _user3Checked = value;
            RaisePropertyChangedEvent("User3Checked");
        }
    }

    public bool User4Checked {
        get => _user4Checked;
        set {
            _user4Checked = value;
            RaisePropertyChangedEvent("User4Checked");
        }
    }

    public bool User5Checked {
        get => _user5Checked;
        set {
            _user5Checked = value;
            RaisePropertyChangedEvent("User5Checked");
        }
    }

    public bool User1BehaviorChecked {
        get => _user1BehaviorChecked;
        set {
            _user1BehaviorChecked = value;
            RaisePropertyChangedEvent("User1BehaviorChecked");
        }
    }

    public bool User2BehaviorChecked {
        get => _user2BehaviorChecked;
        set {
            _user2BehaviorChecked = value;
            RaisePropertyChangedEvent("User2BehaviorChecked");
        }
    }

    public bool User3BehaviorChecked {
        get => _user3BehaviorChecked;
        set {
            _user3BehaviorChecked = value;
            RaisePropertyChangedEvent("User3BehaviorChecked");
        }
    }

    public bool User4BehaviorChecked {
        get => _user4BehaviorChecked;
        set {
            _user4BehaviorChecked = value;
            RaisePropertyChangedEvent("User4BehaviorChecked");
        }
    }

    public bool User5BehaviorChecked {
        get => _user5BehaviorChecked;
        set {
            _user5BehaviorChecked = value;
            RaisePropertyChangedEvent("User5BehaviorChecked");
        }
    }

    public double WeatherLat {
        get => _weatherLat;
        set {
            _weatherLat = value;
            RaisePropertyChangedEvent("WeatherLat");
        }
    }

    public double WeatherLon {
        get => _weatherLon;
        set {
            _weatherLon = value;
            RaisePropertyChangedEvent("WeatherLon");
        }
    }

    public int GridX {
        get => gridX;
        set {
            gridX = value;
            RaisePropertyChangedEvent("GridX");
        }
    }

    public int GridY {
        get => gridY;
        set {
            gridY = value;
            RaisePropertyChangedEvent("GridY");
        }
    }

    public string GridId {
        get => _gridId;
        set {
            _gridId = value;
            RaisePropertyChangedEvent("GridId");
        }
    }

    public string WeatherLocation {
        get => _weatherLocation;
        set {
            _weatherLocation = value;
            RaisePropertyChangedEvent("WeatherLocation");
        }
    }

    #endregion
}