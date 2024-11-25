using HomeControl.Source.ViewModel.Fitness;

namespace HomeControl.Source.Modules.Fitness;

public partial class EditExercise {
    public EditExercise() {
        InitializeComponent();
        DataContext = new EditExerciseVm();
    }
}