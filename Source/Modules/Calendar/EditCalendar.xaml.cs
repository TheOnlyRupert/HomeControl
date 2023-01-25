using System.Windows;
using HomeControl.Source.ViewModel.Calendar;

namespace HomeControl.Source.Modules.Calendar;

public partial class EditCalendar {
    public EditCalendar() {
        InitializeComponent();
        DataContext = new EditCalendarVM();
    }

    private void CustomListLoaded(object sender, RoutedEventArgs e) {
        if (CustomListView.Items.Count > 0) {
            CustomListView.ScrollIntoView(CustomListView.Items[CustomListView.Items.Count - 1]);
        }
    }
}