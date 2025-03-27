using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace GameDevLab
{
    public static class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=GameDevLab.db";

        public static void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Projects (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Path TEXT NOT NULL
                );";

            using var command = new SqliteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        public static void AddProject(string name, string description, string path)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string insertQuery = "INSERT INTO Projects (Name, Description, Path) VALUES (@Name, @Description, @Path);";
            using var command = new SqliteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@Path", path);
            command.ExecuteNonQuery();
        }

        public static List<(int Id, string Name, string Description, string Path)> GetProjects()
        {
            var projects = new List<(int, string, string, string)>();

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string selectQuery = "SELECT Id, Name, Description, Path FROM Projects;";
            using var command = new SqliteCommand(selectQuery, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                projects.Add((
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    reader.GetString(3)
                ));
            }

            return projects;
        }

        public static void DeleteProject(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string deleteQuery = "DELETE FROM Projects WHERE Id = @Id;";
            using var command = new SqliteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }
}