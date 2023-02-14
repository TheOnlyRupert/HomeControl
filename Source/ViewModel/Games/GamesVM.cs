using System.Windows.Input;
using HomeControl.Source.Modules.Games;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class GamesVM : BaseViewModel {
    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "pictionary":
            Pictionary pictionary = new();
            pictionary.ShowDialog();
            pictionary.Close();
            break;
        case "coinFlip":
            CoinFlip coinFlip = new();
            coinFlip.ShowDialog();
            coinFlip.Close();
            break;
        case "tamagotchi":
            Tamagotchi tamagotchi = new();
            tamagotchi.ShowDialog();
            tamagotchi.Close();
            break;
        }
    }
}