using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    IDataBaseHelper _databaseHelper;

    public UserRepository(IDataBaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }
    
    public async Task AddAsync(User user)
    {
        string query = @"INSERT INTO User 
                         (Email, HashPassword) 
                         VALUES 
                         (@Email, @HashPassword)";

        var parameters = new Dictionary<string, object>
        {
            {"Email", user.Email},
            {"HashPassword", user.HashPassword}
        };

        await Task.Run(() => _databaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<User> GetByIdAsync(int id)
    {
        string query = @"SELECT Id, Email, HashPassword 
                         FROM User 
                         WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            {"Id", id}
        };

        var result = await Task.Run(() => _databaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если пользователь не найден
        }

        var row = result[0];
        
        var user = new User
        {
            Id = Convert.ToInt32(row["Id"]),
            Email = row["Email"].ToString(),
            HashPassword = row["HashPassword"].ToString()
        };

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        string query = @"UPDATE User 
                         SET Email = @Email, 
                             HashPassword = @HashPassword 
                         WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            {"Id", user.Id},
            {"Email", user.Email},
            {"HashPassword", user.HashPassword}
        };

        await Task.Run(() => _databaseHelper.ExecuteNonQuery(query, parameters));
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        string query = @"SELECT Id, Email, HashPassword 
                         FROM User 
                         WHERE Email = @Email";

        var parameters = new Dictionary<string, object>
        {
            {"Email", email}
        };

        var result = await Task.Run(() => _databaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если пользователь не найден
        }

        var row = result[0];
        
        var user = new User
        {
            Id = Convert.ToInt32(row["Id"]),
            Email = row["Email"].ToString(),
            HashPassword = row["HashPassword"].ToString()
        };

        return user;
    }
}