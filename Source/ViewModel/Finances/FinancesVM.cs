using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Finances;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class FinancesVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;
    private string _textBlock1, _textBlock2, _textBlock3, _textBlock4, _textBlock5, _textBlock6, _textBlock7, _textBlock8;

    public FinancesVM() {
        try {
            ReferenceValues.JsonFinanceMaster = JsonSerializer.Deserialize<JsonFinances>(FileHelpers.LoadFileText("finances", true));
        } catch (Exception) {
            ReferenceValues.JsonFinanceMaster = new JsonFinances {
                financeList = new ObservableCollection<FinanceBlock>()
            };

            FileHelpers.SaveFileText("finances", JsonSerializer.Serialize(ReferenceValues.JsonFinanceMaster), true);
        }

        TextBlock1 = ReferenceValues.JsonSettingsMaster.FinanceBlock1;
        TextBlock2 = ReferenceValues.JsonSettingsMaster.FinanceBlock2;
        TextBlock3 = ReferenceValues.JsonSettingsMaster.FinanceBlock3;
        TextBlock4 = ReferenceValues.JsonSettingsMaster.FinanceBlock4;
        TextBlock5 = ReferenceValues.JsonSettingsMaster.FinanceBlock5;
        TextBlock6 = ReferenceValues.JsonSettingsMaster.FinanceBlock6;
        TextBlock7 = ReferenceValues.JsonSettingsMaster.FinanceBlock7;
        TextBlock8 = ReferenceValues.JsonSettingsMaster.FinanceBlock8;

        simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        if (!ReferenceValues.LockUI) {
            switch (param) {
            case "edit":
                EditFinances editFinances = new();
                editFinances.ShowDialog();
                editFinances.Close();

                simpleMessenger.PushMessage("RefreshFinances", null);
                break;
            }
        } else {
            ReferenceValues.SoundToPlay = "locked";
            SoundDispatcher.PlaySound();
        }
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "RefreshFinances":
            break;
        }
    }

    #region Fields

    public string TextBlock1 {
        get => _textBlock1;
        set {
            _textBlock1 = value;
            RaisePropertyChangedEvent("TextBlock1");
        }
    }

    public string TextBlock2 {
        get => _textBlock2;
        set {
            _textBlock2 = value;
            RaisePropertyChangedEvent("TextBlock2");
        }
    }

    public string TextBlock3 {
        get => _textBlock3;
        set {
            _textBlock3 = value;
            RaisePropertyChangedEvent("TextBlock3");
        }
    }

    public string TextBlock4 {
        get => _textBlock4;
        set {
            _textBlock4 = value;
            RaisePropertyChangedEvent("TextBlock4");
        }
    }

    public string TextBlock5 {
        get => _textBlock5;
        set {
            _textBlock5 = value;
            RaisePropertyChangedEvent("TextBlock5");
        }
    }

    public string TextBlock6 {
        get => _textBlock6;
        set {
            _textBlock6 = value;
            RaisePropertyChangedEvent("TextBlock6");
        }
    }

    public string TextBlock7 {
        get => _textBlock7;
        set {
            _textBlock7 = value;
            RaisePropertyChangedEvent("TextBlock7");
        }
    }

    public string TextBlock8 {
        get => _textBlock8;
        set {
            _textBlock8 = value;
            RaisePropertyChangedEvent("TextBlock8");
        }
    }

    #endregion
}