﻿using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;
using Task = HomeControl.Source.Json.Task;

namespace HomeControl.Source.ViewModel.Behavior;

public class TasksDailyVM : BaseViewModel {
    private ObservableCollection<string> _imageList;
    private int _requiredTime;
    private string _taskHeaderText, _taskName, _imageSelected, _editVisibility;
    private ObservableCollection<Task> _taskList;
    private Task _taskSelected;

    public TasksDailyVM() {
        try {
            switch (ReferenceValues.ActiveBehaviorUser) {
            case 1:
                TaskHeaderText = ReferenceValues.JsonSettingsMaster.User1Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1;
                break;
            case 2:
                TaskHeaderText = ReferenceValues.JsonSettingsMaster.User2Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser2;
                break;
            case 3:
                TaskHeaderText = ReferenceValues.JsonSettingsMaster.User3Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser3;
                break;
            case 4:
                TaskHeaderText = ReferenceValues.JsonSettingsMaster.User4Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
                TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser4;
                break;
            case 5:
                TaskHeaderText = ReferenceValues.JsonSettingsMaster.User5Name + "'s " + DateTime.Now.DayOfWeek + " Tasks";
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

        EditVisibility = ReferenceValues.JsonSettingsMaster.DebugMode ? "VISIBLE" : "COLLAPSED";
        ImageList = ReferenceValues.IconImageList;
        ImageSelected = "bathtub";
        RequiredTime = 24;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    private void ButtonCommandLogic(object param) {
        MessageBoxResult confirmation;
        switch (param) {
        case "add":
            if (string.IsNullOrWhiteSpace(TaskName)) {
                SoundDispatcher.PlaySound("missing_info");
            } else {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "TasksDailyVM",
                    Description = "Adding daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                TaskList.Add(new Task {
                    TaskName = TaskName,
                    ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png",
                    RequiredTime = RequiredTime
                });

                SoundDispatcher.PlaySound("newTask");
                TaskName = "";
                RequiredTime = 24;
                SaveJson();
            }

            break;
        case "update":
            try {
                if (TaskSelected.TaskName != null) {
                    if (string.IsNullOrWhiteSpace(TaskName)) {
                        SoundDispatcher.PlaySound("missing_info");
                    } else {
                        confirmation = MessageBox.Show("Are you sure you want to update task?", "Confirmation", MessageBoxButton.YesNo);
                        if (confirmation == MessageBoxResult.Yes) {
                            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "TasksDailyVM",
                                Description = "Updating daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                            });
                            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                            TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                                TaskName = TaskName,
                                ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png",
                                RequiredTime = RequiredTime
                            });

                            TaskList.Remove(TaskSelected);
                            TaskName = "";
                            RequiredTime = 24;
                            SaveJson();
                        }
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskDailyVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case "delete":
            try {
                if (TaskSelected.TaskName != null) {
                    confirmation = MessageBox.Show("Are you sure you want to delete task?", "Confirmation", MessageBoxButton.YesNo);
                    if (confirmation == MessageBoxResult.Yes) {
                        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "TaskDailyVM",
                            Description = "Deleting daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageSelected
                        });
                        FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

                        SoundDispatcher.PlaySound("newTask");
                        TaskList.Remove(TaskSelected);
                        RequiredTime = 24;
                        SaveJson();
                    }
                }
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "TaskDailyVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }

            break;
        case "complete":
            try {
                if (!TaskSelected.IsCompleted) {
                    SoundDispatcher.PlaySound("achievement1");

                    TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                        TaskName = TaskName,
                        ImageName = "../../../Resources/Images/icons/" + ImageSelected + ".png",
                        IsCompleted = true,
                        DateCompleted = DateTime.Now.ToString("HH:mm"),
                        RequiredTime = RequiredTime
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
                        DateCompleted = "",
                        RequiredTime = RequiredTime
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
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "TaskDailyVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        try {
            FileHelpers.SaveFileText("tasks", JsonSerializer.Serialize(ReferenceValues.JsonTasksMaster), true);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "TaskDailyVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }
    }

    private void PopulateDetailedView(Task value) {
        TaskName = value.TaskName;
        ImageSelected = value.ImageName.Substring(32, value.ImageName.Length - 36);
        RequiredTime = value.RequiredTime;
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

    public int RequiredTime {
        get => _requiredTime;
        set {
            _requiredTime = value;
            RaisePropertyChangedEvent("RequiredTime");
        }
    }

    #endregion
}