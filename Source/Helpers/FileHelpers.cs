using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.Helpers;

public static class FileHelpers {
    public static string LoadFileText(string fileName) {
        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + fileName + ".json");
            string fileText = null;
            while (!streamReader.EndOfStream) {
                fileText = streamReader.ReadToEnd();
            }

            streamReader.Close();

            return fileText;
        } catch (Exception e) {
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "BehaviorFromJson",
                Description = e.ToString()
            });
            SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));

            return null;
        }
    }

    public static void SaveFileText(string fileName, string fileText) {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        File.WriteAllText(ReferenceValues.FILE_DIRECTORY + fileName + ".json", fileText);
    }
}