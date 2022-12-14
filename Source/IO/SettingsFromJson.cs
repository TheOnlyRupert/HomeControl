using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class SettingsFromJson {
    public SettingsFromJson() {
        ReferenceValues.JsonMasterSettings = new JsonSettings();

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "settings.json");
            string settingsString = null;
            while (!streamReader.EndOfStream) {
                settingsString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (settingsString != null) {
                try {
                    JsonSettings jsonSettings = JsonSerializer.Deserialize<JsonSettings>(settingsString, options);
                    ReferenceValues.JsonMasterSettings = jsonSettings;
                } catch (Exception e) {
                    Console.WriteLine("Failed to Deserialize settings.json..." + e);
                }
            }
        } catch (Exception) { }
    }
}