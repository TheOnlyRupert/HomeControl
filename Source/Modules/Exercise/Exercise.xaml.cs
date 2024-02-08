using HomeControl.Source.ViewModel.Exercise;

namespace HomeControl.Source.Modules.Exercise;

public partial class Exercise {
    public Exercise() {
        InitializeComponent();
        DataContext = new ExerciseVM();
    }
}