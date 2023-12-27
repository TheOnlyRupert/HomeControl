using System;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class ChristmasVM : BaseViewModel {
    private string _countdown;

    public ChristmasVM() {
        UpdateCountdown();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    #region Fields

    public string Countdown {
        get => _countdown;
        set {
            _countdown = value;
            RaisePropertyChangedEvent("Countdown");
        }
    }

    #endregion

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            UpdateCountdown();
        }
    }

    private void UpdateCountdown() {
        Countdown = (new DateTime(DateTime.Now.Year, 12, 25) - DateTime.Now.Date).TotalDays + " DAYS";

        if (Countdown == "1 DAYS") {
            Countdown = "TOMORROW";
        }

        if (Countdown == "0 DAYS") {
            Countdown = "TODAY";
        }
    }
}