namespace OOP_LAB1.Infrastructure.Data;

using Microsoft.Data.Sqlite;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Initialize()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        CreateBanksTable(connection);

    }

    private void CreateBanksTable(SqliteConnection connection)
    {
        var command = connection.CreateCommand();
        command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Banks 
                (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL UNIQUE
                );
                    ";

        command.ExecuteNonQuery();
    }

    
}