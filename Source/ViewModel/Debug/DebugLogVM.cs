using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeControl.Source.Json;
using HomeControl.Source.Modules;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Debug;

public class DebugLogVM : BaseViewModel {
    private string _copyrightText;
    private ObservableCollection<DebugTextBlock> _debugList;

    public DebugLogVM() {
        DebugList = ReferenceValues.JsonDebugMaster.DebugBlockList;

        CopyrightText = ReferenceValues.COPYRIGHT + "  v" + ReferenceValues.VERSION_MAJOR + "." + ReferenceValues.VERSION_MINOR + "." + ReferenceValues.VERSION_PATCH + "-" +
                        ReferenceValues.VERSION_BRANCH;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "settings":
            Settings settings = new();
            settings.ShowDialog();
            settings.Close();
            break;
        }
    }

    #region Fields

    public ObservableCollection<DebugTextBlock> DebugList {
        get => _debugList;
        set {
            _debugList = value;
            RaisePropertyChangedEvent("DebugList");
        }
    }

    public string CopyrightText {
        get => _copyrightText;
        set {
            _copyrightText = value;
            RaisePropertyChangedEvent("CopyrightText");
        }
    }

    #endregion
}