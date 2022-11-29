using System.Windows.Input;
using HomeControl.Source.Modules.Alarms;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Alarms;

public class AlarmsVM : BaseViewModel {
    private string _timer1Text, _timer2Text, _timer3Text, _timer4Text;
    private bool timerRunning;

    public AlarmsVM() {
        Timer1Text = "NONE";
        Timer2Text = "NONE";
        Timer3Text = "NONE";
        Timer4Text = "NONE";
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "timer1":
            break;
        case "timer2":
            break;
        case "timer3":
            break;
        case "timer4":
            break;
        }

        EditAlarm editAlarm = new();
        editAlarm.ShowDialog();
        editAlarm.Close();
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

    #endregion
}