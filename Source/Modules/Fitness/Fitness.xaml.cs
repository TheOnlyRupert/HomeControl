using HomeControl.Source.ViewModel.Fitness;

namespace HomeControl.Source.Modules.Fitness;

public partial class Fitness {
    public Fitness() {
        InitializeComponent();
        DataContext = new FitnessVm();
    }
}