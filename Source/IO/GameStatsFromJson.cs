﻿using System;
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
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "GameStatsFromJson",
                        Description = e.ToString()
                    });
                    SaveDebugFile.Save();
                }
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "GameStatsFromJson",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }
    }
}