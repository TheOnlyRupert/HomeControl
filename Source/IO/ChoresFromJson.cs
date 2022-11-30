using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class ChoresFromJson {
    public ChoresFromJson() {
        Directory.CreateDirectory(ReferenceValues.FILE_DIRECTORY + "chores/");
    }

    public void ChoresWeekFromJson(DateTime dateTime) {
        string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_week_" + dateTime.ToString("yyyy_MM_dd") + ".json";

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(fileName);
            string choresListString = null;
            while (!streamReader.EndOfStream) {
                choresListString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (choresListString != null) {
                try {
                    JsonChores jsonChores = JsonSerializer.Deserialize<JsonChores>(choresListString, options);
                    ReferenceValues.JsonChoreWeekMasterList = jsonChores;
                } catch (Exception e) {
                    Console.WriteLine("Failed to Deserialize chores.json..." + e);
                }
            }
        } catch (Exception) {
            JsonChores jsonChores = new();
            ObservableCollection<ChoreDetails> choreList = new() {
                new ChoreDetails {
                    Name = "Room1Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task6",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task6",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room7Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room7Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room7Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room7Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room9Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room9Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room9Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room9Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room10Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room10Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room10Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room10Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room10Task5",
                    IsComplete = false,
                    Date = ""
                }
            };

            jsonChores.choreList = choreList;
            ReferenceValues.JsonChoreWeekMasterList = jsonChores;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChores);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
            }
        }
    }

    public void ChoresMonthFromJson(DateTime dateTime) {
        string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_month_" + dateTime.ToString("yyyy_MM") + ".json";

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(fileName);
            string choresListString = null;
            while (!streamReader.EndOfStream) {
                choresListString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (choresListString != null) {
                try {
                    JsonChores jsonChores = JsonSerializer.Deserialize<JsonChores>(choresListString, options);
                    ReferenceValues.JsonChoreMonthMasterList = jsonChores;
                } catch (Exception e) {
                    Console.WriteLine("Failed to Deserialize chores.json..." + e);
                }
            }
        } catch (Exception) {
            JsonChores jsonChores = new();
            ObservableCollection<ChoreDetails> choreList = new() {
                new ChoreDetails {
                    Name = "Room1Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task6",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task7",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task8",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task9",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room1Task10",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room2Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task6",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room3Task7",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task6",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room4Task7",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room6Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room7Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room7Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task5",
                    IsComplete = false,
                    Date = ""
                }
            };

            jsonChores.choreList = choreList;
            ReferenceValues.JsonChoreMonthMasterList = jsonChores;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChores);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
            }
        }
    }
}