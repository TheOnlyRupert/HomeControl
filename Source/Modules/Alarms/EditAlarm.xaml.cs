using System.Windows;
using HomeControl.Source.ViewModel.Alarms;

namespace HomeControl.Source.Modules.Alarms;

public partial class EditAlarm : Window {
    public EditAlarm() {
        InitializeComponent();
        DataContext = new EditAlarmVM();
    }
}