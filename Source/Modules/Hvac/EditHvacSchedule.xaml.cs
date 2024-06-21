using System.Windows;
using HomeControl.Source.ViewModel.Hvac;

namespace HomeControl.Source.Modules.Hvac;

public partial class EditHvacSchedule : Window {
    public EditHvacSchedule() {
        InitializeComponent();
        DataContext = new EditHvacScheduleVM();
    }

    private void CustomListLoaded(object sender, RoutedEventArgs e) {
        if (CustomListView.Items.Count > 0) {
            CustomListView.ScrollIntoView(CustomListView.Items[CustomListView.Items.Count - 1]);
        }
    }
}