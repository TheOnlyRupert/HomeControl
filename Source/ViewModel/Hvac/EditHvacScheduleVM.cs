using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class EditHvacScheduleVM : BaseViewModel {
    private readonly ObservableCollection<string> _daysOfWeek;

    private DateTime _dateTime;
    private ObservableCollection<HvacEvent> _eventList;
    private HvacEvent _eventSelected;
    private string _eventTime, _eventDayOfWeek, _eventTemp;
    private int _temp, _dayOfWeekIndex;

    public EditHvacScheduleVM() {
        _daysOfWeek = [
            "ANY",
            "SUNDAY",
            "MONDAY",
            "TUESDAY",
            "WEDNESDAY",
            "THURSDAY",
            "FRIDAY",
            "SATURDAY"
        ];

        EventList = [];

        try {
            EventList = ReferenceValues.JsonHvacMaster.HvacEvents;
        } catch (Exception) {
            ReferenceValues.JsonHvacMaster = new JsonHvac {
                HvacEvents = []
            };
        }

        _dateTime = new DateTime(2023, 1, 1, 6, 0, 0, 0);
        EventTime = _dateTime.ToString("HH:mm");

        _dayOfWeekIndex = 0;
        EventDayOfWeek = _daysOfWeek[_dayOfWeekIndex];

        _temp = 22;

        TemperatureDisplay();
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private void TemperatureDisplay() {
        double f = _temp * 1.8 + 32;
        EventTemp = _temp + "°C  or  " + (int)f + "°F";
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "subTime":
            _dateTime = _dateTime.AddHours(-1);
            EventTime = _dateTime.ToString("HH:mm");

            break;
        case "addTime":
            _dateTime = _dateTime.AddHours(1);
            EventTime = _dateTime.ToString("HH:mm");

            break;
        case "subDayOfWeek":
            _dayOfWeekIndex--;

            if (_dayOfWeekIndex < 0) {
                _dayOfWeekIndex = 7;
            }

            EventDayOfWeek = _daysOfWeek[_dayOfWeekIndex];

            break;
        case "addDayOfWeek":
            _dayOfWeekIndex++;

            if (_dayOfWeekIndex > 7) {
                _dayOfWeekIndex = 0;
            }

            EventDayOfWeek = _daysOfWeek[_dayOfWeekIndex];

            break;
        case "subTemp":
            if (_temp > 15) {
                _temp--;
            }

            TemperatureDisplay();

            break;
        case "addTemp":
            if (_temp < 30) {
                _temp++;
            }

            TemperatureDisplay();

            break;
        case "add":
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "EditHvacScheduleVM",
                Description = "Adding HVAC scheduled event:  At " + EventTime + ", On " + EventDayOfWeek + ", Set to " + EventTemp + "°C"
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

            EventList.Add(new HvacEvent {
                EventTime = _dateTime,
                EventDayOfWeek = EventDayOfWeek,
                EventTemp = _temp
            });

            SoundDispatcher.PlaySound("newTask");
            SaveJson();

            break;
        case "delete":
            try {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditHvacScheduleVM",
                    Description = "Removing event"
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                EventList.Remove(EventSelected);
                SaveJson();
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditHvacScheduleVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        }
    }

    private void SaveJson() {
        try {
            ReferenceValues.JsonHvacMaster.HvacEvents = EventList;
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditHvacScheduleVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }


        try {
            FileHelpers.SaveFileText("hvac", JsonSerializer.Serialize(ReferenceValues.JsonHvacMaster), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditHvacScheduleVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }
    }

    private void PopulateDetailedView(HvacEvent value) {
        _dateTime = value.EventTime;
        EventTime = _dateTime.ToString("HH:mm");

        EventDayOfWeek = value.EventDayOfWeek;
        EventDayOfWeek = _daysOfWeek[_dayOfWeekIndex];

        _temp = value.EventTemp;
        TemperatureDisplay();
    }

    #region Fields

    public string EventTime {
        get => _eventTime;
        set {
            _eventTime = value;
            RaisePropertyChangedEvent("EventTime");
        }
    }

    public string EventDayOfWeek {
        get => _eventDayOfWeek;
        set {
            _eventDayOfWeek = value;
            RaisePropertyChangedEvent("EventDayOfWeek");
        }
    }

    public string EventTemp {
        get => _eventTemp;
        set {
            _eventTemp = value;
            RaisePropertyChangedEvent("EventTemp");
        }
    }

    public ObservableCollection<HvacEvent> EventList {
        get => _eventList;
        set {
            _eventList = value;
            RaisePropertyChangedEvent("EventList");
        }
    }

    public HvacEvent EventSelected {
        get => _eventSelected;
        set {
            _eventSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("EventSelected");
        }
    }

    #endregion
}