using System;
using System.Media;
using System.Windows;
using System.Windows.Resources;

namespace HomeControl.Source.Control;

public class PlaySound {
    private readonly SoundPlayer _audio;
    private readonly bool _canPlay;
    private bool _isPlaying;

    public PlaySound(string name) {
        StreamResourceInfo sri = Application.GetResourceStream(new Uri("pack://application:,,,/HomeControl;component/Resources/Sounds/" + name + ".wav"));

        if (sri != null) {
            _audio = new SoundPlayer(sri.Stream);
            _audio.Load();
            _canPlay = true;
        } else {
            _canPlay = false;
        }
    }

    public void Play(bool isLooping) {
        if (_canPlay) {
            if (isLooping) {
                _audio.PlayLooping();
            } else {
                _audio.Play();
            }

            _isPlaying = true;
        }
    }

    public void Stop() {
        _audio.Stop();
        _isPlaying = false;
    }

    public bool IsPlaying() {
        return _isPlaying;
    }
}