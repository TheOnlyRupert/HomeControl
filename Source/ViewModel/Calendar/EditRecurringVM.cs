using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class EditRecurringVM : BaseViewModel {
    private readonly string fileName;
    private CalendarEventsRecurring _calendarEventSelected;
    private string _dateText, _holidayText;
    private ObservableCollection<CalendarEventsRecurring> _eventList;

    public EditRecurringVM() {
        fileName = ReferenceValues.FILE_DIRECTORY + "recurringDates.json";
        EventList = new ObservableCollection<CalendarEventsRecurring>();
        DateText = DateTime.Now.ToShortDateString();
        HolidayText = "";

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
                        JsonCalendarRecurring jsonCalendarRecurring = JsonSerializer.Deserialize<JsonCalendarRecurring>(eventsListString, options);

                        if (jsonCalendarRecurring != null) {
                            EventList = jsonCalendarRecurring.eventsListRecurring;
                        }
                    } catch (Exception e) {
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "WARN",
                            Module = "EditRecurringVM",
                            Description = e.ToString()
                        });
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditRecurringVM",
                    Description = e.ToString()
                });
            }
        }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        MessageBoxResult confirmation;

        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(HolidayText)) {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            } else {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditRecurringVM",
                    Description = "Adding recurring calendar event: " + Convert.ToDateTime(DateText).ToString("MM-dd") + ", " + HolidayText
                });

                EventList.Add(new CalendarEventsRecurring {
                    date = Convert.ToDateTime(DateText),
                    eventText = HolidayText
                });

                ReferenceValues.SoundToPlay = "birthday";
                SoundDispatcher.PlaySound();
                HolidayText = "";

                SaveJson();
            }

            break;
        case "update":
            try {
                if (CalendarEventSelected.eventText != null) {
                    if (string.IsNullOrWhiteSpace(HolidayText)) {
                        ReferenceValues.SoundToPlay = "missing_info";
                        SoundDispatcher.PlaySound();
                    } else if (!string.IsNullOrWhiteSpace(CalendarEventSelected.eventText)) {
                        confirmation = MessageBox.Show("Are you sure you want to update event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (confirmation == MessageBoxResult.Yes) {
                            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditRecurringVM",
                                Description = "Updating recurring calendar event: " + Convert.ToDateTime(DateText).ToString("MM-dd") + ", " + HolidayText
                            });

                            EventList.Insert(EventList.IndexOf(CalendarEventSelected), new CalendarEventsRecurring {
                                date = Convert.ToDateTime(DateText),
                                eventText = HolidayText
                            });
                            EventList.Remove(CalendarEventSelected);

                            ReferenceValues.SoundToPlay = "birthday";
                            SoundDispatcher.PlaySound();
                            HolidayText = "";

                            SaveJson();
                        }
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditRecurringVM",
                    Description = e.ToString()
                });
            }

            break;
        case "delete":
            try {
                if (CalendarEventSelected.eventText != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditRecurringVM",
                            Description = "Adding recurring calendar event: " + Convert.ToDateTime(DateText).ToString("MM-dd") + ", " + HolidayText
                        });

                        EventList.Remove(CalendarEventSelected);
                        HolidayText = "";

                        SaveJson();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditRecurringVM",
                    Description = e.ToString()
                });
            }

            break;
        }
    }

    private void PopulateDetailedView(CalendarEventsRecurring value) {
        DateText = value.date.ToShortDateString();
        HolidayText = value.eventText;
    }

    private void SaveJson() {
        try {
            ReferenceValues.JsonCalendarRecurringMaster.eventsListRecurring = EventList;
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonCalendarRecurringMaster);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditRecurringVM",
                Description = e.ToString()
            });
        }
    }

    #region Fields

    public string DateText {
        get => _dateText;
        set {
            if (string.IsNullOrWhiteSpace(value)) {
                value = DateTime.Now.ToShortDateString();
            }

            _dateText = value;
            RaisePropertyChangedEvent("DateText");
        }
    }

    public string HolidayText {
        get => _holidayText;
        set {
            _holidayText = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("HolidayText");
        }
    }

    public ObservableCollection<CalendarEventsRecurring> EventList {
        get => _eventList;
        set {
            _eventList = value;
            RaisePropertyChangedEvent("EventList");
        }
    }

    public CalendarEventsRecurring CalendarEventSelected {
        get => _calendarEventSelected;
        set {
            _calendarEventSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("EventSelected");
        }
    }

    #endregion
}