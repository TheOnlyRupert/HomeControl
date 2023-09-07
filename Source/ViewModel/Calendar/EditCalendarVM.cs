using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class EditCalendarVM : BaseViewModel {
    private CalendarEvents _calendarEventSelected;

    private string _eventDate, _eventText, _locationText, _descriptionText, _user1BackgroundColor, _user2BackgroundColor, _childrenBackgroundColor, _homeBackgroundColor,
        _otherBackgroundColor, selectedPerson, _user1NameText, _user2NameText, _startTimeText, _endTimeText, _parentsBackgroundColor, _dupeButtonBackgroundColor, _dupeText;

    private ObservableCollection<CalendarEvents> _eventList;

    public EditCalendarVM() {
        User1NameText = ReferenceValues.JsonSettingsMaster.User1Name;
        User2NameText = ReferenceValues.JsonSettingsMaster.User2Name;
        EventText = "";

        PopulateEvent();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void PopulateEvent() {
        EventDate = ReferenceValues.CalendarEventDate.ToLongDateString();
        EventList = new ObservableCollection<CalendarEvents>();
        selectedPerson = "Home";
        CalendarEventSelected = new CalendarEvents();

        if (ReferenceValues.IsCalendarDupeModeEnabled) {
            DupeText = "Duplicate Mode Enabled\n" + ReferenceValues.DupeEvent.StartTime + " - " + ReferenceValues.DupeEvent.EndTime + "  " + ReferenceValues.DupeEvent.EventName;
            DupeButtonBackgroundColor = "Green";
            EventText = ReferenceValues.DupeEvent.EventName;
            LocationText = ReferenceValues.DupeEvent.Location;
            DescriptionText = ReferenceValues.DupeEvent.Description;
            StartTimeText = ReferenceValues.DupeEvent.StartTime;
            EndTimeText = ReferenceValues.DupeEvent.EndTime;
        } else {
            DupeText = "";
            DupeButtonBackgroundColor = "Transparent";
        }

        /* Get Calendar Events */
        foreach (CalendarEvents calendar in ReferenceValues.JsonCalendarMaster.eventsList) {
            Console.WriteLine(ReferenceValues.CalendarEventDate.Date + " ... " + calendar.Date);
            if (ReferenceValues.CalendarEventDate.Date == calendar.Date) {
                EventList.Add(calendar);
            }
        }
    }

    private void PopulateDetailedView(CalendarEvents value) {
        EventText = value.EventName;
        LocationText = value.Location;
        DescriptionText = value.Description;
        StartTimeText = value.StartTime;
        EndTimeText = value.EndTime;
    }

    private void ButtonLogic(object param) {
        MessageBoxResult confirmation;

        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(EventText)) {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            } else {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditCalendarVM",
                    Description = "Adding calendar event: " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " + DescriptionText + ", " +
                                  LocationText + ", " + selectedPerson
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));

                EventList.Add(new CalendarEvents {
                    Date = DateTime.Today,
                    EventName = EventText,
                    Description = DescriptionText,
                    Location = LocationText,
                    StartTime = StartTimeText,
                    EndTime = EndTimeText
                });

                ReferenceValues.SoundToPlay = "scribble1";
                SoundDispatcher.PlaySound();
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
                if (CalendarEventSelected.EventName != null) {
                    if (string.IsNullOrWhiteSpace(EventText)) {
                        ReferenceValues.SoundToPlay = "missing_info";
                        SoundDispatcher.PlaySound();
                    } else if (!string.IsNullOrWhiteSpace(CalendarEventSelected.EventName)) {
                        confirmation = MessageBox.Show("Are you sure you want to update event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (confirmation == MessageBoxResult.Yes) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditCalendarVM",
                                Description = "Updating calendar event: " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " +
                                              DescriptionText + ", " +
                                              LocationText + ", " + selectedPerson
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));

                            EventList.Insert(EventList.IndexOf(CalendarEventSelected), new CalendarEvents {
                                Date = DateTime.Today,
                                EventName = EventText,
                                Description = DescriptionText,
                                Location = LocationText,
                                StartTime = StartTimeText,
                                EndTime = EndTimeText
                            });
                            EventList.Remove(CalendarEventSelected);

                            ReferenceValues.SoundToPlay = "scribble2";
                            SoundDispatcher.PlaySound();
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
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
            }

            break;
        case "delete":
            try {
                if (CalendarEventSelected.EventName != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditCalendarVM",
                            Description = "Removing calendar event: " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " + DescriptionText +
                                          ", " +
                                          LocationText + ", " + selectedPerson
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));

                        EventList.Remove(CalendarEventSelected);
                        ReferenceValues.SoundToPlay = "scribble3";
                        SoundDispatcher.PlaySound();

                        SaveJson();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
            }

            break;

        case "dupe":
            if (ReferenceValues.IsCalendarDupeModeEnabled) {
                ReferenceValues.IsCalendarDupeModeEnabled = false;
                DupeButtonBackgroundColor = "Transparent";
                DupeText = "";
            } else {
                if (string.IsNullOrWhiteSpace(EventText)) {
                    ReferenceValues.SoundToPlay = "missing_info";
                    SoundDispatcher.PlaySound();
                } else {
                    ReferenceValues.IsCalendarDupeModeEnabled = true;
                    DupeButtonBackgroundColor = "Green";

                    ReferenceValues.DupeEvent = new CalendarEvents {
                        EventName = EventText,
                        Description = DescriptionText,
                        Location = LocationText,
                        StartTime = StartTimeText,
                        EndTime = EndTimeText
                    };

                    DupeText = "Duplicate Mode Enabled\n" + StartTimeText + " - " + EndTimeText + "  " + EventText;
                }
            }

            break;
        case "user1":
            selectedPerson = ReferenceValues.JsonSettingsMaster.User1Name;

            break;
        case "user2":
            selectedPerson = ReferenceValues.JsonSettingsMaster.User2Name;

            break;
        case "user3":
            selectedPerson = "Children";

            break;
        case "user4":
            selectedPerson = "Parents";

            break;
        case "user5":
            selectedPerson = "Home";

            break;
        case "other":
            selectedPerson = "Other";

            break;
        case "addDay":
            ReferenceValues.CalendarEventDate = ReferenceValues.CalendarEventDate.AddDays(1);
            PopulateEvent();
            break;
        case "subDay":
            ReferenceValues.CalendarEventDate = ReferenceValues.CalendarEventDate.AddDays(-1);
            PopulateEvent();
            break;
        }
    }

    private void SaveJson() {
        try {
            IOrderedEnumerable<CalendarEvents> orderByResult = from s in EventList orderby s.StartTime select s;
            EventList = new ObservableCollection<CalendarEvents>(orderByResult.ToList());

            //TODO: ADD EVENTS TO MAIN LIST

            //FileHelpers.SaveFileText("calendar", JsonSerializer.Serialize(ReferenceValues.JsonCalendarMaster));
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditCalendarVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
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
            _eventText = value;
            RaisePropertyChangedEvent("EventText");
        }
    }

    public string LocationText {
        get => _locationText;
        set {
            _locationText = value;
            RaisePropertyChangedEvent("LocationText");
        }
    }

    public string DescriptionText {
        get => _descriptionText;
        set {
            _descriptionText = value;
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