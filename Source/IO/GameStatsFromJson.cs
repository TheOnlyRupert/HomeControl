using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class GameStatsFromJson {
    public GameStatsFromJson() {
        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "gameStats.json");
            string gameStatsString = null;
            while (!streamReader.EndOfStream) {
                gameStatsString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (gameStatsString != null) {
                try {
                    JsonGameStats jsonGameStats = JsonSerializer.Deserialize<JsonGameStats>(gameStatsString, options);
                    ReferenceValues.JsonGameStatsMaster = jsonGameStats;
                } catch (Exception e) {
                    Console.WriteLine("Failed to Deserialize gameStats.json..." + e);
                }
            }
        } catch (Exception) { }
    }
}