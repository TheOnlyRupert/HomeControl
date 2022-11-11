using HomeControl.Source.ViewModel.Calendar;

namespace HomeControl.Source.Modules.Calendar;

public partial class CalendarEvent {
    public CalendarEvent() {
        InitializeComponent();
        DataContext = new CalendarEventVM();
    }
}