using System;
using System.Windows.Input;
using HomeControl.Source.Control;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class CoinFlipVM : BaseViewModel {
    private readonly Random _random;
    private readonly PlaySound coinFlip;
    private string _imageSource;

    public CoinFlipVM() {
        _random = new Random();
        coinFlip = new PlaySound("coin_flip");
        Flip();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    #region Fields

    public string ImageSource {
        get => _imageSource;
        set {
            _imageSource = value;
            RaisePropertyChangedEvent("ImageSource");
        }
    }

    #endregion

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "flip":
            Flip();
            break;
        }
    }

    private void Flip() {
        coinFlip.Play(false);
        int rand = _random.Next(0, 2);

        ImageSource = rand switch {
            0 => "../../../Resources/Images/games/quarter_head.png",
            1 => "../../../Resources/Images/games/quarter_tail.png",
            _ => ImageSource
        };
    }
}