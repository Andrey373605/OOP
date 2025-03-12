using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    
    IDataBaseHelper _dataBaseHelper;

    public EmployeeRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }
    public Task AddAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Employee employee)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Employee> GetEmployeeByUserIdAsync(int bankId, int userId)
    {
        string query = @"SELECT Id, UserId, BankId, Role 
                         FROM Employee 
                         WHERE BankId = @BankId AND UserId = @UserId";

        var parameters = new Dictionary<string, object>
        {
            {"BankId", bankId},
            {"UserId", userId}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если сотрудник не найден
        }

        var row = result[0];

        var employee = new Employee
        {
            Id = Convert.ToInt32(row["Id"]),
            UserId = Convert.ToInt32(row["UserId"]),
            BankId = Convert.ToInt32(row["BankId"]),
            Role = (EmployeeRole)Convert.ToInt32(row["Role"])
        };

        return employee;
    }
}