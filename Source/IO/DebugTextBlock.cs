using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class JsonDebug {
    public ObservableCollection<DebugTextBlock> DebugList { get; set; }
}

public class DebugTextBlock {
    public DateTime Date { get; set; }
    public string Level { get; set; }
    public string Module { get; set; }
    public string Description { get; set; }
}

public static class SaveDebugFile {
    public static void Save() {
        try {
            string jsonString = JsonSerializer.Serialize(ReferenceValues.DebugTextBlockOutput);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "debug.json", jsonString);
        } catch (Exception e) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "DebugTextBlock",
                Description = e.ToString()
            });
        }
    }
}