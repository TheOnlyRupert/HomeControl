using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class EditCalendarVM : BaseViewModel {
    private CalendarEvents _calendarEventSelected;

    private string _eventDate, _eventText, _locationText, _descriptionText, _user1NameText, _user2NameText, _user3NameText, _user4NameText, _user5NameText, _startTimeText, _endTimeText,
        _dupeButtonBackgroundColor, _dupeText, _priority0BorderColor, _priority1BorderColor, _priority2BorderColor, _user1BorderColor, _user2BorderColor, _user3BorderColor, _user4BorderColor,
        _user5BorderColor, _homeBorderColor;

    private ObservableCollection<CalendarEvents> _eventList;

    private BitmapImage _imageUser1, _imageUser2, _imageUser3, _imageUser4, _imageUser5, _imageHome;

    private int _priority0BorderThickness, _priority1BorderThickness, _priority2BorderThickness, _user1BorderThickness, _user2BorderThickness, _user3BorderThickness, _user4BorderThickness,
        _user5BorderThickness, _homeBorderThickness, priority, user;

    public EditCalendarVM() {
        EventText = "";
        EndTimeText = "";
        StartTimeText = "";
        DescriptionText = "";
        LocationText = "";

        PriorityLogic(0);
        UserLogic(0);

        try {
            Uri uri = new(ReferenceValues.FILE_DIRECTORY + "icons/user1.png", UriKind.RelativeOrAbsolute);
            ImageUser1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user2.png", UriKind.RelativeOrAbsolute);
            ImageUser2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user3.png", UriKind.RelativeOrAbsolute);
            ImageUser3 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user4.png", UriKind.RelativeOrAbsolute);
            ImageUser4 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user5.png", UriKind.RelativeOrAbsolute);
            ImageUser5 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user0.png", UriKind.RelativeOrAbsolute);
            ImageHome = new BitmapImage(uri);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditCalendarVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
        }

        PopulateEvent();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void PopulateEvent() {
        EventDate = ReferenceValues.CalendarEventDate.ToLongDateString();
        EventList = new ObservableCollection<CalendarEvents>();
        CalendarEventSelected = new CalendarEvents();

        foreach (CalendarDates dates in ReferenceValues.JsonCalendarMaster.DatesList) {
            if (ReferenceValues.CalendarEventDate.ToString("yyyy-MM-dd") == dates.Date) {
                EventList = dates.EventsList;
            }
        }

        if (ReferenceValues.IsCalendarDupeModeEnabled) {
            DupeText = "Duplicate Mode Enabled\n" + ReferenceValues.DupeEvent.StartTime + " - " + ReferenceValues.DupeEvent.EndTime + "  " + ReferenceValues.DupeEvent.EventName;
            DupeButtonBackgroundColor = "Green";
            EventText = ReferenceValues.DupeEvent.EventName;
            LocationText = ReferenceValues.DupeEvent.Location;
            DescriptionText = ReferenceValues.DupeEvent.Description;
            StartTimeText = ReferenceValues.DupeEvent.StartTime;
            EndTimeText = ReferenceValues.DupeEvent.EndTime;
            PriorityLogic(ReferenceValues.DupeEvent.Priority);
            UserLogic(ReferenceValues.DupeEvent.UserId);
        } else {
            DupeText = "";
            DupeButtonBackgroundColor = "Transparent";
        }
    }

    private void PriorityLogic(int button) {
        priority = button;

        switch (button) {
        case 0:
            Priority0BorderColor = "Green";
            Priority1BorderColor = "DarkSlateGray";
            Priority2BorderColor = "DarkSlateGray";
            Priority0BorderThickness = 4;
            Priority1BorderThickness = 1;
            Priority2BorderThickness = 1;
            break;
        case 1:
            Priority0BorderColor = "DarkSlateGray";
            Priority1BorderColor = "Green";
            Priority2BorderColor = "DarkSlateGray";
            Priority0BorderThickness = 1;
            Priority1BorderThickness = 4;
            Priority2BorderThickness = 1;
            break;
        case 2:
            Priority0BorderColor = "DarkSlateGray";
            Priority1BorderColor = "DarkSlateGray";
            Priority2BorderColor = "Green";
            Priority0BorderThickness = 1;
            Priority1BorderThickness = 1;
            Priority2BorderThickness = 4;
            break;
        }
    }

    private void UserLogic(int button) {
        user = button;

        switch (button) {
        case 0:
            HomeBorderColor = "Green";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 4;
            User1BorderThickness = 1;
            User2BorderThickness = 1;
            User3BorderThickness = 1;
            User4BorderThickness = 1;
            User5BorderThickness = 1;
            break;
        case 1:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "Green";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 1;
            User1BorderThickness = 4;
            User2BorderThickness = 1;
            User3BorderThickness = 1;
            User4BorderThickness = 1;
            User5BorderThickness = 1;
            break;
        case 2:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "Green";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 1;
            User1BorderThickness = 1;
            User2BorderThickness = 4;
            User3BorderThickness = 1;
            User4BorderThickness = 1;
            User5BorderThickness = 1;
            break;
        case 3:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "Green";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 1;
            User1BorderThickness = 1;
            User2BorderThickness = 1;
            User3BorderThickness = 4;
            User4BorderThickness = 1;
            User5BorderThickness = 1;
            break;
        case 4:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "Green";
            User5BorderColor = "DarkSlateGray";
            HomeBorderThickness = 1;
            User1BorderThickness = 1;
            User2BorderThickness = 1;
            User3BorderThickness = 1;
            User4BorderThickness = 4;
            User5BorderThickness = 1;
            break;
        case 5:
            HomeBorderColor = "DarkSlateGray";
            User1BorderColor = "DarkSlateGray";
            User2BorderColor = "DarkSlateGray";
            User3BorderColor = "DarkSlateGray";
            User4BorderColor = "DarkSlateGray";
            User5BorderColor = "Green";
            HomeBorderThickness = 1;
            User1BorderThickness = 1;
            User2BorderThickness = 1;
            User3BorderThickness = 1;
            User4BorderThickness = 1;
            User5BorderThickness = 4;
            break;
        }
    }

    private void PopulateDetailedView(CalendarEvents value) {
        EventText = value.EventName;
        LocationText = value.Location;
        DescriptionText = value.Description;
        StartTimeText = value.StartTime;
        EndTimeText = value.EndTime;

        PriorityLogic(value.Priority);
        UserLogic(value.UserId);
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
                                  LocationText + ", "
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));

                EventList.Add(new CalendarEvents {
                    EventName = EventText,
                    Description = DescriptionText,
                    Location = LocationText,
                    StartTime = StartTimeText,
                    EndTime = EndTimeText,
                    Priority = priority,
                    UserId = user,
                    Image = ReferenceValues.FILE_DIRECTORY + "icons/user" + user + ".png"
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
                                              LocationText + ", "
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));

                            EventList.Insert(EventList.IndexOf(CalendarEventSelected), new CalendarEvents {
                                EventName = EventText,
                                Description = DescriptionText,
                                Location = LocationText,
                                StartTime = StartTimeText,
                                EndTime = EndTimeText,
                                Priority = priority,
                                UserId = user,
                                Image = ReferenceValues.FILE_DIRECTORY + "icons/user" + user + ".png"
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
                                          LocationText + ", "
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
                        EndTime = EndTimeText,
                        Priority = priority,
                        UserId = user,
                        Image = ReferenceValues.FILE_DIRECTORY + "icons/user" + user + ".png"
                    };

                    DupeText = "Duplicate Mode Enabled\n" + StartTimeText + " - " + EndTimeText + "  " + EventText;
                }
            }

            break;
        case "priority0":
            PriorityLogic(0);
            break;
        case "priority1":
            PriorityLogic(1);
            break;
        case "priority2":
            PriorityLogic(2);
            break;
        case "user1":
            UserLogic(1);
            break;
        case "user2":
            UserLogic(2);
            break;
        case "user3":
            UserLogic(3);
            break;
        case "user4":
            UserLogic(4);
            break;
        case "user5":
            UserLogic(5);
            break;
        case "user0":
            UserLogic(0);
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

            bool createNew = true;
            foreach (CalendarDates dates in ReferenceValues.JsonCalendarMaster.DatesList) {
                if (dates.Date == ReferenceValues.CalendarEventDate.ToString("yyyy-MM-dd")) {
                    createNew = false;
                    dates.EventsList = EventList;
                }
            }

            if (createNew) {
                ReferenceValues.JsonCalendarMaster.DatesList.Add(new CalendarDates {
                    Date = ReferenceValues.CalendarEventDate.ToString("yyyy-MM-dd"),
                    EventsList = EventList
                });
            }

            FileHelpers.SaveFileText("calendar", JsonSerializer.Serialize(ReferenceValues.JsonCalendarMaster));
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

    public string User3NameText {
        get => _user3NameText;
        set {
            _user3NameText = value;
            RaisePropertyChangedEvent("User3NameText");
        }
    }

    public string User4NameText {
        get => _user4NameText;
        set {
            _user4NameText = value;
            RaisePropertyChangedEvent("User4NameText");
        }
    }

    public string User5NameText {
        get => _user5NameText;
        set {
            _user5NameText = value;
            RaisePropertyChangedEvent("User5NameText");
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

    public string User1BorderColor {
        get => _user1BorderColor;
        set {
            _user1BorderColor = value;
            RaisePropertyChangedEvent("User1BorderColor");
        }
    }

    public string User2BorderColor {
        get => _user2BorderColor;
        set {
            _user2BorderColor = value;
            RaisePropertyChangedEvent("User2BorderColor");
        }
    }

    public string User3BorderColor {
        get => _user3BorderColor;
        set {
            _user3BorderColor = value;
            RaisePropertyChangedEvent("User3BorderColor");
        }
    }

    public string User4BorderColor {
        get => _user4BorderColor;
        set {
            _user4BorderColor = value;
            RaisePropertyChangedEvent("User4BorderColor");
        }
    }

    public string User5BorderColor {
        get => _user5BorderColor;
        set {
            _user5BorderColor = value;
            RaisePropertyChangedEvent("User5BorderColor");
        }
    }

    public string HomeBorderColor {
        get => _homeBorderColor;
        set {
            _homeBorderColor = value;
            RaisePropertyChangedEvent("HomeBorderColor");
        }
    }

    public string Priority0BorderColor {
        get => _priority0BorderColor;
        set {
            _priority0BorderColor = value;
            RaisePropertyChangedEvent("Priority0BorderColor");
        }
    }

    public string Priority1BorderColor {
        get => _priority1BorderColor;
        set {
            _priority1BorderColor = value;
            RaisePropertyChangedEvent("Priority1BorderColor");
        }
    }

    public string Priority2BorderColor {
        get => _priority2BorderColor;
        set {
            _priority2BorderColor = value;
            RaisePropertyChangedEvent("Priority2BorderColor");
        }
    }

    public int User1BorderThickness {
        get => _user1BorderThickness;
        set {
            _user1BorderThickness = value;
            RaisePropertyChangedEvent("User1BorderThickness");
        }
    }

    public int User2BorderThickness {
        get => _user2BorderThickness;
        set {
            _user2BorderThickness = value;
            RaisePropertyChangedEvent("User2BorderThickness");
        }
    }

    public int User3BorderThickness {
        get => _user3BorderThickness;
        set {
            _user3BorderThickness = value;
            RaisePropertyChangedEvent("User3BorderThickness");
        }
    }

    public int User4BorderThickness {
        get => _user4BorderThickness;
        set {
            _user4BorderThickness = value;
            RaisePropertyChangedEvent("User4BorderThickness");
        }
    }

    public int User5BorderThickness {
        get => _user5BorderThickness;
        set {
            _user5BorderThickness = value;
            RaisePropertyChangedEvent("User5BorderThickness");
        }
    }

    public int HomeBorderThickness {
        get => _homeBorderThickness;
        set {
            _homeBorderThickness = value;
            RaisePropertyChangedEvent("HomeBorderThickness");
        }
    }

    public int Priority0BorderThickness {
        get => _priority0BorderThickness;
        set {
            _priority0BorderThickness = value;
            RaisePropertyChangedEvent("Priority0BorderThickness");
        }
    }

    public int Priority1BorderThickness {
        get => _priority1BorderThickness;
        set {
            _priority1BorderThickness = value;
            RaisePropertyChangedEvent("Priority1BorderThickness");
        }
    }

    public int Priority2BorderThickness {
        get => _priority2BorderThickness;
        set {
            _priority2BorderThickness = value;
            RaisePropertyChangedEvent("Priority2BorderThickness");
        }
    }

    public BitmapImage ImageUser1 {
        get => _imageUser1;
        set {
            _imageUser1 = value;
            RaisePropertyChangedEvent("ImageUser1");
        }
    }

    public BitmapImage ImageUser2 {
        get => _imageUser2;
        set {
            _imageUser2 = value;
            RaisePropertyChangedEvent("ImageUser2");
        }
    }

    public BitmapImage ImageUser3 {
        get => _imageUser3;
        set {
            _imageUser3 = value;
            RaisePropertyChangedEvent("ImageUser3");
        }
    }

    public BitmapImage ImageUser4 {
        get => _imageUser4;
        set {
            _imageUser4 = value;
            RaisePropertyChangedEvent("ImageUser4");
        }
    }

    public BitmapImage ImageUser5 {
        get => _imageUser5;
        set {
            _imageUser5 = value;
            RaisePropertyChangedEvent("ImageUser5");
        }
    }

    public BitmapImage ImageHome {
        get => _imageHome;
        set {
            _imageHome = value;
            RaisePropertyChangedEvent("ImageHome");
        }
    }

    #endregion
}