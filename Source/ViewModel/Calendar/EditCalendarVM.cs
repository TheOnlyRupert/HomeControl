using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;
using MySql.Data.MySqlClient;

namespace HomeControl.Source.ViewModel.Calendar;

public class EditCalendarVM : BaseViewModel {
    private CalendarEvents _calendarEventSelected;
    private string _detailedWeather, _temperatureHighLow, _eventDateWeekday;

    private DateTime _eventDate;

    private ObservableCollection<CalendarEvents> _eventList;

    private string _eventText, _locationText, _descriptionText, _user1NameText, _user2NameText, _user3NameText, _user4NameText, _user5NameText, _startTimeText, _endTimeText,
        _dupeButtonBackgroundColor, _priority0BorderColor, _priority1BorderColor, _priority2BorderColor, _user1BorderColor, _user2BorderColor, _user3BorderColor, _user4BorderColor,
        _user5BorderColor, _homeBorderColor;

    private string? _forecastWeatherIcon1;
    private string? _forecastWeatherIcon2;

    private BitmapImage _imageUser1, _imageUser2, _imageUser3, _imageUser4, _imageUser5, _imageHome;

    private int _priority0BorderThickness, _priority1BorderThickness, _priority2BorderThickness, _user1BorderThickness, _user2BorderThickness, _user3BorderThickness, _user4BorderThickness,
        _user5BorderThickness, _homeBorderThickness, priority, user, _databaseId;

    public EditCalendarVM() {
        EventText = "";
        EndTimeText = "";
        StartTimeText = "";
        DescriptionText = "";
        LocationText = "";

        PriorityLogic(0);
        UserLogic(0);

        try {
            Uri uri = new(ReferenceValues.DocumentsDirectory + "icons/user1.png", UriKind.RelativeOrAbsolute);
            ImageUser1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user2.png", UriKind.RelativeOrAbsolute);
            ImageUser2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user3.png", UriKind.RelativeOrAbsolute);
            ImageUser3 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user4.png", UriKind.RelativeOrAbsolute);
            ImageUser4 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user5.png", UriKind.RelativeOrAbsolute);
            ImageUser5 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DocumentsDirectory + "icons/user0.png", UriKind.RelativeOrAbsolute);
            ImageHome = new BitmapImage(uri);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditCalendarVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        PopulateEvent();
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private void PopulateEvent() {
        EventDateWeekday = ReferenceValues.CalendarEventDate.ToString("dddd");
        EventDate = ReferenceValues.CalendarEventDate;
        EventList = [];
        CalendarEventSelected = new CalendarEvents();

        foreach (CalendarEvents calendarEvents in ReferenceValues.JsonCalendarMaster.EventsList) {
            if (ReferenceValues.CalendarEventDate.Date == calendarEvents.Date) {
                calendarEvents.Image = ReferenceValues.DocumentsDirectory + "icons/user" + calendarEvents.UserId + ".png";
                EventList.Add(calendarEvents);
            }
        }


        /* Populate Weather */
        ForecastWeatherIcon1 = null;
        ForecastWeatherIcon2 = null;
        TemperatureHighLow = "";
        DetailedWeather = "";

        try {
            foreach (JsonWeather.Periods periods in ReferenceValues.ForecastSevenDay.properties.periods) {
                if (periods.startTime.Date == ReferenceValues.CalendarEventDate.Date && periods.isDaytime) {
                    string?[] weatherIcons = WeatherHelpers.RegexWeatherForecast(periods.shortForecast);
                    ForecastWeatherIcon1 = WeatherHelpers.GetWeatherIcon(weatherIcons[0], periods.isDaytime, periods.temperature, periods.windSpeed);
                    if (weatherIcons.Length > 1) {
                        ForecastWeatherIcon2 = WeatherHelpers.GetWeatherIcon(weatherIcons[1], periods.isDaytime, periods.temperature, periods.windSpeed);
                    }

                    TemperatureHighLow = periods.temperature + "°";
                    DetailedWeather = periods.detailedForecast;
                }
            }
        } catch (Exception) {
            //ignore
        }

        if (ReferenceValues.IsCalendarDupeModeEnabled) {
            DupeButtonBackgroundColor = "Green";
            EventText = ReferenceValues.DupeEvent.EventName;
            LocationText = ReferenceValues.DupeEvent.Location;
            DescriptionText = ReferenceValues.DupeEvent.Description;
            StartTimeText = ReferenceValues.DupeEvent.StartTime;
            EndTimeText = ReferenceValues.DupeEvent.EndTime;
            PriorityLogic(ReferenceValues.DupeEvent.Priority);
            UserLogic(ReferenceValues.DupeEvent.UserId);
        } else {
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
        if (value != null) {
            EventText = value.EventName;
            LocationText = value.Location;
            DescriptionText = value.Description;
            StartTimeText = value.StartTime;
            EndTimeText = value.EndTime;
            _databaseId = value.DatabaseID;

            PriorityLogic(value.Priority);
            UserLogic(value.UserId);
        }
    }

    private void ButtonLogic(object param) {
        MessageBoxResult confirmation;

        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(EventText)) {
                SoundDispatcher.PlaySound("missing_info");
            } else {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditCalendarVM",
                    Description = "Adding calendar event: User" + user + ", " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " + DescriptionText + ", " +
                                  LocationText + ", "
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                EventText ??= "";
                DescriptionText ??= "";
                LocationText ??= "";
                StartTimeText ??= "";
                EndTimeText ??= "";
                LocationText ??= "";
                LocationText ??= "";
                
                EventList.Add(new CalendarEvents {
                    EventName = EventText,
                    Description = DescriptionText,
                    Location = LocationText,
                    StartTime = StartTimeText,
                    EndTime = EndTimeText,
                    Priority = priority,
                    UserId = user,
                    Image = ReferenceValues.DocumentsDirectory + "icons/user" + user + ".png"
                });

                /* Add to Database */
                using MySqlConnection connection = new(ReferenceValues.DatabaseConnectionString);
                const string query = "INSERT INTO calendar_events (event_name, event_date, start_time, end_time, description, location, user_id, priority) " +
                                     "VALUES (@event_name, @event_date, @start_time, @end_time, @description, @location, @user_id, @priority)";

                try {
                    connection.Open();

                    // Set up command and add parameters to prevent SQL injection
                    using MySqlCommand command = new(query, connection);
                    command.Parameters.AddWithValue("@event_name", EventText);
                    command.Parameters.AddWithValue("@event_date", EventDate);
                    command.Parameters.AddWithValue("@start_time", StartTimeText);
                    command.Parameters.AddWithValue("@end_time", EndTimeText);
                    command.Parameters.AddWithValue("@description", DescriptionText);
                    command.Parameters.AddWithValue("@location", LocationText);
                    command.Parameters.AddWithValue("@user_id", user);
                    command.Parameters.AddWithValue("@priority", priority);

                    // Execute the insert query
                    command.ExecuteNonQuery();

                    connection.Close();
                } catch (Exception) {
                    //todo: this
                }

                SoundDispatcher.PlaySound("scribble1");
                EventText = "";
                DescriptionText = "";
                LocationText = "";
                StartTimeText = "";
                EndTimeText = "";
            }

            break;
        case "update":
            try {
                if (string.IsNullOrWhiteSpace(EventText)) {
                    SoundDispatcher.PlaySound("missing_info");
                } else if (!string.IsNullOrWhiteSpace(CalendarEventSelected.EventName)) {
                    confirmation = MessageBox.Show("Are you sure you want to update event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditCalendarVM",
                            Description = "Updating calendar event: User" + user + ", " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " +
                                          DescriptionText + ", " +
                                          LocationText + ", "
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        EventText ??= "";
                        DescriptionText ??= "";
                        LocationText ??= "";
                        StartTimeText ??= "";
                        EndTimeText ??= "";
                        LocationText ??= "";
                        LocationText ??= "";

                        EventList.Insert(EventList.IndexOf(CalendarEventSelected), new CalendarEvents {
                            EventName = EventText,
                            Description = DescriptionText,
                            Location = LocationText,
                            StartTime = StartTimeText,
                            EndTime = EndTimeText,
                            Priority = priority,
                            UserId = user,
                            Image = ReferenceValues.DocumentsDirectory + "icons/user" + user + ".png"
                        });
                        EventList.Remove(CalendarEventSelected);

                        /* Update Database */
                        using MySqlConnection connection = new(ReferenceValues.DatabaseConnectionString);
                        const string query =
                            "UPDATE calendar_events SET event_name = @event_name, event_date = @event_date, start_time = @start_time, end_time = @end_time, description = @description, location = @location, user_id = @user_id, priority = @priority WHERE id = @id";

                        try {
                            connection.Open();

                            using MySqlCommand command = new(query, connection);
                            command.Parameters.AddWithValue("@id", _databaseId);
                            command.Parameters.AddWithValue("@event_name", EventText);
                            command.Parameters.AddWithValue("@event_date", EventDate);
                            command.Parameters.AddWithValue("@start_time", StartTimeText);
                            command.Parameters.AddWithValue("@end_time", EndTimeText);
                            command.Parameters.AddWithValue("@description", DescriptionText);
                            command.Parameters.AddWithValue("@location", LocationText);
                            command.Parameters.AddWithValue("@user_id", user);
                            command.Parameters.AddWithValue("@priority", priority);
                            command.ExecuteNonQuery();

                            connection.Close();
                        } catch (Exception) {
                            //todo: this
                        }
                        
                        SoundDispatcher.PlaySound("scribble2");
                        EventText = "";
                        DescriptionText = "";
                        LocationText = "";
                        StartTimeText = "";
                        EndTimeText = "";
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case "delete":
            try {
                confirmation = MessageBox.Show("Are you sure you want to delete event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes) {
                    ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "INFO",
                        Module = "EditCalendarVM",
                        Description = "Removing calendar event: User" + user + ", " + EventDate + ", " + "(" + StartTimeText + "-" + EndTimeText + "), " + EventText + ", " + DescriptionText +
                                      ", " + LocationText + ", "
                    });
                    FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                    EventList.Remove(CalendarEventSelected);

                    /* Update Database */
                    using MySqlConnection connection = new(ReferenceValues.DatabaseConnectionString);
                    const string query = "DELETE FROM calendar_events WHERE id = @id";

                    try {
                        connection.Open();

                        using (MySqlCommand command = new(query, connection)) {
                            command.Parameters.AddWithValue("@id", _databaseId);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    } catch (Exception) {
                        //todo: this
                    }

                    SoundDispatcher.PlaySound("scribble3");
                    //EventText = "";
                    //DescriptionText = "";
                    //LocationText = "";
                    //StartTimeText = "";
                    //EndTimeText = "";
                    //LocationText = "";
                    //LocationText = "";
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditCalendarVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;

        case "dupe":
            if (ReferenceValues.IsCalendarDupeModeEnabled) {
                ReferenceValues.IsCalendarDupeModeEnabled = false;
                DupeButtonBackgroundColor = "Transparent";
                EventText = "";
                LocationText = "";
                DescriptionText = "";
                StartTimeText = "";
                EndTimeText = "";
            } else {
                if (string.IsNullOrWhiteSpace(EventText)) {
                    SoundDispatcher.PlaySound("missing_info");
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
                        Image = ReferenceValues.DocumentsDirectory + "icons/user" + user + ".png"
                    };
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

    #region Fields

    public DateTime EventDate {
        get => _eventDate;
        set {
            _eventDate = value;
            RaisePropertyChangedEvent("EventDate");
        }
    }

    public string EventDateWeekday {
        get => _eventDateWeekday;
        set {
            _eventDateWeekday = value;
            RaisePropertyChangedEvent("EventDateWeekday");
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

    public string? ForecastWeatherIcon1 {
        get => _forecastWeatherIcon1;
        set {
            _forecastWeatherIcon1 = value;
            RaisePropertyChangedEvent("ForecastWeatherIcon1");
        }
    }

    public string? ForecastWeatherIcon2 {
        get => _forecastWeatherIcon2;
        set {
            _forecastWeatherIcon2 = value;
            RaisePropertyChangedEvent("ForecastWeatherIcon2");
        }
    }

    public string TemperatureHighLow {
        get => _temperatureHighLow;
        set {
            _temperatureHighLow = value;
            RaisePropertyChangedEvent("TemperatureHighLow");
        }
    }

    public string DetailedWeather {
        get => _detailedWeather;
        set {
            _detailedWeather = value;
            RaisePropertyChangedEvent("DetailedWeather");
        }
    }

    #endregion
}