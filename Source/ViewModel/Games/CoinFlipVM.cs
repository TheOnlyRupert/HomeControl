using System;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Control;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class CoinFlipVM : BaseViewModel {
    private readonly Random _random;
    private readonly PlaySound coinFlip;
    private string _imageSource, _gameStats;

    public CoinFlipVM() {
        _random = new Random();
        coinFlip = new PlaySound("coin_flip");
        Flip();
        RefreshStats();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void RefreshStats() {
        try {
            GameStats = "Total Heads: " + ReferenceValues.JsonGameStatsMaster.CoinHead + "\nTotal Tails: " + ReferenceValues.JsonGameStatsMaster.CoinTails;
            try {
                string jsonString = JsonSerializer.Serialize(ReferenceValues.JsonGameStatsMaster);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "gameStats.json", jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save finances.json... " + e.Message);
            }
        } catch (Exception) { }
    }

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

        switch (rand) {
        case 0:
            ImageSource = "../../../Resources/Images/games/quarter_head.png";
            try {
                ReferenceValues.JsonGameStatsMaster.CoinHead++;
            } catch (Exception) {
                ReferenceValues.JsonGameStatsMaster = new JsonGameStats();
                ReferenceValues.JsonGameStatsMaster.CoinHead = 1;
            }

            RefreshStats();
            break;
        case 1:
            ImageSource = "../../../Resources/Images/games/quarter_tail.png";
            try {
                ReferenceValues.JsonGameStatsMaster.CoinTails++;
            } catch (Exception) {
                ReferenceValues.JsonGameStatsMaster = new JsonGameStats();
                ReferenceValues.JsonGameStatsMaster.CoinTails = 1;
            }

            RefreshStats();
            break;
        default:
            ImageSource = ImageSource;
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

    public string GameStats {
        get => _gameStats;
        set {
            _gameStats = value;
            RaisePropertyChangedEvent("GameStats");
        }
    }

    #endregion
}