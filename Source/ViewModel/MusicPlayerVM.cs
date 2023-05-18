using System.Windows.Input;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class MusicPlayerVM : BaseViewModel {
    private int _progressValue;
    private string _songName, _shuffleIcon, _artistName, _timer1Text, _timer2Text, _imageName;

    public MusicPlayerVM() {
        ImageName = null;
        ArtistName = null;
        SongName = null;
        Timer1Text = null;
        Timer2Text = null;

        //File tagFile = File.Create("C:/Users/higgi/OneDrive/Desktop/untitled.mp3");
        //Console.WriteLine(tagFile.Length + ", " + tagFile.Tag.Title + ", " + tagFile.Properties.Duration.Seconds);

        //ThreadedAudioPlayerLogic();
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);


    private static void ThreadedAudioPlayerLogic() {
        //Dispatcher.CurrentDispatcher.VerifyAccess();

        //MediaPlayer mediaPlayer = new();
        //mediaPlayer.Open(new Uri("C:/Users/higgi/OneDrive/Desktop/untitled.mp3"));
        //mediaPlayer.Play();

        //Dispatcher.Run();
    }

    private void ButtonCommandLogic(object param) {
        switch (param) {
        case "play":
            break;
        }
    }

    #region Fields

    public string SongName {
        get => _songName;
        set {
            _songName = value;
            RaisePropertyChangedEvent("SongName");
        }
    }

    public string ShuffleIcon {
        get => _shuffleIcon;
        set {
            _shuffleIcon = value;
            RaisePropertyChangedEvent("ShuffleIcon");
        }
    }

    public string ArtistName {
        get => _artistName;
        set {
            _artistName = value;
            RaisePropertyChangedEvent("ArtistName");
        }
    }

    public string Timer1Text {
        get => _timer1Text;
        set {
            _timer1Text = value;
            RaisePropertyChangedEvent("Timer1Text");
        }
    }

    public string Timer2Text {
        get => _timer2Text;
        set {
            _timer2Text = value;
            RaisePropertyChangedEvent("Timer2Text");
        }
    }

    public int ProgressValue {
        get => _progressValue;
        set {
            _progressValue = value;
            RaisePropertyChangedEvent("ProgressValue");
        }
    }

    public string ImageName {
        get => _imageName;
        set {
            _imageName = value;
            RaisePropertyChangedEvent("ImageName");
        }
    }

    #endregion
}