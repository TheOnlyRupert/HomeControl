using System;
using System.Windows.Input;
using HomeControl.Source.Control;
using HomeControl.Source.Modules.Games;
using HomeControl.Source.ModulesTesting.TamagotchiTests;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class TamagotchiVM : BaseViewModel {
    private readonly PlaySound eggHatchSound;
    private string _imageSource, _imageSourceEggTop, _imageSourceEggBottom, _debugVisibility;
    private int animalID, hatchStage1, hatchStage2;
    private bool isHatching;

    public TamagotchiVM() {
        eggHatchSound = new PlaySound("egg_hatch");
        hatchStage2 = 1;
        DebugVisibility = "VISIBLE";

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "Refresh") {
            if (isHatching) {
                HatchLogic();
            }
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "feed":
            TamagotchiFeed tamagotchiFeed = new();
            tamagotchiFeed.ShowDialog();
            tamagotchiFeed.Close();
            break;

        /* DEBUG SECTION */
        case "debug1":
            hatchStage2 = 1;
            isHatching = true;
            eggHatchSound.Play(false);
            break;
        case "debug2":
            RandomizeAnimal();
            break;
        case "debug3":
            ItemTest itemTest = new();
            itemTest.ShowDialog();
            itemTest.Close();
            break;
        case "debug4":
            break;
        case "debug5":
            break;
        case "debug6":
            break;
        case "debug7":
            break;
        case "debug8":
            break;
        case "debug9":
            break;
        case "debug10":
            break;
        case "debug11":
            break;
        case "debug12":
            break;
        }
    }

    private void RandomizeAnimal() {
        isHatching = false;
        hatchStage2 = 1;

        Random random = new();
        animalID = random.Next(32) * 13;
        hatchStage1 = random.Next(0, 2);
        ImageSource = "../../../Resources/Images/games/eggs_accented/tile" + animalID.ToString("D3") + ".png";
        ImageSourceEggTop = "null";
        ImageSourceEggBottom = "null";
    }

    private void HatchLogic() {
        switch (hatchStage2) {
        case < 5:
            ImageSourceEggTop = "";
            ImageSource = "../../../Resources/Images/games/eggs_accented/tile" + (animalID + hatchStage1 * 6 + hatchStage2).ToString("D3") + ".png";
            ImageSourceEggBottom = "";
            hatchStage2++;
            break;
        case 5:
            ImageSourceEggTop = "../../../Resources/Images/games/eggs_accented/tile" + (animalID + hatchStage1 * 6 + 5).ToString("D3") + ".png";
            ImageSource = "ANIMAL TODO!";
            ImageSourceEggBottom = "../../../Resources/Images/games/eggs_accented/tile" + (animalID + hatchStage1 * 6 + 6).ToString("D3") + ".png";
            hatchStage2++;
            break;
        case > 8:
            ImageSourceEggTop = "";
            ImageSource = "ANIMAL TODO!";
            ImageSourceEggBottom = "";
            isHatching = false;
            hatchStage2 = 1;
            break;
        default:
            hatchStage2++;
            break;
        }
    }

    #region Fields

    public string ImageSource {
        get => _imageSource;
        set {
            _imageSource = value;
            RaisePropertyChangedEvent("ImageSource");
        }
    }

    public string ImageSourceEggTop {
        get => _imageSourceEggTop;
        set {
            _imageSourceEggTop = value;
            RaisePropertyChangedEvent("ImageSourceEggTop");
        }
    }

    public string ImageSourceEggBottom {
        get => _imageSourceEggBottom;
        set {
            _imageSourceEggBottom = value;
            RaisePropertyChangedEvent("ImageSourceEggBottom");
        }
    }

    public string DebugVisibility {
        get => _debugVisibility;
        set {
            _debugVisibility = value;
            RaisePropertyChangedEvent("DebugVisibility");
        }
    }

    #endregion
}