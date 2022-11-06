using System;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel {
    public class CalenderVM : BaseViewModel {
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
            _button42EventText3;

        public CalenderVM() {
            Button1Date = "";
            Button1HolidayText = "";
            Button1EventText1 = "";
            Button1EventText2 = "";
            Button1EventText3 = "";
            Button2Date = "";
            Button2HolidayText = "";
            Button2EventText1 = "";
            Button2EventText2 = "";
            Button2EventText3 = "";
            Button3Date = "";
            Button3HolidayText = "";
            Button3EventText1 = "";
            Button3EventText2 = "";
            Button3EventText3 = "";
            Button4Date = "";
            Button4HolidayText = "";
            Button4EventText1 = "";
            Button4EventText2 = "";
            Button4EventText3 = "";
            Button5Date = "";
            Button5HolidayText = "";
            Button5EventText1 = "";
            Button5EventText2 = "";
            Button5EventText3 = "";
            Button6Date = "";
            Button6HolidayText = "";
            Button6EventText1 = "";
            Button6EventText2 = "";
            Button6EventText3 = "";
            Button7Date = "";
            Button7HolidayText = "";
            Button7EventText1 = "";
            Button7EventText2 = "";
            Button7EventText3 = "";
            Button8Date = "";
            Button8HolidayText = "";
            Button8EventText1 = "";
            Button8EventText2 = "";
            Button8EventText3 = "";
            Button9Date = "";
            Button9HolidayText = "";
            Button9EventText1 = "";
            Button9EventText2 = "";
            Button9EventText3 = "";
            Button10Date = "";
            Button10HolidayText = "";
            Button10EventText1 = "";
            Button10EventText2 = "";
            Button10EventText3 = "";
            Button11Date = "";
            Button11HolidayText = "";
            Button11EventText1 = "";
            Button11EventText2 = "";
            Button11EventText3 = "";
            Button12Date = "";
            Button12HolidayText = "";
            Button12EventText1 = "";
            Button12EventText2 = "";
            Button12EventText3 = "";
            Button13Date = "";
            Button13HolidayText = "";
            Button13EventText1 = "";
            Button13EventText2 = "";
            Button13EventText3 = "";
            Button14Date = "";
            Button14HolidayText = "";
            Button14EventText1 = "";
            Button14EventText2 = "";
            Button14EventText3 = "";
            Button15Date = "";
            Button15HolidayText = "";
            Button15EventText1 = "";
            Button15EventText2 = "";
            Button15EventText3 = "";
            Button16Date = "";
            Button16HolidayText = "";
            Button16EventText1 = "";
            Button16EventText2 = "";
            Button16EventText3 = "";
            Button17Date = "";
            Button17HolidayText = "";
            Button17EventText1 = "";
            Button17EventText2 = "";
            Button17EventText3 = "";
            Button18Date = "";
            Button18HolidayText = "";
            Button18EventText1 = "";
            Button18EventText2 = "";
            Button18EventText3 = "";
            Button19Date = "";
            Button19HolidayText = "";
            Button19EventText1 = "";
            Button19EventText2 = "";
            Button19EventText3 = "";
            Button20Date = "";
            Button20HolidayText = "";
            Button20EventText1 = "";
            Button20EventText2 = "";
            Button20EventText3 = "";
            Button21Date = "";
            Button21HolidayText = "";
            Button21EventText1 = "";
            Button21EventText2 = "";
            Button21EventText3 = "";
            Button22Date = "";
            Button22HolidayText = "";
            Button22EventText1 = "";
            Button22EventText2 = "";
            Button22EventText3 = "";
            Button23Date = "";
            Button23HolidayText = "";
            Button23EventText1 = "";
            Button23EventText2 = "";
            Button23EventText3 = "";
            Button24Date = "";
            Button24HolidayText = "";
            Button24EventText1 = "";
            Button24EventText2 = "";
            Button24EventText3 = "";
            Button25Date = "";
            Button25HolidayText = "";
            Button25EventText1 = "";
            Button25EventText2 = "";
            Button25EventText3 = "";
            Button26Date = "";
            Button26HolidayText = "";
            Button26EventText1 = "";
            Button26EventText2 = "";
            Button26EventText3 = "";
            Button27Date = "";
            Button27HolidayText = "";
            Button27EventText1 = "";
            Button27EventText2 = "";
            Button27EventText3 = "";
            Button28Date = "";
            Button28HolidayText = "";
            Button28EventText1 = "";
            Button28EventText2 = "";
            Button28EventText3 = "";
            Button29Date = "";
            Button29HolidayText = "";
            Button29EventText1 = "";
            Button29EventText2 = "";
            Button29EventText3 = "";
            Button30Date = "";
            Button30HolidayText = "";
            Button30EventText1 = "";
            Button30EventText2 = "";
            Button30EventText3 = "";
            Button31Date = "";
            Button31HolidayText = "";
            Button31EventText1 = "";
            Button31EventText2 = "";
            Button31EventText3 = "";
            Button32Date = "";
            Button32HolidayText = "";
            Button32EventText1 = "";
            Button32EventText2 = "";
            Button32EventText3 = "";
            Button33Date = "";
            Button33HolidayText = "";
            Button33EventText1 = "";
            Button33EventText2 = "";
            Button33EventText3 = "";
            Button34Date = "";
            Button34HolidayText = "";
            Button34EventText1 = "";
            Button34EventText2 = "";
            Button34EventText3 = "";
            Button35Date = "";
            Button35HolidayText = "";
            Button35EventText1 = "";
            Button35EventText2 = "";
            Button35EventText3 = "";
            Button36Date = "";
            Button36HolidayText = "";
            Button36EventText1 = "";
            Button36EventText2 = "";
            Button36EventText3 = "";
            Button37Date = "";
            Button37HolidayText = "";
            Button37EventText1 = "";
            Button37EventText2 = "";
            Button37EventText3 = "";
            Button38Date = "";
            Button38HolidayText = "";
            Button38EventText1 = "";
            Button38EventText2 = "";
            Button38EventText3 = "";
            Button39Date = "";
            Button39HolidayText = "";
            Button39EventText1 = "";
            Button39EventText2 = "";
            Button39EventText3 = "";
            Button40Date = "";
            Button40HolidayText = "";
            Button40EventText1 = "";
            Button40EventText2 = "";
            Button40EventText3 = "";
            Button41Date = "";
            Button41HolidayText = "";
            Button41EventText1 = "";
            Button41EventText2 = "";
            Button41EventText3 = "";
            Button42Date = "";
            Button42HolidayText = "";
            Button42EventText1 = "";
            Button42EventText2 = "";
            Button42EventText3 = "";

            PopulateCalender(DateTime.Now);
        }

        public void PopulateCalender(DateTime dateTime) {
            dateTime = new DateTime(2023, 3, 10);
            DateTime startingOn2023 = new(2023, 1, 1);
            int mathDate = (dateTime.Year - startingOn2023.Year) * 504;
            mathDate += dateTime.Month * 42;
            dateTime = startingOn2023.AddDays(mathDate);

            Console.WriteLine(dateTime.ToShortDateString());
        }


        #region Fields

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

        #endregion
    }
}