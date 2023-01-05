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

public class ChoresWeekUser1VM : BaseViewModel {
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/choresUser1_week_" + ReferenceValues.ChoreWeekStartDate.ToString("yyyy_MM_dd") + ".json";

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
        _room10Task4DateText, _room10Task5DateText;

    public ChoresWeekUser1VM() {
        GetButtonColors();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "room1Task1":
            SwitchButtonLogic("Room1Task1");
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
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreWeekUser1MasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreWeekUser1MasterList.choreList) {
                switch (choreDetails.Name) {
                case "Room1Task1":
                    Room1Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task1DateText = choreDetails.Date;
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

    #endregion
}