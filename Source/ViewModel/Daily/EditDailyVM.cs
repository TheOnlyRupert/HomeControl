using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Daily;

public class EditDailyVM : BaseViewModel {
    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "button1":
            SwitchButtonLogic("User1Task1");
            break;
        case "button2":
            SwitchButtonLogic("User1Task2");
            break;
        case "button3":
            SwitchButtonLogic("User1Task3");
            break;
        case "button4":
            SwitchButtonLogic("User1Task4");
            break;
        }
    }

    private void SwitchButtonLogic(string value) {
        int index = ReferenceValues.JsonDailyMasterList.dailyListUser1.IndexOf(ReferenceValues.JsonDailyMasterList.dailyListUser1.First(i => i.Name == value));
        if (ReferenceValues.JsonDailyMasterList.dailyListUser1[index].IsComplete) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset this chore?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes) {
                ReferenceValues.JsonDailyMasterList.dailyListUser1[index].IsComplete = !ReferenceValues.JsonDailyMasterList.dailyListUser1[index].IsComplete;
                ReferenceValues.JsonDailyMasterList.dailyListUser1[index].Time = "";
            }
        } else {
            ReferenceValues.JsonDailyMasterList.dailyListUser1[index].IsComplete = !ReferenceValues.JsonDailyMasterList.dailyListUser1[index].IsComplete;
            ReferenceValues.JsonDailyMasterList.dailyListUser1[index].Time = DateTime.Now.ToString("HH:mm");
        }
    }
}