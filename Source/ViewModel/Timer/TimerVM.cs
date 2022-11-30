using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Resources;
using HomeControl.Source.Modules.Timer;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Timer;

public class TimerVM : BaseViewModel {
    private readonly SoundPlayer player;
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

        StreamResourceInfo sri = Application.GetResourceStream(new Uri("pack://application:,,,/HomeControl;component/Resources/Sounds/alarm.wav"));
        if (sri != null) {
            player = new SoundPlayer(sri.Stream);
        }

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "Refresh") {
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
                player.Load();
                player.PlayLooping();
            }
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "timer1":
            ReferenceValues.ActiveTimerEdit = 0;
            if (ReferenceValues.SwitchTimerDirection[0] && ReferenceValues.IsTimerRunning[0]) {
                ReferenceValues.IsTimerRunning[0] = false;
                ReferenceValues.IsTimerAlarmActive = false;
                player.Stop();
                Timer1Text = "NONE";
                Timer1Color = "White";
            } else {
                EditTimer editTimer = new();
                editTimer.ShowDialog();
                editTimer.Close();
            }

            break;
        case "timer2":
            ReferenceValues.ActiveTimerEdit = 1;
            if (ReferenceValues.SwitchTimerDirection[1] && ReferenceValues.IsTimerRunning[1]) {
                ReferenceValues.IsTimerRunning[1] = false;
                ReferenceValues.IsTimerAlarmActive = false;
                player.Stop();
                Timer2Text = "NONE";
                Timer2Color = "White";
            } else {
                EditTimer editTimer = new();
                editTimer.ShowDialog();
                editTimer.Close();
            }

            break;
        case "timer3":
            ReferenceValues.ActiveTimerEdit = 2;
            if (ReferenceValues.SwitchTimerDirection[2] && ReferenceValues.IsTimerRunning[2]) {
                ReferenceValues.IsTimerRunning[2] = false;
                ReferenceValues.IsTimerAlarmActive = false;
                player.Stop();
                Timer3Text = "NONE";
                Timer3Color = "White";
            } else {
                EditTimer editTimer = new();
                editTimer.ShowDialog();
                editTimer.Close();
            }

            break;
        case "timer4":
            ReferenceValues.ActiveTimerEdit = 3;
            if (ReferenceValues.SwitchTimerDirection[3] && ReferenceValues.IsTimerRunning[3]) {
                ReferenceValues.IsTimerRunning[3] = false;
                ReferenceValues.IsTimerAlarmActive = false;
                player.Stop();
                Timer4Text = "NONE";
                Timer4Color = "White";
            } else {
                EditTimer editTimer = new();
                editTimer.ShowDialog();
                editTimer.Close();
            }

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