using HomeControl.Source.ViewModel;

namespace HomeControl.Source.Modules;

public partial class Function {
    public Function() {
        InitializeComponent();
        DataContext = new FunctionVM();
    }
}