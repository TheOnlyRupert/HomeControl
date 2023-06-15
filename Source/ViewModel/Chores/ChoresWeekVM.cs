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

public class ChoresWeekVM : BaseViewModel {
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_week_" + ReferenceValues.ChoreWeekStartDate.ToString("yyyy_MM_dd") + ".json";

    private string _room1Task1Color, _room1Task2Color, _room1Task3Color, _room1Task4Color, _room1Task5Color, _room2Task1Color, _room2Task2Color, _room2Task3Color, _room2Task4Color,
        _room2Task5Color, _room2Task6Color, _room3Task1Color, _room3Task2Color, _room3Task3Color, _room3Task4Color, _room4Task1Color, _room4Task2Color, _room4Task3Color,
        _room4Task4Color, _room4Task5Color, _room5Task1Color, _room5Task2Color, _room5Task3Color, _room5Task4Color, _room5Task5Color, _room6Task1Color, _room6Task2Color,
        _room6Task3Color, _room6Task4Color, _room6Task5Color, _room6Task6Color, _room7Task1Color, _room7Task2Color, _room7Task3Color, _room7Task4Color, _room8Task1Color,
        _room8Task2Color, _room8Task3Color, _room8Task4Color, _room9Task1Color, _room9Task2Color, _room9Task3Color, _room9Task4Color, _room10Task1Color, _room10Task2Color,
        _room10Task3Color, _room10Task4Color, _room10Task5Color, _room1Task1DateText, _room1Task2DateText, _room1Task3DateText, _room1Task4DateText, _room1Task5DateText,
        _room2Task1DateText,
        _room2Task2DateText, _room2Task3DateText, _room2Task4DateText, _room2Task5DateText, _room2Task6DateText, _room3Task1DateText, _room3Task2DateText, _room3Task3DateText,
        _room3Task4DateText, _room4Task1DateText,
        _room4Task2DateText, _room4Task3DateText, _room4Task4DateText, _room4Task5DateText, _room5Task1DateText, _room5Task2DateText, _room5Task3DateText, _room5Task4DateText,
        _room5Task5DateText, _room6Task1DateText,
        _room6Task2DateText, _room6Task3DateText, _room6Task4DateText, _room6Task5DateText, _room6Task6DateText, _room7Task1DateText, _room7Task2DateText, _room7Task3DateText,
        _room7Task4DateText, _room8Task1DateText,
        _room8Task2DateText, _room8Task3DateText, _room8Task4DateText, _room9Task1DateText, _room9Task2DateText, _room9Task3DateText, _room9Task4DateText, _room10Task1DateText,
        _room10Task2DateText, _room10Task3DateText,
        _room10Task4DateText, _room10Task5DateText, _room11Task1DateText, _room11Task2DateText, _room12Task1DateText, _room12Task2DateText, _room11Task1Color, _room11Task2Color,
        _room12Task1Color, _room12Task2Color;

    private bool allowSound;

    public ChoresWeekVM() {
        allowSound = false;
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
        case "room2Task6":
            SwitchButtonLogic("Room2Task6");
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
        case "room5Task1":
            SwitchButtonLogic("Room5Task1");
            break;
        case "room5Task2":
            SwitchButtonLogic("Room5Task2");
            break;
        case "room5Task3":
            SwitchButtonLogic("Room5Task3");
            break;
        case "room5Task4":
            SwitchButtonLogic("Room5Task4");
            break;
        case "room5Task5":
            SwitchButtonLogic("Room5Task5");
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
        case "room6Task6":
            SwitchButtonLogic("Room6Task6");
            break;
        case "room7Task1":
            SwitchButtonLogic("Room7Task1");
            break;
        case "room7Task2":
            SwitchButtonLogic("Room7Task2");
            break;
        case "room7Task3":
            SwitchButtonLogic("Room7Task3");
            break;
        case "room7Task4":
            SwitchButtonLogic("Room7Task4");
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
        case "room9Task1":
            SwitchButtonLogic("Room9Task1");
            break;
        case "room9Task2":
            SwitchButtonLogic("Room9Task2");
            break;
        case "room9Task3":
            SwitchButtonLogic("Room9Task3");
            break;
        case "room9Task4":
            SwitchButtonLogic("Room9Task4");
            break;
        case "room10Task1":
            SwitchButtonLogic("Room10Task1");
            break;
        case "room10Task2":
            SwitchButtonLogic("Room10Task2");
            break;
        case "room10Task3":
            SwitchButtonLogic("Room10Task3");
            break;
        case "room10Task4":
            SwitchButtonLogic("Room10Task4");
            break;
        case "room10Task5":
            SwitchButtonLogic("Room10Task5");
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
        }

        if (allowSound) {
            ReferenceValues.SoundToPlay = "achievement1";
            SoundDispatcher.PlaySound();
            allowSound = false;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreWeekMasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "ChoresWeekVM",
                Description = e.ToString()
            });
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreWeekMasterList.choreList.IndexOf(ReferenceValues.JsonChoreWeekMasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreWeekMasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreWeekMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreWeekMasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreWeekMasterList.choreList[index].Date = "";

                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "ChoresWeekVM",
                    Description = ReferenceValues.JsonMasterSettings.User2Name + " removed weekly task: " + value
                });
            }
        } else {
            ReferenceValues.JsonChoreWeekMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreWeekMasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreWeekMasterList.choreList[index].Date = DateTime.Now.ToString("yyyy-MM-dd");
            allowSound = true;

            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "INFO",
                Module = "ChoresWeekVM",
                Description = ReferenceValues.JsonMasterSettings.User2Name + " completed weekly task: " + value
            });
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreWeekMasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreWeekMasterList.choreList) {
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
                case "Room2Task6":
                    Room2Task6Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room2Task6DateText = choreDetails.Date;
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
                case "Room5Task4":
                    Room5Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room5Task4DateText = choreDetails.Date;
                    break;
                case "Room5Task5":
                    Room5Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room5Task5DateText = choreDetails.Date;
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
                case "Room6Task6":
                    Room6Task6Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room6Task6DateText = choreDetails.Date;
                    break;
                case "Room7Task1":
                    Room7Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room7Task1DateText = choreDetails.Date;
                    break;
                case "Room7Task2":
                    Room7Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room7Task2DateText = choreDetails.Date;
                    break;
                case "Room7Task3":
                    Room7Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room7Task3DateText = choreDetails.Date;
                    break;
                case "Room7Task4":
                    Room7Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room7Task4DateText = choreDetails.Date;
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
                case "Room9Task1":
                    Room9Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room9Task1DateText = choreDetails.Date;
                    break;
                case "Room9Task2":
                    Room9Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room9Task2DateText = choreDetails.Date;
                    break;
                case "Room9Task3":
                    Room9Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room9Task3DateText = choreDetails.Date;
                    break;
                case "Room9Task4":
                    Room9Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room9Task4DateText = choreDetails.Date;
                    break;
                case "Room10Task1":
                    Room10Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room10Task1DateText = choreDetails.Date;
                    break;
                case "Room10Task2":
                    Room10Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room10Task2DateText = choreDetails.Date;
                    break;
                case "Room10Task3":
                    Room10Task3Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room10Task3DateText = choreDetails.Date;
                    break;
                case "Room10Task4":
                    Room10Task4Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room10Task4DateText = choreDetails.Date;
                    break;
                case "Room10Task5":
                    Room10Task5Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room10Task5DateText = choreDetails.Date;
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

    public string Room2Task6Color {
        get => _room2Task6Color;
        set {
            _room2Task6Color = value;
            RaisePropertyChangedEvent("Room2Task6Color");
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

    public string Room5Task4Color {
        get => _room5Task4Color;
        set {
            _room5Task4Color = value;
            RaisePropertyChangedEvent("Room5Task4Color");
        }
    }

    public string Room5Task5Color {
        get => _room5Task5Color;
        set {
            _room5Task5Color = value;
            RaisePropertyChangedEvent("Room5Task5Color");
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

    public string Room6Task6Color {
        get => _room6Task6Color;
        set {
            _room6Task6Color = value;
            RaisePropertyChangedEvent("Room6Task6Color");
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

    public string Room7Task3Color {
        get => _room7Task3Color;
        set {
            _room7Task3Color = value;
            RaisePropertyChangedEvent("Room7Task3Color");
        }
    }

    public string Room7Task4Color {
        get => _room7Task4Color;
        set {
            _room7Task4Color = value;
            RaisePropertyChangedEvent("Room7Task4Color");
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

    public string Room9Task3Color {
        get => _room9Task3Color;
        set {
            _room9Task3Color = value;
            RaisePropertyChangedEvent("Room9Task3Color");
        }
    }

    public string Room9Task4Color {
        get => _room9Task4Color;
        set {
            _room9Task4Color = value;
            RaisePropertyChangedEvent("Room9Task4Color");
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

    public string Room10Task3Color {
        get => _room10Task3Color;
        set {
            _room10Task3Color = value;
            RaisePropertyChangedEvent("Room10Task3Color");
        }
    }

    public string Room10Task4Color {
        get => _room10Task4Color;
        set {
            _room10Task4Color = value;
            RaisePropertyChangedEvent("Room10Task4Color");
        }
    }

    public string Room10Task5Color {
        get => _room10Task5Color;
        set {
            _room10Task5Color = value;
            RaisePropertyChangedEvent("Room10Task5Color");
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

    public string Room2Task6DateText {
        get => _room2Task6DateText;
        set {
            _room2Task6DateText = value;
            RaisePropertyChangedEvent("Room2Task6DateText");
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

    public string Room5Task4DateText {
        get => _room5Task4DateText;
        set {
            _room5Task4DateText = value;
            RaisePropertyChangedEvent("Room5Task4DateText");
        }
    }

    public string Room5Task5DateText {
        get => _room5Task5DateText;
        set {
            _room5Task5DateText = value;
            RaisePropertyChangedEvent("Room5Task5DateText");
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

    public string Room6Task6DateText {
        get => _room6Task6DateText;
        set {
            _room6Task6DateText = value;
            RaisePropertyChangedEvent("Room6Task6DateText");
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

    public string Room7Task3DateText {
        get => _room7Task3DateText;
        set {
            _room7Task3DateText = value;
            RaisePropertyChangedEvent("Room7Task3DateText");
        }
    }

    public string Room7Task4DateText {
        get => _room7Task4DateText;
        set {
            _room7Task4DateText = value;
            RaisePropertyChangedEvent("Room7Task4DateText");
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

    public string Room9Task3DateText {
        get => _room9Task3DateText;
        set {
            _room9Task3DateText = value;
            RaisePropertyChangedEvent("Room9Task3DateText");
        }
    }

    public string Room9Task4DateText {
        get => _room9Task4DateText;
        set {
            _room9Task4DateText = value;
            RaisePropertyChangedEvent("Room9Task4DateText");
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

    public string Room10Task3DateText {
        get => _room10Task3DateText;
        set {
            _room10Task3DateText = value;
            RaisePropertyChangedEvent("Room10Task3DateText");
        }
    }

    public string Room10Task4DateText {
        get => _room10Task4DateText;
        set {
            _room10Task4DateText = value;
            RaisePropertyChangedEvent("Room10Task4DateText");
        }
    }

    public string Room10Task5DateText {
        get => _room10Task5DateText;
        set {
            _room10Task5DateText = value;
            RaisePropertyChangedEvent("Room10Task5DateText");
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

    #endregion
}