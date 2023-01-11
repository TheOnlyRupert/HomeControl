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

public class ChoresSpecialVM : BaseViewModel {
    private readonly PlaySound completeSound;
    private readonly string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_special_" + DateTime.Now.ToString("yyyy_MM") + ".json";

    private string _Task1Color, _Task2Color, _Task1DateText, _Task2DateText;

    public ChoresSpecialVM() {
        completeSound = new PlaySound("achievement1");
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
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonChoreSpecialMasterList);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(fileName, jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
        }

        GetButtonColors();
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonChoreSpecialMasterList.choreList.IndexOf(ReferenceValues.JsonChoreSpecialMasterList.choreList.First(i => i.Name == value));
        if (ReferenceValues.JsonChoreSpecialMasterList.choreList[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonChoreSpecialMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreSpecialMasterList.choreList[index].IsComplete;
                ReferenceValues.JsonChoreSpecialMasterList.choreList[index].Date = "";
            }
        } else {
            ReferenceValues.JsonChoreSpecialMasterList.choreList[index].IsComplete = !ReferenceValues.JsonChoreSpecialMasterList.choreList[index].IsComplete;
            ReferenceValues.JsonChoreSpecialMasterList.choreList[index].Date = DateTime.Now.ToString("yyyy-MM-dd");
            completeSound.Play(false);
        }
    }

    private void GetButtonColors() {
        if (ReferenceValues.JsonChoreSpecialMasterList != null) {
            foreach (ChoreDetails choreDetails in ReferenceValues.JsonChoreSpecialMasterList.choreList) {
                switch (choreDetails.Name) {
                case "Task1":
                    Task1Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Task1DateText = choreDetails.Date;
                    break;
                case "Task2":
                    Task2Color = choreDetails.IsComplete ? "Green" : "Transparent";
                    Task2DateText = choreDetails.Date;
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

    #endregion
}