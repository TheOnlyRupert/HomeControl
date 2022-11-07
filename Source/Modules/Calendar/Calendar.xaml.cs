using HomeControl.Source.ViewModel.Calendar;

namespace HomeControl.Source.Modules.Calendar; 

public partial class Calendar {
    public Calendar() {
        InitializeComponent();
        DataContext = new CalendarVM();
    }
}