using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class EditRecurringVM : BaseViewModel {
    private CalendarEventsRecurring _calendarEventSelected;

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private string DateText { get; set; } = DateTime.Now.ToShortDateString();

    private string HolidayText { get; set; } = string.Empty;

    private ObservableCollection<CalendarEventsRecurring> EventList { get; } = ReferenceValues.JsonCalendarMaster.EventsListRecurring;

    public CalendarEventsRecurring CalendarEventSelected {
        get => _calendarEventSelected;
        set {
            _calendarEventSelected = value;
            PopulateDetailedView(value);
        }
    }

    public ObservableCollection<string> ImageList { get; set; } = ReferenceValues.IconImageList;

    private string ImageSelected { get; } = "bathtub";

    private void ButtonLogic(object param) {
        if (param == null) return;

        switch (param.ToString()) {
        case "add":
            AddEvent();
            break;
        case "update":
            UpdateEvent();
            break;
        case "delete":
            DeleteEvent();
            break;
        }
    }

    private void AddEvent() {
        if (string.IsNullOrWhiteSpace(HolidayText)) {
            PlaySound("missing_info");
            return;
        }

        LogDebug("Adding recurring calendar event: " + $"{DateText}, {HolidayText}");

        EventList.Add(new CalendarEventsRecurring {
            Date = DateTime.Parse(DateText),
            EventText = HolidayText,
            Image = $"../../../Resources/Images/icons/{ImageSelected}.png"
        });

        PlaySound("birthday");
        HolidayText = string.Empty;
        SaveJson();
    }

    private void UpdateEvent() {
        if (CalendarEventSelected?.EventText == null || string.IsNullOrWhiteSpace(HolidayText)) {
            PlaySound("missing_info");
            return;
        }

        MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to update event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirmation != MessageBoxResult.Yes) return;

        LogDebug("Updating recurring calendar event: " + $"{DateText}, {HolidayText}");

        CalendarEventsRecurring updatedEvent = new() {
            Date = DateTime.Parse(DateText),
            EventText = HolidayText,
            Image = $"../../../Resources/Images/icons/{ImageSelected}.png"
        };

        EventList.Insert(EventList.IndexOf(CalendarEventSelected), updatedEvent);
        EventList.Remove(CalendarEventSelected);

        PlaySound("birthday");
        HolidayText = string.Empty;
        SaveJson();
    }

    private void DeleteEvent() {
        if (CalendarEventSelected?.EventText == null) return;

        MessageBoxResult confirmation = MessageBox.Show("Are you sure you want to delete event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirmation != MessageBoxResult.Yes) return;

        LogDebug("Deleting recurring calendar event: " + $"{DateText}, {HolidayText}");

        EventList.Remove(CalendarEventSelected);
        HolidayText = string.Empty;
        SaveJson();
    }

    private static void LogDebug(string description) {
        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "INFO",
            Module = nameof(EditRecurringVM),
            Description = description
        });
        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
    }

    private static void PlaySound(string soundName) {
        ReferenceValues.SoundToPlay = soundName;
        SoundDispatcher.PlaySound();
    }

    private void SaveJson() {
        try {
            ReferenceValues.JsonCalendarMaster.EventsListRecurring = EventList;
            FileHelpers.SaveFileText("calendar", JsonSerializer.Serialize(ReferenceValues.JsonCalendarMaster), true);
        } catch (Exception e) {
            LogDebug(e.ToString());
        }
    }

    private void PopulateDetailedView(CalendarEventsRecurring value) {
        if (value == null) return;
        DateText = value.Date.ToShortDateString();
        HolidayText = value.EventText;
    }
}