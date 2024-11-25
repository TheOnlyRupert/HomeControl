using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Fitness;

public class EditExerciseVm : BaseViewModel {
    private ObservableCollection<Exercises>? _backList;
    private ObservableCollection<Exercises>? _bicepsList;
    private ObservableCollection<Exercises>? _cardioList;
    private ObservableCollection<Exercises>? _chestList;

    private int _chestSelectedIndex, _shouldersSelectedIndex, _tricepsSelectedIndex, _bicepsSelectedIndex, _backSelectedIndex, _coreSelectedIndex, _legsSelectedIndex, _cardioSelectedIndex,
        _minReps, _minSets, _minWeight, _exerciseSelectedIndex, _muscleGroupSelectedIndex;

    private ObservableCollection<Exercises>? _coreList;

    private ObservableCollection<Exercises>? _exerciseList;

    private string? _exerciseName;
    private Exercises? _exerciseSelected;
    private ObservableCollection<string>? _imageList;
    private string? _imageSelected;
    private ObservableCollection<Exercises>? _legsList;
    private ObservableCollection<string>? _muscleGroupList;
    private string? _muscleGroupSelected;

    private bool _needsEquipment;
    private ObservableCollection<Exercises>? _shouldersList;
    private ObservableCollection<Exercises>? _tricepsList;

    public EditExerciseVm() {
        MuscleGroupList = [
            "Chest",
            "Shoulders",
            "Triceps",
            "Biceps",
            "Back",
            "Core",
            "Legs",
            "Cardio"
        ];

        MuscleGroupSelectedIndex = 0;
        ExerciseSelectedIndex = 0;
        ChestSelectedIndex = -1;
        ShouldersSelectedIndex = -1;
        TricepsSelectedIndex = -1;
        BicepsSelectedIndex = -1;
        BackSelectedIndex = -1;
        CoreSelectedIndex = -1;
        LegsSelectedIndex = -1;
        CardioSelectedIndex = -1;
        ExerciseList = ReferenceValues.JsonFitnessMaster.Exercises;

        ExerciseList ??= [];
        ChestList ??= [];
        ShouldersList ??= [];
        TricepsList ??= [];
        BicepsList ??= [];
        BackList ??= [];
        LegsList ??= [];
        CoreList ??= [];
        CardioList ??= [];

        ListToMuscleGroup();
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    private void PopulateDetailedView(Exercises? value) {
        if (value != null) {
            ExerciseName = value.Name;
            MuscleGroupSelected = value.MuscleGroup;
            MinReps = value.MinReps;
            MinSets = value.MinSets;
            MinWeight = value.MinWeight;
            NeedsEquipment = value.NeedsEquipment;
        }
    }

    private void ListToMuscleGroup() {
        ChestList?.Clear();
        ShouldersList?.Clear();
        TricepsList?.Clear();
        BicepsList?.Clear();
        BackList?.Clear();
        CoreList?.Clear();
        LegsList?.Clear();
        CardioList?.Clear();

        if (ExerciseList != null)
            foreach (Exercises exercise in ExerciseList) {
                switch (exercise.MuscleGroup) {
                case "Chest":
                    ChestList?.Add(exercise);
                    break;
                case "Shoulders":
                    ShouldersList?.Add(exercise);
                    break;
                case "Triceps":
                    TricepsList?.Add(exercise);
                    break;
                case "Biceps":
                    BicepsList?.Add(exercise);
                    break;
                case "Back":
                    BackList?.Add(exercise);
                    break;
                case "Abdominal":
                    CoreList?.Add(exercise);
                    break;
                case "Legs":
                    LegsList?.Add(exercise);
                    break;
                case "Cardio":
                    CardioList?.Add(exercise);
                    break;
                }
            }
    }

    private void ButtonCommandLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(ExerciseName)) {
                SoundDispatcher.PlaySound("missing_info");
            } else {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditExerciseVM",
                    Description = "Adding exercise to " + MuscleGroupSelected + ": " + ExerciseName
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                ExerciseList?.Add(new Exercises {
                    Name = ExerciseName,
                    MuscleGroup = MuscleGroupSelected,
                    MinReps = MinReps,
                    MinSets = MinSets,
                    MinWeight = MinWeight,
                    NeedsEquipment = NeedsEquipment
                });

                SoundDispatcher.PlaySound("newTask");
                ExerciseName = "";

                SaveJson();
                ListToMuscleGroup();
            }

            break;
        case "update":
            try {
                if (ExerciseSelected?.Name != null) {
                    if (string.IsNullOrWhiteSpace(ExerciseName)) {
                        SoundDispatcher.PlaySound("missing_info");
                    } else {
                        confirmation = MessageBox.Show("Are you sure you want to update exercise?", "Confirmation", MessageBoxButton.YesNo);
                        if (confirmation == MessageBoxResult.Yes) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditExerciseVM",
                                Description = "Updating exercise to " + MuscleGroupSelected + ": " + ExerciseName
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            ExerciseList?.Insert(ExerciseList.IndexOf(ExerciseSelected), new Exercises {
                                Name = ExerciseName,
                                MuscleGroup = MuscleGroupSelected,
                                MinReps = MinReps,
                                MinSets = MinSets,
                                MinWeight = MinWeight,
                                NeedsEquipment = NeedsEquipment
                            });

                            ExerciseList?.Remove(ExerciseSelected);
                            ExerciseName = "";

                            SaveJson();
                            ListToMuscleGroup();
                        }
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditExerciseVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case "delete":
            try {
                if (ExerciseSelected?.Name != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete exercise?", "Confirmation", MessageBoxButton.YesNo);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditExerciseVM",
                            Description = "Deleting monthly exercise " + MuscleGroupSelected + ": " + ExerciseName
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        SoundDispatcher.PlaySound("newTask");
                        ExerciseList?.Remove(ExerciseSelected);

                        SaveJson();
                        ListToMuscleGroup();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditExerciseVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        }
    }

    private void SaveJson() {
        try {
            ReferenceValues.JsonFitnessMaster.Exercises = ExerciseList;

            FileHelpers.SaveFileText("exercise", JsonSerializer.Serialize(ReferenceValues.JsonFitnessMaster), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditExerciseVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }
    }

    #region Fields

    public string? ImageSelected {
        get => _imageSelected;
        set {
            _imageSelected = value;
            RaisePropertyChangedEvent("ImageSelected");
        }
    }

    public string? MuscleGroupSelected {
        get => _muscleGroupSelected;
        set {
            _muscleGroupSelected = value;
            RaisePropertyChangedEvent("MuscleGroupSelected");
        }
    }

    public int MuscleGroupSelectedIndex {
        get => _muscleGroupSelectedIndex;
        set {
            _muscleGroupSelectedIndex = value;
            RaisePropertyChangedEvent("MuscleGroupSelectedIndex");
        }
    }

    public string? ExerciseName {
        get => _exerciseName;
        set {
            _exerciseName = value;
            RaisePropertyChangedEvent("ExerciseName");
        }
    }

    public int MinReps {
        get => _minReps;
        set {
            _minReps = value;
            RaisePropertyChangedEvent("MinReps");
        }
    }

    public int MinSets {
        get => _minSets;
        set {
            _minSets = value;
            RaisePropertyChangedEvent("MinSets");
        }
    }

    public int MinWeight {
        get => _minWeight;
        set {
            _minWeight = value;
            RaisePropertyChangedEvent("MinWeight");
        }
    }

    public bool NeedsEquipment {
        get => _needsEquipment;
        set {
            _needsEquipment = value;
            RaisePropertyChangedEvent("NeedsEquipment");
        }
    }

    public ObservableCollection<string>? MuscleGroupList {
        get => _muscleGroupList;
        set {
            _muscleGroupList = value;
            RaisePropertyChangedEvent("MuscleGroupList");
        }
    }

    public ObservableCollection<string>? ImageList {
        get => _imageList;
        set {
            _imageList = value;
            RaisePropertyChangedEvent("ImageList");
        }
    }

    public ObservableCollection<Exercises>? ExerciseList {
        get => _exerciseList;
        set {
            _exerciseList = value;
            RaisePropertyChangedEvent("ExerciseList");
        }
    }

    public ObservableCollection<Exercises>? ChestList {
        get => _chestList;
        set {
            _chestList = value;
            RaisePropertyChangedEvent("ChestList");
        }
    }

    public ObservableCollection<Exercises>? ShouldersList {
        get => _shouldersList;
        set {
            _shouldersList = value;
            RaisePropertyChangedEvent("ShouldersList");
        }
    }

    public ObservableCollection<Exercises>? BackList {
        get => _backList;
        set {
            _backList = value;
            RaisePropertyChangedEvent("BackList");
        }
    }

    public ObservableCollection<Exercises>? TricepsList {
        get => _tricepsList;
        set {
            _tricepsList = value;
            RaisePropertyChangedEvent("TricepsList");
        }
    }

    public ObservableCollection<Exercises>? BicepsList {
        get => _bicepsList;
        set {
            _bicepsList = value;
            RaisePropertyChangedEvent("BicepsList");
        }
    }

    public ObservableCollection<Exercises>? CoreList {
        get => _coreList;
        set {
            _coreList = value;
            RaisePropertyChangedEvent("CoreList");
        }
    }

    public ObservableCollection<Exercises>? LegsList {
        get => _legsList;
        set {
            _legsList = value;
            RaisePropertyChangedEvent("LegsList");
        }
    }

    public ObservableCollection<Exercises>? CardioList {
        get => _cardioList;
        set {
            _cardioList = value;
            RaisePropertyChangedEvent("CardioList");
        }
    }

    public Exercises? ExerciseSelected {
        get => _exerciseSelected;
        set {
            _exerciseSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("ExerciseSelected");
        }
    }

    public int ExerciseSelectedIndex {
        get => _exerciseSelectedIndex;
        set {
            _exerciseSelectedIndex = value;
            RaisePropertyChangedEvent("ExerciseSelectedIndex");
        }
    }

    public int ChestSelectedIndex {
        get => _chestSelectedIndex;
        set {
            _chestSelectedIndex = value;
            _shouldersSelectedIndex = -1;
            _tricepsSelectedIndex = -1;
            _bicepsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _coreSelectedIndex = -1;
            _legsSelectedIndex = -1;
            _cardioSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("TricepsSelectedIndex");
            RaisePropertyChangedEvent("BicepsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("CoreSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
            RaisePropertyChangedEvent("CardioSelectedIndex");
        }
    }

    public int ShouldersSelectedIndex {
        get => _shouldersSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = value;
            _tricepsSelectedIndex = -1;
            _bicepsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _coreSelectedIndex = -1;
            _legsSelectedIndex = -1;
            _cardioSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("TricepsSelectedIndex");
            RaisePropertyChangedEvent("BicepsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("CoreSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
            RaisePropertyChangedEvent("CardioSelectedIndex");
        }
    }

    public int TricepsSelectedIndex {
        get => _tricepsSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _tricepsSelectedIndex = value;
            _bicepsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _coreSelectedIndex = -1;
            _legsSelectedIndex = -1;
            _cardioSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("TricepsSelectedIndex");
            RaisePropertyChangedEvent("BicepsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("CoreSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
            RaisePropertyChangedEvent("CardioSelectedIndex");
        }
    }

    public int BicepsSelectedIndex {
        get => _bicepsSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _tricepsSelectedIndex = -1;
            _bicepsSelectedIndex = value;
            _backSelectedIndex = -1;
            _coreSelectedIndex = -1;
            _legsSelectedIndex = -1;
            _cardioSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("TricepsSelectedIndex");
            RaisePropertyChangedEvent("BicepsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("CoreSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
            RaisePropertyChangedEvent("CardioSelectedIndex");
        }
    }

    public int BackSelectedIndex {
        get => _backSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _tricepsSelectedIndex = -1;
            _bicepsSelectedIndex = -1;
            _backSelectedIndex = value;
            _coreSelectedIndex = -1;
            _legsSelectedIndex = -1;
            _cardioSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("TricepsSelectedIndex");
            RaisePropertyChangedEvent("BicepsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("CoreSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
            RaisePropertyChangedEvent("CardioSelectedIndex");
        }
    }

    public int CoreSelectedIndex {
        get => _coreSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _tricepsSelectedIndex = -1;
            _bicepsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _coreSelectedIndex = value;
            _legsSelectedIndex = -1;
            _cardioSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("TricepsSelectedIndex");
            RaisePropertyChangedEvent("BicepsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("CoreSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
            RaisePropertyChangedEvent("CardioSelectedIndex");
        }
    }

    public int LegsSelectedIndex {
        get => _legsSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _tricepsSelectedIndex = -1;
            _bicepsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _coreSelectedIndex = -1;
            _legsSelectedIndex = value;
            _cardioSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("TricepsSelectedIndex");
            RaisePropertyChangedEvent("BicepsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("CoreSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
            RaisePropertyChangedEvent("CardioSelectedIndex");
        }
    }

    public int CardioSelectedIndex {
        get => _cardioSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _tricepsSelectedIndex = -1;
            _bicepsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _coreSelectedIndex = -1;
            _legsSelectedIndex = -1;
            _cardioSelectedIndex = value;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("TricepsSelectedIndex");
            RaisePropertyChangedEvent("BicepsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("CoreSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
            RaisePropertyChangedEvent("CardioSelectedIndex");
        }
    }

    #endregion
}