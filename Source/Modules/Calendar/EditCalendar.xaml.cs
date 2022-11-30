using HomeControl.Source.ViewModel.Calendar;

namespace HomeControl.Source.Modules.Calendar;

public partial class EditCalendar {
    public EditCalendar() {
        InitializeComponent();
        DataContext = new EditCalendarVM();
    }
}