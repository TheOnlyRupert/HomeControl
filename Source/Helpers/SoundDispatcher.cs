using System.IO;
using System.Media;

public static class SoundDispatcher {
    public static void PlaySound(string soundFilePath) {
        if (File.Exists(soundFilePath)) {
            using SoundPlayer player = new("pack://siteoforigin:,,,/Resources/Sounds/" + soundFilePath + ".wav");
            player.PlaySync();
        } else {
            Console.WriteLine($"Sound file not found: {soundFilePath}");
        }
    }
}