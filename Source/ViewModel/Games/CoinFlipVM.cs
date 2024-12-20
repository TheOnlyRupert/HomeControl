﻿using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class CoinFlipVM : BaseViewModel {
    private readonly Random _random;
    private string _imageSource, _gameStats;

    public CoinFlipVM() {
        SoundDispatcher.PlaySound("coin_flip");
        _random = new Random();
        Flip();
        RefreshStats();
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }

    private void RefreshStats() {
        try {
            GameStats = "Total Heads: " + ReferenceValues.JsonGameStatsMaster.CoinHead + "\nTotal Tails: " + ReferenceValues.JsonGameStatsMaster.CoinTails;
            try {
                FileHelpers.SaveFileText("gameStats", JsonSerializer.Serialize(ReferenceValues.JsonGameStatsMaster), true);
            } catch (Exception e) {
                ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "CoinFlipVM",
                    Description = e.ToString()
                });
                FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
            }
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "CoinFlipVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "flip":
            Flip();
            break;
        }
    }

    private void Flip() {
        SoundDispatcher.PlaySound("coin_flip");
        int rand = _random.Next(0, 2);

        switch (rand) {
        case 0:
            ImageSource = "../../../Resources/Images/games/quarter_head.png";
            try {
                ReferenceValues.JsonGameStatsMaster.CoinHead++;
            } catch (Exception) {
                ReferenceValues.JsonGameStatsMaster = new JsonGameStats {
                    CoinHead = 1
                };
            }

            RefreshStats();
            break;
        case 1:
            ImageSource = "../../../Resources/Images/games/quarter_tail.png";
            try {
                ReferenceValues.JsonGameStatsMaster.CoinTails++;
            } catch (Exception) {
                ReferenceValues.JsonGameStatsMaster = new JsonGameStats {
                    CoinTails = 1
                };
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