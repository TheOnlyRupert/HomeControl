using System.Windows.Controls;
using HomeControl.Source.ViewModel.Security;

namespace HomeControl.Source.Modules.Security;

public partial class Security : Page {
    public Security() {
        InitializeComponent();
        DataContext = new SecurityVM();
    }
}