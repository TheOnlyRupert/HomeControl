using HomeControl.Source.ViewModel.Exercise;

namespace HomeControl.Source.Modules.Exercise;

public partial class EditExercise {
    public EditExercise() {
        InitializeComponent();
        DataContext = new EditExerciseVM();
    }
}