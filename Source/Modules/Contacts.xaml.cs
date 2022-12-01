using System.Windows;
using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Contacts : Window {
    public Contacts() {
        InitializeComponent();
        DataContext = new ContactsVM();
    }
}