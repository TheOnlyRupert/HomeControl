using HomeControl.Source.ViewModel.Hvac;

namespace HomeControl.Source.Modules.Hvac;

public partial class Hvac {
    public Hvac() {
        InitializeComponent();
        DataContext = new HvacVM();
    }
}