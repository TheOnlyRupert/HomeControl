using System;
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
    private string _databasePassword;
    private bool _isDebugModeChecked, _isMetricUnitsChecked, _isEasterEggsChecked, _isLocallyHosted, _isDatabaseHosted, _isIpEnabled;

    private List<string> _trashDayList;

    private string _userAgent, _user1Name, _user2Name, _user3Name, _user4Name, _user5Name, _user1NameLegal, _user2NameLegal, _user3NameLegal, _user4NameLegal, _user5NameLegal,
        _user1Phone1, _user1Phone2, _user2Phone1, _user2Phone2, _petNames, _neighbor1Location, _neighbor1Name, _neighbor1Phone1, _neighbor1Phone2, _neighbor2Location,
        _neighbor2Name, _neighbor2Phone1, _neighbor2Phone2, _addressLine1, _addressLine2, _fireExtinguisherLocation, _hospitalAddressLine1, _hospitalAddressLine2, _wifiGuestName,
        _wifiGuestPassword, _wifiPrivateName, _wifiPrivatePassword, _policeName, _policePhone, _emergencyContact1Name, _emergencyContact1Phone1, _emergencyContact1Phone2,
        _emergencyContact2Name, _emergencyContact2Phone1, _emergencyContact2Phone2, _alarmCode, _comPort, _trashDaySelected, _weatherLocation, _gridId, _financeBlock1, _financeBlock2, _financeBlock3,
        _financeBlock4, _financeBlock5, _financeBlock6, _financeBlock7, _financeBlock8, _financeBlock9, _databaseHost, _databaseUsername;

    private double _weatherLat, _weatherLon;

    private int gridX, gridY;

    public SettingsVM() {
        TrashDayList = [
            ..new[] {
                "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "DISABLED"
            }
        ];
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
        IsDebugModeChecked = ReferenceValues.JsonSettingsMaster.DebugMode;
        IsMetricUnitsChecked = ReferenceValues.JsonSettingsMaster.UseMetricUnits;
        IsEasterEggsChecked = ReferenceValues.JsonSettingsMaster.EnableEasterEggs;
        TrashDaySelected = ReferenceValues.JsonSettingsMaster.TrashDay;
        WeatherLat = ReferenceValues.JsonSettingsMaster.WeatherLat;
        WeatherLon = ReferenceValues.JsonSettingsMaster.WeatherLon;
        GridX = ReferenceValues.JsonSettingsMaster.GridX;
        GridY = ReferenceValues.JsonSettingsMaster.GridY;
        GridId = ReferenceValues.JsonSettingsMaster.GridId;
        WeatherLocation = ReferenceValues.JsonSettingsMaster.WeatherLocation;
        FinanceBlock1 = ReferenceValues.JsonSettingsMaster.FinanceBlock1;
        FinanceBlock2 = ReferenceValues.JsonSettingsMaster.FinanceBlock2;
        FinanceBlock3 = ReferenceValues.JsonSettingsMaster.FinanceBlock3;
        FinanceBlock4 = ReferenceValues.JsonSettingsMaster.FinanceBlock4;
        FinanceBlock5 = ReferenceValues.JsonSettingsMaster.FinanceBlock5;
        FinanceBlock6 = ReferenceValues.JsonSettingsMaster.FinanceBlock6;
        FinanceBlock7 = ReferenceValues.JsonSettingsMaster.FinanceBlock7;
        FinanceBlock8 = ReferenceValues.JsonSettingsMaster.FinanceBlock8;
        FinanceBlock9 = ReferenceValues.JsonSettingsMaster.FinanceBlock9;
        DatabaseHost = ReferenceValues.JsonSettingsMaster.DatabaseHost;
        DatabaseUsername = ReferenceValues.JsonSettingsMaster.DatabaseUsername;
        DatabasePassword = ReferenceValues.JsonSettingsMaster.DatabasePassword;

        if (ReferenceValues.JsonSettingsMaster.IsDatabaseHosted) {
            IsDatabaseHosted = true;
            IsLocallyHosted = false;
        } else {
            IsDatabaseHosted = false;
            IsLocallyHosted = true;
        }
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

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
            ReferenceValues.JsonSettingsMaster.DebugMode = IsDebugModeChecked;
            ReferenceValues.JsonSettingsMaster.UseMetricUnits = IsMetricUnitsChecked;
            ReferenceValues.JsonSettingsMaster.EnableEasterEggs = IsEasterEggsChecked;
            ReferenceValues.JsonSettingsMaster.TrashDay = TrashDaySelected;
            ReferenceValues.JsonSettingsMaster.WeatherLat = WeatherLat;
            ReferenceValues.JsonSettingsMaster.WeatherLon = WeatherLon;
            ReferenceValues.JsonSettingsMaster.GridX = GridX;
            ReferenceValues.JsonSettingsMaster.GridY = GridY;
            ReferenceValues.JsonSettingsMaster.GridId = GridId;
            ReferenceValues.JsonSettingsMaster.WeatherLocation = WeatherLocation;
            ReferenceValues.JsonSettingsMaster.FinanceBlock1 = FinanceBlock1;
            ReferenceValues.JsonSettingsMaster.FinanceBlock2 = FinanceBlock2;
            ReferenceValues.JsonSettingsMaster.FinanceBlock3 = FinanceBlock3;
            ReferenceValues.JsonSettingsMaster.FinanceBlock4 = FinanceBlock4;
            ReferenceValues.JsonSettingsMaster.FinanceBlock5 = FinanceBlock5;
            ReferenceValues.JsonSettingsMaster.FinanceBlock6 = FinanceBlock6;
            ReferenceValues.JsonSettingsMaster.FinanceBlock7 = FinanceBlock7;
            ReferenceValues.JsonSettingsMaster.FinanceBlock8 = FinanceBlock8;
            ReferenceValues.JsonSettingsMaster.FinanceBlock9 = FinanceBlock9;
            ReferenceValues.JsonSettingsMaster.DatabaseHost = DatabaseHost;
            ReferenceValues.JsonSettingsMaster.DatabaseUsername = DatabaseUsername;
            ReferenceValues.JsonSettingsMaster.DatabasePassword = DatabasePassword;

            if (IsLocallyHosted) {
                ReferenceValues.JsonSettingsMaster.IsDatabaseHosted = false;
                IsDatabaseHosted = false;
            } else {
                ReferenceValues.JsonSettingsMaster.IsDatabaseHosted = true;
                IsDatabaseHosted = true;
                IsLocallyHosted = false;
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

            watcher.PositionChanged += (_, e) => {
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

    public bool IsDebugModeChecked {
        get => _isDebugModeChecked;
        set {
            _isDebugModeChecked = value;
            RaisePropertyChangedEvent("IsDebugModeChecked");
        }
    }

    public bool IsMetricUnitsChecked {
        get => _isMetricUnitsChecked;
        set {
            _isMetricUnitsChecked = value;
            RaisePropertyChangedEvent("IsMetricUnitsChecked");
        }
    }

    public bool IsEasterEggsChecked {
        get => _isEasterEggsChecked;
        set {
            _isEasterEggsChecked = value;
            RaisePropertyChangedEvent("IsEasterEggsChecked");
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

    public string FinanceBlock1 {
        get => _financeBlock1;
        set {
            _financeBlock1 = value;
            RaisePropertyChangedEvent("FinanceBlock1");
        }
    }

    public string FinanceBlock2 {
        get => _financeBlock2;
        set {
            _financeBlock2 = value;
            RaisePropertyChangedEvent("FinanceBlock2");
        }
    }

    public string FinanceBlock3 {
        get => _financeBlock3;
        set {
            _financeBlock3 = value;
            RaisePropertyChangedEvent("FinanceBlock3");
        }
    }

    public string FinanceBlock4 {
        get => _financeBlock4;
        set {
            _financeBlock4 = value;
            RaisePropertyChangedEvent("FinanceBlock4");
        }
    }

    public string FinanceBlock5 {
        get => _financeBlock5;
        set {
            _financeBlock5 = value;
            RaisePropertyChangedEvent("FinanceBlock5");
        }
    }

    public string FinanceBlock6 {
        get => _financeBlock6;
        set {
            _financeBlock6 = value;
            RaisePropertyChangedEvent("FinanceBlock6");
        }
    }

    public string FinanceBlock7 {
        get => _financeBlock7;
        set {
            _financeBlock7 = value;
            RaisePropertyChangedEvent("FinanceBlock7");
        }
    }

    public string FinanceBlock8 {
        get => _financeBlock8;
        set {
            _financeBlock8 = value;
            RaisePropertyChangedEvent("FinanceBlock8");
        }
    }

    public string FinanceBlock9 {
        get => _financeBlock9;
        set {
            _financeBlock9 = value;
            RaisePropertyChangedEvent("FinanceBlock9");
        }
    }

    public bool IsLocallyHosted {
        get => _isLocallyHosted;
        set {
            _isLocallyHosted = value;
            if (value) {
                IsIpEnabled = false;
            }

            RaisePropertyChangedEvent("IsLocallyHosted");
        }
    }

    public bool IsDatabaseHosted {
        get => _isDatabaseHosted;
        set {
            _isDatabaseHosted = value;
            if (value) {
                IsIpEnabled = true;
            }

            RaisePropertyChangedEvent("IsDatabaseHosted");
        }
    }

    public string DatabaseHost {
        get => _databaseHost;
        set {
            _databaseHost = value;
            RaisePropertyChangedEvent("DatabaseHost");
        }
    }

    public string DatabaseUsername {
        get => _databaseUsername;
        set {
            _databaseUsername = value;
            RaisePropertyChangedEvent("DatabaseUsername");
        }
    }

    public string DatabasePassword {
        get => _databasePassword;
        set {
            _databasePassword = value;
            RaisePropertyChangedEvent("DatabasePassword");
        }
    }

    public bool IsIpEnabled {
        get => _isIpEnabled;
        set {
            _isIpEnabled = value;
            RaisePropertyChangedEvent("IsIpEnabled");
        }
    }

    #endregion
}