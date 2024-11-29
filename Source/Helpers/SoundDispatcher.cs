using System.IO;
using System.Reflection;
using NAudio.Wave;

namespace HomeControl.Source.Helpers;

public abstract class SoundDispatcher {
    public static void PlaySound(string resourceName) {
        string fullResourceName = $"HomeControl.Resources.Sounds.{resourceName}.wav";

        Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName);
        if (stream != null) {
            MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0;

            try {
                WaveFileReader waveStream = new(memoryStream);
                WaveOutEvent outputDevice = new();

                outputDevice.Init(waveStream);
                outputDevice.Play();

                outputDevice.PlaybackStopped += (_, _) => {
                    outputDevice.Dispose();
                    waveStream.Dispose();
                    memoryStream.Dispose();
                };
            } catch (Exception ex) {
                FileHelpers.LogDebugMessage("WARN", "SoundDispatcher.PlaySound", $"An error occurred while playing sound:\n{ex.Message}");
            }
        } else {
            FileHelpers.LogDebugMessage("WARN", "SoundDispatcher.PlaySound", $"Sound Resource not found: {fullResourceName}");
        }
    }
}