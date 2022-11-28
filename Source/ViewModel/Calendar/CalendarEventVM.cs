using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class CalendarEventVM : BaseViewModel {
    private readonly JsonCalendar _jsonCalendar;

    private readonly string fileName;
    private CalendarEvents _calendarEventSelected;

    private string _eventDate, _eventText, _locationText, _descriptionText, _user1BackgroundColor, _user2BackgroundColor, _childrenBackgroundColor, _homeBackgroundColor,
        _otherBackgroundColor, selectedPerson, _user1NameText, _user2NameText;

    private ObservableCollection<CalendarEvents> _eventList;

    public CalendarEventVM() {
        EventDate = ReferenceValues.CalendarEventDate.ToLongDateString();
        fileName = ReferenceValues.FILE_DIRECTORY + "events/" + ReferenceValues.CalendarEventDate.ToString("yyyy_MM_dd") + ".json";
        EventList = new ObservableCollection<CalendarEvents>();
        EventText = "";
        _jsonCalendar = new JsonCalendar();
        selectedPerson = "Home";
        User1NameText = ReferenceValues.User1Name;
        User2NameText = ReferenceValues.User2Name;
        CalendarEventSelected = new CalendarEvents();
        UserButtonLogic();

        if (File.Exists(fileName)) {
            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            try {
                StreamReader streamReader = new(fileName);
                string eventsListString = null;
                while (!streamReader.EndOfStream) {
                    eventsListString = streamReader.ReadToEnd();
                }

                if (eventsListString != null) {
                    try {
                        JsonCalendar currentJsonCalendar = JsonSerializer.Deserialize<JsonCalendar>(eventsListString, options);

                        if (currentJsonCalendar != null) {
                            EventList = currentJsonCalendar.eventsList;
                        }
                    } catch (Exception e) {
                        Console.WriteLine(e);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void PopulateDetailedView(CalendarEvents value) {
        EventText = value.name;
        LocationText = value.location;
        DescriptionText = value.description;

        if (value.person == ReferenceValues.User1Name) {
            selectedPerson = ReferenceValues.User1Name;
        } else if (value.person == ReferenceValues.User2Name) {
            selectedPerson = ReferenceValues.User2Name;
        } else if (string.IsNullOrEmpty(value.person)) {
            selectedPerson = "Home";
        } else {
            selectedPerson = value.person;
        }

        UserButtonLogic();
    }

    private void UserButtonLogic() {
        User1BackgroundColor = "Transparent";
        User2BackgroundColor = "Transparent";
        ChildrenBackgroundColor = "Transparent";
        HomeBackgroundColor = "Transparent";
        OtherBackgroundColor = "Transparent";

        if (selectedPerson == ReferenceValues.User1Name) {
            User1BackgroundColor = "Green";
        } else if (selectedPerson == ReferenceValues.User2Name) {
            User2BackgroundColor = "Green";
        } else {
            switch (selectedPerson) {
            case "Children":
                ChildrenBackgroundColor = "Green";
                break;
            case "Home":
                HomeBackgroundColor = "Green";
                break;
            default:
                OtherBackgroundColor = "Green";
                break;
            }
        }
    }

    private void ButtonLogic(object param) {
        MessageBoxResult confirmation;

        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(EventText)) {
                MessageBox.Show("Missing Event", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                EventList.Add(new CalendarEvents {
                    name = EventText,
                    description = DescriptionText,
                    location = LocationText,
                    person = selectedPerson
                });

                EventText = "";
                DescriptionText = "";
                LocationText = "";

                SaveJson();
            }

            break;
        case "update":
            try {
                if (CalendarEventSelected.name != null) {
                    if (string.IsNullOrWhiteSpace(EventText)) {
                        MessageBox.Show("Event needs a name.", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                    } else if (!string.IsNullOrWhiteSpace(CalendarEventSelected.name)) {
                        confirmation = MessageBox.Show("Are you sure you want to update event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (confirmation == MessageBoxResult.Yes) {
                            EventList.Insert(EventList.IndexOf(CalendarEventSelected), new CalendarEvents {
                                name = EventText,
                                description = DescriptionText,
                                location = LocationText,
                                person = selectedPerson
                            });
                            EventList.Remove(CalendarEventSelected);

                            EventText = "";
                            DescriptionText = "";
                            LocationText = "";

                            SaveJson();
                        }
                    }
                }
            } catch (Exception) { }

            break;
        case "delete":
            try {
                if (CalendarEventSelected.name != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmation == MessageBoxResult.Yes) {
                        EventList.Remove(CalendarEventSelected);

                        SaveJson();
                    }
                }
            } catch (Exception) { }

            break;

        case "user1":
            selectedPerson = ReferenceValues.User1Name;
            UserButtonLogic();
            break;

        case "user2":
            selectedPerson = ReferenceValues.User2Name;
            UserButtonLogic();
            break;

        case "children":
            selectedPerson = "Children";
            UserButtonLogic();
            break;

        case "home":
            selectedPerson = "Home";
            UserButtonLogic();
            break;

        case "other":
            selectedPerson = "Other";
            UserButtonLogic();
            break;
        }
    }

    private void SaveJson() {
        if (EventList.Count > 0) {
            try {
                _jsonCalendar.eventsList = EventList;
                string jsonString = JsonSerializer.Serialize(_jsonCalendar);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
            }
        } else {
            try {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(fileName);
            } catch (Exception e) {
                Console.WriteLine("Unable to delete " + fileName + "... " + e.Message);
            }
        }
    }

    #region Fields

    public string EventDate {
        get => _eventDate;
        set {
            _eventDate = value;
            RaisePropertyChangedEvent("EventDate");
        }
    }

    public string EventText {
        get => _eventText;
        set {
            _eventText = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("EventText");
        }
    }

    public string LocationText {
        get => _locationText;
        set {
            _locationText = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("LocationText");
        }
    }

    public string DescriptionText {
        get => _descriptionText;
        set {
            _descriptionText = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("DescriptionText");
        }
    }

    public ObservableCollection<CalendarEvents> EventList {
        get => _eventList;
        set {
            _eventList = value;
            RaisePropertyChangedEvent("EventList");
        }
    }

    public CalendarEvents CalendarEventSelected {
        get => _calendarEventSelected;
        set {
            _calendarEventSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("EventSelected");
        }
    }

    public string User1BackgroundColor {
        get => _user1BackgroundColor;
        set {
            _user1BackgroundColor = value;
            RaisePropertyChangedEvent("User1BackgroundColor");
        }
    }

    public string User2BackgroundColor {
        get => _user2BackgroundColor;
        set {
            _user2BackgroundColor = value;
            RaisePropertyChangedEvent("User2BackgroundColor");
        }
    }

    public string ChildrenBackgroundColor {
        get => _childrenBackgroundColor;
        set {
            _childrenBackgroundColor = value;
            RaisePropertyChangedEvent("ChildrenBackgroundColor");
        }
    }

    public string HomeBackgroundColor {
        get => _homeBackgroundColor;
        set {
            _homeBackgroundColor = value;
            RaisePropertyChangedEvent("HomeBackgroundColor");
        }
    }

    public string OtherBackgroundColor {
        get => _otherBackgroundColor;
        set {
            _otherBackgroundColor = value;
            RaisePropertyChangedEvent("OtherBackgroundColor");
        }
    }

    public string User1NameText {
        get => _user1NameText;
        set {
            _user1NameText = value;
            RaisePropertyChangedEvent("User1NameText");
        }
    }

    public string User2NameText {
        get => _user2NameText;
        set {
            _user2NameText = value;
            RaisePropertyChangedEvent("User2NameText");
        }
    }

    #endregion
}