using HomeControl.Source.ViewModel.Alarms;

namespace HomeControl.Source.Modules.Alarms;

public partial class Alarms {
    public Alarms() {
        InitializeComponent();
        DataContext = new AlarmsVM();
    }
}