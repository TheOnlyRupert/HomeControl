using System;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.Modules.Exercise;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Exercise;

public class ExerciseVM : BaseViewModel {
    public ExerciseVM() {
        try {
            ReferenceValues.JsonExerciseMaster = JsonSerializer.Deserialize<JsonExercise>(FileHelpers.LoadFileText("exercise", true));
        } catch (Exception) {
            ReferenceValues.JsonExerciseMaster = new JsonExercise();

            FileHelpers.SaveFileText("exercise", JsonSerializer.Serialize(ReferenceValues.JsonTasksMaster), true);
        }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "edit":
            EditExercise editExercise = new();
            editExercise.ShowDialog();
            editExercise.Close();
            break;
        }
    }
}