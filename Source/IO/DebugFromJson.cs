using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class DebugFromJson {
    public DebugFromJson() {
        ReferenceValues.DebugTextBlockOutput = new ObservableCollection<DebugTextBlock>();

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "debug.json");
            string settingsString = null;
            while (!streamReader.EndOfStream) {
                settingsString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (settingsString != null) {
                try {
                    ObservableCollection<DebugTextBlock> jsonDebug = JsonSerializer.Deserialize<ObservableCollection<DebugTextBlock>>(settingsString, options);
                    ReferenceValues.DebugTextBlockOutput = jsonDebug;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "SettingsFromJson",
                        Description = e.ToString()
                    });
                    SaveDebugFile.Save();
                }
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "SettingsFromJson",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }
    }
}