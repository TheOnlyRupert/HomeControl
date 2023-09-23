using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

/*
 * Rules: (long greens; short reds):
 * Level 1 -->  Green Min/Max:  5 - 10,  Red Min/Max: 5 - 15
 * Level 2 -->  Green Min/Max: 10 - 20,  Red Min/Max: 5 - 15
 * Level 3 -->  Green Min/Max: 15 - 35,  Red Min/Max: 5 - 12
 * Level 4 -->  Green Min/Max: 20 - 50,  Red Min/Max: 4 - 10
 * Level 5 -->  Green Min/Max: 20 - 60,  Red Min/Max: 2 - 10
 */

public class TrafficLightVM : BaseViewModel {
    private string _gameColor, _gamePausedVisibility, _gameStatusText, _gameLevelText;
    private int _progressBarValue;
    private bool isHardcoreActive;

    public TrafficLightVM() {
        GameColor = "Red";
        GameStatusText = "New Game...";
        GameLevelText = "Level 1";
        isHardcoreActive = false;
        GamePausedVisibility = "HIDDEN";
        ProgressBarValue = 0;
    }

    #region Fields

    public string GameColor {
        get => _gameColor;
        set {
            _gameColor = value;
            RaisePropertyChangedEvent("GameColor");
        }
    }

    public string GamePausedVisibility {
        get => _gamePausedVisibility;
        set {
            _gamePausedVisibility = value;
            RaisePropertyChangedEvent("GamePausedVisibility");
        }
    }

    public string GameStatusText {
        get => _gameStatusText;
        set {
            _gameStatusText = value;
            RaisePropertyChangedEvent("GameStatusText");
        }
    }

    public string GameLevelText {
        get => _gameLevelText;
        set {
            _gameLevelText = value;
            RaisePropertyChangedEvent("GameLevelText");
        }
    }

    public int ProgressBarValue {
        get => _progressBarValue;
        set {
            _progressBarValue = value;
            RaisePropertyChangedEvent("ProgressBarValue");
        }
    }

    #endregion
}