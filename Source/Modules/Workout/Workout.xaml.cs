using HomeControl.Source.ViewModel.Workout;

namespace HomeControl.Source.Modules.Workout;

public partial class Workout {
    public Workout() {
        InitializeComponent();
        DataContext = new WorkoutVM();
    }
}