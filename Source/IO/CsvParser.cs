using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using HomeControl.Source.Helpers;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public static class CsvParser {
    private static readonly ObservableCollection<FinanceBlock> _list = new();

    static CsvParser() {
        try {
            Directory.CreateDirectory(ReferenceValues.FILE_DIRECTORY + "events/");

            if (!File.Exists(ReferenceValues.FILE_DIRECTORY + "settings.csv")) {
                Console.WriteLine("settings.csv does not exist. Restoring default settings");
                StreamWriter file = new(ReferenceValues.FILE_DIRECTORY + "settings.csv", true);
                file.WriteLine("!KEY,VALUE\nUserAgent,");
                file.Close();
            }

            if (!File.Exists(ReferenceValues.FILE_DIRECTORY + "finances2022.csv")) {
                Console.WriteLine("finances2022.csv does not exist. Restoring default settings");
                StreamWriter file = new(ReferenceValues.FILE_DIRECTORY + "finances2022.csv", true);
                file.WriteLine("!ADDSUB,DATE,ITEM,AMOUNT,CATEGORY,PERSON");
                file.Close();
            }

            if (!File.Exists(ReferenceValues.FILE_DIRECTORY + "behavior.csv")) {
                Console.WriteLine("behavior.csv does not exist. Restoring default settings");
                StreamWriter file = new(ReferenceValues.FILE_DIRECTORY + "behavior.csv", true);
                file.WriteLine("!KEY,VALUE");
                file.Close();
            }
        } catch (Exception) {
            MessageBox.Show("Error! Unable to create program files in directory.\n" +
                            "You must correct this issue and try running the program again.", "Fatal Error");
            Environment.Exit(0);
        }
    }

    public static ObservableCollection<FinanceBlock> GetFinanceList() {
        try {
            StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "finances2022.csv");
            while (!streamReader.EndOfStream) {
                string str = streamReader.ReadLine();

                if (str != null && str[0] != '!') {
                    string[] strArray = str.Split(',');

                    _list.Add(new FinanceBlock {
                        AddSub = strArray[0],
                        Date = strArray[1],
                        Item = strArray[2],
                        Cost = strArray[3],
                        Category = strArray[4],
                        Person = strArray[5]
                    });
                }
            }

            streamReader.Close();
        } catch (Exception) {
            MessageBox.Show("Error! finances2022.csv file error.", "Error");
        }

        return _list;
    }

    public static void AddFiance(string line) {
        try {
            StreamWriter file = new(ReferenceValues.FILE_DIRECTORY + "finances2022.csv", true);
            file.WriteLine(line);
            file.Close();
        } catch (Exception) {
            MessageBox.Show("Error! Unable to save finances2022.csv file.", "Error");
        }
    }

    public static string[] GetSettings() {
        StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "settings.csv");
        string[] values = new string[1];

        while (!streamReader.EndOfStream) {
            string str = streamReader.ReadLine();

            if (str != null && str[0] != '!') {
                string[] strArray = str.Split(',');
                switch (strArray[0]) {
                case "UserAgent":
                    try {
                        values[0] = strArray[1];
                    } catch (Exception) {
                        Console.WriteLine("Error when reading value of \"UserAgent\" in settings.csv, using default settings");
                    }

                    break;
                }
            }
        }

        return values;
    }

    public static void SaveSettings() {
        try {
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "settings.csv", "!KEY,!VALUE\nUserAgent," + ReferenceValues.UserAgent.Trim());
        } catch (Exception e) {
            Console.WriteLine("Unable to save settings.csv... " + e.Message);
        }
    }

    public static string[] GetBehavior() {
        StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "behavior.csv");
        string[] values = new string[1];

        while (!streamReader.EndOfStream) {
            string str = streamReader.ReadLine();

            if (str != null && str[0] != '!') {
                string[] strArray = str.Split(',');
                switch (strArray[0]) {
                case "UserAgent":
                    try {
                        values[0] = strArray[1];
                    } catch (Exception) {
                        values[0] = "null";
                        Console.WriteLine("Error when reading value of \"UserAgent\" in settings.csv, using default settings");
                    }

                    break;
                case "Child1Name":
                    try {
                        values[1] = strArray[1];
                    } catch (Exception) {
                        values[1] = "null";
                        Console.WriteLine("Error when reading value of \"Child1Name\" in settings.csv, using default settings");
                    }

                    break;
                case "Child2Name":
                    try {
                        values[2] = strArray[2];
                    } catch (Exception) {
                        values[2] = "null";
                        Console.WriteLine("Error when reading value of \"Child2Name\" in settings.csv, using default settings");
                    }

                    break;
                case "Child3Name":
                    try {
                        values[3] = strArray[3];
                    } catch (Exception) {
                        values[3] = "null";
                        Console.WriteLine("Error when reading value of \"Child3Name\" in settings.csv, using default settings");
                    }

                    break;
                }
            }
        }

        return values;
    }

    public static void SaveBehavior() {
        try {
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "behavior.csv", "!KEY,!VALUE\nChild1Name," + ReferenceValues.Child1Name.Trim());
        } catch (Exception e) {
            Console.WriteLine("Unable to save settings.csv... " + e.Message);
        }
    }
}