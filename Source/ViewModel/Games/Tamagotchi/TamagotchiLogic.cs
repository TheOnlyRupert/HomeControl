using System;

namespace HomeControl.Source.ViewModel.Games.Tamagotchi;

public static class TamagotchiLogic {
    public static void TickLogic() {
        try {
            if (ReferenceValues.TamagotchiMaster.Age <= 0 || ReferenceValues.TamagotchiMaster.Health <= 0) {
                return;
            }

            /* Tick Age - Should live around 5.8 days */
            ReferenceValues.TamagotchiMaster.Age -= 0.0002;

            if (ReferenceValues.TamagotchiMaster.Age <= 0) {
                ReferenceValues.TamagotchiMaster.Age = 0;
                return;
            }

            /* Start fresh */
            ReferenceValues.TamagotchiMaster.HealthMultiplier = 0;
            ReferenceValues.TamagotchiMaster.HungerMultiplier = 0;
            ReferenceValues.TamagotchiMaster.HappinessMultiplier = 0;
            ReferenceValues.TamagotchiMaster.FatigueMultiplier = 0;
            ReferenceValues.TamagotchiMaster.AnxietyMultiplier = 0;
            ReferenceValues.TamagotchiMaster.BladderMultiplier = 0;
            ReferenceValues.TamagotchiMaster.WeightMultiplier = 0;
            ReferenceValues.TamagotchiMaster.CleanlinessMultiplier = 0;

            switch (ReferenceValues.TamagotchiMaster.Health) {
            /* Health Multiplier Logic */
            case > 85:
                ReferenceValues.TamagotchiMaster.HealthMultiplier += 1;
                break;
            case > 70:
                ReferenceValues.TamagotchiMaster.HealthMultiplier += 1.5;
                break;
            case > 50:
                ReferenceValues.TamagotchiMaster.HealthMultiplier += 2;
                ReferenceValues.TamagotchiMaster.FatigueMultiplier += 2;
                break;
            case > 25:
                ReferenceValues.TamagotchiMaster.HealthMultiplier += 4;
                ReferenceValues.TamagotchiMaster.FatigueMultiplier += 4;
                break;
            case > 10:
                ReferenceValues.TamagotchiMaster.HealthMultiplier += 5;
                ReferenceValues.TamagotchiMaster.FatigueMultiplier += 4;
                break;
            default:
                ReferenceValues.TamagotchiMaster.HealthMultiplier += 10;
                ReferenceValues.TamagotchiMaster.FatigueMultiplier += 4;
                break;
            }

            /* Tick Heath */
            if (!ReferenceValues.TamagotchiMaster.ReverseHealth) {
                ReferenceValues.TamagotchiMaster.Health -= 0.0005 * ReferenceValues.TamagotchiMaster.HealthMultiplier;

                if (ReferenceValues.TamagotchiMaster.Health <= 0) {
                    ReferenceValues.TamagotchiMaster.Health = 0;
                }
            } else {
                if (ReferenceValues.TamagotchiMaster.Health >= 100) {
                    ReferenceValues.TamagotchiMaster.Health = 100;
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseHealthDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseHealthDuration--;
                        ReferenceValues.TamagotchiMaster.Health += 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseHealth = false;
                        ReferenceValues.TamagotchiMaster.IsBusy = false;
                    }
                }
            }

            /* Tick Happiness */
            if (!ReferenceValues.TamagotchiMaster.ReverseHappiness) {
                ReferenceValues.TamagotchiMaster.Happiness -= 0.01 * ReferenceValues.TamagotchiMaster.HappinessMultiplier;

                if (ReferenceValues.TamagotchiMaster.Happiness <= 0) {
                    ReferenceValues.TamagotchiMaster.Happiness = 0;
                }
            } else {
                if (ReferenceValues.TamagotchiMaster.Happiness >= 100) {
                    ReferenceValues.TamagotchiMaster.Happiness = 100;
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseHappinessDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseHappinessDuration--;
                        ReferenceValues.TamagotchiMaster.Happiness += 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseHappiness = false;
                        ReferenceValues.TamagotchiMaster.IsBusy = false;
                    }
                }
            }

            /* Tick Hunger */
            if (!ReferenceValues.TamagotchiMaster.ReverseHunger) {
                if (ReferenceValues.TamagotchiMaster.IsSleeping) {
                    ReferenceValues.TamagotchiMaster.Hunger -= 0.001 * ReferenceValues.TamagotchiMaster.HungerMultiplier;
                } else {
                    ReferenceValues.TamagotchiMaster.Hunger -= 0.002 * ReferenceValues.TamagotchiMaster.HungerMultiplier;
                }

                if (ReferenceValues.TamagotchiMaster.Hunger <= 0) {
                    //todo: manage weight loss */
                    //todo: manage health loss */
                    ReferenceValues.TamagotchiMaster.Hunger = 0;
                }
            } else {
                if (ReferenceValues.TamagotchiMaster.Hunger >= 100) {
                    //todo: manage weight gain */
                    ReferenceValues.TamagotchiMaster.Hunger = 100;
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseHungerDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseHungerDuration--;
                        ReferenceValues.TamagotchiMaster.Hunger += 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseHunger = false;
                        ReferenceValues.TamagotchiMaster.IsBusy = false;
                    }
                }
            }

            /* Tick Fatigue */
            if (ReferenceValues.TamagotchiMaster.IsSleeping) {
                ReferenceValues.TamagotchiMaster.ReverseFatigue = true;
                ReferenceValues.TamagotchiMaster.ReverseFatigueDuration = 1;
            }

            if (!ReferenceValues.TamagotchiMaster.ReverseFatigue) {
                ReferenceValues.TamagotchiMaster.Fatigue -= 0.0023 * ReferenceValues.TamagotchiMaster.FatigueMultiplier;

                if (ReferenceValues.TamagotchiMaster.Fatigue <= 0) {
                    ReferenceValues.TamagotchiMaster.Fatigue = 0;
                }
            } else {
                if (ReferenceValues.TamagotchiMaster.Fatigue >= 100) {
                    ReferenceValues.TamagotchiMaster.Fatigue = 100;
                } else {
                    if (ReferenceValues.TamagotchiMaster.ReverseFatigueDuration > 0) {
                        ReferenceValues.TamagotchiMaster.ReverseFatigueDuration--;
                        if (ReferenceValues.TamagotchiMaster.IsLightOn) {
                            ReferenceValues.TamagotchiMaster.Fatigue += 0.0015;
                        } else {
                            ReferenceValues.TamagotchiMaster.Fatigue += 0.0023;
                        }
                    } else {
                        ReferenceValues.TamagotchiMaster.ReverseFatigue = false;
                    }
                }
            }

            /* Tick Bladder */
            if (!ReferenceValues.TamagotchiMaster.ReverseBladder) {
                ReferenceValues.TamagotchiMaster.Bladder -= 0.01 * ReferenceValues.TamagotchiMaster.BladderMultiplier;

                if (ReferenceValues.TamagotchiMaster.Bladder <= 0) {
                    //todo: add bathroom logic
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
                ReferenceValues.TamagotchiMaster.Anxiety -= 0.01 * ReferenceValues.TamagotchiMaster.AnxietyMultiplier;

                if (ReferenceValues.TamagotchiMaster.Anxiety <= 0) {
                    ReferenceValues.TamagotchiMaster.Anxiety = 0;
                    ReferenceValues.TamagotchiMaster.HappinessMultiplier = 5;
                }
            } else {
                if (ReferenceValues.TamagotchiMaster.ReverseAnxietyDuration > 0) {
                    ReferenceValues.TamagotchiMaster.ReverseAnxietyDuration--;
                } else {
                    ReferenceValues.TamagotchiMaster.ReverseAnxiety = false;
                }
            }

            /* Tick Cleanliness */
            if (!ReferenceValues.TamagotchiMaster.ReverseCleanliness) {
                ReferenceValues.TamagotchiMaster.Cleanliness -= 0.01 * ReferenceValues.TamagotchiMaster.CleanlinessMultiplier;

                if (ReferenceValues.TamagotchiMaster.Cleanliness <= 0) {
                    ReferenceValues.TamagotchiMaster.Cleanliness = 0;
                    ReferenceValues.TamagotchiMaster.HappinessMultiplier = 5;
                }
            } else {
                if (ReferenceValues.TamagotchiMaster.ReverseCleanlinessDuration > 0) {
                    ReferenceValues.TamagotchiMaster.ReverseCleanlinessDuration--;
                    ReferenceValues.TamagotchiMaster.Cleanliness += 1 * ReferenceValues.TamagotchiMaster.CleanlinessMultiplier;
                } else {
                    ReferenceValues.TamagotchiMaster.ReverseCleanliness = false;
                    ReferenceValues.TamagotchiMaster.IsBusy = false;
                }
            }

            /* Light Logic */
            if (!ReferenceValues.TamagotchiMaster.IsSleeping) {
                if (!ReferenceValues.TamagotchiMaster.IsLightOn) {
                    ReferenceValues.TamagotchiMaster.AnxietyMultiplier = 3;
                }
            } else {
                if (ReferenceValues.TamagotchiMaster.IsLightOn) {
                    ReferenceValues.TamagotchiMaster.FatigueMultiplier = 1;
                }
            }
        } catch (Exception) {
            //ignore
        }
    }
}