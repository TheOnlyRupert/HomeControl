using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games.Tamagotchi;

public class TamagotchiVM : BaseViewModel {
    private double _debugAgeText, _debugHealthText, _debugHungerText, _debugHappinessText, _debugFatigueText, _debugAnxietyText, _debugBladderText, _debugWeightText, _debugCleanlinessText;

    private string _nameText, _tamagotchi, _debugAgeOutput, _debugHealthOutput, _debugHungerOutput, _debugHappinessOutput, _debugFatigueOutput, _debugAnxietyOutput, _debugBladderOutput,
        _debugWeightOutput, _debugCleanlinessOutput, _isMaleText, _isLightOnText, _isSleepingText, _isBusyText, _debugVisibility, _isBusyVisibility;

    public TamagotchiVM() {
        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;

        try {
            ReferenceValues.TamagotchiMaster = JsonSerializer.Deserialize<Json.Tamagotchi>(FileHelpers.LoadFileText("tamagotchi", true));
        } catch (Exception) {
            ReferenceValues.TamagotchiMaster = new Json.Tamagotchi {
                Name = "Chuck Livingston",
                Age = 100,
                Health = 100,
                Hunger = 100,
                Happiness = 100,
                Fatigue = 100,
                Weight = 10,
                Bladder = 100,
                Anxiety = 100,
                Cleanliness = 100,
                IsMale = true,
                IsLightOn = true,
                IsSleeping = false,
                IsBusy = false,
                ReverseAnxiety = false,
                ReverseBladder = false,
                ReverseFatigue = false,
                ReverseHappiness = false,
                ReverseHealth = false,
                ReverseHunger = false,
                ReverseWeight = false,
                ReverseCleanliness = false,
                HealthMultiplier = 1,
                HungerMultiplier = 1,
                HappinessMultiplier = 1,
                FatigueMultiplier = 1,
                AnxietyMultiplier = 1,
                BladderMultiplier = 1,
                WeightMultiplier = 1,
                CleanlinessMultiplier = 0,
                ReverseFatigueDuration = 0,
                ReverseAnxietyDuration = 0,
                ReverseBladderDuration = 0,
                ReverseHappinessDuration = 0,
                ReverseHealthDuration = 0,
                ReverseHungerDuration = 0,
                ReverseWeightDuration = 0,
                ReverseCleanlinessDuration = 0
            };

            FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
        }

        if (ReferenceValues.JsonSettingsMaster.DebugMode) {
            DebugVisibility = "VISIBLE";

            DebugAgeText = 100;
            DebugHealthText = 100;
            DebugHungerText = 100;
            DebugHappinessText = 100;
            DebugFatigueText = 100;
            DebugAnxietyText = 100;
            DebugBladderText = 100;
            DebugWeightText = 100;
            DebugCleanlinessText = 100;
        } else {
            DebugVisibility = "HIDDEN";
        }

        IsBusyVisibility = "HIDDEN";
        NameText = ReferenceValues.TamagotchiMaster.Name;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "Refresh":
            RefreshDisplay();
            break;
        case "MinChanged":
            FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
            break;
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "food":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.Age != 0) {
                SoundDispatcher.PlaySound("eating");

                ReferenceValues.TamagotchiMaster.ReverseHunger = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseHungerDuration = 10;
            }

            break;
        case "toilet":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.Age != 0) {
                SoundDispatcher.PlaySound("flush");

                ReferenceValues.TamagotchiMaster.ReverseBladder = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseBladderDuration = 10;
            }

            break;
        case "trash":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.Age != 0) {
                SoundDispatcher.PlaySound("trash");

                ReferenceValues.TamagotchiMaster.ReverseCleanliness = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseCleanlinessDuration = 10;
            }

            break;
        case "light":
            SoundDispatcher.PlaySound("click");

            ReferenceValues.TamagotchiMaster.IsLightOn = !ReferenceValues.TamagotchiMaster.IsLightOn;
            break;
        case "medicine":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.Age != 0) {
                SoundDispatcher.PlaySound("medicine");

                ReferenceValues.TamagotchiMaster.ReverseHealth = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseHealthDuration = 10;
            }

            break;
        case "play":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.Age != 0) {
                SoundDispatcher.PlaySound("fun");

                ReferenceValues.TamagotchiMaster.ReverseHappiness = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseHappinessDuration = 10;
            }

            break;
        case "debugAge":
            ReferenceValues.TamagotchiMaster.Age = DebugAgeText;
            break;
        case "debugHealth":
            ReferenceValues.TamagotchiMaster.Health = DebugHealthText;
            break;
        case "debugHunger":
            ReferenceValues.TamagotchiMaster.Hunger = DebugHungerText;
            break;
        case "debugHappiness":
            ReferenceValues.TamagotchiMaster.Happiness = DebugHappinessText;
            break;
        case "debugFatigue":
            ReferenceValues.TamagotchiMaster.Fatigue = DebugFatigueText;
            break;
        case "debugAnxiety":
            ReferenceValues.TamagotchiMaster.Anxiety = DebugAnxietyText;
            break;
        case "debugBladder":
            ReferenceValues.TamagotchiMaster.Bladder = DebugBladderText;
            break;
        case "debugWeight":
            ReferenceValues.TamagotchiMaster.Weight = DebugWeightText;
            break;
        case "debugCleanliness":
            ReferenceValues.TamagotchiMaster.Cleanliness = DebugCleanlinessText;
            break;
        case "debugIsLightOn":
            ReferenceValues.TamagotchiMaster.IsLightOn = !ReferenceValues.TamagotchiMaster.IsLightOn;
            break;
        case "debugIsMale":
            ReferenceValues.TamagotchiMaster.IsMale = !ReferenceValues.TamagotchiMaster.IsMale;
            break;
        case "debugIsSleeping":
            ReferenceValues.TamagotchiMaster.IsSleeping = !ReferenceValues.TamagotchiMaster.IsSleeping;
            break;
        case "debugIsBusy":
            ReferenceValues.TamagotchiMaster.IsBusy = !ReferenceValues.TamagotchiMaster.IsBusy;
            break;
        }

        RefreshDisplay();
        try {
            FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
        } catch (Exception) {
            //ignore
        }
    }

    private void RefreshDisplay() {
        DebugAgeOutput = "Age: " + ReferenceValues.TamagotchiMaster.Age;
        DebugHealthOutput = "Health: " + ReferenceValues.TamagotchiMaster.Health;
        DebugHungerOutput = "Hunger: " + ReferenceValues.TamagotchiMaster.Hunger;
        DebugHappinessOutput = "Happiness: " + ReferenceValues.TamagotchiMaster.Happiness;
        DebugFatigueOutput = "Fatigue: " + ReferenceValues.TamagotchiMaster.Fatigue;
        DebugAnxietyOutput = "Anxiety: " + ReferenceValues.TamagotchiMaster.Anxiety;
        DebugBladderOutput = "Bladder: " + ReferenceValues.TamagotchiMaster.Bladder;
        DebugWeightOutput = "Weight: " + ReferenceValues.TamagotchiMaster.Weight;
        DebugCleanlinessOutput = "Cleanliness: " + ReferenceValues.TamagotchiMaster.Cleanliness;

        IsLightOnText = "Light: " + ReferenceValues.TamagotchiMaster.IsLightOn;
        IsMaleText = "Male: " + ReferenceValues.TamagotchiMaster.IsMale;
        IsSleepingText = "Sleeping: " + ReferenceValues.TamagotchiMaster.IsSleeping;
        IsBusyText = "Busy: " + ReferenceValues.TamagotchiMaster.IsBusy;

        IsBusyVisibility = ReferenceValues.TamagotchiMaster.IsBusy ? "VISIBLE" : "HIDDEN";
    }

    #region Fields

    public string NameText {
        get => _nameText;
        set {
            _nameText = value;
            RaisePropertyChangedEvent("NameText");
        }
    }

    public string Tamagotchi {
        get => _tamagotchi;
        set {
            _tamagotchi = value;
            RaisePropertyChangedEvent("Tamagotchi");
        }
    }

    public string DebugAgeOutput {
        get => _debugAgeOutput;
        set {
            _debugAgeOutput = value;
            RaisePropertyChangedEvent("DebugAgeOutput");
        }
    }

    public string DebugHealthOutput {
        get => _debugHealthOutput;
        set {
            _debugHealthOutput = value;
            RaisePropertyChangedEvent("DebugHealthOutput");
        }
    }

    public string DebugHungerOutput {
        get => _debugHungerOutput;
        set {
            _debugHungerOutput = value;
            RaisePropertyChangedEvent("DebugHungerOutput");
        }
    }

    public string DebugHappinessOutput {
        get => _debugHappinessOutput;
        set {
            _debugHappinessOutput = value;
            RaisePropertyChangedEvent("DebugHappinessOutput");
        }
    }

    public string DebugFatigueOutput {
        get => _debugFatigueOutput;
        set {
            _debugFatigueOutput = value;
            RaisePropertyChangedEvent("DebugFatigueOutput");
        }
    }

    public string DebugAnxietyOutput {
        get => _debugAnxietyOutput;
        set {
            _debugAnxietyOutput = value;
            RaisePropertyChangedEvent("DebugAnxietyOutput");
        }
    }

    public string DebugBladderOutput {
        get => _debugBladderOutput;
        set {
            _debugBladderOutput = value;
            RaisePropertyChangedEvent("DebugBladderOutput");
        }
    }

    public string DebugWeightOutput {
        get => _debugWeightOutput;
        set {
            _debugWeightOutput = value;
            RaisePropertyChangedEvent("DebugWeightOutput");
        }
    }

    public string DebugCleanlinessOutput {
        get => _debugCleanlinessOutput;
        set {
            _debugCleanlinessOutput = value;
            RaisePropertyChangedEvent("DebugCleanlinessOutput");
        }
    }

    public double DebugAgeText {
        get => _debugAgeText;
        set {
            _debugAgeText = value;
            RaisePropertyChangedEvent("DebugAgeText");
        }
    }

    public double DebugHealthText {
        get => _debugHealthText;
        set {
            _debugHealthText = value;
            RaisePropertyChangedEvent("DebugHealthText");
        }
    }

    public double DebugHungerText {
        get => _debugHungerText;
        set {
            _debugHungerText = value;
            RaisePropertyChangedEvent("DebugHungerText");
        }
    }

    public double DebugHappinessText {
        get => _debugHappinessText;
        set {
            _debugHappinessText = value;
            RaisePropertyChangedEvent("DebugHappinessText");
        }
    }

    public double DebugFatigueText {
        get => _debugFatigueText;
        set {
            _debugFatigueText = value;
            RaisePropertyChangedEvent("DebugFatigueText");
        }
    }

    public double DebugAnxietyText {
        get => _debugAnxietyText;
        set {
            _debugAnxietyText = value;
            RaisePropertyChangedEvent("DebugAnxietyText");
        }
    }

    public double DebugBladderText {
        get => _debugBladderText;
        set {
            _debugBladderText = value;
            RaisePropertyChangedEvent("DebugBladderText");
        }
    }

    public double DebugWeightText {
        get => _debugWeightText;
        set {
            _debugWeightText = value;
            RaisePropertyChangedEvent("DebugWeightText");
        }
    }

    public double DebugCleanlinessText {
        get => _debugCleanlinessText;
        set {
            _debugCleanlinessText = value;
            RaisePropertyChangedEvent("DebugCleanlinessText");
        }
    }

    public string IsMaleText {
        get => _isMaleText;
        set {
            _isMaleText = value;
            RaisePropertyChangedEvent("IsMaleText");
        }
    }

    public string IsLightOnText {
        get => _isLightOnText;
        set {
            _isLightOnText = value;
            RaisePropertyChangedEvent("IsLightOnText");
        }
    }

    public string IsSleepingText {
        get => _isSleepingText;
        set {
            _isSleepingText = value;
            RaisePropertyChangedEvent("IsSleepingText");
        }
    }

    public string IsBusyText {
        get => _isBusyText;
        set {
            _isBusyText = value;
            RaisePropertyChangedEvent("IsBusyText");
        }
    }

    public string DebugVisibility {
        get => _debugVisibility;
        set {
            _debugVisibility = value;
            RaisePropertyChangedEvent("DebugVisibility");
        }
    }

    public string IsBusyVisibility {
        get => _isBusyVisibility;
        set {
            _isBusyVisibility = value;
            RaisePropertyChangedEvent("IsBusyVisibility");
        }
    }

    #endregion
}