
using Microsoft.Data.Sqlite;

namespace OOP_LAB1.Infrastructure.Data;

public class DatabaseHelper : IDataBaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = new SqliteCommand(query, connection);

        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue($"@{param.Key}", param.Value ?? DBNull.Value);
            }
        }

        command.ExecuteNonQuery();
    }
    
    public List<Dictionary<string, object>> ExecuteQuery(string query, Dictionary<string, object> parameters = null)
    {
        var result = new List<Dictionary<string, object>>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = new SqliteCommand(query, connection);

        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue($"@{param.Key}", param.Value ?? DBNull.Value);
            }
        }

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var row = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader[i];
            }
            result.Add(row);
        }

        return result;
    }
    
    public int GetLastInsertId()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = new SqliteCommand("SELECT last_insert_rowid();", connection);
        var result = command.ExecuteScalar();

        return Convert.ToInt32(result);
    }
    
}