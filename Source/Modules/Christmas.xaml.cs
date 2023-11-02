using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Christmas {
    public Christmas() {
        InitializeComponent();
        DataContext = new ChristmasVM();
    }
}