using System;
using System.IO;
using System.Text.Json;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public class FinancesFromJson {
    public FinancesFromJson() {
        JsonSerializerOptions options = new() {
            IncludeFields = true
        };

        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "finances.json");
            string financeListString = null;
            while (!streamReader.EndOfStream) {
                financeListString = streamReader.ReadToEnd();
            }

            streamReader.Close();

            if (financeListString != null) {
                try {
                    JsonFinances jsonFinances = JsonSerializer.Deserialize<JsonFinances>(financeListString, options);
                    ReferenceValues.JsonFinanceMasterList = jsonFinances;
                } catch (Exception e) {
                    Console.WriteLine("Failed to Deserialize finances.json..." + e);
                }
            }
        } catch (Exception) { }
    }
}