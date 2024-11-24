using HomeControl.Source.ViewModel.Exercise;

namespace HomeControl.Source.Modules.Fitness;

public partial class EditExercise {
    public EditExercise() {
        InitializeComponent();
        DataContext = new EditExerciseVM();
    }
}