using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class HvacFromJson {
    public HvacFromJson() {
        ReferenceValues.JsonHvacSettings = new JsonHvac();

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "hvac.json");
            string hvacString = null;
            while (!streamReader.EndOfStream) {
                hvacString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (hvacString != null) {
                try {
                    JsonHvac jsonHvacSettings = JsonSerializer.Deserialize<JsonHvac>(hvacString, options);
                    ReferenceValues.JsonHvacSettings = jsonHvacSettings;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "HvacFromJson",
                        Description = e.ToString()
                    });
                    SaveDebugFile.Save();
                }
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "HvacFromJson",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }
    }
}