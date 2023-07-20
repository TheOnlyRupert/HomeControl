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
    private string _taskHeaderText, _taskName, _imageName;
    private ObservableCollection<Task> _taskList;
    private Task _taskSelected;

    public TasksDailyVM() {
        TaskList = new ObservableCollection<Task>();
        try {
            TaskList = ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1;
        } catch (Exception) {
            ReferenceValues.JsonTasksMaster.JsonTasksDaily = new JsonTasksDaily {
                TaskListDailyUser1 = new ObservableCollection<Task>()
            };
        }

        TaskHeaderText = ReferenceValues.ActiveBehaviorUser + " " + DateTime.Now.DayOfWeek + " Tasks";
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
                    Description = "Adding daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageName
                });
                SaveDebugFile.Save();

                TaskList.Add(new Task {
                    TaskName = TaskName,
                    ImageName = ImageName
                });

                ReferenceValues.SoundToPlay = "newTask";
                SoundDispatcher.PlaySound();
                TaskName = "";
                ImageName = "";
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
                                Description = "Updating daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageName
                            });
                            SaveDebugFile.Save();

                            TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                                TaskName = TaskName,
                                ImageName = ImageName
                            });

                            ReferenceValues.SoundToPlay = "cash";
                            SoundDispatcher.PlaySound();
                            TaskList.Remove(TaskSelected);
                            TaskName = "";
                            ImageName = "";
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
                            Description = "Deleting daily task to " + ReferenceValues.ActiveBehaviorUser + ": " + TaskName + ", " + ImageName
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
            if (TaskSelected.TaskName != null) {
                if (!TaskSelected.IsCompleted) {
                    ReferenceValues.SoundToPlay = "achievement1";
                    SoundDispatcher.PlaySound();

                    TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                        TaskName = TaskName,
                        ImageName = ImageName,
                        IsCompleted = true,
                        DateCompleted = DateTime.Now.ToString("HH:mm")
                    });

                    TaskList.Remove(TaskSelected);
                    SaveJson();
                }
            }

            break;
        case "reset":
            if (TaskSelected.TaskName != null) {
                if (TaskSelected.IsCompleted) {
                    TaskSelected.IsCompleted = false;

                    TaskList.Insert(TaskList.IndexOf(TaskSelected), new Task {
                        TaskName = TaskName,
                        ImageName = ImageName,
                        DateCompleted = ""
                    });

                    TaskList.Remove(TaskSelected);
                    SaveJson();
                }
            }

            break;
        }
    }

    private void SaveJson() {
        if (TaskList.Count > 0) {
            try {
                ReferenceValues.JsonTasksMaster.JsonTasksDaily.TaskListDailyUser1 = TaskList;
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
        ImageName = value.ImageName;
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
            _taskName = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("TaskName");
        }
    }

    public string ImageName {
        get => _imageName;
        set {
            _imageName = VerifyInput.VerifyTextAlphaNumericSpace(value);
            RaisePropertyChangedEvent("ImageName");
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

    #endregion
}