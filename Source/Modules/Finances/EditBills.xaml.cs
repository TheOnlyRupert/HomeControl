using System.Windows;
using HomeControl.Source.ViewModel.Finances;

namespace HomeControl.Source.Modules.Finances;

public partial class EditBills {
    public EditBills() {
        InitializeComponent();
        DataContext = new EditBillsVM();
    }

    private void CustomListLoaded(object sender, RoutedEventArgs e) {
        if (CustomListView.Items.Count > 0) {
            CustomListView.ScrollIntoView(CustomListView.Items[CustomListView.Items.Count - 1]);
        }
    }
}