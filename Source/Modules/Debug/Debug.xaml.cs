using HomeControl.Source.ViewModel.Debug;

namespace HomeControl.Source.Modules.Debug;

public partial class Debug {
    public Debug() {
        InitializeComponent();
        DataContext = new DebugVM();
    }
}