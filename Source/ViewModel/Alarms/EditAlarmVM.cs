using System.Windows.Input;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Alarms;

public class EditAlarmVM : BaseViewModel {
    private string _mainTimerOutput, _alarmNumberText, _editTextBox, _mainTimerOutputVisibility, _editTextBoxVisibility;

    public EditAlarmVM() {
        MainTimerOutput = "00:00";
        AlarmNumberText = "Alarm 1";
        MainTimerOutputVisibility = "HIDDEN";
        EditTextBoxVisibility = "VISIBLE";
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    public string MainTimerOutput {
        get => _mainTimerOutput;
        set {
            _mainTimerOutput = value;
            RaisePropertyChangedEvent("MainTimerOutput");
        }
    }

    public string AlarmNumberText {
        get => _alarmNumberText;
        set {
            _alarmNumberText = value;
            RaisePropertyChangedEvent("AlarmNumberText");
        }
    }

    public string EditTextBox {
        get => _editTextBox;
        set {
            _editTextBox = value;
            RaisePropertyChangedEvent("EditTextBox");
        }
    }

    public string MainTimerOutputVisibility {
        get => _mainTimerOutputVisibility;
        set {
            _mainTimerOutputVisibility = value;
            RaisePropertyChangedEvent("MainTimerOutputVisibility");
        }
    }

    public string EditTextBoxVisibility {
        get => _editTextBoxVisibility;
        set {
            _editTextBoxVisibility = value;
            RaisePropertyChangedEvent("EditTextBoxVisibility");
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "timerAdd":
            break;
        case "timerSub":
            break;
        case "timerPlayPause":
            break;
        case "timerStop":
            break;
        }
    }
}