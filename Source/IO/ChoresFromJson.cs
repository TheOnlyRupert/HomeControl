using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class TasksFromJson {
    public TasksFromJson() {
        ReferenceValues.JsonTasksMaster = new JsonTasks();

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "tasks.json");
            string tasksString = null;
            while (!streamReader.EndOfStream) {
                tasksString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (tasksString != null) {
                try {
                    JsonTasks jsonTasks = JsonSerializer.Deserialize<JsonTasks>(tasksString, options);
                    ReferenceValues.JsonTasksMaster = jsonTasks;
                } catch (Exception e) {
                    ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                        Date = DateTime.Now,
                        Level = "WARN",
                        Module = "TasksFromJson",
                        Description = e.ToString()
                    });
                    SaveDebugFile.Save();
                }
            }
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "TasksFromJson",
                Description = e.ToString()
            });
            SaveDebugFile.Save();
        }
    }
}