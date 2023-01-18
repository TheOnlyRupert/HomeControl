using System.Windows.Input;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class CoinFlipVM : BaseViewModel {
    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "flip":
            break;
        }
    }
}