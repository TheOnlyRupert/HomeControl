using System;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class ContactsVM : BaseViewModel {
    private string _user1Name, _user2Name,
        _user1Phone1,
        _user1Phone2,
        _user2Phone1,
        _user2Phone2,
        _childrenNames,
        _petNames,
        _neighbor1Location,
        _neighbor1Name,
        _neighbor1Phone1,
        _neighbor1Phone2,
        _neighbor2Location,
        _neighbor2Name,
        _neighbor2Phone1,
        _neighbor2Phone2,
        _addressLine1,
        _addressLine2,
        _fireExtinguisherLocation,
        _hospitalAddressLine1,
        _hospitalAddressLine2,
        _wifiGuestName,
        _wifiGuestPassword,
        _wifiPrivateName,
        _wifiPrivatePassword,
        _policeName,
        _policePhone,
        _emergencyContact1Name,
        _emergencyContact1Phone1,
        _emergencyContact1Phone2,
        _emergencyContact2Name,
        _emergencyContact2Phone1,
        _emergencyContact2Phone2,
        _alarmCode, _privateWifiVisibility;

    public ContactsVM() {
        PrivateWifiVisibility = ReferenceValues.LockUI ? "HIDDEN" : "VISIBLE";

        try {
            User1Name = ReferenceValues.JsonMasterSettings.User1NameLegal;
            try {
                User1Phone1 = "(" + ReferenceValues.JsonMasterSettings.User1Phone1[0] + ReferenceValues.JsonMasterSettings.User1Phone1[1] +
                              ReferenceValues.JsonMasterSettings.User1Phone1[2] + ") " + ReferenceValues.JsonMasterSettings.User1Phone1[3] +
                              ReferenceValues.JsonMasterSettings.User1Phone1[4] + ReferenceValues.JsonMasterSettings.User1Phone1[5] + "-" +
                              ReferenceValues.JsonMasterSettings.User1Phone1[6] + ReferenceValues.JsonMasterSettings.User1Phone1[7] +
                              ReferenceValues.JsonMasterSettings.User1Phone1[8] + ReferenceValues.JsonMasterSettings.User1Phone1[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            try {
                User1Phone2 = "(" + ReferenceValues.JsonMasterSettings.User1Phone2[0] + ReferenceValues.JsonMasterSettings.User1Phone2[1] +
                              ReferenceValues.JsonMasterSettings.User1Phone2[2] + ") " + ReferenceValues.JsonMasterSettings.User1Phone2[3] +
                              ReferenceValues.JsonMasterSettings.User1Phone2[4] + ReferenceValues.JsonMasterSettings.User1Phone2[5] + "-" +
                              ReferenceValues.JsonMasterSettings.User1Phone2[6] + ReferenceValues.JsonMasterSettings.User1Phone2[7] +
                              ReferenceValues.JsonMasterSettings.User1Phone2[8] + ReferenceValues.JsonMasterSettings.User1Phone2[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            User2Name = ReferenceValues.JsonMasterSettings.User2NameLegal;
            try {
                User2Phone1 = "(" + ReferenceValues.JsonMasterSettings.User2Phone1[0] + ReferenceValues.JsonMasterSettings.User2Phone1[1] +
                              ReferenceValues.JsonMasterSettings.User2Phone1[2] + ") " + ReferenceValues.JsonMasterSettings.User2Phone1[3] +
                              ReferenceValues.JsonMasterSettings.User2Phone1[4] + ReferenceValues.JsonMasterSettings.User2Phone1[5] + "-" +
                              ReferenceValues.JsonMasterSettings.User2Phone1[6] + ReferenceValues.JsonMasterSettings.User2Phone1[7] +
                              ReferenceValues.JsonMasterSettings.User2Phone1[8] + ReferenceValues.JsonMasterSettings.User2Phone1[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            try {
                User2Phone2 = "(" + ReferenceValues.JsonMasterSettings.User2Phone2[0] + ReferenceValues.JsonMasterSettings.User2Phone2[1] +
                              ReferenceValues.JsonMasterSettings.User2Phone2[2] + ") " + ReferenceValues.JsonMasterSettings.User2Phone2[3] +
                              ReferenceValues.JsonMasterSettings.User2Phone2[4] + ReferenceValues.JsonMasterSettings.User2Phone2[5] + "-" +
                              ReferenceValues.JsonMasterSettings.User2Phone2[6] + ReferenceValues.JsonMasterSettings.User2Phone2[7] +
                              ReferenceValues.JsonMasterSettings.User2Phone2[8] + ReferenceValues.JsonMasterSettings.User2Phone2[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            ChildrenNames = ReferenceValues.JsonMasterSettings.User3NameLegal + ", " + ReferenceValues.JsonMasterSettings.User4NameLegal + ", " +
                            ReferenceValues.JsonMasterSettings.User5NameLegal;
            PetNames = ReferenceValues.JsonMasterSettings.PetNames;
            Neighbor1Location = ReferenceValues.JsonMasterSettings.Neighbor1Location;
            Neighbor1Name = ReferenceValues.JsonMasterSettings.Neighbor1Name;
            try {
                Neighbor1Phone1 = "(" + ReferenceValues.JsonMasterSettings.Neighbor1Phone1[0] + ReferenceValues.JsonMasterSettings.Neighbor1Phone1[1] +
                                  ReferenceValues.JsonMasterSettings.Neighbor1Phone1[2] + ") " + ReferenceValues.JsonMasterSettings.Neighbor1Phone1[3] +
                                  ReferenceValues.JsonMasterSettings.Neighbor1Phone1[4] + ReferenceValues.JsonMasterSettings.Neighbor1Phone1[5] + "-" +
                                  ReferenceValues.JsonMasterSettings.Neighbor1Phone1[6] + ReferenceValues.JsonMasterSettings.Neighbor1Phone1[7] +
                                  ReferenceValues.JsonMasterSettings.Neighbor1Phone1[8] + ReferenceValues.JsonMasterSettings.Neighbor1Phone1[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            try {
                Neighbor1Phone2 = "(" + ReferenceValues.JsonMasterSettings.Neighbor1Phone2[0] + ReferenceValues.JsonMasterSettings.Neighbor1Phone2[1] +
                                  ReferenceValues.JsonMasterSettings.Neighbor1Phone2[2] + ") " + ReferenceValues.JsonMasterSettings.Neighbor1Phone2[3] +
                                  ReferenceValues.JsonMasterSettings.Neighbor1Phone2[4] + ReferenceValues.JsonMasterSettings.Neighbor1Phone2[5] + "-" +
                                  ReferenceValues.JsonMasterSettings.Neighbor1Phone2[6] + ReferenceValues.JsonMasterSettings.Neighbor1Phone2[7] +
                                  ReferenceValues.JsonMasterSettings.Neighbor1Phone2[8] + ReferenceValues.JsonMasterSettings.Neighbor1Phone2[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            Neighbor2Location = ReferenceValues.JsonMasterSettings.Neighbor2Location;
            Neighbor2Name = ReferenceValues.JsonMasterSettings.Neighbor2Name;
            try {
                Neighbor2Phone1 = "(" + ReferenceValues.JsonMasterSettings.Neighbor2Phone1[0] + ReferenceValues.JsonMasterSettings.Neighbor2Phone1[1] +
                                  ReferenceValues.JsonMasterSettings.Neighbor2Phone1[2] + ") " + ReferenceValues.JsonMasterSettings.Neighbor2Phone1[3] +
                                  ReferenceValues.JsonMasterSettings.Neighbor2Phone1[4] + ReferenceValues.JsonMasterSettings.Neighbor2Phone1[5] + "-" +
                                  ReferenceValues.JsonMasterSettings.Neighbor2Phone1[6] + ReferenceValues.JsonMasterSettings.Neighbor2Phone1[7] +
                                  ReferenceValues.JsonMasterSettings.Neighbor2Phone1[8] + ReferenceValues.JsonMasterSettings.Neighbor2Phone1[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            try {
                Neighbor2Phone2 = "(" + ReferenceValues.JsonMasterSettings.Neighbor2Phone2[0] + ReferenceValues.JsonMasterSettings.Neighbor2Phone2[1] +
                                  ReferenceValues.JsonMasterSettings.Neighbor2Phone2[2] + ") " + ReferenceValues.JsonMasterSettings.Neighbor2Phone2[3] +
                                  ReferenceValues.JsonMasterSettings.Neighbor2Phone2[4] + ReferenceValues.JsonMasterSettings.Neighbor2Phone2[5] + "-" +
                                  ReferenceValues.JsonMasterSettings.Neighbor2Phone2[6] + ReferenceValues.JsonMasterSettings.Neighbor2Phone2[7] +
                                  ReferenceValues.JsonMasterSettings.Neighbor2Phone2[8] + ReferenceValues.JsonMasterSettings.Neighbor2Phone2[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

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
            try {
                PolicePhone = "(" + ReferenceValues.JsonMasterSettings.PolicePhone[0] + ReferenceValues.JsonMasterSettings.PolicePhone[1] +
                              ReferenceValues.JsonMasterSettings.PolicePhone[2] + ") " + ReferenceValues.JsonMasterSettings.PolicePhone[3] +
                              ReferenceValues.JsonMasterSettings.PolicePhone[4] + ReferenceValues.JsonMasterSettings.PolicePhone[5] + "-" +
                              ReferenceValues.JsonMasterSettings.PolicePhone[6] + ReferenceValues.JsonMasterSettings.PolicePhone[7] +
                              ReferenceValues.JsonMasterSettings.PolicePhone[8] + ReferenceValues.JsonMasterSettings.PolicePhone[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            EmergencyContact1Name = ReferenceValues.JsonMasterSettings.EmergencyContact1Name;
            try {
                EmergencyContact1Phone1 = "(" + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[0] + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[1] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[2] + ") " + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[3] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[4] + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[5] + "-" +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[6] + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[7] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[8] + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone1[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            try {
                EmergencyContact1Phone2 = "(" + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[0] + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[1] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[2] + ") " + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[3] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[4] + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[5] + "-" +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[6] + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[7] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[8] + ReferenceValues.JsonMasterSettings.EmergencyContact1Phone2[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            EmergencyContact2Name = ReferenceValues.JsonMasterSettings.EmergencyContact2Name;
            try {
                EmergencyContact2Phone1 = "(" + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[0] + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[1] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[2] + ") " + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[3] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[4] + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[5] + "-" +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[6] + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[7] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[8] + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone1[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            try {
                EmergencyContact2Phone2 = "(" + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[0] + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[1] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[2] + ") " + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[3] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[4] + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[5] + "-" +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[6] + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[7] +
                                          ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[8] + ReferenceValues.JsonMasterSettings.EmergencyContact2Phone2[9];
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ContactsVM",
                    Description = e.ToString()
                });
            }

            AlarmCode = ReferenceValues.JsonMasterSettings.AlarmCode;
            WifiPrivateName = ReferenceValues.JsonMasterSettings.WifiPrivateName;
            WifiPrivatePassword = ReferenceValues.JsonMasterSettings.WifiPrivatePassword;
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "ContactsVM",
                Description = e.ToString()
            });
        }
    }

    #region Fields

    public string User1Name {
        get => _user1Name;
        set {
            _user1Name = value;
            RaisePropertyChangedEvent("User1Name");
        }
    }

    public string User1Phone1 {
        get => _user1Phone1;
        set {
            _user1Phone1 = value;
            RaisePropertyChangedEvent("User1Phone1");
        }
    }

    public string User1Phone2 {
        get => _user1Phone2;
        set {
            _user1Phone2 = value;
            RaisePropertyChangedEvent("User1Phone2");
        }
    }

    public string User2Name {
        get => _user2Name;
        set {
            _user2Name = value;
            RaisePropertyChangedEvent("User2Name");
        }
    }

    public string User2Phone1 {
        get => _user2Phone1;
        set {
            _user2Phone1 = value;
            RaisePropertyChangedEvent("User2Phone1");
        }
    }

    public string User2Phone2 {
        get => _user2Phone2;
        set {
            _user2Phone2 = value;
            RaisePropertyChangedEvent("User2Phone2");
        }
    }

    public string ChildrenNames {
        get => _childrenNames;
        set {
            _childrenNames = value;
            RaisePropertyChangedEvent("ChildrenNames");
        }
    }

    public string PetNames {
        get => _petNames;
        set {
            _petNames = value;
            RaisePropertyChangedEvent("PetNames");
        }
    }

    public string Neighbor1Location {
        get => _neighbor1Location;
        set {
            _neighbor1Location = value;
            RaisePropertyChangedEvent("Neighbor1Location");
        }
    }

    public string Neighbor1Name {
        get => _neighbor1Name;
        set {
            _neighbor1Name = value;
            RaisePropertyChangedEvent("Neighbor1Name");
        }
    }

    public string Neighbor1Phone1 {
        get => _neighbor1Phone1;
        set {
            _neighbor1Phone1 = value;
            RaisePropertyChangedEvent("Neighbor1Phone1");
        }
    }

    public string Neighbor1Phone2 {
        get => _neighbor1Phone2;
        set {
            _neighbor1Phone2 = value;
            RaisePropertyChangedEvent("Neighbor1Phone2");
        }
    }

    public string Neighbor2Location {
        get => _neighbor2Location;
        set {
            _neighbor2Location = value;
            RaisePropertyChangedEvent("Neighbor2Location");
        }
    }

    public string Neighbor2Name {
        get => _neighbor2Name;
        set {
            _neighbor2Name = value;
            RaisePropertyChangedEvent("Neighbor2Name");
        }
    }

    public string Neighbor2Phone1 {
        get => _neighbor2Phone1;
        set {
            _neighbor2Phone1 = value;
            RaisePropertyChangedEvent("Neighbor2Phone1");
        }
    }

    public string Neighbor2Phone2 {
        get => _neighbor2Phone2;
        set {
            _neighbor2Phone2 = value;
            RaisePropertyChangedEvent("Neighbor2Phone2");
        }
    }

    public string AddressLine1 {
        get => _addressLine1;
        set {
            _addressLine1 = value;
            RaisePropertyChangedEvent("AddressLine1");
        }
    }

    public string AddressLine2 {
        get => _addressLine2;
        set {
            _addressLine2 = value;
            RaisePropertyChangedEvent("AddressLine2");
        }
    }

    public string FireExtinguisherLocation {
        get => _fireExtinguisherLocation;
        set {
            _fireExtinguisherLocation = value;
            RaisePropertyChangedEvent("FireExtinguisherLocation");
        }
    }

    public string HospitalAddressLine1 {
        get => _hospitalAddressLine1;
        set {
            _hospitalAddressLine1 = value;
            RaisePropertyChangedEvent("HospitalAddressLine1");
        }
    }

    public string HospitalAddressLine2 {
        get => _hospitalAddressLine2;
        set {
            _hospitalAddressLine2 = value;
            RaisePropertyChangedEvent("HospitalAddressLine2");
        }
    }

    public string WifiGuestName {
        get => _wifiGuestName;
        set {
            _wifiGuestName = value;
            RaisePropertyChangedEvent("WifiGuestName");
        }
    }

    public string WifiGuestPassword {
        get => _wifiGuestPassword;
        set {
            _wifiGuestPassword = value;
            RaisePropertyChangedEvent("WifiGuestPassword");
        }
    }

    public string WifiPrivateName {
        get => _wifiPrivateName;
        set {
            _wifiPrivateName = value;
            RaisePropertyChangedEvent("WifiPrivateName");
        }
    }

    public string WifiPrivatePassword {
        get => _wifiPrivatePassword;
        set {
            _wifiPrivatePassword = value;
            RaisePropertyChangedEvent("WifiPrivatePassword");
        }
    }

    public string PoliceName {
        get => _policeName;
        set {
            _policeName = value;
            RaisePropertyChangedEvent("PoliceName");
        }
    }

    public string PolicePhone {
        get => _policePhone;
        set {
            _policePhone = value;
            RaisePropertyChangedEvent("PolicePhone");
        }
    }

    public string EmergencyContact1Name {
        get => _emergencyContact1Name;
        set {
            _emergencyContact1Name = value;
            RaisePropertyChangedEvent("EmergencyContact1Name");
        }
    }

    public string EmergencyContact1Phone1 {
        get => _emergencyContact1Phone1;
        set {
            _emergencyContact1Phone1 = value;
            RaisePropertyChangedEvent("EmergencyContact1Phone1");
        }
    }

    public string EmergencyContact1Phone2 {
        get => _emergencyContact1Phone2;
        set {
            _emergencyContact1Phone2 = value;
            RaisePropertyChangedEvent("EmergencyContact1Phone2");
        }
    }

    public string EmergencyContact2Name {
        get => _emergencyContact2Name;
        set {
            _emergencyContact2Name = value;
            RaisePropertyChangedEvent("EmergencyContact2Name");
        }
    }

    public string EmergencyContact2Phone1 {
        get => _emergencyContact2Phone1;
        set {
            _emergencyContact2Phone1 = value;
            RaisePropertyChangedEvent("EmergencyContact2Phone1");
        }
    }

    public string EmergencyContact2Phone2 {
        get => _emergencyContact2Phone2;
        set {
            _emergencyContact2Phone2 = value;
            RaisePropertyChangedEvent("EmergencyContact2Phone2");
        }
    }

    public string AlarmCode {
        get => _alarmCode;
        set {
            _alarmCode = value;
            RaisePropertyChangedEvent("AlarmCode");
        }
    }

    public string PrivateWifiVisibility {
        get => _privateWifiVisibility;
        set {
            _privateWifiVisibility = value;
            RaisePropertyChangedEvent("PrivateWifiVisibility");
        }
    }

    #endregion
}