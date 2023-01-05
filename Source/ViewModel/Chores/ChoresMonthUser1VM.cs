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

public class ChoresMonthUser1VM : BaseViewModel {
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/choresUser1_month_" + ReferenceValues.ChoreMonthStartDate.ToString("yyyy_MM") + ".json";

    private string _room1Task1Color, _room4Task1Color, _room5Task1Color, _room1Task1DateText, _room4Task1DateText, _room5Task1DateText;

    public ChoresMonthUser1VM() {
        GetButtonColors();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "room1Task1":
            SwitchButtonLogic("Room1Task1");
            break;
        case "room4Task1":
            SwitchButtonLogic("Room4Task1");
            break;
        case "room5Task1":
            SwitchButtonLogic("Room5Task1");
            break;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreMonthUser1MasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
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
            }
        } else {
            ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreMonthUser1MasterList.choreList[index].Date = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreMonthUser1MasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreMonthUser1MasterList.choreList) {
                switch (choreDetails.Name) {
                case "Room1Task1":
                    Room1Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room1Task1DateText = choreDetails.Date;
                    break;
                case "Room4Task1":
                    Room4Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room4Task1DateText = choreDetails.Date;
                    break;
                case "Room5Task1":
                    Room5Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Room5Task1DateText = choreDetails.Date;
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

    public string Room5Task1Color {
        get => _room5Task1Color;
        set {
            _room5Task1Color = value;
            RaisePropertyChangedEvent("Room5Task1Color");
        }
    }

    public string Room4Task1Color {
        get => _room4Task1Color;
        set {
            _room4Task1Color = value;
            RaisePropertyChangedEvent("Room4Task1Color");
        }
    }

    public string Room1Task1DateText {
        get => _room1Task1DateText;
        set {
            _room1Task1DateText = value;
            RaisePropertyChangedEvent("Room1Task1DateText");
        }
    }

    public string Room4Task1DateText {
        get => _room4Task1DateText;
        set {
            _room4Task1DateText = value;
            RaisePropertyChangedEvent("Room4Task1DateText");
        }
    }

    public string Room5Task1DateText {
        get => _room5Task1DateText;
        set {
            _room5Task1DateText = value;
            RaisePropertyChangedEvent("Room5Task1DateText");
        }
    }

    #endregion
}