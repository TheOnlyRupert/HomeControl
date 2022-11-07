using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules; 

public partial class Alarms {
    public Alarms() {
        InitializeComponent();
        DataContext = new AlarmsVM();
    }
}