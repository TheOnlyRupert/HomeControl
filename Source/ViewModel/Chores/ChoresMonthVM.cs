using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Chores;

public class ChoresMonthVM : BaseViewModel {
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_month_" + ReferenceValues.ChoreWeekStartDate.ToString("yyyy_MM") + ".json";

    private string _room1Task1Color, _room1Task2Color, _room1Task3Color, _room1Task4Color, _room1Task5Color, _room1Task6Color, _room1Task7Color, _room1Task8Color, _room1Task9Color,
        _room1Task10Color, _room2Task1Color, _room2Task2Color, _room2Task3Color, _room2Task4Color, _room2Task5Color, _room3Task1Color, _room3Task2Color, _room3Task3Color,
        _room3Task4Color,
        _room3Task5Color, _room3Task6Color, _room3Task7Color, _room4Task1Color, _room4Task2Color, _room4Task3Color, _room4Task4Color, _room4Task5Color, _room4Task6Color,
        _room4Task7Color, _room5Task1Color, _room5Task2Color, _room5Task3Color, _room6Task1Color, _room6Task2Color, _room6Task3Color, _room6Task4Color, _room6Task5Color,
        _room7Task1Color, _room7Task2Color, _room8Task1Color, _room8Task2Color, _room8Task3Color, _room8Task4Color, _room8Task5Color, _room1Task1DateText, _room1Task2DateText,
        _room1Task3DateText, _room1Task4DateText, _room1Task5DateText, _room1Task6DateText, _room1Task7DateText, _room1Task8DateText, _room1Task9DateText, _room1Task10DateText,
        _room2Task1DateText,
        _room2Task2DateText, _room2Task3DateText, _room2Task4DateText, _room2Task5DateText, _room3Task1DateText, _room3Task2DateText, _room3Task3DateText, _room3Task4DateText,
        _room3Task5DateText, _room3Task6DateText, _room3Task7DateText, _room4Task1DateText, _room4Task2DateText, _room4Task3DateText, _room4Task4DateText, _room4Task5DateText,
        _room4Task6DateText, _room4Task7DateText, _room5Task1DateText, _room5Task2DateText, _room5Task3DateText, _room6Task1DateText, _room6Task2DateText, _room6Task3DateText,
        _room6Task4DateText, _room6Task5DateText, _room7Task1DateText, _room7Task2DateText, _room8Task1DateText, _room8Task2DateText, _room8Task3DateText, _room8Task4DateText,
        _room8Task5DateText;

    public ChoresMonthVM() {
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
        case "room1Task4":
            SwitchButtonLogic("Room1Task4");
            break;
        case "room1Task5":
            SwitchButtonLogic("Room1Task5");
            break;
        case "room1Task6":
            SwitchButtonLogic("Room1Task6");
            break;
        case "room1Task7":
            SwitchButtonLogic("Room1Task7");
            break;
        case "room1Task8":
            SwitchButtonLogic("Room1Task8");
            break;
        case "room1Task9":
            SwitchButtonLogic("Room1Task9");
            break;
        case "room1Task10":
            SwitchButtonLogic("Room1Task10");
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
        case "room2Task5":
            SwitchButtonLogic("Room2Task5");
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
        case "room3Task4":
            SwitchButtonLogic("Room3Task4");
            break;
        case "room3Task5":
            SwitchButtonLogic("Room3Task5");
            break;
        case "room3Task6":
            SwitchButtonLogic("Room3Task6");
            break;
        case "room3Task7":
            SwitchButtonLogic("Room3Task7");
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
        case "room6Task3":
            SwitchButtonLogic("Room6Task3");
            break;
        case "room6Task4":
            SwitchButtonLogic("Room6Task4");
            break;
        case "room6Task5":
            SwitchButtonLogic("Room6Task5");
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
        case "room8Task3":
            SwitchButtonLogic("Room8Task3");
            break;
        case "room8Task4":
            SwitchButtonLogic("Room8Task4");
            break;
        case "room8Task5":
            SwitchButtonLogic("Room8Task5");
            break;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreMonthMasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreMonthMasterList.choreList.IndexOf(ReferenceValues.JsonChoreMonthMasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreMonthMasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this chore?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreMonthMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreMonthMasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreMonthMasterList.choreList[index].Date = "";
            }
        } else {
            ReferenceValues.JsonChoreMonthMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreMonthMasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreMonthMasterList.choreList[index].Date = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreMonthMasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreMonthMasterList.choreList) {
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
                case "Room1Task4":
                    Room1Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task4DateText = choreDetails.Date;
                    break;
                case "Room1Task5":
                    Room1Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task5DateText = choreDetails.Date;
                    break;
                case "Room1Task6":
                    Room1Task6Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task6DateText = choreDetails.Date;
                    break;
                case "Room1Task7":
                    Room1Task7Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task7DateText = choreDetails.Date;
                    break;
                case "Room1Task8":
                    Room1Task8Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task8DateText = choreDetails.Date;
                    break;
                case "Room1Task9":
                    Room1Task9Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task9DateText = choreDetails.Date;
                    break;
                case "Room1Task10":
                    Room1Task10Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task10DateText = choreDetails.Date;
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
                case "Room2Task5":
                    Room2Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room2Task5DateText = choreDetails.Date;
                    break;
                case "Room3Task1":
                    Room3Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task1DateText = choreDetails.Date;
                    break;
                case "Room3Task2":
                    Room3Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task2DateText = choreDetails.Date;
                    break;
                case "Room3Task3":
                    Room3Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task3DateText = choreDetails.Date;
                    break;
                case "Room3Task4":
                    Room3Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task4DateText = choreDetails.Date;
                    break;
                case "Room3Task5":
                    Room3Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task5DateText = choreDetails.Date;
                    break;
                case "Room3Task6":
                    Room3Task6Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task6DateText = choreDetails.Date;
                    break;
                case "Room3Task7":
                    Room3Task7Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room3Task7DateText = choreDetails.Date;
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
                case "Room6Task3":
                    Room6Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task3DateText = choreDetails.Date;
                    break;
                case "Room6Task4":
                    Room6Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task4DateText = choreDetails.Date;
                    break;
                case "Room6Task5":
                    Room6Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task5DateText = choreDetails.Date;
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
                case "Room8Task3":
                    Room8Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room8Task3DateText = choreDetails.Date;
                    break;
                case "Room8Task4":
                    Room8Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room8Task4DateText = choreDetails.Date;
                    break;
                case "Room8Task5":
                    Room8Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room8Task5DateText = choreDetails.Date;
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

    public string Room1Task4Color {
        get => _room1Task4Color;
        set {
            _room1Task4Color = value;
            RaisePropertyChangedEvent("Room1Task4Color");
        }
    }

    public string Room1Task6Color {
        get => _room1Task6Color;
        set {
            _room1Task6Color = value;
            RaisePropertyChangedEvent("Room1Task6Color");
        }
    }

    public string Room1Task7Color {
        get => _room1Task7Color;
        set {
            _room1Task7Color = value;
            RaisePropertyChangedEvent("Room1Task7Color");
        }
    }

    public string Room1Task8Color {
        get => _room1Task8Color;
        set {
            _room1Task8Color = value;
            RaisePropertyChangedEvent("Room1Task8Color");
        }
    }

    public string Room1Task9Color {
        get => _room1Task9Color;
        set {
            _room1Task9Color = value;
            RaisePropertyChangedEvent("Room1Task9Color");
        }
    }

    public string Room1Task10Color {
        get => _room1Task10Color;
        set {
            _room1Task10Color = value;
            RaisePropertyChangedEvent("Room1Task10Color");
        }
    }

    public string Room1Task5Color {
        get => _room1Task5Color;
        set {
            _room1Task5Color = value;
            RaisePropertyChangedEvent("Room1Task5Color");
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

    public string Room2Task5Color {
        get => _room2Task5Color;
        set {
            _room2Task5Color = value;
            RaisePropertyChangedEvent("Room2Task5Color");
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

    public string Room3Task3Color {
        get => _room3Task3Color;
        set {
            _room3Task3Color = value;
            RaisePropertyChangedEvent("Room3Task3Color");
        }
    }

    public string Room3Task4Color {
        get => _room3Task4Color;
        set {
            _room3Task4Color = value;
            RaisePropertyChangedEvent("Room3Task4Color");
        }
    }

    public string Room3Task5Color {
        get => _room3Task5Color;
        set {
            _room3Task5Color = value;
            RaisePropertyChangedEvent("Room3Task5Color");
        }
    }

    public string Room3Task6Color {
        get => _room3Task6Color;
        set {
            _room3Task6Color = value;
            RaisePropertyChangedEvent("Room3Task6Color");
        }
    }

    public string Room3Task7Color {
        get => _room3Task7Color;
        set {
            _room3Task7Color = value;
            RaisePropertyChangedEvent("Room3Task7Color");
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

    public string Room6Task5Color {
        get => _room6Task5Color;
        set {
            _room6Task5Color = value;
            RaisePropertyChangedEvent("Room6Task5Color");
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

    public string Room8Task3Color {
        get => _room8Task3Color;
        set {
            _room8Task3Color = value;
            RaisePropertyChangedEvent("Room8Task3Color");
        }
    }

    public string Room8Task4Color {
        get => _room8Task4Color;
        set {
            _room8Task4Color = value;
            RaisePropertyChangedEvent("Room8Task4Color");
        }
    }

    public string Room8Task5Color {
        get => _room8Task5Color;
        set {
            _room8Task5Color = value;
            RaisePropertyChangedEvent("Room8Task5Color");
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

    public string Room1Task4DateText {
        get => _room1Task4DateText;
        set {
            _room1Task4DateText = value;
            RaisePropertyChangedEvent("Room1Task4DateText");
        }
    }

    public string Room1Task6DateText {
        get => _room1Task6DateText;
        set {
            _room1Task6DateText = value;
            RaisePropertyChangedEvent("Room1Task6DateText");
        }
    }

    public string Room1Task7DateText {
        get => _room1Task7DateText;
        set {
            _room1Task7DateText = value;
            RaisePropertyChangedEvent("Room1Task7DateText");
        }
    }

    public string Room1Task8DateText {
        get => _room1Task8DateText;
        set {
            _room1Task8DateText = value;
            RaisePropertyChangedEvent("Room1Task8DateText");
        }
    }

    public string Room1Task9DateText {
        get => _room1Task9DateText;
        set {
            _room1Task9DateText = value;
            RaisePropertyChangedEvent("Room1Task9DateText");
        }
    }

    public string Room1Task10DateText {
        get => _room1Task10DateText;
        set {
            _room1Task10DateText = value;
            RaisePropertyChangedEvent("Room1Task10DateText");
        }
    }

    public string Room1Task5DateText {
        get => _room1Task5DateText;
        set {
            _room1Task5DateText = value;
            RaisePropertyChangedEvent("Room1Task5DateText");
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

    public string Room2Task5DateText {
        get => _room2Task5DateText;
        set {
            _room2Task5DateText = value;
            RaisePropertyChangedEvent("Room2Task5DateText");
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

    public string Room3Task3DateText {
        get => _room3Task3DateText;
        set {
            _room3Task3DateText = value;
            RaisePropertyChangedEvent("Room3Task3DateText");
        }
    }

    public string Room3Task4DateText {
        get => _room3Task4DateText;
        set {
            _room3Task4DateText = value;
            RaisePropertyChangedEvent("Room3Task4DateText");
        }
    }

    public string Room3Task5DateText {
        get => _room3Task5DateText;
        set {
            _room3Task5DateText = value;
            RaisePropertyChangedEvent("Room3Task5DateText");
        }
    }

    public string Room3Task6DateText {
        get => _room3Task6DateText;
        set {
            _room3Task6DateText = value;
            RaisePropertyChangedEvent("Room3Task6DateText");
        }
    }

    public string Room3Task7DateText {
        get => _room3Task7DateText;
        set {
            _room3Task7DateText = value;
            RaisePropertyChangedEvent("Room3Task7DateText");
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

    public string Room6Task5DateText {
        get => _room6Task5DateText;
        set {
            _room6Task5DateText = value;
            RaisePropertyChangedEvent("Room6Task5DateText");
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

    public string Room8Task3DateText {
        get => _room8Task3DateText;
        set {
            _room8Task3DateText = value;
            RaisePropertyChangedEvent("Room8Task3DateText");
        }
    }

    public string Room8Task4DateText {
        get => _room8Task4DateText;
        set {
            _room8Task4DateText = value;
            RaisePropertyChangedEvent("Room8Task4DateText");
        }
    }

    public string Room8Task5DateText {
        get => _room8Task5DateText;
        set {
            _room8Task5DateText = value;
            RaisePropertyChangedEvent("Room8Task5DateText");
        }
    }

    #endregion
}