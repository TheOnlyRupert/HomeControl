using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class DebugLog {
    public DebugLog() {
        InitializeComponent();
        DataContext = new DebugLogVM();
    }
}