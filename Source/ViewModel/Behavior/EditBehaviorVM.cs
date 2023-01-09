using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Control;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Behavior;

public class EditBehaviorVM : BaseViewModel {
    private readonly PlaySound strikeSound, rewardSound;

    private string _childName, _childStar1, _childStar2, _childStar3, _childStar4, _childStar5, _childStrike1, _childStrike2, _childStrike3, _rewardButtonVisibility,
        _progressBarChildValueText;

    private int _progressBarChildValue, stars, strikes;

    public EditBehaviorVM() {
        strikeSound = new PlaySound("buzzer");
        rewardSound = new PlaySound("yay");

        switch (ReferenceValues.ActiveChild) {
        case 0:
            ChildName = ReferenceValues.JsonMasterSettings.User3Name;
            stars = ReferenceValues.JsonBehaviorMaster.Child1Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.Child1Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.Child1Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.Child1Progress + "/5";
            break;
        case 1:
            ChildName = ReferenceValues.JsonMasterSettings.User4Name;
            stars = ReferenceValues.JsonBehaviorMaster.Child2Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.Child2Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.Child2Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.Child2Progress + "/5";
            break;
        case 2:
            ChildName = ReferenceValues.JsonMasterSettings.User5Name;
            stars = ReferenceValues.JsonBehaviorMaster.Child3Stars;
            strikes = ReferenceValues.JsonBehaviorMaster.Child3Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehaviorMaster.Child3Progress;
            ProgressBarChildValueText = ReferenceValues.JsonBehaviorMaster.Child3Progress + "/5";
            break;
        }

        RefreshBehavior();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            ReferenceValues.JsonBehaviorMaster.Child1Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.Child2Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.Child3Strikes = 0;
            strikes = 0;
            RefreshBehavior();
        }
    }

    private void RefreshBehavior() {
        ChildStar1 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar2 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar3 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar4 = "../../../Resources/Images/behavior/star_black.png";
        ChildStar5 = "../../../Resources/Images/behavior/star_black.png";
        ChildStrike1 = "../../../Resources/Images/behavior/strike_black.png";
        ChildStrike2 = "../../../Resources/Images/behavior/strike_black.png";
        ChildStrike3 = "../../../Resources/Images/behavior/strike_black.png";

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

        /* Save Progress */
        switch (ReferenceValues.ActiveChild) {
        case 0:
            ReferenceValues.JsonBehaviorMaster.Child1Stars = stars;
            ReferenceValues.JsonBehaviorMaster.Child1Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.Child1Progress = ProgressBarChildValue;
            break;
        case 1:
            ReferenceValues.JsonBehaviorMaster.Child2Stars = stars;
            ReferenceValues.JsonBehaviorMaster.Child2Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.Child2Progress = ProgressBarChildValue;
            break;
        case 2:
            ReferenceValues.JsonBehaviorMaster.Child3Stars = stars;
            ReferenceValues.JsonBehaviorMaster.Child3Strikes = strikes;
            ReferenceValues.JsonBehaviorMaster.Child3Progress = ProgressBarChildValue;
            break;
        }

        RewardButtonVisibility = stars == 5 ? "VISIBLE" : "HIDDEN";

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonBehaviorMaster);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "behavior.json", jsonString);
        } catch (Exception e) {
            Console.WriteLine("Unable to save " + ReferenceValues.FILE_DIRECTORY + "behavior.json" + "... " + e.Message);
        }
    }

    private void ButtonLogic(object param) {
        MessageBoxResult result;

        switch (param) {
        case "addStar":
            if (stars < 5) {
                result = MessageBox.Show("Are you sure you want to add a star?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) {
                    stars++;
                }
            }

            break;
        case "removeStar":
            if (stars > 0) {
                result = MessageBox.Show("Are you sure you want to remove a star?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) {
                    stars--;
                }
            }

            break;
        case "addStrike":
            if (strikes < 3) {
                result = MessageBox.Show("Are you sure you want to add a strike?\nThis will reset all progress (but not stars)", "Confirmation", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) {
                    strikes++;
                    ProgressBarChildValue = 0;
                    ProgressBarChildValueText = "0/5";
                    strikeSound.Play(false);

                    if (strikes == 3) {
                        stars--;
                        if (stars < 0) {
                            stars = 0;
                        }
                    }
                }
            }

            break;
        case "removeStrike":
            if (strikes > 0) {
                result = MessageBox.Show("Are you sure you want to remove a strike?\nNote: Strikes get removed at midnight", "Confirmation", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) {
                    strikes--;
                }
            }

            break;

        case "add1":
            if (strikes != 3) {
                ProgressBarChildValue++;
                ProgressBarChildValueText = ProgressBarChildValue + "/5";
                if (ProgressBarChildValue > 4) {
                    if (stars < 5) {
                        stars++;
                        ProgressBarChildValue = 0;
                        ProgressBarChildValueText = "0/5";
                    } else {
                        ProgressBarChildValue = 5;
                        ProgressBarChildValueText = "5/5";
                    }
                }
            }

            break;
        case "remove1":
            if (strikes != 3) {
                ProgressBarChildValue--;
                ProgressBarChildValueText = ProgressBarChildValue + "/5";
                if (ProgressBarChildValue < 0) {
                    if (stars > 0) {
                        stars--;
                        ProgressBarChildValue = 4;
                        ProgressBarChildValueText = "4/5";
                    } else {
                        ProgressBarChildValue = 0;
                        ProgressBarChildValueText = "0/5";
                    }
                }
            }

            break;
        case "reward":
            if (stars == 5) {
                stars = 0;
                ProgressBarChildValue = 0;
                ProgressBarChildValueText = "0/5";
                RewardButtonVisibility = "HIDDEN";
                rewardSound.Play(false);
            }

            break;
        }

        RefreshBehavior();
    }

    #region Fields

    public string ChildName {
        get => _childName;
        set {
            _childName = value;
            RaisePropertyChangedEvent("ChildName");
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

    #endregion
}