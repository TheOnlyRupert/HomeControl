using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class RecipeFromJson {
    public RecipeFromJson() {
        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "recipes.json");
            string financeListString = null;
            while (!streamReader.EndOfStream) {
                financeListString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (financeListString != null) {
                try {
                    JsonRecipe jsonRecipe = JsonSerializer.Deserialize<JsonRecipe>(financeListString, options);
                    ReferenceValues.JsonRecipesMaster = jsonRecipe;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "RecipeFromJson",
                        Description = e.ToString()
                    });
                    SaveDebugFile.Save();
                }
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "RecipeFromJson",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }
    }
}