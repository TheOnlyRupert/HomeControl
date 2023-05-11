using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class ChoreFundsFromJson {
    public ChoreFundsFromJson() {
        ReferenceValues.JsonChoreFundsMaster = new JsonChoreFunds();

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "chorefunds.json");
            string settingsString = null;
            while (!streamReader.EndOfStream) {
                settingsString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (settingsString != null) {
                try {
                    JsonChoreFunds jsonSettings = JsonSerializer.Deserialize<JsonChoreFunds>(settingsString, options);
                    ReferenceValues.JsonChoreFundsMaster = jsonSettings;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoreFundsFromJson",
                        Description = e.ToString()
                    });
                }
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "ChoreFundsFromJson",
                Description = e.ToString()
            });
        }
    }
}