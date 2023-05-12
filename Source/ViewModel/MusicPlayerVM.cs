using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using HomeControl.Source.IO;
using HomeControl.Source.ViewModel.Base;

namespace HomeControl.Source.ViewModel;

public class MusicPlayerVM : BaseViewModel {
    private string _songName, _shuffleIcon, _artistName, _timer1Text, _timer2Text;
    private int _progressValue;

    public MusicPlayerVM() {
        SpotifyToken token = GetAccessToken();
        try {
            Playlist playlist = GetPlayLists(token.access_token, "1");
            Console.WriteLine(playlist.href);
        } catch (Exception e) {
            Console.WriteLine(e);
        }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private SpotifyToken GetAccessToken() {
        try {
            const string postString = "grant_type=client_credentials";
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);

            WebRequest request = WebRequest.Create("https://accounts.spotify.com/api/token");
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic YmRjYzZiMDdiNmE1NDAwZWFlN2Y5MDRmNmM2NzE0Y2I6M2I4NzYzYmFkNTdiNGFjNWE1Mjc4NjgyMzI1NzIwODQ");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new(responseStream);
            string responseFromServer = reader.ReadToEnd();

            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            return JsonSerializer.Deserialize<SpotifyToken>(responseFromServer, options);
        } catch (Exception) {
            return default;
        }
    }

    private static T GetSpotifyType<T>(string token, string url) {
        try {
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json; charset=utf-8";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Console.WriteLine(responseFromServer);

            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            return JsonSerializer.Deserialize<T>(responseFromServer, options);
        } catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    private Playlist GetPlayLists(string token, string user) {
        string url = $"https://api.spotify.com/v1/users/{user}/playlists";
        Playlist playLists = GetSpotifyType<Playlist>(token, url);
        return playLists;
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

    #endregion
}