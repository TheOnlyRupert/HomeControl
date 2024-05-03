using System.Globalization;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Finances;

public class FinancesVM : BaseViewModel {
    private readonly CrossViewMessenger simpleMessenger;

    private double _progressTotal, _progressBlock1, _progressBlock2, _progressBlock3, _progressBlock4, _progressBlock5, _progressBlock6, _progressBlock7, _progressBlock8, _progressBlock9;

    private string _textBlock1, _textBlock2, _textBlock3, _textBlock4, _textBlock5, _textBlock6, _textBlock7, _textBlock8, _textBlock9, _progressTotalText, _progressBlockText1, _progressBlockText2,
        _progressBlockText3, _progressBlockText4, _progressBlockText5, _progressBlockText6, _progressBlockText7, _progressBlockText8, _progressBlockText9, _progressTotalColor;

    public FinancesVM() {
        TextBlock1 = ReferenceValues.JsonSettingsMaster.FinanceBlock1;
        TextBlock2 = ReferenceValues.JsonSettingsMaster.FinanceBlock2;
        TextBlock3 = ReferenceValues.JsonSettingsMaster.FinanceBlock3;
        TextBlock4 = ReferenceValues.JsonSettingsMaster.FinanceBlock4;
        TextBlock5 = ReferenceValues.JsonSettingsMaster.FinanceBlock5;
        TextBlock6 = ReferenceValues.JsonSettingsMaster.FinanceBlock6;
        TextBlock7 = ReferenceValues.JsonSettingsMaster.FinanceBlock7;
        TextBlock8 = ReferenceValues.JsonSettingsMaster.FinanceBlock8;
        TextBlock9 = ReferenceValues.JsonSettingsMaster.FinanceBlock9;

        RefreshFinanceView();

        simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "RefreshFinances":
            RefreshFinanceView();

            break;
        }
    }

    private void RefreshFinanceView() {
        ProgressBlock1 = ReferenceValues.JsonFinanceMaster.Category1Percentage;
        ProgressBlockText1 = "$" + ReferenceValues.JsonFinanceMaster.Category1Total;
        ProgressBlock2 = ReferenceValues.JsonFinanceMaster.Category2Percentage;
        ProgressBlockText2 = "$" + ReferenceValues.JsonFinanceMaster.Category2Total;
        ProgressBlock3 = ReferenceValues.JsonFinanceMaster.Category3Percentage;
        ProgressBlockText3 = "$" + ReferenceValues.JsonFinanceMaster.Category3Total;
        ProgressBlock4 = ReferenceValues.JsonFinanceMaster.Category4Percentage;
        ProgressBlockText4 = "$" + ReferenceValues.JsonFinanceMaster.Category4Total;
        ProgressBlock5 = ReferenceValues.JsonFinanceMaster.Category5Percentage;
        ProgressBlockText5 = "$" + ReferenceValues.JsonFinanceMaster.Category5Total;
        ProgressBlock6 = ReferenceValues.JsonFinanceMaster.Category6Percentage;
        ProgressBlockText6 = "$" + ReferenceValues.JsonFinanceMaster.Category6Total;
        ProgressBlock7 = ReferenceValues.JsonFinanceMaster.Category7Percentage;
        ProgressBlockText7 = "$" + ReferenceValues.JsonFinanceMaster.Category7Total;
        ProgressBlock8 = ReferenceValues.JsonFinanceMaster.Category8Percentage;
        ProgressBlockText8 = "$" + ReferenceValues.JsonFinanceMaster.Category8Total;
        ProgressBlock9 = ReferenceValues.JsonFinanceMaster.Category9Percentage;
        ProgressBlockText9 = "$" + ReferenceValues.JsonFinanceMaster.Category9Total;

        ProgressTotalColor = ProgressTotal switch {
            <= 75 => "CornflowerBlue",
            > 75 and < 85 => "Orange",
            _ => "Red"
        };

        ProgressTotalText = "Remaining:  $" + ReferenceValues.JsonFinanceMaster.TotalAmount;
        ProgressTotal = ReferenceValues.JsonFinanceMaster.TotalPercentage;

        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        culture.NumberFormat.CurrencyNegativePattern = 1;
        string amount = string.Format(culture, "{0:C}", ReferenceValues.JsonFinanceMaster.TotalAmount);
        ProgressTotalText = "Remaining:  " + amount;
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

    public string TextBlock9 {
        get => _textBlock9;
        set {
            _textBlock9 = value;
            RaisePropertyChangedEvent("TextBlock9");
        }
    }

    public double ProgressTotal {
        get => _progressTotal;
        set {
            _progressTotal = value;
            RaisePropertyChangedEvent("ProgressTotal");
        }
    }

    public string ProgressTotalText {
        get => _progressTotalText;
        set {
            _progressTotalText = value;
            RaisePropertyChangedEvent("ProgressTotalText");
        }
    }

    public double ProgressBlock1 {
        get => _progressBlock1;
        set {
            _progressBlock1 = value;
            RaisePropertyChangedEvent("ProgressBlock1");
        }
    }

    public string ProgressBlockText1 {
        get => _progressBlockText1;
        set {
            _progressBlockText1 = value;
            RaisePropertyChangedEvent("ProgressBlockText1");
        }
    }

    public double ProgressBlock2 {
        get => _progressBlock2;
        set {
            _progressBlock2 = value;
            RaisePropertyChangedEvent("ProgressBlock2");
        }
    }

    public string ProgressBlockText2 {
        get => _progressBlockText2;
        set {
            _progressBlockText2 = value;
            RaisePropertyChangedEvent("ProgressBlockText2");
        }
    }

    public double ProgressBlock3 {
        get => _progressBlock3;
        set {
            _progressBlock3 = value;
            RaisePropertyChangedEvent("ProgressBlock3");
        }
    }

    public string ProgressBlockText3 {
        get => _progressBlockText3;
        set {
            _progressBlockText3 = value;
            RaisePropertyChangedEvent("ProgressBlockText3");
        }
    }

    public double ProgressBlock4 {
        get => _progressBlock4;
        set {
            _progressBlock4 = value;
            RaisePropertyChangedEvent("ProgressBlock4");
        }
    }

    public string ProgressBlockText4 {
        get => _progressBlockText4;
        set {
            _progressBlockText4 = value;
            RaisePropertyChangedEvent("ProgressBlockText4");
        }
    }

    public double ProgressBlock5 {
        get => _progressBlock5;
        set {
            _progressBlock5 = value;
            RaisePropertyChangedEvent("ProgressBlock5");
        }
    }

    public string ProgressBlockText5 {
        get => _progressBlockText5;
        set {
            _progressBlockText5 = value;
            RaisePropertyChangedEvent("ProgressBlockText5");
        }
    }

    public double ProgressBlock6 {
        get => _progressBlock6;
        set {
            _progressBlock6 = value;
            RaisePropertyChangedEvent("ProgressBlock6");
        }
    }

    public string ProgressBlockText6 {
        get => _progressBlockText6;
        set {
            _progressBlockText6 = value;
            RaisePropertyChangedEvent("ProgressBlockText6");
        }
    }

    public double ProgressBlock7 {
        get => _progressBlock7;
        set {
            _progressBlock7 = value;
            RaisePropertyChangedEvent("ProgressBlock7");
        }
    }

    public string ProgressBlockText7 {
        get => _progressBlockText7;
        set {
            _progressBlockText7 = value;
            RaisePropertyChangedEvent("ProgressBlockText7");
        }
    }

    public double ProgressBlock8 {
        get => _progressBlock8;
        set {
            _progressBlock8 = value;
            RaisePropertyChangedEvent("ProgressBlock8");
        }
    }

    public string ProgressBlockText8 {
        get => _progressBlockText8;
        set {
            _progressBlockText8 = value;
            RaisePropertyChangedEvent("ProgressBlockText8");
        }
    }

    public double ProgressBlock9 {
        get => _progressBlock9;
        set {
            _progressBlock9 = value;
            RaisePropertyChangedEvent("ProgressBlock9");
        }
    }

    public string ProgressBlockText9 {
        get => _progressBlockText9;
        set {
            _progressBlockText9 = value;
            RaisePropertyChangedEvent("ProgressBlockText9");
        }
    }

    public string ProgressTotalColor {
        get => _progressTotalColor;
        set {
            _progressTotalColor = value;
            RaisePropertyChangedEvent("ProgressTotalColor");
        }
    }

    #endregion
}