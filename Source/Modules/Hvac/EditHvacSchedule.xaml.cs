using System.Windows;
using HomeControl.Source.ViewModel.Hvac;

namespace HomeControl.Source.Modules.Hvac;

public partial class EditHvacSchedule : Window {
    public EditHvacSchedule() {
        InitializeComponent();
        DataContext = new EditHvacScheduleVM();
    }
}