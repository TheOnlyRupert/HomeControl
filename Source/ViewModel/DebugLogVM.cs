using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class DebugLogVM : BaseViewModel {
    private ObservableCollection<DebugTextBlock> _debugList;

    public DebugLogVM() {
        DebugList = ReferenceValues.DebugTextBlockOutput;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    #region Fields

    public ObservableCollection<DebugTextBlock> DebugList {
        get => _debugList;
        set {
            _debugList = value;
            RaisePropertyChangedEvent("DebugList");
        }
    }

    #endregion

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "settings":
            Settings settings = new();
            settings.ShowDialog();
            settings.Close();
            break;
        }
    }
}