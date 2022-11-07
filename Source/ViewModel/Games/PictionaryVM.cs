using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using HomeControl.Source.Control;
using HomeControl.Source.Helpers;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games; 

public class PictionaryVM : BaseViewModel {
    private static bool isTimerActive, isGameActive;

    private static DispatcherTimer dispatcherTimer;
    private readonly PlaySound soundBuzzer, soundDing, soundTap;

    private string _progressBarValue, _progressBarValueMaster, _progressBarVisibility, _menuVisibility, _timerAmountText, _wordsAmountText, _wordsEasyColor, _wordsMediumColor,
        _wordsHardColor, _wordsAdultColor, _realTime, _gameVisibility, _outputText;

    private bool includeEasyWords, includeMediumWords, includeHardWords, includeAdultWords;
    private List<string> playableWords;
    private double progressBarAdditive, progressBarRate, progressBarAdditiveMaster, progressBarRateMaster;
    private int timerCountdownNum, wordsAmountInt;

    public PictionaryVM() {
        soundBuzzer = new PlaySound("buzzer");
        soundDing = new PlaySound("ding");
        soundTap = new PlaySound("tap");
        isTimerActive = includeEasyWords = includeMediumWords = includeHardWords = includeAdultWords = false;
        timerCountdownNum = 60;
        WordsAmountText = "1";
        TimerAmountText = "60";
        ProgressBarVisibility = "VISIBLE";
        MenuVisibility = "VISIBLE";
        GameVisibility = "HIDDEN";
        WordsEasyColor = "Green";
        WordsMediumColor = "Transparent";
        WordsHardColor = "Transparent";
        WordsAdultColor = "Transparent";
        wordsAmountInt = 1;
        progressBarRate = 60;
        isGameActive = false;
        OutputText = "";
        playableWords = new List<string>();

        dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        dispatcherTimer.Start();
    }

    public ICommand GlobalKeyboardListener => new DelegateCommand(GlobalKeyboardListenerLogic, true);

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    public static void DisposeWindow() {
        isTimerActive = false;
        dispatcherTimer.Stop();
    }

    private void GlobalKeyboardListenerLogic(object obj) {
        switch (obj) { }
    }

    /* Called every second while program is running */
    private void dispatcherTimer_Tick(object sender, EventArgs e) {
        if (isGameActive) {
            if (isTimerActive) {
                if (timerCountdownNum == 0) {
                    soundBuzzer.Play();
                    isTimerActive = false;
                    ProgressBarValue = "100";
                    progressBarAdditive = 0;
                    double conversion = timerCountdownNum;
                    progressBarAdditive = 0;
                    progressBarRate = 100 / conversion;
                } else {
                    timerCountdownNum--;
                    TimerAmountText = timerCountdownNum.ToString();
                    progressBarAdditiveMaster += progressBarRateMaster;
                    ProgressBarValueMaster = progressBarAdditiveMaster.ToString(CultureInfo.InvariantCulture);
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
        case "wordsAdd":
            if (wordsAmountInt < 9) {
                wordsAmountInt++;
                WordsAmountText = wordsAmountInt.ToString();
            }

            break;
        case "wordsSub":
            if (wordsAmountInt > 1) {
                wordsAmountInt--;
                WordsAmountText = wordsAmountInt.ToString();
            }

            break;
        case "timerAdd":
            if (timerCountdownNum < 999) {
                timerCountdownNum++;
                TimerAmountText = timerCountdownNum.ToString();
            }

            break;
        case "timerSub":
            if (timerCountdownNum > 1) {
                timerCountdownNum--;
                TimerAmountText = timerCountdownNum.ToString();
            }

            break;
        case "play":
            if (WordsEasyColor == "Transparent" && WordsMediumColor == "Transparent" && WordsHardColor == "Transparent" && WordsAdultColor == "Transparent") {
                MessageBox.Show("At least one word level must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                isTimerActive = true;
                isGameActive = true;
                MenuVisibility = "HIDDEN";
                GameVisibility = "VISIBLE";

                GeneratePlayableWords();
                NextWord();
            }

            break;
        case "nextWord":
            NextWord();
            break;
        }
    }

    private void NextWord() {
        OutputText = "";
        int i = 4;
        try {
            i = int.Parse(WordsAmountText);
        } catch (Exception) { }

        try {
            for (int i2 = 0; i2 < i; i2++) {
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

    public string ProgressBarValue {
        get => _progressBarValue;
        set {
            _progressBarValue = value;
            RaisePropertyChangedEvent("ProgressBarValue");
        }
    }

    public string ProgressBarVisibility {
        get => _progressBarVisibility;
        set {
            _progressBarVisibility = value;
            RaisePropertyChangedEvent("ProgressBarVisibility");
        }
    }

    public string ProgressBarValueMaster {
        get => _progressBarValueMaster;
        set {
            _progressBarValueMaster = value;
            RaisePropertyChangedEvent("ProgressBarValueMaster");
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

    public string WordsAmountText {
        get => _wordsAmountText;
        set {
            _wordsAmountText = value;
            RaisePropertyChangedEvent("WordsAmountText");
        }
    }

    public string TimerAmountText {
        get => _timerAmountText;
        set {
            _timerAmountText = value;
            RaisePropertyChangedEvent("TimerAmountText");
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

    public string RealTime {
        get => _realTime;
        set {
            _realTime = value;
            RaisePropertyChangedEvent("RealTime");
        }
    }

    public string OutputText {
        get => _outputText;
        set {
            _outputText = value;
            RaisePropertyChangedEvent("OutputText");
        }
    }

    #endregion
}