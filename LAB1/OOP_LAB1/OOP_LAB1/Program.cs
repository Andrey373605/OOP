using OOP_LAB1.Infrastructure.Data;

DatabaseInitializer ini = new DatabaseInitializer("Data Source=sample.db");
DatabaseHelper helper = new DatabaseHelper("Data Source=sample.db");
var query = "INSERT INTO Banks (id, name) VALUES (@id, @name)";
var parameters = new Dictionary<string, object>
{
    { "id", 1 },
    { "name", "Example Bank" }
};
helper.ExecuteNonQuery(query, parameters);
ini.Initialize();
