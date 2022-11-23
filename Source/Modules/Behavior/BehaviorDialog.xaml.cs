using HomeControl.Source.ViewModel.Behavior;

namespace HomeControl.Source.Modules.Behavior;

public partial class BehaviorDialog {
    public BehaviorDialog() {
        InitializeComponent();
        DataContext = new BehaviorDialogVM();
    }
}