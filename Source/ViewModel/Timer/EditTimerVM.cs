using System.Windows.Input;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Timer;

public class EditTimerVM : BaseViewModel {
    private string _mainTimerOutput, _timerNumberText, _timerNumberColor;

    public EditTimerVM() {
        MainTimerOutput = $"{ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit]:000}:{ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit]:00}";
        TimerNumberText = "Timer " + ReferenceValues.ActiveTimerEdit;
        TimerNumberColor = ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] ? "Red" : "YellowGreen";

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "Refresh") {
            if (ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                MainTimerOutput = $"{ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit]:000}:{ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit]:00}";
                TimerNumberColor = ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] ? "Red" : "YellowGreen";
            }
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "timerSecAdd":
            if (!ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit]++;
                ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] = false;
                if (ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit] > 59) {
                    ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit] = 0;
                }
            }

            break;
        case "timerMinAdd":
            if (!ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit]++;
                ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] = false;
            }

            break;
        case "timerMinAdd15":
            if (!ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit] += 15;
                ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] = false;
            }

            break;
        case "timerSecSub":
            if (!ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit]--;
                if (ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit] < 0) {
                    ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit] = 59;
                }

                ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] = false;
            }

            break;
        case "timerMinSub":
            if (!ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit]--;
                if (ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit] < 0) {
                    ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit] = 0;
                }

                ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] = false;
            }

            break;
        case "timerMinSub15":
            if (!ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit] -= 15;
                if (ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit] < 0) {
                    ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit] = 0;
                }

                ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] = false;
            }

            break;
        case "timerPlayPause":
            if (ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit] = false;
            } else {
                ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit] = true;
            }

            ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] = false;
            ReferenceValues.IsTimerAlarmActive = false;
            //ReferenceValues.TimerSound.Stop();
            TimerNumberColor = "YellowGreen";

            break;
        case "timerStop":
            if (ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit] = false;
            }

            ReferenceValues.SwitchTimerDirection[ReferenceValues.ActiveTimerEdit] = false;
            ReferenceValues.IsTimerAlarmActive = false;
            ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit] = 0;
            ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit] = 0;
            //ReferenceValues.TimerSound.Stop();
            TimerNumberColor = "YellowGreen";

            break;
        case "button1":
            if (!ReferenceValues.IsTimerRunning[ReferenceValues.ActiveTimerEdit]) {
                ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit] = 18;
                ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit] = 0;
            }

            break;
        }

        MainTimerOutput = $"{ReferenceValues.TimerMinutes[ReferenceValues.ActiveTimerEdit]:000}:{ReferenceValues.TimerSeconds[ReferenceValues.ActiveTimerEdit]:00}";
    }


    #region Fields

    public string MainTimerOutput {
        get => _mainTimerOutput;
        set {
            _mainTimerOutput = value;
            RaisePropertyChangedEvent("MainTimerOutput");
        }
    }

    public string TimerNumberText {
        get => _timerNumberText;
        set {
            _timerNumberText = value;
            RaisePropertyChangedEvent("TimerNumberText");
        }
    }

    public string TimerNumberColor {
        get => _timerNumberColor;
        set {
            _timerNumberColor = value;
            RaisePropertyChangedEvent("TimerNumberColor");
        }
    }

    #endregion
}