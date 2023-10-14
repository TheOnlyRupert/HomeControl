using System;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games.Tamagotchi;

public class TamagotchiVM : BaseViewModel {
    private string _nameText, _debugStats, _tamagotchi;

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
                DroppingAmount = 0,
                Weight = 10,
                Bladder = 100,
                Anxiety = 100,
                GrowthStage = 1,
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
                HealthMultiplier = 1,
                HungerMultiplier = 1,
                HappinessMultiplier = 1,
                FatigueMultiplier = 1,
                AnxietyMultiplier = 1,
                BladderMultiplier = 1,
                WeightMultiplier = 1,
                ReverseFatigueDuration = 0,
                ReverseAnxietyDuration = 0,
                ReverseBladderDuration = 0,
                ReverseHappinessDuration = 0,
                ReverseHealthDuration = 0,
                ReverseHungerDuration = 0,
                ReverseWeightDuration = 0,
                HealthMultiplierDuration = 0,
                AnxietyMultiplierDuration = 0,
                BladderMultiplierDuration = 0,
                FatigueMultiplierDuration = 0,
                HappinessMultiplierDuration = 0,
                HungerMultiplierDuration = 0,
                WeightMultiplierDuration = 0
            };

            FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
        }

        NameText = ReferenceValues.TamagotchiMaster.Name;

        RefreshDisplay();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        switch (e.PropertyName) {
        case "Refresh": {
            if (ReferenceValues.TamagotchiMaster.GrowthStage != -1) {
                ReferenceValues.TamagotchiMaster.Age--;

                /* Kill it */
                if (ReferenceValues.TamagotchiMaster.Age <= 0) {
                    ReferenceValues.TamagotchiMaster.GrowthStage = -1;
                }
                
                /* Tick Heath */
                if (!ReferenceValues.TamagotchiMaster.ReverseHealth) {
                    ReferenceValues.TamagotchiMaster.Health -= 0.1 * ReferenceValues.TamagotchiMaster.HealthMultiplier;

                    if (ReferenceValues.TamagotchiMaster.HealthMultiplierDuration <= 0) {
                        ReferenceValues.TamagotchiMaster.HealthMultiplier = 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.HealthMultiplierDuration--;
                    }

                    if (ReferenceValues.TamagotchiMaster.Health <= 0) {
                        ReferenceValues.TamagotchiMaster.GrowthStage = 0;

                        try {
                            FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
                        } catch (Exception) {
                            //ignore
                        }
                    }
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseHealthDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseHealthDuration--;
                        ReferenceValues.TamagotchiMaster.Health += 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseHealth = false;
                        ReferenceValues.TamagotchiMaster.IsBusy = false;
                    }
                }

                /* Tick Happiness */
                if (!ReferenceValues.TamagotchiMaster.ReverseHappiness) {
                    ReferenceValues.TamagotchiMaster.Happiness -= 0.1 * ReferenceValues.TamagotchiMaster.HappinessMultiplier;

                    if (ReferenceValues.TamagotchiMaster.HappinessMultiplierDuration <= 0) {
                        ReferenceValues.TamagotchiMaster.HappinessMultiplier = 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.HappinessMultiplierDuration--;
                    }

                    if (ReferenceValues.TamagotchiMaster.Happiness <= 0) {
                        ReferenceValues.TamagotchiMaster.Happiness = 0;
                        ReferenceValues.TamagotchiMaster.AnxietyMultiplier = 5;
                        ReferenceValues.TamagotchiMaster.AnxietyMultiplierDuration = 1;
                    }
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseHappinessDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseHappinessDuration--;
                        ReferenceValues.TamagotchiMaster.Happiness += 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseHappiness = false;
                        ReferenceValues.TamagotchiMaster.IsBusy = false;
                    }
                }

                /* Tick Hunger */
                if (!ReferenceValues.TamagotchiMaster.ReverseHunger) {
                    ReferenceValues.TamagotchiMaster.Hunger -= 0.1 * ReferenceValues.TamagotchiMaster.HungerMultiplier;

                    if (ReferenceValues.TamagotchiMaster.HungerMultiplierDuration <= 0) {
                        ReferenceValues.TamagotchiMaster.HungerMultiplier = 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.HungerMultiplierDuration--;
                    }

                    if (ReferenceValues.TamagotchiMaster.Hunger <= 0) {
                        ReferenceValues.TamagotchiMaster.Hunger = 0;
                        ReferenceValues.TamagotchiMaster.HealthMultiplier = 5;
                        ReferenceValues.TamagotchiMaster.HealthMultiplierDuration = 1;
                    }
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseHungerDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseHungerDuration--;
                        ReferenceValues.TamagotchiMaster.Hunger += 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseHunger = false;
                        ReferenceValues.TamagotchiMaster.IsBusy = false;
                    }
                }

                /* Tick Fatigue */
                if (!ReferenceValues.TamagotchiMaster.ReverseFatigue) {
                    ReferenceValues.TamagotchiMaster.Fatigue -= 0.1 * ReferenceValues.TamagotchiMaster.FatigueMultiplier;

                    if (ReferenceValues.TamagotchiMaster.FatigueMultiplierDuration <= 0) {
                        ReferenceValues.TamagotchiMaster.FatigueMultiplier = 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.FatigueMultiplierDuration--;
                    }

                    if (ReferenceValues.TamagotchiMaster.Fatigue <= 0) {
                        ReferenceValues.TamagotchiMaster.Fatigue = 0;
                    }
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseFatigueDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseFatigueDuration--;
                        ReferenceValues.TamagotchiMaster.Fatigue += 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseFatigue = false;
                    }
                }

                /* Tick Bladder */
                if (!ReferenceValues.TamagotchiMaster.ReverseBladder) {
                    ReferenceValues.TamagotchiMaster.Bladder -= 0.1 * ReferenceValues.TamagotchiMaster.BladderMultiplier;

                    if (ReferenceValues.TamagotchiMaster.BladderMultiplierDuration <= 0) {
                        ReferenceValues.TamagotchiMaster.BladderMultiplier = 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.BladderMultiplierDuration--;
                    }

                    if (ReferenceValues.TamagotchiMaster.Bladder <= 0) {
                        AddDropping();
                        ReferenceValues.TamagotchiMaster.HealthMultiplier = 5;
                        ReferenceValues.TamagotchiMaster.HealthMultiplierDuration = 5;
                        ReferenceValues.TamagotchiMaster.Bladder = 100;
                    }
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseBladderDuration >= 0) {
                        ReferenceValues.TamagotchiMaster.ReverseBladderDuration--;
                        ReferenceValues.TamagotchiMaster.Bladder += 10;
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseBladder = false;
                        ReferenceValues.TamagotchiMaster.IsBusy = false;
                    }
                }

                /* Tick Anxiety */
                if (!ReferenceValues.TamagotchiMaster.ReverseAnxiety) {
                    ReferenceValues.TamagotchiMaster.Anxiety -= 0.1 * ReferenceValues.TamagotchiMaster.AnxietyMultiplier;

                    if (ReferenceValues.TamagotchiMaster.AnxietyMultiplierDuration <= 0) {
                        ReferenceValues.TamagotchiMaster.AnxietyMultiplier = 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.AnxietyMultiplierDuration--;
                    }

                    if (ReferenceValues.TamagotchiMaster.Anxiety <= 0) {
                        ReferenceValues.TamagotchiMaster.Anxiety = 0;
                        ReferenceValues.TamagotchiMaster.HappinessMultiplier = 5;
                        ReferenceValues.TamagotchiMaster.HappinessMultiplierDuration = 1;
                    }
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseAnxietyDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseAnxietyDuration--;
                        ReferenceValues.TamagotchiMaster.Anxiety += 1 * ReferenceValues.TamagotchiMaster.AnxietyMultiplier;

                        if (ReferenceValues.TamagotchiMaster.AnxietyMultiplierDuration <= 0) {
                            ReferenceValues.TamagotchiMaster.AnxietyMultiplier = 1;
                        } else {
                            ReferenceValues.TamagotchiMaster.AnxietyMultiplierDuration--;
                        }
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseAnxiety = false;
                    }
                }

                /* Light Logic */
                if (!ReferenceValues.TamagotchiMaster.IsSleeping) {
                    if (!ReferenceValues.TamagotchiMaster.IsLightOn) {
                        ReferenceValues.TamagotchiMaster.AnxietyMultiplier = 5;
                        ReferenceValues.TamagotchiMaster.AnxietyMultiplierDuration = 1;
                    }
                } else {
                    if (ReferenceValues.TamagotchiMaster.IsLightOn) {
                        ReferenceValues.TamagotchiMaster.FatigueMultiplier = 1;
                        ReferenceValues.TamagotchiMaster.FatigueMultiplierDuration = 1;
                    }
                }
            }

            RefreshDisplay();
            break;
        }
        case "MinChanged":
            try {
                FileHelpers.SaveFileText("tamagotchi", JsonSerializer.Serialize(ReferenceValues.TamagotchiMaster), true);
            } catch (Exception) {
                //ignore
            }

            break;
        }
    }

    private void AddDropping() { }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "food":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.GrowthStage != 0) {
                ReferenceValues.SoundToPlay = "eating";
                SoundDispatcher.PlaySound();

                ReferenceValues.TamagotchiMaster.ReverseHunger = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseHungerDuration = 10;
            }

            break;
        case "toilet":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.GrowthStage != 0) {
                ReferenceValues.SoundToPlay = "flush";
                SoundDispatcher.PlaySound();

                ReferenceValues.TamagotchiMaster.ReverseBladder = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseBladderDuration = 10;
            }

            break;
        case "light":
            ReferenceValues.SoundToPlay = "click";
            SoundDispatcher.PlaySound();

            ReferenceValues.TamagotchiMaster.IsLightOn = !ReferenceValues.TamagotchiMaster.IsLightOn;
            break;
        case "medicine":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.GrowthStage != 0) {
                ReferenceValues.SoundToPlay = "medicine";
                SoundDispatcher.PlaySound();

                ReferenceValues.TamagotchiMaster.ReverseHealth = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseHealthDuration = 10;
            }

            break;
        case "play":
            if (!ReferenceValues.TamagotchiMaster.IsSleeping && !ReferenceValues.TamagotchiMaster.IsBusy && ReferenceValues.TamagotchiMaster.GrowthStage != 0) {
                ReferenceValues.SoundToPlay = "fun";
                SoundDispatcher.PlaySound();

                ReferenceValues.TamagotchiMaster.ReverseHappiness = true;
                ReferenceValues.TamagotchiMaster.IsBusy = true;
                ReferenceValues.TamagotchiMaster.ReverseHappinessDuration = 10;
            }

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
        if (ReferenceValues.TamagotchiMaster.GrowthStage != 0) {
            DebugStats = "Age: " + ReferenceValues.TamagotchiMaster.Age + "\nHealth: " + ReferenceValues.TamagotchiMaster.Health + "\nHappiness: " +
                         ReferenceValues.TamagotchiMaster.Happiness + "\nHunger: " + ReferenceValues.TamagotchiMaster.Hunger + "\nFatigue: " + ReferenceValues.TamagotchiMaster.Fatigue +
                         "\nBladder: " + ReferenceValues.TamagotchiMaster.Bladder + "\nAnxiety: " + ReferenceValues.TamagotchiMaster.Anxiety + "\nLights: " +
                         ReferenceValues.TamagotchiMaster.IsLightOn;
        } else {
            DebugStats = "DEAD!";
        }

        switch (ReferenceValues.TamagotchiMaster.GrowthStage) {
        case -1:
            Tamagotchi = "";
            break;
        case 0:
            Tamagotchi = "../../Resources/Images/games/tamagotchi/egg1.png";
            break;
        case 1:
            Tamagotchi = "../../Resources/Images/games/tamagotchi/egg2.png";
            break;
        }
    }

    #region Fields

    public string NameText {
        get => _nameText;
        set {
            _nameText = value;
            RaisePropertyChangedEvent("NameText");
        }
    }

    public string DebugStats {
        get => _debugStats;
        set {
            _debugStats = value;
            RaisePropertyChangedEvent("DebugStats");
        }
    }

    public string Tamagotchi {
        get => _tamagotchi;
        set {
            _tamagotchi = value;
            RaisePropertyChangedEvent("Tamagotchi");
        }
    }

    #endregion
}