using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeControl.Source.Json;
using HomeControl.Source.Modules;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class DebugLogVM : BaseViewModel {
    // ReSharper disable UnusedMember.Global
    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    private static void ButtonCommandLogic(object param) {
        if (param?.ToString() == "settings") {
            Settings settings = new();
            settings.ShowDialog();
            settings.Close();
        }
    }

    public ObservableCollection<DebugTextBlock> DebugList { get; set; } = ReferenceValues.JsonDebugMaster.DebugBlockList;
    public string CopyrightText { get; set; } = $"{ReferenceValues.Copyright} v{ReferenceValues.VersionMajor}.{ReferenceValues.VersionMinor}.{ReferenceValues.VersionPatch}-{ReferenceValues.VersionBranch}";
}