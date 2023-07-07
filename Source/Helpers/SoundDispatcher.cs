using System;
using System.Threading.Tasks;
using System.Windows.Media;
using HomeControl.Source.Reference;

namespace HomeControl.Source.Helpers;

public static class SoundDispatcher {
    private static MediaPlayer mplayer;

    public static void PlaySound() {
        Task.Factory.StartNew(PlaySoundThreaded);
    }

    private static void PlaySoundThreaded() {
        mplayer = new MediaPlayer();
        mplayer.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/" + ReferenceValues.SoundToPlay + ".wav"));
        mplayer.Play();
    }
}