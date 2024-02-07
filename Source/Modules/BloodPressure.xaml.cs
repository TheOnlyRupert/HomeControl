using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class BloodPressure {
    public BloodPressure() {
        InitializeComponent();
        DataContext = new BloodPressureVM();
    }
}