using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresMonthUser1VM : BaseViewModel {
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/choresUser1_month_" + DateTime.Now.ToString("yyyy_MM") + ".json";

    private string _room15Task1Color, _room15Task2Color, _room15Task1DateText, _room15Task2DateText;
    private bool allowSound;

    public ChoresMonthUser1VM() {
        allowSound = false;
        GetButtonColors();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "room15Task1":
            SwitchButtonLogic("Room15Task1");
            break;
        case "room15Task2":
            SwitchButtonLogic("Room15Task2");
            break;
        }

        if (allowSound) {
            ReferenceValues.SoundToPlay = "achievement1";
            SoundDispatcher.PlaySound();
            allowSound = false;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreMonthUser1MasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "ChoresMonthUser1VM",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreMonthUser1MasterList.choreList.IndexOf(ReferenceValues.JsonChoreMonthUser1MasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].Date = "";

                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "ChoresMonthUser1VM",
                    Description = ReferenceValues.JsonMasterSettings.User1Name + " removed monthly task: " + value
                });
                SaveDebugFile.Save();
            }
        } else {
            ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].Date = DateTime.Now.ToString("yyyy-MM-dd");
            allowSound = true;

            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "ChoresMonthUser1VM",
                Description = ReferenceValues.JsonMasterSettings.User1Name + " completed monthly task: " + value
            });
            SaveDebugFile.Save();
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreMonthUser1MasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreMonthUser1MasterList.choreList) {
                switch (choreDetails.Name) {
                case "Room15Task1":
                    Room15Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room15Task1DateText = choreDetails.Date;
                    break;
                case "Room15Task2":
                    Room15Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room15Task2DateText = choreDetails.Date;
                    break;
                }
            }
        }
    }

    #region Fields

    public string Room15Task1Color {
        get => _room15Task1Color;
        set {
            _room15Task1Color = value;
            RaisePropertyChangedEvent("Room15Task1Color");
        }
    }

    public string Room15Task2Color {
        get => _room15Task2Color;
        set {
            _room15Task2Color = value;
            RaisePropertyChangedEvent("Room15Task2Color");
        }
    }

    public string Room15Task1DateText {
        get => _room15Task1DateText;
        set {
            _room15Task1DateText = value;
            RaisePropertyChangedEvent("Room15Task1DateText");
        }
    }

    public string Room15Task2DateText {
        get => _room15Task2DateText;
        set {
            _room15Task2DateText = value;
            RaisePropertyChangedEvent("Room15Task2DateText");
        }
    }

    #endregion
}