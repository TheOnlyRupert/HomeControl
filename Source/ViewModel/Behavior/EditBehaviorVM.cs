﻿using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Behavior;

public class EditBehaviorVM : BaseViewModel {
    private string _childName, _childStar1, _childStar2, _childStar3, _childStar4, _childStar5, _childStrike1, _childStrike2, _childStrike3;
    private int _progressBarChildValue, stars, strikes;

    public EditBehaviorVM() {
        ChildName = ReferenceValues.ChildName[ReferenceValues.ActiveChild];

        switch (ReferenceValues.ActiveChild) {
        case 0:
            stars = ReferenceValues.JsonBehavior.Child1Stars;
            strikes = ReferenceValues.JsonBehavior.Child1Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehavior.Child1Progress;
            break;
        case 1:
            stars = ReferenceValues.JsonBehavior.Child2Stars;
            strikes = ReferenceValues.JsonBehavior.Child2Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehavior.Child2Progress;
            break;
        case 2:
            stars = ReferenceValues.JsonBehavior.Child3Stars;
            strikes = ReferenceValues.JsonBehavior.Child3Strikes;
            ProgressBarChildValue = ReferenceValues.JsonBehavior.Child3Progress;
            break;
        }

        RefreshBehavior();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            ReferenceValues.JsonBehavior.Child1Strikes = 0;
            ReferenceValues.JsonBehavior.Child2Strikes = 0;
            ReferenceValues.JsonBehavior.Child3Strikes = 0;
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
            ReferenceValues.JsonBehavior.Child1Stars = stars;
            ReferenceValues.JsonBehavior.Child1Strikes = strikes;
            ReferenceValues.JsonBehavior.Child1Progress = ProgressBarChildValue;
            break;
        case 1:
            ReferenceValues.JsonBehavior.Child2Stars = stars;
            ReferenceValues.JsonBehavior.Child2Strikes = strikes;
            ReferenceValues.JsonBehavior.Child2Progress = ProgressBarChildValue;
            break;
        case 2:
            ReferenceValues.JsonBehavior.Child3Stars = stars;
            ReferenceValues.JsonBehavior.Child3Strikes = strikes;
            ReferenceValues.JsonBehavior.Child3Progress = ProgressBarChildValue;
            break;
        }

        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonBehavior);
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
            ProgressBarChildValue++;
            if (ProgressBarChildValue > 99) {
                if (stars < 5) {
                    stars++;
                    ProgressBarChildValue = 0;
                } else {
                    ProgressBarChildValue = 100;
                }
            }

            break;
        case "add10":
            ProgressBarChildValue += 10;
            if (ProgressBarChildValue > 99) {
                if (stars < 5) {
                    stars++;
                    ProgressBarChildValue = 0;
                } else {
                    ProgressBarChildValue = 100;
                }
            }

            break;
        case "add25":
            ProgressBarChildValue += 25;
            if (ProgressBarChildValue > 99) {
                if (stars < 5) {
                    stars++;
                    ProgressBarChildValue = 0;
                } else {
                    ProgressBarChildValue = 100;
                }
            }

            break;

        case "remove1":
            ProgressBarChildValue--;
            if (ProgressBarChildValue < 0) {
                if (stars > 0) {
                    stars--;
                    ProgressBarChildValue = 90;
                } else {
                    ProgressBarChildValue = 0;
                }
            }

            break;
        case "remove10":
            ProgressBarChildValue -= 10;
            if (ProgressBarChildValue < 0) {
                if (stars > 0) {
                    stars--;
                    ProgressBarChildValue = 90;
                } else {
                    ProgressBarChildValue = 0;
                }
            }

            break;
        case "remove25":
            ProgressBarChildValue -= 25;
            if (ProgressBarChildValue < 0) {
                if (stars > 0) {
                    stars--;
                    ProgressBarChildValue = 90;
                } else {
                    ProgressBarChildValue = 0;
                }
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

    #endregion
}