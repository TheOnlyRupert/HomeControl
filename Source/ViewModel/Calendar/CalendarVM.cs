using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Calendar;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class CalendarVM : BaseViewModel {
    private string _button1Date, _button1HolidayText, _button1EventText1, _button1EventText2, _button1EventText3, _button2Date, _button2HolidayText, _button2EventText1,
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
        _button42EventText3, _currentMonthAndYear, _button1EventColor1, _button1EventColor2, _button1EventColor3, _button2EventColor1, _button2EventColor2, _button2EventColor3,
        _button3EventColor1, _button3EventColor2, _button3EventColor3, _button4EventColor1, _button4EventColor2, _button4EventColor3, _button5EventColor1, _button5EventColor2,
        _button5EventColor3, _button6EventColor1, _button6EventColor2, _button6EventColor3, _button7EventColor1, _button7EventColor2, _button7EventColor3, _button8EventColor1,
        _button8EventColor2, _button8EventColor3, _button9EventColor1, _button9EventColor2, _button9EventColor3, _button10EventColor1, _button10EventColor2, _button10EventColor3,
        _button11EventColor1, _button11EventColor2, _button11EventColor3, _button12EventColor1, _button12EventColor2, _button12EventColor3, _button13EventColor1,
        _button13EventColor2, _button13EventColor3, _button14EventColor1, _button14EventColor2, _button14EventColor3, _button15EventColor1, _button15EventColor2,
        _button15EventColor3, _button16EventColor1, _button16EventColor2, _button16EventColor3, _button17EventColor1, _button17EventColor2, _button17EventColor3,
        _button18EventColor1, _button18EventColor2, _button18EventColor3, _button19EventColor1, _button19EventColor2, _button19EventColor3, _button20EventColor1,
        _button20EventColor2, _button20EventColor3, _button21EventColor1, _button21EventColor2, _button21EventColor3, _button22EventColor1, _button22EventColor2,
        _button22EventColor3, _button23EventColor1, _button23EventColor2, _button23EventColor3, _button24EventColor1, _button24EventColor2, _button24EventColor3,
        _button25EventColor1, _button25EventColor2, _button25EventColor3, _button26EventColor1, _button26EventColor2, _button26EventColor3, _button27EventColor1,
        _button27EventColor2, _button27EventColor3, _button28EventColor1, _button28EventColor2, _button28EventColor3, _button29EventColor1, _button29EventColor2,
        _button29EventColor3, _button30EventColor1, _button30EventColor2, _button30EventColor3, _button31EventColor1, _button31EventColor2, _button31EventColor3,
        _button32EventColor1, _button32EventColor2, _button32EventColor3, _button33EventColor1, _button33EventColor2, _button33EventColor3, _button34EventColor1,
        _button34EventColor2, _button34EventColor3, _button35EventColor1, _button35EventColor2, _button35EventColor3, _button36EventColor1, _button36EventColor2,
        _button36EventColor3, _button37EventColor1, _button37EventColor2, _button37EventColor3, _button38EventColor1, _button38EventColor2, _button38EventColor3,
        _button39EventColor1, _button39EventColor2, _button39EventColor3, _button40EventColor1, _button40EventColor2, _button40EventColor3, _button41EventColor1,
        _button41EventColor2, _button41EventColor3, _button42EventColor1, _button42EventColor2, _button42EventColor3, _button1BackgroundColor, _button2BackgroundColor,
        _button3BackgroundColor, _button4BackgroundColor, _button5BackgroundColor, _button6BackgroundColor, _button7BackgroundColor, _button8BackgroundColor,
        _button9BackgroundColor, _button10BackgroundColor, _button11BackgroundColor, _button12BackgroundColor, _button13BackgroundColor, _button14BackgroundColor,
        _button15BackgroundColor, _button16BackgroundColor, _button17BackgroundColor, _button18BackgroundColor, _button19BackgroundColor, _button20BackgroundColor,
        _button21BackgroundColor, _button22BackgroundColor, _button23BackgroundColor, _button24BackgroundColor, _button25BackgroundColor, _button26BackgroundColor,
        _button27BackgroundColor, _button28BackgroundColor, _button29BackgroundColor, _button30BackgroundColor, _button31BackgroundColor, _button32BackgroundColor,
        _button33BackgroundColor, _button34BackgroundColor, _button35BackgroundColor, _button36BackgroundColor, _button37BackgroundColor, _button38BackgroundColor,
        _button39BackgroundColor, _button40BackgroundColor, _button41BackgroundColor, _button42BackgroundColor;

    private DateTime currentDateTime, button1DateTime;

    public CalendarVM() {
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
            ReferenceValues.CalendarEventDate = button1DateTime;
            OpenEventDialog();
            break;
        case "button2":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(1);
            OpenEventDialog();
            break;
        case "button3":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(2);
            OpenEventDialog();
            break;
        case "button4":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(3);
            OpenEventDialog();
            break;
        case "button5":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(4);
            OpenEventDialog();
            break;
        case "button6":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(5);
            OpenEventDialog();
            break;
        case "button7":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(6);
            OpenEventDialog();
            break;
        case "button8":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(7);
            OpenEventDialog();
            break;
        case "button9":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(8);
            OpenEventDialog();
            break;
        case "button10":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(9);
            OpenEventDialog();
            break;
        case "button11":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(10);
            OpenEventDialog();
            break;
        case "button12":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(11);
            OpenEventDialog();
            break;
        case "button13":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(12);
            OpenEventDialog();
            break;
        case "button14":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(13);
            OpenEventDialog();
            break;
        case "button15":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(14);
            OpenEventDialog();
            break;
        case "button16":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(15);
            OpenEventDialog();
            break;
        case "button17":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(16);
            OpenEventDialog();
            break;
        case "button18":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(17);
            OpenEventDialog();
            break;
        case "button19":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(18);
            OpenEventDialog();
            break;
        case "button20":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(19);
            OpenEventDialog();
            break;
        case "button21":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(20);
            OpenEventDialog();
            break;
        case "button22":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(21);
            OpenEventDialog();
            break;
        case "button23":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(22);
            OpenEventDialog();
            break;
        case "button24":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(23);
            OpenEventDialog();
            break;
        case "button25":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(24);
            OpenEventDialog();
            break;
        case "button26":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(25);
            OpenEventDialog();
            break;
        case "button27":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(26);
            OpenEventDialog();
            break;
        case "button28":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(27);
            OpenEventDialog();
            break;
        case "button29":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(28);
            OpenEventDialog();
            break;
        case "button30":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(29);
            OpenEventDialog();
            break;
        case "button31":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(30);
            OpenEventDialog();
            break;
        case "button32":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(31);
            OpenEventDialog();
            break;
        case "button33":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(32);
            OpenEventDialog();
            break;
        case "button34":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(33);
            OpenEventDialog();
            break;
        case "button35":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(34);
            OpenEventDialog();
            break;
        case "button36":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(35);
            OpenEventDialog();
            break;
        case "button37":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(36);
            OpenEventDialog();
            break;
        case "button38":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(37);
            OpenEventDialog();
            break;
        case "button39":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(38);
            OpenEventDialog();
            break;
        case "button40":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(39);
            OpenEventDialog();
            break;
        case "button41":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(40);
            OpenEventDialog();
            break;
        case "button42":
            ReferenceValues.CalendarEventDate = button1DateTime.AddDays(41);
            OpenEventDialog();
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
        Button1EventText1 = "";
        Button1EventText2 = "";
        Button1EventText3 = "";
        Button2HolidayText = "";
        Button2EventText1 = "";
        Button2EventText2 = "";
        Button2EventText3 = "";
        Button3HolidayText = "";
        Button3EventText1 = "";
        Button3EventText2 = "";
        Button3EventText3 = "";
        Button4HolidayText = "";
        Button4EventText1 = "";
        Button4EventText2 = "";
        Button4EventText3 = "";
        Button5HolidayText = "";
        Button5EventText1 = "";
        Button5EventText2 = "";
        Button5EventText3 = "";
        Button6EventText1 = "";
        Button6EventText2 = "";
        Button6EventText3 = "";
        Button7HolidayText = "";
        Button7EventText1 = "";
        Button7EventText2 = "";
        Button7EventText3 = "";
        Button8HolidayText = "";
        Button8EventText1 = "";
        Button8EventText2 = "";
        Button8EventText3 = "";
        Button9HolidayText = "";
        Button9EventText1 = "";
        Button9EventText2 = "";
        Button9EventText3 = "";
        Button10HolidayText = "";
        Button10EventText1 = "";
        Button10EventText2 = "";
        Button10EventText3 = "";
        Button11HolidayText = "";
        Button11EventText1 = "";
        Button11EventText2 = "";
        Button11EventText3 = "";
        Button12HolidayText = "";
        Button12EventText1 = "";
        Button12EventText2 = "";
        Button12EventText3 = "";
        Button13HolidayText = "";
        Button13EventText1 = "";
        Button13EventText2 = "";
        Button13EventText3 = "";
        Button14HolidayText = "";
        Button14EventText1 = "";
        Button14EventText2 = "";
        Button14EventText3 = "";
        Button15HolidayText = "";
        Button15EventText1 = "";
        Button15EventText2 = "";
        Button15EventText3 = "";
        Button16HolidayText = "";
        Button16EventText1 = "";
        Button16EventText2 = "";
        Button16EventText3 = "";
        Button17HolidayText = "";
        Button17EventText1 = "";
        Button17EventText2 = "";
        Button17EventText3 = "";
        Button18HolidayText = "";
        Button18EventText1 = "";
        Button18EventText2 = "";
        Button18EventText3 = "";
        Button19HolidayText = "";
        Button19EventText1 = "";
        Button19EventText2 = "";
        Button19EventText3 = "";
        Button20HolidayText = "";
        Button20EventText1 = "";
        Button20EventText2 = "";
        Button20EventText3 = "";
        Button21HolidayText = "";
        Button21EventText1 = "";
        Button21EventText2 = "";
        Button21EventText3 = "";
        Button22HolidayText = "";
        Button22EventText1 = "";
        Button22EventText2 = "";
        Button22EventText3 = "";
        Button23HolidayText = "";
        Button23EventText1 = "";
        Button23EventText2 = "";
        Button23EventText3 = "";
        Button24HolidayText = "";
        Button24EventText1 = "";
        Button24EventText2 = "";
        Button24EventText3 = "";
        Button25HolidayText = "";
        Button25EventText1 = "";
        Button25EventText2 = "";
        Button25EventText3 = "";
        Button26HolidayText = "";
        Button26EventText1 = "";
        Button26EventText2 = "";
        Button26EventText3 = "";
        Button27HolidayText = "";
        Button27EventText1 = "";
        Button27EventText2 = "";
        Button27EventText3 = "";
        Button28HolidayText = "";
        Button28EventText1 = "";
        Button28EventText2 = "";
        Button28EventText3 = "";
        Button29HolidayText = "";
        Button29EventText1 = "";
        Button29EventText2 = "";
        Button29EventText3 = "";
        Button30HolidayText = "";
        Button30EventText1 = "";
        Button30EventText2 = "";
        Button30EventText3 = "";
        Button31HolidayText = "";
        Button31EventText1 = "";
        Button31EventText2 = "";
        Button31EventText3 = "";
        Button32HolidayText = "";
        Button32EventText1 = "";
        Button32EventText2 = "";
        Button32EventText3 = "";
        Button33HolidayText = "";
        Button33EventText1 = "";
        Button33EventText2 = "";
        Button33EventText3 = "";
        Button34HolidayText = "";
        Button34EventText1 = "";
        Button34EventText2 = "";
        Button34EventText3 = "";
        Button35HolidayText = "";
        Button35EventText1 = "";
        Button35EventText2 = "";
        Button35EventText3 = "";
        Button36HolidayText = "";
        Button36EventText1 = "";
        Button36EventText2 = "";
        Button36EventText3 = "";
        Button37HolidayText = "";
        Button37EventText1 = "";
        Button37EventText2 = "";
        Button37EventText3 = "";
        Button38HolidayText = "";
        Button38EventText1 = "";
        Button38EventText2 = "";
        Button38EventText3 = "";
        Button39HolidayText = "";
        Button39EventText1 = "";
        Button39EventText2 = "";
        Button39EventText3 = "";
        Button40HolidayText = "";
        Button40EventText1 = "";
        Button40EventText2 = "";
        Button40EventText3 = "";
        Button41HolidayText = "";
        Button41EventText1 = "";
        Button41EventText2 = "";
        Button41EventText3 = "";
        Button42HolidayText = "";
        Button42EventText1 = "";
        Button42EventText2 = "";
        Button42EventText3 = "";
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
        PopulateEvents();
    }

    private static List<HolidayBlock> GetHolidays(int year) {
        List<HolidayBlock> holidays = new();

        // New Years Day -- January 1st
        DateTime newYearsDate = new(year, 1, 1);
        holidays.Add(new HolidayBlock {
            Date = newYearsDate,
            Holiday = "New Year's Day"
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
            Holiday = "Thanksgiving Day"
        });

        // Day After Thanksgiving
        holidays.Add(new HolidayBlock {
            Date = thanksgivingDay.AddDays(1),
            Holiday = "Day After Thanksgiving"
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
            Holiday = "Daylight Savings Start"
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
            Holiday = "Daylight Savings End"
        });

        return holidays;
    }


    private void PopulateEvents() {
        /* Button 1 */
        try {
            Button1EventText1 = ReferenceValues.JsonCalendarMasterEventList[0].eventsList[0].name;
            Button1EventText2 = ReferenceValues.JsonCalendarMasterEventList[0].eventsList[1].name;
            Button1EventText3 = ReferenceValues.JsonCalendarMasterEventList[0].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button1EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[0].eventsList[0].person);
            Button1EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[0].eventsList[1].person);
            Button1EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[0].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[0].eventsList.Count > 3) {
                Button1EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[0].eventsList.Count - 2) + " More Events...";
                Button1EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 2 */
        try {
            Button2EventText1 = ReferenceValues.JsonCalendarMasterEventList[1].eventsList[0].name;
            Button2EventText2 = ReferenceValues.JsonCalendarMasterEventList[1].eventsList[1].name;
            Button2EventText3 = ReferenceValues.JsonCalendarMasterEventList[1].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button2EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[1].eventsList[0].person);
            Button2EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[1].eventsList[1].person);
            Button2EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[1].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[1].eventsList.Count > 3) {
                Button2EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[1].eventsList.Count - 2) + " More Events...";
                Button2EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 3 */
        try {
            Button3EventText1 = ReferenceValues.JsonCalendarMasterEventList[2].eventsList[0].name;
            Button3EventText2 = ReferenceValues.JsonCalendarMasterEventList[2].eventsList[1].name;
            Button3EventText3 = ReferenceValues.JsonCalendarMasterEventList[2].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button3EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[2].eventsList[0].person);
            Button3EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[2].eventsList[1].person);
            Button3EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[2].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[2].eventsList.Count > 3) {
                Button3EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[2].eventsList.Count - 2) + " More Events...";
                Button3EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 4 */
        try {
            Button4EventText1 = ReferenceValues.JsonCalendarMasterEventList[3].eventsList[0].name;
            Button4EventText2 = ReferenceValues.JsonCalendarMasterEventList[3].eventsList[1].name;
            Button4EventText3 = ReferenceValues.JsonCalendarMasterEventList[3].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button4EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[3].eventsList[0].person);
            Button4EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[3].eventsList[1].person);
            Button4EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[3].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[3].eventsList.Count > 3) {
                Button4EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[3].eventsList.Count - 2) + " More Events...";
                Button4EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 5 */
        try {
            Button5EventText1 = ReferenceValues.JsonCalendarMasterEventList[4].eventsList[0].name;
            Button5EventText2 = ReferenceValues.JsonCalendarMasterEventList[4].eventsList[1].name;
            Button5EventText3 = ReferenceValues.JsonCalendarMasterEventList[4].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button5EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[4].eventsList[0].person);
            Button5EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[4].eventsList[1].person);
            Button5EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[4].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[4].eventsList.Count > 3) {
                Button5EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[4].eventsList.Count - 2) + " More Events...";
                Button5EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 6 */
        try {
            Button6EventText1 = ReferenceValues.JsonCalendarMasterEventList[5].eventsList[0].name;
            Button6EventText2 = ReferenceValues.JsonCalendarMasterEventList[5].eventsList[1].name;
            Button6EventText3 = ReferenceValues.JsonCalendarMasterEventList[5].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button6EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[5].eventsList[0].person);
            Button6EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[5].eventsList[1].person);
            Button6EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[5].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[5].eventsList.Count > 3) {
                Button6EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[5].eventsList.Count - 2) + " More Events...";
                Button6EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 7 */
        try {
            Button7EventText1 = ReferenceValues.JsonCalendarMasterEventList[6].eventsList[0].name;
            Button7EventText2 = ReferenceValues.JsonCalendarMasterEventList[6].eventsList[1].name;
            Button7EventText3 = ReferenceValues.JsonCalendarMasterEventList[6].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button7EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[6].eventsList[0].person);
            Button7EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[6].eventsList[1].person);
            Button7EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[6].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[6].eventsList.Count > 3) {
                Button7EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[6].eventsList.Count - 2) + " More Events...";
                Button7EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 8 */
        try {
            Button8EventText1 = ReferenceValues.JsonCalendarMasterEventList[7].eventsList[0].name;
            Button8EventText2 = ReferenceValues.JsonCalendarMasterEventList[7].eventsList[1].name;
            Button8EventText3 = ReferenceValues.JsonCalendarMasterEventList[7].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button8EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[7].eventsList[0].person);
            Button8EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[7].eventsList[1].person);
            Button8EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[7].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[7].eventsList.Count > 3) {
                Button8EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[7].eventsList.Count - 2) + " More Events...";
                Button8EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 9 */
        try {
            Button9EventText1 = ReferenceValues.JsonCalendarMasterEventList[8].eventsList[0].name;
            Button9EventText2 = ReferenceValues.JsonCalendarMasterEventList[8].eventsList[1].name;
            Button9EventText3 = ReferenceValues.JsonCalendarMasterEventList[8].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button9EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[8].eventsList[0].person);
            Button9EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[8].eventsList[1].person);
            Button9EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[8].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[8].eventsList.Count > 3) {
                Button9EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[8].eventsList.Count - 2) + " More Events...";
                Button9EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 10 */
        try {
            Button10EventText1 = ReferenceValues.JsonCalendarMasterEventList[9].eventsList[0].name;
            Button10EventText2 = ReferenceValues.JsonCalendarMasterEventList[9].eventsList[1].name;
            Button10EventText3 = ReferenceValues.JsonCalendarMasterEventList[9].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button10EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[9].eventsList[0].person);
            Button10EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[9].eventsList[1].person);
            Button10EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[9].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[9].eventsList.Count > 3) {
                Button10EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[9].eventsList.Count - 2) + " More Events...";
                Button10EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 11 */
        try {
            Button11EventText1 = ReferenceValues.JsonCalendarMasterEventList[10].eventsList[0].name;
            Button11EventText2 = ReferenceValues.JsonCalendarMasterEventList[10].eventsList[1].name;
            Button11EventText3 = ReferenceValues.JsonCalendarMasterEventList[10].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button11EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[10].eventsList[0].person);
            Button11EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[10].eventsList[1].person);
            Button11EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[10].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[10].eventsList.Count > 3) {
                Button11EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[10].eventsList.Count - 2) + " More Events...";
                Button11EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 12 */
        try {
            Button12EventText1 = ReferenceValues.JsonCalendarMasterEventList[11].eventsList[0].name;
            Button12EventText2 = ReferenceValues.JsonCalendarMasterEventList[11].eventsList[1].name;
            Button12EventText3 = ReferenceValues.JsonCalendarMasterEventList[11].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button12EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[11].eventsList[0].person);
            Button12EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[11].eventsList[1].person);
            Button12EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[11].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[11].eventsList.Count > 3) {
                Button12EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[11].eventsList.Count - 2) + " More Events...";
                Button12EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 13 */
        try {
            Button13EventText1 = ReferenceValues.JsonCalendarMasterEventList[12].eventsList[0].name;
            Button13EventText2 = ReferenceValues.JsonCalendarMasterEventList[12].eventsList[1].name;
            Button13EventText3 = ReferenceValues.JsonCalendarMasterEventList[12].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button13EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[12].eventsList[0].person);
            Button13EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[12].eventsList[1].person);
            Button13EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[12].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[12].eventsList.Count > 3) {
                Button13EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[12].eventsList.Count - 2) + " More Events...";
                Button13EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 14 */
        try {
            Button14EventText1 = ReferenceValues.JsonCalendarMasterEventList[13].eventsList[0].name;
            Button14EventText2 = ReferenceValues.JsonCalendarMasterEventList[13].eventsList[1].name;
            Button14EventText3 = ReferenceValues.JsonCalendarMasterEventList[13].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button14EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[13].eventsList[0].person);
            Button14EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[13].eventsList[1].person);
            Button14EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[13].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[13].eventsList.Count > 3) {
                Button14EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[13].eventsList.Count - 2) + " More Events...";
                Button14EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 15 */
        try {
            Button15EventText1 = ReferenceValues.JsonCalendarMasterEventList[14].eventsList[0].name;
            Button15EventText2 = ReferenceValues.JsonCalendarMasterEventList[14].eventsList[1].name;
            Button15EventText3 = ReferenceValues.JsonCalendarMasterEventList[14].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button15EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[14].eventsList[0].person);
            Button15EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[14].eventsList[1].person);
            Button15EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[14].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[14].eventsList.Count > 3) {
                Button15EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[14].eventsList.Count - 2) + " More Events...";
                Button15EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 16 */
        try {
            Button16EventText1 = ReferenceValues.JsonCalendarMasterEventList[15].eventsList[0].name;
            Button16EventText2 = ReferenceValues.JsonCalendarMasterEventList[15].eventsList[1].name;
            Button16EventText3 = ReferenceValues.JsonCalendarMasterEventList[15].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button16EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[15].eventsList[0].person);
            Button16EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[15].eventsList[1].person);
            Button16EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[15].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[15].eventsList.Count > 3) {
                Button16EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[15].eventsList.Count - 2) + " More Events...";
                Button16EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 17 */
        try {
            Button17EventText1 = ReferenceValues.JsonCalendarMasterEventList[16].eventsList[0].name;
            Button17EventText2 = ReferenceValues.JsonCalendarMasterEventList[16].eventsList[1].name;
            Button17EventText3 = ReferenceValues.JsonCalendarMasterEventList[16].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button17EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[16].eventsList[0].person);
            Button17EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[16].eventsList[1].person);
            Button17EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[16].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[16].eventsList.Count > 3) {
                Button17EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[16].eventsList.Count - 2) + " More Events...";
                Button17EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 18 */
        try {
            Button18EventText1 = ReferenceValues.JsonCalendarMasterEventList[17].eventsList[0].name;
            Button18EventText2 = ReferenceValues.JsonCalendarMasterEventList[17].eventsList[1].name;
            Button18EventText3 = ReferenceValues.JsonCalendarMasterEventList[17].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button18EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[17].eventsList[0].person);
            Button18EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[17].eventsList[1].person);
            Button18EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[17].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[17].eventsList.Count > 3) {
                Button18EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[17].eventsList.Count - 2) + " More Events...";
                Button18EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 19 */
        try {
            Button19EventText1 = ReferenceValues.JsonCalendarMasterEventList[18].eventsList[0].name;
            Button19EventText2 = ReferenceValues.JsonCalendarMasterEventList[18].eventsList[1].name;
            Button19EventText3 = ReferenceValues.JsonCalendarMasterEventList[18].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button19EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[18].eventsList[0].person);
            Button19EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[18].eventsList[1].person);
            Button19EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[18].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[18].eventsList.Count > 3) {
                Button19EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[18].eventsList.Count - 2) + " More Events...";
                Button19EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 20 */
        try {
            Button20EventText1 = ReferenceValues.JsonCalendarMasterEventList[19].eventsList[0].name;
            Button20EventText2 = ReferenceValues.JsonCalendarMasterEventList[19].eventsList[1].name;
            Button20EventText3 = ReferenceValues.JsonCalendarMasterEventList[19].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button20EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[19].eventsList[0].person);
            Button20EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[19].eventsList[1].person);
            Button20EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[19].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[19].eventsList.Count > 3) {
                Button20EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[19].eventsList.Count - 2) + " More Events...";
                Button20EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 21 */
        try {
            Button21EventText1 = ReferenceValues.JsonCalendarMasterEventList[20].eventsList[0].name;
            Button21EventText2 = ReferenceValues.JsonCalendarMasterEventList[20].eventsList[1].name;
            Button21EventText3 = ReferenceValues.JsonCalendarMasterEventList[20].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button21EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[20].eventsList[0].person);
            Button21EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[20].eventsList[1].person);
            Button21EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[20].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[20].eventsList.Count > 3) {
                Button21EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[20].eventsList.Count - 2) + " More Events...";
                Button21EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 22 */
        try {
            Button22EventText1 = ReferenceValues.JsonCalendarMasterEventList[21].eventsList[0].name;
            Button22EventText2 = ReferenceValues.JsonCalendarMasterEventList[21].eventsList[1].name;
            Button22EventText3 = ReferenceValues.JsonCalendarMasterEventList[21].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button22EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[21].eventsList[0].person);
            Button22EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[21].eventsList[1].person);
            Button22EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[21].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[21].eventsList.Count > 3) {
                Button22EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[21].eventsList.Count - 2) + " More Events...";
                Button22EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 23 */
        try {
            Button23EventText1 = ReferenceValues.JsonCalendarMasterEventList[22].eventsList[0].name;
            Button23EventText2 = ReferenceValues.JsonCalendarMasterEventList[22].eventsList[1].name;
            Button23EventText3 = ReferenceValues.JsonCalendarMasterEventList[22].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button23EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[22].eventsList[0].person);
            Button23EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[22].eventsList[1].person);
            Button23EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[22].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[22].eventsList.Count > 3) {
                Button23EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[22].eventsList.Count - 2) + " More Events...";
                Button23EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 24 */
        try {
            Button24EventText1 = ReferenceValues.JsonCalendarMasterEventList[23].eventsList[0].name;
            Button24EventText2 = ReferenceValues.JsonCalendarMasterEventList[23].eventsList[1].name;
            Button24EventText3 = ReferenceValues.JsonCalendarMasterEventList[23].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button24EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[23].eventsList[0].person);
            Button24EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[23].eventsList[1].person);
            Button24EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[23].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[23].eventsList.Count > 3) {
                Button24EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[23].eventsList.Count - 2) + " More Events...";
                Button24EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 25 */
        try {
            Button25EventText1 = ReferenceValues.JsonCalendarMasterEventList[24].eventsList[0].name;
            Button25EventText2 = ReferenceValues.JsonCalendarMasterEventList[24].eventsList[1].name;
            Button25EventText3 = ReferenceValues.JsonCalendarMasterEventList[24].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button25EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[24].eventsList[0].person);
            Button25EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[24].eventsList[1].person);
            Button25EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[24].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[24].eventsList.Count > 3) {
                Button25EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[24].eventsList.Count - 2) + " More Events...";
                Button25EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 26 */
        try {
            Button26EventText1 = ReferenceValues.JsonCalendarMasterEventList[25].eventsList[0].name;
            Button26EventText2 = ReferenceValues.JsonCalendarMasterEventList[25].eventsList[1].name;
            Button26EventText3 = ReferenceValues.JsonCalendarMasterEventList[25].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button26EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[25].eventsList[0].person);
            Button26EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[25].eventsList[1].person);
            Button26EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[25].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[25].eventsList.Count > 3) {
                Button26EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[25].eventsList.Count - 2) + " More Events...";
                Button26EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 27 */
        try {
            Button27EventText1 = ReferenceValues.JsonCalendarMasterEventList[26].eventsList[0].name;
            Button27EventText2 = ReferenceValues.JsonCalendarMasterEventList[26].eventsList[1].name;
            Button27EventText3 = ReferenceValues.JsonCalendarMasterEventList[26].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button27EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[26].eventsList[0].person);
            Button27EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[26].eventsList[1].person);
            Button27EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[26].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[26].eventsList.Count > 3) {
                Button27EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[26].eventsList.Count - 2) + " More Events...";
                Button27EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 28 */
        try {
            Button28EventText1 = ReferenceValues.JsonCalendarMasterEventList[27].eventsList[0].name;
            Button28EventText2 = ReferenceValues.JsonCalendarMasterEventList[27].eventsList[1].name;
            Button28EventText3 = ReferenceValues.JsonCalendarMasterEventList[27].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button28EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[27].eventsList[0].person);
            Button28EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[27].eventsList[1].person);
            Button28EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[27].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[27].eventsList.Count > 3) {
                Button28EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[27].eventsList.Count - 2) + " More Events...";
                Button28EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 29 */
        try {
            Button29EventText1 = ReferenceValues.JsonCalendarMasterEventList[28].eventsList[0].name;
            Button29EventText2 = ReferenceValues.JsonCalendarMasterEventList[28].eventsList[1].name;
            Button29EventText3 = ReferenceValues.JsonCalendarMasterEventList[28].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button29EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[28].eventsList[0].person);
            Button29EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[28].eventsList[1].person);
            Button29EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[28].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[28].eventsList.Count > 3) {
                Button29EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[28].eventsList.Count - 2) + " More Events...";
                Button29EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 30 */
        try {
            Button30EventText1 = ReferenceValues.JsonCalendarMasterEventList[29].eventsList[0].name;
            Button30EventText2 = ReferenceValues.JsonCalendarMasterEventList[29].eventsList[1].name;
            Button30EventText3 = ReferenceValues.JsonCalendarMasterEventList[29].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button30EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[29].eventsList[0].person);
            Button30EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[29].eventsList[1].person);
            Button30EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[29].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[29].eventsList.Count > 3) {
                Button30EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[29].eventsList.Count - 2) + " More Events...";
                Button30EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 31 */
        try {
            Button31EventText1 = ReferenceValues.JsonCalendarMasterEventList[30].eventsList[0].name;
            Button31EventText2 = ReferenceValues.JsonCalendarMasterEventList[30].eventsList[1].name;
            Button31EventText3 = ReferenceValues.JsonCalendarMasterEventList[30].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button31EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[30].eventsList[0].person);
            Button31EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[30].eventsList[1].person);
            Button31EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[30].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[30].eventsList.Count > 3) {
                Button31EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[30].eventsList.Count - 2) + " More Events...";
                Button31EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 32 */
        try {
            Button32EventText1 = ReferenceValues.JsonCalendarMasterEventList[31].eventsList[0].name;
            Button32EventText2 = ReferenceValues.JsonCalendarMasterEventList[31].eventsList[1].name;
            Button32EventText3 = ReferenceValues.JsonCalendarMasterEventList[31].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button32EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[31].eventsList[0].person);
            Button32EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[31].eventsList[1].person);
            Button32EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[31].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[31].eventsList.Count > 3) {
                Button32EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[31].eventsList.Count - 2) + " More Events...";
                Button32EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 33 */
        try {
            Button33EventText1 = ReferenceValues.JsonCalendarMasterEventList[32].eventsList[0].name;
            Button33EventText2 = ReferenceValues.JsonCalendarMasterEventList[32].eventsList[1].name;
            Button33EventText3 = ReferenceValues.JsonCalendarMasterEventList[32].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button33EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[32].eventsList[0].person);
            Button33EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[32].eventsList[1].person);
            Button33EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[32].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[32].eventsList.Count > 3) {
                Button33EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[32].eventsList.Count - 2) + " More Events...";
                Button33EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 34 */
        try {
            Button34EventText1 = ReferenceValues.JsonCalendarMasterEventList[33].eventsList[0].name;
            Button34EventText2 = ReferenceValues.JsonCalendarMasterEventList[33].eventsList[1].name;
            Button34EventText3 = ReferenceValues.JsonCalendarMasterEventList[33].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button34EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[33].eventsList[0].person);
            Button34EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[33].eventsList[1].person);
            Button34EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[33].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[33].eventsList.Count > 3) {
                Button34EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[33].eventsList.Count - 2) + " More Events...";
                Button34EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 35 */
        try {
            Button35EventText1 = ReferenceValues.JsonCalendarMasterEventList[34].eventsList[0].name;
            Button35EventText2 = ReferenceValues.JsonCalendarMasterEventList[34].eventsList[1].name;
            Button35EventText3 = ReferenceValues.JsonCalendarMasterEventList[34].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button35EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[34].eventsList[0].person);
            Button35EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[34].eventsList[1].person);
            Button35EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[34].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[34].eventsList.Count > 3) {
                Button35EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[34].eventsList.Count - 2) + " More Events...";
                Button35EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 36 */
        try {
            Button36EventText1 = ReferenceValues.JsonCalendarMasterEventList[35].eventsList[0].name;
            Button36EventText2 = ReferenceValues.JsonCalendarMasterEventList[35].eventsList[1].name;
            Button36EventText3 = ReferenceValues.JsonCalendarMasterEventList[35].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button36EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[35].eventsList[0].person);
            Button36EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[35].eventsList[1].person);
            Button36EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[35].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[35].eventsList.Count > 3) {
                Button36EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[35].eventsList.Count - 2) + " More Events...";
                Button36EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 37 */
        try {
            Button37EventText1 = ReferenceValues.JsonCalendarMasterEventList[36].eventsList[0].name;
            Button37EventText2 = ReferenceValues.JsonCalendarMasterEventList[36].eventsList[1].name;
            Button37EventText3 = ReferenceValues.JsonCalendarMasterEventList[36].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button37EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[36].eventsList[0].person);
            Button37EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[36].eventsList[1].person);
            Button37EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[36].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[36].eventsList.Count > 3) {
                Button37EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[36].eventsList.Count - 2) + " More Events...";
                Button37EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 38 */
        try {
            Button38EventText1 = ReferenceValues.JsonCalendarMasterEventList[37].eventsList[0].name;
            Button38EventText2 = ReferenceValues.JsonCalendarMasterEventList[37].eventsList[1].name;
            Button38EventText3 = ReferenceValues.JsonCalendarMasterEventList[37].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button38EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[37].eventsList[0].person);
            Button38EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[37].eventsList[1].person);
            Button38EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[37].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[37].eventsList.Count > 3) {
                Button38EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[37].eventsList.Count - 2) + " More Events...";
                Button38EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 39 */
        try {
            Button39EventText1 = ReferenceValues.JsonCalendarMasterEventList[38].eventsList[0].name;
            Button39EventText2 = ReferenceValues.JsonCalendarMasterEventList[38].eventsList[1].name;
            Button39EventText3 = ReferenceValues.JsonCalendarMasterEventList[38].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button39EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[38].eventsList[0].person);
            Button39EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[38].eventsList[1].person);
            Button39EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[38].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[38].eventsList.Count > 3) {
                Button39EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[38].eventsList.Count - 2) + " More Events...";
                Button39EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 40 */
        try {
            Button40EventText1 = ReferenceValues.JsonCalendarMasterEventList[39].eventsList[0].name;
            Button40EventText2 = ReferenceValues.JsonCalendarMasterEventList[39].eventsList[1].name;
            Button40EventText3 = ReferenceValues.JsonCalendarMasterEventList[39].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button40EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[39].eventsList[0].person);
            Button40EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[39].eventsList[1].person);
            Button40EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[39].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[39].eventsList.Count > 3) {
                Button40EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[39].eventsList.Count - 2) + " More Events...";
                Button40EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 41 */
        try {
            Button41EventText1 = ReferenceValues.JsonCalendarMasterEventList[40].eventsList[0].name;
            Button41EventText2 = ReferenceValues.JsonCalendarMasterEventList[40].eventsList[1].name;
            Button41EventText3 = ReferenceValues.JsonCalendarMasterEventList[40].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button41EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[40].eventsList[0].person);
            Button41EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[40].eventsList[1].person);
            Button41EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[40].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[40].eventsList.Count > 3) {
                Button41EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[40].eventsList.Count - 2) + " More Events...";
                Button41EventColor3 = "White";
            }
        } catch (Exception) { }

        /* Button 42 */
        try {
            Button42EventText1 = ReferenceValues.JsonCalendarMasterEventList[41].eventsList[0].name;
            Button42EventText2 = ReferenceValues.JsonCalendarMasterEventList[41].eventsList[1].name;
            Button42EventText3 = ReferenceValues.JsonCalendarMasterEventList[41].eventsList[2].name;
        } catch (Exception) { }

        try {
            Button42EventColor1 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[41].eventsList[0].person);
            Button42EventColor2 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[41].eventsList[1].person);
            Button42EventColor3 = GetColorByName(ReferenceValues.JsonCalendarMasterEventList[41].eventsList[2].person);
        } catch (Exception) { }

        try {
            if (ReferenceValues.JsonCalendarMasterEventList[41].eventsList.Count > 3) {
                Button42EventText3 = "+" + (ReferenceValues.JsonCalendarMasterEventList[41].eventsList.Count - 2) + " More Events...";
                Button42EventColor3 = "White";
            }
        } catch (Exception) { }
    }

    //TODO: hardcoded the names for now, add these as config options in the future.
    private string GetColorByName(string name) {
        switch (name) {
        case "Robert":
            return "CornflowerBlue";
        case "Brittany":
            return "LightGreen";
        case "Children":
            return "LightGray";
        default:
            return "White";
        }
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

    public string Button1EventText1 {
        get => _button1EventText1;
        set {
            _button1EventText1 = value;
            RaisePropertyChangedEvent("Button1EventText1");
        }
    }

    public string Button1EventText2 {
        get => _button1EventText2;
        set {
            _button1EventText2 = value;
            RaisePropertyChangedEvent("Button1EventText2");
        }
    }

    public string Button1EventText3 {
        get => _button1EventText3;
        set {
            _button1EventText3 = value;
            RaisePropertyChangedEvent("Button1EventText3");
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

    public string Button2EventText1 {
        get => _button2EventText1;
        set {
            _button2EventText1 = value;
            RaisePropertyChangedEvent("Button2EventText1");
        }
    }

    public string Button2EventText2 {
        get => _button2EventText2;
        set {
            _button2EventText2 = value;
            RaisePropertyChangedEvent("Button2EventText2");
        }
    }

    public string Button2EventText3 {
        get => _button2EventText3;
        set {
            _button2EventText3 = value;
            RaisePropertyChangedEvent("Button2EventText3");
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

    public string Button3EventText1 {
        get => _button3EventText1;
        set {
            _button3EventText1 = value;
            RaisePropertyChangedEvent("Button3EventText1");
        }
    }

    public string Button3EventText2 {
        get => _button3EventText2;
        set {
            _button3EventText2 = value;
            RaisePropertyChangedEvent("Button3EventText2");
        }
    }

    public string Button3EventText3 {
        get => _button3EventText3;
        set {
            _button3EventText3 = value;
            RaisePropertyChangedEvent("Button3EventText3");
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

    public string Button4EventText1 {
        get => _button4EventText1;
        set {
            _button4EventText1 = value;
            RaisePropertyChangedEvent("Button4EventText1");
        }
    }

    public string Button4EventText2 {
        get => _button4EventText2;
        set {
            _button4EventText2 = value;
            RaisePropertyChangedEvent("Button4EventText2");
        }
    }

    public string Button4EventText3 {
        get => _button4EventText3;
        set {
            _button4EventText3 = value;
            RaisePropertyChangedEvent("Button4EventText3");
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

    public string Button5EventText1 {
        get => _button5EventText1;
        set {
            _button5EventText1 = value;
            RaisePropertyChangedEvent("Button5EventText1");
        }
    }

    public string Button5EventText2 {
        get => _button5EventText2;
        set {
            _button5EventText2 = value;
            RaisePropertyChangedEvent("Button5EventText2");
        }
    }

    public string Button5EventText3 {
        get => _button5EventText3;
        set {
            _button5EventText3 = value;
            RaisePropertyChangedEvent("Button5EventText3");
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

    public string Button6EventText1 {
        get => _button6EventText1;
        set {
            _button6EventText1 = value;
            RaisePropertyChangedEvent("Button6EventText1");
        }
    }

    public string Button6EventText2 {
        get => _button6EventText2;
        set {
            _button6EventText2 = value;
            RaisePropertyChangedEvent("Button6EventText2");
        }
    }

    public string Button6EventText3 {
        get => _button6EventText3;
        set {
            _button6EventText3 = value;
            RaisePropertyChangedEvent("Button6EventText3");
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

    public string Button7EventText1 {
        get => _button7EventText1;
        set {
            _button7EventText1 = value;
            RaisePropertyChangedEvent("Button7EventText1");
        }
    }

    public string Button7EventText2 {
        get => _button7EventText2;
        set {
            _button7EventText2 = value;
            RaisePropertyChangedEvent("Button7EventText2");
        }
    }

    public string Button7EventText3 {
        get => _button7EventText3;
        set {
            _button7EventText3 = value;
            RaisePropertyChangedEvent("Button7EventText3");
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

    public string Button8EventText1 {
        get => _button8EventText1;
        set {
            _button8EventText1 = value;
            RaisePropertyChangedEvent("Button8EventText1");
        }
    }

    public string Button8EventText2 {
        get => _button8EventText2;
        set {
            _button8EventText2 = value;
            RaisePropertyChangedEvent("Button8EventText2");
        }
    }

    public string Button8EventText3 {
        get => _button8EventText3;
        set {
            _button8EventText3 = value;
            RaisePropertyChangedEvent("Button8EventText3");
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

    public string Button9EventText1 {
        get => _button9EventText1;
        set {
            _button9EventText1 = value;
            RaisePropertyChangedEvent("Button9EventText1");
        }
    }

    public string Button9EventText2 {
        get => _button9EventText2;
        set {
            _button9EventText2 = value;
            RaisePropertyChangedEvent("Button9EventText2");
        }
    }

    public string Button9EventText3 {
        get => _button9EventText3;
        set {
            _button9EventText3 = value;
            RaisePropertyChangedEvent("Button9EventText3");
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

    public string Button10EventText1 {
        get => _button10EventText1;
        set {
            _button10EventText1 = value;
            RaisePropertyChangedEvent("Button10EventText1");
        }
    }

    public string Button10EventText2 {
        get => _button10EventText2;
        set {
            _button10EventText2 = value;
            RaisePropertyChangedEvent("Button10EventText2");
        }
    }

    public string Button10EventText3 {
        get => _button10EventText3;
        set {
            _button10EventText3 = value;
            RaisePropertyChangedEvent("Button10EventText3");
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

    public string Button11EventText1 {
        get => _button11EventText1;
        set {
            _button11EventText1 = value;
            RaisePropertyChangedEvent("Button11EventText1");
        }
    }

    public string Button11EventText2 {
        get => _button11EventText2;
        set {
            _button11EventText2 = value;
            RaisePropertyChangedEvent("Button11EventText2");
        }
    }

    public string Button11EventText3 {
        get => _button11EventText3;
        set {
            _button11EventText3 = value;
            RaisePropertyChangedEvent("Button11EventText3");
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

    public string Button12EventText1 {
        get => _button12EventText1;
        set {
            _button12EventText1 = value;
            RaisePropertyChangedEvent("Button12EventText1");
        }
    }

    public string Button12EventText2 {
        get => _button12EventText2;
        set {
            _button12EventText2 = value;
            RaisePropertyChangedEvent("Button12EventText2");
        }
    }

    public string Button12EventText3 {
        get => _button12EventText3;
        set {
            _button12EventText3 = value;
            RaisePropertyChangedEvent("Button12EventText3");
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

    public string Button13EventText1 {
        get => _button13EventText1;
        set {
            _button13EventText1 = value;
            RaisePropertyChangedEvent("Button13EventText1");
        }
    }

    public string Button13EventText2 {
        get => _button13EventText2;
        set {
            _button13EventText2 = value;
            RaisePropertyChangedEvent("Button13EventText2");
        }
    }

    public string Button13EventText3 {
        get => _button13EventText3;
        set {
            _button13EventText3 = value;
            RaisePropertyChangedEvent("Button13EventText3");
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

    public string Button14EventText1 {
        get => _button14EventText1;
        set {
            _button14EventText1 = value;
            RaisePropertyChangedEvent("Button14EventText1");
        }
    }

    public string Button14EventText2 {
        get => _button14EventText2;
        set {
            _button14EventText2 = value;
            RaisePropertyChangedEvent("Button14EventText2");
        }
    }

    public string Button14EventText3 {
        get => _button14EventText3;
        set {
            _button14EventText3 = value;
            RaisePropertyChangedEvent("Button14EventText3");
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

    public string Button15EventText1 {
        get => _button15EventText1;
        set {
            _button15EventText1 = value;
            RaisePropertyChangedEvent("Button15EventText1");
        }
    }

    public string Button15EventText2 {
        get => _button15EventText2;
        set {
            _button15EventText2 = value;
            RaisePropertyChangedEvent("Button15EventText2");
        }
    }

    public string Button15EventText3 {
        get => _button15EventText3;
        set {
            _button15EventText3 = value;
            RaisePropertyChangedEvent("Button15EventText3");
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

    public string Button16EventText1 {
        get => _button16EventText1;
        set {
            _button16EventText1 = value;
            RaisePropertyChangedEvent("Button16EventText1");
        }
    }

    public string Button16EventText2 {
        get => _button16EventText2;
        set {
            _button16EventText2 = value;
            RaisePropertyChangedEvent("Button16EventText2");
        }
    }

    public string Button16EventText3 {
        get => _button16EventText3;
        set {
            _button16EventText3 = value;
            RaisePropertyChangedEvent("Button16EventText3");
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

    public string Button17EventText1 {
        get => _button17EventText1;
        set {
            _button17EventText1 = value;
            RaisePropertyChangedEvent("Button17EventText1");
        }
    }

    public string Button17EventText2 {
        get => _button17EventText2;
        set {
            _button17EventText2 = value;
            RaisePropertyChangedEvent("Button17EventText2");
        }
    }

    public string Button17EventText3 {
        get => _button17EventText3;
        set {
            _button17EventText3 = value;
            RaisePropertyChangedEvent("Button17EventText3");
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

    public string Button18EventText1 {
        get => _button18EventText1;
        set {
            _button18EventText1 = value;
            RaisePropertyChangedEvent("Button18EventText1");
        }
    }

    public string Button18EventText2 {
        get => _button18EventText2;
        set {
            _button18EventText2 = value;
            RaisePropertyChangedEvent("Button18EventText2");
        }
    }

    public string Button18EventText3 {
        get => _button18EventText3;
        set {
            _button18EventText3 = value;
            RaisePropertyChangedEvent("Button18EventText3");
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

    public string Button19EventText1 {
        get => _button19EventText1;
        set {
            _button19EventText1 = value;
            RaisePropertyChangedEvent("Button19EventText1");
        }
    }

    public string Button19EventText2 {
        get => _button19EventText2;
        set {
            _button19EventText2 = value;
            RaisePropertyChangedEvent("Button19EventText2");
        }
    }

    public string Button19EventText3 {
        get => _button19EventText3;
        set {
            _button19EventText3 = value;
            RaisePropertyChangedEvent("Button19EventText3");
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

    public string Button20EventText1 {
        get => _button20EventText1;
        set {
            _button20EventText1 = value;
            RaisePropertyChangedEvent("Button20EventText1");
        }
    }

    public string Button20EventText2 {
        get => _button20EventText2;
        set {
            _button20EventText2 = value;
            RaisePropertyChangedEvent("Button20EventText2");
        }
    }

    public string Button20EventText3 {
        get => _button20EventText3;
        set {
            _button20EventText3 = value;
            RaisePropertyChangedEvent("Button20EventText3");
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

    public string Button21EventText1 {
        get => _button21EventText1;
        set {
            _button21EventText1 = value;
            RaisePropertyChangedEvent("Button21EventText1");
        }
    }

    public string Button21EventText2 {
        get => _button21EventText2;
        set {
            _button21EventText2 = value;
            RaisePropertyChangedEvent("Button21EventText2");
        }
    }

    public string Button21EventText3 {
        get => _button21EventText3;
        set {
            _button21EventText3 = value;
            RaisePropertyChangedEvent("Button21EventText3");
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

    public string Button22EventText1 {
        get => _button22EventText1;
        set {
            _button22EventText1 = value;
            RaisePropertyChangedEvent("Button22EventText1");
        }
    }

    public string Button22EventText2 {
        get => _button22EventText2;
        set {
            _button22EventText2 = value;
            RaisePropertyChangedEvent("Button22EventText2");
        }
    }

    public string Button22EventText3 {
        get => _button22EventText3;
        set {
            _button22EventText3 = value;
            RaisePropertyChangedEvent("Button22EventText3");
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

    public string Button23EventText1 {
        get => _button23EventText1;
        set {
            _button23EventText1 = value;
            RaisePropertyChangedEvent("Button23EventText1");
        }
    }

    public string Button23EventText2 {
        get => _button23EventText2;
        set {
            _button23EventText2 = value;
            RaisePropertyChangedEvent("Button23EventText2");
        }
    }

    public string Button23EventText3 {
        get => _button23EventText3;
        set {
            _button23EventText3 = value;
            RaisePropertyChangedEvent("Button23EventText3");
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

    public string Button24EventText1 {
        get => _button24EventText1;
        set {
            _button24EventText1 = value;
            RaisePropertyChangedEvent("Button24EventText1");
        }
    }

    public string Button24EventText2 {
        get => _button24EventText2;
        set {
            _button24EventText2 = value;
            RaisePropertyChangedEvent("Button24EventText2");
        }
    }

    public string Button24EventText3 {
        get => _button24EventText3;
        set {
            _button24EventText3 = value;
            RaisePropertyChangedEvent("Button24EventText3");
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

    public string Button25EventText1 {
        get => _button25EventText1;
        set {
            _button25EventText1 = value;
            RaisePropertyChangedEvent("Button25EventText1");
        }
    }

    public string Button25EventText2 {
        get => _button25EventText2;
        set {
            _button25EventText2 = value;
            RaisePropertyChangedEvent("Button25EventText2");
        }
    }

    public string Button25EventText3 {
        get => _button25EventText3;
        set {
            _button25EventText3 = value;
            RaisePropertyChangedEvent("Button25EventText3");
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

    public string Button26EventText1 {
        get => _button26EventText1;
        set {
            _button26EventText1 = value;
            RaisePropertyChangedEvent("Button26EventText1");
        }
    }

    public string Button26EventText2 {
        get => _button26EventText2;
        set {
            _button26EventText2 = value;
            RaisePropertyChangedEvent("Button26EventText2");
        }
    }

    public string Button26EventText3 {
        get => _button26EventText3;
        set {
            _button26EventText3 = value;
            RaisePropertyChangedEvent("Button26EventText3");
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

    public string Button27EventText1 {
        get => _button27EventText1;
        set {
            _button27EventText1 = value;
            RaisePropertyChangedEvent("Button27EventText1");
        }
    }

    public string Button27EventText2 {
        get => _button27EventText2;
        set {
            _button27EventText2 = value;
            RaisePropertyChangedEvent("Button27EventText2");
        }
    }

    public string Button27EventText3 {
        get => _button27EventText3;
        set {
            _button27EventText3 = value;
            RaisePropertyChangedEvent("Button27EventText3");
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

    public string Button28EventText1 {
        get => _button28EventText1;
        set {
            _button28EventText1 = value;
            RaisePropertyChangedEvent("Button28EventText1");
        }
    }

    public string Button28EventText2 {
        get => _button28EventText2;
        set {
            _button28EventText2 = value;
            RaisePropertyChangedEvent("Button28EventText2");
        }
    }

    public string Button28EventText3 {
        get => _button28EventText3;
        set {
            _button28EventText3 = value;
            RaisePropertyChangedEvent("Button28EventText3");
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

    public string Button29EventText1 {
        get => _button29EventText1;
        set {
            _button29EventText1 = value;
            RaisePropertyChangedEvent("Button29EventText1");
        }
    }

    public string Button29EventText2 {
        get => _button29EventText2;
        set {
            _button29EventText2 = value;
            RaisePropertyChangedEvent("Button29EventText2");
        }
    }

    public string Button29EventText3 {
        get => _button29EventText3;
        set {
            _button29EventText3 = value;
            RaisePropertyChangedEvent("Button29EventText3");
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

    public string Button30EventText1 {
        get => _button30EventText1;
        set {
            _button30EventText1 = value;
            RaisePropertyChangedEvent("Button30EventText1");
        }
    }

    public string Button30EventText2 {
        get => _button30EventText2;
        set {
            _button30EventText2 = value;
            RaisePropertyChangedEvent("Button30EventText2");
        }
    }

    public string Button30EventText3 {
        get => _button30EventText3;
        set {
            _button30EventText3 = value;
            RaisePropertyChangedEvent("Button30EventText3");
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

    public string Button31EventText1 {
        get => _button31EventText1;
        set {
            _button31EventText1 = value;
            RaisePropertyChangedEvent("Button31EventText1");
        }
    }

    public string Button31EventText2 {
        get => _button31EventText2;
        set {
            _button31EventText2 = value;
            RaisePropertyChangedEvent("Button31EventText2");
        }
    }

    public string Button31EventText3 {
        get => _button31EventText3;
        set {
            _button31EventText3 = value;
            RaisePropertyChangedEvent("Button31EventText3");
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

    public string Button32EventText1 {
        get => _button32EventText1;
        set {
            _button32EventText1 = value;
            RaisePropertyChangedEvent("Button32EventText1");
        }
    }

    public string Button32EventText2 {
        get => _button32EventText2;
        set {
            _button32EventText2 = value;
            RaisePropertyChangedEvent("Button32EventText2");
        }
    }

    public string Button32EventText3 {
        get => _button32EventText3;
        set {
            _button32EventText3 = value;
            RaisePropertyChangedEvent("Button32EventText3");
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

    public string Button33EventText1 {
        get => _button33EventText1;
        set {
            _button33EventText1 = value;
            RaisePropertyChangedEvent("Button33EventText1");
        }
    }

    public string Button33EventText2 {
        get => _button33EventText2;
        set {
            _button33EventText2 = value;
            RaisePropertyChangedEvent("Button33EventText2");
        }
    }

    public string Button33EventText3 {
        get => _button33EventText3;
        set {
            _button33EventText3 = value;
            RaisePropertyChangedEvent("Button33EventText3");
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

    public string Button34EventText1 {
        get => _button34EventText1;
        set {
            _button34EventText1 = value;
            RaisePropertyChangedEvent("Button34EventText1");
        }
    }

    public string Button34EventText2 {
        get => _button34EventText2;
        set {
            _button34EventText2 = value;
            RaisePropertyChangedEvent("Button34EventText2");
        }
    }

    public string Button34EventText3 {
        get => _button34EventText3;
        set {
            _button34EventText3 = value;
            RaisePropertyChangedEvent("Button34EventText3");
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

    public string Button35EventText1 {
        get => _button35EventText1;
        set {
            _button35EventText1 = value;
            RaisePropertyChangedEvent("Button35EventText1");
        }
    }

    public string Button35EventText2 {
        get => _button35EventText2;
        set {
            _button35EventText2 = value;
            RaisePropertyChangedEvent("Button35EventText2");
        }
    }

    public string Button35EventText3 {
        get => _button35EventText3;
        set {
            _button35EventText3 = value;
            RaisePropertyChangedEvent("Button35EventText3");
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

    public string Button36EventText1 {
        get => _button36EventText1;
        set {
            _button36EventText1 = value;
            RaisePropertyChangedEvent("Button36EventText1");
        }
    }

    public string Button36EventText2 {
        get => _button36EventText2;
        set {
            _button36EventText2 = value;
            RaisePropertyChangedEvent("Button36EventText2");
        }
    }

    public string Button36EventText3 {
        get => _button36EventText3;
        set {
            _button36EventText3 = value;
            RaisePropertyChangedEvent("Button36EventText3");
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

    public string Button37EventText1 {
        get => _button37EventText1;
        set {
            _button37EventText1 = value;
            RaisePropertyChangedEvent("Button37EventText1");
        }
    }

    public string Button37EventText2 {
        get => _button37EventText2;
        set {
            _button37EventText2 = value;
            RaisePropertyChangedEvent("Button37EventText2");
        }
    }

    public string Button37EventText3 {
        get => _button37EventText3;
        set {
            _button37EventText3 = value;
            RaisePropertyChangedEvent("Button37EventText3");
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

    public string Button38EventText1 {
        get => _button38EventText1;
        set {
            _button38EventText1 = value;
            RaisePropertyChangedEvent("Button38EventText1");
        }
    }

    public string Button38EventText2 {
        get => _button38EventText2;
        set {
            _button38EventText2 = value;
            RaisePropertyChangedEvent("Button38EventText2");
        }
    }

    public string Button38EventText3 {
        get => _button38EventText3;
        set {
            _button38EventText3 = value;
            RaisePropertyChangedEvent("Button38EventText3");
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

    public string Button39EventText1 {
        get => _button39EventText1;
        set {
            _button39EventText1 = value;
            RaisePropertyChangedEvent("Button39EventText1");
        }
    }

    public string Button39EventText2 {
        get => _button39EventText2;
        set {
            _button39EventText2 = value;
            RaisePropertyChangedEvent("Button39EventText2");
        }
    }

    public string Button39EventText3 {
        get => _button39EventText3;
        set {
            _button39EventText3 = value;
            RaisePropertyChangedEvent("Button39EventText3");
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

    public string Button40EventText1 {
        get => _button40EventText1;
        set {
            _button40EventText1 = value;
            RaisePropertyChangedEvent("Button40EventText1");
        }
    }

    public string Button40EventText2 {
        get => _button40EventText2;
        set {
            _button40EventText2 = value;
            RaisePropertyChangedEvent("Button40EventText2");
        }
    }

    public string Button40EventText3 {
        get => _button40EventText3;
        set {
            _button40EventText3 = value;
            RaisePropertyChangedEvent("Button40EventText3");
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

    public string Button41EventText1 {
        get => _button41EventText1;
        set {
            _button41EventText1 = value;
            RaisePropertyChangedEvent("Button41EventText1");
        }
    }

    public string Button41EventText2 {
        get => _button41EventText2;
        set {
            _button41EventText2 = value;
            RaisePropertyChangedEvent("Button41EventText2");
        }
    }

    public string Button41EventText3 {
        get => _button41EventText3;
        set {
            _button41EventText3 = value;
            RaisePropertyChangedEvent("Button41EventText3");
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

    public string Button42EventText1 {
        get => _button42EventText1;
        set {
            _button42EventText1 = value;
            RaisePropertyChangedEvent("Button42EventText1");
        }
    }

    public string Button42EventText2 {
        get => _button42EventText2;
        set {
            _button42EventText2 = value;
            RaisePropertyChangedEvent("Button42EventText2");
        }
    }

    public string Button42EventText3 {
        get => _button42EventText3;
        set {
            _button42EventText3 = value;
            RaisePropertyChangedEvent("Button42EventText3");
        }
    }

    public string Button1EventColor1 {
        get => _button1EventColor1;
        set {
            _button1EventColor1 = value;
            RaisePropertyChangedEvent("Button1EventColor1");
        }
    }

    public string Button1EventColor2 {
        get => _button1EventColor2;
        set {
            _button1EventColor2 = value;
            RaisePropertyChangedEvent("Button1EventColor2");
        }
    }

    public string Button1EventColor3 {
        get => _button1EventColor3;
        set {
            _button1EventColor3 = value;
            RaisePropertyChangedEvent("Button1EventColor3");
        }
    }

    public string Button2EventColor1 {
        get => _button2EventColor1;
        set {
            _button2EventColor1 = value;
            RaisePropertyChangedEvent("Button2EventColor1");
        }
    }

    public string Button2EventColor2 {
        get => _button2EventColor2;
        set {
            _button2EventColor2 = value;
            RaisePropertyChangedEvent("Button2EventColor2");
        }
    }

    public string Button2EventColor3 {
        get => _button2EventColor3;
        set {
            _button2EventColor3 = value;
            RaisePropertyChangedEvent("Button2EventColor3");
        }
    }

    public string Button3EventColor1 {
        get => _button3EventColor1;
        set {
            _button3EventColor1 = value;
            RaisePropertyChangedEvent("Button3EventColor1");
        }
    }

    public string Button3EventColor2 {
        get => _button3EventColor2;
        set {
            _button3EventColor2 = value;
            RaisePropertyChangedEvent("Button3EventColor2");
        }
    }

    public string Button3EventColor3 {
        get => _button3EventColor3;
        set {
            _button3EventColor3 = value;
            RaisePropertyChangedEvent("Button3EventColor3");
        }
    }

    public string Button4EventColor1 {
        get => _button4EventColor1;
        set {
            _button4EventColor1 = value;
            RaisePropertyChangedEvent("Button4EventColor1");
        }
    }

    public string Button4EventColor2 {
        get => _button4EventColor2;
        set {
            _button4EventColor2 = value;
            RaisePropertyChangedEvent("Button4EventColor2");
        }
    }

    public string Button4EventColor3 {
        get => _button4EventColor3;
        set {
            _button4EventColor3 = value;
            RaisePropertyChangedEvent("Button4EventColor3");
        }
    }

    public string Button5EventColor1 {
        get => _button5EventColor1;
        set {
            _button5EventColor1 = value;
            RaisePropertyChangedEvent("Button5EventColor1");
        }
    }

    public string Button5EventColor2 {
        get => _button5EventColor2;
        set {
            _button5EventColor2 = value;
            RaisePropertyChangedEvent("Button5EventColor2");
        }
    }

    public string Button5EventColor3 {
        get => _button5EventColor3;
        set {
            _button5EventColor3 = value;
            RaisePropertyChangedEvent("Button5EventColor3");
        }
    }

    public string Button6EventColor1 {
        get => _button6EventColor1;
        set {
            _button6EventColor1 = value;
            RaisePropertyChangedEvent("Button6EventColor1");
        }
    }

    public string Button6EventColor2 {
        get => _button6EventColor2;
        set {
            _button6EventColor2 = value;
            RaisePropertyChangedEvent("Button6EventColor2");
        }
    }

    public string Button6EventColor3 {
        get => _button6EventColor3;
        set {
            _button6EventColor3 = value;
            RaisePropertyChangedEvent("Button6EventColor3");
        }
    }

    public string Button7EventColor1 {
        get => _button7EventColor1;
        set {
            _button7EventColor1 = value;
            RaisePropertyChangedEvent("Button7EventColor1");
        }
    }

    public string Button7EventColor2 {
        get => _button7EventColor2;
        set {
            _button7EventColor2 = value;
            RaisePropertyChangedEvent("Button7EventColor2");
        }
    }

    public string Button7EventColor3 {
        get => _button7EventColor3;
        set {
            _button7EventColor3 = value;
            RaisePropertyChangedEvent("Button7EventColor3");
        }
    }

    public string Button8EventColor1 {
        get => _button8EventColor1;
        set {
            _button8EventColor1 = value;
            RaisePropertyChangedEvent("Button8EventColor1");
        }
    }

    public string Button8EventColor2 {
        get => _button8EventColor2;
        set {
            _button8EventColor2 = value;
            RaisePropertyChangedEvent("Button8EventColor2");
        }
    }

    public string Button8EventColor3 {
        get => _button8EventColor3;
        set {
            _button8EventColor3 = value;
            RaisePropertyChangedEvent("Button8EventColor3");
        }
    }

    public string Button9EventColor1 {
        get => _button9EventColor1;
        set {
            _button9EventColor1 = value;
            RaisePropertyChangedEvent("Button9EventColor1");
        }
    }

    public string Button9EventColor2 {
        get => _button9EventColor2;
        set {
            _button9EventColor2 = value;
            RaisePropertyChangedEvent("Button9EventColor2");
        }
    }

    public string Button9EventColor3 {
        get => _button9EventColor3;
        set {
            _button9EventColor3 = value;
            RaisePropertyChangedEvent("Button9EventColor3");
        }
    }

    public string Button10EventColor1 {
        get => _button10EventColor1;
        set {
            _button10EventColor1 = value;
            RaisePropertyChangedEvent("Button10EventColor1");
        }
    }

    public string Button10EventColor2 {
        get => _button10EventColor2;
        set {
            _button10EventColor2 = value;
            RaisePropertyChangedEvent("Button10EventColor2");
        }
    }

    public string Button10EventColor3 {
        get => _button10EventColor3;
        set {
            _button10EventColor3 = value;
            RaisePropertyChangedEvent("Button10EventColor3");
        }
    }

    public string Button11EventColor1 {
        get => _button11EventColor1;
        set {
            _button11EventColor1 = value;
            RaisePropertyChangedEvent("Button11EventColor1");
        }
    }

    public string Button11EventColor2 {
        get => _button11EventColor2;
        set {
            _button11EventColor2 = value;
            RaisePropertyChangedEvent("Button11EventColor2");
        }
    }

    public string Button11EventColor3 {
        get => _button11EventColor3;
        set {
            _button11EventColor3 = value;
            RaisePropertyChangedEvent("Button11EventColor3");
        }
    }

    public string Button12EventColor1 {
        get => _button12EventColor1;
        set {
            _button12EventColor1 = value;
            RaisePropertyChangedEvent("Button12EventColor1");
        }
    }

    public string Button12EventColor2 {
        get => _button12EventColor2;
        set {
            _button12EventColor2 = value;
            RaisePropertyChangedEvent("Button12EventColor2");
        }
    }

    public string Button12EventColor3 {
        get => _button12EventColor3;
        set {
            _button12EventColor3 = value;
            RaisePropertyChangedEvent("Button12EventColor3");
        }
    }

    public string Button13EventColor1 {
        get => _button13EventColor1;
        set {
            _button13EventColor1 = value;
            RaisePropertyChangedEvent("Button13EventColor1");
        }
    }

    public string Button13EventColor2 {
        get => _button13EventColor2;
        set {
            _button13EventColor2 = value;
            RaisePropertyChangedEvent("Button13EventColor2");
        }
    }

    public string Button13EventColor3 {
        get => _button13EventColor3;
        set {
            _button13EventColor3 = value;
            RaisePropertyChangedEvent("Button13EventColor3");
        }
    }

    public string Button14EventColor1 {
        get => _button14EventColor1;
        set {
            _button14EventColor1 = value;
            RaisePropertyChangedEvent("Button14EventColor1");
        }
    }

    public string Button14EventColor2 {
        get => _button14EventColor2;
        set {
            _button14EventColor2 = value;
            RaisePropertyChangedEvent("Button14EventColor2");
        }
    }

    public string Button14EventColor3 {
        get => _button14EventColor3;
        set {
            _button14EventColor3 = value;
            RaisePropertyChangedEvent("Button14EventColor3");
        }
    }

    public string Button15EventColor1 {
        get => _button15EventColor1;
        set {
            _button15EventColor1 = value;
            RaisePropertyChangedEvent("Button15EventColor1");
        }
    }

    public string Button15EventColor2 {
        get => _button15EventColor2;
        set {
            _button15EventColor2 = value;
            RaisePropertyChangedEvent("Button15EventColor2");
        }
    }

    public string Button15EventColor3 {
        get => _button15EventColor3;
        set {
            _button15EventColor3 = value;
            RaisePropertyChangedEvent("Button15EventColor3");
        }
    }

    public string Button16EventColor1 {
        get => _button16EventColor1;
        set {
            _button16EventColor1 = value;
            RaisePropertyChangedEvent("Button16EventColor1");
        }
    }

    public string Button16EventColor2 {
        get => _button16EventColor2;
        set {
            _button16EventColor2 = value;
            RaisePropertyChangedEvent("Button16EventColor2");
        }
    }

    public string Button16EventColor3 {
        get => _button16EventColor3;
        set {
            _button16EventColor3 = value;
            RaisePropertyChangedEvent("Button16EventColor3");
        }
    }

    public string Button17EventColor1 {
        get => _button17EventColor1;
        set {
            _button17EventColor1 = value;
            RaisePropertyChangedEvent("Button17EventColor1");
        }
    }

    public string Button17EventColor2 {
        get => _button17EventColor2;
        set {
            _button17EventColor2 = value;
            RaisePropertyChangedEvent("Button17EventColor2");
        }
    }

    public string Button17EventColor3 {
        get => _button17EventColor3;
        set {
            _button17EventColor3 = value;
            RaisePropertyChangedEvent("Button17EventColor3");
        }
    }

    public string Button18EventColor1 {
        get => _button18EventColor1;
        set {
            _button18EventColor1 = value;
            RaisePropertyChangedEvent("Button18EventColor1");
        }
    }

    public string Button18EventColor2 {
        get => _button18EventColor2;
        set {
            _button18EventColor2 = value;
            RaisePropertyChangedEvent("Button18EventColor2");
        }
    }

    public string Button18EventColor3 {
        get => _button18EventColor3;
        set {
            _button18EventColor3 = value;
            RaisePropertyChangedEvent("Button18EventColor3");
        }
    }

    public string Button19EventColor1 {
        get => _button19EventColor1;
        set {
            _button19EventColor1 = value;
            RaisePropertyChangedEvent("Button19EventColor1");
        }
    }

    public string Button19EventColor2 {
        get => _button19EventColor2;
        set {
            _button19EventColor2 = value;
            RaisePropertyChangedEvent("Button19EventColor2");
        }
    }

    public string Button19EventColor3 {
        get => _button19EventColor3;
        set {
            _button19EventColor3 = value;
            RaisePropertyChangedEvent("Button19EventColor3");
        }
    }

    public string Button20EventColor1 {
        get => _button20EventColor1;
        set {
            _button20EventColor1 = value;
            RaisePropertyChangedEvent("Button20EventColor1");
        }
    }

    public string Button20EventColor2 {
        get => _button20EventColor2;
        set {
            _button20EventColor2 = value;
            RaisePropertyChangedEvent("Button20EventColor2");
        }
    }

    public string Button20EventColor3 {
        get => _button20EventColor3;
        set {
            _button20EventColor3 = value;
            RaisePropertyChangedEvent("Button20EventColor3");
        }
    }

    public string Button21EventColor1 {
        get => _button21EventColor1;
        set {
            _button21EventColor1 = value;
            RaisePropertyChangedEvent("Button21EventColor1");
        }
    }

    public string Button21EventColor2 {
        get => _button21EventColor2;
        set {
            _button21EventColor2 = value;
            RaisePropertyChangedEvent("Button21EventColor2");
        }
    }

    public string Button21EventColor3 {
        get => _button21EventColor3;
        set {
            _button21EventColor3 = value;
            RaisePropertyChangedEvent("Button21EventColor3");
        }
    }

    public string Button22EventColor1 {
        get => _button22EventColor1;
        set {
            _button22EventColor1 = value;
            RaisePropertyChangedEvent("Button22EventColor1");
        }
    }

    public string Button22EventColor2 {
        get => _button22EventColor2;
        set {
            _button22EventColor2 = value;
            RaisePropertyChangedEvent("Button22EventColor2");
        }
    }

    public string Button22EventColor3 {
        get => _button22EventColor3;
        set {
            _button22EventColor3 = value;
            RaisePropertyChangedEvent("Button22EventColor3");
        }
    }

    public string Button23EventColor1 {
        get => _button23EventColor1;
        set {
            _button23EventColor1 = value;
            RaisePropertyChangedEvent("Button23EventColor1");
        }
    }

    public string Button23EventColor2 {
        get => _button23EventColor2;
        set {
            _button23EventColor2 = value;
            RaisePropertyChangedEvent("Button23EventColor2");
        }
    }

    public string Button23EventColor3 {
        get => _button23EventColor3;
        set {
            _button23EventColor3 = value;
            RaisePropertyChangedEvent("Button23EventColor3");
        }
    }

    public string Button24EventColor1 {
        get => _button24EventColor1;
        set {
            _button24EventColor1 = value;
            RaisePropertyChangedEvent("Button24EventColor1");
        }
    }

    public string Button24EventColor2 {
        get => _button24EventColor2;
        set {
            _button24EventColor2 = value;
            RaisePropertyChangedEvent("Button24EventColor2");
        }
    }

    public string Button24EventColor3 {
        get => _button24EventColor3;
        set {
            _button24EventColor3 = value;
            RaisePropertyChangedEvent("Button24EventColor3");
        }
    }

    public string Button25EventColor1 {
        get => _button25EventColor1;
        set {
            _button25EventColor1 = value;
            RaisePropertyChangedEvent("Button25EventColor1");
        }
    }

    public string Button25EventColor2 {
        get => _button25EventColor2;
        set {
            _button25EventColor2 = value;
            RaisePropertyChangedEvent("Button25EventColor2");
        }
    }

    public string Button25EventColor3 {
        get => _button25EventColor3;
        set {
            _button25EventColor3 = value;
            RaisePropertyChangedEvent("Button25EventColor3");
        }
    }

    public string Button26EventColor1 {
        get => _button26EventColor1;
        set {
            _button26EventColor1 = value;
            RaisePropertyChangedEvent("Button26EventColor1");
        }
    }

    public string Button26EventColor2 {
        get => _button26EventColor2;
        set {
            _button26EventColor2 = value;
            RaisePropertyChangedEvent("Button26EventColor2");
        }
    }

    public string Button26EventColor3 {
        get => _button26EventColor3;
        set {
            _button26EventColor3 = value;
            RaisePropertyChangedEvent("Button26EventColor3");
        }
    }

    public string Button27EventColor1 {
        get => _button27EventColor1;
        set {
            _button27EventColor1 = value;
            RaisePropertyChangedEvent("Button27EventColor1");
        }
    }

    public string Button27EventColor2 {
        get => _button27EventColor2;
        set {
            _button27EventColor2 = value;
            RaisePropertyChangedEvent("Button27EventColor2");
        }
    }

    public string Button27EventColor3 {
        get => _button27EventColor3;
        set {
            _button27EventColor3 = value;
            RaisePropertyChangedEvent("Button27EventColor3");
        }
    }

    public string Button28EventColor1 {
        get => _button28EventColor1;
        set {
            _button28EventColor1 = value;
            RaisePropertyChangedEvent("Button28EventColor1");
        }
    }

    public string Button28EventColor2 {
        get => _button28EventColor2;
        set {
            _button28EventColor2 = value;
            RaisePropertyChangedEvent("Button28EventColor2");
        }
    }

    public string Button28EventColor3 {
        get => _button28EventColor3;
        set {
            _button28EventColor3 = value;
            RaisePropertyChangedEvent("Button28EventColor3");
        }
    }

    public string Button29EventColor1 {
        get => _button29EventColor1;
        set {
            _button29EventColor1 = value;
            RaisePropertyChangedEvent("Button29EventColor1");
        }
    }

    public string Button29EventColor2 {
        get => _button29EventColor2;
        set {
            _button29EventColor2 = value;
            RaisePropertyChangedEvent("Button29EventColor2");
        }
    }

    public string Button29EventColor3 {
        get => _button29EventColor3;
        set {
            _button29EventColor3 = value;
            RaisePropertyChangedEvent("Button29EventColor3");
        }
    }

    public string Button30EventColor1 {
        get => _button30EventColor1;
        set {
            _button30EventColor1 = value;
            RaisePropertyChangedEvent("Button30EventColor1");
        }
    }

    public string Button30EventColor2 {
        get => _button30EventColor2;
        set {
            _button30EventColor2 = value;
            RaisePropertyChangedEvent("Button30EventColor2");
        }
    }

    public string Button30EventColor3 {
        get => _button30EventColor3;
        set {
            _button30EventColor3 = value;
            RaisePropertyChangedEvent("Button30EventColor3");
        }
    }

    public string Button31EventColor1 {
        get => _button31EventColor1;
        set {
            _button31EventColor1 = value;
            RaisePropertyChangedEvent("Button31EventColor1");
        }
    }

    public string Button31EventColor2 {
        get => _button31EventColor2;
        set {
            _button31EventColor2 = value;
            RaisePropertyChangedEvent("Button31EventColor2");
        }
    }

    public string Button31EventColor3 {
        get => _button31EventColor3;
        set {
            _button31EventColor3 = value;
            RaisePropertyChangedEvent("Button31EventColor3");
        }
    }

    public string Button32EventColor1 {
        get => _button32EventColor1;
        set {
            _button32EventColor1 = value;
            RaisePropertyChangedEvent("Button32EventColor1");
        }
    }

    public string Button32EventColor2 {
        get => _button32EventColor2;
        set {
            _button32EventColor2 = value;
            RaisePropertyChangedEvent("Button32EventColor2");
        }
    }

    public string Button32EventColor3 {
        get => _button32EventColor3;
        set {
            _button32EventColor3 = value;
            RaisePropertyChangedEvent("Button32EventColor3");
        }
    }

    public string Button33EventColor1 {
        get => _button33EventColor1;
        set {
            _button33EventColor1 = value;
            RaisePropertyChangedEvent("Button33EventColor1");
        }
    }

    public string Button33EventColor2 {
        get => _button33EventColor2;
        set {
            _button33EventColor2 = value;
            RaisePropertyChangedEvent("Button33EventColor2");
        }
    }

    public string Button33EventColor3 {
        get => _button33EventColor3;
        set {
            _button33EventColor3 = value;
            RaisePropertyChangedEvent("Button33EventColor3");
        }
    }

    public string Button34EventColor1 {
        get => _button34EventColor1;
        set {
            _button34EventColor1 = value;
            RaisePropertyChangedEvent("Button34EventColor1");
        }
    }

    public string Button34EventColor2 {
        get => _button34EventColor2;
        set {
            _button34EventColor2 = value;
            RaisePropertyChangedEvent("Button34EventColor2");
        }
    }

    public string Button34EventColor3 {
        get => _button34EventColor3;
        set {
            _button34EventColor3 = value;
            RaisePropertyChangedEvent("Button34EventColor3");
        }
    }

    public string Button35EventColor1 {
        get => _button35EventColor1;
        set {
            _button35EventColor1 = value;
            RaisePropertyChangedEvent("Button35EventColor1");
        }
    }

    public string Button35EventColor2 {
        get => _button35EventColor2;
        set {
            _button35EventColor2 = value;
            RaisePropertyChangedEvent("Button35EventColor2");
        }
    }

    public string Button35EventColor3 {
        get => _button35EventColor3;
        set {
            _button35EventColor3 = value;
            RaisePropertyChangedEvent("Button35EventColor3");
        }
    }

    public string Button36EventColor1 {
        get => _button36EventColor1;
        set {
            _button36EventColor1 = value;
            RaisePropertyChangedEvent("Button36EventColor1");
        }
    }

    public string Button36EventColor2 {
        get => _button36EventColor2;
        set {
            _button36EventColor2 = value;
            RaisePropertyChangedEvent("Button36EventColor2");
        }
    }

    public string Button36EventColor3 {
        get => _button36EventColor3;
        set {
            _button36EventColor3 = value;
            RaisePropertyChangedEvent("Button36EventColor3");
        }
    }

    public string Button37EventColor1 {
        get => _button37EventColor1;
        set {
            _button37EventColor1 = value;
            RaisePropertyChangedEvent("Button37EventColor1");
        }
    }

    public string Button37EventColor2 {
        get => _button37EventColor2;
        set {
            _button37EventColor2 = value;
            RaisePropertyChangedEvent("Button37EventColor2");
        }
    }

    public string Button37EventColor3 {
        get => _button37EventColor3;
        set {
            _button37EventColor3 = value;
            RaisePropertyChangedEvent("Button37EventColor3");
        }
    }

    public string Button38EventColor1 {
        get => _button38EventColor1;
        set {
            _button38EventColor1 = value;
            RaisePropertyChangedEvent("Button38EventColor1");
        }
    }

    public string Button38EventColor2 {
        get => _button38EventColor2;
        set {
            _button38EventColor2 = value;
            RaisePropertyChangedEvent("Button38EventColor2");
        }
    }

    public string Button38EventColor3 {
        get => _button38EventColor3;
        set {
            _button38EventColor3 = value;
            RaisePropertyChangedEvent("Button38EventColor3");
        }
    }

    public string Button39EventColor1 {
        get => _button39EventColor1;
        set {
            _button39EventColor1 = value;
            RaisePropertyChangedEvent("Button39EventColor1");
        }
    }

    public string Button39EventColor2 {
        get => _button39EventColor2;
        set {
            _button39EventColor2 = value;
            RaisePropertyChangedEvent("Button39EventColor2");
        }
    }

    public string Button39EventColor3 {
        get => _button39EventColor3;
        set {
            _button39EventColor3 = value;
            RaisePropertyChangedEvent("Button39EventColor3");
        }
    }

    public string Button40EventColor1 {
        get => _button40EventColor1;
        set {
            _button40EventColor1 = value;
            RaisePropertyChangedEvent("Button40EventColor1");
        }
    }

    public string Button40EventColor2 {
        get => _button40EventColor2;
        set {
            _button40EventColor2 = value;
            RaisePropertyChangedEvent("Button40EventColor2");
        }
    }

    public string Button40EventColor3 {
        get => _button40EventColor3;
        set {
            _button40EventColor3 = value;
            RaisePropertyChangedEvent("Button40EventColor3");
        }
    }

    public string Button41EventColor1 {
        get => _button41EventColor1;
        set {
            _button41EventColor1 = value;
            RaisePropertyChangedEvent("Button41EventColor1");
        }
    }

    public string Button41EventColor2 {
        get => _button41EventColor2;
        set {
            _button41EventColor2 = value;
            RaisePropertyChangedEvent("Button41EventColor2");
        }
    }

    public string Button41EventColor3 {
        get => _button41EventColor3;
        set {
            _button41EventColor3 = value;
            RaisePropertyChangedEvent("Button41EventColor3");
        }
    }

    public string Button42EventColor1 {
        get => _button42EventColor1;
        set {
            _button42EventColor1 = value;
            RaisePropertyChangedEvent("Button42EventColor1");
        }
    }

    public string Button42EventColor2 {
        get => _button42EventColor2;
        set {
            _button42EventColor2 = value;
            RaisePropertyChangedEvent("Button42EventColor2");
        }
    }

    public string Button42EventColor3 {
        get => _button42EventColor3;
        set {
            _button42EventColor3 = value;
            RaisePropertyChangedEvent("Button42EventColor3");
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

    #endregion
}