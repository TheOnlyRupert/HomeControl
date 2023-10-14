using System;
using System.Text.Json;
using HomeControl.Source.Helpers;

namespace HomeControl.Source.ViewModel.Games.Tamagotchi;

public static class TamagotchiLogic {
    public static void TickLogic() {
        try {
            if (ReferenceValues.TamagotchiMaster.Age == 0) {
                return;
            }

            ReferenceValues.TamagotchiMaster.Age -= 0.01;

            /* Tick Heath */
            if (!ReferenceValues.TamagotchiMaster.ReverseHealth) {
                ReferenceValues.TamagotchiMaster.Health -= 0.01 * ReferenceValues.TamagotchiMaster.HealthMultiplier;

                if (ReferenceValues.TamagotchiMaster.HealthMultiplierDuration <= 0) {
                    ReferenceValues.TamagotchiMaster.HealthMultiplier = 1;
                } else {
                    ReferenceValues.TamagotchiMaster.HealthMultiplierDuration--;
                }

                if (ReferenceValues.TamagotchiMaster.Health <= 0) {
                    ReferenceValues.TamagotchiMaster.Age = 0;

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
                ReferenceValues.TamagotchiMaster.Happiness -= 0.01 * ReferenceValues.TamagotchiMaster.HappinessMultiplier;

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
                ReferenceValues.TamagotchiMaster.Hunger -= 0.01 * ReferenceValues.TamagotchiMaster.HungerMultiplier;

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
                ReferenceValues.TamagotchiMaster.Fatigue -= 0.01 * ReferenceValues.TamagotchiMaster.FatigueMultiplier;

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
                ReferenceValues.TamagotchiMaster.Bladder -= 0.01 * ReferenceValues.TamagotchiMaster.BladderMultiplier;

                if (ReferenceValues.TamagotchiMaster.BladderMultiplierDuration <= 0) {
                    ReferenceValues.TamagotchiMaster.BladderMultiplier = 1;
                } else {
                    ReferenceValues.TamagotchiMaster.BladderMultiplierDuration--;
                }

                if (ReferenceValues.TamagotchiMaster.Bladder <= 0) {
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
                ReferenceValues.TamagotchiMaster.Anxiety -= 0.01 * ReferenceValues.TamagotchiMaster.AnxietyMultiplier;

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

            /* Tick Cleanliness */
            if (!ReferenceValues.TamagotchiMaster.ReverseCleanliness) {
                ReferenceValues.TamagotchiMaster.Cleanliness -= 0.01 * ReferenceValues.TamagotchiMaster.CleanlinessMultiplier;

                if (ReferenceValues.TamagotchiMaster.CleanlinessMultiplierDuration <= 0) {
                    ReferenceValues.TamagotchiMaster.CleanlinessMultiplier = 1;
                } else {
                    ReferenceValues.TamagotchiMaster.CleanlinessMultiplierDuration--;
                }

                if (ReferenceValues.TamagotchiMaster.Cleanliness <= 0) {
                    ReferenceValues.TamagotchiMaster.Cleanliness = 0;
                    ReferenceValues.TamagotchiMaster.HappinessMultiplier = 5;
                    ReferenceValues.TamagotchiMaster.HappinessMultiplierDuration = 1;
                }
            } else {
                if (ReferenceValues.TamagotchiMaster.ReverseCleanlinessDuration > 0) {
                    ReferenceValues.TamagotchiMaster.ReverseCleanlinessDuration--;
                    ReferenceValues.TamagotchiMaster.Cleanliness += 1 * ReferenceValues.TamagotchiMaster.CleanlinessMultiplier;

                    if (ReferenceValues.TamagotchiMaster.CleanlinessMultiplierDuration <= 0) {
                        ReferenceValues.TamagotchiMaster.CleanlinessMultiplier = 1;
                    } else {
                        ReferenceValues.TamagotchiMaster.CleanlinessMultiplierDuration--;
                    }
                } else {
                    ReferenceValues.TamagotchiMaster.ReverseCleanliness = false;
                    ReferenceValues.TamagotchiMaster.IsBusy = false;
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
        } catch (Exception) {
            //ignore
        }
    }
}