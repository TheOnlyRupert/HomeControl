using System;
using System.Windows.Threading;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class FunctionVM : BaseViewModel {
    private string _color1, _color2;
    private bool switchColor;

    public FunctionVM() {
        Color1 = "Transparent";
        Color2 = "Black";

        DispatcherTimer dispatcherTimer = new();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e) {
        if (switchColor) {
            Color1 = "Transparent";
            Color2 = "Black";
            switchColor = false;
        } else {
            Color1 = "Black";
            Color2 = "Transparent";
            switchColor = true;
        }
    }

    #region Fields

    public string Color1 {
        get => _color1;
        set {
            _color1 = value;
            RaisePropertyChangedEvent("Color1");
        }
    }

    public string Color2 {
        get => _color2;
        set {
            _color2 = value;
            RaisePropertyChangedEvent("Color2");
        }
    }

    #endregion
}