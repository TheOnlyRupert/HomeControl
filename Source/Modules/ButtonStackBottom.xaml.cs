using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class ButtonStackBottom {
    public ButtonStackBottom() {
        InitializeComponent();
        DataContext = new ButtonStackBottomVM();
    }
}