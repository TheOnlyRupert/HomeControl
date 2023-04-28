using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Password {
    public Password() {
        InitializeComponent();
        DataContext = new PasswordVM();
    }
}