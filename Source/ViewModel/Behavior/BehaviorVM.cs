using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Control;
using HomeControl.Source.IO;
using HomeControl.Source.Modules.Behavior;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Behavior;

public class BehaviorVM : BaseViewModel {
    private readonly PlaySound uiLocked;

    private BitmapImage _imageUser1, _imageUser2, _imageChild1, _imageChild2, _imageChild3;

    private int _progressBarUser1Value, _progressBarUser2Value, _progressBarChild1Value, _progressBarChild2Value, _progressBarChild3Value;

    private string _user1Name, _user2Name, _child1Name, _child2Name, _child3Name, _user1Star1, _user1Star2, _user1Star3, _user1Star4, _user1Star5, _user2Star1, _user2Star2,
        _user2Star3, _user2Star4, _user2Star5, _child1Star1, _child1Star2, _child1Star3, _child1Star4, _child1Star5, _child2Star1, _child2Star2, _child2Star3, _child2Star4,
        _child2Star5, _child3Star1, _child3Star2, _child3Star3, _child3Star4, _child3Star5, _user1Strike1, _user1Strike2, _user1Strike3, _user2Strike1, _user2Strike2,
        _user2Strike3, _child1Strike1, _child1Strike2, _child1Strike3, _child2Strike1, _child2Strike2, _child2Strike3, _child3Strike1, _child3Strike2, _child3Strike3,
        _progressBarUser1ValueText, _progressBarUser2ValueText, _progressBarChild1ValueText, _progressBarChild2ValueText, _progressBarChild3ValueText;

    public BehaviorVM() {
        uiLocked = new PlaySound("locked");

        User1Name = ReferenceValues.JsonMasterSettings.User1Name;
        User2Name = ReferenceValues.JsonMasterSettings.User2Name;
        Child1Name = ReferenceValues.JsonMasterSettings.User3Name;
        Child2Name = ReferenceValues.JsonMasterSettings.User4Name;
        Child3Name = ReferenceValues.JsonMasterSettings.User5Name;
        try {
            Uri uri = new(ReferenceValues.FILE_DIRECTORY + "icons/user1.png", UriKind.RelativeOrAbsolute);
            ImageUser1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user2.png", UriKind.RelativeOrAbsolute);
            ImageUser2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user3.png", UriKind.RelativeOrAbsolute);
            ImageChild1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user4.png", UriKind.RelativeOrAbsolute);
            ImageChild2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.FILE_DIRECTORY + "icons/user5.png", UriKind.RelativeOrAbsolute);
            ImageChild3 = new BitmapImage(uri);
        } catch (Exception) { }

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;

        new BehaviorFromJson();

        /* Remove strikes if program was closed before midnight */
        try {
            if (!ReferenceValues.JsonBehaviorMaster.Date.Day.Equals(DateTime.Now.Day)) {
                ReferenceValues.JsonBehaviorMaster.User1Strikes = 0;
                ReferenceValues.JsonBehaviorMaster.User2Strikes = 0;
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
        User1Star1 = "../../../Resources/Images/behavior/star_black.png";
        User1Star2 = "../../../Resources/Images/behavior/star_black.png";
        User1Star3 = "../../../Resources/Images/behavior/star_black.png";
        User1Star4 = "../../../Resources/Images/behavior/star_black.png";
        User1Star5 = "../../../Resources/Images/behavior/star_black.png";
        User2Star1 = "../../../Resources/Images/behavior/star_black.png";
        User2Star2 = "../../../Resources/Images/behavior/star_black.png";
        User2Star3 = "../../../Resources/Images/behavior/star_black.png";
        User2Star4 = "../../../Resources/Images/behavior/star_black.png";
        User2Star5 = "../../../Resources/Images/behavior/star_black.png";
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

        User1Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        User1Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        User1Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        User2Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        User2Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        User2Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        Child1Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        Child1Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        Child1Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        Child2Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        Child2Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        Child2Strike3 = "../../../Resources/Images/behavior/strike_black.png";
        Child3Strike1 = "../../../Resources/Images/behavior/strike_black.png";
        Child3Strike2 = "../../../Resources/Images/behavior/strike_black.png";
        Child3Strike3 = "../../../Resources/Images/behavior/strike_black.png";

        switch (ReferenceValues.JsonBehaviorMaster.User1Stars) {
        case 1:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            User1Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star4 = "../../../Resources/Images/behavior/star_gold.png";
            User1Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User2Stars) {
        case 1:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 2:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 3:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 4:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star4 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        case 5:
            User2Star1 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star2 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star3 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star4 = "../../../Resources/Images/behavior/star_gold.png";
            User2Star5 = "../../../Resources/Images/behavior/star_gold.png";
            break;
        }

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

        switch (ReferenceValues.JsonBehaviorMaster.User1Strikes) {
        case 1:
            User1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            User1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User1Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            User1Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User1Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            User1Strike3 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        }

        switch (ReferenceValues.JsonBehaviorMaster.User2Strikes) {
        case 1:
            User2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 2:
            User2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User2Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            break;
        case 3:
            User2Strike1 = "../../../Resources/Images/behavior/strike_red.png";
            User2Strike2 = "../../../Resources/Images/behavior/strike_red.png";
            User2Strike3 = "../../../Resources/Images/behavior/strike_red.png";
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

        ProgressBarUser1Value = ReferenceValues.JsonBehaviorMaster.User1Progress;
        ProgressBarUser1ValueText = ReferenceValues.JsonBehaviorMaster.User1Progress + "/5";
        ProgressBarUser2Value = ReferenceValues.JsonBehaviorMaster.User2Progress;
        ProgressBarUser2ValueText = ReferenceValues.JsonBehaviorMaster.User2Progress + "/5";
        ProgressBarChild1Value = ReferenceValues.JsonBehaviorMaster.Child1Progress;
        ProgressBarChild1ValueText = ReferenceValues.JsonBehaviorMaster.Child1Progress + "/5";
        ProgressBarChild2Value = ReferenceValues.JsonBehaviorMaster.Child2Progress;
        ProgressBarChild2ValueText = ReferenceValues.JsonBehaviorMaster.Child2Progress + "/5";
        ProgressBarChild3Value = ReferenceValues.JsonBehaviorMaster.Child3Progress;
        ProgressBarChild3ValueText = ReferenceValues.JsonBehaviorMaster.Child3Progress + "/5";
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "DateChanged") {
            ReferenceValues.JsonBehaviorMaster.User1Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.User2Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.Child1Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.Child2Strikes = 0;
            ReferenceValues.JsonBehaviorMaster.Child3Strikes = 0;
            RefreshBehavior();
        }
    }

    private void ButtonLogic(object param) {
        if (!ReferenceValues.LockUI) {
            switch (param) {
            case "user1":
                ReferenceValues.ActiveBehaviorUser = 0;
                break;
            case "user2":
                ReferenceValues.ActiveBehaviorUser = 1;
                break;
            case "child1":
                ReferenceValues.ActiveBehaviorUser = 2;
                break;
            case "child2":
                ReferenceValues.ActiveBehaviorUser = 3;
                break;
            case "child3":
                ReferenceValues.ActiveBehaviorUser = 4;
                break;
            }

            EditBehavior editBehavior = new();
            editBehavior.ShowDialog();
            editBehavior.Close();
            RefreshBehavior();
        } else {
            uiLocked.Play(false);
        }
    }

    #region Fields

    public string User1Name {
        get => _user1Name;
        set {
            _user1Name = value;
            RaisePropertyChangedEvent("User1Name");
        }
    }

    public string User2Name {
        get => _user2Name;
        set {
            _user2Name = value;
            RaisePropertyChangedEvent("User2Name");
        }
    }

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

    public string User1Star1 {
        get => _user1Star1;
        set {
            _user1Star1 = value;
            RaisePropertyChangedEvent("User1Star1");
        }
    }

    public string User1Star2 {
        get => _user1Star2;
        set {
            _user1Star2 = value;
            RaisePropertyChangedEvent("User1Star2");
        }
    }

    public string User1Star3 {
        get => _user1Star3;
        set {
            _user1Star3 = value;
            RaisePropertyChangedEvent("User1Star3");
        }
    }

    public string User1Star4 {
        get => _user1Star4;
        set {
            _user1Star4 = value;
            RaisePropertyChangedEvent("User1Star4");
        }
    }

    public string User1Star5 {
        get => _user1Star5;
        set {
            _user1Star5 = value;
            RaisePropertyChangedEvent("User1Star5");
        }
    }

    public string User2Star1 {
        get => _user2Star1;
        set {
            _user2Star1 = value;
            RaisePropertyChangedEvent("User2Star1");
        }
    }

    public string User2Star2 {
        get => _user2Star2;
        set {
            _user2Star2 = value;
            RaisePropertyChangedEvent("User2Star2");
        }
    }

    public string User2Star3 {
        get => _user2Star3;
        set {
            _user2Star3 = value;
            RaisePropertyChangedEvent("User2Star3");
        }
    }

    public string User2Star4 {
        get => _user2Star4;
        set {
            _user2Star4 = value;
            RaisePropertyChangedEvent("User2Star4");
        }
    }

    public string User2Star5 {
        get => _user2Star5;
        set {
            _user2Star5 = value;
            RaisePropertyChangedEvent("User2Star5");
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

    public string User1Strike1 {
        get => _user1Strike1;
        set {
            _user1Strike1 = value;
            RaisePropertyChangedEvent("User1Strike1");
        }
    }

    public string User1Strike2 {
        get => _user1Strike2;
        set {
            _user1Strike2 = value;
            RaisePropertyChangedEvent("User1Strike2");
        }
    }

    public string User1Strike3 {
        get => _user1Strike3;
        set {
            _user1Strike3 = value;
            RaisePropertyChangedEvent("User1Strike3");
        }
    }

    public string User2Strike1 {
        get => _user2Strike1;
        set {
            _user2Strike1 = value;
            RaisePropertyChangedEvent("User2Strike1");
        }
    }

    public string User2Strike2 {
        get => _user2Strike2;
        set {
            _user2Strike2 = value;
            RaisePropertyChangedEvent("User2Strike2");
        }
    }

    public string User2Strike3 {
        get => _user2Strike3;
        set {
            _user2Strike3 = value;
            RaisePropertyChangedEvent("User2Strike3");
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

    public int ProgressBarUser1Value {
        get => _progressBarUser1Value;
        set {
            _progressBarUser1Value = value;
            RaisePropertyChangedEvent("ProgressBarUser1Value");
        }
    }

    public int ProgressBarUser2Value {
        get => _progressBarUser2Value;
        set {
            _progressBarUser2Value = value;
            RaisePropertyChangedEvent("ProgressBarUser2Value");
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

    public string ProgressBarUser1ValueText {
        get => _progressBarUser1ValueText;
        set {
            _progressBarUser1ValueText = value;
            RaisePropertyChangedEvent("ProgressBarUser1ValueText");
        }
    }

    public string ProgressBarUser2ValueText {
        get => _progressBarUser2ValueText;
        set {
            _progressBarUser2ValueText = value;
            RaisePropertyChangedEvent("ProgressBarUser2ValueText");
        }
    }

    public string ProgressBarChild1ValueText {
        get => _progressBarChild1ValueText;
        set {
            _progressBarChild1ValueText = value;
            RaisePropertyChangedEvent("ProgressBarChild1ValueText");
        }
    }

    public string ProgressBarChild2ValueText {
        get => _progressBarChild2ValueText;
        set {
            _progressBarChild2ValueText = value;
            RaisePropertyChangedEvent("ProgressBarChild2ValueText");
        }
    }

    public string ProgressBarChild3ValueText {
        get => _progressBarChild3ValueText;
        set {
            _progressBarChild3ValueText = value;
            RaisePropertyChangedEvent("ProgressBarChild3ValueText");
        }
    }

    public BitmapImage ImageUser1 {
        get => _imageUser1;
        set {
            _imageUser1 = value;
            RaisePropertyChangedEvent("ImageUser1");
        }
    }

    public BitmapImage ImageUser2 {
        get => _imageUser2;
        set {
            _imageUser2 = value;
            RaisePropertyChangedEvent("ImageUser2");
        }
    }

    public BitmapImage ImageChild1 {
        get => _imageChild1;
        set {
            _imageChild1 = value;
            RaisePropertyChangedEvent("ImageChild1");
        }
    }

    public BitmapImage ImageChild2 {
        get => _imageChild2;
        set {
            _imageChild2 = value;
            RaisePropertyChangedEvent("ImageChild2");
        }
    }

    public BitmapImage ImageChild3 {
        get => _imageChild3;
        set {
            _imageChild3 = value;
            RaisePropertyChangedEvent("ImageChild3");
        }
    }

    #endregion
}