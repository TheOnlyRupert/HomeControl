using System.Windows.Input;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class GroceriesVM : BaseViewModel {
    private string _krogerSource, _aldiSource, _foodLionSource;

    public GroceriesVM() {
        KrogerSource = "https://www.kroger.com/weeklyad";
        AldiSource = "https://www.aldi.us/en/weekly-specials/our-weekly-ads/";
        FoodLionSource = "https://www.foodlion.com/weekly-specials/";
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "krogerWeekly":
            KrogerSource = "https://www.kroger.com/weeklyad";
            break;
        case "aldiWeekly":
            AldiSource = "https://platform.liquidus.net/promotion";
            break;
        case "foodLionWeekly":
            FoodLionSource = "https://www.foodlion.com/weekly-specials/";
            break;
        case "krogerList":
            KrogerSource = "https://www.kroger.com/shopping/list";
            break;
        case "aldiList":
            AldiSource = "https://platform.liquidus.net/saved-deals";
            break;
        case "foodLionList":
            FoodLionSource = "https://www.foodlion.com/account/shopping-list/";
            break;
        }
    }

    #region Fields

    public string KrogerSource {
        get => _krogerSource;
        set {
            _krogerSource = value;
            RaisePropertyChangedEvent("KrogerSource");
        }
    }

    public string AldiSource {
        get => _aldiSource;
        set {
            _aldiSource = value;
            RaisePropertyChangedEvent("AldiSource");
        }
    }

    public string FoodLionSource {
        get => _foodLionSource;
        set {
            _foodLionSource = value;
            RaisePropertyChangedEvent("FoodLionSource");
        }
    }

    #endregion
}