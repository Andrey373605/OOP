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
    public async Task AddAsync(Employee employee)
    {
        string query = @"INSERT INTO Employee 
                         (UserId, BankId, Role) 
                         VALUES 
                         (@UserId, @BankId, @Role);";

        var parameters = new Dictionary<string, object>
        {
            {"UserId", employee.UserId},
            {"BankId", employee.BankId},
            {"Role", employee.Role},
            {"Status", employee.Status}
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public Task DeleteAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        string query = @"SELECT Id, UserId, BankId, Role 
                         FROM Employee 
                         WHERE Id = @Id;";

        var parameters = new Dictionary<string, object>
        {
            {"Id", id}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        if (result.Count == 0)
        {
            return null; // Если клиент не найден
        }

        var row = result[0];

        var employee = new Employee
        {
            Id = Convert.ToInt32(row["Id"]),
            UserId = Convert.ToInt32(row["UserId"]),
            BankId = Convert.ToInt32(row["BankId"]),
            Role = (EmployeeRole)Convert.ToInt32(row["Role"]),
            Status = (EmployeeStatus)Convert.ToInt32(row["Status"]),
        };

        return employee;
    }

    public async Task UpdateAsync(Employee employee)
    {
        string query = @"UPDATE Employee 
                         SET UserId = @UserId, 
                             BankId = @BankId, 
                             Role = @Role,
                         WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            {"Id", employee.Id},
            {"BankId", employee.BankId},
            {"UserId", employee.UserId},
            {"Role", employee.Role},
            {"Status", employee.Status}
        };

        await Task.Run(() => _dataBaseHelper.ExecuteNonQuery(query, parameters));
    }
    
    public async Task<Employee> GetEmployeeByUserIdAsync(int bankId, int userId)
    {
        string query = @"SELECT Id, UserId, BankId, Role, Status
                         FROM Employee 
                         WHERE BankId = @BankId AND UserId = @UserId AND Status = @Status";

        var parameters = new Dictionary<string, object>
        {
            {"BankId", bankId},
            {"UserId", userId},
            {"Status", (int)EmployeeStatus.Active}
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
            Role = (EmployeeRole)Convert.ToInt32(row["Role"]),
            Status = (EmployeeStatus)Convert.ToInt32(row["Status"]),
        };

        return employee;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        string query = @"SELECT Id, UserId, BankId, Role, Status
                         FROM Employee ";

        var parameters = new Dictionary<string, object>
        {
            {"Status", (int)EmployeeStatus.Active}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        var employees = new List<Employee>();

        foreach (var row in result)
        {
            employees.Add(new Employee
            {
                Id = Convert.ToInt32(row["Id"]),
                UserId = Convert.ToInt32(row["UserId"]),
                BankId = Convert.ToInt32(row["BankId"]),
                Role = (EmployeeRole)Convert.ToInt32(row["Role"]),
                Status = (EmployeeStatus)Convert.ToInt32(row["Status"])
            });
        }
        return employees;
    }

    public async Task<IEnumerable<Employee>> GetEmployeeRequestsAsync()
    {
        string query = @"SELECT Id, UserId, BankId, Role, Status
                         FROM Employee ";

        var parameters = new Dictionary<string, object>
        {
            {"Status", (int)EmployeeStatus.Application}
        };

        var result = await Task.Run(() => _dataBaseHelper.ExecuteQuery(query, parameters));

        var employees = new List<Employee>();

        foreach (var row in result)
        {
            employees.Add(new Employee
            {
                Id = Convert.ToInt32(row["Id"]),
                UserId = Convert.ToInt32(row["UserId"]),
                BankId = Convert.ToInt32(row["BankId"]),
                Role = (EmployeeRole)Convert.ToInt32(row["Role"]),
                Status = (EmployeeStatus)Convert.ToInt32(row["Status"])
            });
        }
        return employees;
    }
}