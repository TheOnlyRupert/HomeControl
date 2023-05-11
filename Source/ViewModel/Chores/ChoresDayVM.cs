using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresDayVM : BaseViewModel {
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_day_" + DateTime.Now.ToString("yyyy_MM_dd") + ".json";

    private string _Task1Color, _Task2Color, _Task3Color, _Task4Color, _Task1DateText, _Task2DateText, _Task3DateText, _Task4DateText;
    private bool allowSound;

    public ChoresDayVM() {
        allowSound = false;
        GetButtonColors();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "task1":
            SwitchButtonLogic("Task1");
            break;
        case "task2":
            SwitchButtonLogic("Task2");
            break;
        case "task3":
            SwitchButtonLogic("Task3");
            break;
        case "task4":
            SwitchButtonLogic("Task4");
            break;
        }

        if (allowSound) {
            MediaPlayer sound = new();
            sound.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/achievement1.wav"));
            sound.Play();
            allowSound = false;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreDayMasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "ChoresDayVM",
                Description = e.ToString()
            });
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreDayMasterList.choreList.IndexOf(ReferenceValues.JsonChoreDayMasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreDayMasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreDayMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreDayMasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreDayMasterList.choreList[index].Date = "";

                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "ChoresDayVM",
                    Description = ReferenceValues.JsonMasterSettings.User2Name + " removed daily task: " + value
                });
            }
        } else {
            ReferenceValues.JsonChoreDayMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreDayMasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreDayMasterList.choreList[index].Date = DateTime.Now.ToString("HH:mm");
            allowSound = true;

            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "ChoresDayVM",
                Description = ReferenceValues.JsonMasterSettings.User2Name + " completed daily task: " + value
            });
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreDayMasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreDayMasterList.choreList) {
                switch (choreDetails.Name) {
                case "Task1":
                    Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Task1DateText = choreDetails.Date;
                    break;
                case "Task2":
                    Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Task2DateText = choreDetails.Date;
                    break;
                case "Task3":
                    Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Task3DateText = choreDetails.Date;
                    break;
                case "Task4":
                    Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Task4DateText = choreDetails.Date;
                    break;
                }
            }
        }
    }

    #region Fields

    public string Task1Color {
        get => _Task1Color;
        set {
            _Task1Color = value;
            RaisePropertyChangedEvent("Task1Color");
        }
    }

    public string Task2Color {
        get => _Task2Color;
        set {
            _Task2Color = value;
            RaisePropertyChangedEvent("Task2Color");
        }
    }

    public string Task3Color {
        get => _Task3Color;
        set {
            _Task3Color = value;
            RaisePropertyChangedEvent("Task3Color");
        }
    }

    public string Task4Color {
        get => _Task4Color;
        set {
            _Task4Color = value;
            RaisePropertyChangedEvent("Task4Color");
        }
    }

    public string Task1DateText {
        get => _Task1DateText;
        set {
            _Task1DateText = value;
            RaisePropertyChangedEvent("Task1DateText");
        }
    }

    public string Task2DateText {
        get => _Task2DateText;
        set {
            _Task2DateText = value;
            RaisePropertyChangedEvent("Task2DateText");
        }
    }

    public string Task3DateText {
        get => _Task3DateText;
        set {
            _Task3DateText = value;
            RaisePropertyChangedEvent("Task3DateText");
        }
    }

    public string Task4DateText {
        get => _Task4DateText;
        set {
            _Task4DateText = value;
            RaisePropertyChangedEvent("Task4DateText");
        }
    }

    #endregion
}