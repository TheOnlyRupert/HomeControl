using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules.Behavior;

public partial class Behavior {
    public Behavior() {
        InitializeComponent();
        DataContext = new BehaviorVM();
    }
}