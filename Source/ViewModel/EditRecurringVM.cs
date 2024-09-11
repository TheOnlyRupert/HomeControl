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
    private string _dateText, _holidayText, _imageSelected;
    private ObservableCollection<CalendarEventsRecurring> _eventList;
    private ObservableCollection<string> _imageList;

    public EditRecurringVM() {
        EventList = ReferenceValues.JsonCalendarMaster.EventsListRecurring;
        DateText = DateTime.Now.ToShortDateString();
        HolidayText = "";

        ImageList = ReferenceValues.IconImageList;
        ImageSelected = "bathtub";
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private void ButtonLogic(object param) {
        MessageBoxResult confirmation;

        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(HolidayText)) {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            } else {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditRecurringVM",
                    Description = "Adding recurring calendar event: " + Convert.ToDateTime(DateText).ToString("MM-dd") + ", " + HolidayText
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                EventList.Add(new CalendarEventsRecurring {
                    Date = Convert.ToDateTime(DateText),
                    EventText = HolidayText,
                    Image = "../../../Resources/Images/icons/" + ImageSelected + ".png"
                });

                ReferenceValues.SoundToPlay = "birthday";
                SoundDispatcher.PlaySound();
                HolidayText = "";

                SaveJson();
            }

            break;
        case "update":
            try {
                if (CalendarEventSelected.EventText != null) {
                    if (string.IsNullOrWhiteSpace(HolidayText)) {
                        ReferenceValues.SoundToPlay = "missing_info";
                        SoundDispatcher.PlaySound();
                    } else if (!string.IsNullOrWhiteSpace(CalendarEventSelected.EventText)) {
                        confirmation = MessageBox.Show("Are you sure you want to update event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (confirmation == MessageBoxResult.Yes) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditRecurringVM",
                                Description = "Updating recurring calendar event: " + Convert.ToDateTime(DateText).ToString("MM-dd") + ", " + HolidayText
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            EventList.Insert(EventList.IndexOf(CalendarEventSelected), new CalendarEventsRecurring {
                                Date = Convert.ToDateTime(DateText),
                                EventText = HolidayText,
                                Image = "../../../Resources/Images/icons/" + ImageSelected + ".png"
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
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditRecurringVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case "delete":
            try {
                if (CalendarEventSelected.EventText != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditRecurringVM",
                            Description = "Adding recurring calendar event: " + Convert.ToDateTime(DateText).ToString("MM-dd") + ", " + HolidayText
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        EventList.Remove(CalendarEventSelected);
                        HolidayText = "";

                        SaveJson();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditRecurringVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        }
    }

    private void PopulateDetailedView(CalendarEventsRecurring value) {
        DateText = value.Date.ToShortDateString();
        HolidayText = value.EventText;
    }

    private void SaveJson() {
        try {
            ReferenceValues.JsonCalendarMaster.EventsListRecurring = EventList;
            FileHelpers.SaveFileText("calendar", JsonSerializer.Serialize(ReferenceValues.JsonCalendarMaster), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditRecurringVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
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
            _holidayText = value;
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

    public ObservableCollection<string> ImageList {
        get => _imageList;
        set {
            _imageList = value;
            RaisePropertyChangedEvent("ImageList");
        }
    }

    public string ImageSelected {
        get => _imageSelected;
        set {
            _imageSelected = value;
            RaisePropertyChangedEvent("ImageSelected");
        }
    }

    #endregion
}