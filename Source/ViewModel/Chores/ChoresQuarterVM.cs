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

public class ChoresQuarterVM : BaseViewModel {
    private readonly PlaySound completeSound;
    private readonly string fileName;

    private string _room1Task1Color, _room2Task1Color, _room2Task2Color, _room2Task3Color, _room2Task4Color, _room3Task1Color, _room4Task1Color, _room4Task2Color, _room4Task3Color,
        _room4Task4Color, _room4Task5Color, _room4Task6Color, _room4Task7Color, _room6Task1Color, _room6Task2Color, _room6Task3Color, _room6Task4Color, _room7Task1Color,
        _room9Task1Color, _room10Task1Color, _room11Task1Color, _room11Task2Color, _room1Task1DateText, _room2Task1DateText, _room2Task2DateText, _room2Task3DateText,
        _room2Task4DateText, _room3Task1DateText, _room4Task1DateText, _room4Task2DateText, _room4Task3DateText, _room4Task4DateText, _room4Task5DateText, _room4Task6DateText,
        _room4Task7DateText, _room6Task1DateText, _room6Task2DateText, _room6Task3DateText, _room6Task4DateText, _room7Task1DateText, _room9Task1DateText, _room10Task1DateText,
        _room11Task1DateText, _room11Task2DateText;

    public ChoresQuarterVM() {
        fileName = ReferenceValues.ChoreMonthStartDate.Month switch {
            > 0 and < 3 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter1_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy") + ".json",
            > 2 and < 6 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter2_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy") + ".json",
            > 5 and < 9 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter3_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy") + ".json",
            _ => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter4_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy") + ".json"
        };

        completeSound = new PlaySound("achievement1");
        GetButtonColors();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "room1Task1":
            SwitchButtonLogic("Room1Task1");
            break;
        case "room2Task1":
            SwitchButtonLogic("Room2Task1");
            break;
        case "room2Task2":
            SwitchButtonLogic("Room2Task2");
            break;
        case "room2Task3":
            SwitchButtonLogic("Room2Task3");
            break;
        case "room2Task4":
            SwitchButtonLogic("Room2Task4");
            break;
        case "room3Task1":
            SwitchButtonLogic("Room3Task1");
            break;
        case "room4Task1":
            SwitchButtonLogic("Room4Task1");
            break;
        case "room4Task2":
            SwitchButtonLogic("Room4Task2");
            break;
        case "room4Task3":
            SwitchButtonLogic("Room4Task3");
            break;
        case "room4Task4":
            SwitchButtonLogic("Room4Task4");
            break;
        case "room4Task5":
            SwitchButtonLogic("Room4Task5");
            break;
        case "room4Task6":
            SwitchButtonLogic("Room4Task6");
            break;
        case "room4Task7":
            SwitchButtonLogic("Room4Task7");
            break;
        case "room6Task1":
            SwitchButtonLogic("Room6Task1");
            break;
        case "room6Task2":
            SwitchButtonLogic("Room6Task2");
            break;
        case "room6Task3":
            SwitchButtonLogic("Room6Task3");
            break;
        case "room6Task4":
            SwitchButtonLogic("Room6Task4");
            break;
        case "room7Task1":
            SwitchButtonLogic("Room7Task1");
            break;
        case "room9Task1":
            SwitchButtonLogic("Room9Task1");
            break;
        case "room10Task1":
            SwitchButtonLogic("Room10Task1");
            break;
        case "room11Task1":
            SwitchButtonLogic("Room11Task1");
            break;
        case "room11Task2":
            SwitchButtonLogic("Room11Task2");
            break;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreQuarterMasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreQuarterMasterList.choreList.IndexOf(ReferenceValues.JsonChoreQuarterMasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreQuarterMasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreQuarterMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreQuarterMasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreQuarterMasterList.choreList[index].Date = "";
            }
        } else {
            ReferenceValues.JsonChoreQuarterMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreQuarterMasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreQuarterMasterList.choreList[index].Date = DateTime.Now.ToString("yyyy-MM-dd");
            completeSound.Play(false);
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreQuarterMasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreQuarterMasterList.choreList) {
                switch (choreDetails.Name) {
                case "Room1Task1":
                    Room1Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task1DateText = choreDetails.Date;
                    break;
                case "Room2Task1":
                    Room2Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room2Task1DateText = choreDetails.Date;
                    break;
                case "Room2Task2":
                    Room2Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room2Task2DateText = choreDetails.Date;
                    break;
                case "Room2Task3":
                    Room2Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room2Task3DateText = choreDetails.Date;
                    break;
                case "Room2Task4":
                    Room2Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room2Task4DateText = choreDetails.Date;
                    break;
                case "Room3Task1":
                    Room3Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task1DateText = choreDetails.Date;
                    break;
                case "Room4Task1":
                    Room4Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task1DateText = choreDetails.Date;
                    break;
                case "Room4Task2":
                    Room4Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task2DateText = choreDetails.Date;
                    break;
                case "Room4Task3":
                    Room4Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task3DateText = choreDetails.Date;
                    break;
                case "Room4Task4":
                    Room4Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task4DateText = choreDetails.Date;
                    break;
                case "Room4Task5":
                    Room4Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task5DateText = choreDetails.Date;
                    break;
                case "Room4Task6":
                    Room4Task6Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task6DateText = choreDetails.Date;
                    break;
                case "Room4Task7":
                    Room4Task7Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task7DateText = choreDetails.Date;
                    break;
                case "Room6Task1":
                    Room6Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task1DateText = choreDetails.Date;
                    break;
                case "Room6Task2":
                    Room6Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task2DateText = choreDetails.Date;
                    break;
                case "Room6Task3":
                    Room6Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task3DateText = choreDetails.Date;
                    break;
                case "Room6Task4":
                    Room6Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task4DateText = choreDetails.Date;
                    break;
                case "Room7Task1":
                    Room7Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room7Task1DateText = choreDetails.Date;
                    break;
                case "Room9Task1":
                    Room9Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room9Task1DateText = choreDetails.Date;
                    break;
                case "Room10Task1":
                    Room10Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room10Task1DateText = choreDetails.Date;
                    break;
                case "Room11Task1":
                    Room11Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room11Task1DateText = choreDetails.Date;
                    break;
                case "Room11Task2":
                    Room11Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room11Task2DateText = choreDetails.Date;
                    break;
                }
            }
        }
    }

    #region Fields

    public string Room1Task1Color {
        get => _room1Task1Color;
        set {
            _room1Task1Color = value;
            RaisePropertyChangedEvent("Room1Task1Color");
        }
    }

    public string Room2Task1Color {
        get => _room2Task1Color;
        set {
            _room2Task1Color = value;
            RaisePropertyChangedEvent("Room2Task1Color");
        }
    }

    public string Room2Task2Color {
        get => _room2Task2Color;
        set {
            _room2Task2Color = value;
            RaisePropertyChangedEvent("Room2Task2Color");
        }
    }

    public string Room2Task3Color {
        get => _room2Task3Color;
        set {
            _room2Task3Color = value;
            RaisePropertyChangedEvent("Room2Task3Color");
        }
    }

    public string Room2Task4Color {
        get => _room2Task4Color;
        set {
            _room2Task4Color = value;
            RaisePropertyChangedEvent("Room2Task4Color");
        }
    }

    public string Room3Task1Color {
        get => _room3Task1Color;
        set {
            _room3Task1Color = value;
            RaisePropertyChangedEvent("Room3Task1Color");
        }
    }

    public string Room4Task1Color {
        get => _room4Task1Color;
        set {
            _room4Task1Color = value;
            RaisePropertyChangedEvent("Room4Task1Color");
        }
    }

    public string Room4Task2Color {
        get => _room4Task2Color;
        set {
            _room4Task2Color = value;
            RaisePropertyChangedEvent("Room4Task2Color");
        }
    }

    public string Room4Task3Color {
        get => _room4Task3Color;
        set {
            _room4Task3Color = value;
            RaisePropertyChangedEvent("Room4Task3Color");
        }
    }

    public string Room4Task4Color {
        get => _room4Task4Color;
        set {
            _room4Task4Color = value;
            RaisePropertyChangedEvent("Room4Task4Color");
        }
    }

    public string Room4Task5Color {
        get => _room4Task5Color;
        set {
            _room4Task5Color = value;
            RaisePropertyChangedEvent("Room4Task5Color");
        }
    }

    public string Room4Task6Color {
        get => _room4Task6Color;
        set {
            _room4Task6Color = value;
            RaisePropertyChangedEvent("Room4Task6Color");
        }
    }

    public string Room4Task7Color {
        get => _room4Task7Color;
        set {
            _room4Task7Color = value;
            RaisePropertyChangedEvent("Room4Task7Color");
        }
    }

    public string Room6Task1Color {
        get => _room6Task1Color;
        set {
            _room6Task1Color = value;
            RaisePropertyChangedEvent("Room6Task1Color");
        }
    }

    public string Room6Task2Color {
        get => _room6Task2Color;
        set {
            _room6Task2Color = value;
            RaisePropertyChangedEvent("Room6Task2Color");
        }
    }

    public string Room6Task3Color {
        get => _room6Task3Color;
        set {
            _room6Task3Color = value;
            RaisePropertyChangedEvent("Room6Task3Color");
        }
    }

    public string Room6Task4Color {
        get => _room6Task4Color;
        set {
            _room6Task4Color = value;
            RaisePropertyChangedEvent("Room6Task4Color");
        }
    }

    public string Room7Task1Color {
        get => _room7Task1Color;
        set {
            _room7Task1Color = value;
            RaisePropertyChangedEvent("Room7Task1Color");
        }
    }

    public string Room9Task1Color {
        get => _room9Task1Color;
        set {
            _room9Task1Color = value;
            RaisePropertyChangedEvent("Room9Task1Color");
        }
    }

    public string Room10Task1Color {
        get => _room10Task1Color;
        set {
            _room10Task1Color = value;
            RaisePropertyChangedEvent("Room10Task1Color");
        }
    }

    public string Room11Task1Color {
        get => _room11Task1Color;
        set {
            _room11Task1Color = value;
            RaisePropertyChangedEvent("Room11Task1Color");
        }
    }

    public string Room11Task2Color {
        get => _room11Task2Color;
        set {
            _room11Task2Color = value;
            RaisePropertyChangedEvent("Room11Task2Color");
        }
    }

    public string Room1Task1DateText {
        get => _room1Task1DateText;
        set {
            _room1Task1DateText = value;
            RaisePropertyChangedEvent("Room1Task1DateText");
        }
    }

    public string Room2Task1DateText {
        get => _room2Task1DateText;
        set {
            _room2Task1DateText = value;
            RaisePropertyChangedEvent("Room2Task1DateText");
        }
    }

    public string Room2Task2DateText {
        get => _room2Task2DateText;
        set {
            _room2Task2DateText = value;
            RaisePropertyChangedEvent("Room2Task2DateText");
        }
    }

    public string Room2Task3DateText {
        get => _room2Task3DateText;
        set {
            _room2Task3DateText = value;
            RaisePropertyChangedEvent("Room2Task3DateText");
        }
    }

    public string Room2Task4DateText {
        get => _room2Task4DateText;
        set {
            _room2Task4DateText = value;
            RaisePropertyChangedEvent("Room2Task4DateText");
        }
    }

    public string Room3Task1DateText {
        get => _room3Task1DateText;
        set {
            _room3Task1DateText = value;
            RaisePropertyChangedEvent("Room3Task1DateText");
        }
    }

    public string Room4Task1DateText {
        get => _room4Task1DateText;
        set {
            _room4Task1DateText = value;
            RaisePropertyChangedEvent("Room4Task1DateText");
        }
    }

    public string Room4Task2DateText {
        get => _room4Task2DateText;
        set {
            _room4Task2DateText = value;
            RaisePropertyChangedEvent("Room4Task2DateText");
        }
    }

    public string Room4Task3DateText {
        get => _room4Task3DateText;
        set {
            _room4Task3DateText = value;
            RaisePropertyChangedEvent("Room4Task3DateText");
        }
    }

    public string Room4Task4DateText {
        get => _room4Task4DateText;
        set {
            _room4Task4DateText = value;
            RaisePropertyChangedEvent("Room4Task4DateText");
        }
    }

    public string Room4Task5DateText {
        get => _room4Task5DateText;
        set {
            _room4Task5DateText = value;
            RaisePropertyChangedEvent("Room4Task5DateText");
        }
    }

    public string Room4Task6DateText {
        get => _room4Task6DateText;
        set {
            _room4Task6DateText = value;
            RaisePropertyChangedEvent("Room4Task6DateText");
        }
    }

    public string Room4Task7DateText {
        get => _room4Task7DateText;
        set {
            _room4Task7DateText = value;
            RaisePropertyChangedEvent("Room4Task7DateText");
        }
    }

    public string Room6Task1DateText {
        get => _room6Task1DateText;
        set {
            _room6Task1DateText = value;
            RaisePropertyChangedEvent("Room6Task1DateText");
        }
    }

    public string Room6Task2DateText {
        get => _room6Task2DateText;
        set {
            _room6Task2DateText = value;
            RaisePropertyChangedEvent("Room6Task2DateText");
        }
    }

    public string Room6Task3DateText {
        get => _room6Task3DateText;
        set {
            _room6Task3DateText = value;
            RaisePropertyChangedEvent("Room6Task3DateText");
        }
    }

    public string Room6Task4DateText {
        get => _room6Task4DateText;
        set {
            _room6Task4DateText = value;
            RaisePropertyChangedEvent("Room6Task4DateText");
        }
    }

    public string Room7Task1DateText {
        get => _room7Task1DateText;
        set {
            _room7Task1DateText = value;
            RaisePropertyChangedEvent("Room7Task1DateText");
        }
    }

    public string Room9Task1DateText {
        get => _room9Task1DateText;
        set {
            _room9Task1DateText = value;
            RaisePropertyChangedEvent("Room9Task1DateText");
        }
    }

    public string Room10Task1DateText {
        get => _room10Task1DateText;
        set {
            _room10Task1DateText = value;
            RaisePropertyChangedEvent("Room10Task1DateText");
        }
    }

    public string Room11Task1DateText {
        get => _room11Task1DateText;
        set {
            _room11Task1DateText = value;
            RaisePropertyChangedEvent("Room11Task1DateText");
        }
    }

    public string Room11Task2DateText {
        get => _room11Task2DateText;
        set {
            _room11Task2DateText = value;
            RaisePropertyChangedEvent("Room11Task2DateText");
        }
    }

    #endregion
}