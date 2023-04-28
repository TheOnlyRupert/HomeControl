using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class DebugLogVM : BaseViewModel {
    private string _textOutput;

    public DebugLogVM() {
        TextOutput = ReferenceValues.DebugText;
    }

    #region Fields

    public string TextOutput {
        get => _textOutput;
        set {
            _textOutput = value;
            RaisePropertyChangedEvent("TextOutput");
        }
    }

    #endregion
}