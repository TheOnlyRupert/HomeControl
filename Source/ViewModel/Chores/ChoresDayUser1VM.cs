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

public class ChoresDayUser1VM : BaseViewModel {
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/choresUser1_day_" + DateTime.Now.ToString("yyyy_MM_dd") + ".json";

    private string _Task1Color, _Task2Color, _Task3Color, _Task4Color, _Task1DateText, _Task2DateText, _Task3DateText, _Task4DateText;

    private bool allowSound;

    public ChoresDayUser1VM() {
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
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreDayUser1MasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "ChoresDayUser1VM",
                Description = e.ToString()
            });
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreDayUser1MasterList.choreList.IndexOf(ReferenceValues.JsonChoreDayUser1MasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreDayUser1MasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreDayUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreDayUser1MasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreDayUser1MasterList.choreList[index].Date = "";

                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "ChoresDayUser1VM",
                    Description = ReferenceValues.JsonMasterSettings.User1Name + " removed daily task: " + value
                });
            }
        } else {
            ReferenceValues.JsonChoreDayUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreDayUser1MasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreDayUser1MasterList.choreList[index].Date = DateTime.Now.ToString("HH:mm");
            allowSound = true;

            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "ChoresDayUser1VM",
                Description = ReferenceValues.JsonMasterSettings.User1Name + " completed daily task: " + value
            });
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreDayUser1MasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreDayUser1MasterList.choreList) {
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