using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Behavior {
    public Behavior() {
        InitializeComponent();
        DataContext = new BehaviorVM();
    }
}