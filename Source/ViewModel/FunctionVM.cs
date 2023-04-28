using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class FunctionVM : BaseViewModel {
    private string _color1, _color2;
    private bool switchColor;

    public FunctionVM() {
        Color1 = "Transparent";
        Color2 = "Black";

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "Refresh") {
            /*  Function Module */
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