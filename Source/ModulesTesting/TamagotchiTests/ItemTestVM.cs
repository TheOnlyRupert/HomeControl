using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ModulesTesting.TamagotchiTests;

public class ItemTestVM : BaseViewModel {
    private ObservableCollection<FoodItem> _foodItemList;
    private FoodItem _foodItemSelected;
    private float _inputTempEnvironment, _inputTempItem, _inputTime;
    private string _outputText;
    private FoodItemActive foodItemActive;

    public ItemTestVM() {
        FoodItemList = new ObservableCollection<FoodItem> {
            new() {
                name = "Apple",
                decayRate = 1.0f,
                startingAgeRange = new[] { 10, 15 },
                temperatureFactorFrozen = 2.0f
            },
            new() {
                name = "Chicken",
                decayRate = 1.0f,
                startingAgeRange = new[] { 8, 12 },
                temperatureFactorFrozen = 3.0f
            }
        };

        InputTime = 1;

        FoodItemSelected = FoodItemList[0];
        foodItemActive = new FoodItemActive {
            foodItem = FoodItemSelected,
            age = GetAgeFromRange(FoodItemSelected.startingAgeRange),
            temperature = 0
        };
        UpdateOutputText();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private int GetAgeFromRange(int[] age) {
        Random random = new();
        return random.Next(age[0], age[1]);
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "setItem":
            foodItemActive = new FoodItemActive {
                foodItem = FoodItemSelected,
                age = GetAgeFromRange(FoodItemSelected.startingAgeRange),
                temperature = 0
            };
            UpdateOutputText();
            break;
        case "setTempItem":
            foodItemActive.temperature = InputTempItem;
            UpdateOutputText();
            break;
        case "simulate":
            if ((int)foodItemActive.temperature > (int)InputTempEnvironment) {
                foodItemActive.temperature -= InputTime;
                if ((int)foodItemActive.temperature < (int)InputTempEnvironment) {
                    foodItemActive.temperature = InputTempEnvironment;
                }
            } else if ((int)foodItemActive.temperature < (int)InputTempEnvironment) {
                foodItemActive.temperature += InputTime;
                if ((int)foodItemActive.temperature > (int)InputTempEnvironment) {
                    foodItemActive.temperature = InputTempEnvironment;
                }
            }

            if (foodItemActive.age <= 0) {
                foodItemActive.age = 0;
            } else if (foodItemActive.temperature <= 0) {
                foodItemActive.age -= foodItemActive.foodItem.decayRate / foodItemActive.foodItem.temperatureFactorFrozen;
            } else if (foodItemActive.temperature is > 0 and < 35) {
                foodItemActive.age -= foodItemActive.foodItem.decayRate / (foodItemActive.foodItem.temperatureFactorFrozen / 4);
            } else {
                foodItemActive.age -= foodItemActive.foodItem.decayRate;
            }

            UpdateOutputText();
            break;
        }
    }

    private void UpdateOutputText() {
        OutputText = "FoodItem: " + foodItemActive.foodItem.name + " -- Current Age: " + foodItemActive.age + "  -- Current Environment Temp: " + InputTempEnvironment +
                     " -- Current Item Temp: " + foodItemActive.temperature;
    }


    #region Fields

    public float InputTempEnvironment {
        get => _inputTempEnvironment;
        set {
            _inputTempEnvironment = value;
            RaisePropertyChangedEvent("InputTempEnvironment");
        }
    }

    public float InputTempItem {
        get => _inputTempItem;
        set {
            _inputTempItem = value;
            RaisePropertyChangedEvent("InputTempItem");
        }
    }

    public float InputTime {
        get => _inputTime;
        set {
            _inputTime = value;
            RaisePropertyChangedEvent("InputTime");
        }
    }

    public string OutputText {
        get => _outputText;
        set {
            _outputText = value;
            RaisePropertyChangedEvent("OutputText");
        }
    }

    public ObservableCollection<FoodItem> FoodItemList {
        get => _foodItemList;
        set {
            _foodItemList = value;
            RaisePropertyChangedEvent("FoodItemList");
        }
    }

    public FoodItem FoodItemSelected {
        get => _foodItemSelected;
        set {
            _foodItemSelected = value;
            RaisePropertyChangedEvent("FoodItemSelected");
        }
    }

    #endregion
}