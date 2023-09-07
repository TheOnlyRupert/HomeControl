using System;
using System.Windows.Input;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Timer;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Timer;

public class TimerVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;

    private string _timer1Text, _timer2Text, _timer3Text, _timer4Text, _timer1Color, _timer2Color, _timer3Color, _timer4Color;

    public TimerVM() {
        ReferenceValues.JsonTimerMaster = new JsonTimer();

        RefreshTimer();

        simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "RefreshTimer") {
            RefreshTimer();
        }
    }

    private void RefreshTimer() {
        TimeSpan time = TimeSpan.FromSeconds(ReferenceValues.JsonTimerMaster.Timer1Seconds);
        Timer1Text = (time < TimeSpan.Zero ? "-" : "") + time.ToString(@"hh\:mm\:ss");
        Timer1Color = ReferenceValues.JsonTimerMaster.Timer1Seconds < 0 ? "Red" : "Green";

        if (!ReferenceValues.JsonTimerMaster.IsTimer1Running) {
            Timer1Color = "White";
        }

        time = TimeSpan.FromSeconds(ReferenceValues.JsonTimerMaster.Timer2Seconds);
        Timer2Text = (time < TimeSpan.Zero ? "-" : "") + time.ToString(@"hh\:mm\:ss");
        Timer2Color = ReferenceValues.JsonTimerMaster.Timer2Seconds < 0 ? "Red" : "Green";

        if (!ReferenceValues.JsonTimerMaster.IsTimer2Running) {
            Timer2Color = "White";
        }

        time = TimeSpan.FromSeconds(ReferenceValues.JsonTimerMaster.Timer3Seconds);
        Timer3Text = (time < TimeSpan.Zero ? "-" : "") + time.ToString(@"hh\:mm\:ss");
        Timer3Color = ReferenceValues.JsonTimerMaster.Timer3Seconds < 0 ? "Red" : "Green";

        if (!ReferenceValues.JsonTimerMaster.IsTimer3Running) {
            Timer3Color = "White";
        }

        time = TimeSpan.FromSeconds(ReferenceValues.JsonTimerMaster.Timer4Seconds);
        Timer4Text = (time < TimeSpan.Zero ? "-" : "") + time.ToString(@"hh\:mm\:ss");
        Timer4Color = ReferenceValues.JsonTimerMaster.Timer4Seconds < 0 ? "Red" : "Green";

        if (!ReferenceValues.JsonTimerMaster.IsTimer4Running) {
            Timer4Color = "White";
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "timer":
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
            EditTimer editTimer = new();
            editTimer.ShowDialog();
            editTimer.Close();
            simpleMessenger.PushMessage("RefreshTimer", null);
            ReferenceValues.JsonTimerMaster.IsAlarmSounding = false;
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