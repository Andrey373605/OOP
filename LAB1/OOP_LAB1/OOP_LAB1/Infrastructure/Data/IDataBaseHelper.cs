namespace OOP_LAB1.Infrastructure.Data;

public interface IDataBaseHelper
{
    public void ExecuteNonQuery(string query, Dictionary<string, object> parameters = null);

    public List<Dictionary<string, object>> ExecuteQuery(string query, Dictionary<string, object> parameters = null);

    public int GetLastInsertId();
}