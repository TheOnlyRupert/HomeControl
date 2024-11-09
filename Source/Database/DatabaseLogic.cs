using System;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Task = System.Threading.Tasks.Task;

namespace HomeControl.Source.Database;

public static class DatabaseLogic {
    public static async Task GetTableByName(string tableName) {
        string connectionString = @"Server=" + ReferenceValues.JsonSettingsMaster.DatabaseHost + @";Database=HomeControl;Uid=" + ReferenceValues.JsonSettingsMaster.DatabaseUsername + @";Pwd=" +
                                  ReferenceValues.JsonSettingsMaster.DatabasePassword;
        MySqlConnection connection = new(connectionString);
        connection.CreateCommand();
       
        try {
            await connection.OpenAsync();
            
            string query = "SELECT * FROM " + tableName;
            MySqlCommand command = new(query, connection);

            DbDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) {
                for (int i = 0; i < reader.FieldCount; i++) {
                    Console.Write($@"{reader.GetName(i)}:{reader.GetValue(i)};");
                }

                Console.WriteLine();
            }
        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }
}