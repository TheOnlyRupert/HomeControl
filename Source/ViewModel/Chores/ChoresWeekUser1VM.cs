using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Control;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresWeekUser1VM : BaseViewModel {
    private readonly PlaySound completeSound;
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/choresUser1_week_" + ReferenceValues.ChoreWeekStartDate.ToString("yyyy_MM_dd") + ".json";

    private string _room4Task1Color, _room8Task1Color, _room13Task1Color, _room13Task2Color, _room13Task3Color, _room14Task1Color, _room14Task2Color, _room14Task3Color,
        _room14Task4Color, _room14Task5Color, _room14Task6Color, _room4Task1DateText, _room8Task1DateText, _room13Task1DateText, _room13Task2DateText, _room13Task3DateText,
        _room14Task1DateText,
        _room14Task2DateText, _room14Task3DateText, _room14Task4DateText, _room14Task5DateText, _room14Task6DateText;

    public ChoresWeekUser1VM() {
        completeSound = new PlaySound("achievement1");
        GetButtonColors();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "room4Task1":
            SwitchButtonLogic("Room4Task1");
            break;
        case "room8Task1":
            SwitchButtonLogic("Room8Task1");
            break;
        case "room13Task1":
            SwitchButtonLogic("Room13Task1");
            break;
        case "room13Task2":
            SwitchButtonLogic("Room13Task2");
            break;
        case "room13Task3":
            SwitchButtonLogic("Room13Task3");
            break;
        case "room14Task1":
            SwitchButtonLogic("Room14Task1");
            break;
        case "room14Task2":
            SwitchButtonLogic("Room14Task2");
            break;
        case "room14Task3":
            SwitchButtonLogic("Room14Task3");
            break;
        case "room14Task4":
            SwitchButtonLogic("Room14Task4");
            break;
        case "room14Task5":
            SwitchButtonLogic("Room14Task5");
            break;
        case "room14Task6":
            SwitchButtonLogic("Room14Task6");
            break;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreWeekUser1MasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreWeekUser1MasterList.choreList.IndexOf(ReferenceValues.JsonChoreWeekUser1MasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreWeekUser1MasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreWeekUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreWeekUser1MasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreWeekUser1MasterList.choreList[index].Date = "";
            }
        } else {
            ReferenceValues.JsonChoreWeekUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreWeekUser1MasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreWeekUser1MasterList.choreList[index].Date = DateTime.Now.ToString("yyyy-MM-dd");
            completeSound.Play(false);
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreWeekUser1MasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreWeekUser1MasterList.choreList) {
                switch (choreDetails.Name) {
                case "Room4Task1":
                    Room4Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task1DateText = choreDetails.Date;
                    break;
                case "Room8Task1":
                    Room8Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room8Task1DateText = choreDetails.Date;
                    break;
                case "Room13Task1":
                    Room13Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room13Task1DateText = choreDetails.Date;
                    break;
                case "Room13Task2":
                    Room13Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room13Task2DateText = choreDetails.Date;
                    break;
                case "Room13Task3":
                    Room13Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room13Task3DateText = choreDetails.Date;
                    break;
                case "Room14Task1":
                    Room14Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room14Task1DateText = choreDetails.Date;
                    break;
                case "Room14Task2":
                    Room14Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room14Task2DateText = choreDetails.Date;
                    break;
                case "Room14Task3":
                    Room14Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room14Task3DateText = choreDetails.Date;
                    break;
                case "Room14Task4":
                    Room14Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room14Task4DateText = choreDetails.Date;
                    break;
                case "Room14Task5":
                    Room14Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room14Task5DateText = choreDetails.Date;
                    break;
                case "Room14Task6":
                    Room14Task6Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room14Task6DateText = choreDetails.Date;
                    break;
                }
            }
        }
    }

    #region Fields

    public string Room4Task1Color {
        get => _room4Task1Color;
        set {
            _room4Task1Color = value;
            RaisePropertyChangedEvent("Room4Task1Color");
        }
    }

    public string Room8Task1Color {
        get => _room8Task1Color;
        set {
            _room8Task1Color = value;
            RaisePropertyChangedEvent("Room8Task1Color");
        }
    }

    public string Room13Task1Color {
        get => _room13Task1Color;
        set {
            _room13Task1Color = value;
            RaisePropertyChangedEvent("Room13Task1Color");
        }
    }

    public string Room13Task2Color {
        get => _room13Task2Color;
        set {
            _room13Task2Color = value;
            RaisePropertyChangedEvent("Room13Task2Color");
        }
    }

    public string Room13Task3Color {
        get => _room13Task3Color;
        set {
            _room13Task3Color = value;
            RaisePropertyChangedEvent("Room13Task3Color");
        }
    }

    public string Room14Task1Color {
        get => _room14Task1Color;
        set {
            _room14Task1Color = value;
            RaisePropertyChangedEvent("Room14Task1Color");
        }
    }

    public string Room14Task2Color {
        get => _room14Task2Color;
        set {
            _room14Task2Color = value;
            RaisePropertyChangedEvent("Room14Task2Color");
        }
    }

    public string Room14Task3Color {
        get => _room14Task3Color;
        set {
            _room14Task3Color = value;
            RaisePropertyChangedEvent("Room14Task3Color");
        }
    }

    public string Room14Task4Color {
        get => _room14Task4Color;
        set {
            _room14Task4Color = value;
            RaisePropertyChangedEvent("Room14Task4Color");
        }
    }

    public string Room14Task5Color {
        get => _room14Task5Color;
        set {
            _room14Task5Color = value;
            RaisePropertyChangedEvent("Room14Task5Color");
        }
    }

    public string Room14Task6Color {
        get => _room14Task6Color;
        set {
            _room14Task6Color = value;
            RaisePropertyChangedEvent("Room14Task6Color");
        }
    }

    public string Room4Task1DateText {
        get => _room4Task1DateText;
        set {
            _room4Task1DateText = value;
            RaisePropertyChangedEvent("Room4Task1DateText");
        }
    }

    public string Room8Task1DateText {
        get => _room8Task1DateText;
        set {
            _room8Task1DateText = value;
            RaisePropertyChangedEvent("Room8Task1DateText");
        }
    }

    public string Room13Task1DateText {
        get => _room13Task1DateText;
        set {
            _room13Task1DateText = value;
            RaisePropertyChangedEvent("Room13Task1DateText");
        }
    }

    public string Room13Task2DateText {
        get => _room13Task2DateText;
        set {
            _room13Task2DateText = value;
            RaisePropertyChangedEvent("Room13Task2DateText");
        }
    }

    public string Room13Task3DateText {
        get => _room13Task3DateText;
        set {
            _room13Task3DateText = value;
            RaisePropertyChangedEvent("Room13Task3DateText");
        }
    }

    public string Room14Task1DateText {
        get => _room14Task1DateText;
        set {
            _room14Task1DateText = value;
            RaisePropertyChangedEvent("Room14Task1DateText");
        }
    }

    public string Room14Task2DateText {
        get => _room14Task2DateText;
        set {
            _room14Task2DateText = value;
            RaisePropertyChangedEvent("Room14Task2DateText");
        }
    }

    public string Room14Task3DateText {
        get => _room14Task3DateText;
        set {
            _room14Task3DateText = value;
            RaisePropertyChangedEvent("Room14Task3DateText");
        }
    }

    public string Room14Task4DateText {
        get => _room14Task4DateText;
        set {
            _room14Task4DateText = value;
            RaisePropertyChangedEvent("Room14Task4DateText");
        }
    }

    public string Room14Task5DateText {
        get => _room14Task5DateText;
        set {
            _room14Task5DateText = value;
            RaisePropertyChangedEvent("Room14Task5DateText");
        }
    }

    public string Room14Task6DateText {
        get => _room14Task6DateText;
        set {
            _room14Task6DateText = value;
            RaisePropertyChangedEvent("Room14Task6DateText");
        }
    }

    #endregion
}