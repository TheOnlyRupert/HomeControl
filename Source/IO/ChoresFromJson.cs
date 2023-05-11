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

    public void ChoresDayFromJson(DateTime dateTime) {
        string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_day_" + dateTime.ToString("yyyy_MM_dd") + ".json";
        string fileNameUser1 = ReferenceValues.FILE_DIRECTORY + "chores/choresUser1_day_" + dateTime.ToString("yyyy_MM_dd") + ".json";

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
                    ReferenceValues.JsonChoreDayMasterList = jsonChores;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoresFromJson",
                        Description = e.ToString()
                    });
                }
            }
        } catch (Exception) {
            JsonChores jsonChores = new();
            ObservableCollection<ChoreDetails> choreList = new() {
                new ChoreDetails {
                    Name = "Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Task4",
                    IsComplete = false,
                    Date = ""
                }
            };

            jsonChores.choreList = choreList;
            ReferenceValues.JsonChoreDayMasterList = jsonChores;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChores);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFromJson",
                    Description = e.ToString()
                });
            }
        }

        try {
            StreamReader streamReader = new(fileNameUser1);
            string choresListString = null;
            while (!streamReader.EndOfStream) {
                choresListString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (choresListString != null) {
                try {
                    JsonChores jsonChores = JsonSerializer.Deserialize<JsonChores>(choresListString, options);
                    ReferenceValues.JsonChoreDayUser1MasterList = jsonChores;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoresFromJson",
                        Description = e.ToString()
                    });
                }
            }
        } catch (Exception) {
            JsonChores jsonChores = new();
            ObservableCollection<ChoreDetails> choreList = new() {
                new ChoreDetails {
                    Name = "Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Task4",
                    IsComplete = false,
                    Date = ""
                }
            };

            jsonChores.choreList = choreList;
            ReferenceValues.JsonChoreDayUser1MasterList = jsonChores;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChores);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileNameUser1, jsonString);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFromJson",
                    Description = e.ToString()
                });
            }
        }
    }

    public void ChoresWeekFromJson(DateTime dateTime) {
        string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_week_" + dateTime.ToString("yyyy_MM_dd") + ".json";
        string fileNameUser1 = ReferenceValues.FILE_DIRECTORY + "chores/choresUser1_week_" + dateTime.ToString("yyyy_MM_dd") + ".json";

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
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoresFromJson",
                        Description = e.ToString()
                    });
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
                },
                new ChoreDetails {
                    Name = "Room11Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room11Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room12Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room12Task2",
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
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFromJson",
                    Description = e.ToString()
                });
            }
        }

        try {
            StreamReader streamReader = new(fileNameUser1);
            string choresListString = null;
            while (!streamReader.EndOfStream) {
                choresListString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (choresListString != null) {
                try {
                    JsonChores jsonChores = JsonSerializer.Deserialize<JsonChores>(choresListString, options);
                    ReferenceValues.JsonChoreWeekUser1MasterList = jsonChores;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoresFromJson",
                        Description = e.ToString()
                    });
                }
            }
        } catch (Exception) {
            JsonChores jsonChores = new();
            ObservableCollection<ChoreDetails> choreList = new() {
                new ChoreDetails {
                    Name = "Room4Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room8Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room13Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room13Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room13Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room14Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room14Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room14Task3",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room14Task4",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room14Task5",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room14Task6",
                    IsComplete = false,
                    Date = ""
                }
            };

            jsonChores.choreList = choreList;
            ReferenceValues.JsonChoreWeekUser1MasterList = jsonChores;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChores);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileNameUser1, jsonString);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFromJson",
                    Description = e.ToString()
                });
            }
        }
    }

    public void ChoresMonthFromJson(DateTime dateTime) {
        string fileName = ReferenceValues.FILE_DIRECTORY + "chores/chores_month_" + dateTime.ToString("yyyy_MM") + ".json";
        string fileNameUser1 = ReferenceValues.FILE_DIRECTORY + "chores/choresUser1_month_" + dateTime.ToString("yyyy_MM") + ".json";

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
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoresFromJson",
                        Description = e.ToString()
                    });
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
                    Name = "Room7Task1",
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
                    Name = "Room11Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room15Task1",
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
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFromJson",
                    Description = e.ToString()
                });
            }
        }

        try {
            StreamReader streamReader = new(fileNameUser1);
            string choresListString = null;
            while (!streamReader.EndOfStream) {
                choresListString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (choresListString != null) {
                try {
                    JsonChores jsonChores = JsonSerializer.Deserialize<JsonChores>(choresListString, options);
                    ReferenceValues.JsonChoreMonthUser1MasterList = jsonChores;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoresFromJson",
                        Description = e.ToString()
                    });
                }
            }
        } catch (Exception) {
            JsonChores jsonChores = new();
            ObservableCollection<ChoreDetails> choreList = new() {
                new ChoreDetails {
                    Name = "Room15Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room15Task2",
                    IsComplete = false,
                    Date = ""
                }
            };

            jsonChores.choreList = choreList;
            ReferenceValues.JsonChoreMonthUser1MasterList = jsonChores;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChores);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileNameUser1, jsonString);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFromJson",
                    Description = e.ToString()
                });
            }
        }
    }

    public void ChoresQuarterFromJson(DateTime dateTime) {
        string fileName = dateTime.Month switch {
            > 0 and < 3 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter1_" + dateTime.ToString("yyyy") + ".json",
            > 2 and < 6 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter2_" + dateTime.ToString("yyyy") + ".json",
            > 5 and < 9 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter3_" + dateTime.ToString("yyyy") + ".json",
            _ => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter4_" + dateTime.ToString("yyyy") + ".json"
        };

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
                    ReferenceValues.JsonChoreQuarterMasterList = jsonChores;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoresFromJson",
                        Description = e.ToString()
                    });
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
                    Name = "Room4Task8",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room5Task1",
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
                    Name = "Room11Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room11Task2",
                    IsComplete = false,
                    Date = ""
                }
            };

            jsonChores.choreList = choreList;
            ReferenceValues.JsonChoreQuarterMasterList = jsonChores;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChores);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFromJson",
                    Description = e.ToString()
                });
            }
        }

        fileName = dateTime.Month switch {
            > 0 and < 3 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter1User1_" + dateTime.ToString("yyyy") + ".json",
            > 2 and < 6 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter2User1_" + dateTime.ToString("yyyy") + ".json",
            > 5 and < 9 => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter3User1_" + dateTime.ToString("yyyy") + ".json",
            _ => ReferenceValues.FILE_DIRECTORY + "chores/chores_quarter4User1_" + dateTime.ToString("yyyy") + ".json"
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
                    ReferenceValues.JsonChoreQuarterUser1MasterList = jsonChores;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "ChoresFromJson",
                        Description = e.ToString()
                    });
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
                    Name = "Room11Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room11Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room12Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room12Task2",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room16Task1",
                    IsComplete = false,
                    Date = ""
                },
                new ChoreDetails {
                    Name = "Room16Task2",
                    IsComplete = false,
                    Date = ""
                }
            };

            jsonChores.choreList = choreList;
            ReferenceValues.JsonChoreQuarterUser1MasterList = jsonChores;

            try {
                string jsonString = JsonSerializer.Serialize(jsonChores);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e) {
                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                    Date = DateTime.Now,
                    Level = "WARN",
                    Module = "ChoresFromJson",
                    Description = e.ToString()
                });
            }
        }
    }
}