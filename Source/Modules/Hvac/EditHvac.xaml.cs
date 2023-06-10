using System.Windows;
using HomeControl.Source.ViewModel.Hvac;

namespace HomeControl.Source.Modules.Hvac;

public partial class EditHvac : Window {
    public EditHvac() {
        InitializeComponent();
        DataContext = new EditHvacVM();
    }
}