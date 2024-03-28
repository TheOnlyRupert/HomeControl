using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Hvac;

public class EditHvacScheduleVM : BaseViewModel {
    private readonly ObservableCollection<string> daysOfWeek;
    private ObservableCollection<HvacEvent> _eventList;
    private HvacEvent _eventSelected;
    private string _eventTime, _eventDayOfWeek, _eventTemp;

    private DateTime dateTime;
    private int temp, dayOfWeekIndex;

    public EditHvacScheduleVM() {
        daysOfWeek = new ObservableCollection<string> {
            "ANY",
            "SUNDAY",
            "MONDAY",
            "TUESDAY",
            "WEDNESDAY",
            "THURSDAY",
            "FRIDAY",
            "SATURDAY"
        };

        EventList = new ObservableCollection<HvacEvent>();

        try {
            EventList = ReferenceValues.JsonHvacMaster.HvacEvents;
        } catch (Exception) {
            ReferenceValues.JsonHvacMaster = new JsonHvac {
                HvacEvents = new ObservableCollection<HvacEvent>()
            };
        }

        dateTime = new DateTime(2023, 1, 1, 6, 0, 0, 0);
        EventTime = dateTime.ToString("HH:mm");

        dayOfWeekIndex = 0;
        EventDayOfWeek = daysOfWeek[dayOfWeekIndex];

        temp = 22;

        TemperatureDisplay();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void TemperatureDisplay() {
        double f = temp * 1.8 + 32;
        EventTemp = temp + "°C  or  " + (int)f + "°F";
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "subTime":
            dateTime = dateTime.AddHours(-1);
            EventTime = dateTime.ToString("HH:mm");

            break;
        case "addTime":
            dateTime = dateTime.AddHours(1);
            EventTime = dateTime.ToString("HH:mm");

            break;
        case "subDayOfWeek":
            dayOfWeekIndex--;

            if (dayOfWeekIndex < 0) {
                dayOfWeekIndex = 7;
            }

            EventDayOfWeek = daysOfWeek[dayOfWeekIndex];

            break;
        case "addDayOfWeek":
            dayOfWeekIndex++;

            if (dayOfWeekIndex > 7) {
                dayOfWeekIndex = 0;
            }

            EventDayOfWeek = daysOfWeek[dayOfWeekIndex];

            break;
        case "subTemp":
            if (temp > 15) {
                temp--;
            }

            TemperatureDisplay();

            break;
        case "addTemp":
            if (temp < 30) {
                temp++;
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
                EventTime = dateTime,
                EventDayOfWeek = EventDayOfWeek,
                EventTemp = temp
            });

            ReferenceValues.SoundToPlay = "newTask";
            SoundDispatcher.PlaySound();
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
        dateTime = value.EventTime;
        EventTime = dateTime.ToString("HH:mm");

        EventDayOfWeek = value.EventDayOfWeek;
        EventDayOfWeek = daysOfWeek[dayOfWeekIndex];

        temp = value.EventTemp;
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