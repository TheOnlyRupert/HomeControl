using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Calendar;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class CalendarVM : BaseViewModel {
    private int _button1BorderThickness, _button2BorderThickness, _button3BorderThickness, _button4BorderThickness, _button5BorderThickness, _button6BorderThickness, _button7BorderThickness,
        _button8BorderThickness, _button9BorderThickness, _button10BorderThickness, _button11BorderThickness, _button12BorderThickness, _button13BorderThickness, _button14BorderThickness,
        _button15BorderThickness, _button16BorderThickness, _button17BorderThickness, _button18BorderThickness, _button19BorderThickness, _button20BorderThickness, _button21BorderThickness,
        _button22BorderThickness, _button23BorderThickness, _button24BorderThickness, _button25BorderThickness, _button26BorderThickness, _button27BorderThickness, _button28BorderThickness,
        _button29BorderThickness, _button30BorderThickness, _button31BorderThickness, _button32BorderThickness, _button33BorderThickness, _button34BorderThickness, _button35BorderThickness,
        _button36BorderThickness, _button37BorderThickness, _button38BorderThickness, _button39BorderThickness, _button40BorderThickness, _button41BorderThickness, _button42BorderThickness;

    private string _button1Date, _button1HolidayText, _button2Date, _button2HolidayText, _button3Date, _button3HolidayText, _button4Date, _button4HolidayText, _button5Date,
        _button5HolidayText, _button6Date, _button6HolidayText, _button7Date, _button7HolidayText, _button8Date, _button8HolidayText, _button9Date, _button9HolidayText,
        _button10Date, _button10HolidayText, _button11Date, _button11HolidayText, _button12Date, _button12HolidayText, _button13Date, _button13HolidayText, _button14Date,
        _button14HolidayText, _button15Date, _button15HolidayText, _button16Date, _button16HolidayText, _button17Date, _button17HolidayText, _button18Date, _button18HolidayText,
        _button19Date, _button19HolidayText, _button20Date, _button20HolidayText, _button21Date, _button21HolidayText, _button22Date, _button22HolidayText, _button23Date,
        _button23HolidayText, _button24Date, _button24HolidayText, _button25Date, _button25HolidayText, _button26Date, _button26HolidayText, _button27Date, _button27HolidayText,
        _button28Date, _button28HolidayText, _button29Date, _button29HolidayText, _button30Date, _button30HolidayText, _button31Date, _button31HolidayText, _button32Date,
        _button32HolidayText, _button33Date, _button33HolidayText, _button34Date, _button34HolidayText, _button35Date, _button35HolidayText, _button36Date, _button36HolidayText,
        _button37Date, _button37HolidayText, _button38Date, _button38HolidayText, _button39Date, _button39HolidayText, _button40Date, _button40HolidayText, _button41Date,
        _button41HolidayText, _button42Date, _button42HolidayText, _currentMonthAndYear, _button1BorderColor, _button2BorderColor, _button3BorderColor, _button4BorderColor, _button5BorderColor,
        _button6BorderColor, _button7BorderColor, _button8BorderColor, _button9BorderColor, _button10BorderColor, _button11BorderColor, _button12BorderColor, _button13BorderColor,
        _button14BorderColor, _button15BorderColor, _button16BorderColor, _button17BorderColor, _button18BorderColor, _button19BorderColor, _button20BorderColor, _button21BorderColor,
        _button22BorderColor, _button23BorderColor, _button24BorderColor, _button25BorderColor, _button26BorderColor, _button27BorderColor, _button28BorderColor, _button29BorderColor,
        _button30BorderColor, _button31BorderColor, _button32BorderColor, _button33BorderColor, _button34BorderColor, _button35BorderColor, _button36BorderColor, _button37BorderColor,
        _button38BorderColor, _button39BorderColor, _button40BorderColor, _button41BorderColor, _button42BorderColor;

    private ObservableCollection<CalendarEventsCustom> _button1EventList, _button2EventList, _button3EventList, _button4EventList, _button5EventList, _button6EventList,
        _button7EventList, _button8EventList, _button9EventList, _button10EventList, _button11EventList, _button12EventList, _button13EventList, _button14EventList,
        _button15EventList, _button16EventList, _button17EventList, _button18EventList, _button19EventList, _button20EventList, _button21EventList, _button22EventList,
        _button23EventList, _button24EventList, _button25EventList, _button26EventList, _button27EventList, _button28EventList, _button29EventList, _button30EventList,
        _button31EventList, _button32EventList, _button33EventList, _button34EventList, _button35EventList, _button36EventList, _button37EventList, _button38EventList,
        _button39EventList, _button40EventList, _button41EventList, _button42EventList;

    private DateTime button1DateTime, calendarDate;

    public CalendarVM() {
        try {
            ReferenceValues.JsonCalendarMaster = JsonSerializer.Deserialize<JsonCalendar>(FileHelpers.LoadFileText("calendar", true));
        } catch (Exception) {
            ReferenceValues.JsonCalendarMaster = new JsonCalendar {
                DatesList = new ObservableCollection<CalendarDates>(),
                EventsListRecurring = new ObservableCollection<CalendarEventsRecurring>()
            };

            FileHelpers.SaveFileText("calendar", JsonSerializer.Serialize(ReferenceValues.JsonCalendarMaster), true);
        }

        Button1EventList = new ObservableCollection<CalendarEventsCustom>();
        Button2EventList = new ObservableCollection<CalendarEventsCustom>();
        Button3EventList = new ObservableCollection<CalendarEventsCustom>();
        Button4EventList = new ObservableCollection<CalendarEventsCustom>();
        Button5EventList = new ObservableCollection<CalendarEventsCustom>();
        Button6EventList = new ObservableCollection<CalendarEventsCustom>();
        Button7EventList = new ObservableCollection<CalendarEventsCustom>();
        Button8EventList = new ObservableCollection<CalendarEventsCustom>();
        Button9EventList = new ObservableCollection<CalendarEventsCustom>();
        Button10EventList = new ObservableCollection<CalendarEventsCustom>();
        Button11EventList = new ObservableCollection<CalendarEventsCustom>();
        Button12EventList = new ObservableCollection<CalendarEventsCustom>();
        Button13EventList = new ObservableCollection<CalendarEventsCustom>();
        Button14EventList = new ObservableCollection<CalendarEventsCustom>();
        Button15EventList = new ObservableCollection<CalendarEventsCustom>();
        Button16EventList = new ObservableCollection<CalendarEventsCustom>();
        Button17EventList = new ObservableCollection<CalendarEventsCustom>();
        Button18EventList = new ObservableCollection<CalendarEventsCustom>();
        Button19EventList = new ObservableCollection<CalendarEventsCustom>();
        Button20EventList = new ObservableCollection<CalendarEventsCustom>();
        Button21EventList = new ObservableCollection<CalendarEventsCustom>();
        Button22EventList = new ObservableCollection<CalendarEventsCustom>();
        Button23EventList = new ObservableCollection<CalendarEventsCustom>();
        Button24EventList = new ObservableCollection<CalendarEventsCustom>();
        Button25EventList = new ObservableCollection<CalendarEventsCustom>();
        Button26EventList = new ObservableCollection<CalendarEventsCustom>();
        Button27EventList = new ObservableCollection<CalendarEventsCustom>();
        Button28EventList = new ObservableCollection<CalendarEventsCustom>();
        Button29EventList = new ObservableCollection<CalendarEventsCustom>();
        Button30EventList = new ObservableCollection<CalendarEventsCustom>();
        Button31EventList = new ObservableCollection<CalendarEventsCustom>();
        Button32EventList = new ObservableCollection<CalendarEventsCustom>();
        Button33EventList = new ObservableCollection<CalendarEventsCustom>();
        Button34EventList = new ObservableCollection<CalendarEventsCustom>();
        Button35EventList = new ObservableCollection<CalendarEventsCustom>();
        Button36EventList = new ObservableCollection<CalendarEventsCustom>();
        Button37EventList = new ObservableCollection<CalendarEventsCustom>();
        Button38EventList = new ObservableCollection<CalendarEventsCustom>();
        Button39EventList = new ObservableCollection<CalendarEventsCustom>();
        Button40EventList = new ObservableCollection<CalendarEventsCustom>();
        Button41EventList = new ObservableCollection<CalendarEventsCustom>();
        Button42EventList = new ObservableCollection<CalendarEventsCustom>();

        calendarDate = DateTime.Now;
        CurrentMonthAndYear = calendarDate.ToString("MMMM, yyyy");
        PopulateCalendar(calendarDate);

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            calendarDate = DateTime.Now;
            CurrentMonthAndYear = calendarDate.ToString("MMMM, yyyy");
            PopulateCalendar(DateTime.Now);
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "addMonth":
            calendarDate = calendarDate.AddMonths(1);
            CurrentMonthAndYear = calendarDate.ToString("MMMM, yyyy");
            PopulateCalendar(calendarDate);
            break;
        case "subMonth":
            calendarDate = calendarDate.AddMonths(-1);
            CurrentMonthAndYear = calendarDate.ToString("MMMM, yyyy");
            PopulateCalendar(calendarDate);
            break;
        case "today":
            calendarDate = DateTime.Now;
            CurrentMonthAndYear = calendarDate.ToString("MMMM, yyyy");
            PopulateCalendar(calendarDate);
            break;
        case "recurring":
            if (!ReferenceValues.LockUI) {
                EditRecurring editRecurring = new();
                editRecurring.ShowDialog();
                editRecurring.Close();
                PopulateCalendar(calendarDate);
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button1":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime;
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button2":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(1);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button3":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(2);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button4":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(3);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button5":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(4);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button6":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(5);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button7":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(6);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button8":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(7);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button9":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(8);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button10":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(9);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button11":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(10);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button12":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(11);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button13":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(12);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button14":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(13);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button15":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(14);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button16":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(15);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button17":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(16);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button18":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(17);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button19":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(18);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button20":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(19);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button21":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(20);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button22":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(21);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button23":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(22);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button24":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(23);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button25":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(24);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button26":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(25);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button27":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(26);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button28":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(27);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button29":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(28);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button30":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(29);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button31":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(30);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button32":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(31);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button33":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(32);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button34":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(33);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button35":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(34);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button36":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(35);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button37":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(36);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button38":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(37);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button39":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(38);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button40":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(39);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button41":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(40);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        case "button42":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(41);
                OpenEventDialog();
            } else {
                ReferenceValues.SoundToPlay = "locked";
                SoundDispatcher.PlaySound();
            }

            break;
        }
    }

    private void OpenEventDialog() {
        EditCalendar editCalendar = new();
        editCalendar.ShowDialog();
        editCalendar.Close();
        PopulateCalendar(calendarDate);
    }

    private void PopulateCalendar(DateTime calendarStartingDate) {
        Button1HolidayText = "";
        Button2HolidayText = "";
        Button3HolidayText = "";
        Button4HolidayText = "";
        Button5HolidayText = "";
        Button6HolidayText = "";
        Button7HolidayText = "";
        Button8HolidayText = "";
        Button9HolidayText = "";
        Button10HolidayText = "";
        Button11HolidayText = "";
        Button12HolidayText = "";
        Button13HolidayText = "";
        Button14HolidayText = "";
        Button15HolidayText = "";
        Button16HolidayText = "";
        Button17HolidayText = "";
        Button18HolidayText = "";
        Button19HolidayText = "";
        Button20HolidayText = "";
        Button21HolidayText = "";
        Button22HolidayText = "";
        Button23HolidayText = "";
        Button24HolidayText = "";
        Button25HolidayText = "";
        Button26HolidayText = "";
        Button27HolidayText = "";
        Button28HolidayText = "";
        Button29HolidayText = "";
        Button30HolidayText = "";
        Button31HolidayText = "";
        Button32HolidayText = "";
        Button33HolidayText = "";
        Button34HolidayText = "";
        Button35HolidayText = "";
        Button36HolidayText = "";
        Button37HolidayText = "";
        Button38HolidayText = "";
        Button39HolidayText = "";
        Button40HolidayText = "";
        Button41HolidayText = "";
        Button42HolidayText = "";
        Button1BorderColor = "DarkSlateGray";
        Button2BorderColor = "DarkSlateGray";
        Button3BorderColor = "DarkSlateGray";
        Button4BorderColor = "DarkSlateGray";
        Button5BorderColor = "DarkSlateGray";
        Button6BorderColor = "DarkSlateGray";
        Button7BorderColor = "DarkSlateGray";
        Button8BorderColor = "DarkSlateGray";
        Button9BorderColor = "DarkSlateGray";
        Button10BorderColor = "DarkSlateGray";
        Button11BorderColor = "DarkSlateGray";
        Button12BorderColor = "DarkSlateGray";
        Button13BorderColor = "DarkSlateGray";
        Button14BorderColor = "DarkSlateGray";
        Button15BorderColor = "DarkSlateGray";
        Button16BorderColor = "DarkSlateGray";
        Button17BorderColor = "DarkSlateGray";
        Button18BorderColor = "DarkSlateGray";
        Button19BorderColor = "DarkSlateGray";
        Button20BorderColor = "DarkSlateGray";
        Button21BorderColor = "DarkSlateGray";
        Button22BorderColor = "DarkSlateGray";
        Button23BorderColor = "DarkSlateGray";
        Button24BorderColor = "DarkSlateGray";
        Button25BorderColor = "DarkSlateGray";
        Button26BorderColor = "DarkSlateGray";
        Button27BorderColor = "DarkSlateGray";
        Button28BorderColor = "DarkSlateGray";
        Button29BorderColor = "DarkSlateGray";
        Button30BorderColor = "DarkSlateGray";
        Button31BorderColor = "DarkSlateGray";
        Button32BorderColor = "DarkSlateGray";
        Button33BorderColor = "DarkSlateGray";
        Button34BorderColor = "DarkSlateGray";
        Button35BorderColor = "DarkSlateGray";
        Button36BorderColor = "DarkSlateGray";
        Button37BorderColor = "DarkSlateGray";
        Button38BorderColor = "DarkSlateGray";
        Button39BorderColor = "DarkSlateGray";
        Button40BorderColor = "DarkSlateGray";
        Button41BorderColor = "DarkSlateGray";
        Button42BorderColor = "DarkSlateGray";
        Button1BorderThickness = 1;
        Button2BorderThickness = 1;
        Button3BorderThickness = 1;
        Button4BorderThickness = 1;
        Button5BorderThickness = 1;
        Button6BorderThickness = 1;
        Button7BorderThickness = 1;
        Button8BorderThickness = 1;
        Button9BorderThickness = 1;
        Button10BorderThickness = 1;
        Button11BorderThickness = 1;
        Button12BorderThickness = 1;
        Button13BorderThickness = 1;
        Button14BorderThickness = 1;
        Button15BorderThickness = 1;
        Button16BorderThickness = 1;
        Button17BorderThickness = 1;
        Button18BorderThickness = 1;
        Button19BorderThickness = 1;
        Button20BorderThickness = 1;
        Button21BorderThickness = 1;
        Button22BorderThickness = 1;
        Button23BorderThickness = 1;
        Button24BorderThickness = 1;
        Button25BorderThickness = 1;
        Button26BorderThickness = 1;
        Button27BorderThickness = 1;
        Button28BorderThickness = 1;
        Button29BorderThickness = 1;
        Button30BorderThickness = 1;
        Button31BorderThickness = 1;
        Button32BorderThickness = 1;
        Button33BorderThickness = 1;
        Button34BorderThickness = 1;
        Button35BorderThickness = 1;
        Button36BorderThickness = 1;
        Button37BorderThickness = 1;
        Button38BorderThickness = 1;
        Button39BorderThickness = 1;
        Button40BorderThickness = 1;
        Button41BorderThickness = 1;
        Button42BorderThickness = 1;
        Button1EventList.Clear();
        Button2EventList.Clear();
        Button3EventList.Clear();
        Button4EventList.Clear();
        Button5EventList.Clear();
        Button6EventList.Clear();
        Button7EventList.Clear();
        Button8EventList.Clear();
        Button9EventList.Clear();
        Button10EventList.Clear();
        Button11EventList.Clear();
        Button12EventList.Clear();
        Button13EventList.Clear();
        Button14EventList.Clear();
        Button15EventList.Clear();
        Button16EventList.Clear();
        Button17EventList.Clear();
        Button18EventList.Clear();
        Button19EventList.Clear();
        Button20EventList.Clear();
        Button21EventList.Clear();
        Button22EventList.Clear();
        Button23EventList.Clear();
        Button24EventList.Clear();
        Button25EventList.Clear();
        Button26EventList.Clear();
        Button27EventList.Clear();
        Button28EventList.Clear();
        Button29EventList.Clear();
        Button30EventList.Clear();
        Button31EventList.Clear();
        Button32EventList.Clear();
        Button33EventList.Clear();
        Button34EventList.Clear();
        Button35EventList.Clear();
        Button36EventList.Clear();
        Button37EventList.Clear();
        Button38EventList.Clear();
        Button39EventList.Clear();
        Button40EventList.Clear();
        Button41EventList.Clear();
        Button42EventList.Clear();

        /* Constant */
        DateTime startingYear = new(calendarStartingDate.Year, 1, 1);

        /* Add Years */
        while (startingYear.DayOfWeek != DayOfWeek.Sunday) {
            startingYear = startingYear.AddDays(-1);
        }

        /* Add weeks */
        int mathDate = (calendarStartingDate.Month - 1) * 28;
        while (startingYear.AddDays(mathDate + 6).Month != calendarStartingDate.Month) {
            mathDate += 7;
        }

        /* Adjust Calendar with new days */
        calendarStartingDate = startingYear.AddDays(mathDate);

        Button1Date = calendarStartingDate.ToString("MMM/dd");
        Button2Date = calendarStartingDate.AddDays(1).ToString("MMM/dd");
        Button3Date = calendarStartingDate.AddDays(2).ToString("MMM/dd");
        Button4Date = calendarStartingDate.AddDays(3).ToString("MMM/dd");
        Button5Date = calendarStartingDate.AddDays(4).ToString("MMM/dd");
        Button6Date = calendarStartingDate.AddDays(5).ToString("MMM/dd");
        Button7Date = calendarStartingDate.AddDays(6).ToString("MMM/dd");
        Button8Date = calendarStartingDate.AddDays(7).ToString("MMM/dd");
        Button9Date = calendarStartingDate.AddDays(8).ToString("MMM/dd");
        Button10Date = calendarStartingDate.AddDays(9).ToString("MMM/dd");
        Button11Date = calendarStartingDate.AddDays(10).ToString("MMM/dd");
        Button12Date = calendarStartingDate.AddDays(11).ToString("MMM/dd");
        Button13Date = calendarStartingDate.AddDays(12).ToString("MMM/dd");
        Button14Date = calendarStartingDate.AddDays(13).ToString("MMM/dd");
        Button15Date = calendarStartingDate.AddDays(14).ToString("MMM/dd");
        Button16Date = calendarStartingDate.AddDays(15).ToString("MMM/dd");
        Button17Date = calendarStartingDate.AddDays(16).ToString("MMM/dd");
        Button18Date = calendarStartingDate.AddDays(17).ToString("MMM/dd");
        Button19Date = calendarStartingDate.AddDays(18).ToString("MMM/dd");
        Button20Date = calendarStartingDate.AddDays(19).ToString("MMM/dd");
        Button21Date = calendarStartingDate.AddDays(20).ToString("MMM/dd");
        Button22Date = calendarStartingDate.AddDays(21).ToString("MMM/dd");
        Button23Date = calendarStartingDate.AddDays(22).ToString("MMM/dd");
        Button24Date = calendarStartingDate.AddDays(23).ToString("MMM/dd");
        Button25Date = calendarStartingDate.AddDays(24).ToString("MMM/dd");
        Button26Date = calendarStartingDate.AddDays(25).ToString("MMM/dd");
        Button27Date = calendarStartingDate.AddDays(26).ToString("MMM/dd");
        Button28Date = calendarStartingDate.AddDays(27).ToString("MMM/dd");
        Button29Date = calendarStartingDate.AddDays(28).ToString("MMM/dd");
        Button30Date = calendarStartingDate.AddDays(29).ToString("MMM/dd");
        Button31Date = calendarStartingDate.AddDays(30).ToString("MMM/dd");
        Button32Date = calendarStartingDate.AddDays(31).ToString("MMM/dd");
        Button33Date = calendarStartingDate.AddDays(32).ToString("MMM/dd");
        Button34Date = calendarStartingDate.AddDays(33).ToString("MMM/dd");
        Button35Date = calendarStartingDate.AddDays(34).ToString("MMM/dd");
        Button36Date = calendarStartingDate.AddDays(35).ToString("MMM/dd");
        Button37Date = calendarStartingDate.AddDays(36).ToString("MMM/dd");
        Button38Date = calendarStartingDate.AddDays(37).ToString("MMM/dd");
        Button39Date = calendarStartingDate.AddDays(38).ToString("MMM/dd");
        Button40Date = calendarStartingDate.AddDays(39).ToString("MMM/dd");
        Button41Date = calendarStartingDate.AddDays(40).ToString("MMM/dd");
        Button42Date = calendarStartingDate.AddDays(41).ToString("MMM/dd");

        /* Oct 10 2022 - Probably only need this first button. All other buttons can just add days */
        /* Sep 09 2023 - I was right! I'm smart as fuck! */
        button1DateTime = calendarStartingDate;

        /* Get Holidays (Hardcoded) */
        foreach (Holidays.HolidayBlock holiday in Holidays.GetHolidays(calendarStartingDate.AddDays(7).Year)) {
            if (calendarStartingDate.Month == holiday.Date.Month && calendarStartingDate.Day == holiday.Date.Day) {
                Button1HolidayText = holiday.Holiday;
                if (Button1HolidayText.Length > 0) {
                    Button1BorderColor = "Red";
                    Button1BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(1).Month == holiday.Date.Month && calendarStartingDate.AddDays(1).Day == holiday.Date.Day) {
                Button2HolidayText = holiday.Holiday;
                if (Button2HolidayText.Length > 0) {
                    Button2BorderColor = "Red";
                    Button2BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(2).Month == holiday.Date.Month && calendarStartingDate.AddDays(2).Day == holiday.Date.Day) {
                Button3HolidayText = holiday.Holiday;
                if (Button3HolidayText.Length > 0) {
                    Button3BorderColor = "Red";
                    Button3BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(3).Month == holiday.Date.Month && calendarStartingDate.AddDays(3).Day == holiday.Date.Day) {
                Button4HolidayText = holiday.Holiday;
                if (Button4HolidayText.Length > 0) {
                    Button4BorderColor = "Red";
                    Button4BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(4).Month == holiday.Date.Month && calendarStartingDate.AddDays(4).Day == holiday.Date.Day) {
                Button5HolidayText = holiday.Holiday;
                if (Button5HolidayText.Length > 0) {
                    Button5BorderColor = "Red";
                    Button5BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(5).Month == holiday.Date.Month && calendarStartingDate.AddDays(5).Day == holiday.Date.Day) {
                Button6HolidayText = holiday.Holiday;
                if (Button6HolidayText.Length > 0) {
                    Button6BorderColor = "Red";
                    Button6BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(6).Month == holiday.Date.Month && calendarStartingDate.AddDays(6).Day == holiday.Date.Day) {
                Button7HolidayText = holiday.Holiday;
                if (Button7HolidayText.Length > 0) {
                    Button7BorderColor = "Red";
                    Button7BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(7).Month == holiday.Date.Month && calendarStartingDate.AddDays(7).Day == holiday.Date.Day) {
                Button8HolidayText = holiday.Holiday;
                if (Button8HolidayText.Length > 0) {
                    Button8BorderColor = "Red";
                    Button8BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(8).Month == holiday.Date.Month && calendarStartingDate.AddDays(8).Day == holiday.Date.Day) {
                Button9HolidayText = holiday.Holiday;
                if (Button9HolidayText.Length > 0) {
                    Button9BorderColor = "Red";
                    Button9BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(9).Month == holiday.Date.Month && calendarStartingDate.AddDays(9).Day == holiday.Date.Day) {
                Button10HolidayText = holiday.Holiday;
                if (Button10HolidayText.Length > 0) {
                    Button10BorderColor = "Red";
                    Button10BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(10).Month == holiday.Date.Month && calendarStartingDate.AddDays(10).Day == holiday.Date.Day) {
                Button11HolidayText = holiday.Holiday;
                if (Button11HolidayText.Length > 0) {
                    Button11BorderColor = "Red";
                    Button11BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(11).Month == holiday.Date.Month && calendarStartingDate.AddDays(11).Day == holiday.Date.Day) {
                Button12HolidayText = holiday.Holiday;
                if (Button12HolidayText.Length > 0) {
                    Button12BorderColor = "Red";
                    Button12BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(12).Month == holiday.Date.Month && calendarStartingDate.AddDays(12).Day == holiday.Date.Day) {
                Button13HolidayText = holiday.Holiday;
                if (Button13HolidayText.Length > 0) {
                    Button13BorderColor = "Red";
                    Button13BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(13).Month == holiday.Date.Month && calendarStartingDate.AddDays(13).Day == holiday.Date.Day) {
                Button14HolidayText = holiday.Holiday;
                if (Button14HolidayText.Length > 0) {
                    Button14BorderColor = "Red";
                    Button14BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(14).Month == holiday.Date.Month && calendarStartingDate.AddDays(14).Day == holiday.Date.Day) {
                Button15HolidayText = holiday.Holiday;
                if (Button15HolidayText.Length > 0) {
                    Button15BorderColor = "Red";
                    Button15BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(15).Month == holiday.Date.Month && calendarStartingDate.AddDays(15).Day == holiday.Date.Day) {
                Button16HolidayText = holiday.Holiday;
                if (Button16HolidayText.Length > 0) {
                    Button16BorderColor = "Red";
                    Button16BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(16).Month == holiday.Date.Month && calendarStartingDate.AddDays(16).Day == holiday.Date.Day) {
                Button17HolidayText = holiday.Holiday;
                if (Button17HolidayText.Length > 0) {
                    Button17BorderColor = "Red";
                    Button17BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(17).Month == holiday.Date.Month && calendarStartingDate.AddDays(17).Day == holiday.Date.Day) {
                Button18HolidayText = holiday.Holiday;
                if (Button18HolidayText.Length > 0) {
                    Button18BorderColor = "Red";
                    Button18BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(18).Month == holiday.Date.Month && calendarStartingDate.AddDays(18).Day == holiday.Date.Day) {
                Button19HolidayText = holiday.Holiday;
                if (Button19HolidayText.Length > 0) {
                    Button19BorderColor = "Red";
                    Button19BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(19).Month == holiday.Date.Month && calendarStartingDate.AddDays(19).Day == holiday.Date.Day) {
                Button20HolidayText = holiday.Holiday;
                if (Button20HolidayText.Length > 0) {
                    Button20BorderColor = "Red";
                    Button20BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(20).Month == holiday.Date.Month && calendarStartingDate.AddDays(20).Day == holiday.Date.Day) {
                Button21HolidayText = holiday.Holiday;
                if (Button21HolidayText.Length > 0) {
                    Button21BorderColor = "Red";
                    Button21BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(21).Month == holiday.Date.Month && calendarStartingDate.AddDays(21).Day == holiday.Date.Day) {
                Button22HolidayText = holiday.Holiday;
                if (Button22HolidayText.Length > 0) {
                    Button22BorderColor = "Red";
                    Button22BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(22).Month == holiday.Date.Month && calendarStartingDate.AddDays(22).Day == holiday.Date.Day) {
                Button23HolidayText = holiday.Holiday;
                if (Button23HolidayText.Length > 0) {
                    Button23BorderColor = "Red";
                    Button23BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(23).Month == holiday.Date.Month && calendarStartingDate.AddDays(23).Day == holiday.Date.Day) {
                Button24HolidayText = holiday.Holiday;
                if (Button24HolidayText.Length > 0) {
                    Button24BorderColor = "Red";
                    Button24BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(24).Month == holiday.Date.Month && calendarStartingDate.AddDays(24).Day == holiday.Date.Day) {
                Button25HolidayText = holiday.Holiday;
                if (Button25HolidayText.Length > 0) {
                    Button25BorderColor = "Red";
                    Button25BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(25).Month == holiday.Date.Month && calendarStartingDate.AddDays(25).Day == holiday.Date.Day) {
                Button26HolidayText = holiday.Holiday;
                if (Button26HolidayText.Length > 0) {
                    Button26BorderColor = "Red";
                    Button26BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(26).Month == holiday.Date.Month && calendarStartingDate.AddDays(26).Day == holiday.Date.Day) {
                Button27HolidayText = holiday.Holiday;
                if (Button27HolidayText.Length > 0) {
                    Button27BorderColor = "Red";
                    Button27BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(27).Month == holiday.Date.Month && calendarStartingDate.AddDays(27).Day == holiday.Date.Day) {
                Button28HolidayText = holiday.Holiday;
                if (Button28HolidayText.Length > 0) {
                    Button28BorderColor = "Red";
                    Button28BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(28).Month == holiday.Date.Month && calendarStartingDate.AddDays(28).Day == holiday.Date.Day) {
                Button29HolidayText = holiday.Holiday;
                if (Button29HolidayText.Length > 0) {
                    Button29BorderColor = "Red";
                    Button29BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(29).Month == holiday.Date.Month && calendarStartingDate.AddDays(29).Day == holiday.Date.Day) {
                Button30HolidayText = holiday.Holiday;
                if (Button30HolidayText.Length > 0) {
                    Button30BorderColor = "Red";
                    Button30BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(30).Month == holiday.Date.Month && calendarStartingDate.AddDays(30).Day == holiday.Date.Day) {
                Button31HolidayText = holiday.Holiday;
                if (Button31HolidayText.Length > 0) {
                    Button31BorderColor = "Red";
                    Button31BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(31).Month == holiday.Date.Month && calendarStartingDate.AddDays(31).Day == holiday.Date.Day) {
                Button32HolidayText = holiday.Holiday;
                if (Button32HolidayText.Length > 0) {
                    Button32BorderColor = "Red";
                    Button32BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(32).Month == holiday.Date.Month && calendarStartingDate.AddDays(32).Day == holiday.Date.Day) {
                Button33HolidayText = holiday.Holiday;
                if (Button33HolidayText.Length > 0) {
                    Button33BorderColor = "Red";
                    Button33BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(33).Month == holiday.Date.Month && calendarStartingDate.AddDays(33).Day == holiday.Date.Day) {
                Button34HolidayText = holiday.Holiday;
                if (Button34HolidayText.Length > 0) {
                    Button34BorderColor = "Red";
                    Button34BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(34).Month == holiday.Date.Month && calendarStartingDate.AddDays(34).Day == holiday.Date.Day) {
                Button35HolidayText = holiday.Holiday;
                if (Button35HolidayText.Length > 0) {
                    Button35BorderColor = "Red";
                    Button35BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(35).Month == holiday.Date.Month && calendarStartingDate.AddDays(35).Day == holiday.Date.Day) {
                Button36HolidayText = holiday.Holiday;
                if (Button36HolidayText.Length > 0) {
                    Button36BorderColor = "Red";
                    Button36BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(36).Month == holiday.Date.Month && calendarStartingDate.AddDays(36).Day == holiday.Date.Day) {
                Button37HolidayText = holiday.Holiday;
                if (Button37HolidayText.Length > 0) {
                    Button37BorderColor = "Red";
                    Button37BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(37).Month == holiday.Date.Month && calendarStartingDate.AddDays(37).Day == holiday.Date.Day) {
                Button38HolidayText = holiday.Holiday;
                if (Button38HolidayText.Length > 0) {
                    Button38BorderColor = "Red";
                    Button38BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(38).Month == holiday.Date.Month && calendarStartingDate.AddDays(38).Day == holiday.Date.Day) {
                Button39HolidayText = holiday.Holiday;
                if (Button39HolidayText.Length > 0) {
                    Button39BorderColor = "Red";
                    Button39BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(39).Month == holiday.Date.Month && calendarStartingDate.AddDays(39).Day == holiday.Date.Day) {
                Button40HolidayText = holiday.Holiday;
                if (Button40HolidayText.Length > 0) {
                    Button40BorderColor = "Red";
                    Button40BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(40).Month == holiday.Date.Month && calendarStartingDate.AddDays(40).Day == holiday.Date.Day) {
                Button41HolidayText = holiday.Holiday;
                if (Button41HolidayText.Length > 0) {
                    Button41BorderColor = "Red";
                    Button41BorderThickness = 2;
                }
            }

            if (calendarStartingDate.AddDays(41).Month == holiday.Date.Month && calendarStartingDate.AddDays(41).Day == holiday.Date.Day) {
                Button42HolidayText = holiday.Holiday;
                if (Button42HolidayText.Length > 0) {
                    Button42BorderColor = "Red";
                    Button42BorderThickness = 2;
                }
            }
        }

        /* Set background color for today */
        if (calendarStartingDate.Equals(DateTime.Today)) {
            Button1BorderColor = "Yellow";
            Button1BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(1).Equals(DateTime.Today)) {
            Button2BorderColor = "Yellow";
            Button2BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(2).Equals(DateTime.Today)) {
            Button3BorderColor = "Yellow";
            Button3BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(3).Equals(DateTime.Today)) {
            Button4BorderColor = "Yellow";
            Button4BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(4).Equals(DateTime.Today)) {
            Button5BorderColor = "Yellow";
            Button5BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(5).Equals(DateTime.Today)) {
            Button6BorderColor = "Yellow";
            Button6BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(6).Equals(DateTime.Today)) {
            Button7BorderColor = "Yellow";
            Button7BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(7).Equals(DateTime.Today)) {
            Button8BorderColor = "Yellow";
            Button8BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(8).Equals(DateTime.Today)) {
            Button9BorderColor = "Yellow";
            Button9BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(9).Equals(DateTime.Today)) {
            Button10BorderColor = "Yellow";
            Button10BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(10).Equals(DateTime.Today)) {
            Button11BorderColor = "Yellow";
            Button11BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(11).Equals(DateTime.Today)) {
            Button12BorderColor = "Yellow";
            Button12BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(12).Equals(DateTime.Today)) {
            Button13BorderColor = "Yellow";
            Button13BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(13).Equals(DateTime.Today)) {
            Button14BorderColor = "Yellow";
            Button14BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(14).Equals(DateTime.Today)) {
            Button15BorderColor = "Yellow";
            Button15BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(15).Equals(DateTime.Today)) {
            Button16BorderColor = "Yellow";
            Button16BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(16).Equals(DateTime.Today)) {
            Button17BorderColor = "Yellow";
            Button17BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(17).Equals(DateTime.Today)) {
            Button18BorderColor = "Yellow";
            Button18BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(18).Equals(DateTime.Today)) {
            Button19BorderColor = "Yellow";
            Button19BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(19).Equals(DateTime.Today)) {
            Button20BorderColor = "Yellow";
            Button20BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(20).Equals(DateTime.Today)) {
            Button21BorderColor = "Yellow";
            Button21BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(21).Equals(DateTime.Today)) {
            Button22BorderColor = "Yellow";
            Button22BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(22).Equals(DateTime.Today)) {
            Button23BorderColor = "Yellow";
            Button23BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(23).Equals(DateTime.Today)) {
            Button24BorderColor = "Yellow";
            Button24BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(24).Equals(DateTime.Today)) {
            Button25BorderColor = "Yellow";
            Button25BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(25).Equals(DateTime.Today)) {
            Button26BorderColor = "Yellow";
            Button26BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(26).Equals(DateTime.Today)) {
            Button27BorderColor = "Yellow";
            Button27BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(27).Equals(DateTime.Today)) {
            Button28BorderColor = "Yellow";
            Button28BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(28).Equals(DateTime.Today)) {
            Button29BorderColor = "Yellow";
            Button29BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(29).Equals(DateTime.Today)) {
            Button30BorderColor = "Yellow";
            Button30BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(30).Equals(DateTime.Today)) {
            Button31BorderColor = "Yellow";
            Button31BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(31).Equals(DateTime.Today)) {
            Button32BorderColor = "Yellow";
            Button32BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(32).Equals(DateTime.Today)) {
            Button33BorderColor = "Yellow";
            Button33BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(33).Equals(DateTime.Today)) {
            Button34BorderColor = "Yellow";
            Button34BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(34).Equals(DateTime.Today)) {
            Button35BorderColor = "Yellow";
            Button35BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(35).Equals(DateTime.Today)) {
            Button36BorderColor = "Yellow";
            Button36BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(36).Equals(DateTime.Today)) {
            Button37BorderColor = "Yellow";
            Button37BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(37).Equals(DateTime.Today)) {
            Button38BorderColor = "Yellow";
            Button38BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(38).Equals(DateTime.Today)) {
            Button39BorderColor = "Yellow";
            Button39BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(39).Equals(DateTime.Today)) {
            Button40BorderColor = "Yellow";
            Button40BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(40).Equals(DateTime.Today)) {
            Button41BorderColor = "Yellow";
            Button41BorderThickness = 4;
        } else if (calendarStartingDate.AddDays(41).Equals(DateTime.Today)) {
            Button42BorderColor = "Yellow";
            Button42BorderThickness = 4;
        }

        /* Recurring Events */
        foreach (CalendarEventsRecurring calendar in ReferenceValues.JsonCalendarMaster.EventsListRecurring) {
            if (calendarStartingDate.Month == calendar.Date.Month && calendarStartingDate.Day == calendar.Date.Day) {
                Button1EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(1).Month == calendar.Date.Month && calendarStartingDate.AddDays(1).Day == calendar.Date.Day) {
                Button2EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(2).Month == calendar.Date.Month && calendarStartingDate.AddDays(2).Day == calendar.Date.Day) {
                Button3EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(3).Month == calendar.Date.Month && calendarStartingDate.AddDays(3).Day == calendar.Date.Day) {
                Button4EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(4).Month == calendar.Date.Month && calendarStartingDate.AddDays(4).Day == calendar.Date.Day) {
                Button5EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(5).Month == calendar.Date.Month && calendarStartingDate.AddDays(5).Day == calendar.Date.Day) {
                Button6EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(6).Month == calendar.Date.Month && calendarStartingDate.AddDays(6).Day == calendar.Date.Day) {
                Button7EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(7).Month == calendar.Date.Month && calendarStartingDate.AddDays(7).Day == calendar.Date.Day) {
                Button8EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(8).Month == calendar.Date.Month && calendarStartingDate.AddDays(8).Day == calendar.Date.Day) {
                Button9EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(9).Month == calendar.Date.Month && calendarStartingDate.AddDays(9).Day == calendar.Date.Day) {
                Button10EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(10).Month == calendar.Date.Month && calendarStartingDate.AddDays(10).Day == calendar.Date.Day) {
                Button11EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(11).Month == calendar.Date.Month && calendarStartingDate.AddDays(11).Day == calendar.Date.Day) {
                Button12EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(12).Month == calendar.Date.Month && calendarStartingDate.AddDays(12).Day == calendar.Date.Day) {
                Button13EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(13).Month == calendar.Date.Month && calendarStartingDate.AddDays(13).Day == calendar.Date.Day) {
                Button14EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(14).Month == calendar.Date.Month && calendarStartingDate.AddDays(14).Day == calendar.Date.Day) {
                Button15EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(15).Month == calendar.Date.Month && calendarStartingDate.AddDays(15).Day == calendar.Date.Day) {
                Button16EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(16).Month == calendar.Date.Month && calendarStartingDate.AddDays(16).Day == calendar.Date.Day) {
                Button17EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(17).Month == calendar.Date.Month && calendarStartingDate.AddDays(17).Day == calendar.Date.Day) {
                Button18EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(18).Month == calendar.Date.Month && calendarStartingDate.AddDays(18).Day == calendar.Date.Day) {
                Button19EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(19).Month == calendar.Date.Month && calendarStartingDate.AddDays(19).Day == calendar.Date.Day) {
                Button20EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(20).Month == calendar.Date.Month && calendarStartingDate.AddDays(20).Day == calendar.Date.Day) {
                Button21EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(21).Month == calendar.Date.Month && calendarStartingDate.AddDays(21).Day == calendar.Date.Day) {
                Button22EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(22).Month == calendar.Date.Month && calendarStartingDate.AddDays(22).Day == calendar.Date.Day) {
                Button23EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(23).Month == calendar.Date.Month && calendarStartingDate.AddDays(23).Day == calendar.Date.Day) {
                Button24EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(24).Month == calendar.Date.Month && calendarStartingDate.AddDays(24).Day == calendar.Date.Day) {
                Button25EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(25).Month == calendar.Date.Month && calendarStartingDate.AddDays(25).Day == calendar.Date.Day) {
                Button26EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(26).Month == calendar.Date.Month && calendarStartingDate.AddDays(26).Day == calendar.Date.Day) {
                Button27EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(27).Month == calendar.Date.Month && calendarStartingDate.AddDays(27).Day == calendar.Date.Day) {
                Button28EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(28).Month == calendar.Date.Month && calendarStartingDate.AddDays(28).Day == calendar.Date.Day) {
                Button29EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(29).Month == calendar.Date.Month && calendarStartingDate.AddDays(29).Day == calendar.Date.Day) {
                Button30EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(30).Month == calendar.Date.Month && calendarStartingDate.AddDays(30).Day == calendar.Date.Day) {
                Button31EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(31).Month == calendar.Date.Month && calendarStartingDate.AddDays(31).Day == calendar.Date.Day) {
                Button32EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(32).Month == calendar.Date.Month && calendarStartingDate.AddDays(32).Day == calendar.Date.Day) {
                Button33EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(33).Month == calendar.Date.Month && calendarStartingDate.AddDays(33).Day == calendar.Date.Day) {
                Button34EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(34).Month == calendar.Date.Month && calendarStartingDate.AddDays(34).Day == calendar.Date.Day) {
                Button35EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(35).Month == calendar.Date.Month && calendarStartingDate.AddDays(35).Day == calendar.Date.Day) {
                Button36EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(36).Month == calendar.Date.Month && calendarStartingDate.AddDays(36).Day == calendar.Date.Day) {
                Button37EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(37).Month == calendar.Date.Month && calendarStartingDate.AddDays(37).Day == calendar.Date.Day) {
                Button38EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(38).Month == calendar.Date.Month && calendarStartingDate.AddDays(38).Day == calendar.Date.Day) {
                Button39EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(39).Month == calendar.Date.Month && calendarStartingDate.AddDays(39).Day == calendar.Date.Day) {
                Button40EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(40).Month == calendar.Date.Month && calendarStartingDate.AddDays(40).Day == calendar.Date.Day) {
                Button41EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }

            if (calendarStartingDate.AddDays(41).Month == calendar.Date.Month && calendarStartingDate.AddDays(41).Day == calendar.Date.Day) {
                Button42EventList.Add(new CalendarEventsCustom {
                    Description = calendar.EventText,
                    Image = calendar.Image
                });
            }
        }

        /* Get Calendar Events */
        foreach (CalendarDates dates in ReferenceValues.JsonCalendarMaster.DatesList) {
            if (calendarStartingDate.ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button1EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(1).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button2EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(2).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button3EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(3).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button4EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(4).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button5EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(5).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button6EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(6).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button7EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(7).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button8EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(8).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button9EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(9).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button10EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(10).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button11EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(11).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button12EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(12).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button13EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(13).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button14EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(14).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button15EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(15).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button16EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(16).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button17EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(17).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button18EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(18).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button19EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(19).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button20EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(20).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button21EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(21).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button22EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(22).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button23EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(23).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button24EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(24).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button25EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(25).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button26EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(26).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button27EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(27).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button28EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(28).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button29EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(29).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button30EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(30).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button31EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(31).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button32EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(32).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button33EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(33).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button34EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(34).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button35EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(35).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button36EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(36).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button37EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(37).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button38EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(38).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button39EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(39).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button40EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(40).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button41EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }

            if (calendarStartingDate.AddDays(41).ToString("yyyy-MM-dd") == dates.Date) {
                foreach (CalendarEvents events in dates.EventsList) {
                    Button42EventList.Add(new CalendarEventsCustom {
                        Image = ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user" + events.UserId + ".png",
                        Description = events.StartTime + " - " + events.EndTime + "  " + events.EventName,
                        Priority = events.Priority
                    });
                }
            }
        }
    }

    #region Fields

    public string CurrentMonthAndYear {
        get => _currentMonthAndYear;
        set {
            _currentMonthAndYear = value;
            RaisePropertyChangedEvent("CurrentMonthAndYear");
        }
    }

    public string Button1Date {
        get => _button1Date;
        set {
            _button1Date = value;
            RaisePropertyChangedEvent("Button1Date");
        }
    }

    public string Button1HolidayText {
        get => _button1HolidayText;
        set {
            _button1HolidayText = value;
            RaisePropertyChangedEvent("Button1HolidayText");
        }
    }

    public string Button2Date {
        get => _button2Date;
        set {
            _button2Date = value;
            RaisePropertyChangedEvent("Button2Date");
        }
    }

    public string Button2HolidayText {
        get => _button2HolidayText;
        set {
            _button2HolidayText = value;
            RaisePropertyChangedEvent("Button2HolidayText");
        }
    }

    public string Button3Date {
        get => _button3Date;
        set {
            _button3Date = value;
            RaisePropertyChangedEvent("Button3Date");
        }
    }

    public string Button3HolidayText {
        get => _button3HolidayText;
        set {
            _button3HolidayText = value;
            RaisePropertyChangedEvent("Button3HolidayText");
        }
    }

    public string Button4Date {
        get => _button4Date;
        set {
            _button4Date = value;
            RaisePropertyChangedEvent("Button4Date");
        }
    }

    public string Button4HolidayText {
        get => _button4HolidayText;
        set {
            _button4HolidayText = value;
            RaisePropertyChangedEvent("Button4HolidayText");
        }
    }

    public string Button5Date {
        get => _button5Date;
        set {
            _button5Date = value;
            RaisePropertyChangedEvent("Button5Date");
        }
    }

    public string Button5HolidayText {
        get => _button5HolidayText;
        set {
            _button5HolidayText = value;
            RaisePropertyChangedEvent("Button5HolidayText");
        }
    }

    public string Button6Date {
        get => _button6Date;
        set {
            _button6Date = value;
            RaisePropertyChangedEvent("Button6Date");
        }
    }

    public string Button6HolidayText {
        get => _button6HolidayText;
        set {
            _button6HolidayText = value;
            RaisePropertyChangedEvent("Button6HolidayText");
        }
    }

    public string Button7Date {
        get => _button7Date;
        set {
            _button7Date = value;
            RaisePropertyChangedEvent("Button7Date");
        }
    }

    public string Button7HolidayText {
        get => _button7HolidayText;
        set {
            _button7HolidayText = value;
            RaisePropertyChangedEvent("Button7HolidayText");
        }
    }

    public string Button8Date {
        get => _button8Date;
        set {
            _button8Date = value;
            RaisePropertyChangedEvent("Button8Date");
        }
    }

    public string Button8HolidayText {
        get => _button8HolidayText;
        set {
            _button8HolidayText = value;
            RaisePropertyChangedEvent("Button8HolidayText");
        }
    }

    public string Button9Date {
        get => _button9Date;
        set {
            _button9Date = value;
            RaisePropertyChangedEvent("Button9Date");
        }
    }

    public string Button9HolidayText {
        get => _button9HolidayText;
        set {
            _button9HolidayText = value;
            RaisePropertyChangedEvent("Button9HolidayText");
        }
    }

    public string Button10Date {
        get => _button10Date;
        set {
            _button10Date = value;
            RaisePropertyChangedEvent("Button10Date");
        }
    }

    public string Button10HolidayText {
        get => _button10HolidayText;
        set {
            _button10HolidayText = value;
            RaisePropertyChangedEvent("Button10HolidayText");
        }
    }

    public string Button11Date {
        get => _button11Date;
        set {
            _button11Date = value;
            RaisePropertyChangedEvent("Button11Date");
        }
    }

    public string Button11HolidayText {
        get => _button11HolidayText;
        set {
            _button11HolidayText = value;
            RaisePropertyChangedEvent("Button11HolidayText");
        }
    }

    public string Button12Date {
        get => _button12Date;
        set {
            _button12Date = value;
            RaisePropertyChangedEvent("Button12Date");
        }
    }

    public string Button12HolidayText {
        get => _button12HolidayText;
        set {
            _button12HolidayText = value;
            RaisePropertyChangedEvent("Button12HolidayText");
        }
    }

    public string Button13Date {
        get => _button13Date;
        set {
            _button13Date = value;
            RaisePropertyChangedEvent("Button13Date");
        }
    }

    public string Button13HolidayText {
        get => _button13HolidayText;
        set {
            _button13HolidayText = value;
            RaisePropertyChangedEvent("Button13HolidayText");
        }
    }

    public string Button14Date {
        get => _button14Date;
        set {
            _button14Date = value;
            RaisePropertyChangedEvent("Button14Date");
        }
    }

    public string Button14HolidayText {
        get => _button14HolidayText;
        set {
            _button14HolidayText = value;
            RaisePropertyChangedEvent("Button14HolidayText");
        }
    }

    public string Button15Date {
        get => _button15Date;
        set {
            _button15Date = value;
            RaisePropertyChangedEvent("Button15Date");
        }
    }

    public string Button15HolidayText {
        get => _button15HolidayText;
        set {
            _button15HolidayText = value;
            RaisePropertyChangedEvent("Button15HolidayText");
        }
    }

    public string Button16Date {
        get => _button16Date;
        set {
            _button16Date = value;
            RaisePropertyChangedEvent("Button16Date");
        }
    }

    public string Button16HolidayText {
        get => _button16HolidayText;
        set {
            _button16HolidayText = value;
            RaisePropertyChangedEvent("Button16HolidayText");
        }
    }


    public string Button17Date {
        get => _button17Date;
        set {
            _button17Date = value;
            RaisePropertyChangedEvent("Button17Date");
        }
    }

    public string Button17HolidayText {
        get => _button17HolidayText;
        set {
            _button17HolidayText = value;
            RaisePropertyChangedEvent("Button17HolidayText");
        }
    }

    public string Button18Date {
        get => _button18Date;
        set {
            _button18Date = value;
            RaisePropertyChangedEvent("Button18Date");
        }
    }

    public string Button18HolidayText {
        get => _button18HolidayText;
        set {
            _button18HolidayText = value;
            RaisePropertyChangedEvent("Button18HolidayText");
        }
    }

    public string Button19Date {
        get => _button19Date;
        set {
            _button19Date = value;
            RaisePropertyChangedEvent("Button19Date");
        }
    }

    public string Button19HolidayText {
        get => _button19HolidayText;
        set {
            _button19HolidayText = value;
            RaisePropertyChangedEvent("Button19HolidayText");
        }
    }

    public string Button20Date {
        get => _button20Date;
        set {
            _button20Date = value;
            RaisePropertyChangedEvent("Button20Date");
        }
    }

    public string Button20HolidayText {
        get => _button20HolidayText;
        set {
            _button20HolidayText = value;
            RaisePropertyChangedEvent("Button20HolidayText");
        }
    }

    public string Button21Date {
        get => _button21Date;
        set {
            _button21Date = value;
            RaisePropertyChangedEvent("Button21Date");
        }
    }

    public string Button21HolidayText {
        get => _button21HolidayText;
        set {
            _button21HolidayText = value;
            RaisePropertyChangedEvent("Button21HolidayText");
        }
    }

    public string Button22Date {
        get => _button22Date;
        set {
            _button22Date = value;
            RaisePropertyChangedEvent("Button22Date");
        }
    }

    public string Button22HolidayText {
        get => _button22HolidayText;
        set {
            _button22HolidayText = value;
            RaisePropertyChangedEvent("Button22HolidayText");
        }
    }

    public string Button23Date {
        get => _button23Date;
        set {
            _button23Date = value;
            RaisePropertyChangedEvent("Button23Date");
        }
    }

    public string Button23HolidayText {
        get => _button23HolidayText;
        set {
            _button23HolidayText = value;
            RaisePropertyChangedEvent("Button23HolidayText");
        }
    }

    public string Button24Date {
        get => _button24Date;
        set {
            _button24Date = value;
            RaisePropertyChangedEvent("Button24Date");
        }
    }

    public string Button24HolidayText {
        get => _button24HolidayText;
        set {
            _button24HolidayText = value;
            RaisePropertyChangedEvent("Button24HolidayText");
        }
    }

    public string Button25Date {
        get => _button25Date;
        set {
            _button25Date = value;
            RaisePropertyChangedEvent("Button25Date");
        }
    }

    public string Button25HolidayText {
        get => _button25HolidayText;
        set {
            _button25HolidayText = value;
            RaisePropertyChangedEvent("Button25HolidayText");
        }
    }

    public string Button26Date {
        get => _button26Date;
        set {
            _button26Date = value;
            RaisePropertyChangedEvent("Button26Date");
        }
    }

    public string Button26HolidayText {
        get => _button26HolidayText;
        set {
            _button26HolidayText = value;
            RaisePropertyChangedEvent("Button26HolidayText");
        }
    }

    public string Button27Date {
        get => _button27Date;
        set {
            _button27Date = value;
            RaisePropertyChangedEvent("Button27Date");
        }
    }

    public string Button27HolidayText {
        get => _button27HolidayText;
        set {
            _button27HolidayText = value;
            RaisePropertyChangedEvent("Button27HolidayText");
        }
    }

    public string Button28Date {
        get => _button28Date;
        set {
            _button28Date = value;
            RaisePropertyChangedEvent("Button28Date");
        }
    }

    public string Button28HolidayText {
        get => _button28HolidayText;
        set {
            _button28HolidayText = value;
            RaisePropertyChangedEvent("Button28HolidayText");
        }
    }

    public string Button29Date {
        get => _button29Date;
        set {
            _button29Date = value;
            RaisePropertyChangedEvent("Button29Date");
        }
    }

    public string Button29HolidayText {
        get => _button29HolidayText;
        set {
            _button29HolidayText = value;
            RaisePropertyChangedEvent("Button29HolidayText");
        }
    }

    public string Button30Date {
        get => _button30Date;
        set {
            _button30Date = value;
            RaisePropertyChangedEvent("Button30Date");
        }
    }

    public string Button30HolidayText {
        get => _button30HolidayText;
        set {
            _button30HolidayText = value;
            RaisePropertyChangedEvent("Button30HolidayText");
        }
    }

    public string Button31Date {
        get => _button31Date;
        set {
            _button31Date = value;
            RaisePropertyChangedEvent("Button31Date");
        }
    }

    public string Button31HolidayText {
        get => _button31HolidayText;
        set {
            _button31HolidayText = value;
            RaisePropertyChangedEvent("Button31HolidayText");
        }
    }

    public string Button32Date {
        get => _button32Date;
        set {
            _button32Date = value;
            RaisePropertyChangedEvent("Button32Date");
        }
    }

    public string Button32HolidayText {
        get => _button32HolidayText;
        set {
            _button32HolidayText = value;
            RaisePropertyChangedEvent("Button32HolidayText");
        }
    }

    public string Button33Date {
        get => _button33Date;
        set {
            _button33Date = value;
            RaisePropertyChangedEvent("Button33Date");
        }
    }

    public string Button33HolidayText {
        get => _button33HolidayText;
        set {
            _button33HolidayText = value;
            RaisePropertyChangedEvent("Button33HolidayText");
        }
    }

    public string Button34Date {
        get => _button34Date;
        set {
            _button34Date = value;
            RaisePropertyChangedEvent("Button34Date");
        }
    }

    public string Button34HolidayText {
        get => _button34HolidayText;
        set {
            _button34HolidayText = value;
            RaisePropertyChangedEvent("Button34HolidayText");
        }
    }

    public string Button35Date {
        get => _button35Date;
        set {
            _button35Date = value;
            RaisePropertyChangedEvent("Button35Date");
        }
    }

    public string Button35HolidayText {
        get => _button35HolidayText;
        set {
            _button35HolidayText = value;
            RaisePropertyChangedEvent("Button35HolidayText");
        }
    }

    public string Button36Date {
        get => _button36Date;
        set {
            _button36Date = value;
            RaisePropertyChangedEvent("Button36Date");
        }
    }

    public string Button36HolidayText {
        get => _button36HolidayText;
        set {
            _button36HolidayText = value;
            RaisePropertyChangedEvent("Button36HolidayText");
        }
    }

    public string Button37Date {
        get => _button37Date;
        set {
            _button37Date = value;
            RaisePropertyChangedEvent("Button37Date");
        }
    }

    public string Button37HolidayText {
        get => _button37HolidayText;
        set {
            _button37HolidayText = value;
            RaisePropertyChangedEvent("Button37HolidayText");
        }
    }

    public string Button38Date {
        get => _button38Date;
        set {
            _button38Date = value;
            RaisePropertyChangedEvent("Button38Date");
        }
    }

    public string Button38HolidayText {
        get => _button38HolidayText;
        set {
            _button38HolidayText = value;
            RaisePropertyChangedEvent("Button38HolidayText");
        }
    }

    public string Button39Date {
        get => _button39Date;
        set {
            _button39Date = value;
            RaisePropertyChangedEvent("Button39Date");
        }
    }

    public string Button39HolidayText {
        get => _button39HolidayText;
        set {
            _button39HolidayText = value;
            RaisePropertyChangedEvent("Button39HolidayText");
        }
    }

    public string Button40Date {
        get => _button40Date;
        set {
            _button40Date = value;
            RaisePropertyChangedEvent("Button40Date");
        }
    }

    public string Button40HolidayText {
        get => _button40HolidayText;
        set {
            _button40HolidayText = value;
            RaisePropertyChangedEvent("Button40HolidayText");
        }
    }

    public string Button41Date {
        get => _button41Date;
        set {
            _button41Date = value;
            RaisePropertyChangedEvent("Button41Date");
        }
    }

    public string Button41HolidayText {
        get => _button41HolidayText;
        set {
            _button41HolidayText = value;
            RaisePropertyChangedEvent("Button41HolidayText");
        }
    }

    public string Button42Date {
        get => _button42Date;
        set {
            _button42Date = value;
            RaisePropertyChangedEvent("Button42Date");
        }
    }

    public string Button42HolidayText {
        get => _button42HolidayText;
        set {
            _button42HolidayText = value;
            RaisePropertyChangedEvent("Button42HolidayText");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button1EventList {
        get => _button1EventList;
        set {
            _button1EventList = value;
            RaisePropertyChangedEvent("Button1EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button2EventList {
        get => _button2EventList;
        set {
            _button2EventList = value;
            RaisePropertyChangedEvent("Button2EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button3EventList {
        get => _button3EventList;
        set {
            _button3EventList = value;
            RaisePropertyChangedEvent("Button3EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button4EventList {
        get => _button4EventList;
        set {
            _button4EventList = value;
            RaisePropertyChangedEvent("Button4EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button5EventList {
        get => _button5EventList;
        set {
            _button5EventList = value;
            RaisePropertyChangedEvent("Button5EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button6EventList {
        get => _button6EventList;
        set {
            _button6EventList = value;
            RaisePropertyChangedEvent("Button6EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button7EventList {
        get => _button7EventList;
        set {
            _button7EventList = value;
            RaisePropertyChangedEvent("Button7EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button8EventList {
        get => _button8EventList;
        set {
            _button8EventList = value;
            RaisePropertyChangedEvent("Button8EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button9EventList {
        get => _button9EventList;
        set {
            _button9EventList = value;
            RaisePropertyChangedEvent("Button9EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button10EventList {
        get => _button10EventList;
        set {
            _button10EventList = value;
            RaisePropertyChangedEvent("Button10EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button11EventList {
        get => _button11EventList;
        set {
            _button11EventList = value;
            RaisePropertyChangedEvent("Button11EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button12EventList {
        get => _button12EventList;
        set {
            _button12EventList = value;
            RaisePropertyChangedEvent("Button12EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button13EventList {
        get => _button13EventList;
        set {
            _button13EventList = value;
            RaisePropertyChangedEvent("Button13EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button14EventList {
        get => _button14EventList;
        set {
            _button14EventList = value;
            RaisePropertyChangedEvent("Button14EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button15EventList {
        get => _button15EventList;
        set {
            _button15EventList = value;
            RaisePropertyChangedEvent("Button15EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button16EventList {
        get => _button16EventList;
        set {
            _button16EventList = value;
            RaisePropertyChangedEvent("Button16EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button17EventList {
        get => _button17EventList;
        set {
            _button17EventList = value;
            RaisePropertyChangedEvent("Button17EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button18EventList {
        get => _button18EventList;
        set {
            _button18EventList = value;
            RaisePropertyChangedEvent("Button18EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button19EventList {
        get => _button19EventList;
        set {
            _button19EventList = value;
            RaisePropertyChangedEvent("Button19EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button20EventList {
        get => _button20EventList;
        set {
            _button20EventList = value;
            RaisePropertyChangedEvent("Button20EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button21EventList {
        get => _button21EventList;
        set {
            _button21EventList = value;
            RaisePropertyChangedEvent("Button21EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button22EventList {
        get => _button22EventList;
        set {
            _button22EventList = value;
            RaisePropertyChangedEvent("Button22EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button23EventList {
        get => _button23EventList;
        set {
            _button23EventList = value;
            RaisePropertyChangedEvent("Button23EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button24EventList {
        get => _button24EventList;
        set {
            _button24EventList = value;
            RaisePropertyChangedEvent("Button24EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button25EventList {
        get => _button25EventList;
        set {
            _button25EventList = value;
            RaisePropertyChangedEvent("Button25EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button26EventList {
        get => _button26EventList;
        set {
            _button26EventList = value;
            RaisePropertyChangedEvent("Button26EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button27EventList {
        get => _button27EventList;
        set {
            _button27EventList = value;
            RaisePropertyChangedEvent("Button27EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button28EventList {
        get => _button28EventList;
        set {
            _button28EventList = value;
            RaisePropertyChangedEvent("Button28EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button29EventList {
        get => _button29EventList;
        set {
            _button29EventList = value;
            RaisePropertyChangedEvent("Button29EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button30EventList {
        get => _button30EventList;
        set {
            _button30EventList = value;
            RaisePropertyChangedEvent("Button30EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button31EventList {
        get => _button31EventList;
        set {
            _button31EventList = value;
            RaisePropertyChangedEvent("Button31EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button32EventList {
        get => _button32EventList;
        set {
            _button32EventList = value;
            RaisePropertyChangedEvent("Button32EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button33EventList {
        get => _button33EventList;
        set {
            _button33EventList = value;
            RaisePropertyChangedEvent("Button33EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button34EventList {
        get => _button34EventList;
        set {
            _button34EventList = value;
            RaisePropertyChangedEvent("Button34EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button35EventList {
        get => _button35EventList;
        set {
            _button35EventList = value;
            RaisePropertyChangedEvent("Button35EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button36EventList {
        get => _button36EventList;
        set {
            _button36EventList = value;
            RaisePropertyChangedEvent("Button36EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button37EventList {
        get => _button37EventList;
        set {
            _button37EventList = value;
            RaisePropertyChangedEvent("Button37EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button38EventList {
        get => _button38EventList;
        set {
            _button38EventList = value;
            RaisePropertyChangedEvent("Button38EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button39EventList {
        get => _button39EventList;
        set {
            _button39EventList = value;
            RaisePropertyChangedEvent("Button39EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button40EventList {
        get => _button40EventList;
        set {
            _button40EventList = value;
            RaisePropertyChangedEvent("Button40EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button41EventList {
        get => _button41EventList;
        set {
            _button41EventList = value;
            RaisePropertyChangedEvent("Button41EventList");
        }
    }

    public ObservableCollection<CalendarEventsCustom> Button42EventList {
        get => _button42EventList;
        set {
            _button42EventList = value;
            RaisePropertyChangedEvent("Button42EventList");
        }
    }

    public string Button1BorderColor {
        get => _button1BorderColor;
        set {
            _button1BorderColor = value;
            RaisePropertyChangedEvent("Button1BorderColor");
        }
    }

    public string Button2BorderColor {
        get => _button2BorderColor;
        set {
            _button2BorderColor = value;
            RaisePropertyChangedEvent("Button2BorderColor");
        }
    }

    public string Button3BorderColor {
        get => _button3BorderColor;
        set {
            _button3BorderColor = value;
            RaisePropertyChangedEvent("Button3BorderColor");
        }
    }

    public string Button4BorderColor {
        get => _button4BorderColor;
        set {
            _button4BorderColor = value;
            RaisePropertyChangedEvent("Button4BorderColor");
        }
    }

    public string Button5BorderColor {
        get => _button5BorderColor;
        set {
            _button5BorderColor = value;
            RaisePropertyChangedEvent("Button5BorderColor");
        }
    }

    public string Button6BorderColor {
        get => _button6BorderColor;
        set {
            _button6BorderColor = value;
            RaisePropertyChangedEvent("Button6BorderColor");
        }
    }

    public string Button7BorderColor {
        get => _button7BorderColor;
        set {
            _button7BorderColor = value;
            RaisePropertyChangedEvent("Button7BorderColor");
        }
    }

    public string Button8BorderColor {
        get => _button8BorderColor;
        set {
            _button8BorderColor = value;
            RaisePropertyChangedEvent("Button8BorderColor");
        }
    }

    public string Button9BorderColor {
        get => _button9BorderColor;
        set {
            _button9BorderColor = value;
            RaisePropertyChangedEvent("Button9BorderColor");
        }
    }

    public string Button10BorderColor {
        get => _button10BorderColor;
        set {
            _button10BorderColor = value;
            RaisePropertyChangedEvent("Button10BorderColor");
        }
    }

    public string Button11BorderColor {
        get => _button11BorderColor;
        set {
            _button11BorderColor = value;
            RaisePropertyChangedEvent("Button11BorderColor");
        }
    }

    public string Button12BorderColor {
        get => _button12BorderColor;
        set {
            _button12BorderColor = value;
            RaisePropertyChangedEvent("Button12BorderColor");
        }
    }

    public string Button13BorderColor {
        get => _button13BorderColor;
        set {
            _button13BorderColor = value;
            RaisePropertyChangedEvent("Button13BorderColor");
        }
    }

    public string Button14BorderColor {
        get => _button14BorderColor;
        set {
            _button14BorderColor = value;
            RaisePropertyChangedEvent("Button14BorderColor");
        }
    }

    public string Button15BorderColor {
        get => _button15BorderColor;
        set {
            _button15BorderColor = value;
            RaisePropertyChangedEvent("Button15BorderColor");
        }
    }

    public string Button16BorderColor {
        get => _button16BorderColor;
        set {
            _button16BorderColor = value;
            RaisePropertyChangedEvent("Button16BorderColor");
        }
    }

    public string Button17BorderColor {
        get => _button17BorderColor;
        set {
            _button17BorderColor = value;
            RaisePropertyChangedEvent("Button17BorderColor");
        }
    }

    public string Button18BorderColor {
        get => _button18BorderColor;
        set {
            _button18BorderColor = value;
            RaisePropertyChangedEvent("Button18BorderColor");
        }
    }

    public string Button19BorderColor {
        get => _button19BorderColor;
        set {
            _button19BorderColor = value;
            RaisePropertyChangedEvent("Button19BorderColor");
        }
    }

    public string Button20BorderColor {
        get => _button20BorderColor;
        set {
            _button20BorderColor = value;
            RaisePropertyChangedEvent("Button20BorderColor");
        }
    }

    public string Button21BorderColor {
        get => _button21BorderColor;
        set {
            _button21BorderColor = value;
            RaisePropertyChangedEvent("Button21BorderColor");
        }
    }

    public string Button22BorderColor {
        get => _button22BorderColor;
        set {
            _button22BorderColor = value;
            RaisePropertyChangedEvent("Button22BorderColor");
        }
    }

    public string Button23BorderColor {
        get => _button23BorderColor;
        set {
            _button23BorderColor = value;
            RaisePropertyChangedEvent("Button23BorderColor");
        }
    }

    public string Button24BorderColor {
        get => _button24BorderColor;
        set {
            _button24BorderColor = value;
            RaisePropertyChangedEvent("Button24BorderColor");
        }
    }

    public string Button25BorderColor {
        get => _button25BorderColor;
        set {
            _button25BorderColor = value;
            RaisePropertyChangedEvent("Button25BorderColor");
        }
    }

    public string Button26BorderColor {
        get => _button26BorderColor;
        set {
            _button26BorderColor = value;
            RaisePropertyChangedEvent("Button26BorderColor");
        }
    }

    public string Button27BorderColor {
        get => _button27BorderColor;
        set {
            _button27BorderColor = value;
            RaisePropertyChangedEvent("Button27BorderColor");
        }
    }

    public string Button28BorderColor {
        get => _button28BorderColor;
        set {
            _button28BorderColor = value;
            RaisePropertyChangedEvent("Button28BorderColor");
        }
    }

    public string Button29BorderColor {
        get => _button29BorderColor;
        set {
            _button29BorderColor = value;
            RaisePropertyChangedEvent("Button29BorderColor");
        }
    }

    public string Button30BorderColor {
        get => _button30BorderColor;
        set {
            _button30BorderColor = value;
            RaisePropertyChangedEvent("Button30BorderColor");
        }
    }

    public string Button31BorderColor {
        get => _button31BorderColor;
        set {
            _button31BorderColor = value;
            RaisePropertyChangedEvent("Button31BorderColor");
        }
    }

    public string Button32BorderColor {
        get => _button32BorderColor;
        set {
            _button32BorderColor = value;
            RaisePropertyChangedEvent("Button32BorderColor");
        }
    }

    public string Button33BorderColor {
        get => _button33BorderColor;
        set {
            _button33BorderColor = value;
            RaisePropertyChangedEvent("Button33BorderColor");
        }
    }

    public string Button34BorderColor {
        get => _button34BorderColor;
        set {
            _button34BorderColor = value;
            RaisePropertyChangedEvent("Button34BorderColor");
        }
    }

    public string Button35BorderColor {
        get => _button35BorderColor;
        set {
            _button35BorderColor = value;
            RaisePropertyChangedEvent("Button35BorderColor");
        }
    }

    public string Button36BorderColor {
        get => _button36BorderColor;
        set {
            _button36BorderColor = value;
            RaisePropertyChangedEvent("Button36BorderColor");
        }
    }

    public string Button37BorderColor {
        get => _button37BorderColor;
        set {
            _button37BorderColor = value;
            RaisePropertyChangedEvent("Button37BorderColor");
        }
    }

    public string Button38BorderColor {
        get => _button38BorderColor;
        set {
            _button38BorderColor = value;
            RaisePropertyChangedEvent("Button38BorderColor");
        }
    }

    public string Button39BorderColor {
        get => _button39BorderColor;
        set {
            _button39BorderColor = value;
            RaisePropertyChangedEvent("Button39BorderColor");
        }
    }

    public string Button40BorderColor {
        get => _button40BorderColor;
        set {
            _button40BorderColor = value;
            RaisePropertyChangedEvent("Button40BorderColor");
        }
    }

    public string Button41BorderColor {
        get => _button41BorderColor;
        set {
            _button41BorderColor = value;
            RaisePropertyChangedEvent("Button41BorderColor");
        }
    }

    public string Button42BorderColor {
        get => _button42BorderColor;
        set {
            _button42BorderColor = value;
            RaisePropertyChangedEvent("Button42BorderColor");
        }
    }

    public int Button2BorderThickness {
        get => _button2BorderThickness;
        set {
            _button2BorderThickness = value;
            RaisePropertyChangedEvent("Button2BorderThickness");
        }
    }

    public int Button1BorderThickness {
        get => _button1BorderThickness;
        set {
            _button1BorderThickness = value;
            RaisePropertyChangedEvent("Button1BorderThickness");
        }
    }

    public int Button3BorderThickness {
        get => _button3BorderThickness;
        set {
            _button3BorderThickness = value;
            RaisePropertyChangedEvent("Button3BorderThickness");
        }
    }

    public int Button4BorderThickness {
        get => _button4BorderThickness;
        set {
            _button4BorderThickness = value;
            RaisePropertyChangedEvent("Button4BorderThickness");
        }
    }

    public int Button5BorderThickness {
        get => _button5BorderThickness;
        set {
            _button5BorderThickness = value;
            RaisePropertyChangedEvent("Button5BorderThickness");
        }
    }

    public int Button6BorderThickness {
        get => _button6BorderThickness;
        set {
            _button6BorderThickness = value;
            RaisePropertyChangedEvent("Button6BorderThickness");
        }
    }

    public int Button7BorderThickness {
        get => _button7BorderThickness;
        set {
            _button7BorderThickness = value;
            RaisePropertyChangedEvent("Button7BorderThickness");
        }
    }

    public int Button8BorderThickness {
        get => _button8BorderThickness;
        set {
            _button8BorderThickness = value;
            RaisePropertyChangedEvent("Button8BorderThickness");
        }
    }

    public int Button9BorderThickness {
        get => _button9BorderThickness;
        set {
            _button9BorderThickness = value;
            RaisePropertyChangedEvent("Button9BorderThickness");
        }
    }

    public int Button10BorderThickness {
        get => _button10BorderThickness;
        set {
            _button10BorderThickness = value;
            RaisePropertyChangedEvent("Button10BorderThickness");
        }
    }

    public int Button11BorderThickness {
        get => _button11BorderThickness;
        set {
            _button11BorderThickness = value;
            RaisePropertyChangedEvent("Button11BorderThickness");
        }
    }

    public int Button12BorderThickness {
        get => _button12BorderThickness;
        set {
            _button12BorderThickness = value;
            RaisePropertyChangedEvent("Button12BorderThickness");
        }
    }

    public int Button13BorderThickness {
        get => _button13BorderThickness;
        set {
            _button13BorderThickness = value;
            RaisePropertyChangedEvent("Button13BorderThickness");
        }
    }

    public int Button14BorderThickness {
        get => _button14BorderThickness;
        set {
            _button14BorderThickness = value;
            RaisePropertyChangedEvent("Button14BorderThickness");
        }
    }

    public int Button15BorderThickness {
        get => _button15BorderThickness;
        set {
            _button15BorderThickness = value;
            RaisePropertyChangedEvent("Button15BorderThickness");
        }
    }

    public int Button16BorderThickness {
        get => _button16BorderThickness;
        set {
            _button16BorderThickness = value;
            RaisePropertyChangedEvent("Button16BorderThickness");
        }
    }

    public int Button17BorderThickness {
        get => _button17BorderThickness;
        set {
            _button17BorderThickness = value;
            RaisePropertyChangedEvent("Button17BorderThickness");
        }
    }

    public int Button18BorderThickness {
        get => _button18BorderThickness;
        set {
            _button18BorderThickness = value;
            RaisePropertyChangedEvent("Button18BorderThickness");
        }
    }

    public int Button19BorderThickness {
        get => _button19BorderThickness;
        set {
            _button19BorderThickness = value;
            RaisePropertyChangedEvent("Button19BorderThickness");
        }
    }

    public int Button20BorderThickness {
        get => _button20BorderThickness;
        set {
            _button20BorderThickness = value;
            RaisePropertyChangedEvent("Button20BorderThickness");
        }
    }

    public int Button21BorderThickness {
        get => _button21BorderThickness;
        set {
            _button21BorderThickness = value;
            RaisePropertyChangedEvent("Button21BorderThickness");
        }
    }

    public int Button22BorderThickness {
        get => _button22BorderThickness;
        set {
            _button22BorderThickness = value;
            RaisePropertyChangedEvent("Button22BorderThickness");
        }
    }

    public int Button23BorderThickness {
        get => _button23BorderThickness;
        set {
            _button23BorderThickness = value;
            RaisePropertyChangedEvent("Button23BorderThickness");
        }
    }

    public int Button24BorderThickness {
        get => _button24BorderThickness;
        set {
            _button24BorderThickness = value;
            RaisePropertyChangedEvent("Button24BorderThickness");
        }
    }

    public int Button25BorderThickness {
        get => _button25BorderThickness;
        set {
            _button25BorderThickness = value;
            RaisePropertyChangedEvent("Button25BorderThickness");
        }
    }

    public int Button26BorderThickness {
        get => _button26BorderThickness;
        set {
            _button26BorderThickness = value;
            RaisePropertyChangedEvent("Button26BorderThickness");
        }
    }

    public int Button27BorderThickness {
        get => _button27BorderThickness;
        set {
            _button27BorderThickness = value;
            RaisePropertyChangedEvent("Button27BorderThickness");
        }
    }

    public int Button28BorderThickness {
        get => _button28BorderThickness;
        set {
            _button28BorderThickness = value;
            RaisePropertyChangedEvent("Button28BorderThickness");
        }
    }

    public int Button29BorderThickness {
        get => _button29BorderThickness;
        set {
            _button29BorderThickness = value;
            RaisePropertyChangedEvent("Button29BorderThickness");
        }
    }

    public int Button30BorderThickness {
        get => _button30BorderThickness;
        set {
            _button30BorderThickness = value;
            RaisePropertyChangedEvent("Button30BorderThickness");
        }
    }

    public int Button31BorderThickness {
        get => _button31BorderThickness;
        set {
            _button31BorderThickness = value;
            RaisePropertyChangedEvent("Button31BorderThickness");
        }
    }

    public int Button32BorderThickness {
        get => _button32BorderThickness;
        set {
            _button32BorderThickness = value;
            RaisePropertyChangedEvent("Button32BorderThickness");
        }
    }

    public int Button33BorderThickness {
        get => _button33BorderThickness;
        set {
            _button33BorderThickness = value;
            RaisePropertyChangedEvent("Button33BorderThickness");
        }
    }

    public int Button34BorderThickness {
        get => _button34BorderThickness;
        set {
            _button34BorderThickness = value;
            RaisePropertyChangedEvent("Button34BorderThickness");
        }
    }

    public int Button35BorderThickness {
        get => _button35BorderThickness;
        set {
            _button35BorderThickness = value;
            RaisePropertyChangedEvent("Button35BorderThickness");
        }
    }

    public int Button36BorderThickness {
        get => _button36BorderThickness;
        set {
            _button36BorderThickness = value;
            RaisePropertyChangedEvent("Button36BorderThickness");
        }
    }

    public int Button37BorderThickness {
        get => _button37BorderThickness;
        set {
            _button37BorderThickness = value;
            RaisePropertyChangedEvent("Button37BorderThickness");
        }
    }

    public int Button38BorderThickness {
        get => _button38BorderThickness;
        set {
            _button38BorderThickness = value;
            RaisePropertyChangedEvent("Button38BorderThickness");
        }
    }

    public int Button39BorderThickness {
        get => _button39BorderThickness;
        set {
            _button39BorderThickness = value;
            RaisePropertyChangedEvent("Button39BorderThickness");
        }
    }

    public int Button40BorderThickness {
        get => _button40BorderThickness;
        set {
            _button40BorderThickness = value;
            RaisePropertyChangedEvent("Button40BorderThickness");
        }
    }

    public int Button41BorderThickness {
        get => _button41BorderThickness;
        set {
            _button41BorderThickness = value;
            RaisePropertyChangedEvent("Button41BorderThickness");
        }
    }

    public int Button42BorderThickness {
        get => _button42BorderThickness;
        set {
            _button42BorderThickness = value;
            RaisePropertyChangedEvent("Button42BorderThickness");
        }
    }

    #endregion
}