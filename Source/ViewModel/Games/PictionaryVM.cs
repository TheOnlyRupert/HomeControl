using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class PictionaryVM : BaseViewModel {
    private static bool isGameActive;

    private string _menuVisibility, _wordsEasyColor, _wordsMediumColor, _wordsHardColor, _wordsAdultColor, _gameVisibility, _outputText,
        _button1Text, _button2Text, _enableCountdownColor;

    private int _timerAmount, _wordsAmount, timerMaster_sec;

    private List<string>? playableWords;
    private double progressBarAdditive, progressBarRate, _progressBarValue;

    public PictionaryVM() {
        TimerAmount = 120;

        MenuVisibility = "VISIBLE";
        GameVisibility = "HIDDEN";

        Button1Text = "Play";
        Button2Text = "Play";

        WordsEasyColor = "Green";
        WordsMediumColor = "Transparent";
        WordsHardColor = "Transparent";
        WordsAdultColor = "Transparent";
        EnableCountdownColor = "Green";
        WordsAmount = 3;
        isGameActive = false;
        OutputText = "";
        playableWords = new List<string>();

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand {
        get => new DelegateCommand(ButtonCommandLogic, true);
    }


    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "Refresh") {
            if (ReferenceValues.IsGameTimerRunning && EnableCountdownColor == "Green") {
                /* Plays a sound with 5 - 1 seconds remaining */
                if (timerMaster_sec < 6) {
                    SoundDispatcher.PlaySound("tap");
                }

                if (timerMaster_sec == 0) {
                    ReferenceValues.IsGameTimerRunning = false;
                    SoundDispatcher.PlaySound("buzzer");
                } else {
                    timerMaster_sec--;
                    progressBarAdditive += progressBarRate;
                    ProgressBarValue = (int)progressBarAdditive;
                }
            }
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "wordsEasy":
            WordsEasyColor = WordsEasyColor == "Green" ? "Transparent" : "Green";

            break;
        case "wordsMedium":
            WordsMediumColor = WordsMediumColor == "Green" ? "Transparent" : "Green";

            break;
        case "wordsHard":
            WordsHardColor = WordsHardColor == "Green" ? "Transparent" : "Green";

            break;
        case "wordsAdult":
            WordsAdultColor = WordsAdultColor == "Green" ? "Transparent" : "Green";

            break;
        case "enableCountdown":
            EnableCountdownColor = EnableCountdownColor == "Green" ? "Transparent" : "Green";

            break;
        case "wordsAdd":
            if (WordsAmount < 9) {
                WordsAmount++;
            }

            break;
        case "wordsSub":
            if (WordsAmount > 1) {
                WordsAmount--;
            }

            break;
        case "timerAdd":
            if (TimerAmount < 999) {
                TimerAmount++;
            }

            break;
        case "timerSub":
            if (TimerAmount > 1) {
                TimerAmount--;
            }

            break;
        case "button1":
            if (isGameActive) {
                ReferenceValues.IsGameTimerRunning = false;
                NextWord();
            } else {
                if (WordsEasyColor == "Transparent" && WordsMediumColor == "Transparent" && WordsHardColor == "Transparent" && WordsAdultColor == "Transparent") {
                    MessageBox.Show("At least one word level must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {
                    ReferenceValues.IsGameTimerRunning = false;
                    isGameActive = true;
                    MenuVisibility = "HIDDEN";
                    GameVisibility = "VISIBLE";
                    Button1Text = "Next Word";
                    Button2Text = "Main Menu";

                    GeneratePlayableWords();
                    NextWord();
                }
            }

            break;
        case "button2":
            if (isGameActive) {
                isGameActive = false;
                ReferenceValues.IsGameTimerRunning = false;
                ProgressBarValue = 0;

                MenuVisibility = "VISIBLE";
                GameVisibility = "HIDDEN";
                Button1Text = "Play";
                Button2Text = "Play";
            } else {
                if (WordsEasyColor == "Transparent" && WordsMediumColor == "Transparent" && WordsHardColor == "Transparent" && WordsAdultColor == "Transparent") {
                    MessageBox.Show("At least one word level must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {
                    ReferenceValues.IsGameTimerRunning = false;
                    isGameActive = true;
                    MenuVisibility = "HIDDEN";
                    GameVisibility = "VISIBLE";
                    Button1Text = "Next Word";
                    Button2Text = "Main Menu";

                    GeneratePlayableWords();
                    NextWord();
                }
            }

            break;
        }
    }

    private void NextWord() {
        OutputText = "";

        if (EnableCountdownColor == "Green") {
            timerMaster_sec = TimerAmount;
            double conversion = TimerAmount;
            progressBarAdditive = 0;
            progressBarRate = 100 / conversion;

            ReferenceValues.IsGameTimerRunning = true;
        }

        try {
            for (int i2 = 0; i2 < WordsAmount; i2++) {
                OutputText += i2 + 1 + ":  " + playableWords[i2] + "\n";
                playableWords.RemoveAt(i2);
            }
        } catch (ArgumentOutOfRangeException) {
            GeneratePlayableWords();
            NextWord();
        }
    }

    private void GeneratePlayableWords() {
        playableWords.Clear();

        if (WordsEasyColor == "Green") {
            foreach (string word in WordList.PictionaryEasyList) {
                playableWords.Add(word);
            }
        }

        if (WordsMediumColor == "Green") {
            foreach (string word in WordList.PictionaryMediumList) {
                playableWords.Add(word);
            }
        }

        if (WordsHardColor == "Green") {
            foreach (string word in WordList.PictionaryHardList) {
                playableWords.Add(word);
            }
        }

        if (WordsAdultColor == "Green") {
            foreach (string word in WordList.PictionaryAdultList) {
                playableWords.Add(word);
            }
        }

        /* Randomize the list */
        playableWords = ListHelpers.RandomizeList(playableWords);
    }

    #region Fields

    public double ProgressBarValue {
        get => _progressBarValue;
        set {
            _progressBarValue = value;
            RaisePropertyChangedEvent("ProgressBarValue");
        }
    }

    public string MenuVisibility {
        get => _menuVisibility;
        set {
            _menuVisibility = value;
            RaisePropertyChangedEvent("MenuVisibility");
        }
    }

    public string GameVisibility {
        get => _gameVisibility;
        set {
            _gameVisibility = value;
            RaisePropertyChangedEvent("GameVisibility");
        }
    }

    public string WordsEasyColor {
        get => _wordsEasyColor;
        set {
            _wordsEasyColor = value;
            RaisePropertyChangedEvent("WordsEasyColor");
        }
    }

    public string WordsMediumColor {
        get => _wordsMediumColor;
        set {
            _wordsMediumColor = value;
            RaisePropertyChangedEvent("WordsMediumColor");
        }
    }

    public string WordsHardColor {
        get => _wordsHardColor;
        set {
            _wordsHardColor = value;
            RaisePropertyChangedEvent("WordsHardColor");
        }
    }

    public string WordsAdultColor {
        get => _wordsAdultColor;
        set {
            _wordsAdultColor = value;
            RaisePropertyChangedEvent("WordsAdultColor");
        }
    }

    public string EnableCountdownColor {
        get => _enableCountdownColor;
        set {
            _enableCountdownColor = value;
            RaisePropertyChangedEvent("EnableCountdownColor");
        }
    }

    public string OutputText {
        get => _outputText;
        set {
            _outputText = value;
            RaisePropertyChangedEvent("OutputText");
        }
    }

    public string Button1Text {
        get => _button1Text;
        set {
            _button1Text = value;
            RaisePropertyChangedEvent("Button1Text");
        }
    }

    public string Button2Text {
        get => _button2Text;
        set {
            _button2Text = value;
            RaisePropertyChangedEvent("Button2Text");
        }
    }

    public int WordsAmount {
        get => _wordsAmount;
        set {
            _wordsAmount = value;
            RaisePropertyChangedEvent("WordsAmount");
        }
    }

    public int TimerAmount {
        get => _timerAmount;
        set {
            _timerAmount = value;
            RaisePropertyChangedEvent("TimerAmount");
        }
    }

    #endregion
}