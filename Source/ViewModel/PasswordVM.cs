using System;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class PasswordVM : BaseViewModel {
    private readonly bool passwordAccepted;
    private string _passwordText, _passwordPart1Visibility, _passwordPart2Visibility, _lockButtonText;

    public PasswordVM() {
        ReferenceValues.LockUI = true;
        PasswordText = "";
        LockButtonText = "";
        PasswordPart1Visibility = "VISIBLE";
        PasswordPart2Visibility = "HIDDEN";
        passwordAccepted = true;

        CrossViewMessenger simpleMessenger = CrossViewMessenger.Instance;
        simpleMessenger.MessageValueChanged += OnSimpleMessengerValueChanged;
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private void OnSimpleMessengerValueChanged(object sender, MessageValueChangedEventArgs e) {
        if (e.PropertyName == "ScreenSaverOn") {
            PasswordText = "";
            LockButtonText = "";
            ReferenceValues.LockUI = true;
            PasswordPart1Visibility = "VISIBLE";
            PasswordPart2Visibility = "HIDDEN";
        }
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "0":
            if (PasswordText.Length < 6) {
                PasswordText += "0";
            }

            break;
        case "1":
            if (PasswordText.Length < 6) {
                PasswordText += "1";
            }

            break;
        case "2":
            if (PasswordText.Length < 6) {
                PasswordText += "2";
            }

            break;
        case "3":
            if (PasswordText.Length < 6) {
                PasswordText += "3";
            }

            break;
        case "4":
            if (PasswordText.Length < 6) {
                PasswordText += "4";
            }

            break;
        case "5":
            if (PasswordText.Length < 6) {
                PasswordText += "5";
            }

            break;
        case "6":
            if (PasswordText.Length < 6) {
                PasswordText += "6";
            }

            break;
        case "7":
            if (PasswordText.Length < 6) {
                PasswordText += "7";
            }

            break;
        case "8":
            if (PasswordText.Length < 6) {
                PasswordText += "8";
            }

            break;
        case "9":
            if (PasswordText.Length < 6) {
                PasswordText += "9";
            }

            break;
        case "go":
            if (passwordAccepted) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "INFO",
                    Module = "PasswordVM",
                    Description = "Unlocking UI with correct password"
                });
                SaveDebugFile.Save();

                PasswordText = "";
                LockButtonText = "Lock UI";
                ReferenceValues.LockUI = false;
                PasswordPart1Visibility = "HIDDEN";
                PasswordPart2Visibility = "VISIBLE";
            }

            break;
        case "back":
            if (PasswordText.Length > 0) {
                PasswordText = PasswordText.Substring(0, PasswordText.Length - 1);
            }

            break;
        case "lock":
            PasswordText = "";
            LockButtonText = "";
            ReferenceValues.LockUI = true;
            PasswordPart1Visibility = "VISIBLE";
            PasswordPart2Visibility = "HIDDEN";
            break;
        }
    }

    #region Fields

    public string PasswordText {
        get => _passwordText;
        set {
            _passwordText = value;
            RaisePropertyChangedEvent("PasswordText");
        }
    }

    public string PasswordPart1Visibility {
        get => _passwordPart1Visibility;
        set {
            _passwordPart1Visibility = value;
            RaisePropertyChangedEvent("PasswordPart1Visibility");
        }
    }

    public string PasswordPart2Visibility {
        get => _passwordPart2Visibility;
        set {
            _passwordPart2Visibility = value;
            RaisePropertyChangedEvent("PasswordPart2Visibility");
        }
    }

    public string LockButtonText {
        get => _lockButtonText;
        set {
            _lockButtonText = value;
            RaisePropertyChangedEvent("LockButtonText");
        }
    }

    #endregion
}