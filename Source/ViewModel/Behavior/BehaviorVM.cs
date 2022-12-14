using System;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Behavior;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Behavior;

public class BehaviorVM : BaseViewModel {
    private string _child1Name, _child2Name, _child3Name, _child1Star1, _child1Star2, _child1Star3, _child1Star4, _child1Star5, _child2Star1, _child2Star2, _child2Star3,
        _child2Star4, _child2Star5, _child3Star1, _child3Star2, _child3Star3, _child3Star4, _child3Star5, _child1Strike1, _child1Strike2, _child1Strike3, _child2Strike1,
        _child2Strike2, _child2Strike3, _child3Strike1, _child3Strike2, _child3Strike3;

    private int _progressBarChild1Value, _progressBarChild2Value, _progressBarChild3Value;

    public BehaviorVM() {
        Child1Name = ReferenceValues.ChildName[0];
        Child2Name = ReferenceValues.ChildName[1];
        Child3Name = ReferenceValues.ChildName[2];

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;

        new BehaviorFromJson();

        /* Remove strikes if program was closed before midnight */
        try {
            if (!ReferenceValues.JsonBehaviorMaster.Date.Day.Equals(DateTime.Now.Day)) {
                ReferenceValues.JsonBehaviorMaster.Child1Strikes = 0;
                ReferenceValues.JsonBehaviorMaster.Child2Strikes = 0;
                ReferenceValues.JsonBehaviorMaster.Child3Strikes = 0;
            }
        } catch (Exception) { }

        RefreshBehavior();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void RefreshBehavior() {
        ReferenceValues.JsonBehaviorMaster.Date = DateTime.Now;
        Child1Star1 = "../../../Resources/Images/behavior/star_black.png";
        Child1Star2 = "../../../Resources/Images/behavior/star_black.png";
        Child1Star3 = "../../../Resources/Images/behavior/star_black.png";
        Child1Star4 = "../../../Resources/Images/behavior/star_black.png";
        Child1Star5 = "../../../Resources/Images/behavior/star_black.png";
        Child2Star1 = "../../../Resources/Images/behavior/star_black.png";
        Child2Star2 = "../../../Resources/Images/behavior/star_black.png";
        Child2Star3 = "../../../Resources/Images/behavior/star_black.png";
        Child2Star4 = "../../../Resources/Images/behavior/star_black.png";
        Child2Star5 = "../../../Resources/Images/behavior/star_black.png";
        Child3Star1 = "../../../Resources/Images/behavior/star_black.png";
        Child3Star2 = "../../../Resources/Images/behavior/star_black.png";
        Child3Star3 = "../../../Resources/Images/behavior/star_black.png";
        Child3Star4 = "../../../Resources/Images/behavior/star_black.png";
        Child3Star5 = "../../../Resources/Images/behavior/star_black.png";

        Child1Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        Child1Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        Child1Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        Child2Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        Child2Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        Child2Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        Child3Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        Child3Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        Child3Strike3 = "../../../Resources/Images/behavior/strike_black.png";

        switch (ReferenceValues.JsonBehaviorMaster.Child1Stars) {
        case 1:
            Child1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            Child1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            Child1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            Child1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            Child1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star4 = "../../../Resources/Images/behavior/star_gold.png";
            Child1Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.Child2Stars) {
        case 1:
            Child2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            Child2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            Child2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            Child2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            Child2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star4 = "../../../Resources/Images/behavior/star_gold.png";
            Child2Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.Child3Stars) {
        case 1:
            Child3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            Child3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            Child3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            Child3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star3 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            Child3Star1 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star2 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star3 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star4 = "../../../Resources/Images/behavior/star_gold.png";
            Child3Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.Child1Strikes) {
        case 1:
            Child1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            Child1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            Child1Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            Child1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            Child1Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            Child1Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.Child2Strikes) {
        case 1:
            Child2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            Child2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            Child2Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            Child2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            Child2Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            Child2Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.Child3Strikes) {
        case 1:
            Child3Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            Child3Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            Child3Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            Child3Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            Child3Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            Child3Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        ProgressBarChild1Value = ReferenceValues.JsonBehaviorMaster.Child1Progress;
        ProgressBarChild2Value = ReferenceValues.JsonBehaviorMaster.Child2Progress;
        ProgressBarChild3Value = ReferenceValues.JsonBehaviorMaster.Child3Progress;
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            ReferenceValues.JsonBehaviorMaster.Child1Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.Child2Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.Child3Strikes = 0;
            RefreshBehavior();
        }
    }

    private void ButtonLogic(object param) {
        switch (param) {
        case "child1":
            ReferenceValues.ActiveChild = 0;
            break;
        case "child2":
            ReferenceValues.ActiveChild = 1;
            break;
        case "child3":
            ReferenceValues.ActiveChild = 2;
            break;
        }

        EditBehavior editBehavior = new();
        editBehavior.ShowDialog();
        editBehavior.Close();
        RefreshBehavior();
    }

    #region Fields

    public string Child1Name {
        get => _child1Name;
        set {
            _child1Name = value;
            RaisePropertyChangedEvent("Child1Name");
        }
    }

    public string Child2Name {
        get => _child2Name;
        set {
            _child2Name = value;
            RaisePropertyChangedEvent("Child2Name");
        }
    }

    public string Child3Name {
        get => _child3Name;
        set {
            _child3Name = value;
            RaisePropertyChangedEvent("Child3Name");
        }
    }

    public string Child1Star1 {
        get => _child1Star1;
        set {
            _child1Star1 = value;
            RaisePropertyChangedEvent("Child1Star1");
        }
    }

    public string Child1Star2 {
        get => _child1Star2;
        set {
            _child1Star2 = value;
            RaisePropertyChangedEvent("Child1Star2");
        }
    }

    public string Child1Star3 {
        get => _child1Star3;
        set {
            _child1Star3 = value;
            RaisePropertyChangedEvent("Child1Star3");
        }
    }

    public string Child1Star4 {
        get => _child1Star4;
        set {
            _child1Star4 = value;
            RaisePropertyChangedEvent("Child1Star4");
        }
    }

    public string Child1Star5 {
        get => _child1Star5;
        set {
            _child1Star5 = value;
            RaisePropertyChangedEvent("Child1Star5");
        }
    }

    public string Child2Star1 {
        get => _child2Star1;
        set {
            _child2Star1 = value;
            RaisePropertyChangedEvent("Child2Star1");
        }
    }

    public string Child2Star2 {
        get => _child2Star2;
        set {
            _child2Star2 = value;
            RaisePropertyChangedEvent("Child2Star2");
        }
    }

    public string Child2Star3 {
        get => _child2Star3;
        set {
            _child2Star3 = value;
            RaisePropertyChangedEvent("Child2Star3");
        }
    }

    public string Child2Star4 {
        get => _child2Star4;
        set {
            _child2Star4 = value;
            RaisePropertyChangedEvent("Child2Star4");
        }
    }

    public string Child2Star5 {
        get => _child2Star5;
        set {
            _child2Star5 = value;
            RaisePropertyChangedEvent("Child2Star5");
        }
    }

    public string Child3Star1 {
        get => _child3Star1;
        set {
            _child3Star1 = value;
            RaisePropertyChangedEvent("Child3Star1");
        }
    }

    public string Child3Star2 {
        get => _child3Star2;
        set {
            _child3Star2 = value;
            RaisePropertyChangedEvent("Child3Star2");
        }
    }

    public string Child3Star3 {
        get => _child3Star3;
        set {
            _child3Star3 = value;
            RaisePropertyChangedEvent("Child3Star3");
        }
    }

    public string Child3Star4 {
        get => _child3Star4;
        set {
            _child3Star4 = value;
            RaisePropertyChangedEvent("Child3Star4");
        }
    }

    public string Child3Star5 {
        get => _child3Star5;
        set {
            _child3Star5 = value;
            RaisePropertyChangedEvent("Child3Star5");
        }
    }

    public string Child1Strike1 {
        get => _child1Strike1;
        set {
            _child1Strike1 = value;
            RaisePropertyChangedEvent("Child1Strike1");
        }
    }

    public string Child1Strike2 {
        get => _child1Strike2;
        set {
            _child1Strike2 = value;
            RaisePropertyChangedEvent("Child1Strike2");
        }
    }

    public string Child1Strike3 {
        get => _child1Strike3;
        set {
            _child1Strike3 = value;
            RaisePropertyChangedEvent("Child1Strike3");
        }
    }

    public string Child2Strike1 {
        get => _child2Strike1;
        set {
            _child2Strike1 = value;
            RaisePropertyChangedEvent("Child2Strike1");
        }
    }

    public string Child2Strike2 {
        get => _child2Strike2;
        set {
            _child2Strike2 = value;
            RaisePropertyChangedEvent("Child2Strike2");
        }
    }

    public string Child2Strike3 {
        get => _child2Strike3;
        set {
            _child2Strike3 = value;
            RaisePropertyChangedEvent("Child2Strike3");
        }
    }

    public string Child3Strike1 {
        get => _child3Strike1;
        set {
            _child3Strike1 = value;
            RaisePropertyChangedEvent("Child3Strike1");
        }
    }

    public string Child3Strike2 {
        get => _child3Strike2;
        set {
            _child3Strike2 = value;
            RaisePropertyChangedEvent("Child3Strike2");
        }
    }

    public string Child3Strike3 {
        get => _child3Strike3;
        set {
            _child3Strike3 = value;
            RaisePropertyChangedEvent("Child3Strike3");
        }
    }

    public int ProgressBarChild1Value {
        get => _progressBarChild1Value;
        set {
            _progressBarChild1Value = value;
            RaisePropertyChangedEvent("ProgressBarChild1Value");
        }
    }

    public int ProgressBarChild2Value {
        get => _progressBarChild2Value;
        set {
            _progressBarChild2Value = value;
            RaisePropertyChangedEvent("ProgressBarChild2Value");
        }
    }

    public int ProgressBarChild3Value {
        get => _progressBarChild3Value;
        set {
            _progressBarChild3Value = value;
            RaisePropertyChangedEvent("ProgressBarChild3Value");
        }
    }

    #endregion
}