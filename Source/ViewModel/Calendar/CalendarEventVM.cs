using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class CalendarEventVM : BaseViewModel {
    private CalendarEvents _calendarEventSelected;
    private string _eventDate, _eventText, _locationText, _descriptionText, _saveButtonText;
    private ObservableCollection<CalendarEvents> _eventList;

    public CalendarEventVM() {
        EventDate = ReferenceValues.CalendarEventDate.ToLongDateString();
        SaveButtonText = "Save";
        string fileName = ReferenceValues.FILE_DIRECTORY + "events/" + ReferenceValues.CalendarEventDate.ToString("yyyy_MM_dd") + ".json";
        Console.WriteLine(fileName);

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
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "save":

            break;
        case "delete":
            break;
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

    public string SaveButtonText {
        get => _saveButtonText;
        set {
            _saveButtonText = value;
            RaisePropertyChangedEvent("SaveButtonText");
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

    #endregion
}