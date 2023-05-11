using System;
using System.Windows.Input;
using System.Windows.Media;
using HomeControl.Source.Modules.Timer;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Timer;

public class TimerVM : BaseViewModel {
    private string _timer1Text, _timer2Text, _timer3Text, _timer4Text, _timer1Color, _timer2Color, _timer3Color, _timer4Color;

    public TimerVM() {
        Timer1Text = "NONE";
        Timer2Text = "NONE";
        Timer3Text = "NONE";
        Timer4Text = "NONE";
        Timer1Color = "White";
        Timer2Color = "White";
        Timer3Color = "White";
        Timer4Color = "White";
        ReferenceValues.IsTimerRunning = new[] { false, false, false, false };
        ReferenceValues.TimerMinutes = new int[4];
        ReferenceValues.TimerSeconds = new int[4];
        ReferenceValues.SwitchTimerDirection = new bool[4];

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "Refresh") {
            if (ReferenceValues.IsTimerRunning[0]) {
                if (!ReferenceValues.SwitchTimerDirection[0]) {
                    ReferenceValues.TimerSeconds[0]--;
                    if (ReferenceValues.TimerSeconds[0] < 0) {
                        ReferenceValues.TimerSeconds[0] = 59;
                        --ReferenceValues.TimerMinutes[0];
                        if (ReferenceValues.TimerMinutes[0] < 0) {
                            ReferenceValues.TimerMinutes[0] = 0;
                            ReferenceValues.TimerSeconds[0] = 1;
                            ReferenceValues.SwitchTimerDirection[0] = true;
                            ReferenceValues.IsTimerAlarmActive = true;
                        }
                    }
                } else {
                    ReferenceValues.TimerSeconds[0] = ++ReferenceValues.TimerSeconds[0];
                    if (ReferenceValues.TimerSeconds[0] > 59) {
                        ReferenceValues.TimerSeconds[0] = 0;
                        ++ReferenceValues.TimerMinutes[0];
                    }
                }
            }

            if (ReferenceValues.IsTimerRunning[1]) {
                if (!ReferenceValues.SwitchTimerDirection[1]) {
                    ReferenceValues.TimerSeconds[1]--;
                    if (ReferenceValues.TimerSeconds[1] < 0) {
                        ReferenceValues.TimerSeconds[1] = 59;
                        --ReferenceValues.TimerMinutes[1];
                        if (ReferenceValues.TimerMinutes[1] < 0) {
                            ReferenceValues.TimerMinutes[1] = 0;
                            ReferenceValues.TimerSeconds[1] = 1;
                            ReferenceValues.SwitchTimerDirection[1] = true;
                            ReferenceValues.IsTimerAlarmActive = true;
                        }
                    }
                } else {
                    ReferenceValues.TimerSeconds[1] = ++ReferenceValues.TimerSeconds[1];
                    if (ReferenceValues.TimerSeconds[1] > 59) {
                        ReferenceValues.TimerSeconds[1] = 0;
                        ++ReferenceValues.TimerMinutes[1];
                    }
                }
            }

            if (ReferenceValues.IsTimerRunning[2]) {
                if (!ReferenceValues.SwitchTimerDirection[2]) {
                    ReferenceValues.TimerSeconds[2]--;
                    if (ReferenceValues.TimerSeconds[2] < 0) {
                        ReferenceValues.TimerSeconds[2] = 59;
                        --ReferenceValues.TimerMinutes[2];
                        if (ReferenceValues.TimerMinutes[2] < 0) {
                            ReferenceValues.TimerMinutes[2] = 0;
                            ReferenceValues.TimerSeconds[2] = 1;
                            ReferenceValues.SwitchTimerDirection[2] = true;
                            ReferenceValues.IsTimerAlarmActive = true;
                        }
                    }
                } else {
                    ReferenceValues.TimerSeconds[2] = ++ReferenceValues.TimerSeconds[2];
                    if (ReferenceValues.TimerSeconds[2] > 59) {
                        ReferenceValues.TimerSeconds[2] = 0;
                        ++ReferenceValues.TimerMinutes[2];
                    }
                }
            }

            if (ReferenceValues.IsTimerRunning[3]) {
                if (!ReferenceValues.SwitchTimerDirection[3]) {
                    ReferenceValues.TimerSeconds[3]--;
                    if (ReferenceValues.TimerSeconds[3] < 0) {
                        ReferenceValues.TimerSeconds[3] = 59;
                        --ReferenceValues.TimerMinutes[3];
                        if (ReferenceValues.TimerMinutes[3] < 0) {
                            ReferenceValues.TimerMinutes[3] = 0;
                            ReferenceValues.TimerSeconds[3] = 1;
                            ReferenceValues.SwitchTimerDirection[3] = true;
                            ReferenceValues.IsTimerAlarmActive = true;
                        }
                    }
                } else {
                    ReferenceValues.TimerSeconds[3] = ++ReferenceValues.TimerSeconds[3];
                    if (ReferenceValues.TimerSeconds[3] > 59) {
                        ReferenceValues.TimerSeconds[3] = 0;
                        ++ReferenceValues.TimerMinutes[3];
                    }
                }
            }

            if (ReferenceValues.IsTimerRunning[0]) {
                Timer1Text = $"{ReferenceValues.TimerMinutes[0]:000}:{ReferenceValues.TimerSeconds[0]:00}";
                Timer1Color = ReferenceValues.SwitchTimerDirection[0] ? "Red" : "YellowGreen";
            }

            if (ReferenceValues.IsTimerRunning[1]) {
                Timer2Text = $"{ReferenceValues.TimerMinutes[1]:000}:{ReferenceValues.TimerSeconds[1]:00}";
                Timer2Color = ReferenceValues.SwitchTimerDirection[1] ? "Red" : "YellowGreen";
            }

            if (ReferenceValues.IsTimerRunning[2]) {
                Timer3Text = $"{ReferenceValues.TimerMinutes[2]:000}:{ReferenceValues.TimerSeconds[2]:00}";
                Timer3Color = ReferenceValues.SwitchTimerDirection[2] ? "Red" : "YellowGreen";
            }

            if (ReferenceValues.IsTimerRunning[3]) {
                Timer4Text = $"{ReferenceValues.TimerMinutes[3]:000}:{ReferenceValues.TimerSeconds[3]:00}";
                Timer4Color = ReferenceValues.SwitchTimerDirection[3] ? "Red" : "YellowGreen";
            }

            if (ReferenceValues.IsTimerAlarmActive) {
                MediaPlayer sound = new();
                sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/timerDone.wav"));
                sound.Play();
            }
        }
    }

    private void ButtonLogic(object param) {
        EditTimer editTimer = null;

        switch (param) {
        case "timer1":
            ReferenceValues.ActiveTimerEdit = 0;
            editTimer = new EditTimer();
            break;
        case "timer2":
            ReferenceValues.ActiveTimerEdit = 1;
            editTimer = new EditTimer();
            break;
        case "timer3":
            ReferenceValues.ActiveTimerEdit = 2;
            editTimer = new EditTimer();
            break;
        case "timer4":
            ReferenceValues.ActiveTimerEdit = 3;
            editTimer = new EditTimer();
            break;
        }

        if (editTimer != null) {
            editTimer.ShowDialog();
            editTimer.Close();
        }

        Timer1Text = $"{ReferenceValues.TimerMinutes[0]:000}:{ReferenceValues.TimerSeconds[0]:00}";
        Timer2Text = $"{ReferenceValues.TimerMinutes[1]:000}:{ReferenceValues.TimerSeconds[1]:00}";
        Timer3Text = $"{ReferenceValues.TimerMinutes[2]:000}:{ReferenceValues.TimerSeconds[2]:00}";
        Timer4Text = $"{ReferenceValues.TimerMinutes[3]:000}:{ReferenceValues.TimerSeconds[3]:00}";

        Timer1Color = ReferenceValues.SwitchTimerDirection[0] ? "Red" : "YellowGreen";
        Timer2Color = ReferenceValues.SwitchTimerDirection[1] ? "Red" : "YellowGreen";
        Timer3Color = ReferenceValues.SwitchTimerDirection[2] ? "Red" : "YellowGreen";
        Timer4Color = ReferenceValues.SwitchTimerDirection[3] ? "Red" : "YellowGreen";

        if (ReferenceValues.TimerMinutes[0] == 0 && ReferenceValues.TimerSeconds[0] == 0) {
            Timer1Text = "NONE";
            Timer1Color = "White";
        }

        if (ReferenceValues.TimerMinutes[1] == 0 && ReferenceValues.TimerSeconds[1] == 0) {
            Timer2Text = "NONE";
            Timer2Color = "White";
        }

        if (ReferenceValues.TimerMinutes[2] == 0 && ReferenceValues.TimerSeconds[2] == 0) {
            Timer3Text = "NONE";
            Timer3Color = "White";
        }

        if (ReferenceValues.TimerMinutes[3] == 0 && ReferenceValues.TimerSeconds[3] == 0) {
            Timer4Text = "NONE";
            Timer4Color = "White";
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

    public string Timer2Text {
        get => _timer2Text;
        set {
            _timer2Text = value;
            RaisePropertyChangedEvent("Timer2Text");
        }
    }

    public string Timer3Text {
        get => _timer3Text;
        set {
            _timer3Text = value;
            RaisePropertyChangedEvent("Timer3Text");
        }
    }

    public string Timer4Text {
        get => _timer4Text;
        set {
            _timer4Text = value;
            RaisePropertyChangedEvent("Timer4Text");
        }
    }

    public string Timer1Color {
        get => _timer1Color;
        set {
            _timer1Color = value;
            RaisePropertyChangedEvent("Timer1Color");
        }
    }

    public string Timer2Color {
        get => _timer2Color;
        set {
            _timer2Color = value;
            RaisePropertyChangedEvent("Timer2Color");
        }
    }

    public string Timer3Color {
        get => _timer3Color;
        set {
            _timer3Color = value;
            RaisePropertyChangedEvent("Timer3Color");
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