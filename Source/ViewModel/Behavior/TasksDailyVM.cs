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

public class TasksDailyVM : BaseViewModel {
    private ObservableCollection<string> _imageList;
    private string _taskHeaderText, _taskName, _imageSelected, _editVisibility;
    private ObservableCollection<Task> _taskList;
    private Task _taskSelected;

    public TasksDailyVM() {
        try {
            switch (ReferenceValues.ActiveBehaviorUser) {
            case 1:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User1Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1;
                break;
            case 2:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User2Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2;
                break;
            case 3:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User3Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3;
                break;
            case 4:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User4Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4;
                break;
            case 5:
                TaskHeaderText = ReferenceValues.JsonMasterSettings.User5Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5;
                break;
            }

            TaskList ??= new ObservableCollection<Task>();
        } catch (Exception) {
            ReferenceValues.JsonTasksMaster.JsonTasksDaily = new JsonTasksDaily {
                TaskListDailyUser1 = new ObservableCollection<Task>(),
                TaskListDailyUser2 = new ObservableCollection<Task>(),
                TaskListDailyUser3 = new ObservableCollection<Task>(),
                TaskListDailyUser4 = new ObservableCollection<Task>(),
                TaskListDailyUser5 = new ObservableCollection<Task>()
            };
        }

        EditVisibility = !ReferenceValues.JsonMasterSettings.IsNormalMode ? "VISIBLE" : "COLLAPSED";

        ImageList = ReferenceValues.IconImageList;
        ImageSelected = "alarms";
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
                    Module = "TasksDailyVM",
                    Description = "Adding daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                });
                SaveDebugFile.Save();

                TaskList.Add(new Task {
                    TaskName = TaskName,
                    ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png"
                });

                ReferenceValues.SoundToPlay = "newTask";
                SoundDispatcher.PlaySound();
                TaskName = "";
                SaveJson();
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
                                Module = "TasksDailyVM",
                                Description = "Updating daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                            });
                            SaveDebugFile.Save();

                            TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                                TaskName = TaskName,
                                ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png"
                            });

                            TaskList.Remove(TaskSelected);
                            TaskName = "";
                            SaveJson();
                        }
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskDailyVM",
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
                            Module = "TaskDailyVM",
                            Description = "Deleting daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                        });
                        SaveDebugFile.Save();

                        ReferenceValues.SoundToPlay = "newTask";
                        SoundDispatcher.PlaySound();
                        TaskList.Remove(TaskSelected);
                        SaveJson();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskDailyVM",
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
                        DateCompleted = DateTime.Now.ToString("HH:mm")
                    });

                    TaskList.Remove(TaskSelected);
                    SaveJson();
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
                        DateCompleted = ""
                    });

                    TaskList.Remove(TaskSelected);
                    SaveJson();
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
                    ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1 = TaskList;
                    break;
                case 2:
                    ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2 = TaskList;
                    break;
                case 3:
                    ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3 = TaskList;
                    break;
                case 4:
                    ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4 = TaskList;
                    break;
                case 5:
                    ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser5 = TaskList;
                    break;
                }
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskDailyVM",
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
                    Module = "TaskDailyVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }
        }
    }

    private void PopulateDetailedView(Task value) {
        TaskName = value.TaskName;
        ImageSelected = value.ImageName.Substring(32, value.ImageName.Length - 36);
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

    #endregion
}