using System.Windows.Input;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;
using EditDiet = HomeControl.Source.Modules.Fitness.EditDiet;
using EditExercise = HomeControl.Source.Modules.Fitness.EditExercise;

namespace HomeControl.Source.ViewModel.Fitness;

public class FitnessVm : BaseViewModel {
    public FitnessVm() {
        /* Do this here so I don't get null references in the future */
        ReferenceValues.JsonFitnessMaster = new JsonFitness {
            FitnessTracker = [],
            Exercises = [],
            ExerciseListTable = [],
            Meals = [],
            MealListTable = []
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