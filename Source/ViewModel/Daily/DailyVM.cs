using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Daily;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Daily;

public class DailyVM : BaseViewModel {
    private string _button1BackgroundColor, _button1NameText, _button1CompletedText, _button1ProgressValue, _button1ProgressValueText, _button2BackgroundColor, _button2NameText,
        _button2CompletedText, _button2ProgressValue, _button2ProgressValueText, _button3BackgroundColor, _button3NameText, _button3CompletedText, _button3ProgressValue,
        _button3ProgressValueText, _button4BackgroundColor, _button4NameText, _button4CompletedText, _button4ProgressValue, _button4ProgressValueText, _button5BackgroundColor,
        _button5NameText, _button5CompletedText, _button5ProgressValue, _button5ProgressValueText;

    private int[] tasksCompleted;

    public DailyVM() {
        tasksCompleted = new int[5];

        try {
            Button1NameText = ReferenceValues.User1Name;
            Button2NameText = ReferenceValues.User2Name;
            Button3NameText = ReferenceValues.ChildName[0];
            Button4NameText = ReferenceValues.ChildName[1];
            Button5NameText = ReferenceValues.ChildName[2];
            Button1CompletedText = "0/2";
            Button2CompletedText = "0/3";
            Button3CompletedText = "0/1";
            Button4CompletedText = "None";
            Button5CompletedText = "None";
            Button1ProgressValue = "0";
            Button2ProgressValue = "0";
            Button3ProgressValue = "0";
            Button4ProgressValue = "100";
            Button5ProgressValue = "100";
            Button1ProgressValueText = "0%";
            Button2ProgressValueText = "0%";
            Button3ProgressValueText = "0%";
            Button4ProgressValueText = "100%";
            Button5ProgressValueText = "100%";
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
        switch (param) {
        case "button1":
            EditDaily editDaily = new();
            editDaily.ShowDialog();
            editDaily.Close();

            RefreshFields();
            break;
        }
    }

    private void RefreshFields() {
        tasksCompleted = new int[5];
        DailyFromJson dailyFromJson = new();
        dailyFromJson.DailyDayFromJson(DateTime.Now);

        ReferenceValues.JsonDailyMasterList = new JsonDaily {
            dailyList = new ObservableCollection<DailyDetails>()
        };

        foreach (DailyDetails daily in ReferenceValues.JsonDailyMasterList.dailyList) {
            if (daily.Name == "User1Task1") {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }
            }

            if (daily.Name == "User1Task2") {
                if (daily.IsComplete) {
                    tasksCompleted[0]++;
                }
            }

            if (daily.Name == "User2Task1") {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }
            }

            if (daily.Name == "User2Task2") {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }
            }

            if (daily.Name == "User2Task3") {
                if (daily.IsComplete) {
                    tasksCompleted[1]++;
                }
            }

            if (daily.Name == "User3Task1") {
                if (daily.IsComplete) {
                    tasksCompleted[2]++;
                }
            }
        }
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

    public string Button1ProgressValue {
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

    public string Button2ProgressValue {
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

    public string Button3ProgressValue {
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

    public string Button4ProgressValue {
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

    public string Button5ProgressValue {
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