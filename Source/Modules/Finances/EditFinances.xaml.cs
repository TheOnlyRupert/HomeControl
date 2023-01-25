using System.Windows;
using HomeControl.Source.ViewModel.Finances;

namespace HomeControl.Source.Modules.Finances;

public partial class EditFinances {
    public EditFinances() {
        InitializeComponent();
        DataContext = new EditFinancesVM();
    }

    private void CustomListLoaded(object sender, RoutedEventArgs e) {
        if (CustomListView.Items.Count > 0) {
            CustomListView.ScrollIntoView(CustomListView.Items[CustomListView.Items.Count - 1]);
        }
    }
}