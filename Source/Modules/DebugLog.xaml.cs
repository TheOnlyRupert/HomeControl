using System.Windows;
using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules.Debug;

public partial class DebugLog {
    public DebugLog() {
        InitializeComponent();
        DataContext = new DebugLogVM();
    }

    private void CustomListLoaded(object sender, RoutedEventArgs e) {
        if (CustomListView.Items.Count > 0) {
            CustomListView.ScrollIntoView(CustomListView.Items[CustomListView.Items.Count - 1]);
        }
    }
}