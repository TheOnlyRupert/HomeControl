using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class ButtonStackTop {
    public ButtonStackTop() {
        InitializeComponent();
        DataContext = new ButtonStackTopVM();
    }
}