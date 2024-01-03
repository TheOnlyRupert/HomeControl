using System;
using System.Text.Json;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel.Calendar;

public class EditFilterVM : BaseViewModel {
    private BitmapImage _imageUser0, _imageUser1, _imageUser2, _imageUser3, _imageUser4, _imageUser5;
    private string _user0BorderColor, _user1BorderColor, _user2BorderColor, _user3BorderColor, _user4BorderColor, _user5BorderColor;
    private int _user0BorderThickness, _user1BorderThickness, _user2BorderThickness, _user3BorderThickness, _user4BorderThickness, _user5BorderThickness;

    public EditFilterVM() {
        try {
            Uri uri = new(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user0.png", UriKind.RelativeOrAbsolute);
            ImageUser0 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user1.png", UriKind.RelativeOrAbsolute);
            ImageUser1 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user2.png", UriKind.RelativeOrAbsolute);
            ImageUser2 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user3.png", UriKind.RelativeOrAbsolute);
            ImageUser3 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user4.png", UriKind.RelativeOrAbsolute);
            ImageUser4 = new BitmapImage(uri);
            uri = new Uri(ReferenceValues.DOCUMENTS_DIRECTORY + "icons/user5.png", UriKind.RelativeOrAbsolute);
            ImageUser5 = new BitmapImage(uri);
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "EditCalendarVM",
                Description = e.ToString()
            });
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }

        UserLogic(0);
        UserLogic(1);
        UserLogic(2);
        UserLogic(3);
        UserLogic(4);
        UserLogic(5);
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

    private void ButtonLogic(object param) {
        switch (param) {
        case "user0":
            ReferenceValues.CalendarFilterList[0] = !ReferenceValues.CalendarFilterList[0];
            UserLogic(0);

            break;
        case "user1":
            ReferenceValues.CalendarFilterList[1] = !ReferenceValues.CalendarFilterList[1];
            UserLogic(1);

            break;
        case "user2":
            ReferenceValues.CalendarFilterList[2] = !ReferenceValues.CalendarFilterList[2];
            UserLogic(2);

            break;
        case "user3":
            ReferenceValues.CalendarFilterList[3] = !ReferenceValues.CalendarFilterList[3];
            UserLogic(3);

            break;
        case "user4":
            ReferenceValues.CalendarFilterList[4] = !ReferenceValues.CalendarFilterList[4];
            UserLogic(4);

            break;
        case "user5":
            ReferenceValues.CalendarFilterList[5] = !ReferenceValues.CalendarFilterList[5];
            UserLogic(5);

            break;
        case "reset":
            ReferenceValues.CalendarFilterList = new[] { true, true, true, true, true, true };
            UserLogic(0);
            UserLogic(1);
            UserLogic(2);
            UserLogic(3);
            UserLogic(4);
            UserLogic(5);

            break;
        }
    }

    private void UserLogic(int user) {
        switch (user) {
        case 0:
            if (ReferenceValues.CalendarFilterList[0]) {
                User0BorderColor = "Green";
                User0BorderThickness = 4;
            } else {
                User0BorderColor = "DarkSlateGray";
                User0BorderThickness = 1;
            }

            break;
        case 1:
            if (ReferenceValues.CalendarFilterList[1]) {
                User1BorderColor = "Green";
                User1BorderThickness = 4;
            } else {
                User1BorderColor = "DarkSlateGray";
                User1BorderThickness = 1;
            }

            break;
        case 2:
            if (ReferenceValues.CalendarFilterList[2]) {
                User2BorderColor = "Green";
                User2BorderThickness = 4;
            } else {
                User2BorderColor = "DarkSlateGray";
                User2BorderThickness = 1;
            }

            break;
        case 3:
            if (ReferenceValues.CalendarFilterList[3]) {
                User3BorderColor = "Green";
                User3BorderThickness = 4;
            } else {
                User3BorderColor = "DarkSlateGray";
                User3BorderThickness = 1;
            }

            break;
        case 4:
            if (ReferenceValues.CalendarFilterList[4]) {
                User4BorderColor = "Green";
                User4BorderThickness = 4;
            } else {
                User4BorderColor = "DarkSlateGray";
                User4BorderThickness = 1;
            }

            break;
        case 5:
            if (ReferenceValues.CalendarFilterList[5]) {
                User5BorderColor = "Green";
                User5BorderThickness = 4;
            } else {
                User5BorderColor = "DarkSlateGray";
                User5BorderThickness = 1;
            }

            break;
        }
    }

    #region Fields

    public BitmapImage ImageUser0 {
        get => _imageUser0;
        set {
            _imageUser0 = value;
            RaisePropertyChangedEvent("ImageUser0");
        }
    }

    public BitmapImage ImageUser1 {
        get => _imageUser1;
        set {
            _imageUser1 = value;
            RaisePropertyChangedEvent("ImageUser1");
        }
    }

    public BitmapImage ImageUser2 {
        get => _imageUser2;
        set {
            _imageUser2 = value;
            RaisePropertyChangedEvent("ImageUser2");
        }
    }

    public BitmapImage ImageUser3 {
        get => _imageUser3;
        set {
            _imageUser3 = value;
            RaisePropertyChangedEvent("ImageUser3");
        }
    }

    public BitmapImage ImageUser4 {
        get => _imageUser4;
        set {
            _imageUser4 = value;
            RaisePropertyChangedEvent("ImageUser4");
        }
    }

    public BitmapImage ImageUser5 {
        get => _imageUser5;
        set {
            _imageUser5 = value;
            RaisePropertyChangedEvent("ImageUser5");
        }
    }

    public string User0BorderColor {
        get => _user0BorderColor;
        set {
            _user0BorderColor = value;
            RaisePropertyChangedEvent("User0BorderColor");
        }
    }

    public string User1BorderColor {
        get => _user1BorderColor;
        set {
            _user1BorderColor = value;
            RaisePropertyChangedEvent("User1BorderColor");
        }
    }

    public string User2BorderColor {
        get => _user2BorderColor;
        set {
            _user2BorderColor = value;
            RaisePropertyChangedEvent("User2BorderColor");
        }
    }

    public string User3BorderColor {
        get => _user3BorderColor;
        set {
            _user3BorderColor = value;
            RaisePropertyChangedEvent("User3BorderColor");
        }
    }

    public string User4BorderColor {
        get => _user4BorderColor;
        set {
            _user4BorderColor = value;
            RaisePropertyChangedEvent("User4BorderColor");
        }
    }

    public string User5BorderColor {
        get => _user5BorderColor;
        set {
            _user5BorderColor = value;
            RaisePropertyChangedEvent("User5BorderColor");
        }
    }

    public int User1BorderThickness {
        get => _user1BorderThickness;
        set {
            _user1BorderThickness = value;
            RaisePropertyChangedEvent("User1BorderThickness");
        }
    }

    public int User0BorderThickness {
        get => _user0BorderThickness;
        set {
            _user0BorderThickness = value;
            RaisePropertyChangedEvent("User0BorderThickness");
        }
    }

    public int User2BorderThickness {
        get => _user2BorderThickness;
        set {
            _user2BorderThickness = value;
            RaisePropertyChangedEvent("User2BorderThickness");
        }
    }

    public int User3BorderThickness {
        get => _user3BorderThickness;
        set {
            _user3BorderThickness = value;
            RaisePropertyChangedEvent("User3BorderThickness");
        }
    }

    public int User4BorderThickness {
        get => _user4BorderThickness;
        set {
            _user4BorderThickness = value;
            RaisePropertyChangedEvent("User4BorderThickness");
        }
    }

    public int User5BorderThickness {
        get => _user5BorderThickness;
        set {
            _user5BorderThickness = value;
            RaisePropertyChangedEvent("User5BorderThickness");
        }
    }

    #endregion
}