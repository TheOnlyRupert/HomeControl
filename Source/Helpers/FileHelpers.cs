using System.IO;
using MySql.Data.MySqlClient;

namespace HomeControl.Source.Helpers;

public static class FileHelpers {
    public static string? LoadFileText(string fileName, bool isDocumentsFolder) {
        string filePath = GetFilePath(fileName, isDocumentsFolder);

        try {
            if (!File.Exists(filePath)) {
                LogDebugMessage("WARN", "FileHelpers.LoadFileText", $"File not found: {filePath}");
                return null;
            }

            return File.ReadAllText(filePath);
        } catch (Exception e) {
            LogDebugMessage("WARN", "FileHelpers.LoadFileText", $"Error loading file: {filePath}\n{e}");
            return null;
        }
    }

    public static void SaveFileText(string fileName, string fileText, bool isDocumentsFolder) {
        string filePath = GetFilePath(fileName, isDocumentsFolder);

        try {
            File.WriteAllText(filePath, fileText);
        } catch (Exception e) {
            LogDebugMessage("WARN", "FileHelpers.SaveFileText", $"Error saving file: {filePath}\n{e}");
        }
    }

    private static string GetFilePath(string fileName, bool isDocumentsFolder) {
        string baseDirectory = isDocumentsFolder
            ? ReferenceValues.DocumentsDirectory
            : ReferenceValues.AppDirectory;

        return Path.Combine(baseDirectory, $"{fileName}.json");
    }

    public static void LogDebugMessage(string logLevel, string module, string message) {
        using MySqlConnection connection = new(ReferenceValues.DatabaseConnectionString);
        const string query = "INSERT INTO debug_log (log_level, module, message) VALUES (@logLevel, @module, @message)";

        try {
            connection.Open();

            using (MySqlCommand command = new(query, connection)) {
                command.Parameters.AddWithValue("@logLevel", logLevel);
                command.Parameters.AddWithValue("@module", module);
                command.Parameters.AddWithValue("@message", message);

                command.ExecuteNonQuery();
            }

            connection.Close();
        } catch (Exception) {
            //todo: this
        }
    }
}