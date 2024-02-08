using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Exercise;

public class EditExerciseVM : BaseViewModel {
    private int _chestSelectedIndex, _shouldersSelectedIndex, _armsSelectedIndex, _backSelectedIndex, _abdominalSelectedIndex, _legsSelectedIndex, _minTime, _maxTime, _minHoldTime, _maxHoldTime,
        _difficulty, _exerciseSelectedIndex, _muscleGroupSelectedIndex;

    private ObservableCollection<Json.Exercise> _exerciseList, _chestList, _shouldersList, _armsList, _backList, _abdominalList, _legsList;

    private string _exerciseName, _imageSelected, _muscleGroupSelected;

    private Json.Exercise _exerciseSelected;
    private ObservableCollection<string> _imageList, _muscleGroupList;

    private bool _needsEquipment, _canHold;

    public EditExerciseVM() {
        MuscleGroupList = new ObservableCollection<string> {
            "Chest",
            "Shoulders",
            "Arms",
            "Back",
            "Abdominal",
            "Legs"
        };

        MuscleGroupSelectedIndex = 0;
        ExerciseSelectedIndex = 0;
        ChestSelectedIndex = -1;
        ShouldersSelectedIndex = -1;
        ArmsSelectedIndex = -1;
        BackSelectedIndex = -1;
        AbdominalSelectedIndex = -1;
        LegsSelectedIndex = -1;
        ExerciseList = ReferenceValues.JsonExerciseMaster.ExerciseList;

        ExerciseList ??= new ObservableCollection<Json.Exercise>();
        ChestList ??= new ObservableCollection<Json.Exercise>();
        ShouldersList ??= new ObservableCollection<Json.Exercise>();
        BackList ??= new ObservableCollection<Json.Exercise>();
        LegsList ??= new ObservableCollection<Json.Exercise>();
        AbdominalList ??= new ObservableCollection<Json.Exercise>();
        ArmsList ??= new ObservableCollection<Json.Exercise>();

        ListToMuscleGroup();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void PopulateDetailedView(Json.Exercise value) {
        ExerciseName = value.Name;
        Difficulty = value.Difficulty;
        MuscleGroupSelected = value.MuscleGroup;
        MinTime = value.MinTime;
        MaxTime = value.MaxTime;
        MinHoldTime = value.MinHoldTime;
        MaxHoldTime = value.MaxHoldTime;
        CanHold = value.CanHold;
        NeedsEquipment = value.NeedsEquipment;
    }

    private void ListToMuscleGroup() {
        ChestList.Clear();
        ShouldersList.Clear();
        ArmsList.Clear();
        BackList.Clear();
        AbdominalList.Clear();
        LegsList.Clear();

        foreach (Json.Exercise exercise in ExerciseList) {
            switch (exercise.MuscleGroup) {
            case "Chest":
                ChestList.Add(exercise);
                break;
            case "Shoulders":
                ShouldersList.Add(exercise);
                break;
            case "Arms":
                ArmsList.Add(exercise);
                break;
            case "Back":
                BackList.Add(exercise);
                break;
            case "Abdominal":
                AbdominalList.Add(exercise);
                break;
            case "Legs":
                LegsList.Add(exercise);
                break;
            }
        }
    }

    private void ButtonCommandLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(ExerciseName)) {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            } else {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditExerciseVM",
                    Description = "Adding exercise to " + MuscleGroupSelected + ": " + ExerciseName
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                ExerciseList.Add(new Json.Exercise {
                    Name = ExerciseName,
                    Difficulty = Difficulty,
                    MuscleGroup = MuscleGroupSelected,
                    MinTime = MinTime,
                    MaxTime = MaxTime,
                    MinHoldTime = MinHoldTime,
                    MaxHoldTime = MaxHoldTime,
                    CanHold = CanHold,
                    NeedsEquipment = NeedsEquipment
                });

                ReferenceValues.SoundToPlay = "newTask";
                SoundDispatcher.PlaySound();
                ExerciseName = "";

                SaveJson();
                ListToMuscleGroup();
            }

            break;
        case "update":
            try {
                if (ExerciseSelected.Name != null) {
                    if (string.IsNullOrWhiteSpace(ExerciseName)) {
                        ReferenceValues.SoundToPlay = "missing_info";
                        SoundDispatcher.PlaySound();
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

                            ExerciseList.Insert(ExerciseList.IndexOf(ExerciseSelected), new Json.Exercise {
                                Name = ExerciseName,
                                Difficulty = Difficulty,
                                MuscleGroup = MuscleGroupSelected,
                                MinTime = MinTime,
                                MaxTime = MaxTime,
                                MinHoldTime = MinHoldTime,
                                MaxHoldTime = MaxHoldTime,
                                CanHold = CanHold,
                                NeedsEquipment = NeedsEquipment
                            });

                            ExerciseList.Remove(ExerciseSelected);
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
                if (ExerciseSelected.Name != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete exercise?", "Confirmation", MessageBoxButton.YesNo);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditExerciseVM",
                            Description = "Deleting monthly exercise " + MuscleGroupSelected + ": " + ExerciseName
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        ReferenceValues.SoundToPlay = "newTask";
                        SoundDispatcher.PlaySound();
                        ExerciseList.Remove(ExerciseSelected);

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
            ReferenceValues.JsonExerciseMaster.ExerciseList = ExerciseList;

            FileHelpers.SaveFileText("exercise", JsonSerializer.Serialize(ReferenceValues.JsonExerciseMaster), true);
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

    public ObservableCollection<Json.Exercise> ExerciseList {
        get => _exerciseList;
        set {
            _exerciseList = value;
            RaisePropertyChangedEvent("ExerciseList");
        }
    }

    public string ImageSelected {
        get => _imageSelected;
        set {
            _imageSelected = value;
            RaisePropertyChangedEvent("ImageSelected");
        }
    }

    public string MuscleGroupSelected {
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

    public string ExerciseName {
        get => _exerciseName;
        set {
            _exerciseName = value;
            RaisePropertyChangedEvent("ExerciseName");
        }
    }

    public int Difficulty {
        get => _difficulty;
        set {
            _difficulty = value;
            RaisePropertyChangedEvent("Difficulty");
        }
    }

    public int MinTime {
        get => _minTime;
        set {
            _minTime = value;
            RaisePropertyChangedEvent("MinTime");
        }
    }

    public int MaxTime {
        get => _maxTime;
        set {
            _maxTime = value;
            RaisePropertyChangedEvent("MaxTime");
        }
    }

    public int MinHoldTime {
        get => _minHoldTime;
        set {
            _minHoldTime = value;
            RaisePropertyChangedEvent("MinHoldTime");
        }
    }

    public int MaxHoldTime {
        get => _maxHoldTime;
        set {
            _maxHoldTime = value;
            RaisePropertyChangedEvent("MaxHoldTime");
        }
    }

    public bool NeedsEquipment {
        get => _needsEquipment;
        set {
            _needsEquipment = value;
            RaisePropertyChangedEvent("NeedsEquipment");
        }
    }

    public bool CanHold {
        get => _canHold;
        set {
            _canHold = value;
            RaisePropertyChangedEvent("CanHold");
        }
    }

    public ObservableCollection<string> MuscleGroupList {
        get => _muscleGroupList;
        set {
            _muscleGroupList = value;
            RaisePropertyChangedEvent("MuscleGroupList");
        }
    }

    public ObservableCollection<string> ImageList {
        get => _imageList;
        set {
            _imageList = value;
            RaisePropertyChangedEvent("ImageList");
        }
    }

    public ObservableCollection<Json.Exercise> ChestList {
        get => _chestList;
        set {
            _chestList = value;
            RaisePropertyChangedEvent("ChestList");
        }
    }

    public ObservableCollection<Json.Exercise> ShouldersList {
        get => _shouldersList;
        set {
            _shouldersList = value;
            RaisePropertyChangedEvent("ShouldersList");
        }
    }

    public ObservableCollection<Json.Exercise> BackList {
        get => _backList;
        set {
            _backList = value;
            RaisePropertyChangedEvent("BackList");
        }
    }

    public ObservableCollection<Json.Exercise> ArmsList {
        get => _armsList;
        set {
            _armsList = value;
            RaisePropertyChangedEvent("ArmsList");
        }
    }

    public ObservableCollection<Json.Exercise> AbdominalList {
        get => _abdominalList;
        set {
            _abdominalList = value;
            RaisePropertyChangedEvent("AbdominalList");
        }
    }

    public ObservableCollection<Json.Exercise> LegsList {
        get => _legsList;
        set {
            _legsList = value;
            RaisePropertyChangedEvent("LegsList");
        }
    }

    public Json.Exercise ExerciseSelected {
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
            _armsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _abdominalSelectedIndex = -1;
            _legsSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("ArmsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("AbdominalSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
        }
    }

    public int ShouldersSelectedIndex {
        get => _shouldersSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = value;
            _armsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _abdominalSelectedIndex = -1;
            _legsSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("ArmsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("AbdominalSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
        }
    }

    public int ArmsSelectedIndex {
        get => _armsSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _armsSelectedIndex = value;
            _backSelectedIndex = -1;
            _abdominalSelectedIndex = -1;
            _legsSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("ArmsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("AbdominalSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
        }
    }

    public int BackSelectedIndex {
        get => _backSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _armsSelectedIndex = -1;
            _backSelectedIndex = value;
            _abdominalSelectedIndex = -1;
            _legsSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("ArmsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("AbdominalSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
        }
    }

    public int AbdominalSelectedIndex {
        get => _abdominalSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _armsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _abdominalSelectedIndex = value;
            _legsSelectedIndex = -1;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("ArmsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("AbdominalSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
        }
    }

    public int LegsSelectedIndex {
        get => _legsSelectedIndex;
        set {
            _chestSelectedIndex = -1;
            _shouldersSelectedIndex = -1;
            _armsSelectedIndex = -1;
            _backSelectedIndex = -1;
            _abdominalSelectedIndex = -1;
            _legsSelectedIndex = value;
            RaisePropertyChangedEvent("ChestSelectedIndex");
            RaisePropertyChangedEvent("ShouldersSelectedIndex");
            RaisePropertyChangedEvent("ArmsSelectedIndex");
            RaisePropertyChangedEvent("BackSelectedIndex");
            RaisePropertyChangedEvent("AbdominalSelectedIndex");
            RaisePropertyChangedEvent("LegsSelectedIndex");
        }
    }

    #endregion
}