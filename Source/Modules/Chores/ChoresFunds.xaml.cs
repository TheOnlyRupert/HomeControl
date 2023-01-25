using System.Windows;
using HomeControl.Source.ViewModel.Chores;

namespace HomeControl.Source.Modules.Chores;

public partial class ChoresFunds {
    public ChoresFunds() {
        InitializeComponent();
        DataContext = new ChoresFundsVM();
    }

    private void CustomListLoaded(object sender, RoutedEventArgs e) {
        if (CustomListView.Items.Count > 0) {
            CustomListView.ScrollIntoView(CustomListView.Items[CustomListView.Items.Count - 1]);
        }
    }
}