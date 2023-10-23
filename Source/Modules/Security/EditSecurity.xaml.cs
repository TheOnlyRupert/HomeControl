using HomeControl.Source.ViewModel.Security;

namespace HomeControl.Source.Modules.Security;

public partial class EditSecurity {
    public EditSecurity() {
        InitializeComponent();
        DataContext = new EditSecurityVM();
    }
}