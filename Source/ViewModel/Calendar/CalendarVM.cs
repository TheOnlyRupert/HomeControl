using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Calendar;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class CalendarVM : BaseViewModel {
    private string _button1Date, _button1HolidayText, _button1EventText2, _button1EventText3, _button2Date, _button2HolidayText, _button2EventText1,
        _button2EventText2, _button2EventText3, _button3Date, _button3HolidayText, _button3EventText1, _button3EventText2, _button3EventText3, _button4Date,
        _button4HolidayText, _button4EventText1, _button4EventText2, _button4EventText3, _button5Date, _button5HolidayText, _button5EventText1, _button5EventText2,
        _button5EventText3, _button6Date, _button6HolidayText, _button6EventText1, _button6EventText2, _button6EventText3, _button7Date, _button7HolidayText,
        _button7EventText1, _button7EventText2, _button7EventText3, _button8Date, _button8HolidayText, _button8EventText1, _button8EventText2, _button8EventText3, _button9Date,
        _button9HolidayText, _button9EventText1, _button9EventText2, _button9EventText3, _button10Date, _button10HolidayText, _button10EventText1, _button10EventText2,
        _button10EventText3, _button11Date, _button11HolidayText, _button11EventText1, _button11EventText2, _button11EventText3, _button12Date, _button12HolidayText,
        _button12EventText1, _button12EventText2, _button12EventText3, _button13Date, _button13HolidayText, _button13EventText1, _button13EventText2, _button13EventText3,
        _button14Date, _button14HolidayText, _button14EventText1, _button14EventText2, _button14EventText3, _button15Date, _button15HolidayText, _button15EventText1,
        _button15EventText2, _button15EventText3, _button16Date, _button16HolidayText, _button16EventText1, _button16EventText2, _button16EventText3, _button17Date,
        _button17HolidayText, _button17EventText1, _button17EventText2, _button17EventText3, _button18Date, _button18HolidayText, _button18EventText1, _button18EventText2,
        _button18EventText3, _button19Date, _button19HolidayText, _button19EventText1, _button19EventText2, _button19EventText3, _button20Date, _button20HolidayText,
        _button20EventText1, _button20EventText2, _button20EventText3, _button21Date, _button21HolidayText, _button21EventText1, _button21EventText2, _button21EventText3,
        _button22Date, _button22HolidayText, _button22EventText1, _button22EventText2, _button22EventText3, _button23Date, _button23HolidayText, _button23EventText1,
        _button23EventText2, _button23EventText3, _button24Date, _button24HolidayText, _button24EventText1, _button24EventText2, _button24EventText3, _button25Date,
        _button25HolidayText, _button25EventText1, _button25EventText2, _button25EventText3, _button26Date, _button26HolidayText, _button26EventText1, _button26EventText2,
        _button26EventText3, _button27Date, _button27HolidayText, _button27EventText1, _button27EventText2, _button27EventText3, _button28Date, _button28HolidayText,
        _button28EventText1, _button28EventText2, _button28EventText3, _button29Date, _button29HolidayText, _button29EventText1, _button29EventText2, _button29EventText3,
        _button30Date, _button30HolidayText, _button30EventText1, _button30EventText2, _button30EventText3, _button31Date, _button31HolidayText, _button31EventText1,
        _button31EventText2, _button31EventText3, _button32Date, _button32HolidayText, _button32EventText1, _button32EventText2, _button32EventText3, _button33Date,
        _button33HolidayText, _button33EventText1, _button33EventText2, _button33EventText3, _button34Date, _button34HolidayText, _button34EventText1, _button34EventText2,
        _button34EventText3, _button35Date, _button35HolidayText, _button35EventText1, _button35EventText2, _button35EventText3, _button36Date, _button36HolidayText,
        _button36EventText1, _button36EventText2, _button36EventText3, _button37Date, _button37HolidayText, _button37EventText1, _button37EventText2, _button37EventText3,
        _button38Date, _button38HolidayText, _button38EventText1, _button38EventText2, _button38EventText3, _button39Date, _button39HolidayText, _button39EventText1,
        _button39EventText2, _button39EventText3, _button40Date, _button40HolidayText, _button40EventText1, _button40EventText2, _button40EventText3, _button41Date,
        _button41HolidayText, _button41EventText1, _button41EventText2, _button41EventText3, _button42Date, _button42HolidayText, _button42EventText1, _button42EventText2,
        _button42EventText3, _currentMonthAndYear, _button1BackgroundColor, _button2BackgroundColor,
        _button3BackgroundColor, _button4BackgroundColor, _button5BackgroundColor, _button6BackgroundColor, _button7BackgroundColor, _button8BackgroundColor,
        _button9BackgroundColor, _button10BackgroundColor, _button11BackgroundColor, _button12BackgroundColor, _button13BackgroundColor, _button14BackgroundColor,
        _button15BackgroundColor, _button16BackgroundColor, _button17BackgroundColor, _button18BackgroundColor, _button19BackgroundColor, _button20BackgroundColor,
        _button21BackgroundColor, _button22BackgroundColor, _button23BackgroundColor, _button24BackgroundColor, _button25BackgroundColor, _button26BackgroundColor,
        _button27BackgroundColor, _button28BackgroundColor, _button29BackgroundColor, _button30BackgroundColor, _button31BackgroundColor, _button32BackgroundColor,
        _button33BackgroundColor, _button34BackgroundColor, _button35BackgroundColor, _button36BackgroundColor, _button37BackgroundColor, _button38BackgroundColor,
        _button39BackgroundColor, _button40BackgroundColor, _button41BackgroundColor, _button42BackgroundColor;

    private ObservableCollection<CalendarEventsCustom> _button1EventList, _button2EventList, _button3EventList, _button4EventList, _button5EventList, _button6EventList,
        _button7EventList,
        _button8EventList, _button9EventList, _button10EventList, _button11EventList, _button12EventList, _button13EventList, _button14EventList, _button15EventList,
        _button16EventList, _button17EventList, _button18EventList, _button19EventList, _button20EventList, _button21EventList, _button22EventList, _button23EventList,
        _button24EventList, _button25EventList, _button26EventList, _button27EventList, _button28EventList, _button29EventList, _button30EventList, _button31EventList,
        _button32EventList, _button33EventList, _button34EventList, _button35EventList, _button36EventList, _button37EventList, _button38EventList, _button39EventList,
        _button40EventList, _button41EventList, _button42EventList;

    private DateTime currentDateTime, button1DateTime;

    public CalendarVM() {
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

        currentDateTime = DateTime.Now;
        CurrentMonthAndYear = currentDateTime.ToString("MMMM, yyyy");
        PopulateCalendar(currentDateTime);

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            currentDateTime = DateTime.Now;
            CurrentMonthAndYear = currentDateTime.ToString("MMMM, yyyy");
            PopulateCalendar(currentDateTime);
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "addMonth":
            currentDateTime = currentDateTime.AddMonths(1);
            CurrentMonthAndYear = currentDateTime.ToString("MMMM, yyyy");
            PopulateCalendar(currentDateTime);
            break;
        case "subMonth":
            currentDateTime = currentDateTime.AddMonths(-1);
            CurrentMonthAndYear = currentDateTime.ToString("MMMM, yyyy");
            PopulateCalendar(currentDateTime);
            break;
        case "today":
            currentDateTime = DateTime.Now;
            CurrentMonthAndYear = currentDateTime.ToString("MMMM, yyyy");
            PopulateCalendar(currentDateTime);
            break;
        case "button1":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime;
                OpenEventDialog();
            }

            break;
        case "button2":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(1);
                OpenEventDialog();
            }

            break;
        case "button3":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(2);
                OpenEventDialog();
            }

            break;
        case "button4":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(3);
                OpenEventDialog();
            }

            break;
        case "button5":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(4);
                OpenEventDialog();
            }

            break;
        case "button6":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(5);
                OpenEventDialog();
            }

            break;
        case "button7":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(6);
                OpenEventDialog();
            }

            break;
        case "button8":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(7);
                OpenEventDialog();
            }

            break;
        case "button9":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(8);
                OpenEventDialog();
            }

            break;
        case "button10":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(9);
                OpenEventDialog();
            }

            break;
        case "button11":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(10);
                OpenEventDialog();
            }

            break;
        case "button12":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(11);
                OpenEventDialog();
            }

            break;
        case "button13":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(12);
                OpenEventDialog();
            }

            break;
        case "button14":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(13);
                OpenEventDialog();
            }

            break;
        case "button15":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(14);
                OpenEventDialog();
            }

            break;
        case "button16":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(15);
                OpenEventDialog();
            }

            break;
        case "button17":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(16);
                OpenEventDialog();
            }

            break;
        case "button18":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(17);
                OpenEventDialog();
            }

            break;
        case "button19":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(18);
                OpenEventDialog();
            }

            break;
        case "button20":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(19);
                OpenEventDialog();
            }

            break;
        case "button21":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(20);
                OpenEventDialog();
            }

            break;
        case "button22":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(21);
                OpenEventDialog();
            }

            break;
        case "button23":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(22);
                OpenEventDialog();
            }

            break;
        case "button24":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(23);
                OpenEventDialog();
            }

            break;
        case "button25":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(24);
                OpenEventDialog();
            }

            break;
        case "button26":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(25);
                OpenEventDialog();
            }

            break;
        case "button27":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(26);
                OpenEventDialog();
            }

            break;
        case "button28":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(27);
                OpenEventDialog();
            }

            break;
        case "button29":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(28);
                OpenEventDialog();
            }

            break;
        case "button30":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(29);
                OpenEventDialog();
            }

            break;
        case "button31":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(30);
                OpenEventDialog();
            }

            break;
        case "button32":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(31);
                OpenEventDialog();
            }

            break;
        case "button33":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(32);
                OpenEventDialog();
            }

            break;
        case "button34":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(33);
                OpenEventDialog();
            }

            break;
        case "button35":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(34);
                OpenEventDialog();
            }

            break;
        case "button36":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(35);
                OpenEventDialog();
            }

            break;
        case "button37":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(36);
                OpenEventDialog();
            }

            break;
        case "button38":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(37);
                OpenEventDialog();
            }

            break;
        case "button39":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(38);
                OpenEventDialog();
            }

            break;
        case "button40":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(39);
                OpenEventDialog();
            }

            break;
        case "button41":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(40);
                OpenEventDialog();
            }

            break;
        case "button42":
            if (!ReferenceValues.LockUI) {
                ReferenceValues.CalendarEventDate = button1DateTime.AddDays(41);
                OpenEventDialog();
            }

            break;
        }
    }

    private void OpenEventDialog() {
        EditCalendar editCalendar = new();
        editCalendar.ShowDialog();
        editCalendar.Close();
        PopulateCalendar(currentDateTime);
    }

    public void PopulateCalendar(DateTime dateTime) {
        /* Clear Calender */
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
        Button1BackgroundColor = "Transparent";
        Button2BackgroundColor = "Transparent";
        Button3BackgroundColor = "Transparent";
        Button4BackgroundColor = "Transparent";
        Button5BackgroundColor = "Transparent";
        Button6BackgroundColor = "Transparent";
        Button7BackgroundColor = "Transparent";
        Button8BackgroundColor = "Transparent";
        Button9BackgroundColor = "Transparent";
        Button10BackgroundColor = "Transparent";
        Button11BackgroundColor = "Transparent";
        Button12BackgroundColor = "Transparent";
        Button13BackgroundColor = "Transparent";
        Button14BackgroundColor = "Transparent";
        Button15BackgroundColor = "Transparent";
        Button16BackgroundColor = "Transparent";
        Button17BackgroundColor = "Transparent";
        Button18BackgroundColor = "Transparent";
        Button19BackgroundColor = "Transparent";
        Button20BackgroundColor = "Transparent";
        Button21BackgroundColor = "Transparent";
        Button22BackgroundColor = "Transparent";
        Button23BackgroundColor = "Transparent";
        Button24BackgroundColor = "Transparent";
        Button25BackgroundColor = "Transparent";
        Button26BackgroundColor = "Transparent";
        Button27BackgroundColor = "Transparent";
        Button28BackgroundColor = "Transparent";
        Button29BackgroundColor = "Transparent";
        Button30BackgroundColor = "Transparent";
        Button31BackgroundColor = "Transparent";
        Button32BackgroundColor = "Transparent";
        Button33BackgroundColor = "Transparent";
        Button34BackgroundColor = "Transparent";
        Button35BackgroundColor = "Transparent";
        Button36BackgroundColor = "Transparent";
        Button37BackgroundColor = "Transparent";
        Button38BackgroundColor = "Transparent";
        Button39BackgroundColor = "Transparent";
        Button40BackgroundColor = "Transparent";
        Button41BackgroundColor = "Transparent";
        Button42BackgroundColor = "Transparent";

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
        DateTime startingYear = new(dateTime.Year, 1, 1);

        /* Add Years */
        while (startingYear.DayOfWeek != DayOfWeek.Sunday) {
            startingYear = startingYear.AddDays(-1);
        }

        /* Add weeks */
        int mathDate = (dateTime.Month - 1) * 28;
        while (startingYear.AddDays(mathDate + 6).Month != dateTime.Month) {
            mathDate += 7;
        }

        /* Adjust Calendar with new days */
        dateTime = startingYear.AddDays(mathDate);

        Button1Date = dateTime.ToString("MMM/dd");
        Button2Date = dateTime.AddDays(1).ToString("MMM/dd");
        Button3Date = dateTime.AddDays(2).ToString("MMM/dd");
        Button4Date = dateTime.AddDays(3).ToString("MMM/dd");
        Button5Date = dateTime.AddDays(4).ToString("MMM/dd");
        Button6Date = dateTime.AddDays(5).ToString("MMM/dd");
        Button7Date = dateTime.AddDays(6).ToString("MMM/dd");
        Button8Date = dateTime.AddDays(7).ToString("MMM/dd");
        Button9Date = dateTime.AddDays(8).ToString("MMM/dd");
        Button10Date = dateTime.AddDays(9).ToString("MMM/dd");
        Button11Date = dateTime.AddDays(10).ToString("MMM/dd");
        Button12Date = dateTime.AddDays(11).ToString("MMM/dd");
        Button13Date = dateTime.AddDays(12).ToString("MMM/dd");
        Button14Date = dateTime.AddDays(13).ToString("MMM/dd");
        Button15Date = dateTime.AddDays(14).ToString("MMM/dd");
        Button16Date = dateTime.AddDays(15).ToString("MMM/dd");
        Button17Date = dateTime.AddDays(16).ToString("MMM/dd");
        Button18Date = dateTime.AddDays(17).ToString("MMM/dd");
        Button19Date = dateTime.AddDays(18).ToString("MMM/dd");
        Button20Date = dateTime.AddDays(19).ToString("MMM/dd");
        Button21Date = dateTime.AddDays(20).ToString("MMM/dd");
        Button22Date = dateTime.AddDays(21).ToString("MMM/dd");
        Button23Date = dateTime.AddDays(22).ToString("MMM/dd");
        Button24Date = dateTime.AddDays(23).ToString("MMM/dd");
        Button25Date = dateTime.AddDays(24).ToString("MMM/dd");
        Button26Date = dateTime.AddDays(25).ToString("MMM/dd");
        Button27Date = dateTime.AddDays(26).ToString("MMM/dd");
        Button28Date = dateTime.AddDays(27).ToString("MMM/dd");
        Button29Date = dateTime.AddDays(28).ToString("MMM/dd");
        Button30Date = dateTime.AddDays(29).ToString("MMM/dd");
        Button31Date = dateTime.AddDays(30).ToString("MMM/dd");
        Button32Date = dateTime.AddDays(31).ToString("MMM/dd");
        Button33Date = dateTime.AddDays(32).ToString("MMM/dd");
        Button34Date = dateTime.AddDays(33).ToString("MMM/dd");
        Button35Date = dateTime.AddDays(34).ToString("MMM/dd");
        Button36Date = dateTime.AddDays(35).ToString("MMM/dd");
        Button37Date = dateTime.AddDays(36).ToString("MMM/dd");
        Button38Date = dateTime.AddDays(37).ToString("MMM/dd");
        Button39Date = dateTime.AddDays(38).ToString("MMM/dd");
        Button40Date = dateTime.AddDays(39).ToString("MMM/dd");
        Button41Date = dateTime.AddDays(40).ToString("MMM/dd");
        Button42Date = dateTime.AddDays(41).ToString("MMM/dd");

        /* Probably only need this first button. All other buttons can just add days */
        button1DateTime = dateTime;

        /* Set background color for today */
        if (dateTime.Equals(DateTime.Today)) {
            Button1BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(1).Equals(DateTime.Today)) {
            Button2BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(2).Equals(DateTime.Today)) {
            Button3BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(3).Equals(DateTime.Today)) {
            Button4BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(4).Equals(DateTime.Today)) {
            Button5BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(5).Equals(DateTime.Today)) {
            Button6BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(6).Equals(DateTime.Today)) {
            Button7BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(7).Equals(DateTime.Today)) {
            Button8BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(8).Equals(DateTime.Today)) {
            Button9BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(9).Equals(DateTime.Today)) {
            Button10BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(10).Equals(DateTime.Today)) {
            Button11BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(11).Equals(DateTime.Today)) {
            Button12BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(12).Equals(DateTime.Today)) {
            Button13BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(13).Equals(DateTime.Today)) {
            Button14BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(14).Equals(DateTime.Today)) {
            Button15BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(15).Equals(DateTime.Today)) {
            Button16BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(16).Equals(DateTime.Today)) {
            Button17BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(17).Equals(DateTime.Today)) {
            Button18BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(18).Equals(DateTime.Today)) {
            Button19BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(19).Equals(DateTime.Today)) {
            Button20BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(20).Equals(DateTime.Today)) {
            Button21BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(21).Equals(DateTime.Today)) {
            Button22BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(22).Equals(DateTime.Today)) {
            Button23BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(23).Equals(DateTime.Today)) {
            Button24BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(24).Equals(DateTime.Today)) {
            Button25BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(25).Equals(DateTime.Today)) {
            Button26BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(26).Equals(DateTime.Today)) {
            Button27BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(27).Equals(DateTime.Today)) {
            Button28BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(28).Equals(DateTime.Today)) {
            Button29BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(29).Equals(DateTime.Today)) {
            Button30BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(30).Equals(DateTime.Today)) {
            Button31BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(31).Equals(DateTime.Today)) {
            Button32BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(32).Equals(DateTime.Today)) {
            Button33BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(33).Equals(DateTime.Today)) {
            Button34BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(34).Equals(DateTime.Today)) {
            Button35BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(35).Equals(DateTime.Today)) {
            Button36BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(36).Equals(DateTime.Today)) {
            Button37BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(37).Equals(DateTime.Today)) {
            Button38BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(38).Equals(DateTime.Today)) {
            Button39BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(39).Equals(DateTime.Today)) {
            Button40BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(40).Equals(DateTime.Today)) {
            Button41BackgroundColor = "DarkBlue";
        } else if (dateTime.AddDays(41).Equals(DateTime.Today)) {
            Button42BackgroundColor = "DarkBlue";
        }

        /* Get Holidays (Hardcoded) */
        foreach (HolidayBlock holiday in GetHolidays(dateTime.AddDays(7).Year)) {
            if (dateTime.Month == holiday.Date.Month && dateTime.Day == holiday.Date.Day) {
                Button1HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(1).Month == holiday.Date.Month && dateTime.AddDays(1).Day == holiday.Date.Day) {
                Button2HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(2).Month == holiday.Date.Month && dateTime.AddDays(2).Day == holiday.Date.Day) {
                Button3HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(3).Month == holiday.Date.Month && dateTime.AddDays(3).Day == holiday.Date.Day) {
                Button4HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(4).Month == holiday.Date.Month && dateTime.AddDays(4).Day == holiday.Date.Day) {
                Button5HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(5).Month == holiday.Date.Month && dateTime.AddDays(5).Day == holiday.Date.Day) {
                Button6HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(6).Month == holiday.Date.Month && dateTime.AddDays(6).Day == holiday.Date.Day) {
                Button7HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(7).Month == holiday.Date.Month && dateTime.AddDays(7).Day == holiday.Date.Day) {
                Button8HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(8).Month == holiday.Date.Month && dateTime.AddDays(8).Day == holiday.Date.Day) {
                Button9HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(9).Month == holiday.Date.Month && dateTime.AddDays(9).Day == holiday.Date.Day) {
                Button10HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(10).Month == holiday.Date.Month && dateTime.AddDays(10).Day == holiday.Date.Day) {
                Button11HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(11).Month == holiday.Date.Month && dateTime.AddDays(11).Day == holiday.Date.Day) {
                Button12HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(12).Month == holiday.Date.Month && dateTime.AddDays(12).Day == holiday.Date.Day) {
                Button13HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(13).Month == holiday.Date.Month && dateTime.AddDays(13).Day == holiday.Date.Day) {
                Button14HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(14).Month == holiday.Date.Month && dateTime.AddDays(14).Day == holiday.Date.Day) {
                Button15HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(15).Month == holiday.Date.Month && dateTime.AddDays(15).Day == holiday.Date.Day) {
                Button16HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(16).Month == holiday.Date.Month && dateTime.AddDays(16).Day == holiday.Date.Day) {
                Button17HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(17).Month == holiday.Date.Month && dateTime.AddDays(17).Day == holiday.Date.Day) {
                Button18HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(18).Month == holiday.Date.Month && dateTime.AddDays(18).Day == holiday.Date.Day) {
                Button19HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(19).Month == holiday.Date.Month && dateTime.AddDays(19).Day == holiday.Date.Day) {
                Button20HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(20).Month == holiday.Date.Month && dateTime.AddDays(20).Day == holiday.Date.Day) {
                Button21HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(21).Month == holiday.Date.Month && dateTime.AddDays(21).Day == holiday.Date.Day) {
                Button22HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(22).Month == holiday.Date.Month && dateTime.AddDays(22).Day == holiday.Date.Day) {
                Button23HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(23).Month == holiday.Date.Month && dateTime.AddDays(23).Day == holiday.Date.Day) {
                Button24HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(24).Month == holiday.Date.Month && dateTime.AddDays(24).Day == holiday.Date.Day) {
                Button25HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(25).Month == holiday.Date.Month && dateTime.AddDays(25).Day == holiday.Date.Day) {
                Button26HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(26).Month == holiday.Date.Month && dateTime.AddDays(26).Day == holiday.Date.Day) {
                Button27HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(27).Month == holiday.Date.Month && dateTime.AddDays(27).Day == holiday.Date.Day) {
                Button28HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(28).Month == holiday.Date.Month && dateTime.AddDays(28).Day == holiday.Date.Day) {
                Button29HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(29).Month == holiday.Date.Month && dateTime.AddDays(29).Day == holiday.Date.Day) {
                Button30HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(30).Month == holiday.Date.Month && dateTime.AddDays(30).Day == holiday.Date.Day) {
                Button31HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(31).Month == holiday.Date.Month && dateTime.AddDays(31).Day == holiday.Date.Day) {
                Button32HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(32).Month == holiday.Date.Month && dateTime.AddDays(32).Day == holiday.Date.Day) {
                Button33HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(33).Month == holiday.Date.Month && dateTime.AddDays(33).Day == holiday.Date.Day) {
                Button34HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(34).Month == holiday.Date.Month && dateTime.AddDays(34).Day == holiday.Date.Day) {
                Button35HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(35).Month == holiday.Date.Month && dateTime.AddDays(35).Day == holiday.Date.Day) {
                Button36HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(36).Month == holiday.Date.Month && dateTime.AddDays(36).Day == holiday.Date.Day) {
                Button37HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(37).Month == holiday.Date.Month && dateTime.AddDays(37).Day == holiday.Date.Day) {
                Button38HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(38).Month == holiday.Date.Month && dateTime.AddDays(38).Day == holiday.Date.Day) {
                Button39HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(39).Month == holiday.Date.Month && dateTime.AddDays(39).Day == holiday.Date.Day) {
                Button40HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(40).Month == holiday.Date.Month && dateTime.AddDays(40).Day == holiday.Date.Day) {
                Button41HolidayText = holiday.Holiday;
            }

            if (dateTime.AddDays(41).Month == holiday.Date.Month && dateTime.AddDays(41).Day == holiday.Date.Day) {
                Button42HolidayText = holiday.Holiday;
            }
        }

        /* Get Calendar Events */
        new CalenderEventsFromJson(button1DateTime);

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[0].eventsList) {
                Button1EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[1].eventsList) {
                Button2EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[2].eventsList) {
                Button3EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[3].eventsList) {
                Button4EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[4].eventsList) {
                Button5EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[5].eventsList) {
                Button6EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[6].eventsList) {
                Button7EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[7].eventsList) {
                Button8EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[8].eventsList) {
                Button9EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[9].eventsList) {
                Button10EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[10].eventsList) {
                Button11EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[11].eventsList) {
                Button12EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[12].eventsList) {
                Button13EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[13].eventsList) {
                Button14EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[14].eventsList) {
                Button15EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[15].eventsList) {
                Button16EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[16].eventsList) {
                Button17EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[17].eventsList) {
                Button18EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[18].eventsList) {
                Button19EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[19].eventsList) {
                Button20EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[20].eventsList) {
                Button21EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[21].eventsList) {
                Button22EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[22].eventsList) {
                Button23EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[23].eventsList) {
                Button24EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[24].eventsList) {
                Button25EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[25].eventsList) {
                Button26EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[26].eventsList) {
                Button27EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[27].eventsList) {
                Button28EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[28].eventsList) {
                Button29EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[29].eventsList) {
                Button30EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[30].eventsList) {
                Button31EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[31].eventsList) {
                Button32EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[32].eventsList) {
                Button33EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[33].eventsList) {
                Button34EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[34].eventsList) {
                Button35EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[35].eventsList) {
                Button36EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[36].eventsList) {
                Button37EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[37].eventsList) {
                Button38EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[38].eventsList) {
                Button39EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[39].eventsList) {
                Button40EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[40].eventsList) {
                Button41EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }

        try {
            foreach (CalendarEvents t in ReferenceValues.JsonCalendarMasterEventList[41].eventsList) {
                Button42EventList.Add(new CalendarEventsCustom {
                    name = t.startTime + " - " + t.endTime + "  " + t.name,
                    person = GetIdFromPerson(t.person)
                });
            }
        } catch (Exception) { }
    }

    private static int GetIdFromPerson(string tPerson) {
        if (tPerson == ReferenceValues.JsonMasterSettings.User1Name) {
            return 1;
        }

        if (tPerson == ReferenceValues.JsonMasterSettings.User2Name) {
            return 2;
        }

        return tPerson switch {
            "Children" => 3,
            "Home" => 4,
            _ => 5
        };
    }

    private static List<HolidayBlock> GetHolidays(int year) {
        List<HolidayBlock> holidays = new();

        // New Years Day -- January 1st
        DateTime newYearsDate = new(year, 1, 1);
        holidays.Add(new HolidayBlock {
            Date = newYearsDate,
            Holiday = "New Year's"
        });

        // MLK Day -- 3rd Monday in January 
        int mlk = (from day in Enumerable.Range(1, 31)
            where new DateTime(year, 1, day).DayOfWeek == DayOfWeek.Monday
            select day).ElementAt(2);
        DateTime mlkDay = new(year, 1, mlk);
        holidays.Add(new HolidayBlock {
            Date = mlkDay,
            Holiday = "MLK Day"
        });

        // Valentine's Day -- February 14th
        DateTime valentinesDay = new(year, 2, 14);
        holidays.Add(new HolidayBlock {
            Date = valentinesDay,
            Holiday = "Valentine's Day"
        });

        // Presidents Day -- 3rd Monday in February 
        int presidents = (from day in Enumerable.Range(1, 28)
            where new DateTime(year, 2, day).DayOfWeek == DayOfWeek.Monday
            select day).ElementAt(2);
        DateTime presidentsDay = new(year, 2, presidents);
        holidays.Add(new HolidayBlock {
            Date = presidentsDay,
            Holiday = "President's Day"
        });

        // Easter Sunday -- Complicated 
        int g = year % 19;
        int c = year / 100;
        int h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
        int i = h - h / 28 * (1 - h / 28 * (29 / (h + 1)) * ((21 - g) / 11));

        int easterDay = i - (year + year / 4 + i + 2 - c + c / 4) % 7 + 28;
        int easterMonth = 3;

        if (easterDay > 31) {
            easterMonth++;
            easterDay -= 31;
        }

        holidays.Add(new HolidayBlock {
            Date = new DateTime(year, easterMonth, easterDay),
            Holiday = "Easter"
        });

        // Mother's Day -- 2nd Sunday in May
        int mothers = (from day in Enumerable.Range(1, 31)
            where new DateTime(year, 5, day).DayOfWeek == DayOfWeek.Sunday
            select day).ElementAt(1);
        DateTime mothersDay = new(year, 5, mothers);
        holidays.Add(new HolidayBlock {
            Date = mothersDay,
            Holiday = "Mother's Day"
        });

        // Memorial Day -- Last monday in May 
        DateTime memorialDay = new(year, 5, 31);
        DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
        while (dayOfWeek != DayOfWeek.Monday) {
            memorialDay = memorialDay.AddDays(-1);
            dayOfWeek = memorialDay.DayOfWeek;
        }

        holidays.Add(new HolidayBlock {
            Date = memorialDay,
            Holiday = "Memorial Day"
        });

        // Father's Day -- 3nd Sunday in June
        int fathers = (from day in Enumerable.Range(1, 30)
            where new DateTime(year, 6, day).DayOfWeek == DayOfWeek.Sunday
            select day).ElementAt(2);
        DateTime fathersDay = new(year, 6, fathers);
        holidays.Add(new HolidayBlock {
            Date = fathersDay,
            Holiday = "Father's Day"
        });

        // Juneteenth - June 19th
        DateTime juneteenth = new(year, 6, 19);
        holidays.Add(new HolidayBlock {
            Date = juneteenth,
            Holiday = "Juneteenth"
        });


        // Independence Day - July 4th
        DateTime independenceDay = new(year, 7, 4);
        holidays.Add(new HolidayBlock {
            Date = independenceDay,
            Holiday = "Independence Day"
        });

        // Labor Day -- 1st Monday in September 
        DateTime laborDay = new(year, 9, 1);
        dayOfWeek = laborDay.DayOfWeek;
        while (dayOfWeek != DayOfWeek.Monday) {
            laborDay = laborDay.AddDays(1);
            dayOfWeek = laborDay.DayOfWeek;
        }

        holidays.Add(new HolidayBlock {
            Date = laborDay,
            Holiday = "Labor Day"
        });

        // Veterans Day -- November 11th
        DateTime veteransDay = new(year, 11, 11);
        holidays.Add(new HolidayBlock {
            Date = veteransDay,
            Holiday = "Veterans Day"
        });

        // Halloween -- October 31st
        DateTime halloweenDay = new(year, 10, 31);
        holidays.Add(new HolidayBlock {
            Date = halloweenDay,
            Holiday = "Halloween"
        });

        // Thanksgiving Day -- 4th Thursday in November 
        int thanksgiving = (from day in Enumerable.Range(1, 30)
            where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
            select day).ElementAt(3);
        DateTime thanksgivingDay = new(year, 11, thanksgiving);
        holidays.Add(new HolidayBlock {
            Date = thanksgivingDay,
            Holiday = "Thanksgiving"
        });

        // Day After Thanksgiving
        holidays.Add(new HolidayBlock {
            Date = thanksgivingDay.AddDays(1),
            Holiday = "Black Friday"
        });

        // Christmas Eve -- December 24th
        DateTime christmasEve = new(year, 12, 24);
        holidays.Add(new HolidayBlock {
            Date = christmasEve,
            Holiday = "Christmas Eve"
        });

        // Christmas Day 
        holidays.Add(new HolidayBlock {
            Date = christmasEve.AddDays(1),
            Holiday = "Christmas Day"
        });

        // Daylight Savings Start -- 2nd Sunday of March
        int dlsStart = (from day in Enumerable.Range(1, 31)
            where new DateTime(year, 3, day).DayOfWeek == DayOfWeek.Sunday
            select day).ElementAt(1);
        DateTime dlsStartDay = new(year, 3, dlsStart);
        holidays.Add(new HolidayBlock {
            Date = dlsStartDay,
            Holiday = "DST Start"
        });

        // Daylight Savings End -- 1st Sunday in November
        DateTime dlsEnd = new(year, 11, 1);
        dayOfWeek = dlsEnd.DayOfWeek;
        while (dayOfWeek != DayOfWeek.Sunday) {
            dlsEnd = dlsEnd.AddDays(1);
            dayOfWeek = dlsEnd.DayOfWeek;
        }

        holidays.Add(new HolidayBlock {
            Date = dlsEnd,
            Holiday = "DST End"
        });

        return holidays;
    }

    private class HolidayBlock {
        public DateTime Date { get; set; }
        public string Holiday { get; set; }
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

    public string Button1BackgroundColor {
        get => _button1BackgroundColor;
        set {
            _button1BackgroundColor = value;
            RaisePropertyChangedEvent("Button1BackgroundColor");
        }
    }

    public string Button2BackgroundColor {
        get => _button2BackgroundColor;
        set {
            _button2BackgroundColor = value;
            RaisePropertyChangedEvent("Button2BackgroundColor");
        }
    }

    public string Button3BackgroundColor {
        get => _button3BackgroundColor;
        set {
            _button3BackgroundColor = value;
            RaisePropertyChangedEvent("Button3BackgroundColor");
        }
    }

    public string Button4BackgroundColor {
        get => _button4BackgroundColor;
        set {
            _button4BackgroundColor = value;
            RaisePropertyChangedEvent("Button4BackgroundColor");
        }
    }

    public string Button5BackgroundColor {
        get => _button5BackgroundColor;
        set {
            _button5BackgroundColor = value;
            RaisePropertyChangedEvent("Button5BackgroundColor");
        }
    }

    public string Button6BackgroundColor {
        get => _button6BackgroundColor;
        set {
            _button6BackgroundColor = value;
            RaisePropertyChangedEvent("Button6BackgroundColor");
        }
    }

    public string Button7BackgroundColor {
        get => _button7BackgroundColor;
        set {
            _button7BackgroundColor = value;
            RaisePropertyChangedEvent("Button7BackgroundColor");
        }
    }

    public string Button8BackgroundColor {
        get => _button8BackgroundColor;
        set {
            _button8BackgroundColor = value;
            RaisePropertyChangedEvent("Button8BackgroundColor");
        }
    }

    public string Button9BackgroundColor {
        get => _button9BackgroundColor;
        set {
            _button9BackgroundColor = value;
            RaisePropertyChangedEvent("Button9BackgroundColor");
        }
    }

    public string Button10BackgroundColor {
        get => _button10BackgroundColor;
        set {
            _button10BackgroundColor = value;
            RaisePropertyChangedEvent("Button10BackgroundColor");
        }
    }

    public string Button11BackgroundColor {
        get => _button11BackgroundColor;
        set {
            _button11BackgroundColor = value;
            RaisePropertyChangedEvent("Button11BackgroundColor");
        }
    }

    public string Button12BackgroundColor {
        get => _button12BackgroundColor;
        set {
            _button12BackgroundColor = value;
            RaisePropertyChangedEvent("Button12BackgroundColor");
        }
    }

    public string Button13BackgroundColor {
        get => _button13BackgroundColor;
        set {
            _button13BackgroundColor = value;
            RaisePropertyChangedEvent("Button13BackgroundColor");
        }
    }

    public string Button14BackgroundColor {
        get => _button14BackgroundColor;
        set {
            _button14BackgroundColor = value;
            RaisePropertyChangedEvent("Button14BackgroundColor");
        }
    }

    public string Button15BackgroundColor {
        get => _button15BackgroundColor;
        set {
            _button15BackgroundColor = value;
            RaisePropertyChangedEvent("Button15BackgroundColor");
        }
    }

    public string Button16BackgroundColor {
        get => _button16BackgroundColor;
        set {
            _button16BackgroundColor = value;
            RaisePropertyChangedEvent("Button16BackgroundColor");
        }
    }

    public string Button17BackgroundColor {
        get => _button17BackgroundColor;
        set {
            _button17BackgroundColor = value;
            RaisePropertyChangedEvent("Button17BackgroundColor");
        }
    }

    public string Button18BackgroundColor {
        get => _button18BackgroundColor;
        set {
            _button18BackgroundColor = value;
            RaisePropertyChangedEvent("Button18BackgroundColor");
        }
    }

    public string Button19BackgroundColor {
        get => _button19BackgroundColor;
        set {
            _button19BackgroundColor = value;
            RaisePropertyChangedEvent("Button19BackgroundColor");
        }
    }

    public string Button20BackgroundColor {
        get => _button20BackgroundColor;
        set {
            _button20BackgroundColor = value;
            RaisePropertyChangedEvent("Button20BackgroundColor");
        }
    }

    public string Button21BackgroundColor {
        get => _button21BackgroundColor;
        set {
            _button21BackgroundColor = value;
            RaisePropertyChangedEvent("Button21BackgroundColor");
        }
    }

    public string Button22BackgroundColor {
        get => _button22BackgroundColor;
        set {
            _button22BackgroundColor = value;
            RaisePropertyChangedEvent("Button22BackgroundColor");
        }
    }

    public string Button23BackgroundColor {
        get => _button23BackgroundColor;
        set {
            _button23BackgroundColor = value;
            RaisePropertyChangedEvent("Button23BackgroundColor");
        }
    }

    public string Button24BackgroundColor {
        get => _button24BackgroundColor;
        set {
            _button24BackgroundColor = value;
            RaisePropertyChangedEvent("Button24BackgroundColor");
        }
    }

    public string Button25BackgroundColor {
        get => _button25BackgroundColor;
        set {
            _button25BackgroundColor = value;
            RaisePropertyChangedEvent("Button25BackgroundColor");
        }
    }

    public string Button26BackgroundColor {
        get => _button26BackgroundColor;
        set {
            _button26BackgroundColor = value;
            RaisePropertyChangedEvent("Button26BackgroundColor");
        }
    }

    public string Button27BackgroundColor {
        get => _button27BackgroundColor;
        set {
            _button27BackgroundColor = value;
            RaisePropertyChangedEvent("Button27BackgroundColor");
        }
    }

    public string Button28BackgroundColor {
        get => _button28BackgroundColor;
        set {
            _button28BackgroundColor = value;
            RaisePropertyChangedEvent("Button28BackgroundColor");
        }
    }

    public string Button29BackgroundColor {
        get => _button29BackgroundColor;
        set {
            _button29BackgroundColor = value;
            RaisePropertyChangedEvent("Button29BackgroundColor");
        }
    }

    public string Button30BackgroundColor {
        get => _button30BackgroundColor;
        set {
            _button30BackgroundColor = value;
            RaisePropertyChangedEvent("Button30BackgroundColor");
        }
    }

    public string Button31BackgroundColor {
        get => _button31BackgroundColor;
        set {
            _button31BackgroundColor = value;
            RaisePropertyChangedEvent("Button31BackgroundColor");
        }
    }

    public string Button32BackgroundColor {
        get => _button32BackgroundColor;
        set {
            _button32BackgroundColor = value;
            RaisePropertyChangedEvent("Button32BackgroundColor");
        }
    }

    public string Button33BackgroundColor {
        get => _button33BackgroundColor;
        set {
            _button33BackgroundColor = value;
            RaisePropertyChangedEvent("Button33BackgroundColor");
        }
    }

    public string Button34BackgroundColor {
        get => _button34BackgroundColor;
        set {
            _button34BackgroundColor = value;
            RaisePropertyChangedEvent("Button34BackgroundColor");
        }
    }

    public string Button35BackgroundColor {
        get => _button35BackgroundColor;
        set {
            _button35BackgroundColor = value;
            RaisePropertyChangedEvent("Button35BackgroundColor");
        }
    }

    public string Button36BackgroundColor {
        get => _button36BackgroundColor;
        set {
            _button36BackgroundColor = value;
            RaisePropertyChangedEvent("Button36BackgroundColor");
        }
    }

    public string Button37BackgroundColor {
        get => _button37BackgroundColor;
        set {
            _button37BackgroundColor = value;
            RaisePropertyChangedEvent("Button37BackgroundColor");
        }
    }

    public string Button38BackgroundColor {
        get => _button38BackgroundColor;
        set {
            _button38BackgroundColor = value;
            RaisePropertyChangedEvent("Button38BackgroundColor");
        }
    }

    public string Button39BackgroundColor {
        get => _button39BackgroundColor;
        set {
            _button39BackgroundColor = value;
            RaisePropertyChangedEvent("Button39BackgroundColor");
        }
    }

    public string Button40BackgroundColor {
        get => _button40BackgroundColor;
        set {
            _button40BackgroundColor = value;
            RaisePropertyChangedEvent("Button40BackgroundColor");
        }
    }

    public string Button41BackgroundColor {
        get => _button41BackgroundColor;
        set {
            _button41BackgroundColor = value;
            RaisePropertyChangedEvent("Button41BackgroundColor");
        }
    }

    public string Button42BackgroundColor {
        get => _button42BackgroundColor;
        set {
            _button42BackgroundColor = value;
            RaisePropertyChangedEvent("Button42BackgroundColor");
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

    #endregion
}