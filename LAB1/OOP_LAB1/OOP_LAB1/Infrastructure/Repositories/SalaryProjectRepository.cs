using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class SalaryProjectRepository : ISalaryProjectRepository
{
    private readonly IDataBaseHelper _dataBaseHelper;

    public SalaryProjectRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }

    public async Task AddAsync(SalaryProject salaryProject)
    {
        var query = @"
            INSERT INTO SalaryProject (EnterpriseId, Balance, BankId, Status)
            VALUES (@EnterpriseId, @Balance, @BankId, @Status)";

        var parameters = new Dictionary<string, object>
        {
            ["EnterpriseId"] = salaryProject.EnterpriseId,
            ["Balance"] = salaryProject.Balance,
            ["BankId"] = salaryProject.BankId,
            ["Status"] = (int)salaryProject.Status
        };

        _dataBaseHelper.ExecuteNonQuery(query, parameters);

        salaryProject.Id = _dataBaseHelper.GetLastInsertId();
    }

    public async Task<SalaryProject> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM SalaryProject WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { ["Id"] = id };

        var result = _dataBaseHelper.ExecuteQuery(query, parameters).FirstOrDefault();

        if (result == null)
            return null;

        return new SalaryProject
        {
            Id = Convert.ToInt32(result["Id"]),
            EnterpriseId = Convert.ToInt32(result["EnterpriseId"]),
            Balance = Convert.ToDecimal(result["Balance"]),
            BankId = Convert.ToInt32(result["BankId"]),
            Status = (SalaryProjectStatus)Convert.ToInt32(result["Status"])
        };
    }

    public async Task UpdateAsync(SalaryProject salaryProject)
    {
        var query = @"
            UPDATE SalaryProject
            SET EnterpriseId = @EnterpriseId, Balance = @Balance, BankId = @BankId, Status = @Status
            WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            ["Id"] = salaryProject.Id,
            ["EnterpriseId"] = salaryProject.EnterpriseId,
            ["Balance"] = salaryProject.Balance,
            ["BankId"] = salaryProject.BankId,
            ["Status"] = (int)salaryProject.Status
        };

        _dataBaseHelper.ExecuteNonQuery(query, parameters);
    }

    public async Task AddAccountToSalaryProjectAsync(SalaryProject project, Account account, decimal salary)
    {
        var query = @"
            INSERT INTO Salary (AccountId, SalaryProjectId, Amount)
            VALUES (@AccountId, @SalaryProjectId, @Amount)";

        var parameters = new Dictionary<string, object>
        {
            ["AccountId"] = account.Id,
            ["SalaryProjectId"] = project.Id,
            ["Amount"] = salary
        };

        _dataBaseHelper.ExecuteNonQuery(query, parameters);
    }

    
    public async Task UpdateSalaryAsync(SalaryProject project, Account account, decimal amount)
    {
        var query = @"
            UPDATE Salary
            SET Amount = @Amount
            WHERE AccountId = @AccountId AND SalaryProjectId = @SalaryProjectId";

        var parameters = new Dictionary<string, object>
        {
            ["AccountId"] = account.Id,
            ["SalaryProjectId"] = project.Id,
            ["Amount"] = amount
        };

        _dataBaseHelper.ExecuteNonQuery(query, parameters);
    }

    public async Task<IEnumerable<Salary>> GetSalaries(int projectId)
    {
        var query = "SELECT * FROM Salary WHERE SalaryProjectId = @SalaryProjectId";
        var parameters = new Dictionary<string, object> { ["SalaryProjectId"] = projectId };

        var results = _dataBaseHelper.ExecuteQuery(query, parameters);

        return results.Select(result => new Salary
        {
            Id = Convert.ToInt32(result["Id"]),
            AccountId = Convert.ToInt32(result["AccountId"]),
            SalaryProjectId = Convert.ToInt32(result["SalaryProjectId"]),
            Amount = Convert.ToDecimal(result["Amount"])
        });
    }
}