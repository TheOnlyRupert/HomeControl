using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class BehaviorFromJson {
    public BehaviorFromJson() {
        ReferenceValues.JsonBehaviorMaster = new JsonBehavior();

        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "behavior.json");
            string behaviorString = null;
            while (!streamReader.EndOfStream) {
                behaviorString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (behaviorString != null) {
                try {
                    JsonBehavior jsonBehavior = JsonSerializer.Deserialize<JsonBehavior>(behaviorString, options);
                    ReferenceValues.JsonBehaviorMaster = jsonBehavior;
                } catch (Exception e) {
                    Console.WriteLine("Failed to Deserialize behavior.json..." + e);
                }
            }
        } catch (Exception) { }
    }
}