using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;
using EditDiet = HomeControl.Source.Modules.Fitness.EditDiet;
using EditExercise = HomeControl.Source.Modules.Fitness.EditExercise;

namespace HomeControl.Source.ViewModel.Fitness;

public class FitnessVM : BaseViewModel {
    public FitnessVM() {
        /* Do this here so I don't get null references in the future */
        ReferenceValues.JsonFitnessMaster = new JsonFitness {
            FitnessTracker = new ObservableCollection<FitnessTracker>(),
            Exercises = new ObservableCollection<Exercises>(),
            ExerciseListTable = new ObservableCollection<ExerciseListTable>(),
            Meals = new ObservableCollection<Meals>(),
            MealListTable = new ObservableCollection<MealListTable>()
        };
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "editExercises":
            EditExercise editExercise = new();
            editExercise.ShowDialog();
            editExercise.Close();
            break;
        case "editDiet":
            EditDiet editDiet = new();
            editDiet.ShowDialog();
            editDiet.Close();
            break;
        }
    }
}