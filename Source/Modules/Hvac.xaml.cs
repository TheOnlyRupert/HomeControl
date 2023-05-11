using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Hvac {
    public Hvac() {
        InitializeComponent();
        DataContext = new HvacVM();
    }
}