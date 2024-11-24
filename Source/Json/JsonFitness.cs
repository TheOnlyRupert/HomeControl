using System.Collections.ObjectModel;

namespace HomeControl.Source.Json;

public class JsonFitness {
    public ObservableCollection<FitnessTracker> FitnessTracker { get; set; }
    public ObservableCollection<Exercises> Exercises { get; set; }
    public ObservableCollection<ExerciseListTable> ExerciseListTable { get; set; }
    public ObservableCollection<MealListTable> MealListTable { get; set; }
    public ObservableCollection<Meals> Meals { get; set; }
}

public class ExerciseListTable {
    public int Id { get; set; }
    public string BodyGroup { get; set; }
    public string Details { get; set; } // JSON data, stored as string or could be deserialized into a proper object.
}

public class Exercises {
    public int Id { get; set; }
    public string Name { get; set; }
    public string MuscleGroup { get; set; }
    public int MinReps { get; set; }
    public int MinSets { get; set; }
    public int MinWeight { get; set; }
    public bool NeedsEquipment { get; set; }
    public string ImageName { get; set; }
}

public class MealListTable {
    public int Id { get; set; }
    public string GroupName { get; set; }
    public int Calories { get; set; }
    public string Details { get; set; } // JSON data, stored as string or could be deserialized into a proper object.
}

public class Meals {
    public int Id { get; set; }
    public string Name { get; set; }
    public string GoodGroup { get; set; }
    public string MealGroup { get; set; }
    public int Calories { get; set; }
    public string ImageName { get; set; }
}

public class FitnessTracker {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int UserID { get; set; }
    public int ExcersizeID { get; set; }
    public int ExercisesListID { get; set; }
    public int Meal1ListID { get; set; }
    public int Meal2ListID { get; set; }
    public int Meal3ListID { get; set; }
    public int Meal4ListID { get; set; }
    public int Meal5ListID { get; set; }
    public int Meal6ListID { get; set; }
    public int CaloriesConsumed { get; set; }
    public int Weight { get; set; }
}