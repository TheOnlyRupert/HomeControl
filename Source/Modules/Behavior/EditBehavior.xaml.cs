using System.Windows;
using HomeControl.Source.ViewModel.Behavior;

namespace HomeControl.Source.Modules.Behavior;

public partial class EditBehavior {
    public EditBehavior() {
        InitializeComponent();
        DataContext = new EditBehaviorVM();
    }

    private void CustomListLoaded(object sender, RoutedEventArgs e) {
        if (CustomListView.Items.Count > 0) {
            CustomListView.ScrollIntoView(CustomListView.Items[CustomListView.Items.Count - 1]);
        }
    }
}