using System;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Timers;
using HomeControl.Source.ViewModel.Base;
using MySql.Data.MySqlClient;

namespace HomeControl.Source.Database;

public static class DatabasePolling {
    private static Timer _timer;
    private static int _lastVersion;
    private static CrossViewMessenger _simpleMessenger;

    public static void StartPolling() {
        _timer = new Timer(10000);
        _timer.Elapsed += CheckForChanges;
        _timer.Start();
    }

    public static void CheckForChanges(object sender, ElapsedEventArgs e) {
        _simpleMessenger = CrossViewMessenger.Instance;

        try {
            using MySqlConnection connection = new(ReferenceValues.DatabaseConnectionString);
            connection.Open();

            /* Behavior - Use this for initial setup also */
            using MySqlCommand command = new("SELECT version FROM versions WHERE id = 1", connection);
            int currentVersion = Convert.ToInt32(command.ExecuteScalar());

            if (currentVersion > _lastVersion) {
                Console.WriteLine(@"Database updated... Retrieving data from behavior.");
                const string query = "SELECT * FROM behavior";
                MySqlCommand command2 = new(query, connection);

                DbDataReader reader = command2.ExecuteReader();

                while (reader.Read()) {
                    string input = "";

                    for (int i = 0; i < reader.FieldCount; i++) {
                        input += $@"{reader.GetName(i)}:{reader.GetValue(i)};";
                    }

                    // Split the input into lines
                    string[] lines = input.Split(['\n'], StringSplitOptions.RemoveEmptyEntries);

                    const string pattern = @"id:(\d+);stars:(\d+);strikes:(\d+);";

                    foreach (string line in lines) {
                        Match match = Regex.Match(line, pattern);

                        if (match.Success) {
                            int id = int.Parse(match.Groups[1].Value);
                            byte stars = byte.Parse(match.Groups[2].Value);
                            byte strikes = byte.Parse(match.Groups[3].Value);

                            switch (id) {
                            case 1:
                                ReferenceValues.JsonBehaviorMaster.User1Stars = stars;
                                ReferenceValues.JsonBehaviorMaster.User1Strikes = strikes;
                                break;
                            case 2:
                                ReferenceValues.JsonBehaviorMaster.User2Stars = stars;
                                ReferenceValues.JsonBehaviorMaster.User2Strikes = strikes;
                                break;
                            case 3:
                                ReferenceValues.JsonBehaviorMaster.User3Stars = stars;
                                ReferenceValues.JsonBehaviorMaster.User3Strikes = strikes;
                                break;
                            case 4:
                                ReferenceValues.JsonBehaviorMaster.User4Stars = stars;
                                ReferenceValues.JsonBehaviorMaster.User4Strikes = strikes;
                                break;
                            case 5:
                                ReferenceValues.JsonBehaviorMaster.User5Stars = stars;
                                ReferenceValues.JsonBehaviorMaster.User5Strikes = strikes;
                                break;
                            }
                        }
                    }

                    _simpleMessenger.PushMessage("RefreshBehavior", null);
                    _lastVersion = currentVersion;
                }
            }
        } catch (Exception) {
            Console.WriteLine(@"Unable to connect to database");
        }
    }

    public static void StopPolling() {
        _timer.Stop();
    }
}