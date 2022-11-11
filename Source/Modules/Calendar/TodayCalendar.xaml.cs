using HomeControl.Source.ViewModel.Calendar;

namespace HomeControl.Source.Modules.Calendar;

public partial class TodayCalendar {
    public TodayCalendar() {
        InitializeComponent();
        DataContext = new TodayCalendarVM();
    }
}