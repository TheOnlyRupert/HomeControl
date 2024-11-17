using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class ContactsVM : BaseViewModel {
    // ReSharper disable UnusedMember.Global
    private static string FormatPhone(string phone) {
        if (string.IsNullOrEmpty(phone) || phone.Length != 10) return phone;
        return $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6)}";
    }

    public string User1Name { get; set; } = ReferenceValues.JsonSettingsMaster.User1NameLegal;
    public string User1Phone1 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.User1Phone1);
    public string User1Phone2 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.User1Phone2);
    public string User2Name { get; set; } = ReferenceValues.JsonSettingsMaster.User2NameLegal;
    public string User2Phone1 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.User2Phone1);
    public string User2Phone2 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.User2Phone2);

    public string ChildrenNames { get; set; } = string.Join(", ", ReferenceValues.JsonSettingsMaster.User3NameLegal,
        ReferenceValues.JsonSettingsMaster.User4NameLegal,
        ReferenceValues.JsonSettingsMaster.User5NameLegal);

    public string PetNames { get; set; } = ReferenceValues.JsonSettingsMaster.PetNames;
    public string Neighbor1Location { get; set; } = ReferenceValues.JsonSettingsMaster.Neighbor1Location;
    public string Neighbor1Name { get; set; } = ReferenceValues.JsonSettingsMaster.Neighbor1Name;
    public string Neighbor1Phone1 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.Neighbor1Phone1);
    public string Neighbor1Phone2 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.Neighbor1Phone2);
    public string Neighbor2Location { get; set; } = ReferenceValues.JsonSettingsMaster.Neighbor2Location;
    public string Neighbor2Name { get; set; } = ReferenceValues.JsonSettingsMaster.Neighbor2Name;
    public string Neighbor2Phone1 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.Neighbor2Phone1);
    public string Neighbor2Phone2 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.Neighbor2Phone2);
    public string AddressLine1 { get; set; } = ReferenceValues.JsonSettingsMaster.AddressLine1;
    public string AddressLine2 { get; set; } = ReferenceValues.JsonSettingsMaster.AddressLine2;
    public string FireExtinguisherLocation { get; set; } = ReferenceValues.JsonSettingsMaster.FireExtinguisherLocation;
    public string HospitalAddressLine1 { get; set; } = ReferenceValues.JsonSettingsMaster.HospitalAddressLine1;
    public string HospitalAddressLine2 { get; set; } = ReferenceValues.JsonSettingsMaster.HospitalAddressLine2;
    public string WifiGuestName { get; set; } = ReferenceValues.JsonSettingsMaster.WifiGuestName;
    public string WifiGuestPassword { get; set; } = ReferenceValues.JsonSettingsMaster.WifiGuestPassword;
    public string WifiPrivateName { get; set; } = ReferenceValues.JsonSettingsMaster.WifiPrivateName;
    public string WifiPrivatePassword { get; set; } = ReferenceValues.JsonSettingsMaster.WifiPrivatePassword;
    public string PoliceName { get; set; } = ReferenceValues.JsonSettingsMaster.PoliceName;
    public string PolicePhone { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.PolicePhone);
    public string EmergencyContact1Name { get; set; } = ReferenceValues.JsonSettingsMaster.EmergencyContact1Name;
    public string EmergencyContact1Phone1 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.EmergencyContact1Phone1);
    public string EmergencyContact1Phone2 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.EmergencyContact1Phone2);
    public string EmergencyContact2Name { get; set; } = ReferenceValues.JsonSettingsMaster.EmergencyContact2Name;
    public string EmergencyContact2Phone1 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.EmergencyContact2Phone1);
    public string EmergencyContact2Phone2 { get; set; } = FormatPhone(ReferenceValues.JsonSettingsMaster.EmergencyContact2Phone2);
    public string AlarmCode { get; set; } = ReferenceValues.JsonSettingsMaster.AlarmCode;
    public string PrivateWifiVisibility { get; set; } = ReferenceValues.LockUi ? "HIDDEN" : "VISIBLE";
}