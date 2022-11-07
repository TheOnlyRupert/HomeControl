using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules; 

public partial class Settings {
    public Settings() {
        InitializeComponent();
        DataContext = new SettingsVM();
    }
}