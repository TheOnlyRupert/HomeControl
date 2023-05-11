using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class EditCalendarVM : BaseViewModel {
    private readonly JsonCalendar _jsonCalendar;
    private readonly string fileName;
    private CalendarEvents _calendarEventSelected;

    private string _eventDate, _eventText, _locationText, _descriptionText, _user1BackgroundColor, _user2BackgroundColor, _childrenBackgroundColor, _homeBackgroundColor,
        _otherBackgroundColor, selectedPerson, _user1NameText, _user2NameText, _startTimeText, _endTimeText, _parentsBackgroundColor, _dupeButtonBackgroundColor, _dupeText;

    private ObservableCollection<CalendarEvents> _eventList;

    public EditCalendarVM() {
        EventDate = ReferenceValues.CalendarEventDate.ToLongDateString();
        fileName = ReferenceValues.FILE_DIRECTORY + "events/" + ReferenceValues.CalendarEventDate.ToString("yyyy_MM_dd") + ".json";
        EventList = new ObservableCollection<CalendarEvents>();
        EventText = "";
        _jsonCalendar = new JsonCalendar();
        selectedPerson = "Home";
        User1NameText = ReferenceValues.JsonMasterSettings.User1Name;
        User2NameText = ReferenceValues.JsonMasterSettings.User2Name;
        CalendarEventSelected = new CalendarEvents();
        UserButtonLogic();

        if (ReferenceValues.IsCalendarDupeModeEnabled) {
            DupeText = "Duplicate Mode Enabled\n" + ReferenceValues.DupeEvent.startTime + " - " + ReferenceValues.DupeEvent.endTime + "  " + ReferenceValues.DupeEvent.name;
            DupeButtonBackgroundColor = "Green";
            EventText = ReferenceValues.DupeEvent.name;
            LocationText = ReferenceValues.DupeEvent.location;
            DescriptionText = ReferenceValues.DupeEvent.description;
            StartTimeText = ReferenceValues.DupeEvent.startTime;
            EndTimeText = ReferenceValues.DupeEvent.endTime;

            if (ReferenceValues.DupeEvent.person == ReferenceValues.JsonMasterSettings.User1Name) {
                selectedPerson = ReferenceValues.JsonMasterSettings.User1Name;
            } else if (ReferenceValues.DupeEvent.person == ReferenceValues.JsonMasterSettings.User2Name) {
                selectedPerson = ReferenceValues.JsonMasterSettings.User2Name;
            } else if (string.IsNullOrEmpty(ReferenceValues.DupeEvent.person)) {
                selectedPerson = "Home";
            } else {
                selectedPerson = ReferenceValues.DupeEvent.person;
            }
        } else {
            DupeText = "";
            DupeButtonBackgroundColor = "Transparent";
        }

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
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "WARN",
                            Module = "EditCalendarVM",
                            Description = e.ToString()
                        });
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
            }
        }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void PopulateDetailedView(CalendarEvents value) {
        EventText = value.name;
        LocationText = value.location;
        DescriptionText = value.description;
        StartTimeText = value.startTime;
        EndTimeText = value.endTime;

        if (value.person == ReferenceValues.JsonMasterSettings.User1Name) {
            selectedPerson = ReferenceValues.JsonMasterSettings.User1Name;
        } else if (value.person == ReferenceValues.JsonMasterSettings.User2Name) {
            selectedPerson = ReferenceValues.JsonMasterSettings.User2Name;
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
        ParentsBackgroundColor = "Transparent";
        ChildrenBackgroundColor = "Transparent";
        HomeBackgroundColor = "Transparent";
        OtherBackgroundColor = "Transparent";

        if (selectedPerson == ReferenceValues.JsonMasterSettings.User1Name) {
            User1BackgroundColor = "Green";
        } else if (selectedPerson == ReferenceValues.JsonMasterSettings.User2Name) {
            User2BackgroundColor = "Green";
        } else {
            switch (selectedPerson) {
            case "Parents":
                ParentsBackgroundColor = "Green";
                break;
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
                MediaPlayer sound = new();
                sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/missing_info.wav"));
                sound.Play();
            } else {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditCalendarVM",
                    Description = "Adding calendar event: " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " + DescriptionText + ", " +
                                  LocationText + ", " + selectedPerson
                });

                EventList.Add(new CalendarEvents {
                    name = EventText,
                    description = DescriptionText,
                    location = LocationText,
                    person = selectedPerson,
                    startTime = StartTimeText,
                    endTime = EndTimeText
                });

                MediaPlayer sound = new();
                sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/scribble1.wav"));
                sound.Play();
                EventText = "";
                DescriptionText = "";
                LocationText = "";
                StartTimeText = "";
                EndTimeText = "";

                SaveJson();
            }

            break;
        case "update":
            try {
                if (CalendarEventSelected.name != null) {
                    if (string.IsNullOrWhiteSpace(EventText)) {
                        MediaPlayer sound = new();
                        sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/missing_info.wav"));
                        sound.Play();
                    } else if (!string.IsNullOrWhiteSpace(CalendarEventSelected.name)) {
                        confirmation = MessageBox.Show("Are you sure you want to update event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (confirmation == MessageBoxResult.Yes) {
                            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditCalendarVM",
                                Description = "Updating calendar event: " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " +
                                              DescriptionText + ", " +
                                              LocationText + ", " + selectedPerson
                            });

                            EventList.Insert(EventList.IndexOf(CalendarEventSelected), new CalendarEvents {
                                name = EventText,
                                description = DescriptionText,
                                location = LocationText,
                                person = selectedPerson,
                                startTime = StartTimeText,
                                endTime = EndTimeText
                            });
                            EventList.Remove(CalendarEventSelected);

                            MediaPlayer sound = new();
                            sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/scribble2.wav"));
                            sound.Play();
                            EventText = "";
                            DescriptionText = "";
                            LocationText = "";
                            StartTimeText = "";
                            EndTimeText = "";

                            SaveJson();
                        }
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
            }

            break;
        case "delete":
            try {
                if (CalendarEventSelected.name != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditCalendarVM",
                            Description = "Removing calendar event: " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " + DescriptionText +
                                          ", " +
                                          LocationText + ", " + selectedPerson
                        });

                        EventList.Remove(CalendarEventSelected);
                        MediaPlayer sound = new();
                        sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/scribble3.wav"));
                        sound.Play();

                        SaveJson();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
            }

            break;

        case "dupe":
            if (ReferenceValues.IsCalendarDupeModeEnabled) {
                ReferenceValues.IsCalendarDupeModeEnabled = false;
                DupeButtonBackgroundColor = "Transparent";
                DupeText = "";
            } else {
                if (string.IsNullOrWhiteSpace(EventText)) {
                    MediaPlayer sound = new();
                    sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/missing_info.wav"));
                    sound.Play();
                } else {
                    ReferenceValues.IsCalendarDupeModeEnabled = true;
                    DupeButtonBackgroundColor = "Green";

                    ReferenceValues.DupeEvent = new CalendarEvents {
                        name = EventText,
                        description = DescriptionText,
                        location = LocationText,
                        person = selectedPerson,
                        startTime = StartTimeText,
                        endTime = EndTimeText
                    };

                    DupeText = "Duplicate Mode Enabled\n" + StartTimeText + " - " + EndTimeText + "  " + EventText;
                }
            }

            break;

        case "user1":
            selectedPerson = ReferenceValues.JsonMasterSettings.User1Name;
            UserButtonLogic();
            break;

        case "user2":
            selectedPerson = ReferenceValues.JsonMasterSettings.User2Name;
            UserButtonLogic();
            break;

        case "children":
            selectedPerson = "Children";
            UserButtonLogic();
            break;

        case "parents":
            selectedPerson = "Parents";
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
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
            }
        } else {
            try {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(fileName);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
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

    public string ParentsBackgroundColor {
        get => _parentsBackgroundColor;
        set {
            _parentsBackgroundColor = value;
            RaisePropertyChangedEvent("ParentsBackgroundColor");
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

    public string StartTimeText {
        get => _startTimeText;
        set {
            _startTimeText = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("StartTimeText");
        }
    }

    public string EndTimeText {
        get => _endTimeText;
        set {
            _endTimeText = VerifyInput.VerifyTextNumeric(value);
            RaisePropertyChangedEvent("EndTimeText");
        }
    }

    public string DupeButtonBackgroundColor {
        get => _dupeButtonBackgroundColor;
        set {
            _dupeButtonBackgroundColor = value;
            RaisePropertyChangedEvent("DupeButtonBackgroundColor");
        }
    }

    public string DupeText {
        get => _dupeText;
        set {
            _dupeText = value;
            RaisePropertyChangedEvent("DupeText");
        }
    }

    #endregion
}