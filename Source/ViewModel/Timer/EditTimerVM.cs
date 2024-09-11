using System;
using System.Windows.Input;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Timer;

public class EditTimerVM : BaseViewModel {
    private string _timer1Text, _timer1Color, _timer2Text, _timer2Color, _timer3Text, _timer3Color, _timer4Text, _timer4Color;

    public EditTimerVM() {
        RefreshTimer();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonLogic, true);
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "RefreshTimer") {
            if (ReferenceValues.JsonTimerMaster.IsTimer1Running || ReferenceValues.JsonTimerMaster.IsTimer2Running || ReferenceValues.JsonTimerMaster.IsTimer3Running ||
                ReferenceValues.JsonTimerMaster.IsTimer4Running) {
                RefreshTimer();
            }
        }
    }

    private void RefreshTimer() {
        TimeSpan time = TimeSpan.FromSeconds(ReferenceValues.JsonTimerMaster.Timer1Seconds);
        Timer1Text = (time < TimeSpan.Zero ? "-" : "") + time.ToString(@"hh\:mm\:ss");

        if (!ReferenceValues.JsonTimerMaster.IsTimer1Running) {
            Timer1Color = "White";
        } else {
            Timer1Color = ReferenceValues.JsonTimerMaster.Timer1Seconds < 0 ? "Red" : "Green";
        }

        time = TimeSpan.FromSeconds(ReferenceValues.JsonTimerMaster.Timer2Seconds);
        Timer2Text = (time < TimeSpan.Zero ? "-" : "") + time.ToString(@"hh\:mm\:ss");

        if (!ReferenceValues.JsonTimerMaster.IsTimer2Running) {
            Timer2Color = "White";
        } else {
            Timer2Color = ReferenceValues.JsonTimerMaster.Timer2Seconds < 0 ? "Red" : "Green";
        }

        time = TimeSpan.FromSeconds(ReferenceValues.JsonTimerMaster.Timer3Seconds);
        Timer3Text = (time < TimeSpan.Zero ? "-" : "") + time.ToString(@"hh\:mm\:ss");

        if (!ReferenceValues.JsonTimerMaster.IsTimer3Running) {
            Timer3Color = "White";
        } else {
            Timer3Color = ReferenceValues.JsonTimerMaster.Timer3Seconds < 0 ? "Red" : "Green";
        }

        time = TimeSpan.FromSeconds(ReferenceValues.JsonTimerMaster.Timer4Seconds);
        Timer4Text = (time < TimeSpan.Zero ? "-" : "") + time.ToString(@"hh\:mm\:ss");

        if (!ReferenceValues.JsonTimerMaster.IsTimer4Running) {
            Timer4Color = "White";
        } else {
            Timer4Color = ReferenceValues.JsonTimerMaster.Timer4Seconds < 0 ? "Red" : "Green";
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "timer1Add1":
            if (!ReferenceValues.JsonTimerMaster.IsTimer1Running) {
                ReferenceValues.JsonTimerMaster.Timer1Seconds += 60;
                RefreshTimer();
            }

            break;
        case "timer1Add5":
            if (!ReferenceValues.JsonTimerMaster.IsTimer1Running) {
                ReferenceValues.JsonTimerMaster.Timer1Seconds += 300;
                RefreshTimer();
            }

            break;
        case "timer1Add10":
            if (!ReferenceValues.JsonTimerMaster.IsTimer1Running) {
                ReferenceValues.JsonTimerMaster.Timer1Seconds += 600;
                RefreshTimer();
            }

            break;
        case "timer1Sub1":
            if (!ReferenceValues.JsonTimerMaster.IsTimer1Running) {
                ReferenceValues.JsonTimerMaster.Timer1Seconds -= 60;
                if (ReferenceValues.JsonTimerMaster.Timer1Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer1Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer1Sub5":
            if (!ReferenceValues.JsonTimerMaster.IsTimer1Running) {
                ReferenceValues.JsonTimerMaster.Timer1Seconds -= 300;
                if (ReferenceValues.JsonTimerMaster.Timer1Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer1Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer1Sub10":
            if (!ReferenceValues.JsonTimerMaster.IsTimer1Running) {
                ReferenceValues.JsonTimerMaster.Timer1Seconds -= 600;
                if (ReferenceValues.JsonTimerMaster.Timer1Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer1Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer1PlayPause":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            ReferenceValues.JsonTimerMaster.IsTimer1Running = !ReferenceValues.JsonTimerMaster.IsTimer1Running;

            break;
        case "timer1Stop":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            ReferenceValues.JsonTimerMaster.IsTimer1Running = false;
            ReferenceValues.JsonTimerMaster.Timer1Seconds = 0;
            RefreshTimer();

            break;
        case "timer2Add1":
            if (!ReferenceValues.JsonTimerMaster.IsTimer2Running) {
                ReferenceValues.JsonTimerMaster.Timer2Seconds += 60;
                RefreshTimer();
            }

            break;
        case "timer2Add5":
            if (!ReferenceValues.JsonTimerMaster.IsTimer2Running) {
                ReferenceValues.JsonTimerMaster.Timer2Seconds += 300;
                RefreshTimer();
            }

            break;
        case "timer2Add10":
            if (!ReferenceValues.JsonTimerMaster.IsTimer2Running) {
                ReferenceValues.JsonTimerMaster.Timer2Seconds += 600;
                RefreshTimer();
            }

            break;
        case "timer2Sub1":
            if (!ReferenceValues.JsonTimerMaster.IsTimer2Running) {
                ReferenceValues.JsonTimerMaster.Timer2Seconds -= 60;
                if (ReferenceValues.JsonTimerMaster.Timer2Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer2Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer2Sub5":
            if (!ReferenceValues.JsonTimerMaster.IsTimer2Running) {
                ReferenceValues.JsonTimerMaster.Timer2Seconds -= 300;
                if (ReferenceValues.JsonTimerMaster.Timer2Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer2Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer2Sub10":
            if (!ReferenceValues.JsonTimerMaster.IsTimer2Running) {
                ReferenceValues.JsonTimerMaster.Timer2Seconds -= 600;
                if (ReferenceValues.JsonTimerMaster.Timer2Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer2Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer2PlayPause":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            ReferenceValues.JsonTimerMaster.IsTimer2Running = !ReferenceValues.JsonTimerMaster.IsTimer2Running;

            break;
        case "timer2Stop":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            ReferenceValues.JsonTimerMaster.IsTimer2Running = false;
            ReferenceValues.JsonTimerMaster.Timer2Seconds = 0;
            RefreshTimer();

            break;
        case "timer3Add1":
            if (!ReferenceValues.JsonTimerMaster.IsTimer3Running) {
                ReferenceValues.JsonTimerMaster.Timer3Seconds += 60;
                RefreshTimer();
            }

            break;
        case "timer3Add5":
            if (!ReferenceValues.JsonTimerMaster.IsTimer3Running) {
                ReferenceValues.JsonTimerMaster.Timer3Seconds += 300;
                RefreshTimer();
            }

            break;
        case "timer3Add10":
            if (!ReferenceValues.JsonTimerMaster.IsTimer3Running) {
                ReferenceValues.JsonTimerMaster.Timer3Seconds += 600;
                RefreshTimer();
            }

            break;
        case "timer3Sub1":
            if (!ReferenceValues.JsonTimerMaster.IsTimer3Running) {
                ReferenceValues.JsonTimerMaster.Timer3Seconds -= 60;
                if (ReferenceValues.JsonTimerMaster.Timer3Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer3Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer3Sub5":
            if (!ReferenceValues.JsonTimerMaster.IsTimer3Running) {
                ReferenceValues.JsonTimerMaster.Timer3Seconds -= 300;
                if (ReferenceValues.JsonTimerMaster.Timer3Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer3Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer3Sub10":
            if (!ReferenceValues.JsonTimerMaster.IsTimer3Running) {
                ReferenceValues.JsonTimerMaster.Timer3Seconds -= 600;
                if (ReferenceValues.JsonTimerMaster.Timer3Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer3Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer3PlayPause":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            ReferenceValues.JsonTimerMaster.IsTimer3Running = !ReferenceValues.JsonTimerMaster.IsTimer3Running;

            break;
        case "timer3Stop":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            ReferenceValues.JsonTimerMaster.IsTimer3Running = false;
            ReferenceValues.JsonTimerMaster.Timer3Seconds = 0;
            RefreshTimer();

            break;
        case "timer4Add1":
            if (!ReferenceValues.JsonTimerMaster.IsTimer4Running) {
                ReferenceValues.JsonTimerMaster.Timer4Seconds += 60;
                RefreshTimer();
            }

            break;
        case "timer4Add5":
            if (!ReferenceValues.JsonTimerMaster.IsTimer4Running) {
                ReferenceValues.JsonTimerMaster.Timer4Seconds += 300;
                RefreshTimer();
            }

            break;
        case "timer4Add10":
            if (!ReferenceValues.JsonTimerMaster.IsTimer4Running) {
                ReferenceValues.JsonTimerMaster.Timer4Seconds += 600;
                RefreshTimer();
            }

            break;
        case "timer4Sub1":
            if (!ReferenceValues.JsonTimerMaster.IsTimer4Running) {
                ReferenceValues.JsonTimerMaster.Timer4Seconds -= 60;
                if (ReferenceValues.JsonTimerMaster.Timer4Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer4Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer4Sub5":
            if (!ReferenceValues.JsonTimerMaster.IsTimer4Running) {
                ReferenceValues.JsonTimerMaster.Timer4Seconds -= 300;
                if (ReferenceValues.JsonTimerMaster.Timer4Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer4Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer4Sub10":
            if (!ReferenceValues.JsonTimerMaster.IsTimer4Running) {
                ReferenceValues.JsonTimerMaster.Timer4Seconds -= 600;
                if (ReferenceValues.JsonTimerMaster.Timer4Seconds < 0) {
                    ReferenceValues.JsonTimerMaster.Timer4Seconds = 0;
                }

                RefreshTimer();
            }

            break;
        case "timer4PlayPause":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            ReferenceValues.JsonTimerMaster.IsTimer4Running = !ReferenceValues.JsonTimerMaster.IsTimer4Running;

            break;
        case "timer4Stop":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            ReferenceValues.JsonTimerMaster.IsTimer4Running = false;
            ReferenceValues.JsonTimerMaster.Timer4Seconds = 0;
            RefreshTimer();

            break;
        }
    }


    #region Fields

    public string Timer1Text {
        get => _timer1Text;
        set {
            _timer1Text = value;
            RaisePropertyChangedEvent("Timer1Text");
        }
    }

    public string Timer1Color {
        get => _timer1Color;
        set {
            _timer1Color = value;
            RaisePropertyChangedEvent("Timer1Color");
        }
    }

    public string Timer2Text {
        get => _timer2Text;
        set {
            _timer2Text = value;
            RaisePropertyChangedEvent("Timer2Text");
        }
    }

    public string Timer2Color {
        get => _timer2Color;
        set {
            _timer2Color = value;
            RaisePropertyChangedEvent("Timer2Color");
        }
    }

    public string Timer3Text {
        get => _timer3Text;
        set {
            _timer3Text = value;
            RaisePropertyChangedEvent("Timer3Text");
        }
    }

    public string Timer3Color {
        get => _timer3Color;
        set {
            _timer3Color = value;
            RaisePropertyChangedEvent("Timer3Color");
        }
    }

    public string Timer4Text {
        get => _timer4Text;
        set {
            _timer4Text = value;
            RaisePropertyChangedEvent("Timer4Text");
        }
    }

    public string Timer4Color {
        get => _timer4Color;
        set {
            _timer4Color = value;
            RaisePropertyChangedEvent("Timer4Color");
        }
    }

    #endregion
}