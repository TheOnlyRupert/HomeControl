using HomeControl.Source.ViewModel.Behavior;

namespace HomeControl.Source.Modules.Behavior;

public partial class EditBehavior {
    public EditBehavior() {
        InitializeComponent();
        DataContext = new EditBehaviorVM();
    }
}