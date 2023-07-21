using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Behavior;

public class TasksWeeklyVM : BaseViewModel {
    private ObservableCollection<string> _imageList, _roomList;

    private int _room1TaskSelectedIndex, _room2TaskSelectedIndex, _room3TaskSelectedIndex, _room4TaskSelectedIndex, _room5TaskSelectedIndex, _room6TaskSelectedIndex,
        _room7TaskSelectedIndex, _room8TaskSelectedIndex, _room9TaskSelectedIndex, _room10TaskSelectedIndex, _room11TaskSelectedIndex, _room12TaskSelectedIndex,
        _room13TaskSelectedIndex, _room14TaskSelectedIndex, _room15TaskSelectedIndex, _room16TaskSelectedIndex, _roomSelectedIndex;

    private string _taskHeaderText, _taskName, _imageSelected, _editVisibility;

    private ObservableCollection<Task> _taskList, _room1TaskList, _room2TaskList, _room3TaskList, _room4TaskList, _room5TaskList, _room6TaskList, _room7TaskList, _room8TaskList,
        _room9TaskList, _room10TaskList, _room11TaskList, _room12TaskList, _room13TaskList, _room14TaskList, _room15TaskList, _room16TaskList;

    private Task _taskSelected;

    public TasksWeeklyVM() {
        TaskList = new ObservableCollection<Task>();

        /* Hardcoded for now... allow this to have custom names in the future */
        RoomList = new ObservableCollection<string> {
            "Kitchen",
            "Living Room",
            "Master Bedroom",
            "Upstairs Bathroom",
            "Pantry/Stairs",
            "Mac Mac Room",
            "Twins Room",
            "Downstairs Bathroom",
            "Family/Computer Room",
            "Downstairs Closet",
            "Laundry Room",
            "Garage",
            "Outside Front Yard",
            "Outside Back Yard",
            "Vehicles",
            "Miscellaneous"
        };

        RoomSelectedIndex = 0;
        Room1TaskSelectedIndex = -1;
        Room2TaskSelectedIndex = -1;
        Room3TaskSelectedIndex = -1;
        Room4TaskSelectedIndex = -1;
        Room5TaskSelectedIndex = -1;
        Room6TaskSelectedIndex = -1;
        Room7TaskSelectedIndex = -1;
        Room8TaskSelectedIndex = -1;
        Room9TaskSelectedIndex = -1;
        Room10TaskSelectedIndex = -1;
        Room11TaskSelectedIndex = -1;
        Room12TaskSelectedIndex = -1;
        Room13TaskSelectedIndex = -1;
        Room14TaskSelectedIndex = -1;
        Room15TaskSelectedIndex = -1;
        Room16TaskSelectedIndex = -1;

        try {
            switch (ReferenceValues.ActiveBehaviorUser) {
            case 1:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User1Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1;
                break;
            case 2:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User2Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2;
                break;
            case 3:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User3Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3;
                break;
            case 4:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User4Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4;
                break;
            case 5:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User5Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5;
                break;
            }
        } catch (Exception) {
            ReferenceValues.JsonTasksMaster.JsonTasksWeekly = new JsonTasksWeekly {
                TaskListWeeklyUser1 = new ObservableCollection<Task>(),
                TaskListWeeklyUser2 = new ObservableCollection<Task>(),
                TaskListWeeklyUser3 = new ObservableCollection<Task>(),
                TaskListWeeklyUser4 = new ObservableCollection<Task>(),
                TaskListWeeklyUser5 = new ObservableCollection<Task>()
            };
        }

        Room1TaskList ??= new ObservableCollection<Task>();
        Room2TaskList ??= new ObservableCollection<Task>();
        Room3TaskList ??= new ObservableCollection<Task>();
        Room4TaskList ??= new ObservableCollection<Task>();
        Room5TaskList ??= new ObservableCollection<Task>();
        Room6TaskList ??= new ObservableCollection<Task>();
        Room7TaskList ??= new ObservableCollection<Task>();
        Room8TaskList ??= new ObservableCollection<Task>();
        Room9TaskList ??= new ObservableCollection<Task>();
        Room10TaskList ??= new ObservableCollection<Task>();
        Room1TaskList ??= new ObservableCollection<Task>();
        Room11TaskList ??= new ObservableCollection<Task>();
        Room12TaskList ??= new ObservableCollection<Task>();
        Room13TaskList ??= new ObservableCollection<Task>();
        Room14TaskList ??= new ObservableCollection<Task>();
        Room15TaskList ??= new ObservableCollection<Task>();
        Room16TaskList ??= new ObservableCollection<Task>();

        EditVisibility = !ReferenceValues.JsonMasterSettings.IsNormalMode ? "VISIBLE" : "COLLAPSED";

        ImageList = ReferenceValues.IconImageList;
        ImageSelected = "alarms";

        TasksListToRoom();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(TaskName)) {
                ReferenceValues.SoundToPlay = "missing_info";
                SoundDispatcher.PlaySound();
            } else {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "TasksWeeklyVM",
                    Description = "Adding weekly task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                });
                SaveDebugFile.Save();

                TaskList.Add(new Task {
                    TaskName = TaskName,
                    ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png",
                    RoomNumber = RoomSelectedIndex
                });

                ReferenceValues.SoundToPlay = "newTask";
                SoundDispatcher.PlaySound();
                TaskName = "";
                SaveJson();

                TasksListToRoom();
            }

            break;
        case "update":
            try {
                if (TaskSelected.TaskName != null) {
                    if (string.IsNullOrWhiteSpace(TaskName)) {
                        ReferenceValues.SoundToPlay = "missing_info";
                        SoundDispatcher.PlaySound();
                    } else {
                        confirmation = MessageBox.Show("Are you sure you want to update task?", "Confirmation", MessageBoxButton.YesNo);
                        if (confirmation == MessageBoxResult.Yes) {
                            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "TasksWeeklyVM",
                                Description = "Updating weekly task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                            });
                            SaveDebugFile.Save();

                            TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                                TaskName = TaskName,
                                ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png",
                                RoomNumber = RoomSelectedIndex
                            });

                            TaskList.Remove(TaskSelected);
                            TaskName = "";
                            SaveJson();

                            TasksListToRoom();
                        }
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskWeeklyVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }

            break;
        case "delete":
            try {
                if (TaskSelected.TaskName != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete charge?", "Confirmation", MessageBoxButton.YesNo);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "TaskWeeklyVM",
                            Description = "Deleting weekly task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                        });
                        SaveDebugFile.Save();

                        ReferenceValues.SoundToPlay = "newTask";
                        SoundDispatcher.PlaySound();
                        TaskList.Remove(TaskSelected);
                        SaveJson();

                        TasksListToRoom();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskWeeklyVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }

            break;
        case "complete":
            try {
                if (!TaskSelected.IsCompleted) {
                    ReferenceValues.SoundToPlay = "achievement1";
                    SoundDispatcher.PlaySound();

                    TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                        TaskName = TaskName,
                        ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png",
                        IsCompleted = true,
                        RoomNumber = RoomSelectedIndex,
                        DateCompleted = DateTime.Now.ToString("MM/dd")
                    });

                    TaskList.Remove(TaskSelected);
                    SaveJson();

                    TasksListToRoom();

                    Room1TaskSelectedIndex = -1;
                    Room2TaskSelectedIndex = -1;
                    Room3TaskSelectedIndex = -1;
                    Room4TaskSelectedIndex = -1;
                    Room5TaskSelectedIndex = -1;
                    Room6TaskSelectedIndex = -1;
                    Room7TaskSelectedIndex = -1;
                    Room8TaskSelectedIndex = -1;
                    Room9TaskSelectedIndex = -1;
                    Room10TaskSelectedIndex = -1;
                    Room11TaskSelectedIndex = -1;
                    Room12TaskSelectedIndex = -1;
                    Room13TaskSelectedIndex = -1;
                    Room14TaskSelectedIndex = -1;
                    Room15TaskSelectedIndex = -1;
                    Room16TaskSelectedIndex = -1;
                }
            } catch (Exception) {
                // ignored
            }

            break;
        case "reset":
            try {
                if (TaskSelected.IsCompleted) {
                    TaskSelected.IsCompleted = false;

                    TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                        TaskName = TaskName,
                        ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png",
                        RoomNumber = RoomSelectedIndex,
                        DateCompleted = ""
                    });

                    TaskList.Remove(TaskSelected);
                    SaveJson();

                    TasksListToRoom();

                    Room1TaskSelectedIndex = -1;
                    Room2TaskSelectedIndex = -1;
                    Room3TaskSelectedIndex = -1;
                    Room4TaskSelectedIndex = -1;
                    Room5TaskSelectedIndex = -1;
                    Room6TaskSelectedIndex = -1;
                    Room7TaskSelectedIndex = -1;
                    Room8TaskSelectedIndex = -1;
                    Room9TaskSelectedIndex = -1;
                    Room10TaskSelectedIndex = -1;
                    Room11TaskSelectedIndex = -1;
                    Room12TaskSelectedIndex = -1;
                    Room13TaskSelectedIndex = -1;
                    Room14TaskSelectedIndex = -1;
                    Room15TaskSelectedIndex = -1;
                    Room16TaskSelectedIndex = -1;
                }
            } catch (Exception) {
                // ignored
            }

            break;
        }
    }

    private void SaveJson() {
        if (TaskList.Count > 0) {
            try {
                switch (ReferenceValues.ActiveBehaviorUser) {
                case 1:
                    ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser1 = TaskList;
                    break;
                case 2:
                    ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser2 = TaskList;
                    break;
                case 3:
                    ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser3 = TaskList;
                    break;
                case 4:
                    ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser4 = TaskList;
                    break;
                case 5:
                    ReferenceValues.JsonTasksMaster.JsonTasksWeekly.TaskListWeeklyUser5 = TaskList;
                    break;
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskWeeklyVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }

            try {
                string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonTasksMaster);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "tasks.json", jsonString);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskWeeklyVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }
        }
    }

    private void PopulateDetailedView(Task value) {
        TaskName = value.TaskName;
        ImageSelected = value.ImageName.Substring(32, value.ImageName.Length - 36);
        RoomSelectedIndex = value.RoomNumber;
    }

    private void TasksListToRoom() {
        Room1TaskList.Clear();
        Room2TaskList.Clear();
        Room3TaskList.Clear();
        Room4TaskList.Clear();
        Room5TaskList.Clear();
        Room6TaskList.Clear();
        Room7TaskList.Clear();
        Room8TaskList.Clear();
        Room9TaskList.Clear();
        Room10TaskList.Clear();
        Room11TaskList.Clear();
        Room12TaskList.Clear();
        Room13TaskList.Clear();
        Room14TaskList.Clear();
        Room15TaskList.Clear();
        Room16TaskList.Clear();

        foreach (Task task in TaskList) {
            switch (task.RoomNumber) {
            case 0:
                Room1TaskList.Add(task);
                break;
            case 1:
                Room2TaskList.Add(task);
                break;
            case 2:
                Room3TaskList.Add(task);
                break;
            case 3:
                Room4TaskList.Add(task);
                break;
            case 4:
                Room5TaskList.Add(task);
                break;
            case 5:
                Room6TaskList.Add(task);
                break;
            case 6:
                Room7TaskList.Add(task);
                break;
            case 7:
                Room8TaskList.Add(task);
                break;
            case 8:
                Room9TaskList.Add(task);
                break;
            case 9:
                Room10TaskList.Add(task);
                break;
            case 10:
                Room11TaskList.Add(task);
                break;
            case 11:
                Room12TaskList.Add(task);
                break;
            case 12:
                Room13TaskList.Add(task);
                break;
            case 13:
                Room14TaskList.Add(task);
                break;
            case 14:
                Room15TaskList.Add(task);
                break;
            case 15:
                Room16TaskList.Add(task);
                break;
            }
        }
    }

    #region Fields

    public string TaskHeaderText {
        get => _taskHeaderText;
        set {
            _taskHeaderText = value;
            RaisePropertyChangedEvent("TaskHeaderText");
        }
    }

    public string TaskName {
        get => _taskName;
        set {
            _taskName = value;
            RaisePropertyChangedEvent("TaskName");
        }
    }

    public ObservableCollection<Task> TaskList {
        get => _taskList;
        set {
            _taskList = value;
            RaisePropertyChangedEvent("TaskList");
        }
    }

    public ObservableCollection<Task> Room1TaskList {
        get => _room1TaskList;
        set {
            _room1TaskList = value;
            RaisePropertyChangedEvent("Room1TaskList");
        }
    }

    public ObservableCollection<Task> Room2TaskList {
        get => _room2TaskList;
        set {
            _room2TaskList = value;
            RaisePropertyChangedEvent("Room2TaskList");
        }
    }

    public ObservableCollection<Task> Room3TaskList {
        get => _room3TaskList;
        set {
            _room3TaskList = value;
            RaisePropertyChangedEvent("Room3TaskList");
        }
    }

    public ObservableCollection<Task> Room4TaskList {
        get => _room4TaskList;
        set {
            _room4TaskList = value;
            RaisePropertyChangedEvent("Room4TaskList");
        }
    }

    public ObservableCollection<Task> Room5TaskList {
        get => _room5TaskList;
        set {
            _room5TaskList = value;
            RaisePropertyChangedEvent("Room5TaskList");
        }
    }

    public ObservableCollection<Task> Room6TaskList {
        get => _room6TaskList;
        set {
            _room6TaskList = value;
            RaisePropertyChangedEvent("Room6TaskList");
        }
    }

    public ObservableCollection<Task> Room7TaskList {
        get => _room7TaskList;
        set {
            _room7TaskList = value;
            RaisePropertyChangedEvent("Room7TaskList");
        }
    }

    public ObservableCollection<Task> Room8TaskList {
        get => _room8TaskList;
        set {
            _room8TaskList = value;
            RaisePropertyChangedEvent("Room8TaskList");
        }
    }

    public ObservableCollection<Task> Room9TaskList {
        get => _room9TaskList;
        set {
            _room9TaskList = value;
            RaisePropertyChangedEvent("Room9TaskList");
        }
    }

    public ObservableCollection<Task> Room10TaskList {
        get => _room10TaskList;
        set {
            _room10TaskList = value;
            RaisePropertyChangedEvent("Room10TaskList");
        }
    }

    public ObservableCollection<Task> Room11TaskList {
        get => _room11TaskList;
        set {
            _room11TaskList = value;
            RaisePropertyChangedEvent("Room11TaskList");
        }
    }

    public ObservableCollection<Task> Room12TaskList {
        get => _room12TaskList;
        set {
            _room12TaskList = value;
            RaisePropertyChangedEvent("Room12TaskList");
        }
    }

    public ObservableCollection<Task> Room13TaskList {
        get => _room13TaskList;
        set {
            _room13TaskList = value;
            RaisePropertyChangedEvent("Room13TaskList");
        }
    }

    public ObservableCollection<Task> Room14TaskList {
        get => _room14TaskList;
        set {
            _room14TaskList = value;
            RaisePropertyChangedEvent("Room14TaskList");
        }
    }

    public ObservableCollection<Task> Room15TaskList {
        get => _room15TaskList;
        set {
            _room15TaskList = value;
            RaisePropertyChangedEvent("Room15TaskList");
        }
    }

    public ObservableCollection<Task> Room16TaskList {
        get => _room16TaskList;
        set {
            _room16TaskList = value;
            RaisePropertyChangedEvent("Room16TaskList");
        }
    }

    public Task TaskSelected {
        get => _taskSelected;
        set {
            _taskSelected = value;
            PopulateDetailedView(value);
            RaisePropertyChangedEvent("TaskSelected");
        }
    }

    public ObservableCollection<string> ImageList {
        get => _imageList;
        set {
            _imageList = value;
            RaisePropertyChangedEvent("ImageList");
        }
    }

    public ObservableCollection<string> RoomList {
        get => _roomList;
        set {
            _roomList = value;
            RaisePropertyChangedEvent("RoomList");
        }
    }

    public string ImageSelected {
        get => _imageSelected;
        set {
            _imageSelected = value;
            RaisePropertyChangedEvent("ImageSelected");
        }
    }

    public string EditVisibility {
        get => _editVisibility;
        set {
            _editVisibility = value;
            RaisePropertyChangedEvent("EditVisibility");
        }
    }

    public int RoomSelectedIndex {
        get => _roomSelectedIndex;
        set {
            _roomSelectedIndex = value;
            RaisePropertyChangedEvent("RoomSelectedIndex");
        }
    }

    public int Room1TaskSelectedIndex {
        get => _room1TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = value;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room2TaskSelectedIndex {
        get => _room2TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = value;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room3TaskSelectedIndex {
        get => _room3TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = value;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room4TaskSelectedIndex {
        get => _room4TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = value;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room5TaskSelectedIndex {
        get => _room5TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = value;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room6TaskSelectedIndex {
        get => _room6TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = value;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room7TaskSelectedIndex {
        get => _room7TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = value;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room8TaskSelectedIndex {
        get => _room8TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = value;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room9TaskSelectedIndex {
        get => _room9TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = value;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room10TaskSelectedIndex {
        get => _room10TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = value;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room11TaskSelectedIndex {
        get => _room11TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = value;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room12TaskSelectedIndex {
        get => _room12TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = value;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room13TaskSelectedIndex {
        get => _room13TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = value;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room14TaskSelectedIndex {
        get => _room14TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = value;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room15TaskSelectedIndex {
        get => _room15TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = value;
            _room16TaskSelectedIndex = -1;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    public int Room16TaskSelectedIndex {
        get => _room16TaskSelectedIndex;
        set {
            _room1TaskSelectedIndex = -1;
            _room2TaskSelectedIndex = -1;
            _room3TaskSelectedIndex = -1;
            _room4TaskSelectedIndex = -1;
            _room5TaskSelectedIndex = -1;
            _room6TaskSelectedIndex = -1;
            _room7TaskSelectedIndex = -1;
            _room8TaskSelectedIndex = -1;
            _room9TaskSelectedIndex = -1;
            _room10TaskSelectedIndex = -1;
            _room11TaskSelectedIndex = -1;
            _room12TaskSelectedIndex = -1;
            _room13TaskSelectedIndex = -1;
            _room14TaskSelectedIndex = -1;
            _room15TaskSelectedIndex = -1;
            _room16TaskSelectedIndex = value;
            RaisePropertyChangedEvent("Room1TaskSelectedIndex");
            RaisePropertyChangedEvent("Room2TaskSelectedIndex");
            RaisePropertyChangedEvent("Room3TaskSelectedIndex");
            RaisePropertyChangedEvent("Room4TaskSelectedIndex");
            RaisePropertyChangedEvent("Room5TaskSelectedIndex");
            RaisePropertyChangedEvent("Room6TaskSelectedIndex");
            RaisePropertyChangedEvent("Room7TaskSelectedIndex");
            RaisePropertyChangedEvent("Room8TaskSelectedIndex");
            RaisePropertyChangedEvent("Room9TaskSelectedIndex");
            RaisePropertyChangedEvent("Room10TaskSelectedIndex");
            RaisePropertyChangedEvent("Room11TaskSelectedIndex");
            RaisePropertyChangedEvent("Room12TaskSelectedIndex");
            RaisePropertyChangedEvent("Room13TaskSelectedIndex");
            RaisePropertyChangedEvent("Room14TaskSelectedIndex");
            RaisePropertyChangedEvent("Room15TaskSelectedIndex");
            RaisePropertyChangedEvent("Room16TaskSelectedIndex");
        }
    }

    #endregion
}