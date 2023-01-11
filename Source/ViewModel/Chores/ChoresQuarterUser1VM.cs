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

public class ChoresQuarterUser1VM : BaseViewModel {
    private readonly PlaySound completeSound;
    private readonly string fileName;

    private string _room1Task1Color, _room1Task2Color, _room1Task3Color, _room2Task1Color, _room2Task2Color, _room3Task1Color, _room3Task2Color, _room4Task1Color, _room4Task2Color,
        _room4Task3Color, _room5Task1Color, _room5Task2Color, _room5Task3Color, _room6Task1Color, _room6Task2Color, _room7Task1Color, _room7Task2Color, _room8Task1Color,
        _room8Task2Color, _room9Task1Color, _room9Task2Color, _room10Task1Color, _room10Task2Color, _room11Task1Color, _room11Task2Color, _room12Task1Color, _room12Task2Color,
        _room16Task1Color, _room16Task2Color, _room1Task3DateText, _room2Task1DateText, _room2Task2DateText, _room3Task1DateText, _room3Task2DateText, _room4Task1DateText,
        _room4Task2DateText, _room4Task3DateText, _room5Task1DateText, _room5Task2DateText, _room5Task3DateText, _room6Task1DateText, _room6Task2DateText, _room7Task1DateText,
        _room7Task2DateText, _room8Task1DateText, _room8Task2DateText, _room9Task1DateText, _room9Task2DateText, _room10Task1DateText, _room10Task2DateText, _room11Task1DateText,
        _room11Task2DateText, _room12Task1DateText, _room12Task2DateText, _room16Task1DateText, _room16Task2DateText, _room1Task1DateText, _room1Task2DateText;

    public ChoresQuarterUser1VM() {
        fileName = ReferenceValues.ChoreMonthStartDate.Month switch {
            > 0 and < 3 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter1User1_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy") + ".json",
            > 2 and < 6 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter2User1_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy") + ".json",
            > 5 and < 9 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter3User1_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy") + ".json",
            _ => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter4User1_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy") + ".json"
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
        case "room1Task2":
            SwitchButtonLogic("Room1Task2");
            break;
        case "room1Task3":
            SwitchButtonLogic("Room1Task3");
            break;
        case "room2Task1":
            SwitchButtonLogic("Room2Task1");
            break;
        case "room2Task2":
            SwitchButtonLogic("Room2Task2");
            break;
        case "room3Task1":
            SwitchButtonLogic("Room3Task1");
            break;
        case "room3Task2":
            SwitchButtonLogic("Room3Task2");
            break;
        case "room3Task3":
            SwitchButtonLogic("Room3Task3");
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
        case "room5Task1":
            SwitchButtonLogic("Room5Task1");
            break;
        case "room5Task2":
            SwitchButtonLogic("Room5Task2");
            break;
        case "room5Task3":
            SwitchButtonLogic("Room5Task3");
            break;
        case "room6Task1":
            SwitchButtonLogic("Room6Task1");
            break;
        case "room6Task2":
            SwitchButtonLogic("Room6Task2");
            break;
        case "room7Task1":
            SwitchButtonLogic("Room7Task1");
            break;
        case "room7Task2":
            SwitchButtonLogic("Room7Task2");
            break;
        case "room8Task1":
            SwitchButtonLogic("Room8Task1");
            break;
        case "room8Task2":
            SwitchButtonLogic("Room8Task2");
            break;
        case "room9Task1":
            SwitchButtonLogic("Room9Task1");
            break;
        case "room9Task2":
            SwitchButtonLogic("Room9Task2");
            break;
        case "room10Task1":
            SwitchButtonLogic("Room10Task1");
            break;
        case "room10Task2":
            SwitchButtonLogic("Room10Task2");
            break;
        case "room11Task1":
            SwitchButtonLogic("Room11Task1");
            break;
        case "room11Task2":
            SwitchButtonLogic("Room11Task2");
            break;
        case "room12Task1":
            SwitchButtonLogic("Room12Task1");
            break;
        case "room12Task2":
            SwitchButtonLogic("Room12Task2");
            break;
        case "room16Task1":
            SwitchButtonLogic("Room16Task1");
            break;
        case "room16Task2":
            SwitchButtonLogic("Room16Task2");
            break;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreQuarterUser1MasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreQuarterUser1MasterList.choreList.IndexOf(ReferenceValues.JsonChoreQuarterUser1MasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreQuarterUser1MasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreQuarterUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreQuarterUser1MasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreQuarterUser1MasterList.choreList[index].Date = "";
            }
        } else {
            ReferenceValues.JsonChoreQuarterUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreQuarterUser1MasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreQuarterUser1MasterList.choreList[index].Date = DateTime.Now.ToString("yyyy-MM-dd");
            completeSound.Play(false);
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreQuarterUser1MasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreQuarterUser1MasterList.choreList) {
                switch (choreDetails.Name) {
                case "Room1Task1":
                    Room1Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task1DateText = choreDetails.Date;
                    break;
                case "Room1Task2":
                    Room1Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task2DateText = choreDetails.Date;
                    break;
                case "Room1Task3":
                    Room1Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task3DateText = choreDetails.Date;
                    break;
                case "Room2Task1":
                    Room2Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room2Task1DateText = choreDetails.Date;
                    break;
                case "Room2Task2":
                    Room2Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room2Task2DateText = choreDetails.Date;
                    break;
                case "Room3Task1":
                    Room3Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task1DateText = choreDetails.Date;
                    break;
                case "Room3Task2":
                    Room3Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task2DateText = choreDetails.Date;
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
                case "Room5Task1":
                    Room5Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room5Task1DateText = choreDetails.Date;
                    break;
                case "Room5Task2":
                    Room5Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room5Task2DateText = choreDetails.Date;
                    break;
                case "Room5Task3":
                    Room5Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room5Task3DateText = choreDetails.Date;
                    break;
                case "Room6Task1":
                    Room6Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task1DateText = choreDetails.Date;
                    break;
                case "Room6Task2":
                    Room6Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task2DateText = choreDetails.Date;
                    break;
                case "Room7Task1":
                    Room7Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room7Task1DateText = choreDetails.Date;
                    break;
                case "Room7Task2":
                    Room7Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room7Task2DateText = choreDetails.Date;
                    break;
                case "Room8Task1":
                    Room8Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room8Task1DateText = choreDetails.Date;
                    break;
                case "Room8Task2":
                    Room8Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room8Task2DateText = choreDetails.Date;
                    break;
                case "Room9Task1":
                    Room9Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room9Task1DateText = choreDetails.Date;
                    break;
                case "Room9Task2":
                    Room9Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room9Task2DateText = choreDetails.Date;
                    break;
                case "Room10Task1":
                    Room10Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room10Task1DateText = choreDetails.Date;
                    break;
                case "Room10Task2":
                    Room10Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room10Task2DateText = choreDetails.Date;
                    break;
                case "Room11Task1":
                    Room11Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room11Task1DateText = choreDetails.Date;
                    break;
                case "Room11Task2":
                    Room11Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room11Task2DateText = choreDetails.Date;
                    break;
                case "Room12Task1":
                    Room12Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room12Task1DateText = choreDetails.Date;
                    break;
                case "Room12Task2":
                    Room12Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room12Task2DateText = choreDetails.Date;
                    break;
                case "Room16Task1":
                    Room16Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room16Task1DateText = choreDetails.Date;
                    break;
                case "Room16Task2":
                    Room16Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room16Task2DateText = choreDetails.Date;
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

    public string Room1Task2Color {
        get => _room1Task2Color;
        set {
            _room1Task2Color = value;
            RaisePropertyChangedEvent("Room1Task2Color");
        }
    }

    public string Room1Task3Color {
        get => _room1Task3Color;
        set {
            _room1Task3Color = value;
            RaisePropertyChangedEvent("Room1Task3Color");
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

    public string Room3Task1Color {
        get => _room3Task1Color;
        set {
            _room3Task1Color = value;
            RaisePropertyChangedEvent("Room3Task1Color");
        }
    }

    public string Room3Task2Color {
        get => _room3Task2Color;
        set {
            _room3Task2Color = value;
            RaisePropertyChangedEvent("Room3Task2Color");
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

    public string Room5Task1Color {
        get => _room5Task1Color;
        set {
            _room5Task1Color = value;
            RaisePropertyChangedEvent("Room5Task1Color");
        }
    }

    public string Room5Task2Color {
        get => _room5Task2Color;
        set {
            _room5Task2Color = value;
            RaisePropertyChangedEvent("Room5Task2Color");
        }
    }

    public string Room5Task3Color {
        get => _room5Task3Color;
        set {
            _room5Task3Color = value;
            RaisePropertyChangedEvent("Room5Task3Color");
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

    public string Room7Task1Color {
        get => _room7Task1Color;
        set {
            _room7Task1Color = value;
            RaisePropertyChangedEvent("Room7Task1Color");
        }
    }

    public string Room7Task2Color {
        get => _room7Task2Color;
        set {
            _room7Task2Color = value;
            RaisePropertyChangedEvent("Room7Task2Color");
        }
    }

    public string Room8Task1Color {
        get => _room8Task1Color;
        set {
            _room8Task1Color = value;
            RaisePropertyChangedEvent("Room8Task1Color");
        }
    }

    public string Room8Task2Color {
        get => _room8Task2Color;
        set {
            _room8Task2Color = value;
            RaisePropertyChangedEvent("Room8Task2Color");
        }
    }

    public string Room9Task1Color {
        get => _room9Task1Color;
        set {
            _room9Task1Color = value;
            RaisePropertyChangedEvent("Room9Task1Color");
        }
    }

    public string Room9Task2Color {
        get => _room9Task2Color;
        set {
            _room9Task2Color = value;
            RaisePropertyChangedEvent("Room9Task2Color");
        }
    }

    public string Room10Task1Color {
        get => _room10Task1Color;
        set {
            _room10Task1Color = value;
            RaisePropertyChangedEvent("Room10Task1Color");
        }
    }

    public string Room10Task2Color {
        get => _room10Task2Color;
        set {
            _room10Task2Color = value;
            RaisePropertyChangedEvent("Room10Task2Color");
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

    public string Room12Task1Color {
        get => _room12Task1Color;
        set {
            _room12Task1Color = value;
            RaisePropertyChangedEvent("Room12Task1Color");
        }
    }

    public string Room12Task2Color {
        get => _room12Task2Color;
        set {
            _room12Task2Color = value;
            RaisePropertyChangedEvent("Room12Task2Color");
        }
    }

    public string Room16Task1Color {
        get => _room16Task1Color;
        set {
            _room16Task1Color = value;
            RaisePropertyChangedEvent("Room16Task1Color");
        }
    }

    public string Room16Task2Color {
        get => _room16Task2Color;
        set {
            _room16Task2Color = value;
            RaisePropertyChangedEvent("Room16Task2Color");
        }
    }

    public string Room1Task1DateText {
        get => _room1Task1DateText;
        set {
            _room1Task1DateText = value;
            RaisePropertyChangedEvent("Room1Task1DateText");
        }
    }

    public string Room1Task2DateText {
        get => _room1Task2DateText;
        set {
            _room1Task2DateText = value;
            RaisePropertyChangedEvent("Room1Task2DateText");
        }
    }

    public string Room1Task3DateText {
        get => _room1Task3DateText;
        set {
            _room1Task3DateText = value;
            RaisePropertyChangedEvent("Room1Task3DateText");
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

    public string Room3Task1DateText {
        get => _room3Task1DateText;
        set {
            _room3Task1DateText = value;
            RaisePropertyChangedEvent("Room3Task1DateText");
        }
    }

    public string Room3Task2DateText {
        get => _room3Task2DateText;
        set {
            _room3Task2DateText = value;
            RaisePropertyChangedEvent("Room3Task2DateText");
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

    public string Room5Task1DateText {
        get => _room5Task1DateText;
        set {
            _room5Task1DateText = value;
            RaisePropertyChangedEvent("Room5Task1DateText");
        }
    }

    public string Room5Task2DateText {
        get => _room5Task2DateText;
        set {
            _room5Task2DateText = value;
            RaisePropertyChangedEvent("Room5Task2DateText");
        }
    }

    public string Room5Task3DateText {
        get => _room5Task3DateText;
        set {
            _room5Task3DateText = value;
            RaisePropertyChangedEvent("Room5Task3DateText");
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

    public string Room7Task1DateText {
        get => _room7Task1DateText;
        set {
            _room7Task1DateText = value;
            RaisePropertyChangedEvent("Room7Task1DateText");
        }
    }

    public string Room7Task2DateText {
        get => _room7Task2DateText;
        set {
            _room7Task2DateText = value;
            RaisePropertyChangedEvent("Room7Task2DateText");
        }
    }

    public string Room8Task1DateText {
        get => _room8Task1DateText;
        set {
            _room8Task1DateText = value;
            RaisePropertyChangedEvent("Room8Task1DateText");
        }
    }

    public string Room8Task2DateText {
        get => _room8Task2DateText;
        set {
            _room8Task2DateText = value;
            RaisePropertyChangedEvent("Room8Task2DateText");
        }
    }

    public string Room9Task1DateText {
        get => _room9Task1DateText;
        set {
            _room9Task1DateText = value;
            RaisePropertyChangedEvent("Room9Task1DateText");
        }
    }

    public string Room9Task2DateText {
        get => _room9Task2DateText;
        set {
            _room9Task2DateText = value;
            RaisePropertyChangedEvent("Room9Task2DateText");
        }
    }

    public string Room10Task1DateText {
        get => _room10Task1DateText;
        set {
            _room10Task1DateText = value;
            RaisePropertyChangedEvent("Room10Task1DateText");
        }
    }

    public string Room10Task2DateText {
        get => _room10Task2DateText;
        set {
            _room10Task2DateText = value;
            RaisePropertyChangedEvent("Room10Task2DateText");
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

    public string Room12Task1DateText {
        get => _room12Task1DateText;
        set {
            _room12Task1DateText = value;
            RaisePropertyChangedEvent("Room12Task1DateText");
        }
    }

    public string Room12Task2DateText {
        get => _room12Task2DateText;
        set {
            _room12Task2DateText = value;
            RaisePropertyChangedEvent("Room12Task2DateText");
        }
    }

    public string Room16Task1DateText {
        get => _room16Task1DateText;
        set {
            _room16Task1DateText = value;
            RaisePropertyChangedEvent("Room16Task1DateText");
        }
    }

    public string Room16Task2DateText {
        get => _room16Task2DateText;
        set {
            _room16Task2DateText = value;
            RaisePropertyChangedEvent("Room16Task2DateText");
        }
    }

    #endregion
}