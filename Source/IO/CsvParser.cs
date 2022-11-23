using System;
using System.IO;
using System.Windows;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO;

public static class CsvParser {
    static CsvParser() {
        try {
            Directory.CreateDirectory(ReferenceValues.FILE_DIRECTORY + "events/");

            if (!File.Exists(ReferenceValues.FILE_DIRECTORY + "settings.csv")) {
                Console.WriteLine("settings.csv does not exist. Restoring default settings");
                StreamWriter file = new(ReferenceValues.FILE_DIRECTORY + "settings.csv", true);
                file.WriteLine("!KEY,VALUE");
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

    public static string[] GetSettings() {
        StreamReader streamReader = new(ReferenceValues.FILE_DIRECTORY + "settings.csv");
        string[] values = { "null", "null", "null", "null", "null", "null" };

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
                case "User1Name":
                    try {
                        values[1] = strArray[1];
                    } catch (Exception) {
                        Console.WriteLine("Error when reading value of \"User1Name\" in settings.csv, using default settings");
                    }

                    break;

                case "User2Name":
                    try {
                        values[2] = strArray[1];
                    } catch (Exception) {
                        Console.WriteLine("Error when reading value of \"User2Name\" in settings.csv, using default settings");
                    }

                    break;
                case "Child1Name":
                    try {
                        values[3] = strArray[1];
                    } catch (Exception) {
                        Console.WriteLine("Error when reading value of \"Child1Name\" in settings.csv, using default settings");
                    }

                    break;
                case "Child2Name":
                    try {
                        values[4] = strArray[1];
                    } catch (Exception) {
                        Console.WriteLine("Error when reading value of \"Child2Name\" in settings.csv, using default settings");
                    }

                    break;
                case "Child3Name":
                    try {
                        values[5] = strArray[1];
                    } catch (Exception) {
                        Console.WriteLine("Error when reading value of \"Child3Name\" in settings.csv, using default settings");
                    }

                    break;
                }
            }
        }

        return values;
    }

    public static void SaveSettings() {
        try {
            File.WriteAllText(ReferenceValues.FILE_DIRECTORY + "settings.csv", "!KEY,VALUE\nUserAgent," + ReferenceValues.UserAgent.Trim()
                                                                                                        + "\nUser1Name," + ReferenceValues.User1Name + "\nUser2Name," +
                                                                                                        ReferenceValues.User2Name + "\nChild1Name," + ReferenceValues.Child1Name
                                                                                                        + "\nChild2Name," + ReferenceValues.Child2Name + "\nChild3Name," +
                                                                                                        ReferenceValues.Child3Name);
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