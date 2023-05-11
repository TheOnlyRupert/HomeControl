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
    public MusicPlayerVM() {
        SpotifyToken token = GetAccessToken();
        try {
            GetPlayLists(token.access_token, "");
        } catch (Exception) { }
    }

    public ICommand ButtonCommand => new DelegateCommand(ButtonCommandLogic, true);

    private SpotifyToken GetAccessToken() {
        try {
            const string postString = "grant_type=client_credentials";
            byte[] byteArray = Encoding.UTF8.GetBytes(postString);

            WebRequest request = WebRequest.Create("https://accounts.spotify.com/api/token");
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic YmRjYzZiMDdiNmE1NDAwZWFlN2Y5MDRmNmM2NzE0Y2I6ZTBiNzIwY2VkYzM1NDg4MzlhMjdkZGYyMGIxZDU1ZjQ");
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

            using WebResponse response = request.GetResponse();
            using Stream dataStream = response.GetResponseStream();
            using StreamReader reader = new(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Console.WriteLine(responseFromServer);

            JsonSerializerOptions options = new() {
                IncludeFields = true
            };

            return JsonSerializer.Deserialize<T>(responseFromServer, options);
        } catch (Exception) {
            return default;
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
}