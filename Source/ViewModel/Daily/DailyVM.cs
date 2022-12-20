using System;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Daily;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Daily;

public class DailyVM : BaseViewModel {
    private string _button1BackgroundColor, _button1NameText, _button1CompletedText, _button1ProgressValueText, _button2BackgroundColor, _button2NameText,
        _button2CompletedText, _button2ProgressValueText, _button3BackgroundColor, _button3NameText, _button3CompletedText,
        _button3ProgressValueText, _button4BackgroundColor, _button4NameText, _button4CompletedText, _button4ProgressValueText, _button5BackgroundColor,
        _button5NameText, _button5CompletedText, _button5ProgressValueText;

    private int _button1ProgressValue, _button2ProgressValue, _button3ProgressValue, _button4ProgressValue, _button5ProgressValue;

    private int[] tasksCompleted;

    public DailyVM() {
        tasksCompleted = new int[5];

        try {
            Button1NameText = ReferenceValues.JsonMasterSettings.User1Name;
            Button2NameText = ReferenceValues.JsonMasterSettings.User2Name;
            Button3NameText = ReferenceValues.JsonMasterSettings.User3Name;
            Button4NameText = ReferenceValues.JsonMasterSettings.User4Name;
            Button5NameText = ReferenceValues.JsonMasterSettings.User5Name;
        } catch (Exception) { }

        RefreshFields();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            RefreshFields();
        }
    }

    private void ButtonLogic(object param) {
        EditDaily editDaily = null;

        switch (param) {
        case "button1":
            ReferenceValues.ActiveUserEdit = 0;
            editDaily = new EditDaily();
            break;
        case "button2":
            ReferenceValues.ActiveUserEdit = 1;
            editDaily = new EditDaily();
            break;
        case "button3":
            ReferenceValues.ActiveUserEdit = 2;
            editDaily = new EditDaily();
            break;
        case "button4":
            ReferenceValues.ActiveUserEdit = 3;
            editDaily = new EditDaily();
            break;
        case "button5":
            ReferenceValues.ActiveUserEdit = 4;
            editDaily = new EditDaily();
            break;
        }

        if (editDaily != null) {
            editDaily.ShowDialog();
            editDaily.Close();
        }

        RefreshFields();
    }

    private void RefreshFields() {
        tasksCompleted = new int[5];
        DailyFromJson dailyFromJson = new();
        dailyFromJson.DailyDayFromJson(DateTime.Now);

        foreach (DailyDetails daily in ReferenceValues.JsonDailyMasterList.dailyListUser1) {
            switch (daily.Name) {
            case "User1Task1": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            case "User1Task2": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User1Task3": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            case "User1Task4": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            case "User1Task5": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            case "User1Task6": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            case "User1Task7": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            case "User1Task8": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            case "User1Task9": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            case "User1Task10": {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }

                break;
            }
            }
        }

        foreach (DailyDetails daily in ReferenceValues.JsonDailyMasterList.dailyListUser2) {
            switch (daily.Name) {
            case "User2Task1": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task2": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task3": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task4": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task5": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task6": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task7": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task8": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task9": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            case "User2Task10": {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }

                break;
            }
            }
        }

        Button1CompletedText = tasksCompleted[0] + "/" + ReferenceValues.JsonDailyMasterList.dailyListUser1.Count;
        Button2CompletedText = tasksCompleted[1] + "/" + ReferenceValues.JsonDailyMasterList.dailyListUser2.Count;
        Button3CompletedText = tasksCompleted[2] + "/" + ReferenceValues.JsonDailyMasterList.dailyListUser3.Count;
        Button4CompletedText = tasksCompleted[3] + "/" + ReferenceValues.JsonDailyMasterList.dailyListUser4.Count;
        Button5CompletedText = tasksCompleted[4] + "/" + ReferenceValues.JsonDailyMasterList.dailyListUser5.Count;

        double progress = Convert.ToDouble(tasksCompleted[0]) / Convert.ToDouble(ReferenceValues.JsonDailyMasterList.dailyListUser1.Count) * 100;
        try {
            Button1ProgressValue = Convert.ToInt16(progress);
            Button1ProgressValueText = Button1ProgressValue + "%";
        } catch (Exception) { }

        progress = Convert.ToDouble(tasksCompleted[1]) / Convert.ToDouble(ReferenceValues.JsonDailyMasterList.dailyListUser2.Count) * 100;
        try {
            Button2ProgressValue = Convert.ToInt16(progress);
            Button2ProgressValueText = Button2ProgressValue + "%";
        } catch (Exception) { }

        progress = Convert.ToDouble(tasksCompleted[2]) / Convert.ToDouble(ReferenceValues.JsonDailyMasterList.dailyListUser3.Count) * 100;
        try {
            Button3ProgressValue = Convert.ToInt16(progress);
            Button3ProgressValueText = Button3ProgressValue + "%";
        } catch (Exception) { }

        progress = Convert.ToDouble(tasksCompleted[3]) / Convert.ToDouble(ReferenceValues.JsonDailyMasterList.dailyListUser4.Count) * 100;
        try {
            Button4ProgressValue = Convert.ToInt16(progress);
            Button4ProgressValueText = Button4ProgressValue + "%";
        } catch (Exception) { }

        progress = Convert.ToDouble(tasksCompleted[4]) / Convert.ToDouble(ReferenceValues.JsonDailyMasterList.dailyListUser5.Count) * 100;
        try {
            Button5ProgressValue = Convert.ToInt16(progress);
            Button5ProgressValueText = Button5ProgressValue + "%";
        } catch (Exception) { }

        Button1BackgroundColor = tasksCompleted[0] == ReferenceValues.JsonDailyMasterList.dailyListUser1.Count ? "Green" : "Transparent";
        Button2BackgroundColor = tasksCompleted[1] == ReferenceValues.JsonDailyMasterList.dailyListUser2.Count ? "Green" : "Transparent";
        Button3BackgroundColor = tasksCompleted[2] == ReferenceValues.JsonDailyMasterList.dailyListUser3.Count ? "Green" : "Transparent";
        Button4BackgroundColor = tasksCompleted[3] == ReferenceValues.JsonDailyMasterList.dailyListUser4.Count ? "Green" : "Transparent";
        Button5BackgroundColor = tasksCompleted[4] == ReferenceValues.JsonDailyMasterList.dailyListUser5.Count ? "Green" : "Transparent";
    }

    #region Fields

    public string Button1BackgroundColor {
        get => _button1BackgroundColor;
        set {
            _button1BackgroundColor = value;
            RaisePropertyChangedEvent("Button1BackgroundColor");
        }
    }

    public string Button1NameText {
        get => _button1NameText;
        set {
            _button1NameText = value;
            RaisePropertyChangedEvent("Button1NameText");
        }
    }

    public string Button1CompletedText {
        get => _button1CompletedText;
        set {
            _button1CompletedText = value;
            RaisePropertyChangedEvent("Button1CompletedText");
        }
    }

    public int Button1ProgressValue {
        get => _button1ProgressValue;
        set {
            _button1ProgressValue = value;
            RaisePropertyChangedEvent("Button1ProgressValue");
        }
    }

    public string Button1ProgressValueText {
        get => _button1ProgressValueText;
        set {
            _button1ProgressValueText = value;
            RaisePropertyChangedEvent("Button1ProgressValueText");
        }
    }

    public string Button2BackgroundColor {
        get => _button2BackgroundColor;
        set {
            _button2BackgroundColor = value;
            RaisePropertyChangedEvent("Button2BackgroundColor");
        }
    }

    public string Button2NameText {
        get => _button2NameText;
        set {
            _button2NameText = value;
            RaisePropertyChangedEvent("Button2NameText");
        }
    }

    public string Button2CompletedText {
        get => _button2CompletedText;
        set {
            _button2CompletedText = value;
            RaisePropertyChangedEvent("Button2CompletedText");
        }
    }

    public int Button2ProgressValue {
        get => _button2ProgressValue;
        set {
            _button2ProgressValue = value;
            RaisePropertyChangedEvent("Button2ProgressValue");
        }
    }

    public string Button2ProgressValueText {
        get => _button2ProgressValueText;
        set {
            _button2ProgressValueText = value;
            RaisePropertyChangedEvent("Button2ProgressValueText");
        }
    }

    public string Button3BackgroundColor {
        get => _button3BackgroundColor;
        set {
            _button3BackgroundColor = value;
            RaisePropertyChangedEvent("Button3BackgroundColor");
        }
    }

    public string Button3NameText {
        get => _button3NameText;
        set {
            _button3NameText = value;
            RaisePropertyChangedEvent("Button3NameText");
        }
    }

    public string Button3CompletedText {
        get => _button3CompletedText;
        set {
            _button3CompletedText = value;
            RaisePropertyChangedEvent("Button3CompletedText");
        }
    }

    public int Button3ProgressValue {
        get => _button3ProgressValue;
        set {
            _button3ProgressValue = value;
            RaisePropertyChangedEvent("Button3ProgressValue");
        }
    }

    public string Button3ProgressValueText {
        get => _button3ProgressValueText;
        set {
            _button3ProgressValueText = value;
            RaisePropertyChangedEvent("Button3ProgressValueText");
        }
    }

    public string Button4BackgroundColor {
        get => _button4BackgroundColor;
        set {
            _button4BackgroundColor = value;
            RaisePropertyChangedEvent("Button4BackgroundColor");
        }
    }

    public string Button4NameText {
        get => _button4NameText;
        set {
            _button4NameText = value;
            RaisePropertyChangedEvent("Button4NameText");
        }
    }

    public string Button4CompletedText {
        get => _button4CompletedText;
        set {
            _button4CompletedText = value;
            RaisePropertyChangedEvent("Button4CompletedText");
        }
    }

    public int Button4ProgressValue {
        get => _button4ProgressValue;
        set {
            _button4ProgressValue = value;
            RaisePropertyChangedEvent("Button4ProgressValue");
        }
    }

    public string Button4ProgressValueText {
        get => _button4ProgressValueText;
        set {
            _button4ProgressValueText = value;
            RaisePropertyChangedEvent("Button4ProgressValueText");
        }
    }

    public string Button5BackgroundColor {
        get => _button5BackgroundColor;
        set {
            _button5BackgroundColor = value;
            RaisePropertyChangedEvent("Button5BackgroundColor");
        }
    }

    public string Button5NameText {
        get => _button5NameText;
        set {
            _button5NameText = value;
            RaisePropertyChangedEvent("Button5NameText");
        }
    }

    public string Button5CompletedText {
        get => _button5CompletedText;
        set {
            _button5CompletedText = value;
            RaisePropertyChangedEvent("Button5CompletedText");
        }
    }

    public int Button5ProgressValue {
        get => _button5ProgressValue;
        set {
            _button5ProgressValue = value;
            RaisePropertyChangedEvent("Button5ProgressValue");
        }
    }

    public string Button5ProgressValueText {
        get => _button5ProgressValueText;
        set {
            _button5ProgressValueText = value;
            RaisePropertyChangedEvent("Button5ProgressValueText");
        }
    }

    #endregion
}