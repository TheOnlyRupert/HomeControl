using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Json;

namespace HomeControl.Source.Helpers;

public static class FileHelpers {
    public static string LoadFileText(string fileName, bool isDocumentsFolder) {
        try {
            StreamReader streamReader = isDocumentsFolder
                ? new StreamReader(ReferenceValues.DOCUMENTS_DIRECTORY + fileName + ".json")
                : new StreamReader(ReferenceValues.AppDirectory + fileName + ".json");

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
                Module = "FileHelpers",
                Description = e.ToString()
            });
            SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);

            return null;
        }
    }

    public static void SaveFileText(string fileName, string fileText, bool isDocumentsFolder) {
        if (isDocumentsFolder) {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.DOCUMENTS_DIRECTORY + fileName + ".json", fileText);
        } else {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.AppDirectory + fileName + ".json", fileText);
        }
    }
}