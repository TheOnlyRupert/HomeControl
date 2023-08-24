using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class TimerFromJson {
    public TimerFromJson() {
        ReferenceValues.JsonTimerSettings = new JsonTimer();

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "timer.json");
            string timerJson = null;
            while (!streamReader.EndOfStream) {
                timerJson = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (timerJson != null) {
                try {
                    JsonTimer jsonTimerSettings = JsonSerializer.Deserialize<JsonTimer>(timerJson, options);
                    ReferenceValues.JsonTimerSettings = jsonTimerSettings;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "TimerFromJson",
                        Description = e.ToString()
                    });
                    SaveDebugFile.Save();
                }
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "TimerFromJson",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }
    }
}