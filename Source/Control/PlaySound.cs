using System;
using System.Media;
using System.Windows;
using System.Windows.Resources;

namespace HomeControl.Source.Control;

public class PlaySound {
    private readonly SoundPlayer _audio;
    private readonly bool _canPlay;

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
                _audio.Play();
            } else {
                _audio.PlayLooping();
            }
        }
    }

    public void Stop() {
        _audio.Stop();
    }
}