using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using HomeControl.Source.Helpers;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Games;

public class NhieVM : BaseViewModel {
    private static bool isGameActive;

    private string _menuVisibility, _questionsNormalColor, _questionsAdultColor, _gameVisibility, _outputText, _button1Text, _button2Text;

    private List<string> playableQuestions;

    public NhieVM() {
        MenuVisibility = "VISIBLE";
        GameVisibility = "HIDDEN";

        Button1Text = "Play";
        Button2Text = "Play";

        QuestionsNormalColor = "Green";
        QuestionsAdultColor = "Transparent";
        isGameActive = false;
        OutputText = "";
        playableQuestions = new List<string>();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "questionsNormal":
            QuestionsNormalColor = QuestionsNormalColor == "Green" ? "Transparent" : "Green";

            break;
        case "questionsAdult":
            QuestionsAdultColor = QuestionsAdultColor == "Green" ? "Transparent" : "Green";

            break;
        case "button1":
            if (isGameActive) {
                NextQuestion();
            } else {
                if (QuestionsNormalColor == "Transparent" && QuestionsAdultColor == "Transparent") {
                    MessageBox.Show("At least one question level must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {
                    isGameActive = true;
                    MenuVisibility = "HIDDEN";
                    GameVisibility = "VISIBLE";
                    Button1Text = "Next Question";
                    Button2Text = "Main Menu";

                    GeneratePlayableQuestions();
                    NextQuestion();
                }
            }

            break;
        case "button2":
            if (isGameActive) {
                isGameActive = false;
                MenuVisibility = "VISIBLE";
                GameVisibility = "HIDDEN";
                Button1Text = "Play";
                Button2Text = "Play";
            } else {
                if (QuestionsNormalColor == "Transparent" && QuestionsAdultColor == "Transparent") {
                    MessageBox.Show("At least one word level must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {
                    isGameActive = true;
                    MenuVisibility = "HIDDEN";
                    GameVisibility = "VISIBLE";
                    Button1Text = "Next Word";
                    Button2Text = "Main Menu";

                    GeneratePlayableQuestions();
                    NextQuestion();
                }
            }

            break;
        }
    }

    private void NextQuestion() {
        OutputText = "";
        Random random = new();
        int i = random.Next(playableQuestions.Count);

        try {
            OutputText = "NEVER HAVE I EVER:\n" + playableQuestions[i];
            playableQuestions.RemoveAt(i);
        } catch (ArgumentOutOfRangeException) {
            GeneratePlayableQuestions();
            NextQuestion();
        }
    }

    private void GeneratePlayableQuestions() {
        playableQuestions.Clear();

        if (QuestionsNormalColor == "Green") {
            foreach (string word in WordList.NhieQuestionList) {
                playableQuestions.Add(word);
            }
        }

        if (QuestionsAdultColor == "Green") {
            foreach (string word in WordList.NhieQuestionListAdult) {
                playableQuestions.Add(word);
            }
        }

        /* Randomize the list */
        playableQuestions = ListHelpers.RandomizeList(playableQuestions);
    }

    #region Fields

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

    public string QuestionsAdultColor {
        get => _questionsAdultColor;
        set {
            _questionsAdultColor = value;
            RaisePropertyChangedEvent("QuestionsAdultColor");
        }
    }

    public string QuestionsNormalColor {
        get => _questionsNormalColor;
        set {
            _questionsNormalColor = value;
            RaisePropertyChangedEvent("QuestionsNormalColor");
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

    #endregion
}