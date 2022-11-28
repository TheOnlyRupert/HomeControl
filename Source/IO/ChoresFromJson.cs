using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Helpers;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class ChoresFromJson {
    public ChoresFromJson(DateTime dateTime) {
        string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_week_" + dateTime.ToString("yyyy_MM_dd") + ".json";
        Directory.CreateDirectory(ReferenceValues.FILE_DIRECTORY + "chores/");

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
                    JsonChoresWeek jsonChores = JsonSerializer.Deserialize<JsonChoresWeek>(choresListString, options);
                    ReferenceValues.JsonChoreMasterList = jsonChores;
                } catch (Exception e) {
                    Console.WriteLine("Failed to Deserialize chores.json..." + e);
                }
            }
        } catch (Exception) {
            JsonChoresWeek jsonChoresWeek = new();
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

            jsonChoresWeek.choreList = choreList;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChoresWeek);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                Console.WriteLine("Unable to save " + fileName + "... " + e.Message);
            }
        }
    }
}