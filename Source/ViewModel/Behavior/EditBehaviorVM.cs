using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Behavior;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Behavior;

public class EditBehaviorVM : BaseViewModel {
    private BitmapImage _imageUser;

    private int _progressBarChildValue, stars, strikes;

    private string _rewardButtonVisibility, _progressBarChildValueText, _childName, _cashAvailable, _cashAvailableColor, _currentMonthText, _currentWeekText, _currentDayText,
        _currentQuarterText, _remainingDay, _remainingWeek, _remainingMonth, _remainingQuarter, _remainingYear, _childStar1, _childStar2,
        _childStar3, _childStar4, _childStar5, _childStrike1, _childStrike2, _childStrike3;

    public EditBehaviorVM() {
        DateTimeFormatInfo dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        System.Globalization.Calendar calendar = dateTimeFormatInfo.Calendar;
        CurrentDayText = DateTime.Now.ToString("dddd");
        CurrentMonthText = DateTime.Now.ToString("MMMM");
        CurrentWeekText = "Week " + calendar.GetWeekOfYear(DateTime.Now, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);
        CurrentQuarterText = DateTime.Now.Month switch {
            > 0 and < 3 => "Quarter 1",
            > 2 and < 6 => "Quarter 2",
            > 5 and < 9 => "Quarter 3",
            _ => "Quarter 4"
        };

        switch (ReferenceValues.ActiveBehaviorUser) {
        case 1:
            ChildName = ReferenceValues.JsonMasterSettings.User1Name;
            stars = ReferenceValues.JsonBehaviorMaster.User1Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User1Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User1Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User1Progress + "/5";
            try {
                Uri uri1 = new(ReferenceValues.FILE_DIRECTORY + "icons/user1.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri1);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }

            break;
        case 2:
            ChildName = ReferenceValues.JsonMasterSettings.User2Name;
            stars = ReferenceValues.JsonBehaviorMaster.User2Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User2Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User2Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User2Progress + "/5";
            try {
                Uri uri2 = new(ReferenceValues.FILE_DIRECTORY + "icons/user2.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri2);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }

            break;
        case 3:
            ChildName = ReferenceValues.JsonMasterSettings.User3Name;
            stars = ReferenceValues.JsonBehaviorMaster.User3Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User3Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User3Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User3Progress + "/5";
            try {
                Uri uri3 = new(ReferenceValues.FILE_DIRECTORY + "icons/user3.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri3);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }

            break;
        case 4:
            ChildName = ReferenceValues.JsonMasterSettings.User4Name;
            stars = ReferenceValues.JsonBehaviorMaster.User4Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User4Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User4Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User4Progress + "/5";
            try {
                Uri uri4 = new(ReferenceValues.FILE_DIRECTORY + "icons/user4.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri4);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }

            break;
        case 5:
            ChildName = ReferenceValues.JsonMasterSettings.User5Name;
            stars = ReferenceValues.JsonBehaviorMaster.User5Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.User5Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.User5Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.User5Progress + "/5";
            try {
                Uri uri5 = new(ReferenceValues.FILE_DIRECTORY + "icons/.png", UriKind.RelativeOrAbsolute);
                ImageUser = new BitmapImage(uri5);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "EditBehaviorVM",
                    Description = e.ToString()
                });
                SaveDebugFile.Save();
            }

            break;
        }

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;

        RefreshBehavior();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            ReferenceValues.JsonBehaviorMaster.User1Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User2Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User3Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User4Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User5Strikes = 0;
            strikes = 0;
            RefreshBehavior();
        }
    }

    private void RefreshBehavior() {
        ReferenceValues.JsonBehaviorMaster.Date = DateTime.Now;
        ChildStar1 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar2 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar3 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar4 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar5 = "../../../Resources/Images/behavior/star_black.png";
        ChildStrike1 = "";
        ChildStrike2 = "";
        ChildStrike3 = "";

        switch (stars) {
        case 1:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar2 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar2 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar3 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            ChildStar1 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar2 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar3 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar4 = "../../../Resources/Images/behavior/star_gold.png";
            ChildStar5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (strikes) {
        case 1:
            ChildStrike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            ChildStrike1 = "../../../Resources/Images/behavior/strike_red.png";
            ChildStrike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            ChildStrike1 = "../../../Resources/Images/behavior/strike_red.png";
            ChildStrike2 = "../../../Resources/Images/behavior/strike_red.png";
            ChildStrike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        RewardButtonVisibility = stars == 5 ? "VISIBLE" : "HIDDEN";

        /* Save Progress */
        switch (ReferenceValues.ActiveBehaviorUser) {
        case 1:
            ReferenceValues.JsonBehaviorMaster.User1Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User1Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User1Progress = ProgressBarChildValue;
            break;
        case 2:
            ReferenceValues.JsonBehaviorMaster.User2Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User2Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User2Progress = ProgressBarChildValue;
            break;
        case 3:
            ReferenceValues.JsonBehaviorMaster.User3Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User3Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User3Progress = ProgressBarChildValue;
            break;
        case 4:
            ReferenceValues.JsonBehaviorMaster.User4Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User4Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User4Progress = ProgressBarChildValue;
            break;
        case 5:
            ReferenceValues.JsonBehaviorMaster.User5Stars = stars;
            ReferenceValues.JsonBehaviorMaster.User5Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.User5Progress = ProgressBarChildValue;
            break;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonBehaviorMaster);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "behavior.json", jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditBehaviorVM",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }
    }

    private void ButtonLogic(object param) {
        if (!ReferenceValues.LockUI) {
            switch (param) {
            case "addStrike":
                if (RewardButtonVisibility == "HIDDEN") {
                    if (strikes < 3) {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to add a strike?\nThis will reset all progress (but not stars)", "Confirmation",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes) {
                            strikes++;
                            ProgressBarChildValue = 0;
                            ProgressBarChildValueText = "0/5";
                            ReferenceValues.SoundToPlay = "buzzer";
                            SoundDispatcher.PlaySound();
                            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditBehaviorVM",
                                Description = "Adding strike to " + ChildName
                            });
                            SaveDebugFile.Save();

                            if (strikes == 3) {
                                stars--;
                                if (stars < 0) {
                                    stars = 0;
                                }
                            }
                        }
                    } else {
                        ReferenceValues.SoundToPlay = "unable";
                        SoundDispatcher.PlaySound();
                    }
                }

                break;
            case "add1":
                if (RewardButtonVisibility == "HIDDEN") {
                    if (strikes != 3) {
                        ProgressBarChildValue++;
                        ProgressBarChildValueText = ProgressBarChildValue + "/5";
                        if (ProgressBarChildValue > 4) {
                            if (stars < 5) {
                                stars++;
                                ProgressBarChildValue = 0;
                                ProgressBarChildValueText = "0/5";

                                if (stars == 5) {
                                    ProgressBarChildValue = 5;
                                    ProgressBarChildValueText = "5/5";
                                }

                                ReferenceValues.SoundToPlay = "yay";
                                SoundDispatcher.PlaySound();
                                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                                    Date = DateTime.Now,
                                    Level = "INFO",
                                    Module = "EditBehaviorVM",
                                    Description = "Adding progress (which resulted in a star) to " + ChildName
                                });
                                SaveDebugFile.Save();
                            } else {
                                ProgressBarChildValue = 5;
                                ProgressBarChildValueText = "5/5";
                            }
                        } else {
                            ReferenceValues.SoundToPlay = "ding";
                            SoundDispatcher.PlaySound();
                            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditBehaviorVM",
                                Description = "Adding progress to " + ChildName
                            });
                            SaveDebugFile.Save();
                        }
                    } else {
                        ReferenceValues.SoundToPlay = "unable";
                        SoundDispatcher.PlaySound();
                    }
                }

                break;
            case "remove1":
                if (RewardButtonVisibility == "HIDDEN" && strikes != 3) {
                    ProgressBarChildValue--;
                    ProgressBarChildValueText = ProgressBarChildValue + "/5";
                    if (ProgressBarChildValue < 0) {
                        if (stars > 0) {
                            stars--;
                            ProgressBarChildValue = 4;
                            ProgressBarChildValueText = "4/5";
                            ReferenceValues.SoundToPlay = "aww";
                            SoundDispatcher.PlaySound();
                            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                                Date = DateTime.Now,
                                Level = "INFO",
                                Module = "EditBehaviorVM",
                                Description = "Removing progress (which resulted in a loss of a star) from " + ChildName
                            });
                            SaveDebugFile.Save();
                        } else {
                            ProgressBarChildValue = 0;
                            ProgressBarChildValueText = "0/5";
                        }
                    } else {
                        ReferenceValues.SoundToPlay = "error";
                        SoundDispatcher.PlaySound();
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "INFO",
                            Module = "EditBehaviorVM",
                            Description = "Removing progress from " + ChildName
                        });
                        SaveDebugFile.Save();
                    }
                } else {
                    ReferenceValues.SoundToPlay = "unable";
                    SoundDispatcher.PlaySound();
                }

                break;
            case "reward":
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "EditBehaviorVM",
                    Description = ChildName + " claimed their reward!"
                });
                SaveDebugFile.Save();
                stars = 0;
                ProgressBarChildValue = 0;
                ProgressBarChildValueText = "0/5";
                RewardButtonVisibility = "HIDDEN";
                ReferenceValues.SoundToPlay = "reward";
                SoundDispatcher.PlaySound();

                break;
            case "daily":
                TasksDaily tasksDaily = new();
                tasksDaily.ShowDialog();
                tasksDaily.Close();

                break;
            }

            RefreshBehavior();
        }
    }

    #region Fields

    public string CurrentMonthText {
        get => _currentMonthText;
        set {
            _currentMonthText = value;
            RaisePropertyChangedEvent("CurrentMonthText");
        }
    }

    public string CurrentWeekText {
        get => _currentWeekText;
        set {
            _currentWeekText = value;
            RaisePropertyChangedEvent("CurrentWeekText");
        }
    }

    public string CurrentDayText {
        get => _currentDayText;
        set {
            _currentDayText = value;
            RaisePropertyChangedEvent("CurrentDayText");
        }
    }


    public string CurrentQuarterText {
        get => _currentQuarterText;
        set {
            _currentQuarterText = value;
            RaisePropertyChangedEvent("CurrentQuarterText");
        }
    }

    public string RemainingDay {
        get => _remainingDay;
        set {
            _remainingDay = value;
            RaisePropertyChangedEvent("RemainingDay");
        }
    }

    public string RemainingWeek {
        get => _remainingWeek;
        set {
            _remainingWeek = value;
            RaisePropertyChangedEvent("RemainingWeek");
        }
    }

    public string RemainingMonth {
        get => _remainingMonth;
        set {
            _remainingMonth = value;
            RaisePropertyChangedEvent("RemainingMonth");
        }
    }

    public string RemainingQuarter {
        get => _remainingQuarter;
        set {
            _remainingQuarter = value;
            RaisePropertyChangedEvent("RemainingQuarter");
        }
    }

    public string RemainingYear {
        get => _remainingYear;
        set {
            _remainingYear = value;
            RaisePropertyChangedEvent("RemainingYear");
        }
    }

    public string ChildName {
        get => _childName;
        set {
            _childName = value;
            RaisePropertyChangedEvent("ChildName");
        }
    }

    public int ProgressBarChildValue {
        get => _progressBarChildValue;
        set {
            _progressBarChildValue = value;
            RaisePropertyChangedEvent("ProgressBarChildValue");
        }
    }

    public string ProgressBarChildValueText {
        get => _progressBarChildValueText;
        set {
            _progressBarChildValueText = value;
            RaisePropertyChangedEvent("ProgressBarChildValueText");
        }
    }

    public string RewardButtonVisibility {
        get => _rewardButtonVisibility;
        set {
            _rewardButtonVisibility = value;
            RaisePropertyChangedEvent("RewardButtonVisibility");
        }
    }

    public BitmapImage ImageUser {
        get => _imageUser;
        set {
            _imageUser = value;
            RaisePropertyChangedEvent("ImageUser");
        }
    }

    public string CashAvailable {
        get => _cashAvailable;
        set {
            _cashAvailable = value;
            RaisePropertyChangedEvent("CashAvailable");
        }
    }

    public string CashAvailableColor {
        get => _cashAvailableColor;
        set {
            _cashAvailableColor = value;
            RaisePropertyChangedEvent("CashAvailableColor");
        }
    }

    public string ChildStar1 {
        get => _childStar1;
        set {
            _childStar1 = value;
            RaisePropertyChangedEvent("ChildStar1");
        }
    }

    public string ChildStar2 {
        get => _childStar2;
        set {
            _childStar2 = value;
            RaisePropertyChangedEvent("ChildStar2");
        }
    }

    public string ChildStar3 {
        get => _childStar3;
        set {
            _childStar3 = value;
            RaisePropertyChangedEvent("ChildStar3");
        }
    }

    public string ChildStar4 {
        get => _childStar4;
        set {
            _childStar4 = value;
            RaisePropertyChangedEvent("ChildStar4");
        }
    }

    public string ChildStar5 {
        get => _childStar5;
        set {
            _childStar5 = value;
            RaisePropertyChangedEvent("ChildStar5");
        }
    }

    public string ChildStrike1 {
        get => _childStrike1;
        set {
            _childStrike1 = value;
            RaisePropertyChangedEvent("ChildStrike1");
        }
    }

    public string ChildStrike2 {
        get => _childStrike2;
        set {
            _childStrike2 = value;
            RaisePropertyChangedEvent("ChildStrike2");
        }
    }

    public string ChildStrike3 {
        get => _childStrike3;
        set {
            _childStrike3 = value;
            RaisePropertyChangedEvent("ChildStrike3");
        }
    }

    #endregion
}