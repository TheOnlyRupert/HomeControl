using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class DailyFromJson {
    public DailyFromJson() {
        Directory.CreateDirectory(ReferenceValues.FILE_DIRECTORY + "daily/");
    }

    public void DailyDayFromJson(DateTime dateTime) {
        string fileName = ReferenceValues.FILE_DIRECTORY + "daily/daily_" + dateTime.ToString("yyyy_MM_dd") + ".json";

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(fileName);
            string dailyString = null;
            while (!streamReader.EndOfStream) {
                dailyString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (dailyString != null) {
                try {
                    JsonDaily jsonDaily = JsonSerializer.Deserialize<JsonDaily>(dailyString, options);
                    ReferenceValues.JsonDailyMasterList = jsonDaily;
                } catch (Exception e) {
                    Console.WriteLine("Failed to Deserialize daily.json..." + e);
                }
            }
        } catch (Exception) {
            JsonDaily JsonDaily = new();
            ObservableCollection<DailyDetails> dailyList = new() {
                new DailyDetails {
                    Name = "User1Task1",
                    IsComplete = false,
                    Time = "0000"
                },
                new DailyDetails {
                    Name = "User1Task2",
                    IsComplete = false,
                    Time = "0000"
                },
                new DailyDetails {
                    Name = "User2Task1",
                    IsComplete = false,
                    Time = "0000"
                },
                new DailyDetails {
                    Name = "User2Task2",
                    IsComplete = false,
                    Time = "0000"
                },
                new DailyDetails {
                    Name = "User2Task3",
                    IsComplete = false,
                    Time = "0000"
                },
                new DailyDetails {
                    Name = "User3Task1",
                    IsComplete = false,
                    Time = "0000"
                }
            };

            JsonDaily.dailyList = dailyList;
            ReferenceValues.JsonDailyMasterList = JsonDaily;

            try {
                string jsonString = JsonSerializer.Serialize(JsonDaily);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
            }
        }
    }
}