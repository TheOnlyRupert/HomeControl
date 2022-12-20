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
            ObservableCollection<DailyDetails> dailyListUser1 = new() {
                new DailyDetails {
                    Name = "User1Task1",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task2",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task3",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task4",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task5",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task6",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task7",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task8",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task9",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User1Task10",
                    IsComplete = false,
                    Time = ""
                }
            };

            ObservableCollection<DailyDetails> dailyListUser2 = new() {
                new DailyDetails {
                    Name = "User2Task1",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task2",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task3",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task4",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task5",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task6",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task7",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task8",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task9",
                    IsComplete = false,
                    Time = ""
                },
                new DailyDetails {
                    Name = "User2Task10",
                    IsComplete = false,
                    Time = ""
                }
            };

            ObservableCollection<DailyDetails> dailyListUser3 = new() {
                new DailyDetails {
                    Name = "User3Task1",
                    IsComplete = false,
                    Time = ""
                }
            };

            ObservableCollection<DailyDetails> dailyListUser4 = new() {
                new DailyDetails {
                    Name = "User4Task1",
                    IsComplete = false,
                    Time = ""
                }
            };

            ObservableCollection<DailyDetails> dailyListUser5 = new() {
                new DailyDetails {
                    Name = "User5Task1",
                    IsComplete = false,
                    Time = ""
                }
            };

            JsonDaily.dailyListUser1 = dailyListUser1;
            JsonDaily.dailyListUser2 = dailyListUser2;
            JsonDaily.dailyListUser3 = dailyListUser3;
            JsonDaily.dailyListUser4 = dailyListUser4;
            JsonDaily.dailyListUser5 = dailyListUser5;
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