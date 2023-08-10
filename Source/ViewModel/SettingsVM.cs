using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class SettingsVM : BaseViewModel {
    private List<string> _trashDayList;

    private string _userAgentText, _user1Name, _user2Name, _user3Name, _user4Name, _user5Name, _user1NameLegal, _user2NameLegal, _user3NameLegal, _user4NameLegal, _user5NameLegal,
        _user1Phone1, _user1Phone2, _user2Phone1, _user2Phone2, _petNames, _neighbor1Location, _neighbor1Name, _neighbor1Phone1, _neighbor1Phone2, _neighbor2Location,
        _neighbor2Name, _neighbor2Phone1, _neighbor2Phone2, _addressLine1, _addressLine2, _fireExtinguisherLocation, _hospitalAddressLine1, _hospitalAddressLine2, _wifiGuestName,
        _wifiGuestPassword, _wifiPrivateName, _wifiPrivatePassword, _policeName, _policePhone, _emergencyContact1Name, _emergencyContact1Phone1, _emergencyContact1Phone2,
        _emergencyContact2Name, _emergencyContact2Phone1, _emergencyContact2Phone2, _alarmCode, _comPort, _trashDaySelected;

    private bool _valueImperialChecked, _valueMetricChecked, _isEditTasksMode, _isNormalMode, _isDebugMode;

    public SettingsVM() {
        TrashDayList = new List<string>(new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "DISABLED" });
        TrashDaySelected = "Wednesday";

        UserAgentText = ReferenceValues.JsonMasterSettings.UserAgent;
        User1Name = ReferenceValues.JsonMasterSettings.User1Name;
        User2Name = ReferenceValues.JsonMasterSettings.User2Name;
        User3Name = ReferenceValues.JsonMasterSettings.User3Name;
        User4Name = ReferenceValues.JsonMasterSettings.User4Name;
        User5Name = ReferenceValues.JsonMasterSettings.User5Name;
        User1NameLegal = ReferenceValues.JsonMasterSettings.User1NameLegal;
        User2NameLegal = ReferenceValues.JsonMasterSettings.User2NameLegal;
        User3NameLegal = ReferenceValues.JsonMasterSettings.User3NameLegal;
        User4NameLegal = ReferenceValues.JsonMasterSettings.User4NameLegal;
        User5NameLegal = ReferenceValues.JsonMasterSettings.User5NameLegal;
        User1Phone1 = ReferenceValues.JsonMasterSettings.User1Phone1;
        User1Phone2 = ReferenceValues.JsonMasterSettings.User1Phone2;
        User2Phone1 = ReferenceValues.JsonMasterSettings.User2Phone1;
        User2Phone2 = ReferenceValues.JsonMasterSettings.User2Phone2;
        PetNames = ReferenceValues.JsonMasterSettings.PetNames;
        Neighbor1Location = ReferenceValues.JsonMasterSettings.Neighbor1Location;
        Neighbor1Name = ReferenceValues.JsonMasterSettings.Neighbor1Name;
        Neighbor1Phone1 = ReferenceValues.JsonMasterSettings.Neighbor1Phone1;
        Neighbor1Phone2 = ReferenceValues.JsonMasterSettings.Neighbor1Phone2;
        Neighbor2Location = ReferenceValues.JsonMasterSettings.Neighbor2Location;
        Neighbor2Name = ReferenceValues.JsonMasterSettings.Neighbor2Name;
        Neighbor2Phone1 = ReferenceValues.JsonMasterSettings.Neighbor2Phone1;
        Neighbor2Phone2 = ReferenceValues.JsonMasterSettings.Neighbor2Phone2;
        AddressLine1 = ReferenceValues.JsonMasterSettings.AddressLine1;
        AddressLine2 = ReferenceValues.JsonMasterSettings.AddressLine2;
        FireExtinguisherLocation = ReferenceValues.JsonMasterSettings.FireExtinguisherLocation;
        HospitalAddressLine1 = ReferenceValues.JsonMasterSettings.HospitalAddressLine1;
        HospitalAddressLine2 = ReferenceValues.JsonMasterSettings.HospitalAddressLine2;
        WifiGuestName = ReferenceValues.JsonMasterSettings.WifiGuestName;
        WifiGuestPassword = ReferenceValues.JsonMasterSettings.WifiGuestPassword;
        WifiPrivateName = ReferenceValues.JsonMasterSettings.WifiPrivateName;
        WifiPrivatePassword = ReferenceValues.JsonMasterSettings.WifiPrivatePassword;
        PoliceName = ReferenceValues.JsonMasterSettings.PoliceName;
        PolicePhone = ReferenceValues.JsonMasterSettings.PolicePhone;
        EmergencyContact1Name = ReferenceValues.JsonMasterSettings.EmergencyContact1Name;
        EmergencyContact1Phone1 = ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1;
        EmergencyContact1Phone2 = ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2;
        EmergencyContact2Name = ReferenceValues.JsonMasterSettings.EmergencyContact2Name;
        EmergencyContact2Phone1 = ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1;
        EmergencyContact2Phone2 = ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2;
        AlarmCode = ReferenceValues.JsonMasterSettings.AlarmCode;
        ComPort = ReferenceValues.JsonMasterSettings.ComPort;
        ValueImperialChecked = ReferenceValues.JsonMasterSettings.IsImperialMode;
        ValueMetricChecked = !ValueImperialChecked;
        IsNormalMode = ReferenceValues.JsonMasterSettings.IsNormalMode;
        IsEditTasksMode = ReferenceValues.JsonMasterSettings.IsEditTasksMode;
        IsDebugMode = ReferenceValues.JsonMasterSettings.IsDebugMode;
        TrashDaySelected = ReferenceValues.JsonMasterSettings.TrashDay;

        if (!IsNormalMode && !IsEditTasksMode && !IsDebugMode) {
            IsNormalMode = true;
        }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "save":
            if (!string.IsNullOrEmpty(UserAgentText) || !string.IsNullOrEmpty(User1Name) || !string.IsNullOrEmpty(User2Name)) {
                ReferenceValues.JsonMasterSettings.UserAgent = UserAgentText;
                ReferenceValues.JsonMasterSettings.User1Name = User1Name;
                ReferenceValues.JsonMasterSettings.User2Name = User2Name;
                ReferenceValues.JsonMasterSettings.User3Name = User3Name;
                ReferenceValues.JsonMasterSettings.User4Name = User4Name;
                ReferenceValues.JsonMasterSettings.User5Name = User5Name;
                ReferenceValues.JsonMasterSettings.User1NameLegal = User1NameLegal;
                ReferenceValues.JsonMasterSettings.User2NameLegal = User2NameLegal;
                ReferenceValues.JsonMasterSettings.User3NameLegal = User3NameLegal;
                ReferenceValues.JsonMasterSettings.User4NameLegal = User4NameLegal;
                ReferenceValues.JsonMasterSettings.User5NameLegal = User5NameLegal;
                ReferenceValues.JsonMasterSettings.User1Phone1 = User1Phone1;
                ReferenceValues.JsonMasterSettings.User1Phone2 = User1Phone2;
                ReferenceValues.JsonMasterSettings.User2Phone1 = User2Phone1;
                ReferenceValues.JsonMasterSettings.User2Phone2 = User2Phone2;
                ReferenceValues.JsonMasterSettings.PetNames = PetNames;
                ReferenceValues.JsonMasterSettings.Neighbor1Location = Neighbor1Location;
                ReferenceValues.JsonMasterSettings.Neighbor1Name = Neighbor1Name;
                ReferenceValues.JsonMasterSettings.Neighbor1Phone1 = Neighbor1Phone1;
                ReferenceValues.JsonMasterSettings.Neighbor1Phone2 = Neighbor1Phone2;
                ReferenceValues.JsonMasterSettings.Neighbor2Location = Neighbor2Location;
                ReferenceValues.JsonMasterSettings.Neighbor2Name = Neighbor2Name;
                ReferenceValues.JsonMasterSettings.Neighbor2Phone1 = Neighbor2Phone1;
                ReferenceValues.JsonMasterSettings.Neighbor2Phone2 = Neighbor2Phone2;
                ReferenceValues.JsonMasterSettings.AddressLine1 = AddressLine1;
                ReferenceValues.JsonMasterSettings.AddressLine2 = AddressLine2;
                ReferenceValues.JsonMasterSettings.FireExtinguisherLocation = FireExtinguisherLocation;
                ReferenceValues.JsonMasterSettings.HospitalAddressLine1 = HospitalAddressLine1;
                ReferenceValues.JsonMasterSettings.HospitalAddressLine2 = HospitalAddressLine2;
                ReferenceValues.JsonMasterSettings.WifiGuestName = WifiGuestName;
                ReferenceValues.JsonMasterSettings.WifiGuestPassword = WifiGuestPassword;
                ReferenceValues.JsonMasterSettings.WifiPrivateName = WifiPrivateName;
                ReferenceValues.JsonMasterSettings.WifiPrivatePassword = WifiPrivatePassword;
                ReferenceValues.JsonMasterSettings.PoliceName = PoliceName;
                ReferenceValues.JsonMasterSettings.PolicePhone = PolicePhone;
                ReferenceValues.JsonMasterSettings.EmergencyContact1Name = EmergencyContact1Name;
                ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1 = EmergencyContact1Phone1;
                ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2 = EmergencyContact1Phone2;
                ReferenceValues.JsonMasterSettings.EmergencyContact2Name = EmergencyContact2Name;
                ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1 = EmergencyContact2Phone1;
                ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2 = EmergencyContact2Phone2;
                ReferenceValues.JsonMasterSettings.AlarmCode = AlarmCode;
                ReferenceValues.JsonMasterSettings.ComPort = ComPort;
                ReferenceValues.JsonMasterSettings.IsImperialMode = ValueImperialChecked;
                ValueMetricChecked = !ValueImperialChecked;
                ReferenceValues.JsonMasterSettings.IsNormalMode = IsNormalMode;
                ReferenceValues.JsonMasterSettings.IsEditTasksMode = IsEditTasksMode;
                ReferenceValues.JsonMasterSettings.IsDebugMode = IsDebugMode;
                ReferenceValues.JsonMasterSettings.TrashDay = TrashDaySelected;

                try {
                    string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonMasterSettings);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "settings.json", jsonString);
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "SettingsVM",
                        Description = e.ToString()
                    });
                    SaveDebugFile.Save();
                }
            } else {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            }

            break;
        }
    }

    #region Fields

    public string UserAgentText {
        get => _userAgentText;
        set {
            _userAgentText = value;
            RaisePropertyChangedEvent("UserAgentText");
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

    #endregion
}