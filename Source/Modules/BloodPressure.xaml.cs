using System.Windows;
using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class BloodPressure {
    public BloodPressure() {
        InitializeComponent();
        DataContext = new BloodPressureVM();
    }

    private void CustomListLoaded(object sender, RoutedEventArgs e) {
        if (CustomListView.Items.Count > 0) {
            CustomListView.ScrollIntoView(CustomListView.Items[CustomListView.Items.Count - 1]);
        }
    }
}