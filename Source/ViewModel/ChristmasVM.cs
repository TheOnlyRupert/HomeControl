using System;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class ChristmasVM : BaseViewModel {
    private string _christmasVisibility, _countdown;

    public ChristmasVM() {
        UpdateVisibility();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "HourChanged") {
            UpdateVisibility();
        }
    }

    private void UpdateVisibility() {
        DateTime dateTime = DateTime.Now;
        if (dateTime.Month == 11) {
            ChristmasVisibility = "VISIBLE";
            UpdateCountdown();
        } else if (dateTime.Month == 12) {
            if (dateTime.Day < 26) {
                ChristmasVisibility = "VISIBLE";
                UpdateCountdown();
            }
        } else {
            ChristmasVisibility = "HIDDEN";
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

    #region Fields

    public string ChristmasVisibility {
        get => _christmasVisibility;
        set {
            _christmasVisibility = value;
            RaisePropertyChangedEvent("ChristmasVisibility");
        }
    }

    public string Countdown {
        get => _countdown;
        set {
            _countdown = value;
            RaisePropertyChangedEvent("Countdown");
        }
    }

    #endregion
}