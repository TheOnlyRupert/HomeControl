using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class ButtonStack {
    public ButtonStack() {
        InitializeComponent();
        DataContext = new ButtonStackVM();
    }
}