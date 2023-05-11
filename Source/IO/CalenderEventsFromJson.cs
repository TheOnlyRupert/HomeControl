using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class CalenderEventsFromJson {
    public CalenderEventsFromJson(DateTime startDate) {
        Directory.CreateDirectory(ReferenceValues.FILE_DIRECTORY + "events/");

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        JsonCalendar[] jsonCalendar = new JsonCalendar[42];

        /* Loop through every file in events folder and see if there are events needing to populate the calendar */
        for (int i = 0; i < 42; i++) {
            foreach (string fileName in Directory.GetFiles(ReferenceValues.FILE_DIRECTORY + "events/", "*.json")) {
                if (fileName.Substring(fileName.Length - 15) == startDate.AddDays(i).ToString("yyyy_MM_dd") + ".json") {
                    try {
                        StreamReader streamReader = new(fileName);
                        string eventsListString = null;
                        while (!streamReader.EndOfStream) {
                            eventsListString = streamReader.ReadToEnd();
                        }

                        streamReader.Close();

                        if (eventsListString != null) {
                            try {
                                JsonCalendar currentJsonCalendar = JsonSerializer.Deserialize<JsonCalendar>(eventsListString, options);

                                if (currentJsonCalendar != null) {
                                    jsonCalendar[i] = currentJsonCalendar;
                                }
                            } catch (Exception e) {
                                ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                                    Date = DateTime.Now,
                                    Level = "WARN",
                                    Module = "CalenderEventsFromJson",
                                    Description = e.ToString()
                                });
                            }
                        }
                    } catch (Exception e) {
                        ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                            Date = DateTime.Now,
                            Level = "WARN",
                            Module = "CalenderEventsFromJson",
                            Description = e.ToString()
                        });
                    }
                }
            }
        }

        ReferenceValues.JsonCalendarMasterEventList = jsonCalendar;
    }
}