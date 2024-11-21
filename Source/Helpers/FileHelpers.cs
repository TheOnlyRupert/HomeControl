using System.IO;
using System.Text.Json;
using HomeControl.Source.Json;

namespace HomeControl.Source.Helpers;

public static class FileHelpers {
    public static string LoadFileText(string fileName, bool isDocumentsFolder) {
        string filePath = GetFilePath(fileName, isDocumentsFolder);

        try {
            if (!File.Exists(filePath)) {
                LogWarning($"File not found: {filePath}");
                return null;
            }

            // Read all text at once for simplicity
            return File.ReadAllText(filePath);
        } catch (Exception e) {
            LogWarning($"Error reading file: {filePath}\n{e}");
            return null;
        }
    }

    public static void SaveFileText(string fileName, string fileText, bool isDocumentsFolder) {
        string filePath = GetFilePath(fileName, isDocumentsFolder);

        try {
            // Write all text at once
            File.WriteAllText(filePath, fileText);
        } catch (Exception e) {
            LogWarning($"Error saving file: {filePath}\n{e}");
        }
    }

    private static string GetFilePath(string fileName, bool isDocumentsFolder) {
        string baseDirectory = isDocumentsFolder
            ? ReferenceValues.DocumentsDirectory
            : ReferenceValues.AppDirectory;

        return Path.Combine(baseDirectory, $"{fileName}.json");
    }

    private static void LogWarning(string message) {
        ReferenceValues.JsonDebugMaster.DebugBlockList.Add(new DebugTextBlock {
            Date = DateTime.Now,
            Level = "WARN",
            Module = nameof(FileHelpers),
            Description = message
        });

        // Save updated debug log
        try {
            string debugFilePath = Path.Combine(ReferenceValues.DocumentsDirectory, "debug.json");
            File.WriteAllText(debugFilePath, JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster));
        } catch {
            // Fail silently if the debug log cannot be saved
        }
    }
}